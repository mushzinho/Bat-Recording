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
        private bool _recording = false;
        private WasapiCapture _soundIn;
        private IWriteable _writer;
        private IWaveSource _waveSource;
        private GlobalHotKey _ghk;
        private const string WavOut = "out.wav";
        private string _operatorData;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._ghk = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.K , this);

        }

        public void SetOperator(string employerLogged)
        {
            this._operatorData = employerLogged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!this._ghk.Register()) {
                throw new Exception("Não foi possivel registrar o atalho de escopo global");
            }
        }

        private void CleanFiles()
        {
            File.Delete("out.mp3");
            File.Delete("out.wav");
            File.Delete("contrato.txt");
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

            StartRecording();

            var contract = new Contract();
            if (contract.ShowDialog() == DialogResult.OK)
            {
                StopRecording();

                Loading loading = new Loading();
                loading.Show(this);

                var saveFileHelper = new SaveFileHelper();
                saveFileHelper.ConvertWithFfmpegAndGetUrl(contract.ClientName, contract.CpfCnpj, _operatorData);

                bool saveAudio = SaveFileToFtpServer("out.mp3", saveFileHelper.OutFileNameComplete + ".mp3");
                var fr = new StreamWriter(File.OpenWrite("contrato.txt"));
                fr.Write(contract.TextToBeSaved);
                fr.Close();
                fr.Dispose();
                bool saveContract = SaveFileToFtpServer("contrato.txt", saveFileHelper.OutFileNameComplete + ".txt");
                

                if (saveAudio && saveContract)
                {
                    MessageBox.Show(@"Dados gravados com sucesso.");
                    this.CleanFiles();
                    loading.Close();
                    loading.Dispose();
                }
                else
                {
                    MessageBox.Show(@"Dados NÃO GRAVADOS no servidor.");
                    loading.Close();
                    loading.Dispose();
                }
                
            }
            else
            {
                StopRecording();
            } 

            contract.Close();
            contract.Dispose();

        }


        private bool SaveFileToFtpServer(string inputFile, string remoteFilePath)
        {
          
            var manageFtp = new ManageFtp();
            var client = manageFtp.Client;
            var upload = client.UploadFile(inputFile, "/" + remoteFilePath, FtpExists.NoCheck, true);
            client.Disconnect();
            return upload;
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


        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopCapture();
            if (!_ghk.Unregiser())
                MessageBox.Show(@"Hotkey failed to unregister!");

        }

        private void StartRecording()
        {
            StartCapture(WavOut);
            ToggleRecordButton();
        }
        private void StopRecording()
        {
            StopCapture();
            ToggleRecordButton();
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
                        Record_Click(null, null);
                        break;

                }
            }
            base.WndProc(ref m);
        }
    }
}
