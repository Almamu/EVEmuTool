namespace EVEmuTool.Forms
{
    partial class LogViewerForm
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.storagesTabs = new System.Windows.Forms.TabControl();
            this.logViewExpanded = new System.Windows.Forms.RichTextBox();
            this.logViewerContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.parseAsMarshalDataOption = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.loadingStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.logViewerContext.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer2.Panel1.Controls.Add(this.storagesTabs);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.logViewExpanded);
            this.splitContainer2.Size = new System.Drawing.Size(800, 403);
            this.splitContainer2.SplitterDistance = 302;
            this.splitContainer2.TabIndex = 5;
            // 
            // storagesTabs
            // 
            this.storagesTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storagesTabs.Location = new System.Drawing.Point(0, 0);
            this.storagesTabs.Name = "storagesTabs";
            this.storagesTabs.SelectedIndex = 0;
            this.storagesTabs.Size = new System.Drawing.Size(800, 302);
            this.storagesTabs.TabIndex = 2;
            // 
            // logViewExpanded
            // 
            this.logViewExpanded.ContextMenuStrip = this.logViewerContext;
            this.logViewExpanded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewExpanded.Location = new System.Drawing.Point(0, 0);
            this.logViewExpanded.Name = "logViewExpanded";
            this.logViewExpanded.ReadOnly = true;
            this.logViewExpanded.Size = new System.Drawing.Size(800, 97);
            this.logViewExpanded.TabIndex = 3;
            this.logViewExpanded.Text = "~\\x00\\x00\\x00\\x00\\x17\\x10\\x15objectCaching.CacheOK\\x16\\x01%\\x10\\x07CacheOK\\x10\\x0" +
    "4args";
            // 
            // logViewerContext
            // 
            this.logViewerContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseAsMarshalDataOption});
            this.logViewerContext.Name = "contextMenuStrip1";
            this.logViewerContext.Size = new System.Drawing.Size(188, 48);
            // 
            // parseAsMarshalDataOption
            // 
            this.parseAsMarshalDataOption.Name = "parseAsMarshalDataOption";
            this.parseAsMarshalDataOption.Size = new System.Drawing.Size(187, 22);
            this.parseAsMarshalDataOption.Text = "Parse as marshal data";
            this.parseAsMarshalDataOption.Click += new System.EventHandler(this.ParseDataAsMarshalContent);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadingStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // loadingStatus
            // 
            this.loadingStatus.Name = "loadingStatus";
            this.loadingStatus.Size = new System.Drawing.Size(114, 17);
            this.loadingStatus.Text = "Loading Workspace ";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 403);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 450);
            this.toolStripContainer1.TabIndex = 7;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // LogViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "LogViewerForm";
            this.Text = "LogViewerForm";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.logViewerContext.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl storagesTabs;
        private System.Windows.Forms.RichTextBox logViewExpanded;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel loadingStatus;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ContextMenuStrip logViewerContext;
        private System.Windows.Forms.ToolStripMenuItem parseAsMarshalDataOption;
    }
}