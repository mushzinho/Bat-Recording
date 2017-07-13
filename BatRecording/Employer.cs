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
            _employers.Clear();
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

        public bool Autentication(string login, string pass)
        {
            this.GetAllEmployers();
            foreach (var employer in _employers)
            {
                string[] employerSplited = employer.Split(':');
                if (employerSplited[1] == login && employerSplited[2] == pass)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DeleteEmployer(string login)
        {
            this.GetAllEmployers();
            int pos = 0;
            bool deleted = false;
            if (_employers.Count > 0)
            {
                foreach (var employer in _employers)
                {
                    string[] employerSplited = employer.Split(':');

                    if (employerSplited[1] == login)
                    {
                        _employers.RemoveAt(pos);
                        deleted = true;
                        this.SaveEmployerToFile();
                        break;
                    }
                    pos++;
                }
            }

            return deleted;

        }

        public bool ChangePassword(string login, string newPass)
        {
            this.GetAllEmployers();
            bool changed = false;

            if (_employers.Count > 0)
            {
                int count = 0;
                foreach (var employer in _employers)
                {
                    string[] employerSplited = employer.Split(':');
                    if (employerSplited[1] == login)
                    {
                        string newEmployer = employerSplited[0] + ":" + login + ":" + newPass;
                        _employers[count] = newEmployer;
                        changed = true;
                        this.SaveEmployerToFile();
                        break;

                    }
                    count++;
                }
            }

            
            return changed;
        }
        

  
    }
}
