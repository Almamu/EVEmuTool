namespace Editor
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.clientListGridView = new System.Windows.Forms.DataGridView();
            this.selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.serverInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.serverPortTextBox = new System.Windows.Forms.TextBox();
            this.serverAddressTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.includeResponsesCheckbox = new System.Windows.Forms.CheckBox();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.packetGridView = new System.Windows.Forms.DataGridView();
            this.packetTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCallID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetOrigin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetTextBox = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.packetTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.clientListGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.serverInfoGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.packetGridView)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripDropDownButton1, this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel1.Text = "Clear packets";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // clientListGridView
            // 
            this.clientListGridView.AllowUserToAddRows = false;
            this.clientListGridView.AllowUserToDeleteRows = false;
            this.clientListGridView.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.clientListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.selected, this.clientID, this.clientAddress, this.clientUser});
            this.clientListGridView.Location = new System.Drawing.Point(8, 6);
            this.clientListGridView.MultiSelect = false;
            this.clientListGridView.Name = "clientListGridView";
            this.clientListGridView.Size = new System.Drawing.Size(778, 97);
            this.clientListGridView.TabIndex = 1;
            // 
            // selected
            // 
            this.selected.DataPropertyName = "IncludePackets";
            this.selected.Frozen = true;
            this.selected.HeaderText = "";
            this.selected.Name = "selected";
            this.selected.Width = 25;
            // 
            // clientID
            // 
            this.clientID.DataPropertyName = "ClientIndex";
            this.clientID.HeaderText = "ID";
            this.clientID.Name = "clientID";
            this.clientID.ReadOnly = true;
            this.clientID.Width = 40;
            // 
            // clientAddress
            // 
            this.clientAddress.DataPropertyName = "Address";
            this.clientAddress.HeaderText = "IP:PORT";
            this.clientAddress.Name = "clientAddress";
            this.clientAddress.ReadOnly = true;
            // 
            // clientUser
            // 
            this.clientUser.DataPropertyName = "Username";
            this.clientUser.HeaderText = "Username";
            this.clientUser.Name = "clientUser";
            this.clientUser.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 425);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.serverInfoGroupBox);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.clientListGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filter information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // serverInfoGroupBox
            // 
            this.serverInfoGroupBox.Controls.Add(this.serverPortTextBox);
            this.serverInfoGroupBox.Controls.Add(this.serverAddressTextBox);
            this.serverInfoGroupBox.Controls.Add(this.label3);
            this.serverInfoGroupBox.Controls.Add(this.label4);
            this.serverInfoGroupBox.Location = new System.Drawing.Point(8, 201);
            this.serverInfoGroupBox.Name = "serverInfoGroupBox";
            this.serverInfoGroupBox.Size = new System.Drawing.Size(315, 87);
            this.serverInfoGroupBox.TabIndex = 4;
            this.serverInfoGroupBox.TabStop = false;
            this.serverInfoGroupBox.Text = "Server information";
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Location = new System.Drawing.Point(53, 42);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(152, 20);
            this.serverPortTextBox.TabIndex = 9;
            this.serverPortTextBox.Text = "25999";
            this.serverPortTextBox.TextChanged += new System.EventHandler(this.serverPortTextBox_TextChanged);
            // 
            // serverAddressTextBox
            // 
            this.serverAddressTextBox.Location = new System.Drawing.Point(53, 19);
            this.serverAddressTextBox.Name = "serverAddressTextBox";
            this.serverAddressTextBox.Size = new System.Drawing.Size(152, 20);
            this.serverAddressTextBox.TabIndex = 8;
            this.serverAddressTextBox.Text = "127.0.0.1";
            this.serverAddressTextBox.TextChanged += new System.EventHandler(this.serverAddressTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.includeResponsesCheckbox);
            this.groupBox2.Controls.Add(this.callNameTextbox);
            this.groupBox2.Controls.Add(this.serviceNameTextbox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(329, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 86);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CallReq filters";
            // 
            // includeResponsesCheckbox
            // 
            this.includeResponsesCheckbox.Checked = true;
            this.includeResponsesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeResponsesCheckbox.Location = new System.Drawing.Point(6, 62);
            this.includeResponsesCheckbox.Name = "includeResponsesCheckbox";
            this.includeResponsesCheckbox.Size = new System.Drawing.Size(130, 17);
            this.includeResponsesCheckbox.TabIndex = 7;
            this.includeResponsesCheckbox.Text = "Include responses";
            this.includeResponsesCheckbox.UseVisualStyleBackColor = true;
            // 
            // callNameTextbox
            // 
            this.callNameTextbox.Location = new System.Drawing.Point(54, 40);
            this.callNameTextbox.Name = "callNameTextbox";
            this.callNameTextbox.Size = new System.Drawing.Size(152, 20);
            this.callNameTextbox.TabIndex = 5;
            // 
            // serviceNameTextbox
            // 
            this.serviceNameTextbox.Location = new System.Drawing.Point(54, 17);
            this.serviceNameTextbox.Name = "serviceNameTextbox";
            this.serviceNameTextbox.Size = new System.Drawing.Size(152, 20);
            this.serviceNameTextbox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Call:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
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
            this.groupBox1.Location = new System.Drawing.Point(8, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 86);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet type";
            // 
            // sessionChangeCheckbox
            // 
            this.sessionChangeCheckbox.Checked = true;
            this.sessionChangeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sessionChangeCheckbox.Location = new System.Drawing.Point(8, 62);
            this.sessionChangeCheckbox.Name = "sessionChangeCheckbox";
            this.sessionChangeCheckbox.Size = new System.Drawing.Size(130, 17);
            this.sessionChangeCheckbox.TabIndex = 7;
            this.sessionChangeCheckbox.Text = "Session Change";
            this.sessionChangeCheckbox.UseVisualStyleBackColor = true;
            // 
            // undeterminedCheckbox
            // 
            this.undeterminedCheckbox.Checked = true;
            this.undeterminedCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.undeterminedCheckbox.Location = new System.Drawing.Point(223, 19);
            this.undeterminedCheckbox.Name = "undeterminedCheckbox";
            this.undeterminedCheckbox.Size = new System.Drawing.Size(130, 17);
            this.undeterminedCheckbox.TabIndex = 6;
            this.undeterminedCheckbox.Text = "Undetermined";
            this.undeterminedCheckbox.UseVisualStyleBackColor = true;
            // 
            // pingRspCheckbox
            // 
            this.pingRspCheckbox.Checked = true;
            this.pingRspCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pingRspCheckbox.Location = new System.Drawing.Point(154, 42);
            this.pingRspCheckbox.Name = "pingRspCheckbox";
            this.pingRspCheckbox.Size = new System.Drawing.Size(130, 17);
            this.pingRspCheckbox.TabIndex = 5;
            this.pingRspCheckbox.Text = "PingRsp";
            this.pingRspCheckbox.UseVisualStyleBackColor = true;
            // 
            // pingReqCheckbox
            // 
            this.pingReqCheckbox.Checked = true;
            this.pingReqCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pingReqCheckbox.Location = new System.Drawing.Point(154, 19);
            this.pingReqCheckbox.Name = "pingReqCheckbox";
            this.pingReqCheckbox.Size = new System.Drawing.Size(130, 17);
            this.pingReqCheckbox.TabIndex = 4;
            this.pingReqCheckbox.Text = "PingReq";
            this.pingReqCheckbox.UseVisualStyleBackColor = true;
            // 
            // exceptionCheckbox
            // 
            this.exceptionCheckbox.Checked = true;
            this.exceptionCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.exceptionCheckbox.Location = new System.Drawing.Point(75, 42);
            this.exceptionCheckbox.Name = "exceptionCheckbox";
            this.exceptionCheckbox.Size = new System.Drawing.Size(130, 17);
            this.exceptionCheckbox.TabIndex = 3;
            this.exceptionCheckbox.Text = "Exception";
            this.exceptionCheckbox.UseVisualStyleBackColor = true;
            // 
            // notificationCheckbox
            // 
            this.notificationCheckbox.Checked = true;
            this.notificationCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notificationCheckbox.Location = new System.Drawing.Point(75, 19);
            this.notificationCheckbox.Name = "notificationCheckbox";
            this.notificationCheckbox.Size = new System.Drawing.Size(130, 17);
            this.notificationCheckbox.TabIndex = 2;
            this.notificationCheckbox.Text = "Notification";
            this.notificationCheckbox.UseVisualStyleBackColor = true;
            // 
            // callRspCheckbox
            // 
            this.callRspCheckbox.Checked = true;
            this.callRspCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.callRspCheckbox.Location = new System.Drawing.Point(8, 42);
            this.callRspCheckbox.Name = "callRspCheckbox";
            this.callRspCheckbox.Size = new System.Drawing.Size(130, 17);
            this.callRspCheckbox.TabIndex = 1;
            this.callRspCheckbox.Text = "CallRsp";
            this.callRspCheckbox.UseVisualStyleBackColor = true;
            // 
            // callReqCheckbox
            // 
            this.callReqCheckbox.Checked = true;
            this.callReqCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.callReqCheckbox.Location = new System.Drawing.Point(8, 19);
            this.callReqCheckbox.Name = "callReqCheckbox";
            this.callReqCheckbox.Size = new System.Drawing.Size(130, 17);
            this.callReqCheckbox.TabIndex = 0;
            this.callReqCheckbox.Text = "CallReq";
            this.callReqCheckbox.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Packet data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.packetGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(792, 399);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 2;
            // 
            // packetGridView
            // 
            this.packetGridView.AllowUserToAddRows = false;
            this.packetGridView.AllowUserToDeleteRows = false;
            this.packetGridView.AllowUserToOrderColumns = true;
            this.packetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.packetTimestamp, this.packetClientID, this.packetCallID, this.packetType, this.packetOrigin, this.packetDestination, this.packetService, this.packetCall});
            this.packetGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetGridView.Location = new System.Drawing.Point(0, 0);
            this.packetGridView.Name = "packetGridView";
            this.packetGridView.ReadOnly = true;
            this.packetGridView.Size = new System.Drawing.Size(792, 171);
            this.packetGridView.TabIndex = 0;
            this.packetGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packetGridView_CellClick);
            this.packetGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.packetGridView_RowsAdded);
            this.packetGridView.SelectionChanged += new System.EventHandler(this.packetGridView_SelectionChanged);
            // 
            // packetTimestamp
            // 
            this.packetTimestamp.DataPropertyName = "Timestamp";
            this.packetTimestamp.HeaderText = "Timestamp";
            this.packetTimestamp.Name = "packetTimestamp";
            this.packetTimestamp.ReadOnly = true;
            // 
            // packetClientID
            // 
            this.packetClientID.DataPropertyName = "ClientIndex";
            this.packetClientID.HeaderText = "Client";
            this.packetClientID.Name = "packetClientID";
            this.packetClientID.ReadOnly = true;
            // 
            // packetCallID
            // 
            this.packetCallID.DataPropertyName = "CallID";
            this.packetCallID.HeaderText = "CallID";
            this.packetCallID.Name = "packetCallID";
            this.packetCallID.ReadOnly = true;
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
            // packetOrigin
            // 
            this.packetOrigin.DataPropertyName = "Origin";
            this.packetOrigin.HeaderText = "Origin";
            this.packetOrigin.Name = "packetOrigin";
            this.packetOrigin.ReadOnly = true;
            // 
            // packetDestination
            // 
            this.packetDestination.DataPropertyName = "Destination";
            this.packetDestination.HeaderText = "Destination";
            this.packetDestination.Name = "packetDestination";
            this.packetDestination.ReadOnly = true;
            // 
            // packetService
            // 
            this.packetService.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packetService.DataPropertyName = "Service";
            this.packetService.HeaderText = "Service";
            this.packetService.Name = "packetService";
            this.packetService.ReadOnly = true;
            this.packetService.Width = 68;
            // 
            // packetCall
            // 
            this.packetCall.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packetCall.DataPropertyName = "Call";
            this.packetCall.HeaderText = "Call";
            this.packetCall.Name = "packetCall";
            this.packetCall.ReadOnly = true;
            this.packetCall.Width = 49;
            // 
            // packetTextBox
            // 
            this.packetTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetTextBox.Font = new System.Drawing.Font("Segoe UI Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.packetTextBox.Location = new System.Drawing.Point(3, 3);
            this.packetTextBox.Name = "packetTextBox";
            this.packetTextBox.ReadOnly = true;
            this.packetTextBox.Size = new System.Drawing.Size(778, 192);
            this.packetTextBox.TabIndex = 1;
            this.packetTextBox.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(792, 224);
            this.tabControl2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.packetTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(784, 198);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Text View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.packetTreeView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(784, 198);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Tree View";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // packetTreeView
            // 
            this.packetTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetTreeView.Location = new System.Drawing.Point(3, 3);
            this.packetTreeView.Name = "packetTreeView";
            this.packetTreeView.Size = new System.Drawing.Size(778, 192);
            this.packetTreeView.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "MainWindow";
            this.Text = "EVEmu Live Packet Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.clientListGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.serverInfoGroupBox.ResumeLayout(false);
            this.serverInfoGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.packetGridView)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TreeView packetTreeView;

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;

        private System.Windows.Forms.DataGridViewTextBoxColumn packetCall;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetCallID;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetOrigin;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetService;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetType;

        private System.Windows.Forms.SplitContainer splitContainer1;

        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientUser;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selected;

        private System.Windows.Forms.RichTextBox packetTextBox;

        private System.Windows.Forms.DataGridView packetGridView;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serverAddressTextBox;
        private System.Windows.Forms.GroupBox serverInfoGroupBox;
        private System.Windows.Forms.TextBox serverPortTextBox;

        private System.Windows.Forms.CheckBox sessionChangeCheckbox;

        private System.Windows.Forms.CheckBox includeResponsesCheckbox;

        private System.Windows.Forms.TextBox callNameTextbox;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serviceNameTextbox;

        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.CheckBox pingRspCheckbox;
        private System.Windows.Forms.CheckBox pingReqCheckbox;
        private System.Windows.Forms.CheckBox undeterminedCheckbox;

        private System.Windows.Forms.CheckBox callRspCheckbox;
        private System.Windows.Forms.CheckBox notificationCheckbox;
        private System.Windows.Forms.CheckBox exceptionCheckbox;

        private System.Windows.Forms.CheckBox callReqCheckbox;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        private System.Windows.Forms.DataGridView clientListGridView;

        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;

        private System.Windows.Forms.ToolStrip toolStrip1;

        #endregion
    }
}