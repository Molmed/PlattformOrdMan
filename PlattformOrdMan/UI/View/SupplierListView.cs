using System.Collections.Generic;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.View
{
    public partial class SupplierListView : OrderManListView
    {
        private Dictionary<int, SupplierViewItem> MySupplierDict;

        public SupplierListView()
        {
            InitializeComponent();
            MySupplierDict = new Dictionary<int, SupplierViewItem>();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            if (supplier != null && MySupplierDict.ContainsKey(supplier.GetId()))
            {
                MySupplierDict[supplier.GetId()].ReloadSupplier(supplier);
            }
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MySupplierDict.ContainsKey(supplierId);
        }

        public override void EndAddItems()
        {
            int supplierId;
            foreach (SupplierViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetSupplierId();
                MySupplierDict.Add(supplierId, viewItem);
            }
            base.EndAddItems();
        }

        public override void EndLoadItems()
        {
            int supplierId;
            MySupplierDict = new Dictionary<int, SupplierViewItem>();
            foreach (SupplierViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetSupplierId();
                MySupplierDict.Add(supplierId, viewItem);
            }
            base.EndLoadItems();
        }

        public override void EndLoadChunk()
        {
            int supplierId;
            ListViewItem[] residue = new ListViewItem[MyAddListViewItemsIndex];
            for (int i = 0; i < MyAddListViewItemsIndex; i++)
            {
                residue[i] = MyAddListViewItems[i];
            }
            foreach (SupplierViewItem viewItem in residue)
            {
                supplierId = viewItem.GetSupplierId();
                MySupplierDict.Add(supplierId, viewItem);
            }
            base.EndLoadChunk();
        }

        protected override void LoadChunk()
        {
            int supplierId;
            foreach (SupplierViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetSupplierId();
                MySupplierDict.Add(supplierId, viewItem);
            }
            base.LoadChunk();
        }

        public override void BeginLoadChunk(int chunkSize)
        {
            base.BeginLoadChunk(chunkSize);
            MySupplierDict = new Dictionary<int, SupplierViewItem>();
        }

    }

    public class SupplierViewItem : ListViewItem
    {
        private Supplier MySupplier;
        public enum ListIndex : int
        { 
            SuppplierName = 0,
            Enabled = 1,
            ShortName = 2,
            TelNr = 3,
            ContractTerminate = 4,
            Comment = 5
        }

        public SupplierViewItem(Supplier supplier)
            : base(supplier.GetIdentifier())
        {
            MySupplier = supplier;
            this.SubItems.Add(supplier.GetEnabledString());
            this.SubItems.Add(supplier.GetShortName());
            this.SubItems.Add(supplier.GetTelNr());
            this.SubItems.Add(supplier.GetContractTerminate());
            this.SubItems.Add(supplier.GetComment());
        }
        public Supplier GetSupplier()
        {
            return MySupplier;
        }

        public int GetSupplierId()
        {
            if (MySupplier == null)
            {
                return PlattformOrdManData.NO_ID;
            }
            return MySupplier.GetId();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MySupplier = supplier;
            UpdateViewItem();
        }

        public void UpdateViewItem()
        {
            this.SubItems[(int)ListIndex.SuppplierName].Text = MySupplier.GetIdentifier();
            this.SubItems[(int)ListIndex.Enabled].Text = MySupplier.GetEnabledString();
            this.SubItems[(int)ListIndex.TelNr].Text = MySupplier.GetTelNr();
            this.SubItems[(int)ListIndex.ContractTerminate].Text = MySupplier.GetContractTerminate();
            this.SubItems[(int)ListIndex.Comment].Text = MySupplier.GetComment();
            this.SubItems[(int)ListIndex.ShortName].Text = MySupplier.GetShortName();
        }
    }

}
