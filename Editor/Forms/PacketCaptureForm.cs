using Editor.Capture;
using Editor.CustomMarshal;
using EVESharp.PythonTypes.Types.Network;
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
        internal class PacketFilters
        {
            public string Service { get; set; } = "";
            public string Method { get; set; } = "";
            public bool CallReq { get; set; } = true;
            public bool CallRsp { get; set; } = true;
            public bool Notification { get; set; } = true;
            public bool ErrorResponse { get; set; } = true;
            public bool SessionChange { get; set; } = true;
            public bool PingReq { get; set; } = true;
            public bool PingRsp { get; set; } = true;
            public bool Undetermined { get; set; } = true;
        }

        private CaptureProcessor mProcessor;
        private BindingSource mDataSource = new BindingSource();
        private List<CaptureEntry> mEntries = new List<CaptureEntry>();
        private List<CaptureEntry> mFilteredPackets = new List<CaptureEntry>();
        private BackgroundWorker mWorker = null;
        private BackgroundWorker mFilterWorker = null;
        private byte[] mByteData = null;
        private TreeNode mTreeNode = null;
        private PacketFilters mFilterConfiguration = new PacketFilters();

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
            this.mFilterWorker = new BackgroundWorker();
            this.mFilterWorker.WorkerSupportsCancellation = false;
            this.mFilterWorker.WorkerReportsProgress = true;
            this.mFilterWorker.DoWork += FilterPackets;
            this.mFilterWorker.RunWorkerCompleted += FilterPacketsCompleted;
            this.mFilterWorker.ProgressChanged += FilterPacketsProgressed;

            this.mDataSource.DataSource = this.mFilteredPackets;
        }

        private void FilterPacketsProgressed(object sender, ProgressChangedEventArgs e)
        {
            // update the window title with the progress...
            this.Text = $"Packet Capture Viewer - Filtering in progress... {e.ProgressPercentage} items left";
        }

        private void FilterPacketsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // filtering completed, re-enable the layout of the container and go on our merry day
            this.mDataSource.ResumeBinding();
            this.packetGridView.DataSource = this.mDataSource;
            this.splitContainer1.Enabled = true;
            this.splitContainer1.ResumeLayout();
            this.packetGridView.ResumeLayout();
        }

        private void FilterPackets(object sender, DoWorkEventArgs e)
        {
            this.mFilteredPackets.Clear();
            int count = this.mEntries.Count;

            foreach(CaptureEntry entry in this.mEntries)
            {
                // send a progress update on each one
                this.mFilterWorker.ReportProgress(--count);

                if (PacketShouldBeDisplayed(entry) == true)
                    this.mFilteredPackets.Add(entry);
            }
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
            this.packetGridView.RowPrePaint += OnRowPrePaint;
        }

        private void OnRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (this.mDataSource.Count <= e.RowIndex)
                return;

            CaptureEntry entry = this.mDataSource[e.RowIndex] as CaptureEntry;

            switch(entry.Type)
            {
                case PyPacket.PacketType.CALL_REQ:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                    break;

                case PyPacket.PacketType.CALL_RSP:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                    break;

                case PyPacket.PacketType.SESSIONCHANGENOTIFICATION:
                case PyPacket.PacketType.SESSIONINITIALSTATENOTIFICATION:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;

                case PyPacket.PacketType.ERRORRESPONSE:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkRed;
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;

                case PyPacket.PacketType.NOTIFICATION:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DodgerBlue;
                    break;

                case PyPacket.PacketType.PING_REQ:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumSlateBlue;
                    break;

                case PyPacket.PacketType.PING_RSP:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SeaGreen;
                    break;

                case PyPacket.PacketType.__Fake_Invalid_Type:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Cyan;
                    break;

                default:
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Black;
                    this.packetGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    break;
            }
        }

        private bool PacketShouldBeDisplayed(CaptureEntry packet)
        {
            // service and method checks should happen before anything else so we can properly filter things before the type
            if (this.mFilterConfiguration.Service.Length > 0 && packet.Service?.Contains(this.mFilterConfiguration.Service) == false)
                return false;
            if (this.mFilterConfiguration.Method.Length > 0 && packet.Call?.Contains(this.mFilterConfiguration.Method) == false)
                return false;
            if (this.mFilterConfiguration.Undetermined == true && packet.Type == PyPacket.PacketType.__Fake_Invalid_Type)
                return true;
            if (this.mFilterConfiguration.CallReq == true && packet.Type == PyPacket.PacketType.CALL_REQ)
                return true;
            if (this.mFilterConfiguration.CallRsp == true && packet.Type == PyPacket.PacketType.CALL_RSP)
                return true;
            if (this.mFilterConfiguration.Notification == true && packet.Type == PyPacket.PacketType.NOTIFICATION)
                return true;
            if (this.mFilterConfiguration.ErrorResponse == true && packet.Type == PyPacket.PacketType.ERRORRESPONSE)
                return true;
            if (this.mFilterConfiguration.PingReq == true && packet.Type == PyPacket.PacketType.PING_REQ)
                return true;
            if (this.mFilterConfiguration.PingRsp == true && packet.Type == PyPacket.PacketType.PING_RSP)
                return true;
            if (this.mFilterConfiguration.SessionChange == true && packet.Type == PyPacket.PacketType.SESSIONCHANGENOTIFICATION)
                return true;
            if (this.mFilterConfiguration.SessionChange == true && packet.Type == PyPacket.PacketType.SESSIONINITIALSTATENOTIFICATION)
                return true;

            return false;
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

            // filter the given packet based on the correct criteria
            if (PacketShouldBeDisplayed(packet) == false)
                return;

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
            // if the worker is busy do not do anything
            if (this.mWorker.IsBusy == true)
                return;

            // get the current selected packet entry
            CaptureEntry entry = GetSelectedEntry();

            if (entry is null)
                return;

            this.mByteData = entry.RawData;

            // start the worker and update the data
            this.mWorker.RunWorkerAsync();
        }

        private void ApplyFilters(object sender, EventArgs e)
        {
            // store the new filters first
            this.mFilterConfiguration.Service = this.serviceNameTextbox.Text;
            this.mFilterConfiguration.Method = this.callNameTextbox.Text;
            this.mFilterConfiguration.CallReq = this.callReqCheckbox.Checked;
            this.mFilterConfiguration.CallRsp = this.callRspCheckbox.Checked;
            this.mFilterConfiguration.ErrorResponse = this.exceptionCheckbox.Checked;
            this.mFilterConfiguration.PingReq = this.pingReqCheckbox.Checked;
            this.mFilterConfiguration.PingRsp = this.pingRspCheckbox.Checked;
            this.mFilterConfiguration.SessionChange = this.sessionChangeCheckbox.Checked;
            this.mFilterConfiguration.Notification = this.notificationCheckbox.Checked;
            this.mFilterConfiguration.Undetermined = this.undeterminedCheckbox.Checked;

            // disable almost everything and suspend the layout of the main container
            this.packetGridView.DataSource = null;
            this.splitContainer1.Enabled = false;
            this.splitContainer1.SuspendLayout();
            this.packetGridView.SuspendLayout();
            this.mDataSource.SuspendBinding();

            // start the background work
            this.mFilterWorker.RunWorkerAsync();
        }
    }
}
