﻿using BatRecording;
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

            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.setOperatorName(loginForm.UserLogged);
                Application.Run(mainWindow);
            }
            else
            {
                Application.Exit();
            }
            
        }
    }
}
