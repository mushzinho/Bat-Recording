using System;
using System.Collections.Generic;
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

        public ManageFtp(string username, string pass, string server = "127.0.0.1")
        {
             this.Client = new FtpClient(server)    
            {
                Port = 29,
                Credentials = new NetworkCredential(username, pass)
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
