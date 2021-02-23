using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Component
{
    public partial class SearchPanel : UserControl
    {
        private const String FREE_TEXT_SEARCH = "Free text search ...";
        public delegate void ExpandEvent();

        public delegate void CollapseEvent();

        public delegate void SupplierChangedEvent();

        public delegate void MerchendiseChangedEvent();

        public event ExpandEvent SearchboxExpanded;
        public event CollapseEvent SearchboxCollapsed;
        public event SupplierChangedEvent SupplierChanged;
        public event MerchendiseChangedEvent MerchendiseChanged;

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
