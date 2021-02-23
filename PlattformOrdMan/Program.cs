using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlattformOrdMan.UI;


namespace Molmed.PlattformOrdMan
{
    static class Program
    {
        private const Boolean MyVersionControl = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            MainForm mainForm;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Init main.
                mainForm = new MainForm();
                if (mainForm.Login(MyVersionControl))
                {
                    Application.Run(mainForm);
                }
            }
            catch (Exception exception)
            {
                MainForm.HandleError("General error", exception);
            }
            finally
            {
                MainForm.LogoutDatabase();
            }
        }
    }
}