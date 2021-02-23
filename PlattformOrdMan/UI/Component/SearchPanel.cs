using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.PostData;

namespace PlattformOrdMan.UI.Component
{
    public partial class SearchPanel : UserControl
    {
        private const String FREE_TEXT_SEARCH = "Free text search ...";
        public delegate void ExpandEvent();

        public delegate void CollapseEvent();

        public delegate void SupplierChangedEvent();

        public delegate void MerchendiseChangedEvent();

        public delegate void UserChangedEvent();

        public event ExpandEvent SearchboxExpanded;
        public event CollapseEvent SearchboxCollapsed;
        public event SupplierChangedEvent SupplierChanged;
        public event MerchendiseChangedEvent MerchendiseChanged;
        public event UserChangedEvent UserChanged;

        private readonly int _panel2OrigHeight;
        private readonly int _splitterDistance;

        public string Caption
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public SearchPanel()
        {
            InitializeComponent();
            _panel2OrigHeight = splitContainer1.Panel2.Height;
            _splitterDistance = splitContainer1.SplitterDistance;
            LinrPanel.Visible = false;
            toggleButton1.SplitterExpanded += OnSplitterExpanded;
            toggleButton1.SplitterCollapsed += OnSplitterCollapsed;
            toggleButton1.OnClick(null, null);
        }

        public void Init()
        {
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
            var suppliers = SupplierManager.GetSuppliersFromCache();
            SupplierCombobox.Init(suppliers, "supplier", true);
            SupplierCombobox.LoadIdentitiesWithInfoText();
            SupplierCombobox.OnMyControlledSelectedIndexChanged += OnSuppierChanged;
            merchandiseCombobox1.Init(true, false);
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.OnMyControlledSelectedIndexChanged += OnMerchendiseChange;
            userComboBox1.Init(true, "booker");
            userComboBox1.LoadIdentitiesWithInfoText();
            userComboBox1.OnMyControlledSelectedIndexChanged +=OnUserChanged;
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
            FreeTextSearchTextBox.Enter += FreeTextSearchTextBoxOnEnter;
        }

        private void FreeTextSearchTextBoxOnEnter(object sender, EventArgs e)
        {
            if (FreeTextSearchTextBox.Text == FREE_TEXT_SEARCH)
            {
                FreeTextSearchTextBox.Text = "";
            }
        }

        private void OnUserChanged()
        {
            UserChanged?.Invoke();
        }

        private void OnMerchendiseChange()
        {
            MerchendiseChanged?.Invoke();
        }

        private void OnSuppierChanged()
        {
            SupplierChanged?.Invoke();
        }

        public void Reload()
        {
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
            SupplierCombobox.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            userComboBox1.LoadIdentitiesWithInfoText();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
        }
        private bool IsWithinSearchCriteria(Post post)
        {
            if (SupplierCombobox.SelectedIndex > 0 &&
                SupplierCombobox.GetSelectedIdentity().GetId() != post.GetSupplierId())
            {
                return false;
            }
            if (merchandiseCombobox1.SelectedIndex > 0 && post.GetMerchandise() != null &&
                merchandiseCombobox1.GetSelectedIdentity().GetId() != post.GetMerchandise().GetId())
            {
                return false;
            }
            if (userComboBox1.SelectedIndex > 0 && post.GetBooker() != null &&
                userComboBox1.GetSelectedIdentity().GetId() != post.GetBooker().GetId())
            {
                return false;
            }
            if (!IsWithinFreeTextSearchCriteria(post))
            {
                return false;
            }
            return true;
        }

        private bool IsWithinFreeTextSearchCriteria(Post post)
        {
            var searchStr = FreeTextSearchTextBox.Text.Trim();
            if (searchStr.Length > 0 &&
                searchStr != FREE_TEXT_SEARCH)
            {
                if (post.GetPurchaseOrderNo().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetSalesOrderNo().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetBookerName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetBookDate().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetSupplier() != null &&
                    post.GetSupplier().GetIdentifier().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetMerchandise() != null &&
                    post.GetMerchandise().GetIdentifier().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetMerchandise() != null &&
                    post.GetMerchandise().GetAmount().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetAmountString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetPriceWithCurrencyString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetOrdererName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetOrderDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalSignUserName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArticleNumberString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceCategoryCodeString2().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoicerUserName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceStatusString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetComment().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceNumber().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        public void AddCreatedSupplier(Supplier supplier)
        {
            SupplierCombobox.AddCreatedSupplier(supplier);
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        {
            merchandiseCombobox1.AddCreatedMerchandise(merchandise);
        }

        private void ResetSearchFields()
        {
            SupplierCombobox.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            userComboBox1.LoadIdentitiesWithInfoText();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
        }

        private void OnSplitterCollapsed()
        {
            splitContainer1.Panel2Collapsed = true;
            groupBox1.Height -= _panel2OrigHeight;
            splitContainer1.SplitterDistance = _splitterDistance;
            LinrPanel.Visible = true;
            SearchboxCollapsed?.Invoke();
        }

        private void OnSplitterExpanded()
        {
            splitContainer1.Panel2Collapsed = false;
            groupBox1.Height += _panel2OrigHeight;
            splitContainer1.SplitterDistance = _splitterDistance;
            LinrPanel.Visible = false;
            SearchboxExpanded?.Invoke();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResetSearchFields();
        }
    }
}
