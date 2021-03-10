using System;
using System.Windows.Forms;
using PlattformOrdMan.Data.PostData;
using DataException = PlattformOrdMan.Data.Exception.DataException;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class SetInvoiceNumberDialog : OrdManForm
    {
        private PostList MyPosts;
        public SetInvoiceNumberDialog(PostList posts)
        {
            InitializeComponent();
            MyPosts = posts;
            InitCustomerNumberCombobox();
        }

        private void InitCustomerNumberCombobox()
        {
            int supplierId;
            if(IsEmpty(MyPosts))
            {
                throw new DataException("Empty post list");
            }
            supplierId = MyPosts[0].GetSupplierId();

            foreach (Post post in MyPosts)
            {
                if (supplierId != post.GetSupplierId())
                {
                    throw new DataException("Posts with different suppliers selected");
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void InvoiceInstCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                NoInvoiceCheckBox.Checked = false;
                NoInvoiceCheckBox.Enabled = false;
            }
            else
            {
                NoInvoiceCheckBox.Enabled = true;
            }
        }

        private void InvoiceClinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                NoInvoiceCheckBox.Checked = false;
                NoInvoiceCheckBox.Enabled = false;
            }
            else
            {
                NoInvoiceCheckBox.Enabled = true;
            }
        }

        private void NoInvoiceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                InvoiceNumberTextBox.Text = "";
                InvoiceNumberTextBox.Enabled = false;
            }
            else
            {
                InvoiceNumberTextBox.Enabled = true;
            }

        }

        public bool NoInvoice
        {
            get
            {
                return NoInvoiceCheckBox.Checked;
            }
        }

        public string InvoiceNumber
        {
            get
            {
                return InvoiceNumberTextBox.Text.Trim();
            }
        }
    }
}
