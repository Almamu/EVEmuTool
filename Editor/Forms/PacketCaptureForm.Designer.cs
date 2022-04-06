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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.packetGridView = new System.Windows.Forms.DataGridView();
            this.packetTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCallID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetDestination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetCall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openInMarshalViewer = new System.Windows.Forms.ToolStripMenuItem();
            this.marshalTreeView = new Editor.Forms.Components.MarshalTreeViewComponent();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(803, 622);
            this.splitContainer1.SplitterDistance = 125;
            this.splitContainer1.TabIndex = 0;
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
            this.splitContainer2.Size = new System.Drawing.Size(803, 493);
            this.splitContainer2.SplitterDistance = 313;
            this.splitContainer2.TabIndex = 0;
            // 
            // packetGridView
            // 
            this.packetGridView.AllowUserToAddRows = false;
            this.packetGridView.AllowUserToDeleteRows = false;
            this.packetGridView.AllowUserToOrderColumns = true;
            this.packetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packetTimestamp,
            this.packetClientID,
            this.packetCallID,
            this.packetType,
            this.packetSource,
            this.packetDestination,
            this.packetService,
            this.packetCall});
            this.packetGridView.ContextMenuStrip = this.packetListContextMenu;
            this.packetGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetGridView.Location = new System.Drawing.Point(0, 0);
            this.packetGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.packetGridView.Name = "packetGridView";
            this.packetGridView.ReadOnly = true;
            this.packetGridView.Size = new System.Drawing.Size(803, 313);
            this.packetGridView.TabIndex = 1;
            this.packetGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OpenInPacketViewer);
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
            // packetClientID
            // 
            this.packetClientID.DataPropertyName = "ClientID";
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
            this.openInMarshalViewer});
            this.packetListContextMenu.Name = "packetListContextMenu";
            this.packetListContextMenu.Size = new System.Drawing.Size(252, 26);
            // 
            // openInMarshalViewer
            // 
            this.openInMarshalViewer.Name = "openInMarshalViewer";
            this.openInMarshalViewer.Size = new System.Drawing.Size(251, 22);
            this.openInMarshalViewer.Text = "Open in Raw Marshal Data Viewer";
            this.openInMarshalViewer.Click += new System.EventHandler(this.OpenInPacketViewer);
            // 
            // marshalTreeView
            // 
            this.marshalTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marshalTreeView.Location = new System.Drawing.Point(0, 0);
            this.marshalTreeView.Name = "marshalTreeView";
            this.marshalTreeView.Size = new System.Drawing.Size(803, 176);
            this.marshalTreeView.TabIndex = 0;
            // 
            // PacketCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 622);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PacketCaptureForm";
            this.Text = "PacketCaptureForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaptureWindowClosing);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn packetTimestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetCallID;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetDestination;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetService;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetCall;
        private System.Windows.Forms.ContextMenuStrip packetListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openInMarshalViewer;
        private Components.MarshalTreeViewComponent marshalTreeView;
    }
}