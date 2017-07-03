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

namespace BatRecording
{
    public partial class SaveAudioDialog : Form
    {
        private string baseURL  = "";
        private string operatorBaseUrl;
        private string operatorName = "pablo";
        public SaveAudioDialog()
        {
            InitializeComponent();

            var todayFormated = DateTime.Now.ToString("d_M_yyyy");

            if (!Directory.Exists(baseURL + todayFormated))
            {
                Directory.CreateDirectory(baseURL + todayFormated);
            }

             this.operatorBaseUrl = baseURL + todayFormated + Path.AltDirectorySeparatorChar + this.operatorName;

            if (!Directory.Exists(operatorBaseUrl))
            {
                Directory.CreateDirectory(operatorBaseUrl);
            }
        }


        private void Finalize_Click(object sender, EventArgs e)
        {
            bool allValid = true;
            if (! (TextBoxClienteName.Text.Length > 5) )
            {
                allValid = false;
                MessageBox.Show("Digite o nome do cliente completo.");
            }

            if (TextBoxCPF.Text.Length != 11 )
            {
                allValid = false;
                MessageBox.Show("Digite o CPF do cliente sem números.");
            }
            if (allValid)
            {
                var clienteName = TextBoxClienteName.Text;
                var clienteCpf = TextBoxCPF.Text;

                var finalTime = DateTime.Now.ToString("HHmmss");

                var outFileName = clienteName + "_" +clienteCpf + "_" + finalTime + ".mp3";
                ConvertWithFfmpeg("out.wav", this.operatorBaseUrl + Path.AltDirectorySeparatorChar + outFileName);
                MessageBox.Show("Gravação salva com sucesso.");
                this.DialogResult = DialogResult.OK;
            }
        }
        public void ConvertWithFfmpeg(string inputFile, string outputFileName)
        {
            var ffmpeg = new NReco.VideoConverter.FFMpegConverter();
            ffmpeg.ConvertMedia(inputFile, outputFileName, NReco.VideoConverter.Format.mpeg);
        }

    }
}
