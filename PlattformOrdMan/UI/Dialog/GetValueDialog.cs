using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class GetValueDialog : OrdManForm
    {
        String MyNullValue;
        Boolean MyAllowNull;

        public GetValueDialog(String title, String prompt, String defaultText)
        {
            //The user must enter a value.
            InitializeComponent();

            Text = title;
            PromptLabel.Text = prompt;
            InputTextBox.Text = defaultText;
            InputTextBox.SelectAll();
            MyAllowNull = false;
            SetInputTextPoxPosition();
        }

        public GetValueDialog(String title, String prompt, String defaultText, String nullValue)
        {
            //If the user does not enter a value, the value will be set to the content of nullValue.
            InitializeComponent();

            Text = title;
            PromptLabel.Text = prompt;
            InputTextBox.Text = defaultText;
            InputTextBox.SelectAll();
            MyAllowNull = true;
            MyNullValue = nullValue;
            SetInputTextPoxPosition();
        }

        private void SetInputTextPoxPosition()
        {
            Point newLoc;
            newLoc = new Point(InputTextBox.Location.X, PromptLabel.Location.Y + PromptLabel.Height + 6);
            InputTextBox.Location = newLoc;
        }

        public String GetText()
        {
            if (IsEmpty(InputTextBox.Text))
            {
                return MyNullValue;
            }
            else
            {
                return InputTextBox.Text.Trim();
            }
        }

        private void InputTextBox_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = OKButton;
        }

        private void InputTextBox_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (IsEmpty(InputTextBox.Text) && !MyAllowNull)
            {
                // No information available.
                MessageBox.Show("Please specify a value.",
                               Config.GetDialogTitleStandard(),
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            InputTextBox.Text = "";
        }

    }
}