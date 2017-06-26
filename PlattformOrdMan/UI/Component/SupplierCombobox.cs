using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class SupplierCombobox : SearchingCombobox
    {
        public SupplierCombobox()
        {
            InitializeComponent();
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MyIdentities.GetById(supplierId) != null;
        }

        public void ReloadSupplier(Supplier supplier)
        {
            Supplier sup = null;
            int ind;
            if (supplier != null && MyIdentities != null)
            {
                sup = (Supplier)MyIdentities.GetById(supplier.GetId());            
            }
            if (sup != null)
            {
                ind = MyIdentities.GetIndex(sup);
                MyIdentities[ind] =  supplier;
                for(int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i] is Supplier &&
                        ((Supplier)this.Items[i]).GetId() == supplier.GetId())
                    {
                        this.Items[i] = MyIdentities[ind];
                        break;
                    }
                }
            }
        }

        public void AddCreatedSupplier(Supplier supplier)
        {
            Supplier selectedSup = null;
            if (supplier != null)
            {
                MyIdentities.Add(supplier);
                MyIdentities.Sort();
                if (this.SelectedIndex > -1)
                {
                    selectedSup = (Supplier)this.SelectedItem;
                    LoadIdentities();
                    this.SelectedItem = selectedSup;
                }
                else
                {
                    LoadIdentities();
                }
            }
        }

        public Supplier GetSelectedSupplier()
        {
            return (Supplier)(this.SelectedItem);
        }

        public Boolean HasSelectedSupplier()
        {
            return this.SelectedItem != null && this.SelectedItem is Supplier;
        }


        public void SetSelectedSupplier(Supplier supplier)
        {
            if (supplier != null)
            {
                this.SelectedText = supplier.GetIdentifier();
            }
            else
            {
                this.SelectedIndex = -1;
            }
        }

        public void SetSelectedSupplier(Int32 supplierId)
        {
            foreach (Supplier s in this.Items)
            {
                if (s.GetId() == supplierId)
                {
                    this.SelectedItem = s;
                    break;
                }
            }
        }
    }
}
