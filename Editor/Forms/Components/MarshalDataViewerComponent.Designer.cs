namespace EVEmuTool.Forms.Components
{
    partial class MarshalDataViewerComponent
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.packetTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.marshalTreeView = new EVEmuTool.Forms.Components.MarshalTreeViewComponent();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.insightTreeView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hexViewHost = new System.Windows.Forms.Integration.ElementHost();
            this.applyChangesAndReload = new System.Windows.Forms.Button();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(710, 441);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.packetTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage3.Size = new System.Drawing.Size(702, 413);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Text View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // packetTextBox
            // 
            this.packetTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetTextBox.Font = new System.Drawing.Font("Segoe UI Mono", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.packetTextBox.Location = new System.Drawing.Point(4, 3);
            this.packetTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.packetTextBox.Name = "packetTextBox";
            this.packetTextBox.ReadOnly = true;
            this.packetTextBox.Size = new System.Drawing.Size(694, 407);
            this.packetTextBox.TabIndex = 1;
            this.packetTextBox.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.marshalTreeView);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage4.Size = new System.Drawing.Size(702, 413);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Tree View";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // marshalTreeView
            // 
            this.marshalTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.marshalTreeView.Location = new System.Drawing.Point(4, 3);
            this.marshalTreeView.Name = "marshalTreeView";
            this.marshalTreeView.Size = new System.Drawing.Size(694, 407);
            this.marshalTreeView.TabIndex = 0;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.splitContainer3);
            this.tabPage11.Location = new System.Drawing.Point(4, 24);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(702, 413);
            this.tabPage11.TabIndex = 2;
            this.tabPage11.Text = "Hex View";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.insightTreeView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel1);
            this.splitContainer3.Panel2.Controls.Add(this.applyChangesAndReload);
            this.splitContainer3.Size = new System.Drawing.Size(696, 407);
            this.splitContainer3.SplitterDistance = 231;
            this.splitContainer3.TabIndex = 3;
            // 
            // insightTreeView
            // 
            this.insightTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insightTreeView.Location = new System.Drawing.Point(0, 0);
            this.insightTreeView.Name = "insightTreeView";
            this.insightTreeView.Size = new System.Drawing.Size(231, 407);
            this.insightTreeView.TabIndex = 0;
            this.insightTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SelectInsightNode);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hexViewHost);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 384);
            this.panel1.TabIndex = 4;
            // 
            // hexViewHost
            // 
            this.hexViewHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexViewHost.Location = new System.Drawing.Point(0, 0);
            this.hexViewHost.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hexViewHost.Name = "hexViewHost";
            this.hexViewHost.Size = new System.Drawing.Size(461, 384);
            this.hexViewHost.TabIndex = 2;
            // 
            // applyChangesAndReload
            // 
            this.applyChangesAndReload.Dock = System.Windows.Forms.DockStyle.Top;
            this.applyChangesAndReload.Location = new System.Drawing.Point(0, 0);
            this.applyChangesAndReload.Name = "applyChangesAndReload";
            this.applyChangesAndReload.Size = new System.Drawing.Size(461, 23);
            this.applyChangesAndReload.TabIndex = 3;
            this.applyChangesAndReload.Text = "Apply changes and reload";
            this.applyChangesAndReload.UseVisualStyleBackColor = true;
            this.applyChangesAndReload.Click += new System.EventHandler(this.ApplyChangesAndReload);
            // 
            // MarshalDataViewerComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Name = "MarshalDataViewerComponent";
            this.Size = new System.Drawing.Size(710, 441);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox packetTextBox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView insightTreeView;
        private System.Windows.Forms.Button applyChangesAndReload;
        private System.Windows.Forms.Integration.ElementHost hexViewHost;
        private System.Windows.Forms.Panel panel1;
        private MarshalTreeViewComponent marshalTreeView;
    }
}
