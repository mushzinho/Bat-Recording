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
    public partial class Admin : Form
    {
        private Employer _employer;

        public Admin()
        {
            InitializeComponent();
            _employer = new Employer();
        }

        private void buttonCriarConta_Click(object sender, EventArgs e)
        {
            bool createEmployer = _employer.CreateEmployer(this.textBoxNomeCriar.Text, this.textBoxLoginCriar.Text,
                this.textBoxSenhaCriar.Text);
            if (createEmployer) MessageBox.Show(@"Cadastro Criado.");

        }

        private void buttonMudarSenha_Click(object sender, EventArgs e)
        {
            bool updatePassEmployer =
                _employer.ChangePassword(this.textBoxLoginMudar.Text, this.textBoxNovaSenha.Text);
            if (updatePassEmployer) MessageBox.Show(@"Senha Alterada.");
        }

        private void buttonDeletarConta_Click(object sender, EventArgs e)
        {
            bool deleteEmployer =
                _employer.DeleteEmployer(this.textBoxDeletarConta.Text);
            if (deleteEmployer) MessageBox.Show(@"Deletado.");
        }
    }
}
