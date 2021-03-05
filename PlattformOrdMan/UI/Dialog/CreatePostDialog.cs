using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Data.PostData;
using CurrencyManager = PlattformOrdMan.Data.CurrencyManager;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class CreatePostDialog : OrdManForm, ISupplierForm, IMerchandiseForm
    {
        public delegate void PostUpdatedHandler(object sender, UpdateHandlerEventArgs e);

        private delegate bool CheckComboxesSelectedCallback();

        private delegate void FixComboboxesSelectionCallback();

        public event PostUpdatedHandler OnPostUpdate;
        private Post _post;
        private readonly PostUpdateMode _updateMode;

        private Component.SearchingCombobox.MyControlledSelectedIndexChanged
            _controlledSelectedIndexChangedForMerchanidse;

        private readonly DateTimeStyles _dateTimeStyles;
        private System.Timers.Timer _fixComboboxSelectionTimer;
        private int _popPrevSel;

        public CreatePostDialog(Post post, PostUpdateMode updateMode)
        {
            InitializeComponent();
            _updateMode = updateMode;
            _post = post;
            _dateTimeStyles = DateTimeStyles.None;
            Init();
        }

        public CreatePostDialog(Post post, PostUpdateMode updateMode, Merchandise merchandice)
        {
            InitializeComponent();
            _updateMode = updateMode;
            _post = post;
            _dateTimeStyles = DateTimeStyles.None;
            Init();
            merchandiseCombobox1.SetSelectedIdentity(merchandice);
        }

        void CreatePostDialog_ResizeEnd(object sender, EventArgs e)
        {
            FixComboboxSelection();
        }

        private void InitOrderingUnitCombobox()
        {
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                OrderingUnitComboBox.Items.Add(PlattformOrdManData.GetPlaceOfPurchaseString(pop));
            }
            var selStr = UserManager.GetCurrentUser().GetPlaceOfPurchaseStringForUser();
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
            if (IsNull(_post) ||
                _post.GetBookDateDT().CompareTo(PlattformOrdManData.CustomerNumberReformDate) > 0)
            {
                InvoiceInstCheckBox.Visible = false;
                InvoiceClinCheckBox.Visible = false;
            }
        }

        private void Init()
        {
            InitInvoiceCheckboxes();
            InitStatusTab();
            FormClosed += (sender, args) =>
            {
                int selectedIndex = tabStatusType.SelectedIndex;
                PlattformOrdManData.Configuration.EditPostTab = (EditPostTab) selectedIndex;
            };
            _popPrevSel = -1;
            _controlledSelectedIndexChangedForMerchanidse =
                merchandiseCombobox1_OnMyControlledSelectedIndexChanged;
            Periodization.EnquiryChanged += Periodization_Changed;
            Account.EnquiryChanged += Account_Changed;
            var suppliers = SupplierManager.GetActiveSuppliersOnly();
            SupplierComboBox.Init(suppliers, "supplier", true);
            SupplierComboBox.LoadIdentitiesWithInfoText();
            SupplierComboBox.OnMyControlledSelectedIndexChanged +=
                SupplierComboBox_OnMyControlledSelectedIndexChanged;
            if (IsNotNull(_post) && !_post.GetMerchandise().IsEnabled())
            {
                merchandiseCombobox1.Init(false, false);
            }
            else
            {
                merchandiseCombobox1.Init(false, true);
            }
            merchandiseCombobox1.LoadIdentitiesWithInfoText();
            merchandiseCombobox1.OnMyControlledSelectedIndexChanged += _controlledSelectedIndexChangedForMerchanidse;
            CurrencyCombobox.SelectedIndexChanged += CurrencyCombobox_SelectedIndexChanged;
            CurrencyCombobox.LoadCurrencies();
            InitStatusFields();
            InitOrderingUnitCombobox();
            switch (_updateMode)
            {
                case PostUpdateMode.Create:
                    AttentionCheckBox.Visible = false;
                    break;
                case PostUpdateMode.Edit:
                    InitEditMode();
                    break;
                case PostUpdateMode.OrderSign:
                    InitOrderMode();
                    break;
            }
            if (IsNotNull(_post) && _post.GetPostStatus() == Post.PostStatus.Completed)
            {
                NoInvoiceCheckBox.Enabled = false;
            }
            ResizeEnd += CreatePostDialog_ResizeEnd;
            Activated += CreatePostDialog_Activated;
            SaveButton.Enabled = false;
            _fixComboboxSelectionTimer = new System.Timers.Timer(100);
            _fixComboboxSelectionTimer.Elapsed += FixComboboxSelectionTimerElapsed;
            _fixComboboxSelectionTimer.Enabled = true;
        }

        public override void ReloadForm()
        {
            var suppliers = SupplierManager.GetActiveSuppliersOnly();
            var products = MerchandiseManager.GetActiveMerchandiseOnly();
            SupplierComboBox.ReloadIdentities(suppliers);
            merchandiseCombobox1.ReloadIdentities(products);
        }

        void CreatePostDialog_Activated(object sender, EventArgs e)
        {
            FixComboboxSelection();
        }


        void FixComboboxSelectionTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsComboboxesSelected())
            {
                _fixComboboxSelectionTimer.Enabled = false;
            }
            FixComboboxSelection();
        }

        private bool IsComboboxesSelected()
        {
            if (SupplierComboBox.InvokeRequired || merchandiseCombobox1.InvokeRequired)
            {
                CheckComboxesSelectedCallback c = IsComboboxesSelected;
                if (!IsDisposed)
                {
                    return (bool)Invoke(c);
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
                (m.WParam == new IntPtr(PlattformOrdManData.SC_MAXIMIZE) ||
                 m.WParam == new IntPtr(PlattformOrdManData.SC_RESTORE) ||
                 m.WParam == new IntPtr(PlattformOrdManData.SC_MINIMIZE)))
            {
                _fixComboboxSelectionTimer.Enabled = true;
            }

            base.WndProc(ref m);
        }

        private void SupplierComboBox_OnMyControlledSelectedIndexChanged()
        {
            int merchandiseId = PlattformOrdManData.NO_ID;
            ShowSupplierButton.Enabled = SupplierComboBox.HasSelectedIdentity();
            if (_updateMode == PostUpdateMode.Create)
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
            BookerUserComboBox.OnMyControlledSelectedIndexChanged +=
                BookerUserComboBox_OnMyControlledSelectedIndexChanged;
            OrdererUserComboBox.OnMyControlledSelectedIndexChanged +=
                OrdererUserComboBox_OnMyControlledSelectedIndexChanged;
            ConfirmedOrderUserComboBox.OnMyControlledSelectedIndexChanged +=
                ConfirmedOrderUserComboBox_OnMyControlledSelectedIndexChanged;
            ArrivalSignUserComboBox.OnMyControlledSelectedIndexChanged +=
                ArrivalSignUserComboBox_OnMyControlledSelectedIndexChanged;
            InvoiceCheckerUserComboBox.OnMyControlledSelectedIndexChanged +=
                InvoiceCheckerUserComboBox_OnMyControlledSelectedIndexChanged;
            if (IsNull(_post))
            {
                BookerUserComboBox.SetSelectedIdentity(UserManager.GetCurrentUser());
                BookDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                BookerUserComboBox.SetSelectedIdentity(_post.GetBooker());
                BookDateTextBox.Text = _post.GetBookDate();
                switch (_post.GetPostStatus())
                {
                    case Post.PostStatus.Booked:
                        break;
                    case Post.PostStatus.Ordered:
                        OrdererUserComboBox.SetSelectedIdentity(_post.GetOrderer());
                        OrderDateTextBox.Text = _post.GetOrderDateString();
                        OrderDateTextBox.Enabled = true;
                        ConfirmedOrderUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.ConfirmedOrder:
                        OrdererUserComboBox.SetSelectedIdentity(_post.GetOrderer());
                        OrderDateTextBox.Text = _post.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(_post.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = _post.GetConfirmedOrderDateString();
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Confirmed:
                        OrdererUserComboBox.SetSelectedIdentity(_post.GetOrderer());
                        OrderDateTextBox.Text = _post.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(_post.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = _post.GetConfirmedOrderDateString();
                        ArrivalDateTextBox.Enabled = true;
                        ArrivalSignUserComboBox.SetSelectedIdentity(_post.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = _post.GetArrivalDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        break;
                    case Post.PostStatus.Completed:
                        OrdererUserComboBox.SetSelectedIdentity(_post.GetOrderer());
                        OrderDateTextBox.Text = _post.GetOrderDateString();
                        ConfirmedOrderUserComboBox.SetSelectedIdentity(_post.GetConfirmedOrderUser());
                        ConfirmedOrderDateTextBox.Text = _post.GetConfirmedOrderDateString();
                        ArrivalSignUserComboBox.SetSelectedIdentity(_post.GetArrivalSignUser());
                        ArrivalDateTextBox.Text = _post.GetArrivalDateString();
                        InvoiceCheckerUserComboBox.SetSelectedIdentity(_post.GetInvoicerUser());
                        InvoiceCheckDateTextBox.Text = _post.GetInvoiceDateString();
                        ArrivalSignUserComboBox.Enabled = true;
                        ArrivalDateTextBox.Enabled = true;
                        ConfirmedOrderDateTextBox.Enabled = true;
                        OrderDateTextBox.Enabled = true;
                        InvoiceCheckDateTextBox.Enabled = true;
                        InvoiceCheckerUserComboBox.Enabled = true;
                        break;
                }
            }
        }

        private void InitEditMode()
        {
            Text = "Update post";
            SaveButton.Text = "Update";
            SetSupplier();
            merchandiseCombobox1.SetSelectedIdentity(_post.GetMerchandise().GetId());
            MerchandiseCommentTextBox.Text = _post.GetMerchandise().GetComment();
            AmountTextBox.Text = _post.GetAmountString();
            CommentTextBox.Text = _post.GetComment();
            PurchaseOrderNoTextBox.Text = _post.GetPurchaseOrderNo();
            SalesOrdernoTextBox.Text = _post.GetSalesOrderNo();
            DeliveryDeviationTextBox.Text = _post.GetDeliveryDeviation();
            ApprPrizeTextBox.Text = _post.GetPriceWithCurrencyString();
            FinalPrizeTextBox.Text = _post.GetFinalPrizeWithCurrencyString();
            InvoiceNumberTextBox.Text = _post.GetInvoiceNumber();
            OrderingUnitComboBox.SelectedItem = _post.GetPlaceOfPurchaseString();
            if (_post.HasSupplier() && _post.GetMerchandise().HasSupplier())
            {
                SupplierComboBox.Enabled = false;
            }
            merchandiseCombobox1.Enabled = false;
            CurrencyCombobox.SetSelectedCurrency(_post.GetCurrencyId());
            ApprArrivalTextBox.Text = _post.GetPredictedArrival();
            InvoiceInstCheckBox.Checked = _post.GetInvoiceInst();
            InvoiceClinCheckBox.Checked = _post.GetInvoiceClin();
            NoInvoiceCheckBox.Checked = _post.IsInvoceAbsent();
            AttentionCheckBox.Checked = _post.AttentionFlag;
            Periodization.SetEnquiry(_post.Periodization);
            Account.SetEnquiry(_post.Account);
        }

        private void FixComboboxSelection()
        {
            if (SupplierComboBox.InvokeRequired || merchandiseCombobox1.InvokeRequired)
            {
                FixComboboxesSelectionCallback f = FixComboboxSelection;
                if (!IsDisposed)
                {
                    Invoke(f);
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
            if (_post.HasSupplier())
            {
                SupplierComboBox.SetSelectedIdentity(_post.GetSupplier().GetId());
            }
            else
            {
                SupplierComboBox.Text = "No Supplier selected";
            }
        }

        private void InitOrderMode()
        {
            Text = "Sign order";
            SaveButton.Text = "Sign";
            SaveButton.Enabled = true;
            SetSupplier();
            MerchandiseCommentTextBox.Text = _post.GetMerchandise().GetComment();
            AmountTextBox.Text = _post.GetAmountString();
            CommentTextBox.Text = _post.GetComment();
            OrderingUnitComboBox.SelectedItem = _post.GetPlaceOfPurchaseString();
            DeliveryDeviationTextBox.Text = _post.GetDeliveryDeviation();
            ApprPrizeTextBox.Text = _post.GetPriceWithCurrencyString();
            SupplierComboBox.Enabled = false;
            merchandiseCombobox1.Enabled = false;
            InvoiceClinCheckBox.Checked = _post.GetInvoiceClin();
            InvoiceInstCheckBox.Checked = _post.GetInvoiceInst();
            ApprArrivalTextBox.Text = _post.GetPredictedArrival();
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
            return InvoiceCheckerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID
                ? Post.InvoiceStatus.Incoming
                : Post.InvoiceStatus.Ok;
        }

        private bool IsUpdated()
        {
            int currencyId = PlattformOrdManData.NO_ID;
            decimal prize, finalPrize;
            int amount;
            int customerNumberId = PlattformOrdManData.NO_ID;

            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                amount = PlattformOrdManData.NO_COUNT;
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
            else if (!GetPrizeDecimal(GetPrizeString(FinalPrizeTextBox.Text), out finalPrize))
            {
                return false;
            }
            if (CurrencyCombobox.HasSelectedCurrency())
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }

            return (amount != _post.GetAmount() ||
                    CommentTextBox.Text != _post.GetComment() ||
                    DeliveryDeviationTextBox.Text != _post.GetDeliveryDeviation() ||
                    prize != _post.GetApprPrizeDecimal() ||
                    (InvoiceInstCheckBox.Checked != _post.GetInvoiceInst()) ||
                    (InvoiceClinCheckBox.Checked != _post.GetInvoiceClin()) ||
                    (NoInvoiceCheckBox.Checked != _post.IsInvoceAbsent()) ||
                    ApprArrivalTextBox.Text.Trim() != _post.GetPredictedArrival() ||
                    currencyId != _post.GetCurrencyId() ||
                    BookerUserComboBox.GetSelectedIdentityId() != _post.GetBookerId() ||
                    OrdererUserComboBox.GetSelectedIdentityId() != _post.GetOrderUserId() ||
                    ArrivalSignUserComboBox.GetSelectedIdentityId() != _post.GetArrivalSignUserId() ||
                    InvoiceCheckerUserComboBox.GetSelectedIdentityId() != _post.GetInvoiceUserId() ||
                    GetBookDate().Date != _post.GetBookDateDT().Date ||
                    GetOrderDate().Date != _post.GetOrderDate().Date ||
                    GetArrivalDate() != _post.GetArrivalDate() ||
                    GetInvoiceDate() != _post.GetInvoiceDate() ||
                    finalPrize != _post.GetFinalPrize() ||
                    InvoiceNumberTextBox.Text.Trim() != _post.GetInvoiceNumber() ||
                    ConfirmedOrderUserComboBox.GetSelectedIdentityId() != _post.GetConfirmedOrderUserId() ||
                    GetConfirmedOrderDate().Date != _post.GetConfirmedOrderDate().Date ||
                    PurchaseOrderNoTextBox.Text.Trim() != _post.GetPurchaseOrderNo() ||
                    SalesOrdernoTextBox.Text.Trim() != _post.GetSalesOrderNo() ||
                    IsOrderingUnitUpdated() ||
                    IsSupplierUpdated() ||
                    AttentionCheckBox.Checked != _post.AttentionFlag ||
                    Periodization.GetEnquiry() != _post.Periodization ||
                    Account.GetEnquiry() != _post.Account
            );
        }

        private bool IsOrderingUnitUpdated()
        {
            if (OrderingUnitComboBox.SelectedIndex > PlattformOrdManData.NO_COUNT)
            {
                return ((string)OrderingUnitComboBox.SelectedItem) != _post.GetPlaceOfPurchaseString();
            }
            else
            {
                return false;
            }
        }

        private bool IsSupplierUpdated()
        {
            if (SupplierComboBox.SelectedIndex <= 0 && _post.HasSupplier())
            {
                return true;
            }
            else if (SupplierComboBox.SelectedIndex <= 0 && !_post.HasSupplier())
            {
                return false;
            }
            else
            {
                return SupplierComboBox.Text != _post.GetSupplierName();
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
            CurrencyCombobox.SetSelectedCurrency(CurrencyManager.GetDefaultCurrency());
        }

        private void merchandiseCombobox1_OnMyControlledSelectedIndexChanged()
        {
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
            var merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            if (IsNotNull(merchandise.GetCurrency()))
            {
                CurrencyCombobox.SetSelectedCurrency(merchandise.GetCurrency());
            }
            if (IsNull(SupplierComboBox.GetSelectedIdentity()) && merchandise.HasSupplier())
            {
                SupplierComboBox.SetSelectedIdentity(merchandise.GetSupplierId());
                merchandiseCombobox1.OnMyControlledSelectedIndexChanged -= _controlledSelectedIndexChangedForMerchanidse;
                merchandiseCombobox1.SetSelectedIdentity(merchandise);
                merchandiseCombobox1.OnMyControlledSelectedIndexChanged += _controlledSelectedIndexChangedForMerchanidse;
            }
            StorageTextBox.Text = merchandise.GetStorage();
        }

        private bool IsProductInLine()
        {
            var merchandise = merchandiseCombobox1.GetSelectedMerchandise();
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
            decimal prize, finalPrize;
            int currencyId = PlattformOrdManData.NO_ID, supplierId = PlattformOrdManData.NO_ID;
            string popStr;
            if (IsNotNull(CurrencyCombobox.GetSelectedCurrency()))
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }
            DialogResult = DialogResult.Cancel;
            if (ApprPrizeTextBox.Text.Trim() == "")
            {
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

            var supplier = (Supplier)SupplierComboBox.GetSelectedIdentity();
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
            var merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            var currentUser = UserManager.GetCurrentUser();
            var bookerUserId = BookerUserComboBox.GetSelectedIdentityId();
            if (IsNull(merchandise) || IsNull(currentUser))
            {
                ShowWarning("Error, either the supplier, merchandise or the active user were not set, create canceled!");
                return false;
            }

            if (OrderingUnitComboBox.SelectedIndex == PlattformOrdManData.NO_COUNT)
            {
                return false;
            }
            else
            {
                // Want to have the string repr of the enum 'Other' not 'Plattform unspec.'
                popStr =
                    PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem)
                        .ToString();
            }

            UpdateMerchandisePrize(finalPrize, currencyId, out prize);
            _post = PostManager.CreatePost(merchandise.GetCurrentArticleNumberId(), bookerUserId, GetCommentFromForm(),
                merchandise.GetId(), supplierId, GetAmountFromForm(), prize, currencyId, InvoiceInstCheckBox.Checked,
                InvoiceClinCheckBox.Checked, NoInvoiceCheckBox.Checked, GetInvoiceNumberFromForm(),
                finalPrize, GetDeliveryDeviationFromForm(), PurchaseOrderNoTextBox.Text, SalesOrdernoTextBox.Text, 
                PurchaseSalesOrderTextBox.Text,popStr, Periodization.GetEnquiry(), 
                Account.GetEnquiry());
            if (GetDate(BookDateTextBox.Text.Trim()).Date != DateTime.Now.Date ||
                OrdererUserComboBox.GetSelectedIdentityId() != PlattformOrdManData.NO_ID)
            {
                bool dummy;
                return UpdatePost(out dummy);
            }
            var placeOfPurchase = (string)OrderingUnitComboBox.SelectedItem;
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
            if (!DateTime.TryParse(dateStr, PlattformOrdManData.MyCultureInfo, _dateTimeStyles, out dateTime))
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
            if (IsNull(_post))
            {
                return true;
            }
            else if ((GetPostStatus() == Post.PostStatus.Completed &&
                      _post.GetPostStatus() != Post.PostStatus.Completed) ||
                     (GetPostStatus() != Post.PostStatus.Completed &&
                      _post.GetPostStatus() == Post.PostStatus.Completed) ||
                     (GetDate(BookDateTextBox.Text.Trim()).Date != _post.GetBookDateDT().Date))
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
            return _post.GetMerchandise().GetInvoiceCagegoryId() == PlattformOrdManData.NO_ID;
        }

        private bool UpdatesToOrdered()
        {
            return !string.IsNullOrEmpty(OrderDateTextBox.Text) &&
                   (_post == null || _post.GetPostStatus() < Post.PostStatus.Ordered);
        }

        private bool HasMandatoryFields()
        {
            var hasFields = true;
            if (UpdatesToOrdered() && !Account.GetEnquiry().HasAnswered)
            {
                hasFields = false;
                Account.SetMarkColor(Color.Red);
            }

            if (UpdatesToOrdered() && !Periodization.GetEnquiry().HasAnswered)
            {
                hasFields = false;
                Periodization.SetMarkColor(Color.Red);
            }
            return hasFields;
        }

        private void InitStatusTab()
        {
            tabStatusType.SelectedIndex = (int) PlattformOrdManData.Configuration.EditPostTab;
        }

        private bool UpdatePost(out bool newSortOrder)
        {
            decimal prize, finalPrize;
            newSortOrder = false;
            int currencyId = PlattformOrdManData.NO_ID;

            var popStr =
                PlattformOrdManData.GetPlaceOfPurchaseFromString((string)OrderingUnitComboBox.SelectedItem).ToString();
            if (IsNotNull(CurrencyCombobox.GetSelectedCurrency()))
            {
                currencyId = CurrencyCombobox.GetSelectedCurrency().GetId();
            }

            if (ApprPrizeTextBox.Text.Trim() == "")
            {
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
            if (GetInvoiceStatus() == Post.InvoiceStatus.Ok &&
                !NoInvoiceCheckBox.Checked &&
                IsInvoiceCategoryMissing())
            {
                var invoiceCategoryDialog = new InvoiceCategoryDialog(_post);
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

            if (!HasMandatoryFields())
            {
                ShowWarning("Please, fill in the missing fields marked with red. ");
                return false;
            }

            newSortOrder = IsNewSortOrderNeeded();
            UpdateMerchandisePrize(finalPrize, currencyId, out prize);
            var bookerUserId = BookerUserComboBox.GetSelectedIdentityId();
            var ordererUserId = OrdererUserComboBox.GetSelectedIdentityId();
            var confirmOrderUserId = ConfirmedOrderUserComboBox.GetSelectedIdentityId();
            var arrivalSignUserId = ArrivalSignUserComboBox.GetSelectedIdentityId();
            var invoicerUserId = InvoiceCheckerUserComboBox.GetSelectedIdentityId();
            _post.UpdatePost(GetCommentFromForm(), prize, GetAmountFromForm(),
                InvoiceClinCheckBox.Checked, InvoiceInstCheckBox.Checked, GetApprArrivalDate(),
                GetInvoiceStatus().ToString(),
                NoInvoiceCheckBox.Checked, currencyId, bookerUserId, GetBookDate(), ordererUserId, GetOrderDate(),
                arrivalSignUserId, GetArrivalDate(), invoicerUserId, GetInvoiceDate(),
                _post.GetMerchandise().GetCurrentArticleNumberId(),
                SupplierComboBox.GetSelectedIdentityId(), GetInvoiceNumberFromForm(), finalPrize,
                GetConfirmedOrderDate(), confirmOrderUserId, GetDeliveryDeviationFromForm(),
                PurchaseOrderNoTextBox.Text, SalesOrdernoTextBox.Text, 
                PurchaseSalesOrderTextBox.Text, popStr,
                AttentionCheckBox.Checked, Periodization.GetEnquiry(),
                Account.GetEnquiry());
            if (_post.IsInvoceAbsent() && _post.GetPostStatus() == Post.PostStatus.Confirmed)
            {
                _post.SignPostInvoice(UserManager.GetCurrentUser(), Post.InvoiceStatus.Ok, _post.IsInvoceAbsent());
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
            if (!DateTime.TryParse(BookDateTextBox.Text.Trim(), out dateTime))
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

        private void UpdateMerchandisePrize(decimal prize, int currencyId, out decimal newApprPrize)
        {
            var merchandise = merchandiseCombobox1.GetSelectedMerchandise();
            if (!GetPrizeDecimal(GetPrizeString(ApprPrizeTextBox.Text), out newApprPrize))
            {
                newApprPrize = -1;
            }
            if ((prize != PlattformOrdManData.NO_COUNT && merchandise.GetApprPrize() != prize) ||
                merchandise.GetCurrencyId() != currencyId)
            {
                var prizeStr = CurrencyManager.GetCurrency(currencyId).GetPriceWithCurrencyString(prize);
                if (prize != PlattformOrdManData.NO_COUNT)
                {
                    newApprPrize = prize;
                }
                if (IsNotNull(_post))
                {
                    _post.SetApprPrizeLocal(newApprPrize);
                }
                var str = "Change default price for product ''" + merchandise.GetIdentifier() + "'' to " + prizeStr +
                          "?";
                if (MessageBox.Show(str, "Price update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    merchandise.SetApprPrize(prize);
                    merchandise.SetCurrencyId(currencyId);
                    merchandise.Set();
                    if (IsNotNull(_post))
                    {
                        _post.ResetMerchanidse();
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool newSortOrder = false;
            try
            {
                Cursor = Cursors.WaitCursor;
                switch (_updateMode)
                {
                    case PostUpdateMode.Create:
                        if (IsProductInLine() &&
                            MessageBox.Show(
                                "There is already an un-completed order with this product in line, proceed anyway?",
                                "Product already in line", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                        {
                            Cursor = Cursors.Default;
                            Close();
                            return;
                        }
                        if (!CreatePost())
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                        break;
                    case PostUpdateMode.Edit:
                        if (!UpdatePost(out newSortOrder))
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                        break;
                    default:
                        throw  new DataException($"Unknown choice: {_updateMode}");
                }
                if (IsNotNull(OnPostUpdate))
                {
                    OnPostUpdate?.Invoke(_post, new UpdateHandlerEventArgs(newSortOrder));
                }
            }
            finally
            {
                Cursor = Cursors.Default;
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
                prize = amount * GetFinalPrize();
            }
            else if (HasApprPrize() &&
                     amount != PlattformOrdManData.NO_COUNT)
            {
                prize = amount * GetApprPrize();
            }

            if (prize != PlattformOrdManData.NO_COUNT &&
                CurrencyCombobox.HasSelectedCurrency())
            {
                TotalPrizeTextBox.Text = CurrencyCombobox.GetSelectedCurrency().GetPriceWithCurrencyString(prize);
            }
            else if (prize != PlattformOrdManData.NO_COUNT &&
                     !CurrencyCombobox.HasSelectedCurrency())
            {
                TotalPrizeTextBox.Text = prize.ToString(CultureInfo.InvariantCulture);
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
            if (_updateMode == PostUpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
            else if (_updateMode == PostUpdateMode.Create)
            {
                SaveButton.Enabled = merchandiseCombobox1.SelectedIndex != -1;
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
            var showCurrenciesDialog = new ShowCurrenciesDialog();
            var currentCurrency = CurrencyCombobox.GetSelectedCurrency();
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
                ConfirmedOrderDateTextBox.Text = "";
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
            if (InvoiceCheckerUserComboBox.GetSelectedIdentityId() == PlattformOrdManData.NO_ID)
            {
                InvoiceCheckDateTextBox.Text = "";
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

        private void ApprArrivalLabel_Click(object sender, EventArgs e)
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
            try
            {
                if (SupplierComboBox.HasSelectedIdentity())
                {
                    var supplier = SupplierComboBox.GetSelectedIdentity() as Supplier;
                    var editSupplierDialog = new EditSupplierDialog(supplier, UpdateMode.Edit)
                    {
                        MdiParent = MdiParent
                    };
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
            if (merchandiseCombobox1.HasSelectedMerchandise())
            {
                var merchandise = merchandiseCombobox1.GetSelectedMerchandise();
                var editMerchandiseDialog = new EditMerchandiseDialog(merchandise, UpdateMode.Edit)
                {
                    MdiParent = MdiParent
                };
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

        private void SupplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AttentionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AttentionCheckBox.BackColor = AttentionCheckBox.Checked ? Color.Red : BackColor;
            HandleSaveButtonEnabled();
        }

        protected void Periodization_Changed(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        protected void Account_Changed(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }

        private void FinalPrizeTextBox_TextChanged(object sender, EventArgs e)
        {
            HandleSaveButtonEnabled();
        }
    }
}