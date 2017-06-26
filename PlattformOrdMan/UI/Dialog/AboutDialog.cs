using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            string version;
            version = Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
                      Assembly.GetExecutingAssembly().GetName().Version.Minor + "." +
                      Assembly.GetExecutingAssembly().GetName().Version.Build + "." +
                      Assembly.GetExecutingAssembly().GetName().Version.Revision;
            lApplicationVersion.Text = version;
            lCurrentUser.Text = UserManager.GetCurrentUser().GetName();
        }
    }
}