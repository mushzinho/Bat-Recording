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
    public partial class AdminPass : Form
    {
        public AdminPass()
        {
            InitializeComponent();
        }

        private void buttonLogarAdmin_Click(object sender, EventArgs e)
        {
            if (this.textBoxSenhaAdmin.Text == "pablo")
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
