using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class EditCurrencyDialog : OrdManForm
    {
        private Currency MyCurrency;
        private UpdateMode MyUpdateMode;
        public EditCurrencyDialog(Currency currency, UpdateMode updateMode)
        {
            InitializeComponent();
            MyCurrency = currency;
            MyUpdateMode = updateMode;
            SaveButton.Enabled = false;
            DescriptionTextBox.TextChanged +=new EventHandler(DescriptionTextBox_TextChanged);
            CurrencyCodeTextBox.TextChanged += new EventHandler(CurrencyCodeTextBox_TextChanged);
            SymbolTextBox.TextChanged += new EventHandler(SymbolTextBox_TextChanged);
            if (MyUpdateMode == UpdateMode.Edit)
            {
                InitEditMode();
            }
        }

        private void InitEditMode()
        {
            DescriptionTextBox.Text = MyCurrency.GetDescription();
            CurrencyCodeTextBox.Text = MyCurrency.GetCode();
            SymbolTextBox.Text = MyCurrency.GetSymbol();
        }

        private void HandleSaveButtonEnabled()
        {
            if (MyUpdateMode == UpdateMode.Create)
            {
                SaveButton.Enabled = IsCurrencyOkForSave();
            }
            else if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsCurrencyOkForSave() && IsUpdated();
            }        
        }

        private void SymbolTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void CurrencyCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();        
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();        
        }

        private bool IsCurrencyOkForSave()
        {
            if ((DescriptionTextBox.Text.Trim().Length > 0 && SymbolTextBox.Text.Trim().Length > 0) ||
                CurrencyCodeTextBox.Text.Trim().Length > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsUpdated()
        {
            String description, code, symbol;
            if (MyCurrency.GetDescription() == null)
            {
                description = "";
            }
            else
            {
                description = MyCurrency.GetDescription();
            }
            if (MyCurrency.GetCode() == null)
            {
                code = "";
            }
            else
            {
                code = MyCurrency.GetCode();            
            }
            if (MyCurrency.GetSymbol() == null)
            {
                symbol = "";
            }
            else 
            {
                symbol = MyCurrency.GetSymbol();
            }

            return (description != DescriptionTextBox.Text.Trim() ||
                code != CurrencyCodeTextBox.Text.Trim() ||
                symbol != SymbolTextBox.Text.Trim());
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public Currency GetCurrency()
        {
            return MyCurrency;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Create)
            {
                MyCurrency = Data.CurrencyManager.CreateCurrency(DescriptionTextBox.Text.Trim(),
                    SymbolTextBox.Text.Trim(), CurrencyCodeTextBox.Text.Trim());
            }
            else if (MyUpdateMode == UpdateMode.Edit)
            {
                MyCurrency.SetDescription(DescriptionTextBox.Text.Trim());
                MyCurrency.SetSymbol(SymbolTextBox.Text.Trim());
                MyCurrency.SetCurrencyCode(CurrencyCodeTextBox.Text.Trim());
                MyCurrency.Set();
            }
            DialogResult = DialogResult.OK;
        }
    }
}
