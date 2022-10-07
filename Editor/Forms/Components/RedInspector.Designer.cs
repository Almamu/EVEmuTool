namespace EVEmuTool.Forms.Components
{
    partial class RedInspector
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.viewerTab = new System.Windows.Forms.TabPage();
            this.contentRichText = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.viewerTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 494);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.contentRichText);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(547, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inspect text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // viewerTab
            // 
            this.viewerTab.Location = new System.Drawing.Point(4, 4);
            this.viewerTab.Name = "viewerTab";
            this.viewerTab.Padding = new System.Windows.Forms.Padding(3);
            this.viewerTab.Size = new System.Drawing.Size(547, 466);
            this.viewerTab.TabIndex = 1;
            this.viewerTab.Text = "3D View";
            this.viewerTab.UseVisualStyleBackColor = true;
            // 
            // contentRichText
            // 
            this.contentRichText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentRichText.Location = new System.Drawing.Point(3, 3);
            this.contentRichText.Name = "contentRichText";
            this.contentRichText.Size = new System.Drawing.Size(541, 460);
            this.contentRichText.TabIndex = 0;
            this.contentRichText.Text = "";
            // 
            // RedInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "RedInspector";
            this.Size = new System.Drawing.Size(555, 494);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage viewerTab;
        private System.Windows.Forms.RichTextBox contentRichText;
    }
}
