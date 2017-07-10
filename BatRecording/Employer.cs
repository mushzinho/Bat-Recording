using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatRecording
{
    class Employer
    {
        private string name;
        private string login;
        private string pass;

        public Employer(string name, string login, string pass)
        {
            this.name = name;
            this.login = login;
            this.pass = pass;
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
