using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;
using PlattformOrdMan.UI.View.Base;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowCurrenciesDialog : OrdManForm
    {
        private const String DELETE = "Delete";
        private const String PROPERTIES = "Properties";

        private CurrencyList MyCurrencies;
        private bool MyIsCurrencyUpdated;

        public ShowCurrenciesDialog()
        {
            InitializeComponent();
            MyCurrencies = PlattformOrdMan.Data.CurrencyManager.GetCurrencies();
            Init();
        }

        private void Init()
        {
            int width;
            width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            CurrencyListView.AddColumn("Description", width, ListDataType.String);
            CurrencyListView.AddColumn("Currency code", width, ListDataType.String);
            CurrencyListView.AddColumn("Symbol", width, ListDataType.String);
            UpdateListView();
            AddMenuItem(CurrencyListView, DELETE, DeleteMenuItem_Click);
            AddMenuItem(CurrencyListView, PROPERTIES, PropertiesMenuItem_Click);
            CurrencyListView.DoubleClick += new EventHandler(PropertiesMenuItem_Click);
            MyIsCurrencyUpdated = false;
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            CurrencyList currencies = new CurrencyList();
            String str;
            str = "Are you sure to delete the " + CurrencyListView.SelectedItems.Count + " items?";
            if (MessageBox.Show(str, "Delete currencies", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            foreach (CurrencyViewItem cViewItem in CurrencyListView.SelectedItems)
            {
                currencies.Add(cViewItem.GetCurrency());
            }
            if (Data.CurrencyManager.DeleteCurrencies(currencies))
            {
                foreach (Currency currency in currencies)
                {
                    MyCurrencies.Remove(currency);
                }
                UpdateListView();
            }
            MyIsCurrencyUpdated = true;
        }

        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            EditCurrencyDialog editCurrencyDialog;
            Currency currency;
            currency = ((CurrencyViewItem)CurrencyListView.SelectedItems[0]).GetCurrency();
            editCurrencyDialog = new EditCurrencyDialog(currency, UpdateMode.Edit);
            if (editCurrencyDialog.ShowDialog() == DialogResult.OK)
            {
                UpdateListView();
                SetSelected(currency);
                MyIsCurrencyUpdated = true;
            }
        }

        private void UpdateListView()
        {
            CurrencyListView.Items.Clear();
            CurrencyListView.BeginUpdate();
            foreach (Currency currency in MyCurrencies)
            {
                CurrencyListView.Items.Add(new CurrencyViewItem(currency));
            }
            CurrencyListView.EndUpdate();
        }

        private void MyCloseButton_Click(object sender, EventArgs e)
        {
            if (MyIsCurrencyUpdated)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private class CurrencyViewItem : ListViewItem
        {
            private Currency MyCurrency;
            public enum ListIndex : int
            { 
                Description = 0,
                CurrencyCode = 1,
                Symbol = 2
            }
            public CurrencyViewItem(Currency currency)
                : base(currency.GetDescription())
            {
                MyCurrency = currency;
                this.SubItems.Add(currency.GetCode());
                this.SubItems.Add(currency.GetSymbol());
            }

            public Currency GetCurrency()
            {
                return MyCurrency;
            }

            public void UpdateViewItem()
            {
                this.SubItems[(int)ListIndex.Description].Text = MyCurrency.GetDescription();
                this.SubItems[(int)ListIndex.CurrencyCode].Text = MyCurrency.GetCode();
                this.SubItems[(int)ListIndex.Symbol].Text = MyCurrency.GetSymbol();
            }
        }

        private void SetSelected(Currency currency)
        {
            CurrencyListView.SelectedIndices.Clear();
            foreach (CurrencyViewItem cViewItem in CurrencyListView.Items)
            {
                if (cViewItem.GetCurrency().GetId() == currency.GetId())
                {
                    CurrencyListView.SelectedIndices.Add(cViewItem.Index);
                    CurrencyListView.EnsureVisible(cViewItem.Index);
                    break;
                }
            }
            CurrencyListView.Select();        
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            EditCurrencyDialog editCurrencyDialog;
            Currency currency;
            editCurrencyDialog = new EditCurrencyDialog(null, UpdateMode.Create);
            if (editCurrencyDialog.ShowDialog() == DialogResult.OK)
            {
                MyCurrencies = Data.CurrencyManager.GetCurrencies();
                currency = editCurrencyDialog.GetCurrency();
                UpdateListView();
                SetSelected(currency);
                MyIsCurrencyUpdated = true;
            }
        }
    }
}
