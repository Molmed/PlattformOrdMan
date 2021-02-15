using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;
using Molmed.PlattformOrdMan.UI.Controller;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.UI.Dialog.OptionsDialog;
using PlattformOrdMan.UI.View.Post;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowOrderHistoryDialog : OrdManForm, ISupplierForm, IMerchandiseForm, IPostForm
    {
        private PostList _posts;
        private Dictionary<int, PostList> _supplierDict;
        private Dictionary<int, PostList> _prodDict;
        private ToolTipHandler _toolTipHandler;
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
        private const String SIGN_INVOICE_ABSENT = "Sign invoice absent";
        private const String MARK_FOR_ATTENTION = "Mark for attention";
        private const String UNMARK_FOR_ATTENTION = "Un-mark attention flag";
        private const String REGRET_ORDER_POST = "Regret Order post";
        private const String REGRET_CONFRIRM_ORDER = "Regret order confirmal";
        private const String REGRET_ARRIVAL_CONFIRMATION = "Regret Arrival confirmation";
        private const String REGRET_INVOICE_OK_AND_SENT = "Regret Invoice OK and Sent";
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
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
            var suppliers = SupplierManager.GetSuppliersFromCache();
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
            AttentionPanel.BackColor = Color.Red;
            userComboBox1.Init(true, "booker");
            userComboBox1.LoadIdentitiesWithInfoText();
            userComboBox1.OnMyControlledSelectedIndexChanged +=
                userComboBox1_OnMyControlledSelectedIndexChanged;
            merchandiseCombobox1.Enabled = true;
            RestoreSortingButton.Enabled = false;
            LoadPosts();
            InitListView();
            SupplierCombobox.OnMyControlledSelectedIndexChanged +=
                SupplierCombobox_OnMyControlledSelectedIndexChanged;
            merchandiseCombobox1.OnMyControlledSelectedIndexChanged +=
                merchandiseCombobox1_OnMyControlledSelectedIndexChanged;
            FreeTextSearchTextBox.Enter += FreeTextSearchTextBox_Enter;
            FormClosing += ShowOrderHistoryDialog_FormClosing;
            //MessageBox.Show(DateTime.Now.Subtract(start).Milliseconds.ToString() + " ms");
        }

        public override void ReloadForm()
        {
            SupplierManager.RefreshCache();
            MerchandiseManager.RefreshCache();
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
            return IsNotNull(_posts.GetById(postId));
        }

        public void ReloadPost(Post post)
        {
            PostsListView.ReloadPost(post);
            var index = _posts.GetIndex(post);
            _posts[index] = post;
        }

        public void AddCreatedPost(Post post)
        {
            _posts.Add(post);
            PostsListView.AddViewItem(post);
            PostsListView.SelectedIndices.Clear();
            PostsListView.Items[0].Selected = true;
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return _supplierDict.ContainsKey(supplierId);
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return _prodDict.ContainsKey(merchandiseId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            PostsListView.ReloadSupplier(supplier);
            if (IsNotNull(supplier) && _supplierDict.ContainsKey(supplier.GetId()))
            {
                foreach (Post post in _supplierDict[supplier.GetId()])
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
            if (IsNotNull(merchandise) && _prodDict.ContainsKey(merchandise.GetId()))
            {
                foreach (Post post in _prodDict[merchandise.GetId()])
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

        private void LoadPosts()
        {
            var today = DateTime.Now;
            var monthsBack = PlattformOrdManData.Configuration.TimeIntervalForPosts;
            var timeRestrToCompletedPostsOnly = PlattformOrdManData.Configuration.TimeRestrictionForCompletedPostsOnly;
            var loadedPosts = monthsBack == PlattformOrdManData.NO_COUNT
                ? PostManager.GetPosts(today, false, timeRestrToCompletedPostsOnly)
                : PostManager.GetPosts(today.AddMonths(-monthsBack), true, timeRestrToCompletedPostsOnly);
            _posts = new PostList();
            _supplierDict = new Dictionary<int, PostList>();
            _prodDict = new Dictionary<int, PostList>();
            foreach (Post post in loadedPosts)
            {
                if (
                    PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Contains(
                        post.GetPlaceOfPurchase().ToString()))
                {
                    _posts.Add(post);
                    PostList tmpPosts;
                    if (!_supplierDict.ContainsKey(post.GetSupplierId()) &&
                        post.GetSupplierId() != PlattformOrdManData.NO_ID)
                    {
                        tmpPosts = new PostList {post};
                        _supplierDict.Add(post.GetSupplierId(), tmpPosts);
                    }
                    else if (post.GetSupplierId() != PlattformOrdManData.NO_ID)
                    {
                        _supplierDict[post.GetSupplierId()].Add(post);
                    }

                    if (!_prodDict.ContainsKey(post.GetMerchandiseId()) &&
                        post.GetMerchandiseId() != PlattformOrdManData.NO_ID)
                    {
                        tmpPosts = new PostList {post};
                        _prodDict.Add(post.GetMerchandiseId(), tmpPosts);
                    }
                    else if (post.GetMerchandiseId() != PlattformOrdManData.NO_ID)
                    {
                        _prodDict[post.GetMerchandiseId()].Add(post);
                    }
                }
            }
            _posts.Sort();
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
            PostsListView.AddColumns();

            AddMenuItem(PostsListView, LOCK_COLUMN_WIDTH, LockColumnWidth_Click);
            AddMenuItem(PostsListView, UN_LOCK_COLUMN_WIDTH, UnlockColumnWidth_Click);
            AddMenuItem(PostsListView, UPDATE, UpdateMenuItem_Click);
            AddMenuItem(PostsListView, MARK_FOR_ATTENTION, MarkForAttention_Click);
            AddMenuItem(PostsListView, UNMARK_FOR_ATTENTION, UnmarkAttentionFlag);
            AddMenuItem(PostsListView, ORDER_POST, OrderPostMenuItem_Click);
            AddMenuItem(PostsListView, CONFIRM_ORDER, ConfirmOrderMenuItem_Click);
            AddMenuItem(PostsListView, CONFIRM_ARRIVAL, ConfirmArrivalMenuItem_Click);
            AddMenuItem(PostsListView, SET_ORDER_NR_SO, SetSalesOrderNo_Click);
            AddMenuItem(PostsListView, REGRET_ORDER_POST, RegretOrderPost);
            AddMenuItem(PostsListView, REGRET_CONFRIRM_ORDER, RegretOrderConfirmation);
            AddMenuItem(PostsListView, SIGN_INVOICE_OK_AND_SENT, SignInvoiceOkAndSentMenuItem_Click);
            AddMenuItem(PostsListView, SIGN_INVOICE_ABSENT, SingInvoiceAbsent_Click);
            AddMenuItem(PostsListView, REGRET_ARRIVAL_CONFIRMATION, RegretArrivalConfirmation);
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
            PostsListView.DoubleClick += UpdateMenuItem_Click;
            PostsListView.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            PostsListView.OnSortOrderSet += PostsListView_OnSortOrderSet;
            PostsListView.ShowItemToolTips = false;
            PostsListView.ItemMouseHover += PostsListView_ItemMouseHover;

            UpdateListView();
        }

        private void ReInitListView()
        {
            // Updates columns and rows only
            PostsListView.Clear();
            PostsListView.AddColumns();
            UpdateListView();
            ReInitAllColumnWidths();
        }

        private void SetInvoiceNumberMenuItem_Click(object sender, EventArgs e)
        {
            bool hasCommonGroup = true, hasCommonSuplier = true;
            try
            {
                var posts = new PostList();
                foreach (PostViewItem viewItem in PostsListView.SelectedItems)
                {
                    posts.Add(viewItem.GetPost());
                }

                if (IsEmpty(posts))
                {
                    return;
                }

                var supplierId = posts[0].GetSupplierId();
                var commonGroup = posts[0].GetGroupCategory();
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
                    MessageBox.Show(
                        "Selected posts have different suppliers, it's not possible to set a common invoice number!",
                        "Invoice number error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!hasCommonGroup)
                {
                    MessageBox.Show(
                        "Selected posts are assigned to different groups (Plattform or Research). Customer numbers for both groups are eligible",
                        "Posts from both groups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                var setInvoiceNumberDialog = new SetInvoiceNumberDialog(posts);
                if (setInvoiceNumberDialog.ShowDialog() == DialogResult.OK)
                {
                    var invoiceNumber = setInvoiceNumberDialog.InvoiceNumber;
                    var noInvoice = setInvoiceNumberDialog.NoInvoice;
                    foreach (Post post in posts)
                    {
                        post.UpdateInvoiceNumber(invoiceNumber, noInvoice);
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
                _toolTipHandler = new ToolTipHandler(this, e.Item.ToolTipText);
                _toolTipHandler.StartTimer();
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
            var expr = PostListViewConfColumns.ColSortOrder + " = " + colIndex;
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                rows[0][PostListViewConfColumns.ColWidth.ToString()] =
                    PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
                ReInitColumnWidth(colIndex, true);
            }
            else
            {
                throw new Data.Exception.DataException("Column sort order mis-match: " + colIndex);
            }
        }

        private void LockColumnWidth(int colIndex)
        {
            var expr = PostListViewConfColumns.ColSortOrder + " = " + colIndex;
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                rows[0][PostListViewConfColumns.ColWidth.ToString()] =
                    PostsListView.Columns[colIndex].Width;
            }
            else
            {
                throw new Data.Exception.DataException("Column sort order mis-match: " + colIndex);
            }
        }

        private void UpdateLockedColumnWidths()
        {
            for (int colIndex = 0; colIndex < PostsListView.Columns.Count; colIndex++)
            {
                var expr = PostListViewConfColumns.ColSortOrder + " = " + colIndex;
                var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
                if (rows.Length == 1)
                {
                    var currentConfWidth = (int) rows[0][PostListViewConfColumns.ColWidth.ToString()];
                    if (currentConfWidth > 0)
                    {
                        rows[0][PostListViewConfColumns.ColWidth.ToString()] =
                            PostsListView.Columns[colIndex].Width;
                    }
                }
                else
                {
                    throw new Data.Exception.DataException("Column sort order mis-match: " + colIndex);
                }
            }
        }

        private int GetClickedColumnHeaderIndex(out bool isFixedWidth)
        {
            // Used to check if user right-clicked on a column header, and if so
            // determine which column was clicked
            Rectangle firstItemRect;
            isFixedWidth = false;
            var mousePosScreen = MousePosition;
            var postListViewStart = PostsListView.PointToScreen(new Point(0, 0));
            if (PostsListView.Items.Count == 0)
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
            int colWidth;
            var expr = PostListViewConfColumns.ColSortOrder + " = " + colInd;
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expr);
            if (rows.Length == 1)
            {
                colWidth = (int) rows[0][PostListViewConfColumns.ColWidth.ToString()];
            }
            else
            {
                throw new Data.Exception.DataException("Column index mis-match: " + colInd);
            }
            return colWidth > 0;
        }

        private void SetColumnConfMenus(object sender)
        {
            bool isFixedColWidth;

            SetVisible(sender, LOCK_COLUMN_WIDTH, false);
            SetVisible(sender, UN_LOCK_COLUMN_WIDTH, false);
            var colInd = GetClickedColumnHeaderIndex(out isFixedColWidth);
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
                SetVisible(sender, SIGN_INVOICE_ABSENT, false);
                SetVisible(sender, MARK_FOR_ATTENTION, false);
                SetVisible(sender, UNMARK_FOR_ATTENTION, false);
                SetVisible(sender, MERCHANDISE, false);
                SetVisible(sender, SUPPLIER, false);
                SetVisible(sender, RESET_INVOICE_STATUS, false);
                SetVisible(sender, "sep", false);
                SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, false);
                SetVisible(sender, REGRET_CONFRIRM_ORDER, false);
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
            SetVisible(sender, SIGN_INVOICE_ABSENT, false);
            SetVisible(sender, MARK_FOR_ATTENTION, false);
            SetVisible(sender, UNMARK_FOR_ATTENTION, false);
            SetVisible(sender, RESET_INVOICE_STATUS, false);
            SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, false);
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
                SetVisible(sender, SIGN_INVOICE_ABSENT, true);
                SetVisible(sender, REGRET_ARRIVAL_CONFIRMATION, true);
            }
            if (IsSelectedItemsOnlyUnflagged())
            {
                SetVisible(sender, MARK_FOR_ATTENTION, true);
            }
            if (IsSelectedItemsOnlyFlagged())
            {
                SetVisible(sender, UNMARK_FOR_ATTENTION, true);
            }

            if (IsSelectedStatusOnlyCompleted())
            {
                SetVisible(sender, RESET_INVOICE_STATUS, true);

                if (IsSelectedStatusOnlyInvoiceOkAndSent())
                {
                    SetVisible(sender, REGRET_INVOICE_OK_AND_SENT, true);
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

        private bool IsSelectedItemsOnlyUnflagged()
        {
            foreach (PostViewItem selectedItem in PostsListView.SelectedItems)
            {
                if (selectedItem.GetPost().AttentionFlag)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsSelectedItemsOnlyFlagged()
        {
            foreach (PostViewItem selectedItem in PostsListView.SelectedItems)
            {
                if (!selectedItem.GetPost().AttentionFlag)
                {
                    return false;
                }
            }
            return true;
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
            PostList posts = new PostList();
            try
            {
                var getValueDialog = new GetValueDialog(SET_ORDER_NR_SO, "Enter a sales order number, please.", "");
                if (getValueDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                foreach (PostViewItem viewItem in PostsListView.SelectedItems)
                {
                    var tmpPost = viewItem.GetPost();
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
            PostList posts = new PostList();
            try
            {
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    var tmpPost = pViewItem.GetPost();
                    posts.Add(tmpPost);
                    tmpPost.ConfirmPostArrival(UserManager.GetCurrentUser().GetId());
                    if (tmpPost.IsInvoceAbsent())
                    {
                        tmpPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok,
                            tmpPost.IsInvoceAbsent());
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
            try
            {
                var str = "Are you sure to delete the " + PostsListView.SelectedItems.Count + " items?";
                if (MessageBox.Show(str, "Delete posts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                    DialogResult.No)
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
            _posts.Remove(post);
        }

        private void SupplierMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var editSupplierDialog = new EditSupplierDialog(GetSelectedPost().GetSupplier(), UpdateMode.Edit)
                {
                    MdiParent = MdiParent
                };
                editSupplierDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }
        }

        private void MerchandiseMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var editMerchandiseDialog = new EditMerchandiseDialog(GetSelectedPost().GetMerchandise(),
                    UpdateMode.Edit) {MdiParent = MdiParent};
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
                CreatePostDialog createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create)
                {
                    MdiParent = MdiParent
                };
                createPostDialog.Show();
            }
            catch (Exception ex)
            {
                HandleError("Error when creating new order", ex);
            }
        }

        private void LockColumnWidth_Click(object sender, EventArgs e)
        {
            try
            {
                var colInd = (int) ((ToolStripMenuItem) sender).Tag;
                LockColumnWidth(colInd);
                var colName = PostsListView.Columns[colInd].Text;
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
            try
            {
                var colInd = (int) ((ToolStripMenuItem) sender).Tag;
                UnlockColumnWidth(colInd);
                var colName = PostsListView.Columns[colInd].Text;
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
            try
            {
                if (IsNull(GetSelectedPost()))
                {
                    return;
                }
                var createPostDialog = new CreatePostDialog(GetSelectedPost(), PostUpdateMode.Edit)
                {
                    MdiParent = MdiParent
                };
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

        private void SetDeviantPostViewItems(PostList posts, out ColumnHeader[] columnHeaders,
            out ListViewItem[] listViewItems)
        {
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
                var viewItem = new ListViewItem(post.GetBookerName());
                viewItem.SubItems.Add(post.GetMerchandiseName());
                viewItem.SubItems.Add(post.GetArticleNumberString());
                viewItem.SubItems.Add(post.GetMerchandise().GetActiveArticleNumberString());
                listViewItems[i++] = viewItem;
            }
        }

        private void OrderPostMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var posts = new PostList();
                ListViewItem[] listViewItems;
                ColumnHeader[] columnHeaders;
                if (!HasActiveArticleNumber(out columnHeaders, out listViewItems))
                {
                    var listDialog = new ListDialog(columnHeaders, listViewItems, "Deviant article number",
                        ListDialog.DialogMode.AcceptanceList,
                        "The folliwing posts have another article number than what is currently active for \nthat product, proceed anyway?");
                    listDialog.SetRowLimitFilterStatus(false);
                    if (listDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
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
            var posts = new PostList();
            try
            {
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
                post = ((PostViewItem) PostsListView.SelectedItems[0]).GetPost();
            }
            return post;
        }

        private bool IsWithinFreeTextSearchCriteria(Post post)
        {
            var searchStr = FreeTextSearchTextBox.Text.Trim();
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
            if (SupplierCombobox.SelectedIndex > 0 &&
                SupplierCombobox.GetSelectedIdentity().GetId() != post.GetSupplierId())
            {
                return false;
            }
            if (merchandiseCombobox1.SelectedIndex > 0 && post.GetMerchandise() != null &&
                merchandiseCombobox1.GetSelectedIdentity().GetId() != post.GetMerchandise().GetId())
            {
                return false;
            }
            if (userComboBox1.SelectedIndex > 0 && post.GetBooker() != null &&
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
            foreach (Post post in _posts)
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
            PostList posts = new PostList();
            foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
            {
                var tmpPost = pViewItem.GetPost();
                posts.Add(tmpPost);
            }
            return posts;
        }

        private void UnmarkAttentionFlag(object sender, EventArgs e)
        {
            try
            {
                SetMarkForAttention(false);
            }
            catch (Exception exception)
            {
                HandleError("Error when un-mark posts for attention", exception);
                throw;
            }
        }

        private void MarkForAttention_Click(object sender, EventArgs e)
        {
            try
            {
                SetMarkForAttention(true);
            }
            catch (Exception exception)
            {
                HandleError("Error when marking posts for attention", exception);
            }
        }

        private void SetMarkForAttention(bool markForAttention)
        {
            PostList posts = new PostList();
            foreach (PostViewItem selectedItem in PostsListView.SelectedItems)
            {
                var tmpPost = selectedItem.GetPost();
                tmpPost.UpdateMarkForAttention(markForAttention);
                posts.Add(tmpPost);
            }
            RedrawPosts(posts);
        }

        private void SingInvoiceAbsent_Click(object sender, EventArgs e)
        {
            PostList posts = new PostList();
            try
            {
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    var tmpPost = pViewItem.GetPost();
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
            var col = PostsListView.Columns[colInd];
            var expression = PostListViewConfColumns.ColSortOrder + " = " + colInd;
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
            try
            {
                if (rows.Length == 1)
                {
                    if (updateHandling)
                    {
                        PostsListView.BeginUpdate();
                    }
                    var configWidth = (int) rows[0][PostListViewConfColumns.ColWidth.ToString()];
                    if (configWidth == PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH)
                    {
                        col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                        var colHeaderWidth = col.Width;
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
                var errStr = "Please select invoice category for the following product(s):\n";
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
            PostList posts = new PostList();
            try
            {
                if (IsInvoiceAbsent())
                {
                    return;
                }
                foreach (PostViewItem pViewItem in PostsListView.SelectedItems)
                {
                    var tmpPost = pViewItem.GetPost();
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

        private void CreatePostFromSameProductMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (PostsListView.SelectedItems.Count > 0)
                {
                    var post = ((PostViewItem) PostsListView.SelectedItems[0]).GetPost();
                    var merchandice = post.GetMerchandise();
                    if (!merchandice.IsEnabled())
                    {
                        MessageBox.Show("The product has to be enabled before proceeding!",
                            "Enable status", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    var createPostDialog = new CreatePostDialog(null, PostUpdateMode.Create, post.GetMerchandise())
                    {
                        MdiParent = MdiParent
                    };
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateListView()
        {
            _posts.ResetMerchandise();
            PostsListView.BeginLoadItems(_posts.Count);
            foreach (Post post in _posts)
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
                var orderHistoryOptionsDialog = new OrderHistoryOptionsDialog {MdiParent = MdiParent};
                orderHistoryOptionsDialog.OnOrderHistoryOptionsOK += OrderHistoryOptions_OK;
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
                Cursor = Cursors.Default;
            }
        }
    }
}