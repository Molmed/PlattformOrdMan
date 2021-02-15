using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan;
using Molmed.PlattformOrdMan.UI.View;
using Molmed.PlattformOrdMan.UI.Controller;
using PlattformOrdMan.UI.View.Base;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowSuppliersDialog : OrdManForm, ISupplierForm
    {
        private SupplierList MySuppliers;
        private const String PROPERTIES = "Properties";
        private const String DELETE = "Delete";
        private const String CONFIRM_ARRIVAL = "Confirm arrival";
        private const String FREE_TEXT_SEARCH = "Free text search ...";

        public ShowSuppliersDialog()
        {
            InitializeComponent();
            Init();
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return IsNotNull(MySuppliers.GetById(supplierId));
        }

        public void ReloadSupplier(Supplier supplier)
        {
            int index;
            SuppliersListView.ReloadSupplier(supplier);
            index = MySuppliers.GetIndex(supplier);
            MySuppliers[index] = supplier;
        }

        private void LoadSuppliers()
        {
            MySuppliers = SupplierManager.GetSuppliers();
        }

        public void AddCreatedSupplier(Supplier supplier)
        {
            if (IsNotNull(supplier))
            {
                ReBuildListView(supplier);
                this.Select();
            }
        }

        private void Init()
        {
            MerchandiseManager.RefreshCache();
            LoadSuppliers();
            if (!UserManager.GetCurrentUser().HasAdministratorRights())
            {
                AddNewButton.Visible = false;
                RestoreSortingButton.Location = AddNewButton.Location;
                RestoreSortingButton.Size = AddNewButton.Size;
            }
            RestoreSortingButton.Enabled = false;
            InitListView();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
            FreeTextSearchTextBox.Enter += new EventHandler(FreeTextSearchTextBox_Enter);
        }

        public override void ReloadForm()
        {
            MerchandiseManager.RefreshCache();
            LoadSuppliers();
            RestoreSortingButton.Enabled = false;
            UpdateListView();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
        }

        void FreeTextSearchTextBox_Enter(object sender, EventArgs e)
        {
            if (FreeTextSearchTextBox.Text == FREE_TEXT_SEARCH)
            {
                FreeTextSearchTextBox.Text = "";
            }
            this.AcceptButton = SearchButton;
        }

        private void FilterSuppliers_old()
        {
            SuppliersListView.Items.Clear();
            SuppliersListView.BeginUpdate();
            foreach (Supplier supplier in MySuppliers)
            {
                if (IsWithinSearchCriteria(supplier))
                {
                    SuppliersListView.Items.Add(new SupplierViewItem(supplier));
                }
            }
            SuppliersListView.EndUpdate();
        }

        private void FilterSuppliers()
        {
            SupplierList filteredSuppliers = new SupplierList();

            foreach (Supplier supplier in MySuppliers)
            {
                if (IsWithinSearchCriteria(supplier))
                {
                    filteredSuppliers.Add(supplier);
                }
            }

            SuppliersListView.BeginLoadItems(filteredSuppliers.Count);
            foreach (Supplier supplier in filteredSuppliers)
            {
                SuppliersListView.AddItem(new SupplierViewItem(supplier));
            }
            SuppliersListView.EndLoadItems();
        }

        private bool IsWithinSearchCriteria(Supplier supplier)
        {
            String searchStr;
            if (FreeTextSearchTextBox.Text != FREE_TEXT_SEARCH)
            {
                searchStr = FreeTextSearchTextBox.Text.Trim();
                if (supplier.GetIdentifier().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (supplier.GetShortName() != null &&
                    supplier.GetShortName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (supplier.GetComment() != null &&
                    supplier.GetComment().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (supplier.GetContractTerminate() != null &&
                    supplier.GetContractTerminate().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (supplier.HasMatchInCustomerNumber(searchStr))
                {
                    return true;
                }
                if (supplier.GetTelNr() != null &&
                    supplier.GetTelNr().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            EditSupplierDialog editSupplierDialog;
            Supplier dummy = null;
            try
            {
                editSupplierDialog = new EditSupplierDialog(dummy, EditSupplierDialog.UpdateMode.Create);
                editSupplierDialog.MdiParent = this.MdiParent;
                editSupplierDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }

        }

        private void SelectSupplier(Supplier supplier)
        {
            foreach (SupplierViewItem sViewItem in SuppliersListView.Items)
            {
                if (sViewItem.GetSupplierId() == supplier.GetId())
                {
                    SuppliersListView.SelectedIndices.Clear();
                    SuppliersListView.SelectedIndices.Add(sViewItem.Index);
                    SuppliersListView.EnsureVisible(sViewItem.Index);
                    break;
                }
            }
            SuppliersListView.Select();
        }


        private void InitListView()
        {
            int width;
            width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            SuppliersListView.AddColumn("Name", width, ListDataType.String);
            SuppliersListView.AddColumn("Enabled", width, ListDataType.String);
            SuppliersListView.AddColumn("Short name", width, ListDataType.String);
            SuppliersListView.AddColumn("Tel nr/fax", width, ListDataType.String);
            SuppliersListView.AddColumn("Contract term.", width, ListDataType.String);
            SuppliersListView.AddColumn("Comment", width, ListDataType.String);

            AddMenuItem(SuppliersListView, DELETE, DeleteMenuItem_Click);
            AddMenuItem(SuppliersListView, PROPERTIES, PropertiesMenuItem_Click);
            AddMenuItem2(SuppliersListView, "sep", null, true);
            new CopyListViewMenu(SuppliersListView);
            SuppliersListView.DoubleClick += new EventHandler(PropertiesMenuItem_Click);
            UpdateListView();
            SuppliersListView.OnSortOrderSet += new SortOrderSet(SuppliersListView_OnSortOrderSet);
        }

        void SuppliersListView_OnSortOrderSet(object sender, EventArgs e)
        {
            RestoreSortingButton.Enabled = true;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            SupplierList suppliers = new SupplierList();
            String str;
            try
            {
                str = "Are you sure to delete the " + SuppliersListView.SelectedItems.Count +" items?";
                if (MessageBox.Show(str, "Delete suppliers", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                foreach (SupplierViewItem supplierViewItem in SuppliersListView.SelectedItems)
                {
                    suppliers.Add(supplierViewItem.GetSupplier());
                }
                SupplierManager.DeleteSuppliers(suppliers);
                foreach (Supplier supplier in suppliers)
                {
                    MySuppliers.Remove(supplier);
                }
                FilterSuppliers();
            }
            catch(Exception ex)
            {
                HandleError("Error when deleting suppliers! (Some of them are probably refered by from a product or post)", ex);
            }
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            Supplier supplier;
            EditSupplierDialog editSupplierDialog;
            try
            {
                supplier = GetSelectedSupplier();
                if (IsNotNull(supplier))
                {
                    editSupplierDialog = new EditSupplierDialog(supplier, EditSupplierDialog.UpdateMode.Edit);
                    editSupplierDialog.MdiParent = this.MdiParent;
                    editSupplierDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }

        }

        private void ReBuildListView(Supplier supplier)
        {
            MySuppliers = SupplierManager.GetSuppliers();
            UpdateListView();
            SelectSupplier(supplier);
            if (SuppliersListView.SelectedIndices.Count > 0)
            {
                SuppliersListView.EnsureVisible(SuppliersListView.SelectedIndices[0]);
            }
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
        }

        private void RefreshListView(Supplier supplier)
        {
            RefreshListView();
            SelectSupplier(supplier);
            if (SuppliersListView.SelectedIndices.Count > 0)
            {
                SuppliersListView.EnsureVisible(SuppliersListView.SelectedIndices[0]);
            }
        }

        private void RefreshListView()
        { 
            foreach(SupplierViewItem supplierViewItem in SuppliersListView.Items)
            {
                supplierViewItem.UpdateViewItem();
            }
        }

        private Supplier GetSelectedSupplier()
        {
            if (SuppliersListView.SelectedItems.Count > 0)
            {
                return ((SupplierViewItem)SuppliersListView.SelectedItems[0]).GetSupplier();
            }
            else
            {
                return null;
            }
        }

        private void UpdateListView()
        {
            SuppliersListView.BeginLoadItems(MySuppliers.Count);
            foreach (Supplier supplier in MySuppliers)
            {
                SuppliersListView.AddItem(new SupplierViewItem(supplier));
            }
            SuppliersListView.EndLoadItems();
        }

        private void RestoreSortingButton_Click(object sender, EventArgs e)
        {
            SuppliersListView.ResetSortOrder();
            FilterSuppliers();
            RestoreSortingButton.Enabled = false;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            FilterSuppliers();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
            FilterSuppliers(); 
        }
    }
}