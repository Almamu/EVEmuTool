namespace EVEmuTool.Forms.Components
{
    partial class MarshalTreeViewComponent
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
            this.packetTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // packetTreeView
            // 
            this.packetTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetTreeView.Location = new System.Drawing.Point(0, 0);
            this.packetTreeView.Name = "packetTreeView";
            this.packetTreeView.Size = new System.Drawing.Size(676, 420);
            this.packetTreeView.TabIndex = 0;
            // 
            // MarshalTreeViewComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.packetTreeView);
            this.Name = "MarshalTreeViewComponent";
            this.Size = new System.Drawing.Size(676, 420);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView packetTreeView;
    }
}
