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
        private GlobalHotKey _ghk1;
        private const string WavOut = "out.wav";
        private string _operatorData;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this._ghk = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.K , this);
            this._ghk1 = new GlobalHotKey(Constants.SHIFT + Constants.CTRL, Keys.P, this);


        }

        public void SetOperator(string employerLogged)
        {
            this._operatorData = employerLogged;
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

                StartRecording();
           
                Contract contract = new Contract();
                if( contract.ShowDialog() == DialogResult.OK)
                {
                    StopRecording();
                    var saveFileHelper = new SaveFileHelper();
                    string data = "Pablo Henrique:Pablo";
                    saveFileHelper.ConvertWithFfmpegAndSave(contract.ClientName, contract.CpfCnpj, data);
                    this.SaveFileToFtpServer("out.mp3", saveFileHelper.OutFileNameComplete);
                    StreamWriter fr = new StreamWriter(File.OpenWrite("contrato.txt"));
                    fr.Write(contract.TextToBeSaved);
                    fr.Close();
                    fr.Dispose();
                    this.SaveFileToFtpServer("contrato.txt", "contrato.txt");
                    //MessageBox.Show(contract.TextToBeSaved);
                     MessageBox.Show("OK");
                }
              
            }
            else
            {
                StopRecording();

            }

        }


        private bool SaveFileToFtpServer(string inputFile, string remoteFilePath)
        {
          
            var manageFtp = new ManageFtp();
            var client = manageFtp.Client;
          //  var url = Path.GetFullPath(sourceFileName);
            var upload = client.UploadFile(inputFile, "/" + @remoteFilePath, FtpExists.NoCheck, true);
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
            if (!_ghk.Unregiser() || !_ghk1.Unregiser())
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
