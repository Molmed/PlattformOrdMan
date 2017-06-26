using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class MerchandiseCombobox : SearchingCombobox
    {
        private int MySupplierId;
        public MerchandiseCombobox()
        {
            InitializeComponent();
        }

        public void Init(bool showNoSelectedString, bool showActiveOnly)
        {
            MerchandiseList products;
            DataIdentityList identities = new DataIdentityList();
            if (showActiveOnly)
            {
                products = MerchandiseManager.GetActiveMerchandiseOnly();
            }
            else
            {
                products = MerchandiseManager.GetMerchandiseFromCache();
            }
            foreach (Merchandise product in products)
            {
                identities.Add(new MerchandiseViewItem(product, true));
            }
            base.Init(identities, "product", showNoSelectedString);
            MySupplierId = PlattformOrdManData.NO_ID;
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return MyIdentities != null && MyIdentities.GetById(merchandiseId) != null;
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            MerchandiseViewItem merchViewItem = null;
            bool showSupplier;
            int ind;
            if (merchandise != null && MyIdentities != null)
            {
                merchViewItem = (MerchandiseViewItem)MyIdentities.GetById(merchandise.GetId());
            }
            if (merchandise != null && merchViewItem != null)
            {
                ind = MyIdentities.GetIndex(merchViewItem);
                showSupplier = MySupplierId == PlattformOrdManData.NO_ID;
                MyIdentities[ind] = new MerchandiseViewItem(merchandise, showSupplier);
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i] is MerchandiseViewItem &&
                        ((MerchandiseViewItem)this.Items[i]).GetId() == merchandise.GetId())
                    {
                        this.Items[i] = MyIdentities[ind];
                        break;
                    }
                }
            }
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        {
            MerchandiseViewItem selectedItem = null;
            bool showSuppliers;
            if (merchandise != null && !HasMerchandiseLoaded(merchandise.GetId()))
            { 
                showSuppliers = MySupplierId == PlattformOrdManData.NO_ID;
                MyIdentities.Add(new MerchandiseViewItem(merchandise, showSuppliers));
                MyIdentities.Sort();
                if (this.SelectedIndex > -1)
                {
                    selectedItem = (MerchandiseViewItem)this.SelectedItem;
                    LoadIdentities();
                    this.SelectedItem = selectedItem;
                }
                else
                {
                    LoadIdentities();
                }
            }
        }

        public Boolean HasSelectedMerchandise()
        {
            return HasSelectedIdentity();
        }

        public override void LoadIdentities()
        {
            LoadMerchandise(MySupplierId);
        }

        public void SetSupplierId(int supplierId)
        {
            MySupplierId = supplierId;
        }

        public void LoadMerchandise(int supplierId)
        {
            this.BeginUpdate();
            this.Items.Clear();
            if (MyShowNoSelectionString)
            {
                this.Items.Add(MyNoSelectionString);
            }
            MySupplierId = supplierId;
            if (MySupplierId == PlattformOrdManData.NO_ID)
            {
                SetShowSupplier(true);
            }
            else
            {
                SetShowSupplier(false);
            }

            foreach (DataIdentity identity in MyIdentities)
            {
                if (supplierId != PlattformOrdManData.NO_ID)
                {
                    if (((MerchandiseViewItem)identity).GetSupplierId() == supplierId)
                    {
                        this.Items.Add(identity);
                    }
                }
                else
                {
                    this.Items.Add(identity);
                }
            }
            this.EndUpdate();
            this.SelectedIndex = -1;
            this.Text = MySearchInfoString;
            this.OnSelectedIndexChanged(new EventArgs());
        }

        protected override bool IsWithinSearchingCriteria(DataIdentity identity, String searchString)
        {
            if (MySupplierId == PlattformOrdManData.NO_ID)
            {
                if (((MerchandiseViewItem)identity).GetFixedIdentifier().ToLower().Contains(searchString.ToLower()))
                {
                    return true;
                }
            }
            else
            {
                if (((MerchandiseViewItem)identity).GetSupplierId() == MySupplierId &&
                    ((MerchandiseViewItem)identity).GetFixedIdentifier().ToLower().Contains(searchString.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public Merchandise GetSelectedMerchandise()
        {
            if (this.SelectedItem == null || this.SelectedItem.GetType() != typeof(MerchandiseViewItem))
            {
                return null;
            }
            return ((MerchandiseViewItem)this.SelectedItem).GetMerchandise();
        }

        public void SetShowSupplier(bool showSupplier)
        {
            foreach (DataIdentity identity in MyIdentities)
            {
                ((MerchandiseViewItem)identity).ShowSupplierInText(showSupplier);
            }
        }

        private class MerchandiseViewItem : DataIdentity
        {
            private Merchandise MyMerchandise;
            public MerchandiseViewItem(Merchandise merchandise, bool showSuppliers)
                : base(merchandise.GetId(), merchandise.GetIdentifier())
            {
                MyMerchandise = merchandise;
                if (showSuppliers)
                {
                    this.UpdateIdentifier(GetIdentifierWithSupplier());
                }
                else
                {
                    this.UpdateIdentifier(GetFixedIdentifier());                
                }
            }

            public String GetFixedIdentifier()
            {
                string identifier;
                if (MyMerchandise == null)
                {
                    return "";
                }
                identifier = MyMerchandise.GetIdentifier();
                if(MyMerchandise.GetAmount() != "")
                {
                    identifier = identifier + ", " + MyMerchandise.GetAmount();
                }
                return identifier;
            }

            public int GetSupplierId()
            {
                int id = -1;
                if (MyMerchandise.GetSupplier() != null)
                {
                    id = MyMerchandise.GetSupplier().GetId();
                }
                return id;
            }

            private String GetIdentifierWithSupplier()
            {
                string identifier;
                if (MyMerchandise == null)
                {
                    return "";
                }
                identifier = GetFixedIdentifier();
                if(IsNotEmpty(MyMerchandise.GetSupplierName()))
                {
                    identifier += " (" + MyMerchandise.GetSupplierName() + ")";
                }
                return identifier;
            }

            public void ShowSupplierInText(bool showSupplier)
            {
                if (showSupplier)
                {
                    this.UpdateIdentifier(GetIdentifierWithSupplier());
                }
                else
                {
                    this.UpdateIdentifier(GetFixedIdentifier());
                }
            }

            public override DataType GetDataType()
            {
                return DataType.Merchandise;
            }

            public Merchandise GetMerchandise()
            {
                return MyMerchandise;
            }
        }

    }
}
