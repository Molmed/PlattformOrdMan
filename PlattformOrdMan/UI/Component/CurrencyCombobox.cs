using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class CurrencyCombobox : ComboBox
    {
        public CurrencyCombobox()
        {
            InitializeComponent();
            //this.SelectedIndexChanged +=new EventHandler(CurrencyCombobox_SelectedIndexChanged);
        }

        public Currency GetSelectedCurrency()
        {
            return (Currency)(this.SelectedItem);
        }

        //private void CurrencyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.SelectedIndex > -1)
        //    {
        //        this.Text = ((Currency)this.SelectedItem).GetIdentityString();
        //    }
        //    else
        //    {
        //        this.Text = "";
        //    }
        //}

        public bool HasCurrencyLoaded(int currId)
        {
            foreach (Currency curr in this.Items)
            {
                if (curr.GetId() == currId)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean HasSelectedCurrency()
        {
            return this.SelectedItem != null;
        }

        public void LoadCurrencies()
        {
            this.BeginUpdate();
            this.Items.Clear();
            foreach (Currency currency in PlattformOrdMan.Data.CurrencyManager.GetCurrencies())
            {
                this.Items.Add(currency);
            }
            this.EndUpdate();
        }

        public void SetSelectedCurrency(Currency currency)
        {
            if (currency != null)
            {
                SetSelectedCurrency(currency.GetId());
            }
            else
            {
                this.SelectedIndex = -1;
            }
        }

        public void SetSelectedCurrency(Int32 currencyId)
        {
            foreach (Currency s in this.Items)
            {
                if (s.GetId() == currencyId)
                {
                    this.SelectedItem = s;
                    break;
                }
            }
        }
    }
}
