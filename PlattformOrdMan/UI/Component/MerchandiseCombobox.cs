using System;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Component
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
                identities.Add(new MerchandiseViewItem(product));
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
            int ind;
            if (merchandise != null && MyIdentities != null)
            {
                merchViewItem = (MerchandiseViewItem)MyIdentities.GetById(merchandise.GetId());
            }
            if (merchandise != null && merchViewItem != null)
            {
                ind = MyIdentities.GetIndex(merchViewItem);
                MyIdentities[ind] = new MerchandiseViewItem(merchandise);
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
            if (merchandise != null && !HasMerchandiseLoaded(merchandise.GetId()))
            { 
                MyIdentities.Add(new MerchandiseViewItem(merchandise));
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

        private class MerchandiseViewItem : DataIdentity
        {
            private Merchandise MyMerchandise;
            public MerchandiseViewItem(Merchandise merchandise)
                : base(merchandise.GetId(), merchandise.GetIdentifier())
            {
                MyMerchandise = merchandise;
                UpdateIdentifier(GetFixedIdentifier());
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
