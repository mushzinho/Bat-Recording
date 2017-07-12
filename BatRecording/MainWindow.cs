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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;

namespace BatRecording
{
    public partial class MainWindow : Form
    {
        private Boolean _recording = false;
        private WasapiCapture _soundIn;
        private IWriteable _writer;
        private IWaveSource _waveSource;
        private GlobalHotKey _ghk;
        private GlobalHotKey _ghk1;
        private const string WavOut = "out.wav";

        public MainWindow()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._ghk = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.K , this);
            this._ghk1 = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.P, this);

            new Employer().CreateEmployer("pabloa", "pablodawdwa","pass");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!this._ghk.Register() || !this._ghk1.Register()) {
                throw new Exception("Não foi possivel registrar o atalho de escopo global");
            }
        }
        private void ToggleRecordButton()
        {
            if (!this._recording)
            {
                this.statusTextBox.Text = @"Gravando.";
                this.RecordButton.Text = @"Parar";
                this._recording = true;
            }else
            {
                this.statusTextBox.Text = @"Não está gravando.";
                this.RecordButton.Text = @"Gravar";
                this._recording = false;
            }
        }

        private void Record_Click(object sender, EventArgs events)
        {
           
            if (!this._recording)
            {

                StartCapture(WavOut);
                ToggleRecordButton();

            }
            else
            {
                StopCapture();
                ToggleRecordButton();
                SaveFileRecorded();

            }

        }

        private bool SaveFileToFtpServer(string sourceFileName)
        {
          
            var manageFtp = new ManageFtp("pablo", "pablo");
            var client = manageFtp.Client;
            var url = Path.GetFullPath(sourceFileName);
            var upload = client.UploadFile(@url, "/" + @sourceFileName , FtpExists.NoCheck, true);
            client.Disconnect();
            return upload;
        }

        private void SaveFileRecorded()
        {
            var saveRecord = MessageBox.Show(@"Deseja salvar essa gravação?", @"Salvar gravação", MessageBoxButtons.YesNo);

            if (saveRecord == DialogResult.Yes)
            {
                SaveAudioDialog saveAudio = new SaveAudioDialog();
                if (saveAudio.ShowDialog(this) == DialogResult.OK)
                {
                    if (SaveFileToFtpServer(saveAudio.OutFileNameComplete))
                    {
                        MessageBox.Show(@"Upload Completo");
                    }
                    
                }
                saveAudio.Dispose();

            }
            else
            {
                var deleteRecord = MessageBox.Show(@"Tem certeza que deseja apagar a gravação? 
Essa ação não pode ser desfeita.", @"Apagar gravação ?", MessageBoxButtons.YesNo);
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
                        MessageBox.Show(saveAudio.OutFileNameComplete);
                    }
                    saveAudio.Dispose();
                }

            }
        }

        private void StartCapture(string fileName)
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Multimedia);
            this._soundIn = new WasapiCapture {Device = defaultDevice};

            this._soundIn.Initialize();
            var soundInSource = new SoundInSource(this._soundIn) { FillWithZeros = false };
            this._waveSource = soundInSource.ToSampleSource().ToWaveSource();
            this._writer = new WaveWriter(fileName, this._waveSource.WaveFormat);

            byte[] buffer = new byte[this._waveSource.WaveFormat.BytesPerSecond / 2];
            soundInSource.DataAvailable += (s, e) =>
            {
                int read;
                while ((read = this._waveSource.Read(buffer, 0, buffer.Length)) > 0)
                    this._writer.Write(buffer, 0, read);
            };

            _soundIn.Start();
        }

        private void StopCapture()
        {
            if (_soundIn != null)
            {
                this._soundIn.Stop();
                this._soundIn.Dispose();
                this._soundIn = null;
                this._waveSource.Dispose();

                if (_writer is IDisposable)
                    ((IDisposable)_writer).Dispose();
            }
        }

        private void SkyCallCenter_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                this.NotifycationIcon.Visible = true;
                this.NotifycationIcon.BalloonTipTitle = @"Bat Recording";
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
            if (!_ghk.Unregiser() || !_ghk1.Unregiser())
                MessageBox.Show(@"Hotkey failed to unregister!");

        }

        private void StartRecording()
        {
            //this.WindowState = FormWindowState.Minimized;
            this.NotifycationIcon.Visible = true;
            this.NotifycationIcon.BalloonTipTitle = @"Bat Recording";
            this.NotifycationIcon.BalloonTipText = "Gravando \nPara parar Pressione CTRL+ SHIT + P";
            this.NotifycationIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.NotifycationIcon.ShowBalloonTip(2000);
           // this.Hide();
            StartCapture(WavOut);
            ToggleRecordButton();
        }
        private void StopRecording()
        {
            StopCapture();
            ToggleRecordButton();
            SaveFileRecorded();
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
                        StopRecording();
                        break;
                }
            }
            base.WndProc(ref m);
        }
    }
}
