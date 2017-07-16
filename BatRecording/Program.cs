using BatRecording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatRecording
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Contract());
          
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.SetOperator(loginForm.AuthenticatedEmployer);
                Application.Run(mainWindow);
            }
            else
            {
                Application.Exit();
            }
       
        }
    }
}
