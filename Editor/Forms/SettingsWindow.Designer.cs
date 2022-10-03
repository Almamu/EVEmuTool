namespace EVEmuTool.Forms
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.dontStartPacketCapture = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.startPacketCapture = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.serverPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.dontStartPacketCapture);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.startPacketCapture);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.serverPort);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.serverAddress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 208);
            this.panel1.TabIndex = 8;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 167);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(326, 23);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveAndClose);
            // 
            // dontStartPacketCapture
            // 
            this.dontStartPacketCapture.AutoSize = true;
            this.dontStartPacketCapture.Location = new System.Drawing.Point(60, 125);
            this.dontStartPacketCapture.Name = "dontStartPacketCapture";
            this.dontStartPacketCapture.Size = new System.Drawing.Size(41, 19);
            this.dontStartPacketCapture.TabIndex = 14;
            this.dontStartPacketCapture.TabStop = true;
            this.dontStartPacketCapture.Text = "No";
            this.dontStartPacketCapture.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Start Packet Capture automatically:";
            // 
            // startPacketCapture
            // 
            this.startPacketCapture.AutoSize = true;
            this.startPacketCapture.Location = new System.Drawing.Point(12, 125);
            this.startPacketCapture.Name = "startPacketCapture";
            this.startPacketCapture.Size = new System.Drawing.Size(42, 19);
            this.startPacketCapture.TabIndex = 12;
            this.startPacketCapture.TabStop = true;
            this.startPacketCapture.Text = "Yes";
            this.startPacketCapture.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Server Port:";
            // 
            // serverPort
            // 
            this.serverPort.Location = new System.Drawing.Point(12, 81);
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(326, 23);
            this.serverPort.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Server Address:";
            // 
            // serverAddress
            // 
            this.serverAddress.Location = new System.Drawing.Point(12, 37);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(326, 23);
            this.serverAddress.TabIndex = 8;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 208);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsWindow";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RadioButton dontStartPacketCapture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton startPacketCapture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverAddress;
    }
}