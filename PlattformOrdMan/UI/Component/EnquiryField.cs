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
        private Enquiry _enquiry;

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
            set
            {
                _enquiry = value;
                InitEnquiry();
            }
        }

        private void InitEnquiry()
        {
            if (_enquiry.HasAnswered)
            {
                YesRadioButton.Checked = _enquiry.HasValue;
                ValueTextBox.Text = _enquiry.Value;
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
