using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.UI.View.Base;

namespace Molmed.PlattformOrdMan.UI.View
{
    public partial class MerchandiseListView : OrderManListView
    {
        private Dictionary<int, List<MerchandiseViewItem>> MyProductDict;
        private Dictionary<int, List<MerchandiseViewItem>> MySupplierDict;

        public MerchandiseListView()
            : base()
        {
            InitializeComponent();
            MyProductDict = new Dictionary<int, List<MerchandiseViewItem>>();
            MySupplierDict = new Dictionary<int, List<MerchandiseViewItem>>();
            SetDefaultSortOrder();
        }

        public MerchandiseListView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            MyProductDict = new Dictionary<int, List<MerchandiseViewItem>>();
            MySupplierDict = new Dictionary<int, List<MerchandiseViewItem>>();
            SetDefaultSortOrder();
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return MyProductDict.ContainsKey(merchandiseId);
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            if (merchandise != null && MyProductDict.ContainsKey(merchandise.GetId()))
            {
                foreach (MerchandiseViewItem viewItem in MyProductDict[merchandise.GetId()])
                {
                    viewItem.ReloadMerchandise(merchandise);
                }
            }
        }

        public void AddCreatedMerchandise(int merchandiseId)
        {
            Merchandise merch = MerchandiseManager.GetMerchandiseById(merchandiseId);
            if (MyProductDict.ContainsKey(merchandiseId))
            {
                MyProductDict[merchandiseId].Add(new MerchandiseViewItem(merch));
            }
            else
            {
                MyProductDict.Add(merchandiseId, new List<MerchandiseViewItem>() { new MerchandiseViewItem(merch)});
            }
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MySupplierDict.ContainsKey(supplierId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            if (supplier != null && MySupplierDict.ContainsKey(supplier.GetId()))
            {
                foreach (MerchandiseViewItem viewItem in MySupplierDict[supplier.GetId()])
                {
                    viewItem.ReloadSupplier(supplier);
                }
            }
        }

        public override void EndAddItems()
        {
            int supplierId, merchId;
            foreach (MerchandiseViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetMerchandise().GetSupplierId();
                if (MySupplierDict.ContainsKey(supplierId))
                {
                    MySupplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    MySupplierDict.Add(supplierId, new List<MerchandiseViewItem>() { viewItem });
                }
                merchId = viewItem.GetMerchandise().GetId();
                if (MyProductDict.ContainsKey(merchId))
                {
                    MyProductDict[merchId].Add(viewItem);
                }
                else
                {
                    MyProductDict.Add(merchId, new List<MerchandiseViewItem>() { viewItem });
                }
            }
            base.EndAddItems();
        }

        public override void EndLoadItems()
        {
            int supplierId, merchId;
            MySupplierDict = new Dictionary<int, List<MerchandiseViewItem>>();
            MyProductDict = new Dictionary<int, List<MerchandiseViewItem>>();
            foreach (MerchandiseViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetMerchandise().GetSupplierId();
                if (MySupplierDict.ContainsKey(supplierId))
                {
                    MySupplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    MySupplierDict.Add(supplierId, new List<MerchandiseViewItem>() { viewItem });
                }
                merchId = viewItem.GetMerchandise().GetId();
                if (MyProductDict.ContainsKey(merchId))
                {
                    MyProductDict[merchId].Add(viewItem);
                }
                else
                {
                    MyProductDict.Add(merchId, new List<MerchandiseViewItem>() { viewItem });
                }
            }
            base.EndLoadItems();
        }

        public override void EndLoadChunk()
        {
            int supplierId, merchId;
            ListViewItem[] residue = new ListViewItem[MyAddListViewItemsIndex];
            for (int i = 0; i < MyAddListViewItemsIndex; i++)
            {
                residue[i] = MyAddListViewItems[i];
            }
            foreach (MerchandiseViewItem viewItem in residue)
            {
                supplierId = viewItem.GetMerchandise().GetSupplierId();
                if (MySupplierDict.ContainsKey(supplierId))
                {
                    MySupplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    MySupplierDict.Add(supplierId, new List<MerchandiseViewItem>() { viewItem });
                }
                merchId = viewItem.GetMerchandise().GetId();
                if (MyProductDict.ContainsKey(merchId))
                {
                    MyProductDict[merchId].Add(viewItem);
                }
                else
                {
                    MyProductDict.Add(merchId, new List<MerchandiseViewItem>() { viewItem });
                }
            }
            base.EndLoadChunk();
        }

        protected override void LoadChunk()
        {
            int supplierId, merchId;
            foreach (MerchandiseViewItem viewItem in MyAddListViewItems)
            {
                supplierId = viewItem.GetMerchandise().GetSupplierId();
                if (MySupplierDict.ContainsKey(supplierId))
                {
                    MySupplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    MySupplierDict.Add(supplierId, new List<MerchandiseViewItem>() { viewItem });
                }
                merchId = viewItem.GetMerchandise().GetId();
                if (MyProductDict.ContainsKey(merchId))
                {
                    MyProductDict[merchId].Add(viewItem);
                }
                else
                {
                    MyProductDict.Add(merchId, new List<MerchandiseViewItem>() { viewItem });                
                }
            }
            base.LoadChunk();
        }

        public override void BeginLoadChunk(int chunkSize)
        {
            base.BeginLoadChunk(chunkSize);
            MyProductDict = new Dictionary<int, List<MerchandiseViewItem>>();
            MySupplierDict = new Dictionary<int, List<MerchandiseViewItem>>();
        }


        public void SetDefaultSortOrder()
        {
            this.ListViewItemSorter = new ListViewComparerSupplierMerchandise();
        }

        private class ListViewComparerSupplierMerchandise : ListViewComparerChiasma
        {
            public ListViewComparerSupplierMerchandise()
                : base()
            {
            }

            public override int Compare(Object object1, Object object2)
            {
                int compareValue = 0;
                MerchandiseViewItem listViewItem1, listViewItem2;
                string supplierName1, supplierName2, productName1, productName2;

                listViewItem1 = (MerchandiseViewItem)object1;
                listViewItem2 = (MerchandiseViewItem)object2;
                supplierName1 = listViewItem1.GetMerchandise().GetSupplierName();
                supplierName2 = listViewItem2.GetMerchandise().GetSupplierName();
                productName1 = listViewItem1.GetMerchandise().GetIdentifier();
                productName2 = listViewItem2.GetMerchandise().GetIdentifier();
                if (supplierName1.CompareTo(supplierName2) > 0)
                {
                    compareValue = 1;
                }
                else if (supplierName1.CompareTo(supplierName2) < 0)
                {
                    compareValue = -1;
                }
                else // same supplier
                {
                    if (productName1.CompareTo(productName2) > 0)
                    {
                        compareValue = 1;
                    }
                    else if (productName1.CompareTo(productName2) < 0)
                    {
                        compareValue = -1;
                    }
                    else
                    {
                        compareValue = 0;
                    }
                }
                return compareValue;
            }
        }
    }

    public class MerchandiseViewItem : ListViewItem
    {
        private Merchandise MyMerchandise;
        public enum ListIndex : int
        {
            MerchandiseName = 0,
            Amount = 1,
            Enabled = 2,
            Supplier = 3,
            InvoiceCategory = 4,
            Category = 5,
            ArticleNumber = 6,
            ApprPrize = 7,
            Storage = 8,
            Comment = 9,
        }

        public MerchandiseViewItem(Merchandise merchandise)
            : base(merchandise.GetIdentifier())
        {
            MyMerchandise = merchandise;
            this.SubItems.Add(merchandise.GetAmount());
            this.SubItems.Add(merchandise.GetEnabledString());
            this.SubItems.Add(merchandise.GetSupplierName());
            this.SubItems.Add(merchandise.GetInvoiceCategoryName());
            this.SubItems.Add(merchandise.GetCategory());
            this.SubItems.Add(merchandise.GetCurrentArticleNumberString());
            this.SubItems.Add(merchandise.GetApprPrizeString());
            this.SubItems.Add(merchandise.GetStorage());
            this.SubItems.Add(merchandise.GetComment());
            if (!MyMerchandise.IsEnabled())
            {
                this.ForeColor = Color.Red;
                this.ToolTipText = "This product is not up to date";
            }
        }
        public Merchandise GetMerchandise()
        {
            return MyMerchandise;
        }

        public void ReloadSupplier(int supplierId)
        {
            MyMerchandise.ResetSupplierLocal();
            UpdateViewItem();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MyMerchandise.ReloadSupplier(supplier);
            UpdateViewItem();
        }

        public void ReloadMerchandise(int merchId)
        {
            MyMerchandise = MerchandiseManager.GetMerchandiseById(merchId);
            UpdateViewItem();
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            MyMerchandise = merchandise;
            UpdateViewItem();
        }

        public void UpdateViewItem()
        {
            if (!MyMerchandise.IsEnabled())
            {
                this.ForeColor = Color.Red;
                this.ToolTipText = "This product is not up to date";
            }
            this.SubItems[(int)ListIndex.MerchandiseName].Text = MyMerchandise.GetIdentifier();
            this.SubItems[(int)ListIndex.Amount].Text = MyMerchandise.GetAmount();
            this.SubItems[(int)ListIndex.Enabled].Text = MyMerchandise.GetEnabledString();
            this.SubItems[(int)ListIndex.Supplier].Text = MyMerchandise.GetSupplierShortName();
            this.SubItems[(int)ListIndex.InvoiceCategory].Text = MyMerchandise.GetInvoiceCategoryName();
            this.SubItems[(int)ListIndex.Category].Text = MyMerchandise.GetCategory();
            this.SubItems[(int)ListIndex.ArticleNumber].Text = MyMerchandise.GetCurrentArticleNumberString();
            this.SubItems[(int)ListIndex.ApprPrize].Text = MyMerchandise.GetApprPrizeString();
            this.SubItems[(int)ListIndex.Storage].Text = MyMerchandise.GetStorage();
            this.SubItems[(int)ListIndex.Comment].Text = MyMerchandise.GetComment();
        }
    }

}
