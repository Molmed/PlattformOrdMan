using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class SignInvoiceDialog : OrdManForm
    {
        private Merchandise MyMerchandise;
        public SignInvoiceDialog(Merchandise merchandise)
        {
            InitializeComponent();
            MyMerchandise = merchandise;
            Init();
        }

        private void Init()
        {
            InvoiceCategoryLabel.Text = GetInvoiceCategoryName();
            InvoiceCategoryCodeLabel.Text = GetInvoiceCategoryCodeString();
        }

        private String GetInvoiceCategoryCodeString()
        {
            if (IsNull(MyMerchandise.GetInvoiceCategory()))
            {
                return "";
            }
            return MyMerchandise.GetInvoiceCategory().GetNumber().ToString();
        }

        private String GetInvoiceCategoryName()
        {
            if (IsNull(MyMerchandise.GetInvoiceCategory()))
            {
                return "Not given";
            }
            return MyMerchandise.GetInvoiceCategoryName();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditMerchandiseButton_Click(object sender, EventArgs e)
        {
            EditMerchandiseDialog editMerchandiseDialog;
            editMerchandiseDialog = new EditMerchandiseDialog(MyMerchandise, UpdateMode.Edit);
            if (editMerchandiseDialog.ShowDialog() == DialogResult.OK)
            {
                MyMerchandise = editMerchandiseDialog.GetMerchandise();
                Init();
            }
        }

        public bool IsInvoiceOkAndSent()
        {
            return InvoiceOKAndSentRadioButton.Checked || NoInvoiceRadioButton.Checked;
        }

        public bool IsInvoiceAbsent()
        {
            return NoInvoiceRadioButton.Checked;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}