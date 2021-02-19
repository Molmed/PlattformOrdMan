using System;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data.PostData
{
    public class UpdateHandlerEventArgs : EventArgs
    {
        public UpdateHandlerEventArgs(bool newSortOrder)
        {
            NewSortOrder = newSortOrder;
        }
        public readonly bool NewSortOrder;
    }

    public class Post : PlattformOrdManData, IDataComment, IDataIdentity, IComparable
    {
        public enum PostStatus
        { 
            Booked,
            Ordered,
            ConfirmedOrder,
            Confirmed,
            Completed,
        }
        public enum InvoiceStatus
        { 
            Incoming,
            Ok
        }
        public bool AttentionFlag { get; set; }

        private ArticleNumber _articleNumber;
        private int _articleNumberId;
        private int _bookerUserId;
        private User _booker;
        private DateTime _bookDate;
        private int _merchandiseId;
        private Merchandise _merchandise;
        private int _supplierId;
        private Supplier _supplier;
        private decimal _apprPrize;
        private DateTime _orderDate;
        private int _ordererUserId;
        private User _orderer;
        private DateTime _predictedArrival;
        private bool _invoiceInst;
        private bool _invoiceClin;
        private DateTime _arrivalDate;
        private int _arrivalSignUserId;
        private User _arrivalSignUser;
        private int _amount;
        private PostStatus _postStatus;
        private InvoiceStatus _invoiceStatus;
        private DateTime _invoiceDate;
        private int _invoicerUserId;
        private User _invoicerUser;
        private bool _isInvoiceAbsent;
        private int _currencyId;
        private Currency _currency;
        private string _invoiceNumber;
        private decimal _finalPrize;
        private int _confirmedOrderUserId;
        private User _confirmedOrderUser;
        private DateTime _confirmedOrderDate;
        private string _deliveryDeviation;
        private string _purchaseOrderNo;
        private string _salesOrderNo;
        private PlaceOfPurchase _placeOfPurchase;
        private string _comment;
        private readonly int _id;
        // These fields are metadata for faster loading time
        private string _merchandiseIdentifier;
        private string _merchandiseAmount;
        private bool _isMerchandiseEnabled;
        private string _merchandiseComment;
        private string _supplierIdentifier;
        private readonly int _invoiceCategoryNumber;
        private Enquiry _periodization;
        private Enquiry _account;


        public Post(DataReader dataReader)
        {
            _id = dataReader.GetInt32(DataIdentityData.ID);
            AttentionFlag = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.ATTENTION_FLAG);
            _comment = dataReader.GetString(DataCommentData.COMMENT);
            _bookerUserId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.AUTHORITY_ID_BOOKER);
            _bookDate = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.BOOK_DATE);
            _merchandiseId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.MERCHANDISE_ID);
            _supplierId = NO_ID;
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.SUPPLIER_ID))
            {
                _supplierId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.SUPPLIER_ID);
            }
            _isInvoiceAbsent = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.INVOICE_ABSENT);
            _deliveryDeviation = dataReader.GetString(PlattformOrdMan.Database.PostData.DELIVERY_DEVIATION);
            _booker = null;
            _merchandise = null;
            _supplier = null;
            _orderer = null;
            _arrivalSignUser = null;
            _invoicerUser = null;
            _orderDate = new DateTime();
            _arrivalDate = new DateTime();
            _apprPrize = NO_COUNT;
            _arrivalSignUserId = NO_ID;
            _ordererUserId = NO_ID;
            _invoicerUserId = NO_ID;
            _currency = null;
            _currencyId = NO_ID;
            _confirmedOrderDate = new DateTime();
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.CURRENCY_ID))
            {
                _currencyId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.CURRENCY_ID);
            }
            SetInvoiceStatus(dataReader.GetString(PlattformOrdMan.Database.PostData.INVOICE_STATUS));
            _invoiceDate = new DateTime();
            _predictedArrival = new DateTime();
            _invoiceNumber = dataReader.GetString(PlattformOrdMan.Database.PostData.INVOICE_NUMBER);
            _finalPrize = dataReader.GetDecimal(PlattformOrdMan.Database.PostData.FINAL_PRIZE, NO_COUNT);
            _confirmedOrderUserId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.AUTHORITY_ID_CONFIRMED_ORDER, NO_ID);
            _purchaseOrderNo = "";
            _salesOrderNo = "";
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.CONFIRMED_ORDER_DATE))
            {
                _confirmedOrderDate = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.CONFIRMED_ORDER_DATE);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.APPR_PRIZE))
            {
                _apprPrize = dataReader.GetDecimal(PlattformOrdMan.Database.PostData.APPR_PRIZE);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.ORDER_DATE))
            {
                _orderDate = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.ORDER_DATE);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.AUTHORITY_ID_ORDERER))
            {
                _ordererUserId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.AUTHORITY_ID_ORDERER);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.AUTHORITY_ID_INVOICER))
            {
                _invoicerUserId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.AUTHORITY_ID_INVOICER);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.INVOICE_DATE))
            {
                _invoiceDate = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.INVOICE_DATE);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.PREDICTED_ARRIVAL))
            {
                _predictedArrival = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.PREDICTED_ARRIVAL);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.INVOICE_INST))
            {
                _invoiceInst = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.INVOICE_INST);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.INVOICE_CLIN))
            {
                _invoiceClin = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.INVOICE_CLIN);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.ARRIVAL_DATE))
            {
                _arrivalDate = dataReader.GetDateTime(PlattformOrdMan.Database.PostData.ARRIVAL_DATE);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.ARRIVAL_SIGN))
            {
                _arrivalSignUserId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.ARRIVAL_SIGN);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.AMOUNT))
            {
                _amount = dataReader.GetInt32(PlattformOrdMan.Database.PostData.AMOUNT);
            }
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.PURCHASE_ORDER_NO))
            {
                _purchaseOrderNo = dataReader.GetString(PlattformOrdMan.Database.PostData.PURCHASE_ORDER_NO);
            }

            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.SALES_ORDER_NO))
            {
                _salesOrderNo = dataReader.GetString(PlattformOrdMan.Database.PostData.SALES_ORDER_NO);
            }

            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.PLACE_OF_PURCHASE))
            {
                var placeOfPurchase = dataReader.GetString(PlattformOrdMan.Database.PostData.PLACE_OF_PURCHASE);
                _placeOfPurchase = (PlaceOfPurchase)(Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase));
            }

            _merchandiseIdentifier = dataReader.GetString(PlattformOrdMan.Database.PostData.MERCHANDISE_IDENTIFIER);
            _merchandiseAmount = dataReader.GetString(PlattformOrdMan.Database.PostData.MERCHANDISE_AMOUNT);
            _isMerchandiseEnabled = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.MERCHANDISE_ENABLED);
            _merchandiseComment = dataReader.GetString(PlattformOrdMan.Database.PostData.MERCHANDISE_COMMENT);
            _supplierIdentifier = dataReader.GetString(PlattformOrdMan.Database.PostData.SUPPLIER_IDENTIFIER);
            _invoiceCategoryNumber = dataReader.GetInt32(PlattformOrdMan.Database.PostData.INVOICE_CATEGORY_NUMBER, NO_COUNT);

            _articleNumber = null;
            _articleNumberId = dataReader.GetInt32(PlattformOrdMan.Database.PostData.ARTICLE_NUMBER_ID, NO_ID);
            if (IsValidId(_articleNumberId))
            {
                LoadArticleNumber(dataReader);
            }

            var periodizationValue = dataReader.GetString(PlattformOrdMan.Database.PostData.PERIODIZATION);
            var periodizationAnswered = false;
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.PERIODIZATION_ANSWERED))
                periodizationAnswered = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.PERIODIZATION_ANSWERED);
            var hasPeriodization = false;
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.HAS_PERIODIZATION))
                hasPeriodization = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.HAS_PERIODIZATION);
            _periodization = new Enquiry(
                periodizationAnswered, hasPeriodization, periodizationValue);

            var accountValue = dataReader.GetString(PlattformOrdMan.Database.PostData.ACCOUNT);
            var accountAnswered = false;
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.ACCOUNT_ANSWERED))
                accountAnswered = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.ACCOUNT_ANSWERED);
            var hasAccount = false;
            if (!dataReader.IsDBNull(PlattformOrdMan.Database.PostData.HAS_ACCOUNT))
                hasAccount = dataReader.GetBoolean(PlattformOrdMan.Database.PostData.HAS_ACCOUNT);
            _account = new Enquiry(accountAnswered, hasAccount, accountValue);

            SetPostStatus();
        }

        public Enquiry Periodization => _periodization;
        public Enquiry Account => _account;

        public bool IsMerchandiseEnabled()
        {
            return _isMerchandiseEnabled;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetIdentifier()
        {
            return "";
        }

        public String GetComment()
        {
            if (IsNull(_comment))
            {
                return "";
            }
            return _comment;
        }

        private void SetComment(String comment)
        {
            _comment = comment;
        }

        public Boolean HasComment()
        {
            return IsNotEmpty(_comment);
        }

        public PlaceOfPurchase GetPlaceOfPurchase()
        {
            return _placeOfPurchase;
        }

        public GroupCategory GetGroupCategory()
        {
            return GetGroupCategory(_placeOfPurchase);
        }

        public string GetPlaceOfPurchaseString()
        {
            return GetPlaceOfPurchaseString(_placeOfPurchase);
        }

        public string GetDeliveryDeviation()
        {
            if (IsNull(_deliveryDeviation))
            {
                return "";
            }
            return _deliveryDeviation;
        }

        public string GetInvoiceNumber()
        {
            if (IsNull(_invoiceNumber))
            {
                return "";
            }
            return _invoiceNumber;
        }

        public string GetPurchaseOrderNo()
        {
            return _purchaseOrderNo;
        }

        public string GetSalesOrderNo()
        {
            return _salesOrderNo;
        }

        public decimal GetFinalPrize()
        {
            return _finalPrize;
        }

        public int GetConfirmedOrderUserId()
        {
            return _confirmedOrderUserId;
        }

        public User GetConfirmedOrderUser()
        {
            if (_confirmedOrderUser == null && _confirmedOrderUserId != NO_ID)
            {
                _confirmedOrderUser = UserManager.GetUser(_confirmedOrderUserId);
            }
            return _confirmedOrderUser;
        }

        public DateTime GetConfirmedOrderDate()
        {
            return _confirmedOrderDate;
        }

        private ArticleNumber GetArticleNumber()
        {
            if (IsNull(_articleNumber) && IsValidId(_articleNumberId))
            {
                _articleNumber = MerchandiseManager.GetArticleNumberById(_articleNumberId);
            }
            return _articleNumber;
        }

        public string GetArticleNumberString()
        {
            if (IsNull(GetArticleNumber()))
            {
                return "";
            }
            return GetArticleNumber().GetIdentifier();
        }

        private void LoadArticleNumber(DataReader reader)
        {
            reader.SetColumnNamePrefix(PlattformOrdMan.Database.PostData.ARTICLE_NUMBER_PREFIX);
            _articleNumber = new ArticleNumber(reader);
            reader.ResetColumnNamePrefix();
        }

        private void SetPostStatus()
        {
            if (IsNull(GetOrderer()))
            {
                _postStatus = PostStatus.Booked;
            }
            else if(IsNull(GetConfirmedOrderUser()))
            {
                _postStatus = PostStatus.Ordered;
            }
            else if (IsNull(GetArrivalSignUser()))
            {
                _postStatus = PostStatus.ConfirmedOrder;
            }
            else if (IsNull(GetInvoicerUser()))
            {
                _postStatus = PostStatus.Confirmed;
            }
            else
            {
                _postStatus = PostStatus.Completed;
            }
        }

        public void ConfirmPostArrival(int arrivalSignUserId)
        {
            Database.ConfirmPostArrival(GetId(), arrivalSignUserId);
            _arrivalSignUserId = arrivalSignUserId;
            _arrivalSignUser = null;
            var tmpPost = PostManager.GetPostById(GetId());
            _arrivalDate = tmpPost.GetArrivalDate();
            _postStatus = PostStatus.Confirmed;
        }

        private Currency GetCurrency()
        {
            if (IsNull(_currency))
            {
                _currency = CurrencyManager.GetCurrency(_currencyId);
            }
            return _currency;
        }

        public int GetCurrencyId()
        {
            return _currencyId;
        }

        public User GetBooker()
        {
            if (IsNull(_booker))
            {
                _booker = UserManager.GetUser(_bookerUserId);
            }
            return _booker;
        }

        public int GetBookerId()
        {
            return _bookerUserId;
        }

        public string GetStringForListViewColumn(PostListViewColumn column)
        {
            switch (column)
            { 
                case PostListViewColumn.Amount:
                    return GetAmountString();
                case PostListViewColumn.InvoiceCategoryCode:
                    return GetInvoiceCategoryCodeString2();
                case PostListViewColumn.ApprArrival:
                    return GetPredictedArrival();
                case PostListViewColumn.ArrivalDate:
                    return GetArrivalDateString();
                case PostListViewColumn.BookDate:
                    return GetBookDate();
                case PostListViewColumn.InvoiceDate:
                    return GetInvoiceDateString();
                case PostListViewColumn.OrderDate:
                    return GetOrderDateString();
                case PostListViewColumn.ApprPrize:
                    return GetPriceWithCurrencyString();
                case PostListViewColumn.FinalPrize:
                    return GetFinalPrizeWithCurrencyString();
                case PostListViewColumn.TotalPrize:
                    return GetTotalPrizeWithCurrencyString();
                case PostListViewColumn.ArrivalSign:
                    return GetArrivalSignUserName();
                case PostListViewColumn.ArtNr:
                    return GetArticleNumberString();
                case PostListViewColumn.Booker:
                    return GetBookerName();
                case PostListViewColumn.Comment:
                    return GetCommentForListView();
                case PostListViewColumn.DeliveryDeviation:
                    return GetDeliveryDeviation();
                case PostListViewColumn.InvoiceClin:
                    return GetInvoiceClinString();
                case PostListViewColumn.InvoiceInst:
                    return GetInvoiceInstString();
                case PostListViewColumn.InvoiceNumber:
                    return GetInvoiceNumber();
                case PostListViewColumn.InvoiceSender:
                    return GetInvoicerUserName();
                case PostListViewColumn.InvoiceStatus:
                    return GetInvoiceStatusString();
                case PostListViewColumn.OrderSign:
                    return GetOrdererName();
                case PostListViewColumn.Product:
                    return GetMerchandiseName2();
                case PostListViewColumn.PurchaseOrderNo:
                    return GetPurchaseOrderNo();
                case PostListViewColumn.SalesOrderNo:
                    return GetSalesOrderNo();
                case PostListViewColumn.Supplier:
                    return GetSupplierName2();
                case PostListViewColumn.PlaceOfPurchase:
                    return GetPlaceOfPurchaseString();
                default:
                    throw new DataException("Unknwon post list view column: " + column);
            }
        }

        public string GetCommentForListView()
        {
            var commentText = "";
            if (IsNotEmpty(_merchandiseComment))
            {
                commentText += _merchandiseComment + "; ";
            }
            commentText = commentText + GetComment();
            return commentText;
        }

        public String GetBookerName()
        {
            if (IsNull(GetBooker()))
            {
                return "";
            }
            return GetBooker().GetName();
        }

        public InvoiceStatus GetInvoiceStatus()
        {
            return _invoiceStatus;
        }

        public String GetInvoiceStatusString()
        {
            if (_invoiceStatus == InvoiceStatus.Incoming)
            {
                return "Not checked";
            }
            else
            {
                if (_isInvoiceAbsent)
                {
                    return "Ok (no invoice)";
                }
                else
                {
                    return "Ok and sent";
                }
            }
        }

        private int GetInvoicerUserId()
        {
            return _invoicerUserId;
        }

        public User GetInvoicerUser()
        {
            if (IsNull(_invoicerUser))
            {
                _invoicerUser = UserManager.GetUser(_invoicerUserId);
            }
            return _invoicerUser;
        }

        public int GetInvoiceUserId()
        {
            return _invoicerUserId;
        }

        public String GetInvoicerUserName()
        {
            if (IsNull(GetInvoicerUser()))
            {
                return "";
            }
            return GetInvoicerUser().GetName();
        }

        public DateTime GetInvoiceDate()
        {
            return _invoiceDate;
        }

        public String GetInvoiceDateString()
        {
            if (_invoiceDate.Ticks == 0)
            {
                return "";
            }
            return _invoiceDate.Date.ToString("yyyy-MM-dd");
        }

        public DateTime GetBookDateDT()
        {
            return _bookDate;
        }

        public String GetBookDate()
        {
            if (IsNull(_bookDate))
            {
                return "";
            }
            return _bookDate.Date.ToString("yyyy-MM-dd");
        }

        public Merchandise GetMerchandise()
        {
            if (IsNull(_merchandise) && _merchandiseId != NO_ID)
            {
                _merchandise = MerchandiseManager.GetMerchandiseById(_merchandiseId);
            }
            return _merchandise;
        }

        public int GetMerchandiseId()
        {
            return _merchandiseId;
        }

        public string GetMerchandiseName2()
        {
            if (IsEmpty(_merchandiseIdentifier))
            {
                return "";
            }

            var name = _merchandiseIdentifier;
            if (IsNotEmpty(_merchandiseAmount))
            {
                name += ", " + _merchandiseAmount;
            }
            return name;            
        }

        public String GetMerchandiseName()
        {
            if (IsNull(GetMerchandise()))
            {
                return "";
            }
            var name = GetMerchandise().GetIdentifier();
            if (IsNotEmpty(GetMerchandise().GetAmount()))
            {
                name = name + ", " + GetMerchandise().GetAmount();
            }
            return name;
        }

        public DateTime GetOrderDate()
        {
            return _orderDate;
        }

        public String GetOrderDateString()
        {
            if (_orderDate.Ticks == 0)
            {
                return "";
            }
            return _orderDate.Date.ToString("yyyy-MM-dd");
        }

        public string GetConfirmedOrderDateString()
        {
            if (_confirmedOrderDate.Ticks == 0)
            {
                return "";
            }
            return _confirmedOrderDate.Date.ToString("yyyy-MM-dd");
        }

        public PostStatus GetPostStatus()
        {
            return _postStatus;
        }

        public String GetPredictedArrival()
        {
            if (_predictedArrival.Ticks == 0)
            {
                return "";
            }
            else
            {
                return _predictedArrival.Date.ToString("yyyy-MM-dd");
            }
        }

        public bool HasSupplier()
        {
            return _supplierId != NO_ID;
        }


        public void ResetSupplierLocal()
        {
            _supplier = null;
        }

        public void ReloadSupplier(Supplier supplier)
        {
            _supplier = supplier;
            if (IsNotNull(supplier))
            {
                _supplierId = supplier.GetId();
                _supplierIdentifier = supplier.GetIdentifier();
            }
            else
            {
                _supplierId = NO_ID;
                _supplierIdentifier = "";
            }
        }

        public void ResetMerchandiseLocal()
        {
            _merchandise = null;
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            _merchandise = merchandise;
            if (IsNotNull(_merchandise))
            {
                _merchandiseAmount = _merchandise.GetAmount();
                _merchandiseComment = _merchandise.GetComment();
                _merchandiseId = _merchandise.GetId();
                _merchandiseIdentifier = _merchandise.GetIdentifier();
                _isMerchandiseEnabled = _merchandise.IsEnabled();
                ReloadSupplier(merchandise.GetSupplier());
            }
            else
            {
                _merchandiseAmount = "";
                _merchandiseComment = "";
                _merchandiseIdentifier = "";
                _isMerchandiseEnabled = false;
                _merchandiseId = NO_ID;
            }
        }

        public Supplier GetSupplier()
        {
            if (IsNull(_supplier))
            {
                _supplier = SupplierManager.GetSupplierById(_supplierId);
            }
            return _supplier;
        }

        public int GetSupplierId()
        {
            return _supplierId;
        }

        public string GetSupplierName2()
        {
            if (IsEmpty(_supplierIdentifier))
            {
                return "No supplier selected";                
            }
            return _supplierIdentifier;
        }

        public String GetSupplierName()
        {
            if (IsNull(GetSupplier()))
            {
                return "No supplier selected";
            }
            return GetSupplier().GetIdentifier();
        }

        public static Boolean operator<(Post post1, Post post2)
        {
            if (post1.GetInvoiceStatus() == InvoiceStatus.Incoming &&
                post2.GetInvoiceStatus() != InvoiceStatus.Incoming)
            {
                return true;
            }
            else if (post1.GetInvoiceStatus() != InvoiceStatus.Incoming &&
                post2.GetInvoiceStatus() == InvoiceStatus.Incoming)
            {
                return false;
            }
            else if (post1.GetBookDateDT().Ticks < post2.GetBookDateDT().Ticks)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean operator ==(Post post1, Post post2)
        {
            if (GetId(post1) == GetId(post2))
            {
                return true;
            }
            return false;
        
        }

        private static int GetId(Post post)
        { 
            // to avoid recursive call to the operator==
            if((object)post == null)
            {
                return NO_ID;
            }
            else
            {
                return post.GetId();
            }
        }

        public static Boolean operator !=(Post post1, Post post2)
        {
            return !(post1 == post2);
        }

        public static Boolean operator <=(Post post1, Post post2)
        {
            return (post1 < post2) || (post1 == post2);
        }

        public static Boolean operator >=(Post post1, Post post2)
        {
            return !(post1 < post2) || (post1 == post2);
        }

        public static Boolean operator>(Post post1, Post post2)
        {
            return !(post1 < post2) && (post1 != post2);
        }

        public override bool Equals(object obj)
        {
            return this == (Post)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            var post = obj as Post;
            if (post != null)
            {
                if (this >= post)
                {
                    return 1;
                }
                return -1;
            }
            else 
            {
                return 1;
            }
        }

        public int GetAmount()
        { 
            return _amount;
        }

        public string GetAccountString()
        {
            if (Account == null || !Account.HasAnswered)
            {
                return "<not answered>";
            }
            else if(!Account.HasValue)
            {
                return "-";
            }
            else
            {
                return Account.Value;
            }
        }

        public string GetPeriodizationString()
        {
            if (Periodization == null || !Periodization.HasAnswered)
            {
                return "<not answered>";
            }

            if (!Periodization.HasValue)
            {
                return "-";
            }

            if (Periodization.HasValue && string.IsNullOrEmpty(Periodization.Value))
            {
                return "yes";
            }

            return Periodization.Value;
        }
        public string GetAmountString()
        {
            if (_amount == NO_COUNT)
            {
                return "";
            }
            else
            {
                return _amount.ToString();
            }
        }

        public decimal GetApprPrizeDecimal()
        {
            return _apprPrize;
        }

        public String GetPriceWithCurrencyString()
        {
            if (_apprPrize == NO_COUNT || IsNull(GetCurrency()))
            {
                return "";
            }
            return _currency.GetPriceWithCurrencyString(_apprPrize);        
        }

        public string GetFinalPrizeWithCurrencyString()
        {
            if (_finalPrize == NO_COUNT || IsNull(GetCurrency()))
            {
                return "";
            }
            return _currency.GetPriceWithCurrencyString(_finalPrize);
        }


        private decimal GetTotalPrize()
        {
            if (_amount != NO_COUNT && _finalPrize != NO_COUNT && IsNotNull(GetCurrency()))
            {
                return _amount * _finalPrize;
            }
            else if (_amount != NO_COUNT && _apprPrize != NO_COUNT && IsNotNull(GetCurrency()))
            {
                return _amount * _apprPrize;
            }
            else
            {
                return NO_COUNT;
            }
        }

        public string GetTotalPrizeWithCurrencyString()
        {
            var prize = GetTotalPrize();
            if (prize != NO_COUNT)
            {
                return _currency.GetPriceWithCurrencyString(prize);
            }
            else
            {
                return "";
            }
        }
        
        public bool GetInvoiceClin()
        {
            return _invoiceClin;
        }

        public bool GetInvoiceInst()
        {
            return _invoiceInst;
        }

        public bool IsInvoceAbsent()
        {
            return _isInvoiceAbsent;
        }

        public String GetInvoiceInstString()
        {
            if (_invoiceInst)
            {
                return "x";
            }
            else
            {
                return "";
            }
        }

        public String GetInvoiceCategoryCodeString2()
        {
            if (_invoiceCategoryNumber != NO_COUNT)
            {
                return _invoiceCategoryNumber.ToString();
            }
            else
            {
                return "";
            }
        }


        public String GetInvoiceClinString()
        {
            if (_invoiceClin)
            {
                return "x";
            }
            else
            {
                return "";
            }
        }

        public User GetOrderer()
        {
            if (IsNull(_orderer))
            {
                _orderer = UserManager.GetUser(_ordererUserId);
            }
            return _orderer;
        }

        public int GetOrderUserId()
        {
            return _ordererUserId;
        }

        public String GetOrdererName()
        {
            if (IsNull(GetOrderer()))
            {
                return "";
            }
            return GetOrderer().GetName();
        }

        public DateTime GetArrivalDate()
        {
            return _arrivalDate;
        }

        public String GetArrivalDateString()
        {
            if(_arrivalDate.Ticks == 0)
            {
                return "";
            }
            return _arrivalDate.Date.ToString("yyyy-MM-dd");
        }

        public User GetArrivalSignUser()
        {
            if (IsNull(_arrivalSignUser))
            {
                _arrivalSignUser = UserManager.GetUser(_arrivalSignUserId);
            }
            return _arrivalSignUser;
        }

        public int GetArrivalSignUserId()
        {
            return _arrivalSignUserId;
        }

        public String GetArrivalSignUserName()
        {
            if (IsNull(GetArrivalSignUser()))
            {
                return "";
            }
            return GetArrivalSignUser().GetName();
        }

        public DataType GetDataType()
        {
            return DataType.Post;
        }

        public bool HasActiveArticleNumberSelected()
        {
            int activeAnId = NO_ID;
            int selANId = NO_ID;
            if (GetMerchandise().HasArticleNumber())
            {
                activeAnId = GetMerchandise().GetActiveArticleNumber().GetId();
            }
            if (IsNotNull(GetArticleNumber()))
            {
                selANId = GetArticleNumber().GetId();
            }

            return selANId == activeAnId;
        }

        private void CheckOrderSign()
        {
            if (Account == null || Periodization == null)
            {
                throw new DataException("Mandatory fields 'Account' or 'Periodization' is not set!");
            }
        }

        public void OrderPost(OrderPostDto bag)
        {
            CheckOrderSign();
            Database.OrderPost(GetId(),  bag.OrderedUserId, bag.Account, bag.Periodization);
            var tmpPost = PostManager.GetPostById(GetId());
            _orderDate = tmpPost.GetOrderDate();
            _ordererUserId = bag.OrderedUserId;
            _orderer = null;
            SetPostStatus();
            if (_isInvoiceAbsent)
            {
                SignPostInvoice(UserManager.GetUser(bag.OrderedUserId), InvoiceStatus.Ok, _isInvoiceAbsent);
            }
        }

        public void ConfirmPostOrdered(int userId)
        {
            Database.ConfirmPostOrdered(GetId(), userId);
            var tempPost = PostManager.GetPostById(GetId());
            _confirmedOrderDate = tempPost.GetConfirmedOrderDate();
            _confirmedOrderUserId = tempPost.GetConfirmedOrderUserId();
            _confirmedOrderUser = null;
            SetPostStatus();
        }

        public void SignPostInvoice(User signer, InvoiceStatus status, bool isInvoiceAbsent)
        {
            Database.SignPostInvoice(GetId(), signer.GetId(), status.ToString(), isInvoiceAbsent);
            var tmpPost = PostManager.GetPostById(GetId());
            _invoiceDate = tmpPost.GetInvoiceDate();
            _invoiceStatus = status;
            _invoicerUserId = signer.GetId();
            _invoicerUser = null;
            _isInvoiceAbsent = isInvoiceAbsent;
            SetPostStatus();
        }

        public void SetApprPrizeLocal(decimal prize)
        {
            _apprPrize = prize;
        }

        public void UpdateMarkForAttention(bool markForAttention)
        {
            DateTime apprArrival;
            DateTime.TryParse(GetPredictedArrival(), out apprArrival);

            UpdatePost(GetComment(), GetApprPrizeDecimal(), GetAmount(), GetInvoiceClin(), GetInvoiceInst(),
                apprArrival, GetInvoiceStatus().ToString(), IsInvoceAbsent(), GetCurrencyId(),
                GetBookerId(), GetBookDateDT(), GetOrderUserId(), GetOrderDate(), GetArrivalSignUserId(),
                GetArrivalDate(), GetInvoiceUserId(), GetInvoiceDate(), _articleNumberId, GetSupplierId(),
                GetInvoiceNumber(), GetFinalPrize(), GetConfirmedOrderDate(), GetConfirmedOrderUserId(), 
                GetDeliveryDeviation(), GetPurchaseOrderNo(), GetSalesOrderNo(), GetPlaceOfPurchase().ToString(),
                markForAttention, _periodization, _account);
        }

        public void UpdateInvoiceNumber(string invoiceNumber, bool isNoInvoice)
        { 
            DateTime apprArrival;
            DateTime.TryParse(GetPredictedArrival(), out apprArrival);
            
            UpdatePost(GetComment(), GetApprPrizeDecimal(), GetAmount(), GetInvoiceClin(), GetInvoiceInst(),
                apprArrival, GetInvoiceStatus().ToString(), isNoInvoice, GetCurrencyId(),
                GetBookerId(), GetBookDateDT(), GetOrderUserId(), GetOrderDate(), GetArrivalSignUserId(),
                GetArrivalDate(), GetInvoicerUserId(), GetInvoiceDate(), _articleNumberId, GetSupplierId(),
                invoiceNumber, GetFinalPrize(), GetConfirmedOrderDate(), GetConfirmedOrderUserId(),
                GetDeliveryDeviation(), GetPurchaseOrderNo(), GetSalesOrderNo(), GetPlaceOfPurchase().ToString(), 
                AttentionFlag, _periodization, _account);
        }

        public void UpdatePost(String comment, decimal apprPrize, int amount, bool invoiceClin, bool invoiceInst,
            DateTime apprArrival, String invoiceStatus, bool isInvoiceAbsent, int currencyId, int bookerUserId, 
            DateTime bookDate, int orderUserId, DateTime orderDate, int arrivalSignUserId, 
            DateTime arrivalDate, int invoiceCheckerUserId, DateTime invoiceDate, 
            int articleNumberId, int supplierId, string invoiceNumber, decimal finalPrize, 
            DateTime confirmedOrderDate, int confirmedOrderUserId, string deliveryDeviation,
            string purchaseOrderNo, string salesOrderNo, string placeOfPurchase, 
            bool attentionFlag, Enquiry periodization, Enquiry account)
        {
            Database.UpdatePost(GetId(), comment, apprPrize, amount, invoiceClin, invoiceInst, apprArrival, 
                invoiceStatus, isInvoiceAbsent, currencyId, bookerUserId, bookDate, orderUserId, orderDate,
                arrivalSignUserId, arrivalDate, invoiceCheckerUserId, invoiceDate, articleNumberId, supplierId,
                invoiceNumber, finalPrize, confirmedOrderDate, confirmedOrderUserId, deliveryDeviation,
                purchaseOrderNo, salesOrderNo, placeOfPurchase, attentionFlag, 
                periodization.Value, periodization.HasValue, periodization.HasAnswered, account.Value,
                account.HasValue, account.HasAnswered);
            SetComment(comment);
            _periodization = periodization;
            _account = account;
            AttentionFlag = attentionFlag;
            _apprPrize = apprPrize;
            _amount = amount;
            _invoiceClin = invoiceClin;
            _invoiceInst = invoiceInst;
            _predictedArrival = apprArrival;
            _isInvoiceAbsent = isInvoiceAbsent;
            _currencyId = currencyId;
            _currency = null;
            SetBookerId(bookerUserId);
            SetOrderUserId(orderUserId);
            SetArrivalSignUserId(arrivalSignUserId);
            SetInvoicerUserId(invoiceCheckerUserId);
            _bookDate = bookDate;
            _orderDate = orderDate;
            _arrivalDate = arrivalDate;
            _invoiceDate = invoiceDate;
            SetInvoiceStatus(invoiceStatus);
            _articleNumberId = articleNumberId;
            _articleNumber = null;
            _supplierId = supplierId;
            _supplier = null;
            _invoiceNumber = invoiceNumber;
            _finalPrize = finalPrize;
            _confirmedOrderDate = confirmedOrderDate;
            _confirmedOrderUserId = confirmedOrderUserId;
            _confirmedOrderUser = null;
            _deliveryDeviation = deliveryDeviation;
            _purchaseOrderNo = purchaseOrderNo;
            _salesOrderNo = salesOrderNo;
            _placeOfPurchase = (PlaceOfPurchase)(Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase));

            // Metadata update
            // Merchandise + invoice number metadata need not to be updated
            _supplierIdentifier = GetSupplierName();

            SetPostStatus();
            OEventHandler.FirePostUpdate(this);
        }

        public void RegretOrderPost()
        {
            Database.RegretOrderPost(GetId());
            _postStatus = PostStatus.Booked;
            _orderDate = new DateTime();
            _ordererUserId = NO_ID;
            _orderer = null;
        }

        public void RegretOrderConfirmed()
        {
            Database.RegretOrderConfirmed(GetId());
            _postStatus = PostStatus.Ordered;
            _confirmedOrderDate = new DateTime();
            _confirmedOrderUserId = NO_ID;
            _confirmedOrderUser = null;
        }

        public void RegretArrivalConfirmation()
        {
            Database.RegretArrivalConfirmation(GetId());
            _postStatus = PostStatus.ConfirmedOrder;
            _arrivalDate = new DateTime();
            _arrivalSignUser = null;
            _arrivalSignUserId = NO_ID;
        }

        public void RegretCompleted()
        {
            Database.RegretCompleted(GetId());
            _postStatus = PostStatus.Confirmed;
            _isInvoiceAbsent = false;
            _invoicerUser = null;
            _invoicerUserId = NO_ID;
            _invoiceStatus = InvoiceStatus.Incoming;
            _invoiceDate = new DateTime();
        }

        public void ResetMerchanidse()
        {
            _merchandise = null;
        }

        public void ResetPostInvoiceStatus()
        {
            Database.ResetPostInvoiceStatus(GetId());
        }

        private void SetBookerId(int bookerId)
        {
            _booker = null;
            _bookerUserId = bookerId;
        }

        private void SetOrderUserId(int orderUserId)
        {
            _orderer = null;
            _ordererUserId = orderUserId;
        }

        private void SetArrivalSignUserId(int arrivalSignUserId)
        {
            _arrivalSignUser = null;
            _arrivalSignUserId = arrivalSignUserId;
        }

        private void SetInvoicerUserId(int invoicerUserId)
        {
            _invoicerUser = null;
            _invoicerUserId = invoicerUserId;
        }

        private void SetInvoiceStatus(String status)
        {
            switch (status)
            {
                case "Incoming":
                    _invoiceStatus = InvoiceStatus.Incoming;
                    break;
                case "Ok":
                    _invoiceStatus = InvoiceStatus.Ok;
                    break;
                default:
                    var str = "Error, invoice status not set to proper value: ''" + status + "''";
                    throw new DataException(str);
            }
        }

        public void SetSalesOrderNo(string salesOrderNo)
        {
            Database.UpdatePostSetSalesOrderNo(GetId(), salesOrderNo);
            _salesOrderNo = salesOrderNo;
        }
    }

    public class PostList : DataIdentityList
    {
        public new Post GetById(Int32 id)
        {
            return (Post)(base.GetById(id));
        }

        public new Post this[Int32 index]
        {
            get
            {
                return (Post)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public void ResetMerchandise()
        {
            foreach (Post post in this)
            {
                post.ResetMerchanidse();
            }
        }
    }

}
