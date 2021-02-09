using System;
using System.ComponentModel;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class EnquiryField : UserControl
    {
        [Category("Action")]
        public event EventHandler EnquiryChanged;

        public EnquiryField()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public Enquiry Enquiry
        {
            set => InitEnquiry(value);
            get
            {
                var hasAnswered = NoRadioButton.Checked || YesRadioButton.Checked;
                var hasValue = YesRadioButton.Checked;
                return new Enquiry(hasAnswered, hasValue, ValueTextBox.Text);
            }
        }

        private void InitEnquiry(Enquiry enquiry)
        {
            if (enquiry.HasAnswered)
            {
                YesRadioButton.Checked = enquiry.HasValue;
                NoRadioButton.Checked = !enquiry.HasValue;
                ValueTextBox.Text = enquiry.Value;
            }
        }

        private void NoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                ValueTextBox.Text = "";
                ValueTextBox.ReadOnly = true;
                ValueTextBox.Enabled = false;
            }
            else
            {
                ValueTextBox.ReadOnly = false;
                ValueTextBox.Enabled = true;
                ActiveControl = ValueTextBox;
            }

            EnquiryChanged?.Invoke(sender, e);
        }

        private void YesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            EnquiryChanged?.Invoke(sender, e);
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            EnquiryChanged?.Invoke(sender, e);
        }
    }
}
