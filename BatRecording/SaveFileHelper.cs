using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatRecording
{
    class SaveFileHelper
    {

        private string _operatorBaseUrl;
        public string OutFileNameComplete;

        public void ConvertWithFfmpegAndGetUrl(string clientName, string cpfCnpj, string operatorData)
        {


            var todayFormated = DateTime.Now.ToString("d_M_yyyy");
            var finalTime = DateTime.Now.ToString("HH_mm_ss");


            this._operatorBaseUrl = todayFormated + Path.AltDirectorySeparatorChar + operatorData.Split(':')[0] + 
                "_" + operatorData.Split(':')[1];

            this.OutFileNameComplete = this._operatorBaseUrl + Path.AltDirectorySeparatorChar +
                                           clientName + "_" + cpfCnpj + "_" + finalTime;


            var ffmpeg = new NReco.VideoConverter.FFMpegConverter();
            ffmpeg.ConvertMedia("out.wav", "out.mp3", NReco.VideoConverter.Format.mpeg);
            
        }
    }
}
