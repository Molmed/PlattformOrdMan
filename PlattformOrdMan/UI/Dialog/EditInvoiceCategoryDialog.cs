using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class EditInvoiceCategoryDialog : OrdManForm
    {
        UpdateMode MyUpdateMode;
        InvoiceCategory MyInvoiceCategory;
        public EditInvoiceCategoryDialog(InvoiceCategory invoiceCategory, UpdateMode updateMode)
        {
            InitializeComponent();
            MyUpdateMode = updateMode;
            MyInvoiceCategory = invoiceCategory;
            Init();
        }

        private void Init()
        {
            SaveButton.Enabled = false;
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    InitCreateMode();
                    break;
                case UpdateMode.Edit:
                    InitEditMode();
                    break;
            }
        }

        private void InitCreateMode()
        {
            this.Text = "Create Invoice Category";
            SaveButton.Text = "Create";
        }

        private void InitEditMode()
        {
            NameTextBox.Text = MyInvoiceCategory.GetIdentifier();
            CodeTextBox.Text = MyInvoiceCategory.GetNumber().ToString();
        }

        private bool IsUpdated()
        {
            if (MyUpdateMode == UpdateMode.Edit && IsNotNull(MyInvoiceCategory))
            {
                return (MyInvoiceCategory.GetIdentifier() != NameTextBox.Text.Trim() ||
                    MyInvoiceCategory.GetNumber().ToString() != CodeTextBox.Text.Trim());
            }
            else if (MyUpdateMode == UpdateMode.Create)
            {
                return (IsNotEmpty(NameTextBox.Text) && IsNotEmpty(CodeTextBox.Text));
            }
            return false;
        }

        private bool CreateInvoiceCategory()
        { 
            int testCode;
            if (!int.TryParse(CodeTextBox.Text.Trim(), out testCode))
            {
                ShowWarning("Error, the code must be a number, create canceled!");
                return false;
            }
            MyInvoiceCategory = InvoiceCategoryManager.CreateInvoiceCategory(NameTextBox.Text.Trim(), testCode);
            return true;
        }

        private bool UpdateInvoiceCategory()
        { 
            int testCode;
            if (!int.TryParse(CodeTextBox.Text.Trim(), out testCode))
            {
                ShowWarning("Error, the code must be a number, create canceled!");
                return false;
            }
            PlattformOrdManData.Database.UpdateInvoiceCategory(MyInvoiceCategory.GetId(), NameTextBox.Text.Trim(), testCode);
            MyInvoiceCategory.SetIdentifier(NameTextBox.Text.Trim());
            MyInvoiceCategory.SetCode(testCode);
            return true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public InvoiceCategory GetInvoiceCategory()
        {
            return MyInvoiceCategory;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveButton.Enabled = IsUpdated();
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveButton.Enabled = IsUpdated();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool updateOK = false;
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    updateOK = CreateInvoiceCategory();
                    break;
                case UpdateMode.Edit:
                    updateOK = UpdateInvoiceCategory();
                    break;
            }
            if (updateOK)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}