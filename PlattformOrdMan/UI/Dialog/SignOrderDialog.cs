using System.Drawing;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class SignOrderDialog : Form
    {
        public SignOrderDialog()
        {
            InitializeComponent();
        }

        public Enquiry Account => accountField1.GetEnquiry();

        public Enquiry Periodization => periodizationField1.GetEnquiry();

        private void OkButton_Click(object sender, System.EventArgs e)
        {
            bool failed = false;
            if (!Account.HasAnswered)
            {
                accountField1.SetMarkColor(Color.Red);
                failed = true;
            }

            if (!Periodization.HasAnswered)
            {
                periodizationField1.SetMarkColor(Color.Red);
                failed = true;
            }

            if (failed)
                return;
            DialogResult = DialogResult.OK;
        }
    }
}
