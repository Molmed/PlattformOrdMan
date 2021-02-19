using System.Reflection;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog
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