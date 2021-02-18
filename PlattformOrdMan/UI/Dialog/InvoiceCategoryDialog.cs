using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Data.PostData;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class InvoiceCategoryDialog : OrdManForm
    {
        private Post MyPost;
        public InvoiceCategoryDialog(Post post)
        {
            InitializeComponent();
            MyPost = post;
            Init();
        }

        private void Init()
        {
            invoiceCategoryCombobox1.LoadInvoiceCategory();
            invoiceCategoryCombobox1.SetSelectedInvoiceCategory(PlattformOrdManData.NO_ID);
            OkButton.Enabled = false;
        }

        private String GetInvoiceCategoryCode()
        {
            if (invoiceCategoryCombobox1.SelectedIndex == 0)
            {
                return "";
            }
            else
            {
                return invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetNumber().ToString();
            }
        }

        private void invoiceCategoryCombobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeTextBox.Text = GetInvoiceCategoryCode();
            if (IsNotNull(invoiceCategoryCombobox1.GetSelectedInvoiceCategory()))
            {
                OkButton.Enabled = true;
            }
            else
            {
                OkButton.Enabled = false;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            int invoiceCategoryId;
            if (IsNull(invoiceCategoryCombobox1.GetSelectedInvoiceCategory()))
            {
                MessageBox.Show("Error, no invoice category selected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
            }
            invoiceCategoryId = invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId();
            MyPost.GetMerchandise().SetInvoiceCategoryId(invoiceCategoryId);
            MyPost.GetMerchandise().Set();
            DialogResult = DialogResult.OK;
        }

        private void EditCategoryButton_Click(object sender, EventArgs e)
        {
            int selectedInvoiceCategoryId;
            selectedInvoiceCategoryId = invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId();
            ShowInvoiceCategoriesDialog showInvoiceCategoryDialog;
            showInvoiceCategoryDialog = new ShowInvoiceCategoriesDialog();
            showInvoiceCategoryDialog.ShowDialog();
            invoiceCategoryCombobox1.LoadInvoiceCategory();
            invoiceCategoryCombobox1.SetSelectedInvoiceCategory(selectedInvoiceCategoryId);
        }

    }
}
