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
    public partial class Form1 : Form
    {
        private Boolean recording = false;
        private WasapiCapture soundIn;
        private IWriteable writer;
        private IWaveSource waveSource;
        private GlobalHotKey ghk;
        private GlobalHotKey ghk1;
        private const string wavOut = "out.wav";

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.ghk = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.K , this);
            this.ghk1 = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.P, this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!this.ghk.Register() || !this.ghk1.Register()) {
                throw new Exception("Não foi possivel registrar o atalho de escopo global");
            }
        }
        private void ToggleRecordButton()
        {
            if (!this.recording)
            {
                this.statusTextBox.Text = @"Gravando.";
                this.RecordButton.Text = @"Parar";
                this.recording = true;
            }else
            {
                this.statusTextBox.Text = @"Não está gravando.";
                this.RecordButton.Text = @"Gravar";
                this.recording = false;
            }
        }

        private void Record_Click(object sender, EventArgs events)
        {
           
            if (!this.recording)
            {

                StartCapture(wavOut);
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
            FtpClient client = new FtpClient("192.168.11.2")
            {
                Port = 29,
                Credentials = new NetworkCredential("pablo", "pablo")
            };
            client.Connect();
            client.RetryAttempts = 3;
            var url = @"C:\Users\Pablo\Documents\visual studio 2015\Projects\SkyCallCenter\SkyCallCenter\bin\Debug\3_7_2017\pablo\PABLO HENRQIUE_88888899912_125042.mp3";
            var upload = client.UploadFile(url, @"/pata.mp3", FtpExists.NoCheck);
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
            this.soundIn = new WasapiCapture {Device = defaultDevice};

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
                this.NotifycationIcon.BalloonTipTitle = @"Bat Recording";
                this.NotifycationIcon.BalloonTipText = @"Minimizado \nPara Gravar Pressione CTRL+ SHIT + K";
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
                MessageBox.Show(@"Hotkey failed to unregister!");

        }

        private void StartRecording()
        {
            this.WindowState = FormWindowState.Minimized;
            this.NotifycationIcon.Visible = true;
            this.NotifycationIcon.BalloonTipTitle = @"Bat Recording";
            this.NotifycationIcon.BalloonTipText = @"Gravando \nPara parar Pressione CTRL+ SHIT + P";
            this.NotifycationIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.NotifycationIcon.ShowBalloonTip(2000);
            this.Hide();
            StartCapture(wavOut);
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
