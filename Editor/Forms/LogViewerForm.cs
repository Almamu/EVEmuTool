using Editor.LogServer;
using EVESharp.PythonTypes.Marshal;
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

namespace Editor.Forms
{
    public partial class LogViewerForm : Form
    {
        private BackgroundWorker mLoaderWorker;
        private BackgroundWorker mUnmarshallerWorker;
        private WorkspaceReader mReader = null;
        private WorkspaceFile mFile = null;
        private string mSelectedData = "";
        private byte[] mPacketData = null;

        public LogViewerForm(WorkspaceReader reader)
        {
            InitializeComponent();

            FileStream fp = ((FileStream)reader.BaseStream);

            // update the form's title
            this.Text = $"LogServer Vieiwer for {fp.Name}";

            this.mReader = reader;
            this.mLoaderWorker = new BackgroundWorker();
            this.mLoaderWorker.WorkerReportsProgress = false;
            this.mLoaderWorker.WorkerSupportsCancellation = false;
            this.mLoaderWorker.DoWork += BackgroundReadWorkspaceFile;
            this.mLoaderWorker.RunWorkerCompleted += BackgroundReadCompleted;
            this.mUnmarshallerWorker = new BackgroundWorker();
            this.mUnmarshallerWorker.WorkerReportsProgress = false;
            this.mUnmarshallerWorker.WorkerSupportsCancellation = false;
            this.mUnmarshallerWorker.DoWork += BackgroundMarshalRead;
            this.mUnmarshallerWorker.RunWorkerCompleted += BackgroundMarshalReadCompleted;

            // set status text
            this.loadingStatus.Text += fp.Name;
            // start the background worker to perform the job
            this.mLoaderWorker.RunWorkerAsync();
        }

        private void BackgroundReadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.storagesTabs.SuspendLayout();

            List<TabPage> result = (List<TabPage>) e.Result;

            this.loadingStatus.Text = "Creating tabs...";

            foreach (TabPage page in result)
                this.storagesTabs.TabPages.Add(page);

            this.storagesTabs.ResumeLayout(false);
            this.loadingStatus.Text = ((FileStream)this.mReader.BaseStream).Name + " loaded successfuly";
        }

        private void BackgroundReadWorkspaceFile(object sender, DoWorkEventArgs e)
        {
            this.mFile = WorkspaceFile.BuildFromByteData(this.mReader);
            e.Result = this.LoadWorkspaceDetails(this.mFile);
        }

        private void BackgroundMarshalReadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.mPacketData is null)
            {
                MessageBox.Show("Marshal data was ininteligible and cannot be parsed... Sorry...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ((WorkspaceForm)this.MdiParent).ShowChildForm(new RawViewerForm(this.mPacketData));

            // clear the reference so memory is free'd
            this.mPacketData = null;
            this.mSelectedData = null;
        }

        private void BackgroundMarshalRead(object sender, DoWorkEventArgs e)
        {
            List<byte> bytes = new List<byte>();

            // if the selected area starts with something that's not ~ find it in the string
            if (this.mSelectedData.StartsWith((char)Specification.MARSHAL_HEADER) == false)
            {
                int index = this.mSelectedData.IndexOf((char)Specification.MARSHAL_HEADER);

                if (index == -1)
                {
                    MessageBox.Show("Cannot find the beginning of the marshal data. Are you sure you've selected a MarshalStream?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.mSelectedData = this.mSelectedData.Substring(index);
            }

            for (int i = 0; i < this.mSelectedData.Length; i++)
            {
                char current = this.mSelectedData[i];

                if (current == '\\')
                {
                    int indicatorIndex = i + 1;

                    if (indicatorIndex >= this.mSelectedData.Length)
                    {
                        bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                        break;
                    }

                    // check next value
                    if (this.mSelectedData[indicatorIndex] == 'x')
                    {
                        int endIndex = indicatorIndex + 1 + 2;

                        if (endIndex >= this.mSelectedData.Length)
                            break;

                        string value = this.mSelectedData.Substring(indicatorIndex + 1, 2);

                        if (byte.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out byte result) == true)
                        {
                            bytes.Add(result);
                            i += 3;
                        }
                        else
                        {
                            // value is all weird and wonky, go to the next value and hope it's better...
                            // HINT: most likely it wont, and marshal data will be unusable from here...
                            i++;
                        }
                    }
                    else if (this.mSelectedData[indicatorIndex] == '\\')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\\")[0]);
                        i++;
                    }
                    else if (this.mSelectedData[indicatorIndex] == 'n')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\n")[0]);
                        i++;
                    }
                    else if (this.mSelectedData[indicatorIndex] == 'r')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\r")[0]);
                        i++;
                    }
                    else if (this.mSelectedData[indicatorIndex] == 't')
                    {
                        bytes.Add(Encoding.ASCII.GetBytes("\t")[0]);
                        i++;
                    }
                }
                else
                {
                    bytes.Add(Encoding.ASCII.GetBytes(new char[] { current })[0]);
                }
            }

            this.mPacketData = bytes.ToArray();
        }

        private List<TabPage> LoadWorkspaceDetails(WorkspaceFile workspace)
        {
            List<TabPage> result = new List<TabPage>();

            // go through the device list
            foreach (Device device in workspace.Devices)
            {
                // go through the storages
                foreach (Storage storage in device.Storages)
                {
                    // generate the controls
                    LogViewerHelper.CreateTabPage(storage.Name, out TabPage tabPage, out DataGridView gridView);
                    // add the data source
                    gridView.DataSource = storage.Lines;
                    // also add listener for index changed so we can show proper, full-length messages
                    gridView.SelectionChanged += LogGrid_SelectionChanged;
                    // store the tabpage in the list
                    result.Add(tabPage);
                }
            }

            return result;
        }

        private string PrepareLogStringForConcatenation(int index, LogLine line)
        {
            string text = line.Line;
            int minimumLength = 253;

            if (text.Length < minimumLength)
                text += "\n";
            if (index > 0 && text.StartsWith("- ") == true)
                text = text.Substring(2);
            if (index > 0 && text.StartsWith("    ") == true)
                text = text.Substring(4);
            if (text.Length == 254 && text.EndsWith("-") == true)
                text = text.Substring(0, text.Length - 1);

            return text;
        }

        private void LogGrid_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            LogLine[] dataSource = gridView.DataSource as LogLine[];
            int index = 0;

            // reset the text
            logViewExpanded.Text = "";

            if (gridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gridView.SelectedRows.Cast<DataGridViewRow>().OrderBy(x => x.Index))
                    logViewExpanded.Text += this.PrepareLogStringForConcatenation(index++, dataSource[row.Index]);
            }
            else if (gridView.SelectedCells.Count > 0)
            {
                Dictionary<int, bool> pairs = new Dictionary<int, bool>();

                foreach (DataGridViewCell cell in gridView.SelectedCells.Cast<DataGridViewCell>().OrderBy(x => x.RowIndex))
                {
                    if (pairs.ContainsKey(cell.RowIndex) == true)
                        continue;

                    logViewExpanded.Text += this.PrepareLogStringForConcatenation(index++, dataSource[cell.RowIndex]);
                    pairs[cell.RowIndex] = true;
                }
            }
        }

        private void ParseDataAsMarshalContent(object sender, EventArgs e)
        {
            if (this.mUnmarshallerWorker.IsBusy == true)
            {
                MessageBox.Show("Cannot try parse a marshal packet when one is currently in process", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.mSelectedData = this.logViewExpanded.SelectedText;
            this.mUnmarshallerWorker.RunWorkerAsync();
        }
    }
}
