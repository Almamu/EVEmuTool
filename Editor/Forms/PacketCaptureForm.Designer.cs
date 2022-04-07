namespace Editor.Forms
{
    partial class PacketCaptureForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.callNameTextbox = new System.Windows.Forms.TextBox();
            this.serviceNameTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sessionChangeCheckbox = new System.Windows.Forms.CheckBox();
            this.undeterminedCheckbox = new System.Windows.Forms.CheckBox();
            this.pingRspCheckbox = new System.Windows.Forms.CheckBox();
            this.pingReqCheckbox = new System.Windows.Forms.CheckBox();
            this.exceptionCheckbox = new System.Windows.Forms.CheckBox();
            this.notificationCheckbox = new System.Windows.Forms.CheckBox();
            this.callRspCheckbox = new System.Windows.Forms.CheckBox();
            this.callReqCheckbox = new System.Windows.Forms.CheckBox();
            this.applyFiltersButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.packetGridView = new System.Windows.Forms.DataGridView();
            this.packetTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCallID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInMarshalViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.marshalTreeView = new Editor.Forms.Components.MarshalTreeViewComponent();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packetGridView)).BeginInit();
            this.packetListContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.applyFiltersButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(765, 808);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 136);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.callNameTextbox);
            this.groupBox2.Controls.Add(this.serviceNameTextbox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(386, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(375, 130);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CallReq filters";
            // 
            // callNameTextbox
            // 
            this.callNameTextbox.Location = new System.Drawing.Point(63, 46);
            this.callNameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callNameTextbox.Name = "callNameTextbox";
            this.callNameTextbox.Size = new System.Drawing.Size(177, 23);
            this.callNameTextbox.TabIndex = 5;
            // 
            // serviceNameTextbox
            // 
            this.serviceNameTextbox.Location = new System.Drawing.Point(63, 20);
            this.serviceNameTextbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.serviceNameTextbox.Name = "serviceNameTextbox";
            this.serviceNameTextbox.Size = new System.Drawing.Size(177, 23);
            this.serviceNameTextbox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Call:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sessionChangeCheckbox);
            this.groupBox1.Controls.Add(this.undeterminedCheckbox);
            this.groupBox1.Controls.Add(this.pingRspCheckbox);
            this.groupBox1.Controls.Add(this.pingReqCheckbox);
            this.groupBox1.Controls.Add(this.exceptionCheckbox);
            this.groupBox1.Controls.Add(this.notificationCheckbox);
            this.groupBox1.Controls.Add(this.callRspCheckbox);
            this.groupBox1.Controls.Add(this.callReqCheckbox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(374, 130);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet type";
            // 
            // sessionChangeCheckbox
            // 
            this.sessionChangeCheckbox.Checked = true;
            this.sessionChangeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sessionChangeCheckbox.Location = new System.Drawing.Point(9, 72);
            this.sessionChangeCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sessionChangeCheckbox.Name = "sessionChangeCheckbox";
            this.sessionChangeCheckbox.Size = new System.Drawing.Size(152, 20);
            this.sessionChangeCheckbox.TabIndex = 7;
            this.sessionChangeCheckbox.Text = "Session Change";
            this.sessionChangeCheckbox.UseVisualStyleBackColor = true;
            // 
            // undeterminedCheckbox
            // 
            this.undeterminedCheckbox.Checked = true;
            this.undeterminedCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.undeterminedCheckbox.Location = new System.Drawing.Point(260, 22);
            this.undeterminedCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.undeterminedCheckbox.Name = "undeterminedCheckbox";
            this.undeterminedCheckbox.Size = new System.Drawing.Size(152, 20);
            this.undeterminedCheckbox.TabIndex = 6;
            this.undeterminedCheckbox.Text = "Undetermined";
            this.undeterminedCheckbox.UseVisualStyleBackColor = true;
            // 
            // pingRspCheckbox
            // 
            this.pingRspCheckbox.Checked = true;
            this.pingRspCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pingRspCheckbox.Location = new System.Drawing.Point(180, 48);
            this.pingRspCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pingRspCheckbox.Name = "pingRspCheckbox";
            this.pingRspCheckbox.Size = new System.Drawing.Size(152, 20);
            this.pingRspCheckbox.TabIndex = 5;
            this.pingRspCheckbox.Text = "PingRsp";
            this.pingRspCheckbox.UseVisualStyleBackColor = true;
            // 
            // pingReqCheckbox
            // 
            this.pingReqCheckbox.Checked = true;
            this.pingReqCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pingReqCheckbox.Location = new System.Drawing.Point(180, 22);
            this.pingReqCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pingReqCheckbox.Name = "pingReqCheckbox";
            this.pingReqCheckbox.Size = new System.Drawing.Size(152, 20);
            this.pingReqCheckbox.TabIndex = 4;
            this.pingReqCheckbox.Text = "PingReq";
            this.pingReqCheckbox.UseVisualStyleBackColor = true;
            // 
            // exceptionCheckbox
            // 
            this.exceptionCheckbox.Checked = true;
            this.exceptionCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exceptionCheckbox.Location = new System.Drawing.Point(88, 48);
            this.exceptionCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.exceptionCheckbox.Name = "exceptionCheckbox";
            this.exceptionCheckbox.Size = new System.Drawing.Size(152, 20);
            this.exceptionCheckbox.TabIndex = 3;
            this.exceptionCheckbox.Text = "Exception";
            this.exceptionCheckbox.UseVisualStyleBackColor = true;
            // 
            // notificationCheckbox
            // 
            this.notificationCheckbox.Checked = true;
            this.notificationCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notificationCheckbox.Location = new System.Drawing.Point(88, 22);
            this.notificationCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notificationCheckbox.Name = "notificationCheckbox";
            this.notificationCheckbox.Size = new System.Drawing.Size(152, 20);
            this.notificationCheckbox.TabIndex = 2;
            this.notificationCheckbox.Text = "Notification";
            this.notificationCheckbox.UseVisualStyleBackColor = true;
            // 
            // callRspCheckbox
            // 
            this.callRspCheckbox.Checked = true;
            this.callRspCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.callRspCheckbox.Location = new System.Drawing.Point(9, 48);
            this.callRspCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callRspCheckbox.Name = "callRspCheckbox";
            this.callRspCheckbox.Size = new System.Drawing.Size(152, 20);
            this.callRspCheckbox.TabIndex = 1;
            this.callRspCheckbox.Text = "CallRsp";
            this.callRspCheckbox.UseVisualStyleBackColor = true;
            // 
            // callReqCheckbox
            // 
            this.callReqCheckbox.Checked = true;
            this.callReqCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.callReqCheckbox.Location = new System.Drawing.Point(9, 22);
            this.callReqCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.callReqCheckbox.Name = "callReqCheckbox";
            this.callReqCheckbox.Size = new System.Drawing.Size(152, 20);
            this.callReqCheckbox.TabIndex = 0;
            this.callReqCheckbox.Text = "CallReq";
            this.callReqCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyFiltersButton
            // 
            this.applyFiltersButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.applyFiltersButton.Location = new System.Drawing.Point(0, 136);
            this.applyFiltersButton.Name = "applyFiltersButton";
            this.applyFiltersButton.Size = new System.Drawing.Size(765, 25);
            this.applyFiltersButton.TabIndex = 8;
            this.applyFiltersButton.Text = "Apply filters";
            this.applyFiltersButton.UseVisualStyleBackColor = true;
            this.applyFiltersButton.Click += new System.EventHandler(this.ApplyFilters);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.packetGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.marshalTreeView);
            this.splitContainer2.Size = new System.Drawing.Size(765, 643);
            this.splitContainer2.SplitterDistance = 407;
            this.splitContainer2.TabIndex = 0;
            // 
            // packetGridView
            // 
            this.packetGridView.AllowUserToAddRows = false;
            this.packetGridView.AllowUserToDeleteRows = false;
            this.packetGridView.AllowUserToResizeRows = false;
            this.packetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packetTimestamp,
            this.Length,
            this.packetType,
            this.packetSource,
            this.packetDestination,
            this.packetCallID,
            this.packetService,
            this.packetCall});
            this.packetGridView.ContextMenuStrip = this.packetListContextMenu;
            this.packetGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetGridView.Location = new System.Drawing.Point(0, 0);
            this.packetGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.packetGridView.MultiSelect = false;
            this.packetGridView.Name = "packetGridView";
            this.packetGridView.ReadOnly = true;
            this.packetGridView.Size = new System.Drawing.Size(765, 407);
            this.packetGridView.TabIndex = 1;
            this.packetGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OpenInPacketViewer);
            this.packetGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CellClick);
            this.packetGridView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OpenInPacketViewer);
            this.packetGridView.SelectionChanged += new System.EventHandler(this.GridSelectionChanged);
            // 
            // packetTimestamp
            // 
            this.packetTimestamp.DataPropertyName = "Timestamp";
            this.packetTimestamp.HeaderText = "Timestamp";
            this.packetTimestamp.Name = "packetTimestamp";
            this.packetTimestamp.ReadOnly = true;
            // 
            // Length
            // 
            this.Length.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Length.DataPropertyName = "Length";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Length.DefaultCellStyle = dataGridViewCellStyle1;
            this.Length.HeaderText = "Length";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            this.Length.Width = 69;
            // 
            // packetType
            // 
            this.packetType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packetType.DataPropertyName = "PacketType";
            this.packetType.HeaderText = "Type";
            this.packetType.Name = "packetType";
            this.packetType.ReadOnly = true;
            this.packetType.Width = 56;
            // 
            // packetSource
            // 
            this.packetSource.DataPropertyName = "Source";
            this.packetSource.HeaderText = "Source";
            this.packetSource.Name = "packetSource";
            this.packetSource.ReadOnly = true;
            // 
            // packetDestination
            // 
            this.packetDestination.DataPropertyName = "Destination";
            this.packetDestination.HeaderText = "Destination";
            this.packetDestination.Name = "packetDestination";
            this.packetDestination.ReadOnly = true;
            // 
            // packetCallID
            // 
            this.packetCallID.DataPropertyName = "CallID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.packetCallID.DefaultCellStyle = dataGridViewCellStyle2;
            this.packetCallID.HeaderText = "CallID";
            this.packetCallID.Name = "packetCallID";
            this.packetCallID.ReadOnly = true;
            // 
            // packetService
            // 
            this.packetService.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packetService.DataPropertyName = "Service";
            this.packetService.HeaderText = "Service";
            this.packetService.Name = "packetService";
            this.packetService.ReadOnly = true;
            this.packetService.Width = 69;
            // 
            // packetCall
            // 
            this.packetCall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packetCall.DataPropertyName = "Call";
            this.packetCall.HeaderText = "Call";
            this.packetCall.Name = "packetCall";
            this.packetCall.ReadOnly = true;
            this.packetCall.Width = 52;
            // 
            // packetListContextMenu
            // 
            this.packetListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInMarshalViewer,
            this.saveToFile});
            this.packetListContextMenu.Name = "packetListContextMenu";
            this.packetListContextMenu.Size = new System.Drawing.Size(252, 70);
            // 
            // openInMarshalViewer
            // 
            this.openInMarshalViewer.Enabled = false;
            this.openInMarshalViewer.Name = "openInMarshalViewer";
            this.openInMarshalViewer.Size = new System.Drawing.Size(251, 22);
            this.openInMarshalViewer.Text = "Open in Raw Marshal Data Viewer";
            this.openInMarshalViewer.Click += new System.EventHandler(this.OpenInPacketViewer);
            // 
            // saveToFile
            // 
            this.saveToFile.Enabled = false;
            this.saveToFile.Name = "saveToFile";
            this.saveToFile.Size = new System.Drawing.Size(251, 22);
            this.saveToFile.Text = "Save to file";
            this.saveToFile.Click += new System.EventHandler(this.SaveToFile);
            // 
            // marshalTreeView
            // 
            this.marshalTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marshalTreeView.Location = new System.Drawing.Point(0, 0);
            this.marshalTreeView.Name = "marshalTreeView";
            this.marshalTreeView.Size = new System.Drawing.Size(765, 232);
            this.marshalTreeView.TabIndex = 0;
            // 
            // PacketCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 808);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PacketCaptureForm";
            this.Text = "Packet Capture Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureWindowClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packetGridView)).EndInit();
            this.packetListContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView packetGridView;
        private System.Windows.Forms.ContextMenuStrip packetListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openInMarshalViewer;
        private Components.MarshalTreeViewComponent marshalTreeView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox callNameTextbox;
        private System.Windows.Forms.TextBox serviceNameTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox sessionChangeCheckbox;
        private System.Windows.Forms.CheckBox undeterminedCheckbox;
        private System.Windows.Forms.CheckBox pingRspCheckbox;
        private System.Windows.Forms.CheckBox pingReqCheckbox;
        private System.Windows.Forms.CheckBox exceptionCheckbox;
        private System.Windows.Forms.CheckBox notificationCheckbox;
        private System.Windows.Forms.CheckBox callRspCheckbox;
        private System.Windows.Forms.CheckBox callReqCheckbox;
        private System.Windows.Forms.Button applyFiltersButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetCallID;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetService;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetCall;
        private System.Windows.Forms.ToolStripMenuItem saveToFile;
    }
}