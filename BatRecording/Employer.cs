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


            string readEmployers = reader.ReadToEnd();
            if (readEmployers == "")
            {
                reader.Close();
                client.Disconnect();
                return;
            }
            string[] allEmployers = readEmployers.Split('\n');
            foreach (var employer in allEmployers)
            {   
                if(employer != "") _employers.Add(employer);
            }
            
            reader.Close();
            client.Disconnect();
        }

        private void SaveEmployerToFile()
        {
            var manageFtp = new ManageFtp("pablo", "pablo");
            var client = manageFtp.Client;
            Stream myFileStream = client.OpenWrite(BaseUrlEmployersFile, FtpDataType.Binary);
          
            StreamWriter writer = new StreamWriter(myFileStream);

            if (_employers.Count == 0)
            {
                writer.Close();
                client.GetReply();
                client.Disconnect();
                return;
            }

            foreach (var employer in _employers)
            {
                writer.Write(employer + "\n");
            }
           
            writer.Flush();
            writer.Close();
            client.GetReply();
            client.Disconnect();
            
        }
        public bool CreateEmployer(string name, string login, string pass)
        {
            this.GetAllEmployers();
            if (_employers.Count > 0)
            {
                foreach (var emp in _employers)
                {
                    if (emp.Split(':')[1] == login)
                    {
                        MessageBox.Show(@"Já existe um funcionario com esse login.");
                        return false;
                    }
                }
            }

            this._name = name;
            this._login = login;
            this._pass = pass;

            string employer = _name + ":" + _login + ":" + _pass;
            _employers.Add(employer);
            this.SaveEmployerToFile();

            return true;
        }

        public void DeleteEmployer(int id)
        {
            this.GetAllEmployers();
            _employers.RemoveAt(id);
            this.SaveEmployerToFile();
        }

        public bool ChangePassword(string login, string newPass)
        {

            return true;
        }
        

  
    }
}
