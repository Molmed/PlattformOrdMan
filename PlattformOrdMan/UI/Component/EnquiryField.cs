using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class EnquiryField : UserControl
    {

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
        }
    }
}
