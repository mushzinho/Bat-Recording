using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    public partial class AdminPass : Form
    {
        public AdminPass()
        {
            InitializeComponent();
        }

        public string CalculateSha1(string text, Encoding enc)
        {
            try
            {
                byte[] buffer = enc.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        private void buttonLogarAdmin_Click(object sender, EventArgs e)
        {

            var sha1 = SHA1.Create();
            string myCryptedValue = this.CalculateSha1(this.textBoxSenhaAdmin.Text, Encoding.UTF8);
            StreamReader reader = File.OpenText("config.txt");
            string configs = reader.ReadToEnd();
            string passHash = configs.Split(':')[1];
          
            if (myCryptedValue == passHash)
            {
                Admin admin = new Admin();
                admin.ShowDialog();
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }
    }
}
