using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class DemandAnswerField : UserControl
    {
        public DemandAnswerField()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
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
