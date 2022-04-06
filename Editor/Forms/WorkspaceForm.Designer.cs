namespace Editor.Forms
{
    partial class WorkspaceForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openSavedWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogServerWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.openRawMarshalData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.startPacketCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.stopPacketCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openLogServerButton = new System.Windows.Forms.ToolStripButton();
            this.openRawMarshalButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startPacketCaptureButton = new System.Windows.Forms.ToolStripButton();
            this.stopPacketCaptureButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.serverMenu,
            this.toolsMenu,
            this.windowsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(737, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSavedWorkspace,
            this.openLogServerWorkspace,
            this.openRawMarshalData,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // openSavedWorkspace
            // 
            this.openSavedWorkspace.Image = global::Editor.Properties.Resources.OpenFolder;
            this.openSavedWorkspace.ImageTransparentColor = System.Drawing.Color.Black;
            this.openSavedWorkspace.Name = "openSavedWorkspace";
            this.openSavedWorkspace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openSavedWorkspace.Size = new System.Drawing.Size(241, 22);
            this.openSavedWorkspace.Text = "&Open Saved Workspace";
            this.openSavedWorkspace.Click += new System.EventHandler(this.OpenFile);
            // 
            // openLogServerWorkspace
            // 
            this.openLogServerWorkspace.Image = global::Editor.Properties.Resources._1;
            this.openLogServerWorkspace.Name = "openLogServerWorkspace";
            this.openLogServerWorkspace.Size = new System.Drawing.Size(241, 22);
            this.openLogServerWorkspace.Text = "Open LogServer Workspace";
            this.openLogServerWorkspace.Click += new System.EventHandler(this.OpenLogServerWorkspace);
            // 
            // openRawMarshalData
            // 
            this.openRawMarshalData.Image = global::Editor.Properties.Resources.Marshal;
            this.openRawMarshalData.Name = "openRawMarshalData";
            this.openRawMarshalData.Size = new System.Drawing.Size(241, 22);
            this.openRawMarshalData.Text = "Open Raw &Marshal Data";
            this.openRawMarshalData.Click += new System.EventHandler(this.OpenRawMarshalData);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Editor.Properties.Resources.Save;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.saveToolStripMenuItem.Text = "&Save As...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(238, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.CloseForm);
            // 
            // serverMenu
            // 
            this.serverMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startPacketCapture,
            this.stopPacketCapture});
            this.serverMenu.Name = "serverMenu";
            this.serverMenu.Size = new System.Drawing.Size(51, 20);
            this.serverMenu.Text = "&Server";
            // 
            // startPacketCapture
            // 
            this.startPacketCapture.Image = global::Editor.Properties.Resources.Play;
            this.startPacketCapture.Name = "startPacketCapture";
            this.startPacketCapture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.startPacketCapture.Size = new System.Drawing.Size(223, 22);
            this.startPacketCapture.Text = "Start Packet Capture";
            this.startPacketCapture.Click += new System.EventHandler(this.StartServer);
            // 
            // stopPacketCapture
            // 
            this.stopPacketCapture.Enabled = false;
            this.stopPacketCapture.Image = global::Editor.Properties.Resources.Stop;
            this.stopPacketCapture.Name = "stopPacketCapture";
            this.stopPacketCapture.Size = new System.Drawing.Size(223, 22);
            this.stopPacketCapture.Text = "Stop Packet Capture";
            this.stopPacketCapture.Click += new System.EventHandler(this.StopServer);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(46, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "&Settings";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OpenSettingsWindow);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(63, 20);
            this.windowsMenu.Text = "&Window";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertically";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontally";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.closeAllToolStripMenuItem.Text = "&Close all";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.openLogServerButton,
            this.openRawMarshalButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.startPacketCaptureButton,
            this.stopPacketCaptureButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(737, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Editor.Properties.Resources.OpenFolder;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Abrir";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // openLogServerButton
            // 
            this.openLogServerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openLogServerButton.Image = global::Editor.Properties.Resources._1;
            this.openLogServerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openLogServerButton.Name = "openLogServerButton";
            this.openLogServerButton.Size = new System.Drawing.Size(23, 22);
            this.openLogServerButton.Text = "openLogServerButton";
            this.openLogServerButton.Click += new System.EventHandler(this.OpenLogServerWorkspace);
            // 
            // openRawMarshalButton
            // 
            this.openRawMarshalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openRawMarshalButton.Image = global::Editor.Properties.Resources.Marshal;
            this.openRawMarshalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openRawMarshalButton.Name = "openRawMarshalButton";
            this.openRawMarshalButton.Size = new System.Drawing.Size(23, 22);
            this.openRawMarshalButton.Text = "toolStripButton2";
            this.openRawMarshalButton.Click += new System.EventHandler(this.OpenRawMarshalData);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Editor.Properties.Resources.Save;
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Guardar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // startPacketCaptureButton
            // 
            this.startPacketCaptureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startPacketCaptureButton.Image = global::Editor.Properties.Resources.Play;
            this.startPacketCaptureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startPacketCaptureButton.Name = "startPacketCaptureButton";
            this.startPacketCaptureButton.Size = new System.Drawing.Size(23, 22);
            this.startPacketCaptureButton.Text = "Start Packet Capture";
            this.startPacketCaptureButton.Click += new System.EventHandler(this.StartServer);
            // 
            // stopPacketCaptureButton
            // 
            this.stopPacketCaptureButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopPacketCaptureButton.Image = global::Editor.Properties.Resources.Stop;
            this.stopPacketCaptureButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopPacketCaptureButton.Name = "stopPacketCaptureButton";
            this.stopPacketCaptureButton.Size = new System.Drawing.Size(23, 22);
            this.stopPacketCaptureButton.Text = "Stop Packet Capture";
            this.stopPacketCaptureButton.Click += new System.EventHandler(this.StopServer);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // WorkspaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 523);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WorkspaceForm";
            this.Text = "EVEmu Live Packet Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingForm);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openSavedWorkspace;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverMenu;
        private System.Windows.Forms.ToolStripMenuItem startPacketCapture;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem stopPacketCapture;
        private System.Windows.Forms.ToolStripMenuItem openLogServerWorkspace;
        private System.Windows.Forms.ToolStripMenuItem openRawMarshalData;
        private System.Windows.Forms.ToolStripButton openLogServerButton;
        private System.Windows.Forms.ToolStripButton openRawMarshalButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton startPacketCaptureButton;
        private System.Windows.Forms.ToolStripButton stopPacketCaptureButton;
    }
}



