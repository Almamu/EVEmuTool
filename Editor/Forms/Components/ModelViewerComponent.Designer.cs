namespace Editor.Forms.Components
{
    partial class ModelViewerComponent
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.surfaceTypeLabel = new System.Windows.Forms.Label();
            this.surfaceCountLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.boundingBoxMinLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.boundingBoxMaxLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.vertexSizeLabel = new System.Windows.Forms.Label();
            this.vertexCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 482);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(627, 63);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.surfaceTypeLabel);
            this.groupBox4.Controls.Add(this.surfaceCountLabel);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(183, 57);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Surfaces";
            // 
            // surfaceTypeLabel
            // 
            this.surfaceTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.surfaceTypeLabel.AutoSize = true;
            this.surfaceTypeLabel.Location = new System.Drawing.Point(72, 34);
            this.surfaceTypeLabel.Name = "surfaceTypeLabel";
            this.surfaceTypeLabel.Size = new System.Drawing.Size(37, 15);
            this.surfaceTypeLabel.TabIndex = 7;
            this.surfaceTypeLabel.Text = "00000";
            this.surfaceTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // surfaceCountLabel
            // 
            this.surfaceCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.surfaceCountLabel.AutoSize = true;
            this.surfaceCountLabel.Location = new System.Drawing.Point(72, 19);
            this.surfaceCountLabel.Name = "surfaceCountLabel";
            this.surfaceCountLabel.Size = new System.Drawing.Size(37, 15);
            this.surfaceCountLabel.TabIndex = 6;
            this.surfaceCountLabel.Text = "00000";
            this.surfaceCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "Count:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel2);
            this.groupBox3.Location = new System.Drawing.Point(192, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 57);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bounding Box";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.boundingBoxMinLabel);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.boundingBoxMaxLabel);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(236, 35);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Min:";
            // 
            // boundingBoxMinLabel
            // 
            this.flowLayoutPanel2.SetFlowBreak(this.boundingBoxMinLabel, true);
            this.boundingBoxMinLabel.Location = new System.Drawing.Point(40, 0);
            this.boundingBoxMinLabel.Name = "boundingBoxMinLabel";
            this.boundingBoxMinLabel.Size = new System.Drawing.Size(193, 15);
            this.boundingBoxMinLabel.TabIndex = 3;
            this.boundingBoxMinLabel.Text = "00000";
            this.boundingBoxMinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Max:";
            // 
            // boundingBoxMaxLabel
            // 
            this.boundingBoxMaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boundingBoxMaxLabel.Location = new System.Drawing.Point(42, 15);
            this.boundingBoxMaxLabel.Name = "boundingBoxMaxLabel";
            this.boundingBoxMaxLabel.Size = new System.Drawing.Size(191, 20);
            this.boundingBoxMaxLabel.TabIndex = 4;
            this.boundingBoxMaxLabel.Text = "00000";
            this.boundingBoxMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.vertexSizeLabel);
            this.groupBox2.Controls.Add(this.vertexCountLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(440, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(122, 57);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vertices";
            // 
            // vertexSizeLabel
            // 
            this.vertexSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vertexSizeLabel.AutoSize = true;
            this.vertexSizeLabel.Location = new System.Drawing.Point(-77, 34);
            this.vertexSizeLabel.Name = "vertexSizeLabel";
            this.vertexSizeLabel.Size = new System.Drawing.Size(37, 15);
            this.vertexSizeLabel.TabIndex = 3;
            this.vertexSizeLabel.Text = "00000";
            this.vertexSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vertexCountLabel
            // 
            this.vertexCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vertexCountLabel.AutoSize = true;
            this.vertexCountLabel.Location = new System.Drawing.Point(-77, 19);
            this.vertexCountLabel.Name = "vertexCountLabel";
            this.vertexCountLabel.Size = new System.Drawing.Size(37, 15);
            this.vertexCountLabel.TabIndex = 2;
            this.vertexCountLabel.Text = "00000";
            this.vertexCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Count:";
            // 
            // ModelViewerComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ModelViewerComponent";
            this.Size = new System.Drawing.Size(627, 545);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label surfaceTypeLabel;
        private System.Windows.Forms.Label surfaceCountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label boundingBoxMinLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label boundingBoxMaxLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label vertexSizeLabel;
        private System.Windows.Forms.Label vertexCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
