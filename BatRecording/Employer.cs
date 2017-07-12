using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;

namespace BatRecording
{
    class Employer
    {
        private string _name;
        private string _login;
        private string _pass;
        private List<string> _employers = new List<string>();
        private const string BaseUrlEmployersFile = "/employers.txt";

        private void GetAllEmployers()
        {
            var manageFtp = new ManageFtp("pablo", "pablo");
            var client = manageFtp.Client;
            Stream myFileStream = client.OpenRead(BaseUrlEmployersFile, FtpDataType.Binary);
            StreamReader reader = new StreamReader(myFileStream, Encoding.UTF8);

            string[] allEmployers = reader.ReadToEnd().Split('\n');
            foreach (var employer in allEmployers)
            {
                _employers.Add(employer);
            }
            
            reader.Close();
            client.Disconnect();
        }

        private void SaveEmployerToFile(string employer)
        {
            var manageFtp = new ManageFtp("pablo", "pablo");
            var client = manageFtp.Client;
            Stream myFileStream = client.OpenAppend(BaseUrlEmployersFile, FtpDataType.Binary);
          
            StreamWriter writer = new StreamWriter(myFileStream);
           
            writer.WriteLine(employer);
            writer.Flush();
            writer.Close();
            client.GetReply();
            client.Disconnect();
            
        }
        public bool CreateEmployer(string name, string login, string pass)
        {
            this.GetAllEmployers();
            if (_employers.Any(lineEmployer => lineEmployer.Contains(login)))
            {
                MessageBox.Show(@"Já existe um funcionario com esse login.");
                return false;
            }
            //TODO: NAO TA CRIANDO COM MESMO NOME
           
            this._name = name;
            this._login = login;
            this._pass = pass;

            string employer = _name + ":" + _login + ":" + _pass;
            this.SaveEmployerToFile(employer);

            return true;
        }

        public bool DeleteEmployer(string login)
        {

            return true;
        }

        public bool ChangePassword(string login, string newPass)
        {

            return true;
        }
        

  
    }
}
