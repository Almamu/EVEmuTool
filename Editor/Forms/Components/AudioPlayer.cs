using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor.Forms.Components
{
    public partial class AudioPlayer : UserControl
    {
        private WaveOutEvent mWaveOut = new WaveOutEvent();
        private WaveStream mWaveStream;

        public AudioPlayer(MemoryStream contents, string extension)
        {
            InitializeComponent();

            if (extension == "ogg")
            {
                this.mWaveStream = new NAudio.Vorbis.VorbisWaveReader(contents);
            }
            else if (extension == "mp3")
            {
                this.mWaveStream = WaveFormatConversionStream.CreatePcmStream(
                    new Mp3FileReaderBase(contents, new Mp3FileReaderBase.FrameDecompressorBuilder(wf => new AcmMp3FrameDecompressor(wf)))
                );
            }
            else if (extension == "wav")
            {
                this.mWaveStream = new WaveFileReader(contents);
            }

            // initialize the waveout with the right audio
            this.mWaveOut.Init(this.mWaveStream);
            this.mWaveOut.PlaybackStopped += MWaveOut_PlaybackStopped;
            this.waveViewer1.WaveStream = this.mWaveStream;
        }

        private void MWaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            this.playButton.Enabled = true;
            this.stopButton.Enabled = false;

            this.mWaveStream.Position = 0;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            this.mWaveOut.Play();

            this.playButton.Enabled = false;
            this.stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.mWaveOut.Stop();

            this.playButton.Enabled = true;
            this.stopButton.Enabled = false;
        }

        private void volumeSlider1_VolumeChanged(object sender, EventArgs e)
        {
            this.mWaveOut.Volume = this.volumeSlider1.Volume;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            this.mWaveOut.Dispose();
        }
    }
}
