using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;
using Molmed.PlattformOrdMan.UI.Controller;
using Molmed.PlattformOrdMan.UI.Component;
using PlattformOrdMan.UI.View.Base;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowMerchandiseDialog : OrdManForm, ISupplierForm, IMerchandiseForm
    {
        private MerchandiseList MyMerchandiseList;
        private const String PROPERTIES = "Properties";
        private const String DELETE = "Delete";
        private const String MAKE_ORDER = "Make order ...";
        private const String SEARCH_PRODUCTS = "Search products ...";
        private EventHandler MySelectedIndexChangedEventHandler;
        private ToolTipHandler MyToolTipHandler;
        

        public ShowMerchandiseDialog(MerchandiseList merchandiseList)
        {
            InitializeComponent();
            MyMerchandiseList = merchandiseList;
            Init();
        }

        private void Init()
        {
            SupplierList suppliers;
            MerchandiseManager.RefreshCache();
            suppliers = SupplierManager.GetSuppliers();
            SupplierCombobox.Init(suppliers, "supplier", true);
            SupplierCombobox.LoadIdentitiesWithInfoText();
            MySelectedIndexChangedEventHandler = new EventHandler(SupplierCombobox_SelectedIndexChanged);
            SupplierCombobox.SelectedIndexChanged += MySelectedIndexChangedEventHandler;
            SupplierCombobox.KeyPress += new KeyPressEventHandler(SupplierCombobox_KeyPress);
            SupplierCombobox.KeyDown += new KeyEventHandler(SupplierCombobox_KeyDown);
            SupplierCombobox.KeyUp += new KeyEventHandler(SupplierCombobox_KeyUp);
            SearchProductTextBox.Text = SEARCH_PRODUCTS;
            SearchProductTextBox.Enter += new EventHandler(SearchProductTextBox_Enter);
            RestoreSortingButton.Enabled = false;
            ShowOnlyEnabledProductsCheckBox.Checked = PlattformOrdManData.Configuration.ShowOnlyEnabledProducts;
            this.FormClosed += new FormClosedEventHandler(ShowMerchandiseDialog_FormClosed);
            InitListView();        
        }

        public override void ReloadForm()
        {
            SupplierList suppliers;
            MerchandiseManager.RefreshCache();
            MyMerchandiseList = MerchandiseManager.GetMerchandiseFromCache();
            suppliers = SupplierManager.GetSuppliers();
            SupplierCombobox.LoadIdentitiesWithInfoText();
            SearchProductTextBox.Text = SEARCH_PRODUCTS;
            RestoreSortingButton.Enabled = false;
            UpdateListView();
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return IsNotNull(MyMerchandiseList.GetById(merchandiseId));
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            int index;
            MerchandiseListView2.ReloadMerchandise(merchandise);
            index = MyMerchandiseList.GetIndex(merchandise);
            MyMerchandiseList[index] = merchandise;
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        {
            if (IsNotNull(merchandise))
            {
                MyMerchandiseList.Add(merchandise);
                MerchandiseViewItem viewItem = new MerchandiseViewItem(merchandise);
                MerchandiseListView2.BeginAddItems(1);
                MerchandiseListView2.AddItem(viewItem);
                MerchandiseListView2.EndAddItems();
                MerchandiseListView2.Sort();
                MerchandiseListView2.SelectedIndices.Clear();
                MerchandiseListView2.SelectedIndices.Add(viewItem.Index);
                viewItem.EnsureVisible();
                MerchandiseListView2.Select();
            }
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MerchandiseListView2.HasSupplierLoaded(supplierId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MerchandiseListView2.ReloadSupplier(supplier);
        }

        public void AddCreatedSupplier(Supplier supplier)
        { 
            // Do nothing
        }

        private void ShowMerchandiseDialog_FormClosed(object sender, EventArgs e)
        {
            PlattformOrdManData.Configuration.ShowOnlyEnabledProducts = ShowOnlyEnabledProductsCheckBox.Checked;
        }

        void SupplierCombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                SupplierCombobox.SelectedIndexChanged -= MySelectedIndexChangedEventHandler;
            }
        }

        void SupplierCombobox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        void SupplierCombobox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                SupplierCombobox.SelectedIndexChanged += MySelectedIndexChangedEventHandler;
            }
        }

        void SupplierCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        void SearchProductTextBox_Enter(object sender, EventArgs e)
        {
            if (SearchProductTextBox.Text == SEARCH_PRODUCTS)
            {
                SearchProductTextBox.Text = "";
            }
            this.AcceptButton = SearchButton;
        }

        private bool IsWithinFreeTextSearch(Merchandise merchandise)
        {
            String searchStr;
            searchStr = SearchProductTextBox.Text.Trim().ToLower();
            if (merchandise.GetIdentifier().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetAmount() != null &&
                merchandise.GetAmount().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetApprPrizeString().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetCategory() != null &&
                merchandise.GetCategory().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetComment() != null &&
                merchandise.GetComment().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetCurrentArticleNumberString() != null &&
                merchandise.GetCurrentArticleNumberString().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetInvoiceCategory() != null &&
                merchandise.GetInvoiceCategory().GetIdentifier().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetStorage() != null &&
                merchandise.GetStorage().ToLower().Contains(searchStr))
            {
                return true;
            }
            if (merchandise.GetSupplier() != null &&
                merchandise.GetSupplier().GetIdentifier().ToLower().Contains(searchStr))
            {
                return true;
            }
            return false;
        }

        private bool IsWithinSearchCriteria(Merchandise merchandise)
        {
            if (SupplierCombobox.SelectedIndex > 0 && merchandise.GetSupplier() != null &&
                SupplierCombobox.GetSelectedIdentity().GetId() != merchandise.GetSupplier().GetId())
            {
                return false;
            }
            else if (SupplierCombobox.SelectedIndex > 0 && merchandise.GetSupplier() == null)
            {
                return false;
            }
            else if (SearchProductTextBox.Text != SEARCH_PRODUCTS && !IsWithinFreeTextSearch(merchandise))
            {
                return false;
            }
            return true;
        }

        private void FilterProducts()
        {
            MerchandiseList filteredProds = new MerchandiseList();
            MerchandiseList enabledFilteredProds = new MerchandiseList();

            foreach (Merchandise merch in MyMerchandiseList)
            {
                if (!ShowOnlyEnabledProductsCheckBox.Checked || merch.IsEnabled())
                {
                    enabledFilteredProds.Add(merch);
                }
            }

            foreach (Merchandise merch in enabledFilteredProds)
            {
                if (IsWithinSearchCriteria(merch))
                {
                    filteredProds.Add(merch);
                }
            }

            MerchandiseListView2.BeginLoadItems(filteredProds.Count);
            foreach (Merchandise merch in filteredProds)
            {
                MerchandiseListView2.AddItem(new MerchandiseViewItem(merch));
            }
            MerchandiseListView2.EndLoadItems();
        }

        private void InitListView()
        {
            int width;
            width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            MerchandiseListView2.AddColumn("Product", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH, ListDataType.String);
            MerchandiseListView2.AddColumn("Amount", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Enabled", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Supplier", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Invoice Category", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Category", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Article number", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Approximate prize", width, ListDataType.Currency);
            MerchandiseListView2.AddColumn("Storage", width, ListDataType.String);
            MerchandiseListView2.AddColumn("Comment", width, ListDataType.String);
            MerchandiseListView2.ShowItemToolTips = false;
            MerchandiseListView2.ItemMouseHover += MerchandiseListView2_ItemMouseHover;
            UpdateListView();
            AddMenuItem(MerchandiseListView2, MAKE_ORDER, MakeOrderMenuItem_Click);
            AddMenuItem(MerchandiseListView2, DELETE, DeleteMenuItem_Click);
            AddMenuItem(MerchandiseListView2, PROPERTIES, PropertiesMenuItem_Click);
            AddMenuItem2(MerchandiseListView2, "sep", null, true);
            new CopyListViewMenu(MerchandiseListView2);
            MerchandiseListView2.DoubleClick += new EventHandler(PropertiesMenuItem_Click);
            MerchandiseListView2.OnSortOrderSet += new SortOrderSet(MerchandiseListView2_OnSortOrderSet);
        }

        void MerchandiseListView2_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (IsNotEmpty(e.Item.ToolTipText))
            {
                MyToolTipHandler = new ToolTipHandler(this, e.Item.ToolTipText);
                MyToolTipHandler.StartTimer();
            }
        }

        private void MakeOrderMenuItem_Click(object sender, EventArgs e)
        {
            CreatePostDialog createPostDialog;
            Merchandise merchandice;
            MerchandiseViewItem merchandiseViewItem;
            bool isDoubtfulProd = false;
            if (MerchandiseListView2.SelectedItems.Count > 0)
            {
                merchandiseViewItem = ((MerchandiseViewItem)MerchandiseListView2.SelectedItems[0]);
                merchandice = merchandiseViewItem.GetMerchandise();
                if (!merchandice.IsEnabled())
                {
                    MessageBox.Show("The product has to be enabled before proceeding (now disabled), continue?",
                        "Enable status", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
                createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create, merchandice);
                createPostDialog.MdiParent = this.MdiParent;
                createPostDialog.Show();
            }            
        }

        void MerchandiseListView2_OnSortOrderSet(object sender, EventArgs e)
        {
            RestoreSortingButton.Enabled = true;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            MerchandiseList merchandiseList = new MerchandiseList();
            String str;
            try
            {
                str = "Are you sure to delete the " + MerchandiseListView2.SelectedItems.Count + " items?";
                if (MessageBox.Show(str, "Delete merchandise", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                foreach (MerchandiseViewItem mViewItem in MerchandiseListView2.SelectedItems)
                {
                    merchandiseList.Add(mViewItem.GetMerchandise());
                }
                MerchandiseManager.DeleteMerchandise(merchandiseList);
                foreach (Merchandise merchandise in merchandiseList)
                {
                    MyMerchandiseList.Remove(merchandise);
                }
                FilterProducts();
            }
            catch (Exception ex)
            {
                HandleError("Error when deleting products! (It's probable that some of the products is refered by in a post)", ex);
            }
        }

        private Merchandise GetSelectedMerchandise()
        {
            if (MerchandiseListView2.SelectedItems.Count > 0)
            {
                return ((MerchandiseViewItem)MerchandiseListView2.SelectedItems[0]).GetMerchandise();
            }
            else
            {
                return null;
            }
        }

        private void AddNewProductToList(Merchandise merchandise)
        {
            MerchandiseViewItem mViewItem;
            MerchandiseListView2.BeginUpdate();
            mViewItem = new MerchandiseViewItem(merchandise);
            MerchandiseListView2.Items.Add(mViewItem);
            MerchandiseListView2.EndUpdate();
            MerchandiseListView2.SetDefaultSortOrder();
            MerchandiseListView2.EnsureVisible(mViewItem.Index);
            SelectMerchandise(merchandise);
            MyMerchandiseList.Add(merchandise);
        }

        private void editMerchandiseDialog_ProductUpdatedHandler(object sender, EventArgs e)
        {
            // Update listview, select the updated item
            // if a new product is created, rebuild the listview and select item
            Merchandise tmpProduct;
            tmpProduct = (Merchandise)sender;
            if (IsNull(MyMerchandiseList.GetById(tmpProduct.GetId())))
            {
                AddNewProductToList(tmpProduct);
            }
            else
            {
                RefreshListView(tmpProduct);
            }
            this.Select();
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            EditMerchandiseDialog editMerchandiseDialog;
            editMerchandiseDialog = new EditMerchandiseDialog(GetSelectedMerchandise(), UpdateMode.Edit);
            editMerchandiseDialog.MdiParent = this.MdiParent;
            editMerchandiseDialog.Show();
        }

        private void RefreshListView(Merchandise merchandise)
        {
            RefreshListView();
            SelectMerchandise(merchandise);
            MerchandiseListView2.EnsureVisible(MerchandiseListView2.SelectedIndices[0]);
        }

        private void RefreshListView()
        {
            foreach (MerchandiseViewItem mViewItem in MerchandiseListView2.Items)
            {
                mViewItem.UpdateViewItem();
            }
        }

        private void ReBuildListView(Merchandise merchandise)
        {
            MyMerchandiseList = MerchandiseManager.GetMerchandiseFromCache();
            UpdateListView();
            SelectMerchandise(merchandise);
            if (MerchandiseListView2.SelectedIndices.Count > 0)
            {
                MerchandiseListView2.EnsureVisible(MerchandiseListView2.SelectedIndices[0]);
            }
            ResetSearchBoxes();
        }

        private void UpdateListView()
        {
            MerchandiseList filteredMerchandise = new MerchandiseList();
            foreach (Merchandise merchandise in MyMerchandiseList)
            {
                if (!ShowOnlyEnabledProductsCheckBox.Checked || merchandise.IsEnabled())
                {
                    filteredMerchandise.Add(merchandise);
                }
            }

            MerchandiseListView2.BeginLoadItems(filteredMerchandise.Count);
            foreach (Merchandise merchandise in filteredMerchandise)
            {
                MerchandiseListView2.AddItem(new MerchandiseViewItem(merchandise));
            }
            MerchandiseListView2.EndLoadItems();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectMerchandise(Merchandise merchandise)
        {
            System.Windows.Forms.ListView.SelectedIndexCollection selectedIndexCollection;
            selectedIndexCollection = new ListView.SelectedIndexCollection(MerchandiseListView2);
            MerchandiseListView2.SelectedIndices.Clear();
            foreach (MerchandiseViewItem mViewItem in MerchandiseListView2.Items)
            {
                if (mViewItem.GetMerchandise().GetId() == merchandise.GetId())
                {
                    MerchandiseListView2.SelectedIndices.Add(mViewItem.Index);
                    MerchandiseListView2.EnsureVisible(mViewItem.Index);
                }
            }
            MerchandiseListView2.Select();
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            EditMerchandiseDialog editMerchandiseDialog;
            editMerchandiseDialog = new EditMerchandiseDialog(null, UpdateMode.Create);
            editMerchandiseDialog.MdiParent = this.MdiParent;
            editMerchandiseDialog.Show();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            FilterProducts();
        }

        private void ResetSearchBoxes()
        {
            SupplierCombobox.SelectedIndexChanged -= MySelectedIndexChangedEventHandler;
            SupplierCombobox.LoadIdentitiesWithInfoText();
            SearchProductTextBox.Text = SEARCH_PRODUCTS;
            SupplierCombobox.SelectedIndexChanged += MySelectedIndexChangedEventHandler;                
        }

        private void ResetSearch()
        {
            SupplierCombobox.SelectedIndexChanged -= MySelectedIndexChangedEventHandler;
            SupplierCombobox.LoadIdentitiesWithInfoText();
            SearchProductTextBox.Text = SEARCH_PRODUCTS;
            FilterProducts();
            SupplierCombobox.SelectedIndexChanged += MySelectedIndexChangedEventHandler;        
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }

        private void RestoreSortingButton_Click(object sender, EventArgs e)
        {
            MerchandiseListView2.ResetSortOrder();
            FilterProducts();
            RestoreSortingButton.Enabled = false;

        }

        private void ShowOnlyEnabledProductsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListView();
        }
    }
}