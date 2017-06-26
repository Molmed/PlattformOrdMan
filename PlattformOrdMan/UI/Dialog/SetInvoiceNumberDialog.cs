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
    public partial class SetInvoiceNumberDialog : OrdManForm
    {
        private PostList MyPosts;
        public SetInvoiceNumberDialog(PostList posts)
        {
            InitializeComponent();
            MyPosts = posts;
            InitCustomerNumberCombobox();
        }

        private void InitCustomerNumberCombobox()
        {
            int supplierId, commonCustNumId;
            Supplier commonSupplier;
            GroupCategory commonGroup;
            CustomerNumber commonCustomerNumber = null;
            bool hasCommonGroupCategory = true, hasCommonCustomerNumber = true;
            if(IsEmpty(MyPosts))
            {
                throw new Data.Exception.DataException("Empty post list");
            }
            supplierId = MyPosts[0].GetSupplierId();
            commonGroup = MyPosts[0].GetGroupCategory();
            commonCustNumId = MyPosts[0].GetCustomerNumberId();
            commonCustomerNumber = MyPosts[0].GetCustomerNumber();

            foreach (Post post in MyPosts)
            {
                if (supplierId != post.GetSupplierId())
                {
                    throw new Data.Exception.DataException("Posts with different suppliers selected");
                }
                if (post.GetGroupCategory() != commonGroup)
                {
                    hasCommonGroupCategory = false;
                }
                if (post.GetCustomerNumberId() != commonCustNumId)
                {
                    hasCommonCustomerNumber = false;
                }
            }
            commonSupplier = SupplierManager.GetSupplierById(supplierId);

            foreach (CustomerNumber cust in commonSupplier.GetCustomerNumbers())
            {
                if (!hasCommonGroupCategory || cust.GetGroupCategory() == commonGroup)
                {
                    CustomerNumberComboBox.Items.Add(cust);
                }
            }
            if (CustomerNumberComboBox.Items.Count == 0)
            {
                CustomerNumberComboBox.Enabled = false;
            }
            if (hasCommonCustomerNumber && IsNotNull(commonCustomerNumber))
            {
                CustomerNumberComboBox.SelectedItem = commonCustomerNumber;
            }
            if (hasCommonGroupCategory)
            {
                CustNumLabel.Text = "Customer number from " + commonGroup.ToString() + " group";
            }
            else
            {
                CustNumLabel.Text = "Customer number from both " + GroupCategory.Plattform.ToString() + " and " +
                    GroupCategory.Research.ToString() + " groups";
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void InvoiceInstCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                NoInvoiceCheckBox.Checked = false;
                NoInvoiceCheckBox.Enabled = false;
            }
            else
            {
                NoInvoiceCheckBox.Enabled = true;
            }
        }

        private void InvoiceClinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                NoInvoiceCheckBox.Checked = false;
                NoInvoiceCheckBox.Enabled = false;
            }
            else
            {
                NoInvoiceCheckBox.Enabled = true;
            }
        }

        private void NoInvoiceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                InvoiceNumberTextBox.Text = "";
                InvoiceNumberTextBox.Enabled = false;
            }
            else
            {
                InvoiceNumberTextBox.Enabled = true;
            }

        }

        public bool NoInvoice
        {
            get
            {
                return NoInvoiceCheckBox.Checked;
            }
        }

        public string InvoiceNumber
        {
            get
            {
                return InvoiceNumberTextBox.Text.Trim();
            }
        }

        public CustomerNumber CustomerNumber
        {
            get
            {
                if (CustomerNumberComboBox.SelectedIndex > -1)
                {
                    return (CustomerNumber)CustomerNumberComboBox.SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public int CustomerNumberId
        {
            get
            {
                if (CustomerNumberComboBox.SelectedIndex > -1)
                {
                    return ((CustomerNumber)CustomerNumberComboBox.SelectedItem).GetId();
                }
                else
                {
                    return PlattformOrdManData.NO_ID;
                }
                
            }
        }
    }
}
