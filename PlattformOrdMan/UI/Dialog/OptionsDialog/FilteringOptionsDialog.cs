using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog.OptionsDialog
{
    public partial class FilteringOptionsDialog : Form
    {
        public FilteringOptionsDialog()
        {
            InitializeComponent();
            InitPlaceOfPurchaseFilteringListView();
        }

        private void InitPlaceOfPurchaseFilteringListView()
        {
            PlaceOfPurchaseFilterListView.Columns.Add("Group", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            PlaceOfPurchaseFilterListView.BeginUpdate();
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                var popStr = PlattformOrdManData.GetPlaceOfPurchaseString(pop);
                var lvi = new ListViewItem(popStr);
                PlaceOfPurchaseFilterListView.Items.Add(lvi);
                if (PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Contains(pop.ToString()))
                {
                    lvi.Checked = true;
                }
            }
            PlaceOfPurchaseFilterListView.EndUpdate();
        }


        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void SavePlaceOfPurchaseFiltering()
        {
            StringCollection popFilter = new StringCollection();
            foreach (ListViewItem lvi in PlaceOfPurchaseFilterListView.Items)
            {
                if (lvi.Checked)
                {
                    var pop = PlattformOrdManData.GetPlaceOfPurchaseFromString(lvi.Text);
                    var popEnumStr = pop.ToString();
                    popFilter.Add(popEnumStr);
                }
            }
            PlattformOrdManData.Configuration.PlaceOfPurchaseFilter = popFilter;
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            SavePlaceOfPurchaseFiltering();
            Close();
        }
    }
}
