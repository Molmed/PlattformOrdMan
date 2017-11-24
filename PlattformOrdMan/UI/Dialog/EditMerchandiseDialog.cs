using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class EditMerchandiseDialog : OrdManForm, ISupplierForm, IMerchandiseForm
    {
        private Merchandise MyMerchandise;
        private UpdateMode MyUpdateMode;
        public EditMerchandiseDialog(Merchandise merchandise, UpdateMode updateMode)
        {
            SupplierList suppliers;
            InitializeComponent();
            MyMerchandise = merchandise;
            MyUpdateMode = updateMode;
            suppliers = SupplierManager.GetSuppliers();
            SupplierCombobox.Init(suppliers, "supplier", true);
            SupplierCombobox.LoadIdentitiesWithInfoText();
            CurrencyCombobox.LoadCurrencies();
            SaveButton.Enabled = false;
            invoiceCategoryCombobox1.LoadInvoiceCategory();
            SupplierCombobox.OnMyControlledSelectedIndexChanged += 
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(SupplierCombobox_OnMyControlledSelectedIndexChanged);
            CurrencyCombobox.SelectedIndexChanged += new EventHandler(CurrencyCombobox_SelectedIndexChanged);
            ArticleNumberComboBox.TextChanged += new EventHandler(ArticleNumberComboBox_TextChanged);

            switch (updateMode)
            { 
                case UpdateMode.Create:
                    InitCreateMode();
                    break;
                case UpdateMode.Edit:
                    InitEditMode();
                    break;
            }
        }

        public override void ReloadForm()
        {
            SupplierList suppliers;
            suppliers = SupplierManager.GetActiveSuppliersOnly();
            SupplierCombobox.ReloadIdentities(suppliers);
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return MyMerchandise.GetId() == merchandiseId;
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            if (merchandise != null && HasMerchandiseLoaded(merchandise.GetId()))
            {
                MyMerchandise = merchandise;
                UpdateForm();
            }
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        { 
            // Do nothing
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return SupplierCombobox.HasSupplierLoaded(supplierId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            SupplierCombobox.ReloadSupplier(supplier);
        }

        public void AddCreatedSupplier(Supplier supplier)
        {
            SupplierCombobox.AddCreatedSupplier(supplier);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitCreateMode()
        {
            SaveButton.Text = "Create";
            this.Text = "Create product";
            DissableCheckBox.Visible = false;
            invoiceCategoryCombobox1.SetSelectedInvoiceCategory(PlattformOrdManData.NO_ID);
            CurrencyCombobox.SetSelectedCurrency(Data.CurrencyManager.GetDefaultCurrency());
        }

        private void CurrencyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String str = PrizeTextBox.Text.Trim();
            PrizeTextBox.Text = 
                CurrencyCombobox.GetSelectedCurrency().GetPriceWithCurrencyString(str);
            HandleSaveButtonEnabled();
        }

        private void SupplierCombobox_OnMyControlledSelectedIndexChanged()
        {
            if (SupplierCombobox.HasSelectedIdentity())
            {
                ShowSupplierButton.Enabled = true;
            }
            else
            {
                ShowSupplierButton.Enabled = false;
            }
            HandleSaveButtonEnabled();
        }

        private void HandleSaveButtonEnabled()
        {
            if (MyUpdateMode == UpdateMode.Create)
            {
                SaveButton.Enabled = IsReadyToCreate();
            }
            else if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void InitEditMode()
        {
            InitArticleNumberCombobox();
            SaveButton.Enabled = false;
            UpdateForm();
            ArticleNumberComboBox.Select(0, 0);
            this.Select();
        }

        private void UpdateForm()
        {
            NameTextBox.Text = MyMerchandise.GetIdentifier();
            AmountTextBox.Text = MyMerchandise.GetAmount();
            PrizeTextBox.Text = MyMerchandise.GetApprPrizeString();
            CategoryTextBox.Text = MyMerchandise.GetCategory();
            StorageTextBox.Text = MyMerchandise.GetStorage();
            CommentTextBox.Text = MyMerchandise.GetComment();
            if (MyMerchandise.GetSupplierId() != PlattformOrdManData.NO_ID)
            {
                SupplierCombobox.SetSelectedIdentity(MyMerchandise.GetSupplierId());
            }
            if (IsNotNull(MyMerchandise.GetCurrentArticleNumber()))
            {
                ArticleNumberComboBox.Text = MyMerchandise.GetCurrentArticleNumberString();
            }
            DissableCheckBox.Checked = !MyMerchandise.IsEnabled();
            if (MyMerchandise.GetCurrencyId() != PlattformOrdManData.NO_ID)
            {
                CurrencyCombobox.SetSelectedCurrency(MyMerchandise.GetCurrency());
            }
            else
            {
                CurrencyCombobox.SetSelectedCurrency(Data.CurrencyManager.GetDefaultCurrency());
            }
            if (MyMerchandise.GetInvoiceCagegoryId() != PlattformOrdManData.NO_ID)
            {
                invoiceCategoryCombobox1.SetSelectedInvoiceCategory(MyMerchandise.GetInvoiceCagegoryId());
            }
            else
            {
                invoiceCategoryCombobox1.SetSelectedInvoiceCategory(PlattformOrdManData.NO_ID);
            }
        }

        private void InitArticleNumberCombobox()
        { 
            if(IsNotNull(MyMerchandise))
            {
                foreach (ArticleNumber ar in MyMerchandise.GetArticleNumbers())
                {
                    ArticleNumberComboBox.Items.Add(ar);
                }
            }
        }

        public Merchandise GetMerchandise()
        {
            return MyMerchandise;
        }

        private bool IsUpdated()
        {
            int supplierId;
            int invoiceCategoryId = PlattformOrdManData.NO_ID;
            int currencyId = PlattformOrdManData.NO_ID;
            if(IsNotNull(CurrencyCombobox.GetSelectedCurrency()))
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }
            if (IsNull(MyMerchandise))
            {
                return false;
            }
            if (IsNotNull(invoiceCategoryCombobox1.GetSelectedInvoiceCategory()))
            {
                invoiceCategoryId = invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId();
            }
            if (IsNull(SupplierCombobox.GetSelectedIdentity()))
            {
                supplierId = PlattformOrdManData.NO_ID;
            }
            else
            {
                supplierId = SupplierCombobox.GetSelectedIdentity().GetId();
            }
            return (AreNotEqual(MyMerchandise.GetIdentifier(), NameTextBox.Text) ||
                    AreNotEqual(MyMerchandise.GetAmount(), AmountTextBox.Text) ||
                    MyMerchandise.GetSupplierId() != supplierId ||
                    AreNotEqual(MyMerchandise.GetCategory(), CategoryTextBox.Text) ||
                    AreNotEqual(MyMerchandise.GetApprPrizeString(), PrizeTextBox.Text) ||
                    AreNotEqual(MyMerchandise.GetStorage(), StorageTextBox.Text) ||
                    AreNotEqual(MyMerchandise.GetComment(), CommentTextBox.Text) ||
                    AreNotEqual(MyMerchandise.GetCurrentArticleNumberString(), ArticleNumberComboBox.Text.Trim()) ||
                    MyMerchandise.IsEnabled() == DissableCheckBox.Checked ||
                    MyMerchandise.GetInvoiceCagegoryId() != invoiceCategoryId ||
                    MyMerchandise.GetCurrencyId() != currencyId);
        }

        private bool IsReadyToCreate()
        { 
            String name;
            name = NameTextBox.Text.Trim();
            return (name.Length > 0);
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }
        }

        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void ArticleNumberComboBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }        
        }

        private void ArticleNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PrizeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void CategoryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void StorageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void DissableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private bool CreateMerchandise()
        { 
            String identifier, amount, storage, comment, articleNumber, category, str;
            int supplierId, invoiceCategoryId, currencyId;
            decimal prize;
            try
            {
                invoiceCategoryId = invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId();
                if (IsNull(SupplierCombobox.GetSelectedIdentity()) ||
                    SupplierCombobox.GetSelectedIdentity().GetId() == PlattformOrdManData.NO_ID)
                {
                    str = "This product have no selected supplier, proceed anyway?";
                    if (MessageBox.Show(str, "No supplier selected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return false;
                    }
                }
                if (SupplierCombobox.HasSelectedIdentity())
                {
                    supplierId = SupplierCombobox.GetSelectedIdentity().GetId();
                }
                else
                {
                    supplierId = PlattformOrdManData.NO_ID;
                }
                identifier = NameTextBox.Text.Trim();
                amount = AmountTextBox.Text.Trim();
                storage = StorageTextBox.Text.Trim();
                comment = CommentTextBox.Text.Trim();
                articleNumber = ArticleNumberComboBox.Text.Trim();
                category = CategoryTextBox.Text.Trim();
                currencyId = PlattformOrdManData.NO_ID;
                if (CurrencyCombobox.HasSelectedCurrency())
                {
                    currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
                }
                if (!GetPrizeDecimal(GetPrizeString(PrizeTextBox.Text), out prize))
                {
                    ShowWarning("Error, the prize could not be converted to a number, create canceled!");
                    return false;
                }
                MyMerchandise = MerchandiseManager.CreateMerchandise(identifier, supplierId, amount,
                    prize, storage, comment, articleNumber, category, invoiceCategoryId, currencyId);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool UpdateMerchandise()
        {
            decimal prize;
            string prizeString, str;
            int supplierId;
            try
            {
                if (IsNull(SupplierCombobox.GetSelectedIdentity()) ||
                    SupplierCombobox.GetSelectedIdentity().GetId() == PlattformOrdManData.NO_ID)
                {
                    str = "This product have no selected supplier, proceed anyway?";
                    if (MessageBox.Show(str, "No supplier selected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return false;
                    }
                    supplierId = PlattformOrdManData.NO_ID;
                }
                else
                {
                    supplierId = SupplierCombobox.GetSelectedIdentity().GetId();
                }
                prizeString = GetPrizeString(PrizeTextBox.Text);
                if (!GetPrizeDecimal(prizeString, out prize))
                {
                    ShowWarning("Error, Approximate Prize could not be converted to a number! Update canceled!");
                    return false;
                }
                if (prizeString.Trim() == "")
                {
                    prize = PlattformOrdManData.NO_COUNT;
                }
                MyMerchandise.SetIdentifier(NameTextBox.Text);
                MyMerchandise.SetAmount(AmountTextBox.Text);
                MyMerchandise.SetSupplierId(supplierId);
                UpdateArticleNumber();
                MyMerchandise.SetApprPrize(prize);
                MyMerchandise.SetCategory(CategoryTextBox.Text);
                MyMerchandise.SetStorage(StorageTextBox.Text);
                MyMerchandise.SetComment(CommentTextBox.Text);
                MyMerchandise.SetEnabled(!DissableCheckBox.Checked);
                MyMerchandise.SetInvoiceCategoryId(invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId());
                MyMerchandise.SetCurrencyId(CurrencyCombobox.GetSelectedCurrency().GetId());
                MyMerchandise.Set();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UpdateArticleNumber()
        {
            if (ArticleNumberComboBox.Text.Trim() != MyMerchandise.GetCurrentArticleNumberString())
            {
                MyMerchandise.AddArticleNumber(ArticleNumberComboBox.Text.Trim(), true);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    if (!CreateMerchandise())
                    {
                        return;
                    };
                    break;
                case UpdateMode.Edit:
                    if (!UpdateMerchandise())
                    {
                        return;
                    }
                    break;
            }
            Close();
        }

        private void EditInvoiceCategoryButton_Click(object sender, EventArgs e)
        {
            int selectedInvoiceCategoryId;
            selectedInvoiceCategoryId = invoiceCategoryCombobox1.GetSelectedInvoiceCategory().GetId();
            ShowInvoiceCategoriesDialog showInvoiceCategoryDialog;
            showInvoiceCategoryDialog = new ShowInvoiceCategoriesDialog();
            showInvoiceCategoryDialog.ShowDialog();
            invoiceCategoryCombobox1.LoadInvoiceCategory();
            invoiceCategoryCombobox1.SetSelectedInvoiceCategory(selectedInvoiceCategoryId);
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
            InvoiceCategoryNumberTextBox.Text = GetInvoiceCategoryCode();
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void EditCurrencyButton_Click(object sender, EventArgs e)
        {
            Currency currentCurrency;
            ShowCurrenciesDialog showCurrenciesDialog;
            showCurrenciesDialog = new ShowCurrenciesDialog();
            currentCurrency = CurrencyCombobox.GetSelectedCurrency();
            if (showCurrenciesDialog.ShowDialog() == DialogResult.OK)
            {
                CurrencyCombobox.LoadCurrencies();
                if (IsNotNull(currentCurrency))
                {
                    CurrencyCombobox.SetSelectedCurrency(currentCurrency);
                }
            }
        }

        private void MakeOrderButton_Click(object sender, EventArgs e)
        {
            CreatePostDialog createPostDialog;
            Merchandise merchandice;
            bool isDoubtfulProd = false;
            merchandice = GetMerchandise();
            if (!merchandice.IsEnabled())
            {
                MessageBox.Show("The product has to be enabled before proceeding (now disabled), continue?",
                    "Enable status", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create, merchandice);
            createPostDialog.MdiParent = this.MdiParent;
            // Subscribe for changes, if any order history window open
            createPostDialog.Show();
        }

        private void ShowSupplierButton_Click(object sender, EventArgs e)
        {
            EditSupplierDialog editSupplierDialog;
            Supplier supplier;
            try
            {
                if (SupplierCombobox.HasSelectedIdentity())
                {
                    supplier = SupplierCombobox.GetSelectedIdentity() as Supplier;
                    editSupplierDialog = new EditSupplierDialog(supplier, UpdateMode.Edit);
                    editSupplierDialog.MdiParent = this.MdiParent;
                    editSupplierDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }

        }
    }
}