namespace EVEmuTool.Forms.Components
{
    partial class AudioPlayer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.stopButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.waveViewer1 = new NAudio.Gui.WaveViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.volumeSlider1);
            this.panel1.Controls.Add(this.stopButton);
            this.panel1.Controls.Add(this.playButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 364);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 33);
            this.panel1.TabIndex = 0;
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Location = new System.Drawing.Point(167, 7);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(93, 23);
            this.volumeSlider1.TabIndex = 2;
            this.volumeSlider1.VolumeChanged += new System.EventHandler(this.volumeSlider1_VolumeChanged);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Image = global::EVEmuTool.Properties.Resources.Stop;
            this.stopButton.Location = new System.Drawing.Point(86, 7);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // playButton
            // 
            this.playButton.Image = global::EVEmuTool.Properties.Resources.Play;
            this.playButton.Location = new System.Drawing.Point(5, 7);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 0;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // waveViewer1
            // 
            this.waveViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveViewer1.Location = new System.Drawing.Point(0, 0);
            this.waveViewer1.Name = "waveViewer1";
            this.waveViewer1.SamplesPerPixel = 128;
            this.waveViewer1.Size = new System.Drawing.Size(527, 364);
            this.waveViewer1.StartPosition = ((long)(0));
            this.waveViewer1.TabIndex = 1;
            this.waveViewer1.WaveStream = null;
            // 
            // AudioPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.waveViewer1);
            this.Controls.Add(this.panel1);
            this.Name = "AudioPlayer";
            this.Size = new System.Drawing.Size(527, 397);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private NAudio.Gui.WaveViewer waveViewer1;
        private NAudio.Gui.VolumeSlider volumeSlider1;
    }
}
