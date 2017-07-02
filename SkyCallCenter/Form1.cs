using CSCore;
using CSCore.Codecs.WAV;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using Hotkeys;
using BatRecording;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    public partial class Form1 : Form
    {
        private Boolean recording = false;
        private WasapiCapture soundIn;
        private IWriteable writer;
        private IWaveSource waveSource;
        private GlobalHotKey ghk;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.ghk = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.K , this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!this.ghk.Register()) {
                throw new Exception("Não foi possivel registrar o atalho de escopo global");
            }
        }

        private void Record_Click(object sender, EventArgs events)
        {
           
            if (!this.recording)
            {

                StartCapture("out.wav");
                this.statusTextBox.Text = "Gravando.";
                this.RecordButton.Text = "Parar";
                this.recording = true;

            }
            else
            {
                StopCapture();
                this.statusTextBox.Text = "Não está gravando.";
                this.RecordButton.Text = "Gravar";
                this.recording = false;
                saveFileRecord();

            }

        }
        private void saveFileRecord()
        {
            var saveRecord = MessageBox.Show("Deseja salvar essa gravação?", "Salvar gravação", MessageBoxButtons.YesNo);

            if (saveRecord == DialogResult.Yes)
            {
                SaveAudioDialog saveAudio = new SaveAudioDialog();
                if (saveAudio.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Agora");
                }
                saveAudio.Dispose();

            }
            else
            {
                var deleteRecord = MessageBox.Show("Tem certeza que deseja apagar a gravação? \nEssa ação não pode ser desfeita.", "Apagar gravação ?", MessageBoxButtons.YesNo);
                if (deleteRecord == DialogResult.Yes)
                {
                    if (File.Exists(@"out.wav"))
                    {
                        File.Delete(@"out.wav");
                    }
                }
                else
                {
                    SaveAudioDialog saveAudio = new SaveAudioDialog();
                    if (saveAudio.ShowDialog(this) == DialogResult.OK)
                    {
                        MessageBox.Show("Agora");
                    }
                    saveAudio.Dispose();
                }

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
                this.NotifycationIcon.BalloonTipTitle = "Bat Recording";
                this.NotifycationIcon.BalloonTipText = "Minimizado \nPara Gravar Pressione CTRL+ SHIT + K";
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


        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
                MessageBox.Show("Hotkey failed to unregister!");

        }

        private void StartRecording()
        {
            this.WindowState = FormWindowState.Minimized;
            this.NotifycationIcon.Visible = true;
            this.NotifycationIcon.BalloonTipTitle = "Bat Recording";
            this.NotifycationIcon.BalloonTipText = "Gravando \nPara parar Pressione CTRL+ SHIT + P";
            this.NotifycationIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.NotifycationIcon.ShowBalloonTip(2000);
            this.Hide();
            StartCapture("out.wav");
        }

        private Keys GetKey(IntPtr LParam)
        {
            return (Keys)((LParam.ToInt32()) >> 16);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                switch (GetKey(m.LParam))
                {
                    case Keys.K:
                        StartRecording();
                        break;
                    case Keys.P:
                        break;
                }
            }
            base.WndProc(ref m);
        }
    }
}
