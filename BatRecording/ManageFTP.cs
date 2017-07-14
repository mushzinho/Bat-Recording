using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using FluentFTP;

namespace BatRecording
{
    class ManageFtp

    {
        public FtpClient Client { get; }

        public ManageFtp()
        {
            StreamReader reader = File.OpenText("config.txt");
            string[] serverConfigs = reader.ReadToEnd().Split(':')[0].Split('/');
            string login = serverConfigs[0];
            string senha = serverConfigs[1];
            string server = serverConfigs[2];

             this.Client = new FtpClient(server)    
            {
                Port = 29,
                Credentials = new NetworkCredential(login, senha)
            };
            try
            {
                this.Client.Connect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        
    }
}
