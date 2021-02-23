using System;
using System.ComponentModel;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class ShowInvoiceCategoriesDialog : OrdManForm
    {
        InvoiceCategoryList MyInvoiceCategories;
        private const String DELETE = "Delete";
        private const String PROPERTIES = "Properties ...";
        public ShowInvoiceCategoriesDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MyInvoiceCategories = InvoiceCategoryManager.GetInvoiceCategories();
            InitListView();
        }

        private void DeleteInvoiceCategoryMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceCategoryList invoiceCategories = new InvoiceCategoryList();
            String str;
            str = "Are you sure to delete the " + InvoiceCategoriesListView.SelectedItems.Count + " items?";
            if (MessageBox.Show(str, "Delete invoice categories", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            foreach (InvoiceCategoryViewItem icViewItem in InvoiceCategoriesListView.SelectedItems)
            {
                invoiceCategories.Add(icViewItem.GetInvoiceCategory());
            }
            if (InvoiceCategoryManager.DeleteInvoiceCategories(invoiceCategories))
            {
                foreach (InvoiceCategory ic in invoiceCategories)
                {
                    MyInvoiceCategories.Remove(ic);
                }
                UpdateListView();
            }        
        }

        private void InitListView()
        {
            InvoiceCategoriesListView.Columns.Add("Invoice category", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            InvoiceCategoriesListView.Columns.Add("Code", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            UpdateListView();
            AddMenuItem(InvoiceCategoriesListView, DELETE, DeleteInvoiceCategoryMenuItem_Click);
            AddMenuItem(InvoiceCategoriesListView, PROPERTIES, PropertiesMenuItem_Click);
            InvoiceCategoriesListView.DoubleClick += new EventHandler(PropertiesMenuItem_Click);
            InvoiceCategoriesListView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
            
        }

        private InvoiceCategory GetSelectedInvoiceCategory()
        {
            if (InvoiceCategoriesListView.SelectedIndices.Count <= 0)
            {
                return null;
            }
            return ((InvoiceCategoryViewItem)InvoiceCategoriesListView.SelectedItems[0]).GetInvoiceCategory();
        }

        private void ContextMenuStrip_Opening(object sender, EventArgs e)
        {
            if (IsNull(GetSelectedInvoiceCategory()))
            {
                SetVisible(sender, PROPERTIES, false);
            }
            else
            {
                SetVisible(sender, PROPERTIES, true);                
            }
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInvoiceCategory();
        }

        private void UpdateInvoiceCategory()
        {
            EditInvoiceCategoryDialog editInvoiceCategoryDialog;
            editInvoiceCategoryDialog = new EditInvoiceCategoryDialog(GetSelectedInvoiceCategory(), UpdateMode.Edit);
            editInvoiceCategoryDialog.ShowDialog();
            RefreshListView();
        }

        private void UpdateListView()
        {
            InvoiceCategoriesListView.Items.Clear();
            InvoiceCategoriesListView.BeginUpdate();
            foreach (InvoiceCategory inCat in MyInvoiceCategories)
            {
                InvoiceCategoriesListView.Items.Add(new InvoiceCategoryViewItem(inCat));
            }
            InvoiceCategoriesListView.EndUpdate();
        }

        private void RefreshListView()
        {
            foreach (InvoiceCategoryViewItem icViewItem in InvoiceCategoriesListView.Items)
            {
                icViewItem.Update();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private class InvoiceCategoryViewItem : ListViewItem
        {
            private enum ListIndex : int
            { 
                identifier = 0,
                code = 1
            }
            private InvoiceCategory MyInvoiceCategory;
            public InvoiceCategoryViewItem(InvoiceCategory invoiceCategory)
                : base(invoiceCategory.GetIdentifier())
            {
                MyInvoiceCategory = invoiceCategory;
                this.SubItems.Add(invoiceCategory.GetNumber().ToString());
            }

            public InvoiceCategory GetInvoiceCategory()
            {
                return MyInvoiceCategory;
            }

            public void Update()
            { 
                this.SubItems[(int)ListIndex.identifier].Text = MyInvoiceCategory.GetIdentifier();
                this.SubItems[(int)ListIndex.code].Text = MyInvoiceCategory.GetNumber().ToString();
            }
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            EditInvoiceCategoryDialog createInvoiceCategoryDialog;
            createInvoiceCategoryDialog = new EditInvoiceCategoryDialog(null, UpdateMode.Create);
            if (createInvoiceCategoryDialog.ShowDialog() == DialogResult.OK)
            {
                MyInvoiceCategories.Add(createInvoiceCategoryDialog.GetInvoiceCategory());
                UpdateListView();
            }
        }
    }
}