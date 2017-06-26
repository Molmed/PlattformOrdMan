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
    public partial class CreateEditCustomerNumber : OrdManForm
    {
        private UpdateMode MyUpdateMode;
        private CustomerNumber MyCustomerNumber;
        private Supplier MySupplier;

        public CreateEditCustomerNumber(UpdateMode updateMode, CustomerNumber custNum, Supplier supplier)
        {
            InitializeComponent();
            MyUpdateMode = updateMode;
            MyCustomerNumber = custNum;
            MySupplier = supplier;
            Init();
        }

        private void Init()
        {
            InitGroupCombobox();
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    this.Text = "Create Customer Number";
                    GroupComboBox.SelectedItem = UserManager.GetCurrentUser().GetGroupCategory();
                    EnabledCheckBox.Checked = true;
                    break;
                case UpdateMode.Edit:
                    this.Text = "Edit Customer Number";
                    GroupComboBox.SelectedItem = MyCustomerNumber.GetGroupCategory();
                    CustomerNumberTextBox.Text = MyCustomerNumber.GetIdentifier();
                    DescriptionTextBox.Text = MyCustomerNumber.GetDescription();
                    EnabledCheckBox.Checked = MyCustomerNumber.IsEnabled();
                    break;
                default:
                    throw new Data.Exception.DataException("Unknown update alternative!");
            }
        }

        private void InitGroupCombobox()
        {
            foreach (GroupCategory groupCategory in Enum.GetValues(typeof(GroupCategory)))
            {
                GroupComboBox.Items.Add(groupCategory);
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private PlaceOfPurchase GetDefaultPlaceOfPurchase()
        {
            return PlattformOrdManData.GetDefaultPlateOfPurchase((GroupCategory)GroupComboBox.SelectedItem);
        }

        private void UpdateCustomerNumber()
        {
            MyCustomerNumber.SetLocal(CustomerNumberTextBox.Text.Trim(), DescriptionTextBox.Text.Trim(), GetDefaultPlaceOfPurchase(),
                EnabledCheckBox.Checked);
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomerNumberTextBox.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter a customer number!", "Customer number missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                UpdateCustomerNumber();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                HandleError("Error when adding/editing customer number.", ex);
            }

        }
    }
}
