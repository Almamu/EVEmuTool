using EVEmuTool.Capture;
using EVEmuTool.Configuration;
using EVEmuTool.LogServer;
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

namespace EVEmuTool.Forms
{
    public partial class WorkspaceForm : Form
    {
        private CaptureServer mCaptureServer = null;
        private CaptureProcessor mProcessor = null;
        private PacketCaptureForm mPacketCaptureForm = null;

        public WorkspaceForm()
        {
            InitializeComponent();

            // load settings from the registry
            CaptureSettings.LoadSettingsFromRegistry();
            // register the packet capture form so it can listen to packets from the server
            this.mPacketCaptureForm = new PacketCaptureForm();
            // check if the server has to be run and start it
            if (CaptureSettings.ServerAutoStart == "True")
                this.StartServer(this, null);
        }

        private void OnCaptureStatusChange(object sender, string newStatus)
        {
            this.toolStripStatusLabel.Text = newStatus;
        }

        private void StartServer(object sender, EventArgs e)
        {
            // the processor only needs to bootup once
            if (this.mProcessor is null)
                this.mProcessor = new CaptureProcessor();

            this.mPacketCaptureForm.Processor = this.mProcessor;

            this.mCaptureServer = new CaptureServer(26000, this.mProcessor);
            this.mCaptureServer.OnStatusChange += OnCaptureStatusChange;

            try
            {
                this.mCaptureServer.Listen();
                ServerStarted();
            }
            catch (Exception ex)
            {
                // server didn't start up, mark it as stopped and show a warning
                this.StopServer(this, null);
                this.OnCaptureStatusChange(this, $"Cannot listen: {ex.Message}");
            }
        }

        private void StopServer(object sender, EventArgs e)
        {
            // ensure the server is up and running and dispose of it
            if (this.mCaptureServer is not null)
                this.mCaptureServer.Dispose();

            this.mCaptureServer = null;
            this.mProcessor = null;

            ServerStopped();
        }

        private void ServerStarted()
        {
            this.startPacketCapture.Enabled = false;
            this.startPacketCaptureButton.Enabled = false;
            this.stopPacketCapture.Enabled = true;
            this.stopPacketCaptureButton.Enabled = true;
        }

        private void ServerStopped()
        {
            this.startPacketCapture.Enabled = true;
            this.startPacketCaptureButton.Enabled = true;
            this.stopPacketCapture.Enabled = false;
            this.stopPacketCaptureButton.Enabled = false;
        }
        
        private void ClosingForm(object sender, EventArgs e)
        {
            this.StopServer(this, e);
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        public void ShowChildForm(Form child)
        {
            child.MdiParent = this;
            child.Show();
        }

        private void OpenLogServerWorkspace(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "lbw",
                Filter = "LogServer Workspace File|*.lbw"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // create a workspace reader and show the proper form
                this.ShowChildForm (new LogViewerForm(new WorkspaceReader(dialog.OpenFile())));
            }
        }

        private void OpenRawMarshalData(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // open a new window with the byte contents of the file
                this.ShowChildForm(new RawViewerForm(dialog.OpenFile()));
            }
        }

        private void OpenSettingsWindow(object sender, EventArgs e)
        {
            SettingsWindow window = new SettingsWindow();

            window.ShowDialog(this);
        }

        private void CloseForm(object sender, EventArgs e)
        {
            // form closing event will take care of freeing stuff
            this.Close();
        }

        private void OpenPacketCaptureWindow(object sender, EventArgs e)
        {
            this.ShowChildForm(this.mPacketCaptureForm);
        }

        private void OpenCacheFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "cache",
                Filter = "EVE Online Cache File|*.cache"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // create a workspace reader and show the proper form
                this.ShowChildForm(new CacheViewerForm(dialog.OpenFile(), dialog.FileName));
            }
        }

        private void OpenStuffFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "stuff",
                Filter = "EVE Online EmbedFS|*.stuff"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // create a file list form
                this.ShowChildForm(new StuffExplorer(dialog.OpenFile(), dialog.FileName));
            }
        }

        private void OpenEVEOnlineDirectory(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                InitialDirectory = "C:/Program Files (x86)/CCP/EVE"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                string[] files = Directory
                    .EnumerateFiles(path, "*.stuff", SearchOption.TopDirectoryOnly)
                    .ToArray();

                this.ShowChildForm(new StuffExplorer(path, files));
            }
        }
    }
}
