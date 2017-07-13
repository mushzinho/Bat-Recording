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
        public string UserLogged;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Length > 2 && textBoxSenha.Text.Length > 2)
            {
                bool result = new Employer().Autentication(textBoxLogin.Text, textBoxSenha.Text);
                if (result)
                {
                    this.UserLogged = textBoxLogin.Text;
                    MessageBox.Show(@"Logado com sucesso");
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
    }
}

