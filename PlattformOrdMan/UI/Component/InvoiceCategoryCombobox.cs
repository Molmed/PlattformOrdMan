using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Component
{
    public partial class InvoiceCategoryCombobox : ComboBox
    {
        public InvoiceCategoryCombobox()
        {
            InitializeComponent();
        }

        public InvoiceCategory GetSelectedInvoiceCategory()
        {
            return (InvoiceCategory)(this.SelectedItem);
        }

        public Boolean HasSelectedInvoiceCategory()
        {
            return this.SelectedItem != null;
        }

        public void LoadInvoiceCategory()
        {
            InvoiceCategory noSelectionIC = new InvoiceCategory();
            // Add all InvoiceCategory to combo box.
            noSelectionIC.MakeNoSelectionIC();
            this.BeginUpdate();
            this.Items.Clear();
            this.Items.Add(noSelectionIC);
            foreach (InvoiceCategory invoiceCategory in InvoiceCategoryManager.GetInvoiceCategories())
            {
                this.Items.Add(invoiceCategory);
            }
            this.EndUpdate();
        }


        public void SetSelectedInvoiceCategory(InvoiceCategory invoiceCategory)
        {
            string identifier;
            if (invoiceCategory != null)
            {
                identifier = invoiceCategory.GetIdentifier();
                this.SelectedText = identifier;
            }
            else
            {
                this.SelectedIndex = 0;
            }
        }

        public void SetSelectedInvoiceCategory(Int32 invoiceCategoryId)
        {
            foreach (InvoiceCategory s in this.Items)
            {
                if (s.GetId() == invoiceCategoryId)
                {
                    this.SelectedItem = s;
                    break;
                }
            }
        }
    }
}
