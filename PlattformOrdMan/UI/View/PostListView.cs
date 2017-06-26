using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.Controller;

namespace Molmed.PlattformOrdMan.UI.View
{
    public partial class PostListView : OrderManListView
    {
        private Dictionary<int, List<PostViewItem>> MySupplierDict;
        private Dictionary<int, List<PostViewItem>> MyProdDict;
        private Dictionary<int, PostViewItem> MyPostDict;
        public PostListView()
        {
            InitializeComponent();
            MySupplierDict = new Dictionary<int, List<PostViewItem>>();
            MyProdDict = new Dictionary<int, List<PostViewItem>>();
            MyPostDict = new Dictionary<int, PostViewItem>();
        }

        public PostListView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            MySupplierDict = new Dictionary<int, List<PostViewItem>>();
            MyProdDict = new Dictionary<int, List<PostViewItem>>();
            MyPostDict = new Dictionary<int, PostViewItem>();
        }

        public override void BeginLoadChunk(int chunkSize)
        {
            base.BeginLoadChunk(chunkSize);
            MySupplierDict = new Dictionary<int, List<PostViewItem>>();
            MyProdDict = new Dictionary<int, List<PostViewItem>>();
            MyPostDict = new Dictionary<int, PostViewItem>();
        }


        public static ListDataType GetListDataType(PostListViewColumn col)
        {
            switch (col)
            { 
                case PostListViewColumn.Amount:
                case PostListViewColumn.InvoiceCategoryCode:
                    return ListDataType.Int32;

                case PostListViewColumn.ApprArrival:
                case PostListViewColumn.ArrivalDate:
                case PostListViewColumn.BookDate:
                case PostListViewColumn.InvoiceDate:
                case PostListViewColumn.OrderDate:
                    return ListDataType.DateTime;

                case PostListViewColumn.ApprPrize:
                case PostListViewColumn.FinalPrize:
                case PostListViewColumn.TotalPrize:
                    return ListDataType.Currency;

                case PostListViewColumn.ArrivalSign:
                case PostListViewColumn.ArtNr:
                case PostListViewColumn.Booker:
                case PostListViewColumn.Comment:
                case PostListViewColumn.DeliveryDeviation:
                case PostListViewColumn.InvoiceClin:
                case PostListViewColumn.InvoiceInst:
                case PostListViewColumn.InvoiceNumber:
                case PostListViewColumn.InvoiceSender:
                case PostListViewColumn.InvoiceStatus:
                case PostListViewColumn.OrderSign:
                case PostListViewColumn.Product:
                case PostListViewColumn.PurchaseOrderNo:
                case PostListViewColumn.SalesOrderNo:
                case PostListViewColumn.Supplier:
                case PostListViewColumn.PlaceOfPurchase:
                case PostListViewColumn.CustomerNumber:
                    return ListDataType.String;
        
                default:
                    throw new Data.Exception.DataException("Unknown enum type: " + col.ToString());
            }
        }

        public static string GetColumnHeaderName(PostListViewColumn col)
        {
            switch (col)
            {
                case PostListViewColumn.Amount:
                case PostListViewColumn.Comment:
                case PostListViewColumn.Supplier:
                case PostListViewColumn.Booker:
                    return col.ToString();

                case PostListViewColumn.InvoiceCategoryCode:
                    return "Invoice category code";
                case PostListViewColumn.ApprArrival:
                    return "Appr. arrival";
                case PostListViewColumn.ArrivalDate:
                    return "Arrival date";
                case PostListViewColumn.BookDate:
                    return "Date";
                case PostListViewColumn.InvoiceDate:
                    return "Invoice check date";
                case PostListViewColumn.OrderDate:
                    return "Order date";
                case PostListViewColumn.ApprPrize:
                    return "Appr. prize";
                case PostListViewColumn.FinalPrize:
                    return "Final prize";
                case PostListViewColumn.TotalPrize:
                    return "Total prize";
                case PostListViewColumn.ArrivalSign:
                    return "Arrival sign.";
                case PostListViewColumn.ArtNr:
                    return "Art. no";
                case PostListViewColumn.DeliveryDeviation:
                    return "Delivery deviation";
                case PostListViewColumn.InvoiceClin:
                    return "Invoice clin";
                case PostListViewColumn.InvoiceInst:
                    return "Invoice inst";
                case PostListViewColumn.InvoiceNumber:
                    return "Invoice number";
                case PostListViewColumn.InvoiceSender:
                    return "Invoice checker";
                case PostListViewColumn.InvoiceStatus:
                    return "Invoice status";
                case PostListViewColumn.OrderSign:
                    return "Order sign";
                case PostListViewColumn.Product:
                    return "Product";
                case PostListViewColumn.PurchaseOrderNo:
                    return "PO";
                case PostListViewColumn.SalesOrderNo:
                    return "SO";
                case PostListViewColumn.PlaceOfPurchase:
                    return "Group";
                case PostListViewColumn.CustomerNumber:
                    return "Customer number";
                default:
                    throw new Data.Exception.DataException("Unknown enum type: " + col.ToString());
            }
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MySupplierDict.ContainsKey(supplierId);
        }

        public void AddViewItem(Post post)
        {
            PostViewItem pViewItem = new PostViewItem(post);
            BeginAddItems(1);
            AddItem(pViewItem);
            EndAddItems();
            this.Sort();
            pViewItem.Selected = true;
            EnsureVisible(pViewItem.Index);
            this.Select();
        }

        public bool HasMerchanidseLoaded(int merchId)
        {
            return MyProdDict.ContainsKey(merchId);
        }

        public bool HasPostLoaded(int postId)
        {
            return MyPostDict.ContainsKey(postId);
        }

        public void ReloadPost(Post post)
        { 
            if(IsNotNull(post))
            {
                if (MyPostDict.ContainsKey(post.GetId()))
                {
                    ((PostViewItem)MyPostDict[post.GetId()]).ReloadPost(post);
                }
            }
        }

        public void ReloadSupplier(int supplierId)
        {
            if (MySupplierDict.ContainsKey(supplierId))
            {
                foreach (PostViewItem viewItem in MySupplierDict[supplierId])
                {
                    viewItem.ReloadSupplier();
                }
            }
        }

        public void ReloadSupplier(Supplier supplier)
        {
            if (IsNotNull(supplier) && MySupplierDict.ContainsKey(supplier.GetId()))
            {
                foreach (PostViewItem viewItem in MySupplierDict[supplier.GetId()])
                {
                    viewItem.ReloadSupplier(supplier);
                }
            }
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            if (IsNotNull(merchandise) && MyProdDict.ContainsKey(merchandise.GetId()))
            {
                foreach (PostViewItem viewItem in MyProdDict[merchandise.GetId()])
                {
                    viewItem.ReloadMerchandise(merchandise);
                }
            }
        }

        private void UpdateDictionaries()
        {
            int supplierId, merchId, postId;
            foreach (PostViewItem viewItem in MyAddListViewItems)
            {
                if (viewItem == null)
                {
                    continue;
                }
                supplierId = viewItem.GetPost().GetSupplierId();
                if (MySupplierDict.ContainsKey(supplierId))
                {
                    MySupplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    MySupplierDict.Add(supplierId, new List<PostViewItem> { viewItem });
                }
                
                merchId = viewItem.GetPost().GetMerchandiseId();
                if (MyProdDict.ContainsKey(merchId))
                {
                    MyProdDict[merchId].Add(viewItem);
                }
                else
                {
                    MyProdDict.Add(merchId, new List<PostViewItem> { viewItem });
                }

                postId = viewItem.GetPost().GetId();
                if (MyPostDict.ContainsKey(postId))
                {
                    MyPostDict[postId] = viewItem;
                }
                else
                {
                    MyPostDict.Add(postId, viewItem);
                }
            }        
        }

        public override void BeginLoadItems(int itemCount)
        {
            MyProdDict = new Dictionary<int, List<PostViewItem>>();
            MySupplierDict = new Dictionary<int, List<PostViewItem>>();
            MyProdDict = new Dictionary<int, List<PostViewItem>>();
            base.BeginLoadItems(itemCount);
        }

        public override void EndAddItems()
        {
            UpdateDictionaries();
            base.EndAddItems();
        }

        public override void EndLoadItems()
        {
            UpdateDictionaries();
            base.EndLoadItems();
        }

        public override void EndLoadChunk()
        {
            UpdateDictionaries();
            base.EndLoadChunk();
        }

        public override void InitList()
        {
            base.InitList();
            this.ListViewItemSorter = new ListViewComparerDefault();
        }

        public override void ResetSortOrder()
        {
            this.ListViewItemSorter = new ListViewComparerDefault();
            MySortColumnIndex = NO_COLUMN_INDEX;
        }


        private class ListViewComparerDefault : ListViewComparerChiasma
        {
            public ListViewComparerDefault()
                : base()
            {

            }

            public override int Compare(object object1, object object2)
            {
                PostViewItem listViewItem1, listViewItem2;
                Post post1, post2;

                listViewItem1 = (PostViewItem)object1;
                listViewItem2 = (PostViewItem)object2;

                post1 = listViewItem1.GetPost();
                post2 = listViewItem2.GetPost();


                if (post1 != null && post2 != null)
                {
                    if (post1 > post2)
                    {
                        return 1;
                    }
                    else if (post1 == post2)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    // post1 and/or post2 is null
                    if (post1 != null && post2 == null)
                    {
                        return 1;
                    }
                    else if (post1 == null && post2 != null)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

    }



    public class PostViewItem : ListViewItem
    {
        private Post MyPost;
        private enum ListIndex : int
        {
            BookDate = 0,
            Booker = 1,
            Product = 2,
            ArtNr = 3,
            Amount = 4,
            Supplier = 5,
            InvoiceCategoryCode = 6,
            ApprPrize = 7,
            FinalPrize = 8,
            TotalPrize = 9,
            ApprArrival = 10,
            InvoiceInst = 11,
            InvoiceClin = 12,
            InvoiceNumber = 13,
            InvoiceStatus = 14,
            DeliveryDeviation = 15,
            OrderDate = 16,
            OrderSign = 17,
            ArrivalDate = 18,
            ArrivalSign = 19,
            InvoiceDate = 20,
            InvoiceSender = 21,
            PurchaseOrderNo = 22,
            SalesOrderNo = 23,
            Comment = 24
        }
        public PostViewItem(Post post)
            : base("")
        {
            DataRow[] rows;
            string sort, colName;
            PostListViewColumn col;
            MyPost = post;
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " asc";

            // Loop through columns in personal config datatable
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            for (int i = 0; i < rows.Length; i++)
            {
                colName = (string)rows[i][Configuration.PostListViewConfColumns.ColEnumName.ToString()];
                col = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colName);
                if (i == 0)
                {
                    this.Text = post.GetStringForListViewColumn(col);
                }
                else
                { 
                    this.SubItems.Add(post.GetStringForListViewColumn(col));
                }
            }
            //this.UseItemStyleForSubItems = false;
            SetStatusColor();

        }

        public void ReloadPost(Post post)
        {
            MyPost = post;
            UpdateViewItem();
        }

        public void ReloadSupplier()
        {
            MyPost.ResetSupplierLocal();
            UpdateViewItem();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MyPost.ReloadSupplier(supplier);
            UpdateViewItem();
        }

        public void ReloadMerchandise()
        {
            MyPost.ResetMerchandiseLocal();
            UpdateViewItem();
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            MyPost.ReloadMerchandise(merchandise);
            UpdateViewItem();
        }

        public Post GetPost()
        {
            return MyPost;
        }

        private void SetStatusColor()
        {
            switch (MyPost.GetPostStatus())
            {
                case Post.PostStatus.Booked:
                    this.BackColor = Color.LightCoral;
                    this.ForeColor = Color.Black;
                    break;
                case Post.PostStatus.Ordered:
                    this.BackColor = Color.Yellow;
                    this.ForeColor = Color.Black;
                    break;
                case Post.PostStatus.ConfirmedOrder:
                    this.BackColor = Color.LightBlue;
                    this.ForeColor = Color.Black;
                    break;
                case Post.PostStatus.Confirmed:
                    this.BackColor = Color.Lime;
                    this.ForeColor = Color.Black;
                    break;
                case Post.PostStatus.Completed:
                    if (MyPost.GetInvoiceStatus() == Post.InvoiceStatus.NotOk)
                    {
                        this.BackColor = Color.Black;
                        this.ForeColor = Color.LightSalmon;
                    }
                    else
                    {
                        this.BackColor = Color.White;
                        this.ForeColor = Color.Black;
                    }
                    break;
            }
            if (!MyPost.IsMerchandiseEnabled())
            {
                this.ForeColor = Color.Red;
                this.ToolTipText = "This product is not up to date";
            }
        }

        protected bool IsNotEmpty(string str)
        {
            return str != null && str.Length > 0;
        }

        public void UpdateViewItem()
        {
            DataRow[] rows;
            string sort, colName;
            PostListViewColumn col;
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " asc";

            // Loop through columns in personal config datatable
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            for (int i = 0; i < rows.Length; i++)
            {
                colName = (string)rows[i][Configuration.PostListViewConfColumns.ColEnumName.ToString()];
                col = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colName);
                
                this.SubItems[(int)rows[i][Configuration.PostListViewConfColumns.ColSortOrder.ToString()]].Text = 
                    MyPost.GetStringForListViewColumn(col);
            }
            SetStatusColor();
        }
    }

}
