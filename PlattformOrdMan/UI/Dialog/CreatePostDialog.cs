using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class CreatePostDialog : OrdManForm, ISupplierForm, IMerchandiseForm
    {
        public delegate void PostUpdatedHandler(object sender, UpdateHandlerEventArgs e);
        private delegate bool CheckComboxesSelectedCallback();
        private delegate void FixComboboxesSelectionCallback();
        public event PostUpdatedHandler OnPostUpdate;
        private Post MyPost;
        private PostUpdateMode MyUpdateMode;
        private PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged MyControlledSelectedIndexChangedForMerchanidse;
        private DateTimeStyles MyDateTimeStyle;
        private System.Timers.Timer MyFixComboboxSelectionTimer;
        private int popPrevSel;

        public CreatePostDialog(Post post, PostUpdateMode updateMode)
        {
            InitializeComponent();
            MyUpdateMode = updateMode;
            MyPost = post;
            MyDateTimeStyle = DateTimeStyles.None;
            Init();
        }

        public CreatePostDialog(Post post, PostUpdateMode updateMode, Merchandise merchandice, 
            bool isDoubtfulProd)
        {
            InitializeComponent();
            MyUpdateMode = updateMode;
            MyPost = post;
            MyDateTimeStyle = DateTimeStyles.None;
            Init();
            merchandiseCombobox1.SetSelectedIdentity(merchandice);
        }

        private bool IsLoadedInCombobox(CustomerNumber custNum)
        {
            foreach (CustomerNumber cust in CustomerNumberComboBox.Items)
            {
                if (cust.GetId() == custNum.GetId())
                {
                    return true;
                }
            }
            return false;
        }

        private void InitCustomerNumberCombobox()
        {
            Supplier supplier;
            PlaceOfPurchase placeOfPurchase;
            if (SupplierComboBox.HasSelectedSupplier() && OrderingUnitComboBox.SelectedIndex >= 0)
            {
                placeOfPurchase = PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem);
                supplier = SupplierComboBox.GetSelectedSupplier();
                CustomerNumberComboBox.Items.Clear();
                foreach (CustomerNumber custNum in supplier.GetCustomerNumbersForUserGroup(placeOfPurchase))
                //foreach (CustomerNumber custNum in supplier.GetCustomerNumbersForCurrentUserGroup())
                {
                    if (custNum.IsEnabled())
                    {
                        CustomerNumberComboBox.Items.Add(custNum);
                    }
                }
                // Add disabled customer number if it exists for this post
                if (IsNotNull(MyPost) && MyPost.HasCustomerNumber() &&
                    !IsLoadedInCombobox(MyPost.GetCustomerNumber()))
                {
                    CustomerNumberComboBox.Items.Add(MyPost.GetCustomerNumber());
                }

            }

            if (CustomerNumberComboBox.Items.Count == 0)
            {
                CustomerNumberComboBox.Enabled = false;
            }
            else if (CustomerNumberComboBox.Items.Count == 1)
            {
                CustomerNumberComboBox.Enabled = true;
                CustomerNumberComboBox.SelectedIndex = 0;
            }
            else
            {
                CustomerNumberComboBox.Enabled = true;
            }
        }

        private void SetDefaultCustomerNumber()
        {
            if (CustomerNumberComboBox.Items.Count == 1)
            {
                CustomerNumberComboBox.SelectedIndex = 0;
            }
            else if (CustomerNumberComboBox.Items.Count > 1)
            {
                CustomerNumberComboBox.Text = "Please select customer number";
            }
            else if (CustomerNumberComboBox.Items.Count == 0)
            {
                CustomerNumberComboBox.Text = "No customer number available!";
            }            
        }

        private void UpdateCustomerNumber()
        {
            if (MyPost != null &&
                MyPost.GetCustomerNumberId() != PlattformOrdManData.NO_ID)
            {
                foreach (object o in CustomerNumberComboBox.Items)
                { 
                    if(o is CustomerNumber && 
                        ((CustomerNumber)o).GetId() == MyPost.GetCustomerNumberId())
                    {
                        CustomerNumberComboBox.SelectedItem = o;
                        return;
                    }
                }
                if (CustomerNumberComboBox.SelectedIndex == -1)
                {
                    throw new Data.Exception.DataException("Unable to find customer number for this product!");
                }
            }
        }

        void CreatePostDialog_ResizeEnd(object sender, EventArgs e)
        {
            FixComboboxSelection();
        }

        private void InitOrderingUnitCombobox()
        {
            string selStr;
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                OrderingUnitComboBox.Items.Add(PlattformOrdManData.GetPlaceOfPurchaseString(pop));
            }
            selStr = UserManager.GetCurrentUser().GetPlaceOfPurchaseStringForUser();
            OrderingUnitComboBox.SelectedItem = selStr;
        }

        public bool HasMerchandiseLoaded(int merchandiseId)
        {
            return merchandiseCombobox1.HasMerchandiseLoaded(merchandiseId);
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            if (IsNotNull(merchandise) &&
                merchandiseCombobox1.HasMerchandiseLoaded(merchandise.GetId()))
            {
                merchandiseCombobox1.ReloadMerchandise(merchandise);
            }
            if (merchandise != null &&
                merchandiseCombobox1.GetSelectedIdentityId() == merchandise.GetId())
            {
                merchandiseCombobox1_OnMyControlledSelectedIndexChanged();
            }
        }

        public void AddCreatedMerchandise(Merchandise merchandise)
        {
            merchandiseCombobox1.AddCreatedMerchandise(merchandise);
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return SupplierComboBox.HasSupplierLoaded(supplierId);
        }

        public void ReloadSupplier(Supplier supplier)
        {
            SupplierComboBox.ReloadSupplier(supplier);
        }

        public void AddCreatedSupplier(Supplier supplier)
        {
            SupplierComboBox.AddCreatedSupplier(supplier);
        }

        private void InitInvoiceCheckboxes()
        {
            if (IsNull(MyPost) ||
                MyPost.GetBookDateDT().CompareTo(PlattformOrdManData.CustomerNumberReformDate) > 0)
            {
                InvoiceInstCheckBox.Visible = false;
                InvoiceClinCheckBox.Visible = false;
            }
        }

        private void Init()
        {
            SupplierList suppliers;
            InitInvoiceCheckboxes();
            popPrevSel = -1;
            MyControlledSelectedIndexChangedForMerchanidse = 
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(merchandiseCombobox1_OnMyControlledSelectedIndexChanged);
            suppliers = SupplierManager.GetActiveSuppliersOnly();
            SupplierComboBox.Init(suppliers, "supplier", true);
            SupplierComboBox.LoadIdentitiesWithInfoText();
            SupplierComboBox.OnMyControlledSelectedIndexChanged +=
                new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(SupplierComboBox_OnMyControlledSelectedIndexChanged);
            if (IsNotNull(MyPost) && !MyPost.GetMerchandise().IsEnabled())
            {
                merchandiseCombobox1.Init(false, false);
            }
            else
            {
                merchandiseCombobox1.Init(false, true);            
            }
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.OnMyControlledSelectedIndexChanged += MyControlledSelectedIndexChangedForMerchanidse;
            CurrencyCombobox.SelectedIndexChanged += new EventHandler(CurrencyCombobox_SelectedIndexChanged);
            CurrencyCombobox.LoadCurrencies();
            InitStatusFields();
            InitOrderingUnitCombobox();
            switch (MyUpdateMode)
            { 
                case PostUpdateMode.Create:
                    SetDefaultCustomerNumber();
                    break;
                case PostUpdateMode.Edit:
                    InitEditMode();
                    break;
                case PostUpdateMode.OrderSign:
                    InitOrderMode();
                    break;
            }
            if (IsNotNull(MyPost) && MyPost.GetPostStatus() == Post.PostStatus.Completed)
            {
                NoInvoiceCheckBox.Enabled = false;
            }
            this.ResizeEnd += CreatePostDialog_ResizeEnd;
            this.Activated += CreatePostDialog_Activated;
            SaveButton.Enabled = false;
            MyFixComboboxSelectionTimer = new System.Timers.Timer(100);
            MyFixComboboxSelectionTimer.Elapsed += MyFixComboboxSelectionTimer_Elapsed;
            MyFixComboboxSelectionTimer.Enabled = true;
        }

        public override void ReloadForm()
        {
            SupplierList suppliers;
            MerchandiseList products;
            suppliers = SupplierManager.GetActiveSuppliersOnly();
            products = MerchandiseManager.GetActiveMerchandiseOnly();
            SupplierComboBox.ReloadIdentities(suppliers);
            merchandiseCombobox1.ReloadIdentities(products);

        }

        void CreatePostDialog_Activated(object sender, EventArgs e)
        {
            FixComboboxSelection();
        }


        void MyFixComboboxSelectionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsComboboxesSelected())
            {
                MyFixComboboxSelectionTimer.Enabled = false;
            }
            FixComboboxSelection();
        }

        private bool IsComboboxesSelected()
        {
            if (this.SupplierComboBox.InvokeRequired || this.merchandiseCombobox1.InvokeRequired)
            {
                CheckComboxesSelectedCallback c = new CheckComboxesSelectedCallback(IsComboboxesSelected);
                if (!this.IsDisposed)
                {
                    return (bool)this.Invoke(c);
                }
            }
            else
            {
                return SupplierComboBox.SelectionStart == 0 || merchandiseCombobox1.SelectionStart == 0;            
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == PlattformOrdManData.WM_SYSCOMMAND &&
                (m.WParam == new IntPtr(PlattformOrdManData.SC_MAXIMIZE)||
                m.WParam == new IntPtr(PlattformOrdManData.SC_RESTORE) ||
                m.WParam == new IntPtr(PlattformOrdManData.SC_MINIMIZE)))
            {
                MyFixComboboxSelectionTimer.Enabled = true;
            }
            
            base.WndProc(ref m);
        }

        private void SupplierComboBox_OnMyControlledSelectedIndexChanged()
        {
            int merchandiseId = PlattformOrdManData.NO_ID;
            if (SupplierComboBox.HasSelectedIdentity())
            {
                ShowSupplierButton.Enabled = true;
            }
            else
            {
                ShowSupplierButton.Enabled = false;
            }
            if (MyUpdateMode == PostUpdateMode.Create)
            {
                if (IsNotNull(SupplierComboBox.GetSelectedIdentity()))
                {
                    // Case: supplier not chosen, user choose product -->
                    //       supplier is chosen --> 
                    //       product list is reloaded. Following code render the chosenn 
                    //       product is not lost (product is re-chosen)
                    // Case: Supplier is chosen, user chose another supplier -->
                    //       product list is reloaded --> code tries to find the previous 
                    //       chosen product in new list, but do not succed (OK). 
                    if (merchandiseCombobox1.HasSelectedIdentity())
                    {
                        merchandiseId = merchandiseCombobox1.GetSelectedMerchandise().GetId();
                    }
                    merchandiseCombobox1.LoadMerchandise(SupplierComboBox.GetSelectedIdentity().GetId());
                    merchandiseCombobox1.SetSelectedIdentity(merchandiseId);
                }
                else
                {
                    merchandiseCombobox1.SetSupplierId(PlattformOrdManData.NO_ID);
                    merchandiseCombobox1.LoadIdentitiesWithInfoText();
                }
            }
            InitCustomerNumberCombobox();
            HandleSaveButtonEnabled();        
        }

        private void CurrencyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String str = ApprPrizeTextBox.Text.Trim();
            ApprPrizeTextBox.Text =
                CurrencyCombobox.GetSelectedCurrency().GetPriceWithCurrencyString(str);
            str = FinalPrizeTextBox.Text.Trim();
            FinalPrizeTextBox.Text =
                CurrencyCombobox.GetSelectedCurrency().GetPriceWithCurrencyString(str);
            HandleSaveButtonEnabled();
        }

        private void AdjustCommentTextBox()
        {
            int offset;
            offset = CommentTextBox.Location.Y - CommentLabel.Location.Y;
            CommentTextBox.Location = new Point(CommentTextBox.Location.X, CommentTextBox.Location.Y - CommentLabel.Location.Y + ApprArrivalLabel.Location.Y);
            CommentTextBox.Height = CommentTextBox.Height + CommentLabel.Location.Y - ApprArrivalLabel.Location.Y;
            CommentLabel.Location = new Point(CommentLabel.Location.X, CommentTextBox.Location.Y - offset);            
        }

        private void InitStatusFields()
        {
            ArrivalSignUserComboBox.Enabled = false;
            ArrivalDateTextBox.Enabled = false;
            InvoiceCheckDateTextBox.Enabled = false;
            InvoiceCheckerUserComboBox.Enabled = false;
            ConfirmedOrderUserComboBox.Enabled = false;
            ConfirmedOrderDateTextBox.Enabled = false;
            OrderDateTextBox.Enabled = false;
            
            BookerUserComboBox.Init(false, "user");
            BookerUserComboBox.LoadIdentities();
            OrdererUserComboBox.Init(true, "user");
            OrdererUserComboBox.LoadIdentities();
            ConfirmedOrderUserComboBox.Init(true, "user");
            ConfirmedOrderUserComboBox.LoadIdentities();
            ArrivalSignUserComboBox.Init(true, "user");
            ArrivalSignUserComboBox.LoadIdentities();
            InvoiceCheckerUserComboBox.Init(true, "user");
            InvoiceCheckerUserComboBox.LoadIdentities();
            BookerUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(BookerUserComboBox_OnMyControlledSelectedIndexChanged);
            OrdererUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(OrdererUserComboBox_OnMyControlledSelectedIndexChanged);
            ConfirmedOrderUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(ConfirmedOrderUserComboBox_OnMyControlledSelectedIndexChanged);
            ArrivalSignUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(ArrivalSignUserComboBox_OnMyControlledSelectedIndexChanged);
            InvoiceCheckerUserComboBox.OnMyControlledSelectedIndexChanged +=new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(InvoiceCheckerUserComboBox_OnMyControlledSelectedIndexChanged);
            InvoiceOKCheckBox.Enabled = false;
            if (IsNull(MyPost))
            {
                BookerUserComboBox.SetSelectedIdentity(UserManager.GetCurrentUser());
                BookDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                BookerUserComboBox.SetSelectedIdentity(MyPost.GetBooker());
                BookDateTextBox.Text = MyPost.GetBookDate();
                switch (MyPost.GetPostStatus())
                {
                    case Post.PostStatus.Booked:
                        break;
                    case Post.PostStatus.Ordered:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        OrderDateTextBox.Enabled = true;
                        ConfirmedOrderUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.ConfirmedOrder:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Confirmed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ArrivalDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Completed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        InvoiceCheckerUserComboBox.SetSelectedIdentity(MyPost.GetInvoicerUser());
                        InvoiceCheckDateTextBox.Text = MyPost.GetInvoiceDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        ArrivalDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        InvoiceOKCheckBox.Enabled = true;
                        if (MyPost.GetInvoiceStatus() == Post.InvoiceStatus.NotOk)
                        {
                            InvoiceOKCheckBox.Checked = false;
                        }
                        break;
                }
            }
        }

        private void UpdateStatusFields()
        { 
            if(MyUpdateMode == PostUpdateMode.Edit && IsNotNull(MyPost))
            {
                MyPost = PostManager.GetPostById(MyPost.GetId());
                OrdererUserComboBox.SelectedIndex = -1;
                OrderDateTextBox.Text = "";
                OrderDateTextBox.Enabled = false;
                ConfirmedOrderUserComboBox.Enabled = false;
                ConfirmedOrderUserComboBox.SelectedIndex = -1;
                ConfirmedOrderDateTextBox.Text = "";
                ConfirmedOrderDateTextBox.Enabled = false;
                ArrivalSignUserComboBox.Enabled = false;
                ArrivalSignUserComboBox.SelectedIndex = -1;
                ArrivalDateTextBox.Text = "";
                ArrivalDateTextBox.Enabled = false;
                InvoiceCheckerUserComboBox.SelectedIndex = -1;
                InvoiceCheckerUserComboBox.Enabled = false;
                InvoiceCheckDateTextBox.Text = "";
                InvoiceCheckDateTextBox.Enabled = false;

                switch (MyPost.GetPostStatus())
                {
                    case Post.PostStatus.Booked:
                        break;
                    case Post.PostStatus.Ordered:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        OrderDateTextBox.Enabled = true;
                        ConfirmedOrderUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.ConfirmedOrder:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Confirmed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ArrivalDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Completed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(MyPost.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = MyPost.GetConfirmedOrderDateString();
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        InvoiceCheckerUserComboBox.SetSelectedIdentity(MyPost.GetInvoicerUser());
                        InvoiceCheckDateTextBox.Text = MyPost.GetInvoiceDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        ArrivalDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        InvoiceOKCheckBox.Enabled = true;
                        if (MyPost.GetInvoiceStatus() == Post.InvoiceStatus.NotOk)
                        {
                            InvoiceOKCheckBox.Checked = false;
                        }
                        break;
                }
            }
        }

        private void InitStatusFields2()
        {
            BookerUserComboBox.Init(false, "user");
            BookerUserComboBox.LoadIdentities();
            OrdererUserComboBox.Init(false, "user");
            OrdererUserComboBox.LoadIdentities();
            ArrivalSignUserComboBox.Init(false, "user");
            ArrivalSignUserComboBox.LoadIdentities();
            InvoiceCheckerUserComboBox.Init(false, "user");
            InvoiceCheckerUserComboBox.LoadIdentities();
            BookerUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(BookerUserComboBox_OnMyControlledSelectedIndexChanged);
            OrdererUserComboBox.OnMyControlledSelectedIndexChanged +=new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(OrdererUserComboBox_OnMyControlledSelectedIndexChanged);
            ArrivalSignUserComboBox.OnMyControlledSelectedIndexChanged += new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox.MyControlledSelectedIndexChanged(ArrivalSignUserComboBox_OnMyControlledSelectedIndexChanged);
            if (IsNull(MyPost))
            {
                BookerUserComboBox.SetSelectedIdentity(UserManager.GetCurrentUser());
                BookDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                BookerUserComboBox.SetSelectedIdentity(MyPost.GetBooker());
                BookDateTextBox.Text = MyPost.GetBookDate();
                switch (MyPost.GetPostStatus())
                {
                    case Post.PostStatus.Booked:
                        break;
                    case Post.PostStatus.Ordered:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        break;
                    case Post.PostStatus.Confirmed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        break;
                    case Post.PostStatus.Completed:
                        OrdererUserComboBox.SetSelectedIdentity(MyPost.GetOrderer());
                        OrderDateTextBox.Text = MyPost.GetOrderDateString();
                        ArrivalSignUserComboBox.SetSelectedIdentity(MyPost.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = MyPost.GetArrivalDateString();
                        InvoiceCheckerUserComboBox.SetSelectedIdentity(MyPost.GetInvoicerUser());
                        InvoiceCheckDateTextBox.Text = MyPost.GetInvoiceDateString();
                        break;
                }
            }
        }

        private void InitEditMode()
        {
            this.Text = "Update post";
            this.SaveButton.Text = "Update";
            SetSupplier();
            merchandiseCombobox1.SetSelectedIdentity(MyPost.GetMerchandise().GetId());
            MerchandiseCommentTextBox.Text = MyPost.GetMerchandise().GetComment();
            AmountTextBox.Text = MyPost.GetAmountString();
            CommentTextBox.Text = MyPost.GetComment();
            PurchaseOrderNoTextBox.Text = MyPost.GetPurchaseOrderNo();
            SalesOrdernoTextBox.Text = MyPost.GetSalesOrderNo();
            DeliveryDeviationTextBox.Text = MyPost.GetDeliveryDeviation();
            ApprPrizeTextBox.Text = MyPost.GetPriceWithCurrencyString();
            FinalPrizeTextBox.Text = MyPost.GetFinalPrizeWithCurrencyString();
            InvoiceNumberTextBox.Text = MyPost.GetInvoiceNumber();
            OrderingUnitComboBox.SelectedItem = MyPost.GetPlaceOfPurchaseString();
            if (MyPost.HasSupplier() && MyPost.GetMerchandise().HasSupplier())
            {
                SupplierComboBox.Enabled = false;
            }
            merchandiseCombobox1.Enabled = false;
            CurrencyCombobox.SetSelectedCurrency(MyPost.GetCurrencyId());
            ApprArrivalTextBox.Text = MyPost.GetPredictedArrival();
            InvoiceInstCheckBox.Checked = MyPost.GetInvoiceInst();
            InvoiceClinCheckBox.Checked = MyPost.GetInvoiceClin();
            NoInvoiceCheckBox.Checked =  MyPost.IsInvoceAbsent();
            InitCustomerNumberCombobox();
            UpdateCustomerNumber();
        }

        private void FixComboboxSelection()
        {
            if (SupplierComboBox.InvokeRequired || merchandiseCombobox1.InvokeRequired)
            {
                FixComboboxesSelectionCallback f = new FixComboboxesSelectionCallback(FixComboboxSelection);
                if (!this.IsDisposed)
                {
                    this.Invoke(f);
                }
            }
            else
            {
                SupplierComboBox.SelectionStart = SupplierComboBox.Text.Length;
                merchandiseCombobox1.SelectionStart = merchandiseCombobox1.Text.Length;    
            }
        }

        private void SetSupplier()
        {
            if (MyPost.HasSupplier())
            {
                SupplierComboBox.SetSelectedIdentity(MyPost.GetSupplier().GetId());
            }
            else
            {
                SupplierComboBox.Text = "No Supplier selected";
            }        
        }

        private void InitOrderMode()
        {
            this.Text = "Sign order";
            this.SaveButton.Text = "Sign";
            this.SaveButton.Enabled = true;
            SetSupplier();
            MerchandiseCommentTextBox.Text = MyPost.GetMerchandise().GetComment();
            AmountTextBox.Text = MyPost.GetAmountString();
            CommentTextBox.Text = MyPost.GetComment();
            OrderingUnitComboBox.SelectedItem = MyPost.GetPlaceOfPurchaseString();
            DeliveryDeviationTextBox.Text = MyPost.GetDeliveryDeviation();
            ApprPrizeTextBox.Text = MyPost.GetPriceWithCurrencyString();
            SupplierComboBox.Enabled = false;
            merchandiseCombobox1.Enabled = false;
            InvoiceClinCheckBox.Checked = MyPost.GetInvoiceClin();
            InvoiceInstCheckBox.Checked = MyPost.GetInvoiceInst();
            ApprArrivalTextBox.Text = MyPost.GetPredictedArrival();
            OrderDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            OrdererUserComboBox.SetSelectedIdentity(UserManager.GetCurrentUser());
        }

        private void MyCloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private Post.InvoiceStatus GetInvoiceStatus()
        {
            if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                return Post.InvoiceStatus.Incoming;
            }
            else if (InvoiceOKCheckBox.Checked)
            {
                return Post.InvoiceStatus.Ok;
            }
            else
            {
                return Post.InvoiceStatus.NotOk;
            }
        }

        private bool IsStatusUpdated()
        {
            return GetPostStatus() != MyPost.GetPostStatus();
        }

        private bool IsUpdated()
        {
            int currencyId = PlattformOrdManData.NO_ID;
            decimal prize, finalPrize;
            bool invoiceOk = true;
            int amount = PlattformOrdManData.NO_COUNT;
            int customerNumberId = PlattformOrdManData.NO_ID;

            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                amount = PlattformOrdManData.NO_COUNT;
            }

            if (CustomerNumberComboBox.SelectedIndex > -1 &&
                CustomerNumberComboBox.SelectedItem is CustomerNumber)
            {
                customerNumberId = ((CustomerNumber)CustomerNumberComboBox.SelectedItem).GetId();
            }

            if (MyPost.GetInvoiceStatus() == Post.InvoiceStatus.NotOk)
            {
                invoiceOk = false;
            }
            if (ApprPrizeTextBox.Text.Trim() == "")
            {
                prize = -1;
            }
            else if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out prize))
            {
                return false;
            }
            if (FinalPrizeTextBox.Text.Trim() == "")
            {
                finalPrize = -1;
            }
            else if(!GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out finalPrize))
            {
                return false;
            }
            if (CurrencyCombobox.HasSelectedCurrency())
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }

            return (amount != MyPost.GetAmount() ||
                CommentTextBox.Text != MyPost.GetComment()||
                DeliveryDeviationTextBox.Text != MyPost.GetDeliveryDeviation() ||
                prize != MyPost.GetApprPrizeDecimal() ||
                (InvoiceInstCheckBox.Checked != MyPost.GetInvoiceInst()) ||
                (InvoiceClinCheckBox.Checked != MyPost.GetInvoiceClin()) ||
                (NoInvoiceCheckBox.Checked != MyPost.IsInvoceAbsent()) ||
                ApprArrivalTextBox.Text.Trim() != MyPost.GetPredictedArrival()||
                currencyId != MyPost.GetCurrencyId() ||
                BookerUserComboBox.GetSelectedIdentityId() != MyPost.GetBookerId() ||
                OrdererUserComboBox.GetSelectedIdentityId() != MyPost.GetOrderUserId() ||
                ArrivalSignUserComboBox.GetSelectedIdentityId() != MyPost.GetArrivalSignUserId() ||
                InvoiceCheckerUserComboBox.GetSelectedIdentityId() != MyPost.GetInvoiceUserId() ||
                GetBookDate().Date != MyPost.GetBookDateDT().Date ||
                GetOrderDate().Date != MyPost.GetOrderDate().Date ||
                GetArrivalDate() != MyPost.GetArrivalDate() || 
                GetInvoiceDate() != MyPost.GetInvoiceDate() ||
                InvoiceOKCheckBox.Checked != invoiceOk || 
                finalPrize != MyPost.GetFinalPrize() ||
                InvoiceNumberTextBox.Text.Trim() != MyPost.GetInvoiceNumber() ||
                ConfirmedOrderUserComboBox.GetSelectedIdentityId() != MyPost.GetConfirmedOrderUserId() ||
                GetConfirmedOrderDate().Date != MyPost.GetConfirmedOrderDate().Date ||
                PurchaseOrderNoTextBox.Text.Trim() != MyPost.GetPurchaseOrderNo() ||
                SalesOrdernoTextBox.Text.Trim() != MyPost.GetSalesOrderNo() ||
                customerNumberId != MyPost.GetCustomerNumberId() ||
                IsOrderingUnitUpdated() ||
                IsSupplierUpdated()
                );
        }

        private bool IsCustomerNumberNotHandled()
        {
            return SupplierComboBox.HasSelectedSupplier() &&
                SupplierComboBox.GetSelectedSupplier().GetCustomerNumbersForCurrentUserGroup().Count > 0 &&
                (CustomerNumberComboBox.SelectedIndex == -1 ||
                (CustomerNumberComboBox.SelectedIndex > -1 && !(CustomerNumberComboBox.SelectedItem is CustomerNumber)));
        }

        private bool IsOrderingUnitUpdated()
        {
            if (OrderingUnitComboBox.SelectedIndex > PlattformOrdManData.NO_COUNT)
            {
                return ((string)OrderingUnitComboBox.SelectedItem) != MyPost.GetPlaceOfPurchaseString();
            }
            else
            {
                return false;
            }
        }

        private bool IsSupplierUpdated()
        { 
            if(SupplierComboBox.SelectedIndex <= 0 && MyPost.HasSupplier())
            {
                return true;
            }
            else if (SupplierComboBox.SelectedIndex <= 0 && !MyPost.HasSupplier())
            {
                return false;
            }
            else
            {
                return SupplierComboBox.Text != MyPost.GetSupplierName();
            }
        }

        private void ResetProductDetails()
        {
            MerchandiseCommentTextBox.Text = "";
            AmountTextBox.Text = "";
            ArticleNumberTextBox.Text = "";
            ApprPrizeTextBox.Text = "";
            InvoiceClinCheckBox.Checked = false;
            InvoiceInstCheckBox.Checked = false;
            NoInvoiceCheckBox.Checked = false;
            ApprArrivalTextBox.Text = "";
            CommentTextBox.Text = "";
            DeliveryDeviationTextBox.Text = "";
            CurrencyCombobox.SetSelectedCurrency(Data.CurrencyManager.GetDefaultCurrency());
        }

        private void merchandiseCombobox1_OnMyControlledSelectedIndexChanged()
        {
            Merchandise merchandise;
            HandleSaveButtonEnabled();
            if (merchandiseCombobox1.HasSelectedIdentity())
            {
                ShowProductButton.Enabled = true;
            }
            else
            {
                ShowProductButton.Enabled = false;
                ResetProductDetails();
                return;
            }
            ApprPrizeTextBox.Text = merchandiseCombobox1.GetSelectedMerchandise().GetApprPrizeString();
            ArticleNumberTextBox.Text = merchandiseCombobox1.GetSelectedMerchandise().GetCurrentArticleNumberString();
            MerchandiseCommentTextBox.Text = merchandiseCombobox1.GetSelectedMerchandise().GetComment();
            merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            if (IsNotNull(merchandise.GetCurrency()))
            {
                CurrencyCombobox.SetSelectedCurrency(merchandise.GetCurrency());
            }
            if (IsNull(SupplierComboBox.GetSelectedIdentity()) && merchandise.HasSupplier())
            {
                SupplierComboBox.SetSelectedIdentity(merchandise.GetSupplierId());
                merchandiseCombobox1.OnMyControlledSelectedIndexChanged -= MyControlledSelectedIndexChangedForMerchanidse;
                merchandiseCombobox1.SetSelectedIdentity(merchandise);
                merchandiseCombobox1.OnMyControlledSelectedIndexChanged += MyControlledSelectedIndexChangedForMerchanidse;
            }
            StorageTextBox.Text = merchandise.GetStorage();
        }

        public Post GetPost()
        {
            return MyPost;
        }

        private bool IsProductInLine()
        {
            Merchandise merchandise;
            merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            if (IsNull(merchandise))
            {
                return false;
            }
            foreach (Post post in PostManager.GetNotFinishedPosts())
            {
                if (IsNotNull(post.GetMerchandise()) &&
                    post.GetMerchandise().GetId() == merchandise.GetId())
                {
                    return true;
                }
            }
            return false;
        }

        private void HandleEmtyAmount()
        {
            if (AmountTextBox.Text.Trim() == "")
            {
                AmountTextBox.Text = "1";
            }
        }

        private bool IsAmountNumeric()
        { 
            int amount;
            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                return false;
            }
            return true;
        }

        private bool CreatePost()
        {
            Supplier supplier;
            Merchandise merchandise= null;
            User currentUser;
            int bookerUserId;
            decimal prize, finalPrize;
            string placeOfPurchase;
            int currencyId = PlattformOrdManData.NO_ID, supplierId = PlattformOrdManData.NO_ID;
            bool dummy;
            int customerNumberId = PlattformOrdManData.NO_ID;
            CustomerNumber custNum;
            string popStr;
            if (IsNotNull(CurrencyCombobox.GetSelectedCurrency()))
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }
            DialogResult = DialogResult.Cancel;
            if (CustomerNumberComboBox.SelectedIndex > -1 &&
                CustomerNumberComboBox.SelectedItem is CustomerNumber)
            {
                custNum = (CustomerNumber)CustomerNumberComboBox.SelectedItem;
                if (custNum != null)
                {
                    customerNumberId = custNum.GetId();
                }
            }
            if (ApprPrizeTextBox.Text.Trim() == "")
            {
                prize = -1;
            }
            else if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out prize))
            {
                ShowWarning("Error, the appriximate prize could not be converted to a number, create canceled!");
                return false;
            }
            if (FinalPrizeTextBox.Text.Trim() == "")
            {
                finalPrize = -1;
            }
            else if (!GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out finalPrize))
            {
                ShowWarning("Error, the final prize could not be converted to a number, create canceled!");
                return false;            
            }

            supplier = (Supplier)SupplierComboBox.GetSelectedIdentity();
            if (IsNotNull(supplier))
            {
                supplierId = supplier.GetId();
            }
            HandleEmtyAmount();
            if (!IsAmountNumeric())
            {
                ShowWarning("Error, the amount is not numeric, create canceled!");
                return false;
            }
            merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            currentUser = UserManager.GetCurrentUser();
            bookerUserId = BookerUserComboBox.GetSelectedIdentityId();
            if (IsNull(merchandise)|| IsNull(currentUser))
            {
                ShowWarning("Error, either the supplier, merchandise or the active user were not set, create canceled!");
                return false;
            }

            if (IsCustomerNumberNotHandled() &&
                OrdererUserComboBox.HasSelectedIdentity() &&
                MessageBox.Show("Customer number has not been chosen, continue anyway?",
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
            {
                return false;
            }

            if (OrderingUnitComboBox.SelectedIndex == PlattformOrdManData.NO_COUNT)
            {
                return false;
            }
            else
            {
                // Want to have the string repr of the enum 'Other' not 'Plattform unspec.'
                popStr = PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem).ToString();
            }

            UpdateMerchandisePrize(finalPrize, currencyId, out prize);
            MyPost = PostManager.CreatePost(merchandise.GetCurrentArticleNumberId(), bookerUserId, GetCommentFromForm(),
                merchandise.GetId(), supplierId, GetAmountFromForm(), prize, currencyId, InvoiceInstCheckBox.Checked,
                InvoiceClinCheckBox.Checked, NoInvoiceCheckBox.Checked, GetInvoiceNumberFromForm(), 
                finalPrize, GetDeliveryDeviationFromForm(), PurchaseOrderNoTextBox.Text, SalesOrdernoTextBox.Text,
                popStr, customerNumberId);
            if (GetDate(BookDateTextBox.Text.Trim()).Date != DateTime.Now.Date ||
                OrdererUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                return UpdatePost(out dummy, false);
            }
            placeOfPurchase = (string)OrderingUnitComboBox.SelectedItem;
            PlattformOrdManData.Configuration.PlaceOfPurchase = 
                PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem);
            if (!PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Contains(placeOfPurchase))
            {
                PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Add(placeOfPurchase);
            }
            return true;
        }

        private int GetAmountFromForm()
        {
            int amount;
            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                amount = PlattformOrdManData.NO_COUNT;
            }
            return amount;
        }

        private string GetInvoiceNumberFromForm()
        {
            return InvoiceNumberTextBox.Text.Trim();
        }

        private string GetDeliveryDeviationFromForm()
        {
            return DeliveryDeviationTextBox.Text.Trim();
        }

        private string GetCommentFromForm()
        {
            return CommentTextBox.Text.Trim();
        }

        private bool CheckDateText(string dateStr, string fieldName)
        {
            DateTime dateTime;
            if (!DateTime.TryParse(dateStr, PlattformOrdManData.MyCultureInfo, MyDateTimeStyle, out dateTime))
            {
                ShowWarning("Error, the " + fieldName + " must be in the format YYYY-MM-DD, update canceled!");
                return false;
            }
            return true;
        }

        private bool CheckDates()
        {
            if (ApprArrivalTextBox.Text.Trim() != "" &&
                !CheckDateText(ApprArrivalTextBox.Text.Trim(), "approximate arrival date"))
            {
                return false;
            }
            if (!CheckDateText(BookDateTextBox.Text.Trim(), "book date"))
            {
                return false;
            }
            if (OrdererUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID &&
                !CheckDateText(OrderDateTextBox.Text.Trim(), "order date"))
            {
                return false;
            }
            if (ConfirmedOrderUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID &&
                !CheckDateText(ConfirmedOrderDateTextBox.Text.Trim(), "order confirmation date"))
            {
                return false;
            }
            if (ArrivalSignUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID &&
                !CheckDateText(ArrivalDateTextBox.Text.Trim(), "arrival date"))
            {
                return false;
            }
            if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID &&
                !CheckDateText(InvoiceCheckDateTextBox.Text.Trim(), "invoice check date"))
            {
                return false;
            }
            return true;
        }

        private Post.PostStatus GetPostStatus()
        {
            if (OrdererUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                return Post.PostStatus.Booked;
            }
            else if (ConfirmedOrderUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                return Post.PostStatus.Ordered;
            }
            else if (ArrivalSignUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                return Post.PostStatus.ConfirmedOrder;
            }
            else if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                return Post.PostStatus.Confirmed;
            }
            else
            {
                return Post.PostStatus.Completed;
            }
        }

        private bool IsNewSortOrderNeeded()
        { 
            //If book date is altered, or
            //if post status changed from anything to completed, or
            // if post status changed from completed to anything
            if(IsNull(MyPost))
            {
                return true;
            }
            else if((GetPostStatus() == Post.PostStatus.Completed &&
                    MyPost.GetPostStatus() != Post.PostStatus.Completed) ||
                    (GetPostStatus() != Post.PostStatus.Completed &&
                    MyPost.GetPostStatus() == Post.PostStatus.Completed) ||
                    (GetDate(BookDateTextBox.Text.Trim()).Date != MyPost.GetBookDateDT().Date))
            {
                return true;
            }
            return false;
        }

        private DateTime GetDate(string dateString)
        { 
            DateTime theDate;
            if (!DateTime.TryParse(dateString, out theDate))
            {
                theDate = new DateTime();
            }
            return theDate;
        }

        private bool IsInvoiceCategoryMissing()
        {
            return MyPost.GetMerchandise().GetInvoiceCagegoryId() == PlattformOrdManData.NO_ID;
        }

        private bool UpdatePost(out bool newSortOrder, bool askCustNumberHandling)
        {
            int bookerUserId, ordererUserId, arrivalSignUserId, invoicerUserId, confirmOrderUserId;
            decimal prize, finalPrize;
            string str;
            newSortOrder = false;
            int currencyId = PlattformOrdManData.NO_ID;
            int customerNumberId = PlattformOrdManData.NO_ID;
            CustomerNumber custNum;
            string popStr;

            popStr = PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem).ToString();
            if (IsNotNull(CurrencyCombobox.GetSelectedCurrency()))
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }
            if (CustomerNumberComboBox.SelectedIndex > -1 &&
                CustomerNumberComboBox.SelectedItem is CustomerNumber)
            {
                custNum = (CustomerNumber)CustomerNumberComboBox.SelectedItem;
                if (custNum != null)
                {
                    customerNumberId = custNum.GetId();
                }
            }

            if (ApprPrizeTextBox.Text.Trim() == "")
            {
                prize = -1;
            }
            else if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out prize))
            {
                ShowWarning("Error, the prize could not be converted to a number, update canceled!");
                return false;
            }
            if (FinalPrizeTextBox.Text.Trim() == "")
            {
                finalPrize = -1;
            }
            else if (!GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out finalPrize))
            {
                ShowWarning("Error, the prize could not be converted to a number, update canceled!");
                return false;                
            }

            if (!CheckDates())
            {
                return false;
            }
            if (!CheckDateUserConsistency())
            {
                return false;
            }
            if (InvoiceOKCheckBox.Checked == false && NoInvoiceCheckBox.Checked)
            {
                str = "Please choose either ''No invoice'' or ''Invoice not OK''!";
                MessageBox.Show(str, "Mismatching invoice status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ((GetInvoiceStatus() == Post.InvoiceStatus.NotOk || GetInvoiceStatus() == Post.InvoiceStatus.Ok) &&
                !NoInvoiceCheckBox.Checked &&
                IsInvoiceCategoryMissing())
            {
                InvoiceCategoryDialog invoiceCategoryDialog;
                invoiceCategoryDialog = new InvoiceCategoryDialog(MyPost);
                if (invoiceCategoryDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return false;
                }
            }
            HandleEmtyAmount();
            if (!IsAmountNumeric())
            {
                ShowWarning("Error, the amount is not numeric, update canceled!");
                return false;
            }

            if (askCustNumberHandling &&
                IsCustomerNumberNotHandled() &&
                OrdererUserComboBox.HasSelectedIdentity() &&
                MessageBox.Show("Customer number has not been chosen, continue anyway?",
                    "Unhandled customer number", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Cancel)
            {
                return false;
            }

            newSortOrder = IsNewSortOrderNeeded();
            UpdateMerchandisePrize(finalPrize, currencyId, out prize);
            bookerUserId = BookerUserComboBox.GetSelectedIdentityId();
            ordererUserId = OrdererUserComboBox.GetSelectedIdentityId();
            confirmOrderUserId = ConfirmedOrderUserComboBox.GetSelectedIdentityId();
            arrivalSignUserId = ArrivalSignUserComboBox.GetSelectedIdentityId();
            invoicerUserId = InvoiceCheckerUserComboBox.GetSelectedIdentityId();
            MyPost.UpdatePost(GetCommentFromForm(), prize, GetAmountFromForm(),
                InvoiceClinCheckBox.Checked, InvoiceInstCheckBox.Checked, GetApprArrivalDate(), GetInvoiceStatus().ToString(),
                NoInvoiceCheckBox.Checked,currencyId, bookerUserId, GetBookDate(), ordererUserId, GetOrderDate(),
                arrivalSignUserId, GetArrivalDate(), invoicerUserId, GetInvoiceDate(), MyPost.GetMerchandise().GetCurrentArticleNumberId(),
                SupplierComboBox.GetSelectedIdentityId(), GetInvoiceNumberFromForm(), finalPrize, 
                GetConfirmedOrderDate(), confirmOrderUserId, GetDeliveryDeviationFromForm(),
                PurchaseOrderNoTextBox.Text, SalesOrdernoTextBox.Text, popStr,
                customerNumberId);
            if (MyPost.IsInvoceAbsent() && MyPost.GetPostStatus() == Post.PostStatus.Confirmed)
            {
                MyPost.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok, MyPost.IsInvoceAbsent());
            }
            return true;
        }

        private bool CheckDateUserConsistency()
        {
            string str;
            if (BookDateTextBox.Text.Trim().Length > 0 && 
                BookerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                str = "Error, the booking date is filled in but no booker!";
                MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (OrderDateTextBox.Text.Trim().Length > 0 &&
                OrdererUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                str = "Error, the order date is filled in but no order person!";
                MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;            
            }
            if (ConfirmedOrderDateTextBox.Text.Trim().Length > 0 &&
                ConfirmedOrderUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                str = "Error, the order confirmation date is filled in but no person to sign it!";
                MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;            
            }
            if (ArrivalDateTextBox.Text.Trim().Length > 0 &&
                ArrivalSignUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                str = "Error, the arrival date is filled in but no arrival sign. person!";
                MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (InvoiceCheckDateTextBox.Text.Trim().Length > 0 &&
                InvoiceCheckerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                str = "Error, the invoice check date is filled in but no invoice check person!";
                MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private DateTime GetApprArrivalDate()
        {
            DateTime dateTime;
            if (!DateTime.TryParse(ApprArrivalTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;        
        }

        private DateTime GetBookDate()
        {
            DateTime dateTime;
            if(!DateTime.TryParse(BookDateTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;        
        }

        private DateTime GetOrderDate()
        {
            DateTime dateTime;
            if (!DateTime.TryParse(OrderDateTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;        
        }

        private DateTime GetConfirmedOrderDate()
        {
            DateTime dateTime;
            if (!DateTime.TryParse(ConfirmedOrderDateTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;
        }

        private DateTime GetArrivalDate()
        {
            DateTime dateTime;
            if (!DateTime.TryParse(ArrivalDateTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;        
        }

        private DateTime GetInvoiceDate()
        {
            DateTime dateTime;
            if (!DateTime.TryParse(InvoiceCheckDateTextBox.Text.Trim(), out dateTime))
            {
                dateTime = new DateTime();
            }
            return dateTime;        
        }

        private bool SignPost(out bool newSortOrder)
        {
            if (!UpdatePost(out newSortOrder, true))
            {
                return false;
            }
            MyPost.OrderPost(UserManager.GetCurrentUser().GetId());
            return true;
        }

        private bool UpdateMerchandisePrize(decimal prize, int currencyId, out decimal newApprPrize)
        {
            Merchandise merchandise = null;
            string str, prizeStr;
            merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out newApprPrize))
            {
                newApprPrize = -1;
            }
            if ((prize != PlattformOrdManData.NO_COUNT && merchandise.GetApprPrize() != prize) ||
                merchandise.GetCurrencyId() != currencyId)
            {
                prizeStr = PlattformOrdMan.Data.CurrencyManager.GetCurrency(currencyId).GetPriceWithCurrencyString(prize);
                if (prize != PlattformOrdManData.NO_COUNT)
                {
                    newApprPrize = prize;
                }
                if (IsNotNull(MyPost))
                {
                    MyPost.SetApprPrizeLocal(newApprPrize);                
                }
                str = "Change default price for product ''" + merchandise.GetIdentifier() + "'' to " + prizeStr + "?";
                if (MessageBox.Show(str, "Price update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    merchandise.SetApprPrize(prize);
                    merchandise.SetCurrencyId(currencyId);
                    merchandise.Set();
                    if (IsNotNull(MyPost))
                    {
                        MyPost.ResetMerchanidse();
                    }
                    return true;
                }
            }
            return false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool newSortOrder = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (MyUpdateMode)
                {
                    case PostUpdateMode.Create:
                        if (IsProductInLine() &&
                                MessageBox.Show("There is already an un-completed order with this product in line, proceed anyway?",
                                "Product already in line", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                        {
                            this.Cursor = Cursors.Default;
                            Close();
                            return;
                        }
                        if (!CreatePost())
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        break;
                    case PostUpdateMode.Edit:
                        if (!UpdatePost(out newSortOrder, true))
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        break;
                    case PostUpdateMode.OrderSign:
                        if (!SignPost(out newSortOrder))
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        break;
                }
                if (IsNotNull(OnPostUpdate))
                {
                    OnPostUpdate(MyPost, new UpdateHandlerEventArgs(newSortOrder));
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;            
            }
            Close();
        }

        private bool HasFinalPrize()
        {
            decimal dummy;
            return FinalPrizeTextBox.Text.Trim() != "" &&
                GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out dummy);
        }

        private bool HasApprPrize()
        {
            decimal dummy;
            return ApprPrizeTextBox.Text.Trim() != "" &&
                GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out dummy);        
        }

        private decimal GetFinalPrize()
        {
            decimal prize;
            if (!GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out prize))
            {
                prize = PlattformOrdManData.NO_COUNT;
            }
            return prize;
        }

        private decimal GetApprPrize()
        {
            decimal prize;
            if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out prize))
            {
                prize = PlattformOrdManData.NO_COUNT;
            }
            return prize;
        }

        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
            SetTotalPrize();
        }

        private void SetTotalPrize()
        {
            int amount;
            decimal prize = PlattformOrdManData.NO_COUNT;
            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                amount = PlattformOrdManData.NO_COUNT;
            }
            if (HasFinalPrize() &&
                amount != PlattformOrdManData.NO_COUNT)
            {
                prize = ((decimal)amount) * GetFinalPrize();
            }
            else if (HasApprPrize() &&
                amount != PlattformOrdManData.NO_COUNT)
            {
                prize = ((decimal)amount) * GetApprPrize();
            }

            if (prize != PlattformOrdManData.NO_COUNT &&
                CurrencyCombobox.HasSelectedCurrency())
            {
                TotalPrizeTextBox.Text = CurrencyCombobox.GetSelectedCurrency().GetPriceWithCurrencyString(prize);
            }
            else if (prize != PlattformOrdManData.NO_COUNT &&
                !CurrencyCombobox.HasSelectedCurrency())
            {
                TotalPrizeTextBox.Text = prize.ToString();
            }
            else
            {
                TotalPrizeTextBox.Text = "";
            }            
        }

        private void ApprPrizeTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
            if (!HasFinalPrize())
            {
                SetTotalPrize();
            }
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void HandleSaveButtonEnabled()
        {
            if (MyUpdateMode == PostUpdateMode.Edit)
            {
                if (IsUpdated())
                {
                    SaveButton.Enabled = true;
                }
                else
                {
                    SaveButton.Enabled = false;
                }
            }
            else if (MyUpdateMode == PostUpdateMode.Create)
            {
                if (merchandiseCombobox1.SelectedIndex != -1)
                {
                    SaveButton.Enabled = true;
                }
                else
                {
                    SaveButton.Enabled = false;
                }
            }
        }

        private void InvoiceInstCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void InvoiceClinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void ApprArrivalTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void NoInvoiceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoInvoiceCheckBox.Checked)
            {
                InvoiceClinCheckBox.Checked = false;
                InvoiceClinCheckBox.Enabled = false;
                InvoiceInstCheckBox.Checked = false;
                InvoiceInstCheckBox.Enabled = false;
            }
            else
            {
                InvoiceInstCheckBox.Enabled = true;
                InvoiceClinCheckBox.Enabled = true;
            }
            HandleSaveButtonEnabled();
        }

        private void EditCurrencyButton_Click(object sender, EventArgs e)
        {
           Currency currentCurrency;
            ShowCurrenciesDialog showCurrenciesDialog;
            showCurrenciesDialog = new ShowCurrenciesDialog();
            currentCurrency = CurrencyCombobox.GetSelectedCurrency();
            if (showCurrenciesDialog.ShowDialog() == DialogResult.OK)
            {
                CurrencyCombobox.LoadCurrencies();
                if (IsNotNull(currentCurrency))
                {
                    CurrencyCombobox.SetSelectedCurrency(currentCurrency);
                }
            }
        }

        private void BookerUserComboBox_OnMyControlledSelectedIndexChanged()
        {
            HandleSaveButtonEnabled();
            if (BookerUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                OrdererUserComboBox.Enabled = true;
                OrderDateTextBox.Enabled = true;
                if (BookDateTextBox.Text == "")
                {
                    BookDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                ConfirmedOrderUserComboBox.SelectedIndex = -1;
                ConfirmedOrderUserComboBox.Enabled = false;
                ConfirmedOrderDateTextBox.Text = "";
                ConfirmedOrderDateTextBox.Enabled = false;
                OrdererUserComboBox.SelectedIndex = -1;
                OrdererUserComboBox.Enabled = false;
                OrderDateTextBox.Text = "";
                OrderDateTextBox.Enabled = false;
                ArrivalSignUserComboBox.SelectedIndex = -1;
                ArrivalSignUserComboBox.Enabled = false;
                ArrivalDateTextBox.Text = "";
                InvoiceCheckDateTextBox.Text = "";
                InvoiceCheckerUserComboBox.SelectedIndex = -1;
                InvoiceCheckerUserComboBox.Enabled = false;            
            }
        }

        private void OrdererUserComboBox_OnMyControlledSelectedIndexChanged()
        {
            HandleSaveButtonEnabled();
            if (OrdererUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                ConfirmedOrderUserComboBox.Enabled = true;
                ConfirmedOrderDateTextBox.Enabled = true;
                if (OrderDateTextBox.Text.Trim() == "")
                {
                    OrderDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                OrderDateTextBox.Text = "";
                ConfirmedOrderUserComboBox.SelectedIndex = -1;
                ConfirmedOrderUserComboBox.Enabled = false;
                ConfirmedOrderDateTextBox.Text = "";
                ConfirmedOrderDateTextBox.Enabled = false;
                ArrivalSignUserComboBox.SelectedIndex = -1;
                ArrivalSignUserComboBox.Enabled = false;
                ArrivalDateTextBox.Text = "";
                ArrivalDateTextBox.Enabled = false;
                InvoiceCheckDateTextBox.Text = "";
                InvoiceCheckerUserComboBox.SelectedIndex = -1;
                InvoiceCheckerUserComboBox.Enabled = false;
                InvoiceCheckDateTextBox.Enabled = false;
            }
        }

        private void ConfirmedOrderUserComboBox_OnMyControlledSelectedIndexChanged()
        {
            HandleSaveButtonEnabled();
            if (ConfirmedOrderUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                ArrivalSignUserComboBox.Enabled = true;
                ArrivalDateTextBox.Enabled = true;
                if (ConfirmedOrderDateTextBox.Text.Trim() == "")
                {
                    ConfirmedOrderDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                ConfirmedOrderDateTextBox.Text  = "";
                ArrivalSignUserComboBox.SelectedIndex = -1;
                ArrivalSignUserComboBox.Enabled = false;
                ArrivalDateTextBox.Text = "";
                ArrivalDateTextBox.Enabled = false;
                InvoiceCheckDateTextBox.Text = "";
                InvoiceCheckerUserComboBox.SelectedIndex = -1;
                InvoiceCheckerUserComboBox.Enabled = false;
                InvoiceCheckDateTextBox.Enabled = false;
            }
        }


        private void ArrivalSignUserComboBox_OnMyControlledSelectedIndexChanged()
        {
            HandleSaveButtonEnabled();
            if (ArrivalSignUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                InvoiceCheckerUserComboBox.Enabled = true;
                InvoiceCheckDateTextBox.Enabled = true;
                if (ArrivalDateTextBox.Text.Trim() == "")
                {
                    ArrivalDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                ArrivalDateTextBox.Text = "";
                InvoiceCheckDateTextBox.Text = "";
                InvoiceCheckDateTextBox.Enabled = false;
                InvoiceCheckerUserComboBox.SelectedIndex = -1;
                InvoiceCheckerUserComboBox.Enabled = false;
            }
        }

        private void InvoiceCheckerUserComboBox_OnMyControlledSelectedIndexChanged()
        {
            HandleSaveButtonEnabled();
            if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID &&
                InvoiceCheckDateTextBox.Text.Trim() == "")
            {
                InvoiceCheckDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                InvoiceOKCheckBox.Enabled = true;
            }
            else
            {
                InvoiceCheckDateTextBox.Text = "";
                InvoiceOKCheckBox.Enabled = false;
            }
        }

        private void BookDateTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void OrderDateTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void ArrivalDateTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void InvoiceCheckDateTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void InvoiceOKCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void FinalPrizeTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
            SetTotalPrize();
        }

        private void InvoiceNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void ConfirmedOrderDateTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void BookerUserComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DeliveryDeviationTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void ArticleNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CurrencyCombobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ApprArrivalLabel_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ShowSupplierButton_Click(object sender, EventArgs e)
        {
            EditSupplierDialog editSupplierDialog;
            Supplier supplier;
            try
            {
                if (SupplierComboBox.HasSelectedIdentity())
                {
                    supplier = SupplierComboBox.GetSelectedIdentity() as Supplier;
                    editSupplierDialog = new EditSupplierDialog(supplier, UpdateMode.Edit);
                    editSupplierDialog.MdiParent = this.MdiParent;
                    editSupplierDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier", ex);
            }
        }

        private void ShowProductButton_Click(object sender, EventArgs e)
        {
            EditMerchandiseDialog editMerchandiseDialog;
            Merchandise merchandise;
            if (merchandiseCombobox1.HasSelectedMerchandise())
            {
                merchandise = merchandiseCombobox1.GetSelectedMerchandise();
                editMerchandiseDialog = new EditMerchandiseDialog(merchandise, UpdateMode.Edit);
                editMerchandiseDialog.MdiParent = this.MdiParent;
                editMerchandiseDialog.Show();
            }
        }

        private void SalesOrdernoTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void PurchaseOrderNoTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void OrderingUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlaceOfPurchase pop;
            if (OrderingUnitComboBox.SelectedIndex >= 0)
            {
                pop = PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem);
                if (popPrevSel == PlattformOrdManData.NO_COUNT ||
                    PlattformOrdManData.GetGroupCategory(pop) != PlattformOrdManData.GetGroupCategory((PlaceOfPurchase)popPrevSel))
                {
                    InitCustomerNumberCombobox();
                }
            }
            HandleSaveButtonEnabled();
            popPrevSel = OrderingUnitComboBox.SelectedIndex;
        }

        private void SupplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustomerNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }
    }
}