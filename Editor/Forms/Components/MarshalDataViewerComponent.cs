using Editor.CustomMarshal;
using Editor.UI;
using EVESharp.PythonTypes;
using EVESharp.PythonTypes.Compression;
using EVESharp.PythonTypes.Marshal;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfHexaEditor.Core.EventArguments;

namespace Editor.Forms.Components
{
    public partial class MarshalDataViewerComponent : UserControl
    {
        private WpfHexaEditor.HexEditor hexView = null;
        private BackgroundWorker mWorker;
        public InsightUnmarshal Unmarshaler { get; private set; } = null;
        public EventHandler OnLoadCompleted;
        public EventHandler<string> OnUnmarshalError;
        private byte[] mByteData = null;
        private Stream mOrigin = null;
        private string mPretty = "";
        private TreeNode mPacketNode = null;
        private TreeNode mInsightsNode = null;
        private bool mWasCompressed = false;

        private MarshalDataViewerComponent()
        {
            InitializeComponent();

            // setup hex views as the editor removes the setup for some absurd reason
            this.hexView = new WpfHexaEditor.HexEditor();
            // add it to the parent
            this.hexViewHost.Child = this.hexView;
            // setup events
            this.hexView.ByteClick += SelectByte;
        }

        public MarshalDataViewerComponent(Stream stream) : this()
        {
            this.mOrigin = stream;
            this.SetupWorker();
        }

        public MarshalDataViewerComponent(byte[] byteData) : this()
        {
            this.mByteData = byteData;
            this.SetupWorker();
        }

        private void SetupWorker()
        {
            this.mWorker = new BackgroundWorker();
            this.mWorker.WorkerSupportsCancellation = false;
            this.mWorker.WorkerReportsProgress = true;
            this.mWorker.DoWork += DoUnmarshal;
            this.mWorker.ProgressChanged += UnmarshalProgress;
            this.mWorker.RunWorkerCompleted += UnmarshalCompleted;
        }

        private void UnmarshalProgress(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 50)
            {
                // partial unmarshal was achieved, inform about it
                this.OnUnmarshalError?.Invoke(this, "Cannot completely unmarshal selected data!");
            }
        }

        private void UnmarshalCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // update the tree view
            // disable painting while this works
            this.insightTreeView.BeginUpdate();

            // ensure the treeViews are empty before starting
            this.insightTreeView.Nodes.Clear();
            // add the compression indicator
            this.insightTreeView.Nodes.Add("Compression: " + this.mWasCompressed);

            // setup the corresponding boxes with the right data
            this.packetTextBox.Text = this.mPretty;
            this.marshalTreeView.SetPacketData(this.mPacketNode);

            // insight is a bit more complex and requires some manual work on it
            foreach (TreeNode entry in this.mInsightsNode.Nodes)
                this.insightTreeView.Nodes.Add(entry);

            this.hexView.Stream = new MemoryStream(this.mByteData);
            this.hexView.RefreshView();
            this.OnLoadCompleted?.Invoke(this, null);

            // re-enable painting
            this.insightTreeView.EndUpdate();
            // re-enable the form too
            this.Enabled = true;
        }

        private void DoUnmarshal(object sender, EventArgs e)
        {
            // if the data comes from a stream ensure the stream is read here too
            if (this.mByteData is null)
                using (this.mOrigin)
                {
                    this.mOrigin.Seek(0, SeekOrigin.End);
                    long length = this.mOrigin.Position;
                    this.mOrigin.Seek(0, SeekOrigin.Begin);
                    this.mByteData = new byte[length];
                    this.mOrigin.Read(this.mByteData, 0, (int) length);
                }
                    
            // if first byte is an x that means we have a compressed stream
            // this has costed me ONE hour of wondering why the fuck my data was not long enough, turns out I don't even remember the specification of what i'm working on...
            if (this.mByteData[0] == Specification.ZLIB_HEADER)
            {
                // decompress the stream and mark it as compressed so it can be reflected in the treelist
                this.mWasCompressed = true;
                MemoryStream memoryStream = new MemoryStream(this.mByteData);
                MemoryStream outputStream = new MemoryStream();
                ZInputStream stream = ZlibHelper.DecompressStream(memoryStream);
                stream.CopyTo(outputStream);
                // now convert the outputStream to a byte array and use it instead
                this.mByteData = outputStream.ToArray();
            }
            try
            {
                this.Unmarshaler = InsightUnmarshal.ReadFromByteArray(this.mByteData);
            }
            catch (UnmarshallException ex)
            {
                this.Unmarshaler = ex.Unmarshal;
                // signal some processso the ui can reflect that the packet might be incomplete
                this.mWorker.ReportProgress(50);
            }

            // generate the pretty-printed versions of the data
            this.mPretty = CustomPrettyPrinter.FromDataType(this.Unmarshaler.Output);
            TreeViewPrettyPrinter.Process(this.Unmarshaler.Output, out this.mPacketNode);
            InsightPrettyPrinter.Process(this.mByteData, this.Unmarshaler, out this.mInsightsNode);
        }

        public void PrepareDataAsync()
        {
            this.mWorker.RunWorkerAsync();
        }

        private void SelectByte(object sender, ByteEventArgs e)
        {
            long point = e.BytePositionInStream;

            foreach (InsightEntry entry in this.Unmarshaler.Insight)
            {
                if (entry.StartPosition <= point && entry.EndPosition >= point)
                {
                    this.insightTreeView.SelectedNode = entry.TreeNode;
                    entry.TreeNode.EnsureVisible();
                    entry.TreeNode.Expand();
                    break;
                }
            }
        }

        private void SelectInsightElement(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (InsightEntry entry in this.Unmarshaler.Insight)
            {
                if (entry.TreeNode == e.Node)
                {
                    this.hexView.SelectionStart = entry.StartPosition;
                    this.hexView.SelectionStop = entry.EndPosition;
                    break;
                }
            }
        }

        private void SelectInsightNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (InsightEntry entry in this.Unmarshaler.Insight)
            {
                if (entry.TreeNode == e.Node)
                {
                    this.hexView.SelectionStart = entry.StartPosition;
                    this.hexView.SelectionStop = entry.EndPosition;
                    break;
                }
            }
        }

        private void ApplyChangesAndReload(object sender, EventArgs e)
        {
            // disable the window so the user cannot interact with it
            this.Enabled = false;
            // update the byte data (this should not fail because MemoryStream)
            this.hexView.SubmitChanges();
            // initialize the worker again
            this.PrepareDataAsync();
        }
    }
}
