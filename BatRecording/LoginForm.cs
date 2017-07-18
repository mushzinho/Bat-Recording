using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    public partial class LoginForm : Form
    {
        public string AuthenticatedEmployer;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Length > 2 && textBoxSenha.Text.Length > 2)
            {   
                Employer  employer = new Employer();
                bool result = employer.Autentication(textBoxLogin.Text, textBoxSenha.Text);
                if (result)
                {
                    this.AuthenticatedEmployer = employer.AuthenticatedEmployer;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(@"Login ou senha inválidos.");
                }
            }
            else
            {
                MessageBox.Show(@"Login ou senha devem ter mais de 2 dígitos.");
            }
            
        }

        private void buttonCriarConta_Click(object sender, EventArgs e)
        {
            AdminPass adminpass = new AdminPass();
            if (adminpass.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("OK");
            }
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys) e.KeyChar == Keys.Enter)
            {
                this.buttonLogin_Click(this, EventArgs.Empty);
            }
        }
    }
}

