using EVEmuTool.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.Forms
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // set settings to the texboxes
            this.serverAddress.Text = CaptureSettings.ServerAddress;
            this.serverPort.Text = CaptureSettings.ServerPort;
            
            if (CaptureSettings.ServerAutoStart == "False")
            {
                this.dontStartPacketCapture.Checked = true;
            }
            else
            {
                this.startPacketCapture.Checked = true;
            }
        }

        private void SaveAndClose(object sender, EventArgs e)
        {
            // check the settings first, port has to be within the 1024-65535 range
            if (ushort.TryParse(this.serverPort.Text, out ushort port) == false || port < 1024)
            {
                MessageBox.Show("The server port must be in the 1024-65535 range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // settings are valid, set the new settings and save them to registry
            CaptureSettings.ServerAddress = this.serverAddress.Text;
            CaptureSettings.ServerPort = this.serverPort.Text;
            CaptureSettings.ServerAutoStart = this.dontStartPacketCapture.Checked ? "False" : "True";
            
            if (CaptureSettings.SaveSettingsToRegistry() == true)
                // show the user some success message
                MessageBox.Show("Save operation completed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // this form doesn't do anything else, close it now
            this.Close();
        }
    }
}
