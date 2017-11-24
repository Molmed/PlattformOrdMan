using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;
using Molmed.PlattformOrdMan.UI.Component;
using PlattformOrdMan.Properties;
using Molmed.PlattformOrdMan.UI.Controller;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowOrderHistoryDialog : OrdManForm, ISupplierForm, IMerchandiseForm, IPostForm
    {
        private PostList MyPosts;
        private Dictionary<int, PostList> MySupplierDict;
        private Dictionary<int, PostList> MyProdDict;
        private ToolTipHandler MyToolTipHandler;
        private const String DELETE = "Delete";
        private const String UPDATE = "Update ...";
        private const String LOCK_COLUMN_WIDTH = "Lock column width";
        private const String UN_LOCK_COLUMN_WIDTH = "Un-lock column width";
        private const String ORDER_POST = "Sign order ...";
        private const String CONFIRM_ORDER = "Set order confirmed";
        private const String CONFIRM_ARRIVAL = "Confirm arrival";
        private const String SET_ORDER_NR_SO = "Set Sales Order No (SO)";
        private const String SUPPLIER = "Supplier ...";
        private const String MERCHANDISE = "Product ...";
        private const String SIGN_INVOICE_OK_AND_SENT = "Sign invoice Ok and sent";
        private const String SIGN_INVOICE_NOT_OK = "Sign invoice NOT Ok";
        private const String SIGN_INVOICE_ABSENT = "Sign invoice absent";
        private const String REGRET_ORDER_POST = "Regret Order post";
        private const String REGRET_CONFRIRM_ORDER = "Regret order confirmal";
        private const String REGRET_ARRIVAL_CONFIRMATION = "Regret Arrival confirmation";
        private const String REGRET_INVOICE_OK_AND_SENT = "Regret Invoice OK and Sent";
        private const String REGRET_INVOICE_NOT_OK = "Regret Invoice not OK";
        private const String REGRET_INVOICE_ABSENT = "Regret Invoice absent";
        private const String REGRET_COMPLETED = "Regret Completed";
        private const String RESET_INVOICE_STATUS = "Reset status";
        private const String SET_INVOICE_NUMBER = "Set invoice number ...";
        private const String CREATE_ORDER_FROM_PRODUCT = "Create new order ...";
        private const String FREE_TEXT_SEARCH = "Free text search ...";
        public ShowOrderHistoryDialog()
        {
            InitializeComponent();
            Init();

        }

        private void Init()
        {
            DateTime start = DateTime.Now;
            SupplierList suppliers;
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
            suppliers = SupplierManager.GetSuppliersFromCache();
            SupplierCombobox.Init(suppliers, "supplier", true);
            SupplierCombobox.LoadIdentitiesWithInfoText();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
            merchandiseCombobox1.Init(true, false);
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            PostOrderInfoLabel.BackColor = Color.LightCoral;
            ProductArrivalLabel.BackColor = Color.Yellow;
            ProductOrderConfirmedLabel.BackColor = Color.LightBlue;
            CompletedPostPanel.BackColor = Color.White;
            InvoiceNotCheckedPanel.BackColor = Color.Lime;
            userComboBox1.Init(true, "booker");
            userComboBox1.LoadIdentitiesWithInfoText();
            userComboBox1.OnMyControlledSelectedIndexChanged += 
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(userComboBox1_OnMyControlledSelectedIndexChanged);
            merchandiseCombobox1.Enabled = true;
            RestoreSortingButton.Enabled = false;
            LoadPosts();
            InitListView();
            SupplierCombobox.OnMyControlledSelectedIndexChanged += 
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(SupplierCombobox_OnMyControlledSelectedIndexChanged);
            merchandiseCombobox1.OnMyControlledSelectedIndexChanged += 
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(merchandiseCombobox1_OnMyControlledSelectedIndexChanged);
            FreeTextSearchTextBox.Enter += new EventHandler(FreeTextSearchTextBox_Enter);
            this.FormClosing += ShowOrderHistoryDialog_FormClosing;
            //MessageBox.Show(DateTime.Now.Subtract(start).Milliseconds.ToString() + " ms");
        }

        public override void ReloadForm()
        {
            SupplierList suppliers;
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
            suppliers = SupplierManager.GetSuppliersFromCache();
            SupplierCombobox.LoadIdentitiesWithInfoText();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            userComboBox1.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.Enabled = true;
            RestoreSortingButton.Enabled = false;
            LoadPosts();
            UpdateListView();
        }

        private void ShowOrderHistoryDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UpdateLockedColumnWidths();
            }
            catch (Exception ex)
            {
                HandleError("Error when closing form", ex);
            }
        }

        public bool HasPostLoaded(int postId)
        {
            return IsNotNull(MyPosts.GetById(postId));
        }

        public void ReloadPost(Post post)
        {
            int index;
            PostsListView.ReloadPost(post);
            index = MyPosts.GetIndex(post);
            MyPosts[index] = post;
        }

        public void AddCreatedPost(Post post)
        {
            MyPosts.Add(post);
            PostsListView.AddViewItem(post);
            PostsListView.SelectedIndices.Clear();
            PostsListView.Items[0].Selected = true;
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return MySupplierDict.ContainsKey(supplierId);
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return MyProdDict.ContainsKey(merchandiseId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            PostsListView.ReloadSupplier(supplier);
            if (IsNotNull(supplier) && MySupplierDict.ContainsKey(supplier.GetId()))
            {
                foreach (Post post in MySupplierDict[supplier.GetId()])
                {
                    if (post.GetSupplierId() != supplier.GetId())
                    {
                        throw new Data.Exception.DataException("Supplier id mismatch!");
                    }
                    post.ReloadSupplier(supplier);
                }
            }
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            PostsListView.ReloadMerchandise(merchandise);
            if (IsNotNull(merchandise) && MyProdDict.ContainsKey(merchandise.GetId()))
            {
                foreach (Post post in MyProdDict[merchandise.GetId()])
                {
                    if (post.GetMerchandiseId() != merchandise.GetId())
                    {
                        throw new Data.Exception.DataException("Merchandise id mismatch!");
                    }
                    post.ReloadMerchandise(merchandise);
                }
            }
        }

        public void AddCreatedSupplier(Supplier supplier)
        {
            SupplierCombobox.AddCreatedSupplier(supplier);
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        {
            merchandiseCombobox1.AddCreatedMerchandise(merchandise);
        }

        private void ShowOrderHistoryDialog_Shown(object sender, EventArgs e)
        {
            //UpdateListView3();
        }

        private void LoadViewItemsTimeMeasure()
        {
            DateTime time1, time2;
            string str;
            PostViewItem[] pwi;
            int ind = 0;
            time1 = new DateTime();
            time2 = new DateTime();
            time1 = DateTime.Now;
            pwi = new PostViewItem[MyPosts.Count];
            foreach (Post post in MyPosts)
            {
                pwi[ind++] = new PostViewItem(post);
            }

            time2 = DateTime.Now;
            str = "LoadViewItems: " + ((time2.Ticks - time1.Ticks) / 10000).ToString() + "ms";
            MessageBox.Show(str, "", MessageBoxButtons.OK);
        }

        private void LoadPosts()
        {
            DateTime today;
            PostList tmpPosts, loadedPosts;
            int monthsBack, i = 0;
            bool timeRestrToCompletedPostsOnly;
            today = DateTime.Now;
            monthsBack = PlattformOrdManData.Configuration.TimeIntervalForPosts;
            timeRestrToCompletedPostsOnly = PlattformOrdManData.Configuration.TimeRestrictionForCompletedPostsOnly;
            if (monthsBack == PlattformOrdManData.NO_COUNT)
            {
                loadedPosts = PostManager.GetPosts(today, false, timeRestrToCompletedPostsOnly);
            }
            else
            {
                loadedPosts = PostManager.GetPosts(today.AddMonths(-monthsBack), true, timeRestrToCompletedPostsOnly);                
            }
            MyPosts = new PostList();
            MySupplierDict = new Dictionary<int, PostList>();
            MyProdDict = new Dictionary<int, PostList>();
            foreach (Post post in loadedPosts)
            {
                if (PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Contains(post.GetPlaceOfPurchase().ToString()))
                {
                    MyPosts.Add(post);
                    if (!MySupplierDict.ContainsKey(post.GetSupplierId()) && 
                        post.GetSupplierId() != PlattformOrdManData.NO_ID)
                    {
                        tmpPosts = new PostList();
                        tmpPosts.Add(post);
                        MySupplierDict.Add(post.GetSupplierId(), tmpPosts);
                    }
                    else if(post.GetSupplierId() != PlattformOrdManData.NO_ID)
                    {
                        MySupplierDict[post.GetSupplierId()].Add(post);
                    }

                    if (!MyProdDict.ContainsKey(post.GetMerchandiseId()) &&
                        post.GetMerchandiseId() != PlattformOrdManData.NO_ID)
                    {
                        tmpPosts = new PostList();
                        tmpPosts.Add(post);
                        MyProdDict.Add(post.GetMerchandiseId(), tmpPosts);
                    }
                    else if (post.GetMerchandiseId() != PlattformOrdManData.NO_ID)
                    {
                        MyProdDict[post.GetMerchandiseId()].Add(post);
                    }
                }
            }
            MyPosts.Sort();
        }

        private void userComboBox1_OnMyControlledSelectedIndexChanged()
        {
            FilterPosts();
        }


        private void merchandiseCombobox1_OnMyControlledSelectedIndexChanged()
        {
            FilterPosts();        
        }

        void FreeTextSearchTextBox_Enter(object sender, EventArgs e)
        {
            if (FreeTextSearchTextBox.Text == FREE_TEXT_SEARCH)
            {
                FreeTextSearchTextBox.Text = "";
            }
        }

        private void InitListView()
        {
            DateTime start = DateTime.Now;
            string sort;
            DataRow[] rows;
            string colHeader, colEnumName;
            int colWidth;
            PostListViewColumn postListViewColumn;
            OrderManListView.ListDataType listDataType;
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " ASC";
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);

            // Add columns to post according to personal configuration
            PostsListView.BeginUpdate();
            foreach (DataRow row in rows)
            { 
                colEnumName = row[Configuration.PostListViewConfColumns.ColEnumName.ToString()].ToString();
                postListViewColumn = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colEnumName);
                colHeader = PostListView.GetColumnHeaderName(postListViewColumn);
                colWidth = (int)row[Configuration.PostListViewConfColumns.ColWidth.ToString()];
                listDataType = PostListView.GetListDataType(postListViewColumn);
                PostsListView.AddColumn(colHeader, colWidth, listDataType);
            }
            PostsListView.EndUpdate();

            AddMenuItem(PostsListView, LOCK_COLUMN_WIDTH, LockColumnWidth_Click);
            AddMenuItem(PostsListView, UN_LOCK_COLUMN_WIDTH, UnlockColumnWidth_Click);
            AddMenuItem(PostsListView, UPDATE, UpdateMenuItem_Click);
            AddMenuItem(PostsListView, ORDER_POST, OrderPostMenuItem_Click);
            AddMenuItem(PostsListView, CONFIRM_ORDER, ConfirmOrderMenuItem_Click);
            AddMenuItem(PostsListView, CONFIRM_ARRIVAL, ConfirmArrivalMenuItem_Click);
            AddMenuItem(PostsListView, SET_ORDER_NR_SO, SetSalesOrderNo_Click);
            AddMenuItem(PostsListView, REGRET_ORDER_POST, RegretOrderPost);
            AddMenuItem(PostsListView, REGRET_CONFRIRM_ORDER, RegretOrderConfirmation);
            AddMenuItem(PostsListView, SIGN_INVOICE_OK_AND_SENT, SignInvoiceOkAndSentMenuItem_Click);
            AddMenuItem(PostsListView, SIGN_INVOICE_NOT_OK, SignInvoiceNotOk_Click);
            AddMenuItem(PostsListView, SIGN_INVOICE_ABSENT, SingInvoiceAbsent_Click);
            AddMenuItem(PostsListView, REGRET_ARRIVAL_CONFIRMATION, RegretArrivalConfirmation);
            AddMenuItem(PostsListView, REGRET_INVOICE_NOT_OK, RegretInvoiceNotOK);
            AddMenuItem(PostsListView, REGRET_INVOICE_OK_AND_SENT, RegretInvoiceOKAndSent);
            AddMenuItem(PostsListView, REGRET_COMPLETED, RegretInvoiceOKAndSent);
            AddMenuItem(PostsListView, RESET_INVOICE_STATUS, ResetInvoiceStatusMenuItem_Click);
            AddMenuItem(PostsListView, SET_INVOICE_NUMBER, SetInvoiceNumberMenuItem_Click);
            AddMenuItem(PostsListView, CREATE_ORDER_FROM_PRODUCT, CreatePostFromSameProductMenuItem_Click);
            AddMenuItem(PostsListView, DELETE, DeleteMenuItem_Click);
            AddMenuItem2(PostsListView, "sep", null, true);
            AddMenuItem(PostsListView, MERCHANDISE, MerchandiseMenuItem_Click);
            AddMenuItem(PostsListView, SUPPLIER, SupplierMenuItem_Click);
            AddMenuItem2(PostsListView, "sep", null, true);
            new CopyListViewMenu(PostsListView);
            PostsListView.DoubleClick += new EventHandler(UpdateMenuItem_Click);
            PostsListView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
            PostsListView.OnSortOrderSet += new OrderManListView.SortOrderSet(PostsListView_OnSortOrderSet);
            PostsListView.ShowItemToolTips = false;
            PostsListView.ItemMouseHover += PostsListView_ItemMouseHover;

            UpdateListView();
        }

        private void ReInitListView()
        { 
            // Updates columns and rows only
            string sort;
            DataRow[] rows;
            string colHeader, colEnumName;
            int colWidth;
            PostListViewColumn postListViewColumn;
            OrderManListView.ListDataType listDataType;
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " ASC";
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);

            // Add columns to post according to personal configuration
            PostsListView.Clear();
            PostsListView.BeginUpdate();
            foreach (DataRow row in rows)
            {
                colEnumName = row[Configuration.PostListViewConfColumns.ColEnumName.ToString()].ToString();
                colWidth = (int)row[Configuration.PostListViewConfColumns.ColWidth.ToString()];
                postListViewColumn = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colEnumName);
                listDataType = PostListView.GetListDataType(postListViewColumn);
                colHeader = PostListView.GetColumnHeaderName(postListViewColumn);
                PostsListView.AddColumn(colHeader, colWidth, listDataType);
            }
            PostsListView.EndUpdate();
            UpdateListView();
            ReInitAllColumnWidths();
        }

        private void SetInvoiceNumberMenuItem_Click(object sender, EventArgs e)
        {
            string invoiceNumber;
            bool noInvoice;
            SetInvoiceNumberDialog setInvoiceNumberDialog;
            PostList posts;
            int supplierId, customerNumberId;
            GroupCategory commonGroup;
            bool hasCommonGroup = true, hasCommonSuplier = true;
            try
            {
                posts = new PostList();
                foreach (PostViewItem viewItem in PostsListView.SelectedItems)
                {
                    posts.Add(viewItem.GetPost());

                }

                if (IsEmpty(posts))
                {
                    return;
                }
                
                supplierId = posts[0].GetSupplierId();
                commonGroup = posts[0].GetGroupCategory();
                foreach (Post post in posts)
                {
                    if (post.GetSupplierId() != supplierId)
                    {
                        hasCommonSuplier = false;
                        break;
                    }
                    if (post.GetGroupCategory() != commonGroup)
                    {
                        hasCommonGroup = false;
                    }
                }

                if (!hasCommonSuplier)
                {
                    MessageBox.Show("Selected posts have different suppliers, it's not possible to set a common invoice number!", "Invoice number error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!hasCommonGroup)
                {
                    MessageBox.Show("Selected posts are assigned to different groups (Plattform or Research). Customer numbers for both groups are eligible",
                        "Posts from both groups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                setInvoiceNumberDialog = new SetInvoiceNumberDialog(posts);
                if (setInvoiceNumberDialog.ShowDialog() == DialogResult.OK)
                {
                    invoiceNumber = setInvoiceNumberDialog.InvoiceNumber;
                    noInvoice = setInvoiceNumberDialog.NoInvoice;
                    customerNumberId = setInvoiceNumberDialog.CustomerNumberId;
                    foreach (Post post in posts)
                    {
                        post.UpdateInvoiceNumber(invoiceNumber, customerNumberId, noInvoice);
                    }
                    RedrawPosts(posts);
                }
            }
                        catch (Exception ex)
            {
                HandleError("Error when setting invoice number", ex);
            }
        }

        void PostsListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            if (IsNotEmpty(e.Item.ToolTipText))
            {
                MyToolTipHandler = new ToolTipHandler(this, e.Item.ToolTipText);
                MyToolTipHandler.StartTimer();
            }
        }

        private void RegretOrderPost(object sender, EventArgs e)
        {
            foreach (Post post in GetSelectedPosts())
            {
                post.RegretOrderPost();
            }
            RedrawPosts(GetSelectedPosts());
        }

        private void RegretOrderConfirmation(object sender, EventArgs e)
        {
            foreach (Post post in GetSelectedPosts())
            {
                post.RegretOrderConfirmed();
            }
            RedrawPosts(GetSelectedPosts());
        }

        private void RegretArrivalConfirmation(object sender, EventArgs e)
        {
            foreach (Post post in GetSelectedPosts())
            {
                post.RegretArrivalConfirmation();
            }
            RedrawPosts(GetSelectedPosts());
        }

        private void RegretInvoiceOKAndSent(object sender, EventArgs e)
        {
            foreach (Post post in GetSelectedPosts())
            {
                post.RegretCompleted();
            }
            RedrawPosts(GetSelectedPosts());
            RestoreSortingButton.Enabled = true;
        }

        private void RegretInvoiceNotOK(object sender, EventArgs e)
        {
            foreach (Post post in GetSelectedPosts())
            {
                post.RegretCompleted();
            }
            RedrawPosts(GetSelectedPosts());
        }

        private void PostsListView_OnSortOrderSet(object sender, EventArgs e)
        {
            RestoreSortingButton.Enabled = true;
        }

        private void UnlockColumnWidth(int colIndex)
        {
            DataRow[] rows;
            string expr;
            expr = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " = " + colIndex.ToString();
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()] = 
                    PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
                ReInitColumnWidth(colIndex, true);
            }
            else
            { 
                throw new Data.Exception.DataException("Column sort order mis-match: "  + colIndex.ToString());
            }
        }

        private void LockColumnWidth(int colIndex)
        {
            DataRow[] rows;
            string expr;
            expr = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " = " + colIndex.ToString();
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()] =
                    PostsListView.Columns[colIndex].Width;
            }
            else
            {
                throw new Data.Exception.DataException("Column sort order mis-match: " + colIndex.ToString());
            }            
        }

        private void UpdateLockedColumnWidths()
        {
            DataRow[] rows;
            string expr;
            int currentConfWidth;
            for (int colIndex = 0; colIndex < PostsListView.Columns.Count; colIndex++)
            {
                expr = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " = " + colIndex.ToString();
                rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
                if (rows.Length == 1)
                {
                    currentConfWidth = (int)rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()];
                    if (currentConfWidth > 0)
                    {
                        rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()] =
                            PostsListView.Columns[colIndex].Width;
                    }
                }
                else
                {
                    throw new Data.Exception.DataException("Column sort order mis-match: " + colIndex.ToString());
                }
            }        
        }

        private int GetClickedColumnHeaderIndex(out bool isFixedWidth)
        { 
            // Used to check if user right-clicked on a column header, and if so
            // determine which column was clicked
            Point mousePosScreen;
            Rectangle postListViewRect;
            Point postListViewStart;
            Rectangle firstItemRect;
            isFixedWidth = false;
            mousePosScreen = MousePosition;
            postListViewRect = PostsListView.Bounds;
            postListViewStart = PostsListView.PointToScreen(new Point(0, 0));
            if(PostsListView.Items.Count == 0)
            {
                return PlattformOrdManData.NO_COUNT;
            }

            if (PostsListView.Items[0].SubItems.Count == 1)
            {
                firstItemRect = PostsListView.RectangleToScreen(PostsListView.Items[0].SubItems[0].Bounds);
                if (mousePosScreen.Y >= postListViewStart.Y &&
                    mousePosScreen.Y <= firstItemRect.Y)
                {
                    isFixedWidth = HasColumnFixedWidthInConfig(0);
                    return 0;
                }
            }
            else
            {
                firstItemRect = PostsListView.RectangleToScreen(PostsListView.Items[0].SubItems[1].Bounds);
                if (mousePosScreen.Y >= postListViewStart.Y &&
                    mousePosScreen.Y <= firstItemRect.Y)
                {
                    if (mousePosScreen.X < firstItemRect.Left)
                    {
                        isFixedWidth = HasColumnFixedWidthInConfig(0);
                        return 0;
                    }
                    for (int i = 1; i < PostsListView.Items[0].SubItems.Count; i++)
                    {
                        firstItemRect = PostsListView.Items[0].SubItems[i].Bounds;
                        firstItemRect = PostsListView.RectangleToScreen(firstItemRect);
                        if (mousePosScreen.X >= firstItemRect.Left &&
                            mousePosScreen.X < firstItemRect.Right)
                        {
                            isFixedWidth = HasColumnFixedWidthInConfig(i);
                            return i;
                        }
                    }
                }
            }
            return PlattformOrdManData.NO_COUNT;
        }

        private bool HasColumnFixedWidthInConfig(int colInd)
        { 
            string expr;
            DataRow[] rows;
            int colWidth = -1;
            expr = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " = " + colInd.ToString();
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                colWidth = (int)rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()];
            }
            else
            {
                throw new Data.Exception.DataException("Column index mis-match: " + colInd.ToString());
            }
            return colWidth > 0;
        }

        private void SetColumnConfMenus(object sender)
        {
            int colInd;
            bool isFixedColWidth;

            SetVisible(sender, LOCK_COLUMN_WIDTH, false);
            SetVisible(sender, UN_LOCK_COLUMN_WIDTH, false);
            colInd = GetClickedColumnHeaderIndex(out isFixedColWidth);
            if (colInd > PlattformOrdManData.NO_COUNT)
            {
                SetVisible(sender, LOCK_COLUMN_WIDTH, true);
                SetVisible(sender, UN_LOCK_COLUMN_WIDTH, true);
                SetTag(sender, LOCK_COLUMN_WIDTH, colInd);
                SetTag(sender, UN_LOCK_COLUMN_WIDTH, colInd);
                if (isFixedColWidth)
                {
                    SetEnable(sender, LOCK_COLUMN_WIDTH, false);
                    SetEnable(sender, UN_LOCK_COLUMN_WIDTH, true);
                }
                else
                {
                    SetEnable(sender, LOCK_COLUMN_WIDTH, true);
                    SetEnable(sender, UN_LOCK_COLUMN_WIDTH, false);
                }
            }            
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetColumnConfMenus(sender);

            if (IsNull(GetSelectedPost()))
            {
                SetVisible(sender, ORDER_POST, false);
                SetVisible(sender, CONFIRM_ORDER, false);
                SetVisible(sender, SET_ORDER_NR_SO, false);
                SetVisible(sender, CONFIRM_ARRIVAL, false);
                SetVisible(sender, UPDATE, false);
                SetVisible(sender, DELETE, false);
                SetVisible(sender, SIGN_INVOICE_OK_AND_SENT, false);
                SetVisible(sender, SIGN_INVOICE_NOT_OK, false);
                SetVisible(sender, SIGN_INVOICE_ABSENT, false);
                SetVisible(sender, MERCHANDISE, false);
                SetVisible(sender, SUPPLIER, false);
                SetVisible(sender, RESET_INVOICE_STATUS, false);
                SetVisible(sender, "sep", false);
                SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, false);
                SetVisible(sender, REGRET_CONFRIRM_ORDER, false);
                SetVisible(sender, REGRET_INVOICE_NOT_OK, false);
                SetVisible(sender, REGRET_INVOICE_OK_AND_SENT, false);
                SetVisible(sender, REGRET_COMPLETED, false);
                SetVisible(sender, REGRET_ORDER_POST, false);
                SetVisible(sender, CREATE_ORDER_FROM_PRODUCT, false);
                SetVisible(sender, SET_INVOICE_NUMBER, false);
                return;
            }
            SetVisible(sender, CREATE_ORDER_FROM_PRODUCT, true);
            SetEnable(sender, CREATE_ORDER_FROM_PRODUCT, false);
            SetVisible(sender, ORDER_POST, false);
            SetVisible(sender, CONFIRM_ORDER, false);
            SetVisible(sender, CONFIRM_ARRIVAL, false);
            SetVisible(sender, SIGN_INVOICE_OK_AND_SENT, false);
            SetVisible(sender, SIGN_INVOICE_NOT_OK, false);
            SetVisible(sender, SIGN_INVOICE_ABSENT, false);
            SetVisible(sender, RESET_INVOICE_STATUS, false);
            SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, false);
            SetVisible(sender, REGRET_INVOICE_NOT_OK, false);
            SetVisible(sender, REGRET_CONFRIRM_ORDER, false);
            SetVisible(sender, REGRET_INVOICE_OK_AND_SENT, false);
            SetVisible(sender, REGRET_COMPLETED, false);
            SetVisible(sender, REGRET_ORDER_POST, false);
            SetVisible(sender, SET_INVOICE_NUMBER, false);
            if (IsSelectedStatusOnlyBooked())
            {
                SetVisible(sender, ORDER_POST, true);
            }

            if (IsSelectedStatusOnlyOrdered())
            {
                SetVisible(sender, CONFIRM_ORDER, true);
                SetVisible(sender, REGRET_ORDER_POST, true);
            }

            if (IsSelectedStatusOnlyConfirmedOrder())
            {
                SetVisible(sender, CONFIRM_ARRIVAL, true);
                SetVisible(sender, REGRET_CONFRIRM_ORDER, true);
            }
            if (IsSelectedStatusOnlyConfirmed())
            {
                SetVisible(sender, SIGN_INVOICE_OK_AND_SENT, true);
                SetVisible(sender, SIGN_INVOICE_NOT_OK, true);
                SetVisible(sender, SIGN_INVOICE_ABSENT, true);
                SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, true);
            }

            if (IsSelectedStatusOnlyCompleted())
            {
                SetVisible(sender, RESET_INVOICE_STATUS, true);

                if (IsSelectedStatusOnlyInvoiceOkAndSent())
                {
                    SetVisible(sender, REGRET_INVOICE_OK_AND_SENT, true);                                    
                }
                else if (IsSelectedStatusOnlyInvoiceNotOk())
                {
                    SetVisible(sender, REGRET_INVOICE_NOT_OK, true);                                        
                }
                else
                {
                    SetVisible(sender, REGRET_COMPLETED, true);                                    
                }
            }

            SetVisible(sender, DELETE, true);
            if (IsSingleSelection())
            {
                SetVisible(sender, UPDATE, true);
                SetVisible(sender, MERCHANDISE, true);
                SetVisible(sender, SUPPLIER, true);
                SetVisible(sender, "sep", true);
                SetEnable(sender, CREATE_ORDER_FROM_PRODUCT, true);
            }
            else
            {
                SetVisible(sender, UPDATE, false);
                SetVisible(sender, MERCHANDISE, false);
                SetVisible(sender, SUPPLIER, false);
                SetVisible(sender, "sep", false);
            }
            if (UserManager.GetCurrentUser().HasAdministratorRights())
            {
                SetVisible(sender, SET_INVOICE_NUMBER, true);
                SetVisible(sender, SET_ORDER_NR_SO, true);
            }

        }

        private bool IsSelectedStatusOnlyConfirmed()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Confirmed)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyCompleted()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Completed)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyInvoiceOkAndSent()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Completed ||
                    pViewItem.GetPost().GetInvoiceStatus() != Post.InvoiceStatus.Ok ||
                    pViewItem.GetPost().IsInvoceAbsent())
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyInvoiceNotOk()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Completed ||
                    pViewItem.GetPost().GetInvoiceStatus() != Post.InvoiceStatus.NotOk)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyOrdered()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Ordered)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyBooked()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.Booked)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedStatusOnlyConfirmedOrder()
        {
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (pViewItem.GetPost().GetPostStatus() != Post.PostStatus.ConfirmedOrder)
                {
                    return false;
                }
            }
            return true;
        }


        private bool IsSingleSelection()
        {
            return PostsListView.SelectedItems.Count == 1;
        }

        private void SetSalesOrderNo_Click(object sender, EventArgs e)
        {
            Post tmpPost;
            PostList posts = new PostList();
            GetValueDialog getValueDialog;
            try
            {
                getValueDialog = new GetValueDialog(SET_ORDER_NR_SO, "Enter a sales order number, please.", "");
                if (getValueDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                foreach (PostViewItem viewItem in PostsListView.SelectedItems)
                {
                    tmpPost = viewItem.GetPost();
                    posts.Add(tmpPost);
                    tmpPost.SetSalesOrderNo(getValueDialog.GetText());
                }
                RedrawPosts(posts);
            }
            catch (Exception ex)
            {
                HandleError("Error when setting sales order number", ex);
            }
        }

        private void ConfirmArrivalMenuItem_Click(object sender, EventArgs e)
        {
            Post tmpPost;
            PostList posts = new PostList();
            try
            {
                if (HasAnySelectedPostUnHandledCustomerNumber() &&
                    MessageBox.Show("Customer number has not been chosen for at least one post, continue anyway?",
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    tmpPost = pViewItem.GetPost();
                    posts.Add(tmpPost);
                    tmpPost.ConfirmPostArrival(UserManager.GetCurrentUser().GetId());
                    if (tmpPost.IsInvoceAbsent())
                    {
                        tmpPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok, tmpPost.IsInvoceAbsent());
                    }
                }
                RedrawPosts(posts);
            }
            catch (Exception ex)
            {
                HandleError("Error when changing post status", ex);
            }

        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            PostList posts = new PostList();
            String str;
            try
            {
                str = "Are you sure to delete the " + PostsListView.SelectedItems.Count + " items?";
                if (MessageBox.Show(str, "Delete posts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                foreach (PostViewItem postViewItem in PostsListView.SelectedItems)
                {
                    posts.Add(postViewItem.GetPost());
                }
                PostManager.DeletePosts(posts);
                foreach (Post post in posts)
                {
                    RemovePostFromList(post);
                }
                FilterPosts();
            }
            catch (Exception ex)
            {
                HandleError("Error when deleting posts!", ex);
            }

        }


        private void RemovePostFromList(Post post)
        {
            MyPosts.Remove(post);
        }

        private void SupplierMenuItem_Click(object sender, EventArgs e)
        {
            EditSupplierDialog editSupplierDialog;
            try
            {
                editSupplierDialog = new EditSupplierDialog(GetSelectedPost().GetSupplier(), UpdateMode.Edit);
                editSupplierDialog.MdiParent = this.MdiParent;
                editSupplierDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }

        }

        private void editMerchandiseDialog_OnProductUpdated(object sender, EventArgs e)
        {
            RefreshListView();
            this.Select();
        }

        private void MerchandiseMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EditMerchandiseDialog editMerchandiseDialog;
                editMerchandiseDialog = new EditMerchandiseDialog(GetSelectedPost().GetMerchandise(), UpdateMode.Edit);
                editMerchandiseDialog.MdiParent = this.MdiParent;
                editMerchandiseDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error showing product", ex);
            }

        }

        private void NewOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                CreatePostDialog createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create);
                createPostDialog.MdiParent = this.MdiParent;
                createPostDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when creating new order", ex);
            }
        }

        private void LockColumnWidth_Click(object sender, EventArgs e)
        {
            int colInd;
            string colName;
            try
            {
                colInd = (int)((ToolStripMenuItem)sender).Tag;
                LockColumnWidth(colInd);
                colName = PostsListView.Columns[colInd].Text;
                MessageBox.Show("Column width for column ''" + colName + "'' will be saved between sessions!",
                    "Colum width locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HandleError("Error when locking column width", ex);
            }
        }

        private void UnlockColumnWidth_Click(object sender, EventArgs e)
        {
            int colInd;
            string colName;
            try
            {
                colInd = (int)((ToolStripMenuItem)sender).Tag;
                UnlockColumnWidth(colInd);
                colName = PostsListView.Columns[colInd].Text;
                MessageBox.Show("Column width for column ''" + colName + "'' is un-locked", "Column width un-locked",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HandleError("Error when un-locking column width", ex);
            }
        }

        private void UpdateMenuItem_Click(object sender, EventArgs e)
        {
            CreatePostDialog createPostDialog;
            try
            {
                if (IsNull(GetSelectedPost()))
                {
                    return;
                }
                createPostDialog = new CreatePostDialog(GetSelectedPost(), PostUpdateMode.Edit);
                createPostDialog.MdiParent = this.MdiParent;
                createPostDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error updating order", ex);
            }

        }

        private bool HasActiveArticleNumber(out ColumnHeader[] columnHeaders, out ListViewItem[] listViewItems)
        {
            PostList deviantPosts = new PostList();

            columnHeaders = new ColumnHeader[0];
            listViewItems = new ListViewItem[0];

            foreach (PostViewItem viewItem in PostsListView.SelectedItems)
            {
                if (!viewItem.GetPost().HasActiveArticleNumberSelected())
                {
                    deviantPosts.Add(viewItem.GetPost());
                }
            }
            if (deviantPosts.Count > 0)
            {
                SetDeviantPostViewItems(deviantPosts, out columnHeaders, out listViewItems);
                return false;
            }
            return true;
        }

        private void SetDeviantPostViewItems(PostList posts, out ColumnHeader[] columnHeaders, out ListViewItem[] listViewItems)
        {
            ListViewItem viewItem;
            int i = 0;
            columnHeaders = new ColumnHeader[4];
            listViewItems = new ListViewItem[posts.Count];
            columnHeaders[0] = new ColumnHeader();
            columnHeaders[0].Text = "Booker";
            columnHeaders[0].Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            columnHeaders[1] = new ColumnHeader();
            columnHeaders[1].Text = "Product";
            columnHeaders[1].Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            columnHeaders[2] = new ColumnHeader();
            columnHeaders[2].Text = "Current article number";
            columnHeaders[2].Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            columnHeaders[3] = new ColumnHeader();
            columnHeaders[3].Text = "Active article number";
            columnHeaders[3].Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            foreach (Post post in posts)
            {
                viewItem = new ListViewItem(post.GetBookerName());
                viewItem.SubItems.Add(post.GetMerchandiseName());
                viewItem.SubItems.Add(post.GetArticleNumberString());
                viewItem.SubItems.Add(post.GetMerchandise().GetActiveArticleNumberString());
                listViewItems[i++] = viewItem;
            }
        }

        private bool HasAnySelectedPostUnHandledCustomerNumber()
        {
            foreach (PostViewItem viewItem in PostsListView.SelectedItems)
            {
                if (!viewItem.GetPost().IsCustomerNumberHandled())
                {
                    return true;
                }
            }
            return false;
        }

        private void OrderPostMenuItem_Click(object sender, EventArgs e)
        {
            PostList posts;
            ListDialog listDialog;
            ColumnHeader[] columnHeaders;
            ListViewItem[] listViewItems;
            try
            {
                posts = new PostList();
                if (!HasActiveArticleNumber(out columnHeaders, out listViewItems))
                {
                    listDialog = new ListDialog(columnHeaders, listViewItems, "Deviant article number", ListDialog.DialogMode.AcceptanceList,
                        "The folliwing posts have another article number than what is currently active for \nthat product, proceed anyway?");
                    listDialog.SetRowLimitFilterStatus(false);
                    if (listDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }
                if (HasAnySelectedPostUnHandledCustomerNumber() &&
                    MessageBox.Show("Customer number has not been chosen for at least one post, continue anyway?", 
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    pViewItem.GetPost().OrderPost(UserManager.GetCurrentUser().GetId());
                    posts.Add(pViewItem.GetPost());
                }
                RedrawPosts(posts);
            }
            catch (Exception ex)
            {
                HandleError("Error when changing post status", ex);
            }

        }

        private void ConfirmOrderMenuItem_Click(object sender, EventArgs e)
        {
            PostList posts;
            posts = new PostList();
            try
            {
                if (HasAnySelectedPostUnHandledCustomerNumber() &&
                    MessageBox.Show("Customer number has not been chosen for at least one post, continue anyway?",
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    pViewItem.GetPost().ConfirmPostOrdered(UserManager.GetCurrentUser().GetId());
                    posts.Add(pViewItem.GetPost());
                }
                RedrawPosts(posts);
            }
            catch (Exception ex)
            {
                HandleError("Error when changing post status", ex);
            }

        }

        private void createPostDialog_OnPostUpdate(object sender, UpdateHandlerEventArgs e)
        {
            Post tmpPost;
            PostList posts;
            // Cases:
            // post is created
            // post is updated
            // post is ordered
            tmpPost = ((Post)sender);
            if (IsNull(MyPosts.GetById(tmpPost.GetId())))
            {
                // New post
                AddCreatedPost(tmpPost);
            }
            else
            {
                posts = new PostList();
                posts.Add(tmpPost);
                RedrawPosts(posts);                
            }

            if (e.NewSortOrder)
            {
                RestoreSortingButton.Enabled = true;
            }
        }

        private void RedrawPosts(PostList posts)
        {
            foreach (Post post in posts)
            {
                foreach (PostViewItem pViewItem in PostsListView.Items)
                {
                    if (pViewItem.GetPost().GetId() == post.GetId())
                    {
                        pViewItem.UpdateViewItem();
                        break;
                    }
                }
            }
        }

        private Post GetSelectedPost()
        {
            Post post = null;
            if (PostsListView.SelectedIndices.Count > 0 && PostsListView.SelectedIndices[0] > -1)
            {
                post = ((PostViewItem)PostsListView.SelectedItems[0]).GetPost();
            }
            return post;
        }

        private bool IsWithinFreeTextSearchCriteria(Post post)
        {
            string searchStr;
            searchStr = FreeTextSearchTextBox.Text.Trim();
            if (searchStr.Length > 0 &&
                searchStr != FREE_TEXT_SEARCH)
            {
                if (post.GetPurchaseOrderNo().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetSalesOrderNo().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetBookerName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetBookDate().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetSupplier() != null &&
                    post.GetSupplier().GetIdentifier().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetMerchandise() != null &&
                    post.GetMerchandise().GetIdentifier().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetMerchandise() != null &&
                    post.GetMerchandise().GetAmount().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetAmountString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetPriceWithCurrencyString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetOrdererName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetOrderDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalSignUserName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArticleNumberString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceCategoryCodeString2().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoicerUserName().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceStatusString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetComment().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetArrivalDateString().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
                if (post.GetInvoiceNumber().ToLower().Contains(searchStr.ToLower()))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        private bool IsWithinSearchCriteria(Post post)
        {
            if(SupplierCombobox.SelectedIndex >0 && 
                SupplierCombobox.GetSelectedIdentity().GetId() != post.GetSupplierId())
            {
                return false;
            }
            if (merchandiseCombobox1.SelectedIndex > 0 && post.GetMerchandise() != null &&
                merchandiseCombobox1.GetSelectedIdentity().GetId() != post.GetMerchandise().GetId())
            {
                return false;
            }
            if(userComboBox1.SelectedIndex > 0 && post.GetBooker() != null &&
                userComboBox1.GetSelectedIdentity().GetId() != post.GetBooker().GetId())
            {
                return false;
            }
            if (!IsWithinFreeTextSearchCriteria(post))
            {
                return false;
            }
            return true;

        }

        private void FilterPosts()
        {
            PostList filteredPosts = new PostList();
            foreach (Post post in MyPosts)
            {
                if (IsWithinSearchCriteria(post))
                {
                    filteredPosts.Add(post);
                }
            }
            PostsListView.BeginLoadItems(filteredPosts.Count);
            foreach (Post post in filteredPosts)
            {
                PostsListView.AddItem(new PostViewItem(post));
            }
            PostsListView.EndLoadItems();
        }

        private PostList GetSelectedPosts()
        {
            Post tmpPost;
            PostList posts = new PostList();
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                tmpPost = pViewItem.GetPost();
                posts.Add(tmpPost);
            }
            return posts;
        }

        private void SignInvoiceNotOk_Click(object sender, EventArgs e)
        {
            Post tmpPost;
            PostList posts = new PostList();
            try
            {
                if (IsInvoiceAbsent())
                {
                    return;
                }
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    tmpPost = pViewItem.GetPost();
                    tmpPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.NotOk, false);
                    posts.Add(tmpPost);
                }
                RedrawPosts(posts);
                RestoreSortingButton.Enabled = true;
            }
            catch (Exception ex)
            {
                HandleError("Error when changing post status", ex);
            }

        }

        private void SingInvoiceAbsent_Click(object sender, EventArgs e)
        {
            Post tmpPost;
            PostList posts = new PostList();
            try
            {
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    tmpPost = pViewItem.GetPost();
                    tmpPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok, true);
                    posts.Add(tmpPost);
                }
                RedrawPosts(posts);
                RestoreSortingButton.Enabled = true;
            }
            catch (Exception ex)
            {
                HandleError("Error when signing invoice absent", ex);
            }


        }

        private void ReInitColumnWidth(int colInd, bool updateHandling)
        {
            string expression;
            DataRow[] rows;
            int configWidth, colHeaderWidth;
            ColumnHeader col;
            col = PostsListView.Columns[colInd];
            expression = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " = " + colInd.ToString();
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
            try
            {
                if (rows.Length == 1)
                {
                    if (updateHandling)
                    {
                        PostsListView.BeginUpdate();
                    }
                    configWidth = (int)rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()];
                    if (configWidth == PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH)
                    {
                        col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                        colHeaderWidth = col.Width;
                        col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (colHeaderWidth > col.Width)
                        {
                            col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                        }
                    }
                }
                else
                {
                    throw new Data.Exception.DataException("Column name mismatch in configuration: " + col.Text);
                }
            }
            finally
            {
                if (updateHandling)
                {
                    PostsListView.EndUpdate();
                }            
            }
        }

        private void ReInitAllColumnWidths()
        { 
            // Loop through all columns
            // Check in config, which columns are fixed width or not
            // For not-fixed widths columns, check widest width between header and
            PostsListView.BeginUpdate();
            foreach (ColumnHeader col in PostsListView.Columns)
            {
                ReInitColumnWidth(col.Index, false);
            }
            PostsListView.EndUpdate();
        }

        private bool IsInvoiceAbsent()
        {
            MerchandiseList productsWithNoInvoiceCategory = new MerchandiseList();
            PostList posts = new PostList();
            String errStr;
            // Check that each product among the selected posts has an invoice number
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                if (IsNull(pViewItem.GetPost().GetMerchandise().GetInvoiceCategory()))
                {
                    productsWithNoInvoiceCategory.Add(pViewItem.GetPost().GetMerchandise());
                }
            }
            if (IsNotEmpty(productsWithNoInvoiceCategory))
            {
                errStr = "Please select invoice category for the following product(s):\n";
                foreach (Merchandise merchandise in productsWithNoInvoiceCategory)
                {
                    errStr += "\n" + merchandise.GetIdentifier();
                }
                MessageBox.Show(errStr, "Invoice category missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            return false;
        }

        private void SignInvoiceOkAndSentMenuItem_Click(object sender, EventArgs e)
        {
            Post tmpPost;
            PostList posts = new PostList();
            try
            {
                if (IsInvoiceAbsent())
                {
                    return;
                }
                if (HasAnySelectedPostUnHandledCustomerNumber() &&
                    MessageBox.Show("Customer number has not been chosen for at least one post, continue anyway?",
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    tmpPost = pViewItem.GetPost();
                    tmpPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok, false);
                    posts.Add(tmpPost);
                }
                RedrawPosts(posts);
                RestoreSortingButton.Enabled = true;
            }
            catch (Exception ex)
            {
                HandleError("Error when changing post status", ex);
            }
        }

        private void RefreshPosts()
        {
            ResetSearchFields();
            LoadPosts();
            MyPosts.Sort();
            UpdateListView();
        }

        private void RefreshPosts(Post post)
        {
            RefreshPosts();
            SelectPost(post);
        }

        private void RefreshPosts(PostList posts)
        {
            RefreshPosts();
            foreach (Post post in posts)
            {
                SelectPost(post, false);
            }
        }

        private void CreatePostFromSameProductMenuItem_Click(object sender, EventArgs e)
        {
            Post post;
            CreatePostDialog createPostDialog;
            Merchandise merchandice;
            bool isDoubtfulProd = false;
            try
            {
                if (PostsListView.SelectedItems.Count > 0)
                {
                    post = ((PostViewItem)PostsListView.SelectedItems[0]).GetPost();
                    merchandice = post.GetMerchandise();
                    if (!merchandice.IsEnabled())
                    {
                        MessageBox.Show("The product has to be enabled before proceeding!",
                            "Enable status", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create, post.GetMerchandise(), isDoubtfulProd);
                    createPostDialog.MdiParent = this.MdiParent;
                    createPostDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error creating new order", ex);
            }

        }

        private void ResetInvoiceStatusMenuItem_Click(object sender, EventArgs e)
        {
            PostList posts = new PostList();
            try
            {
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    posts.Add(pViewItem.GetPost());
                    pViewItem.GetPost().ResetPostInvoiceStatus();
                }
                RedrawPosts(posts);
            }
            catch (Exception ex)
            {
                HandleError("Error while updating database!", ex);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            FilterPosts();
        }

        private void SelectPost(Post post)
        {
            SelectPost(post, true);
        }

        private void SelectPost(Post post, bool clearSelection)
        {
            System.Windows.Forms.ListView.SelectedIndexCollection selectedIndexCollection;
            selectedIndexCollection = new ListView.SelectedIndexCollection(PostsListView);
            foreach(PostViewItem pViewItem in PostsListView.Items)
            {
                if(pViewItem.GetPost().GetId() == post.GetId())
                {
                    if (clearSelection)
                    {
                        PostsListView.SelectedIndices.Clear();
                    }
                    PostsListView.SelectedIndices.Add(pViewItem.Index);
                    PostsListView.EnsureVisible(pViewItem.Index);
                    break;
                }
            }
            PostsListView.Select();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RefreshListView()
        {
            MyPosts.ResetMerchandise();
            foreach (PostViewItem pViewItem in PostsListView.Items)
            {
                pViewItem.UpdateViewItem();
            }
        }

        private void UpdateListView()
        {
            MyPosts.ResetMerchandise();
            PostsListView.BeginLoadItems(MyPosts.Count);
            foreach (Post post in MyPosts)
            {

                PostsListView.AddItem(new PostViewItem(post));
            }

            PostsListView.EndLoadItems();
        }

        private void SupplierCombobox_OnMyControlledSelectedIndexChanged()
        {
            if (IsNotNull(SupplierCombobox.GetSelectedIdentity()))
            {
                merchandiseCombobox1.SetSupplierId(SupplierCombobox.GetSelectedIdentity().GetId());
                merchandiseCombobox1.LoadMerchandise(SupplierCombobox.GetSelectedIdentity().GetId());
            }
            else
            {
                merchandiseCombobox1.SetSupplierId(PlattformOrdManData.NO_ID);
                merchandiseCombobox1.LoadIdentitiesWithInfoText();
            }
            FilterPosts();        
        }

        private void RestoreSortingButton_Click(object sender, EventArgs e)
        {
            try
            {
                PostsListView.ResetSortOrder();
                RestoreSortingButton.Enabled = false;
            }
            catch (Exception ex)
            {
                HandleError("Error when restoring sort order", ex);
            }

        }

        private void ResetSearchFields()
        {
            SupplierCombobox.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            userComboBox1.LoadIdentitiesWithInfoText();
            FreeTextSearchTextBox.Text = FREE_TEXT_SEARCH;        
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearchFields();
                FilterPosts();
            }
            catch (Exception ex)
            {
                HandleError("Error when clearing filter", ex);
            }

        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            try
            {
                OrderHistoryOptionsDialog orderHistoryOptionsDialog;
                orderHistoryOptionsDialog = new OrderHistoryOptionsDialog();
                orderHistoryOptionsDialog.MdiParent = this.MdiParent;
                orderHistoryOptionsDialog.OnOrderHistoryOptionsOK += new OrderHistoryOptionOK(OrderHistoryOptions_OK);
                orderHistoryOptionsDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when setting options", ex);
            }

        }

        private void OrderHistoryOptions_OK(bool isColumnsUpdated)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                LoadPosts();
                if (isColumnsUpdated)
                {
                    ReInitListView();
                }
                FilterPosts();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}