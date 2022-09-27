namespace Editor.Forms
{
    partial class StuffExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StuffExplorer));
            this.stuffExplorerModeTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fileListView = new System.Windows.Forms.TreeView();
            this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileIconImages = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.helpText = new System.Windows.Forms.Label();
            this.stuffExplorerModeTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.rightClickMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stuffExplorerModeTabs
            // 
            this.stuffExplorerModeTabs.Controls.Add(this.tabPage1);
            this.stuffExplorerModeTabs.Controls.Add(this.tabPage2);
            this.stuffExplorerModeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stuffExplorerModeTabs.Location = new System.Drawing.Point(0, 0);
            this.stuffExplorerModeTabs.Name = "stuffExplorerModeTabs";
            this.stuffExplorerModeTabs.SelectedIndex = 0;
            this.stuffExplorerModeTabs.Size = new System.Drawing.Size(263, 499);
            this.stuffExplorerModeTabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fileListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(255, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File list";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fileListView
            // 
            this.fileListView.ContextMenuStrip = this.rightClickMenu;
            this.fileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListView.ImageIndex = 0;
            this.fileListView.ImageList = this.fileIconImages;
            this.fileListView.Location = new System.Drawing.Point(3, 3);
            this.fileListView.Name = "fileListView";
            this.fileListView.SelectedImageIndex = 0;
            this.fileListView.Size = new System.Drawing.Size(249, 465);
            this.fileListView.TabIndex = 1;
            this.fileListView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileListView_AfterSelect);
            // 
            // rightClickMenu
            // 
            this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.rightClickMenu.Name = "rightClickMenu";
            this.rightClickMenu.Size = new System.Drawing.Size(118, 48);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // fileIconImages
            // 
            this.fileIconImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.fileIconImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fileIconImages.ImageStream")));
            this.fileIconImages.TransparentColor = System.Drawing.Color.Transparent;
            this.fileIconImages.Images.SetKeyName(0, "FolderOpened.png");
            this.fileIconImages.Images.SetKeyName(1, "Image.png");
            this.fileIconImages.Images.SetKeyName(2, "ModelThreeD.png");
            this.fileIconImages.Images.SetKeyName(3, "File.png");
            this.fileIconImages.Images.SetKeyName(4, "Blue.png");
            this.fileIconImages.Images.SetKeyName(5, "Red.png");
            this.fileIconImages.Images.SetKeyName(6, "StyleSheet.png");
            this.fileIconImages.Images.SetKeyName(7, "Pickle.png");
            this.fileIconImages.Images.SetKeyName(8, "TextFile.png");
            this.fileIconImages.Images.SetKeyName(9, "Sound.png");
            this.fileIconImages.Images.SetKeyName(10, "Shader.png");
            this.fileIconImages.Images.SetKeyName(11, "Pink.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fileTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(255, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File tree";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // fileTreeView
            // 
            this.fileTreeView.ContextMenuStrip = this.rightClickMenu;
            this.fileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeView.ImageIndex = 0;
            this.fileTreeView.ImageList = this.fileIconImages;
            this.fileTreeView.Location = new System.Drawing.Point(3, 3);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.SelectedImageIndex = 0;
            this.fileTreeView.Size = new System.Drawing.Size(249, 465);
            this.fileTreeView.TabIndex = 0;
            this.fileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.stuffExplorerModeTabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.helpText);
            this.splitContainer1.Size = new System.Drawing.Size(653, 499);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 2;
            // 
            // helpText
            // 
            this.helpText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpText.Location = new System.Drawing.Point(0, 0);
            this.helpText.Name = "helpText";
            this.helpText.Size = new System.Drawing.Size(386, 499);
            this.helpText.TabIndex = 0;
            this.helpText.Text = "Select a file on the left to preview it";
            this.helpText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StuffExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 499);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StuffExplorer";
            this.Text = ".stuff Explorer";
            this.stuffExplorerModeTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.rightClickMenu.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl stuffExplorerModeTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView fileListView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.ImageList fileIconImages;
        private System.Windows.Forms.ContextMenuStrip rightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label helpText;
    }
}