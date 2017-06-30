using CSCore;
using CSCore.Codecs.WAV;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyCallCenter
{
    public partial class Form1 : Form
    {
        private Boolean recording = false;
        private WasapiCapture soundIn;
        private IWriteable writer;
        private IWaveSource waveSource;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Record_Click(object sender, EventArgs events)
        {
           
            if (!this.recording)
            {

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "WAV (*.wav)|*.wav",
                    Title = "Save",
                    FileName = String.Empty
                };
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    StartCapture(sfd.FileName);
                    this.statusTextBox.Text = "Gravando.";
                    this.RecordButton.Text = "Parar";
                    this.recording = true;
                }

            }
            else
            {
                StopCapture();
                this.statusTextBox.Text = "Não está gravando.";
                this.RecordButton.Text = "Gravar";
                this.recording = false;
            }


        }

        private void StartCapture(string fileName)
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
            this.soundIn = new WasapiCapture();
            this.soundIn.Device = defaultDevice;

            this.soundIn.Initialize();
            var soundInSource = new SoundInSource(this.soundIn) { FillWithZeros = false };
            //var singleBlockNotificationStream = new SingleBlockNotificationStream(soundInSource.ToSampleSource());
            //this.waveSource = singleBlockNotificationStream.ToWaveSource();
            this.waveSource = soundInSource.ToSampleSource().ToWaveSource();
            this.writer = new WaveWriter(fileName, this.waveSource.WaveFormat);

            byte[] buffer = new byte[this.waveSource.WaveFormat.BytesPerSecond / 2];
            soundInSource.DataAvailable += (s, e) =>
            {
                int read;
                while ((read = this.waveSource.Read(buffer, 0, buffer.Length)) > 0)
                    this.writer.Write(buffer, 0, read);
            };

            soundIn.Start();


        }

        private void StopCapture()
        {
            if (soundIn != null)
            {
                this.soundIn.Stop();
                this.soundIn.Dispose();
                this.soundIn = null;
                this.waveSource.Dispose();

                if (writer is IDisposable)
                    ((IDisposable)writer).Dispose();
            }
        }

        private void SkyCallCenter_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.NotifycationIcon.Visible = true;
                this.NotifycationIcon.BalloonTipTitle = "BatMorcego Avisa";
                this.NotifycationIcon.BalloonTipText = "Continuo funcionando \n Para Gravar";
                this.NotifycationIcon.BalloonTipIcon = ToolTipIcon.Info;
                this.NotifycationIcon.ShowBalloonTip(1000);
                this.Hide();
            }else if(this.WindowState == FormWindowState.Normal)
            {
                this.NotifycationIcon.Visible = false;
            }
        }

        private void NotificationIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
