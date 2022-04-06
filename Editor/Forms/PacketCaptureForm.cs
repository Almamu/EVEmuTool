using Editor.Capture;
using Editor.CustomMarshal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Forms
{
    public partial class PacketCaptureForm : Form
    {
        private CaptureProcessor mProcessor;
        private BindingSource mDataSource = new BindingSource();
        private List<CaptureEntry> mEntries = new List<CaptureEntry>();
        private BackgroundWorker mWorker = null;
        private byte[] mByteData = null;
        private TreeNode mTreeNode = null;

        public CaptureProcessor Processor
        {
            get => this.mProcessor;
            set
            {
                // ensure the old processor doesn't now about us
                if (this.mProcessor is not null)
                    this.mProcessor.OnPacketCaptured -= OnPacketCaptured;

                this.mProcessor = value;

                // if there's a new processor ensure the event is updated
                if (this.mProcessor is not null)
                    this.mProcessor.OnPacketCaptured += OnPacketCaptured;
            }
        }
        public PacketCaptureForm()
        {
            InitializeComponent();
            ExtendComponents();

            // setup the events
            this.mWorker = new BackgroundWorker();
            this.mWorker.WorkerSupportsCancellation = false;
            this.mWorker.WorkerReportsProgress = true;
            this.mWorker.DoWork += DoUnmarshal;
            this.mWorker.RunWorkerCompleted += UnmarshalCompleted;
        }
        private void UnmarshalCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.marshalTreeView.SetPacketData(this.mTreeNode);
        }

        private void DoUnmarshal(object sender, EventArgs e)
        {
            InsightUnmarshal unmarshaler;

            try
            {
                unmarshaler = PartialUnmarshal.ReadFromByteArray(this.mByteData);
            }
            catch (UnmarshallException ex)
            {
                unmarshaler = ex.Unmarshal;
            }

            // generate the pretty-printed versions of the data
            TreeViewPrettyPrinter.Process(unmarshaler.Output, out this.mTreeNode);
        }

        private void ExtendComponents()
        {
            // setup extra things in the components
            this.packetGridView.AutoGenerateColumns = false;
            this.packetGridView.DataSource = this.mDataSource;
            this.packetGridView.MultiSelect = false;
        }

        private void OnPacketCaptured(object sender, CaptureEntry packet)
        {
            // if this is called from another thread we need to invoke it to ensure it runs in the right place
            if (this.InvokeRequired)
            {
                Invoke(OnPacketCaptured, new object[] { sender, packet });
                return;
            }

            // keep track of all the packet data we have
            this.mEntries.Add(packet);

            // process filters and add the packet to the current visible list
            this.mDataSource.Add(packet);
        }

        private void CaptureWindowClosing(object sender, FormClosingEventArgs e)
        {
            // if the main form is being closed we can close too
            if (e.CloseReason == CloseReason.MdiFormClosing)
                return;

            // prevent the window from being completely closed
            e.Cancel = true;
            this.Hide();
        }

        private void OpenInPacketViewer(object sender, DataGridViewCellEventArgs e)
        {
            this.OpenInPacketViewer(this.mDataSource[e.RowIndex] as CaptureEntry);
        }

        private void OpenInPacketViewer(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.OpenInPacketViewer(this.mDataSource[e.RowIndex] as CaptureEntry);
        }

        private CaptureEntry GetSelectedEntry()
        {
            // detect it from the selected row index
            if (this.packetGridView.SelectedCells.Count > 0)
                return this.mDataSource[this.packetGridView.SelectedCells[0].RowIndex] as CaptureEntry;
            else if (this.packetGridView.SelectedRows.Count > 0)
                return this.mDataSource[this.packetGridView.SelectedRows[0].Index] as CaptureEntry;

            return null;
        }

        private void OpenInPacketViewer(object sender, EventArgs e)
        {
            CaptureEntry entry = GetSelectedEntry();

            if (entry is not null)
                this.OpenInPacketViewer(entry);
        }

        private void OpenInPacketViewer(CaptureEntry entry)
        {
            ((WorkspaceForm)this.MdiParent).ShowChildForm(new RawViewerForm(entry.RawData));
        }

        private void GridSelectionChanged(object sender, EventArgs e)
        {
            // get the current selected packet entry
            CaptureEntry entry = GetSelectedEntry();

            if (entry is null)
                return;

            this.mByteData = entry.RawData;

            // start the worker and update the data
            this.mWorker.RunWorkerAsync();
        }
    }
}
