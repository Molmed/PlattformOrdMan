using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.Data
{
    public class UpdateHandlerEventArgs : EventArgs
    {
        public UpdateHandlerEventArgs(bool newSortOrder)
        {
            this.NewSortOrder = newSortOrder;
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
            Ok,
            NotOk
        }

        private ArticleNumber MyArticleNumber;
        private int MyArticleNumberId;
        private int MyBookerUserId;
        private User MyBooker;
        private DateTime MyBookDate;
        private int MyMerchandiseId;
        private Merchandise MyMerchandise;
        private int MySupplierId;
        private Supplier MySupplier;
        private decimal MyApprPrize;
        private DateTime MyOrderDate;
        private int MyOrdererUserId;
        private User MyOrderer;
        private DateTime MyPredictedArrival;
        private bool MyInvoiceInst;
        private bool MyInvoiceClin;
        private DateTime MyArrivalDate;
        private int MyArrivalSignUserId;
        private User MyArrivalSignUser;
        private int MyAmount;
        private PostStatus MyPostStatus;
        private InvoiceStatus MyInvoiceStatus;
        private DateTime MyInvoiceDate;
        private int MyInvoicerUserId;
        private User MyInvoicerUser;
        private bool MyIsInvoiceAbsent;
        private int MyCurrencyId;
        private Currency MyCurrency;
        private string MyInvoiceNumber;
        private decimal MyFinalPrize;
        private int MyConfirmedOrderUserId;
        private User MyConfirmedOrderUser;
        private DateTime MyConfirmedOrderDate;
        private string MyDeliveryDeviation;
        private string MyPurchaseOrderNo;
        private string MySalesOrderNo;
        private PlaceOfPurchase MyPlaceOfPurchase;
        private string MyComment;
        private int MyId;
        private int MyCustomerNumberId;
        private CustomerNumber MyCustomerNumber;
        // These fields are metadata for faster loading time
        private string MyMerchandiseIdentifier;
        private string MyMerchandiseAmount;
        private bool MyIsMerchandiseEnabled;
        private string MyMerchandiseComment;
        private string MySupplierIdentifier;
        private string MyCustomerNumberDescription;
        private string MyCustomerNumberIdentifier;
        private int MyInvoiceCategoryNumber;

        public Post(DataReader dataReader)
        {
            string placeOfPurchase;
            MyId = dataReader.GetInt32(DataIdentityData.ID);
            MyComment = dataReader.GetString(DataCommentData.COMMENT);
            MyBookerUserId = dataReader.GetInt32(PostData.AUTHORITY_ID_BOOKER);
            MyBookDate = dataReader.GetDateTime(PostData.BOOK_DATE);
            MyMerchandiseId = dataReader.GetInt32(PostData.MERCHANDISE_ID);
            MySupplierId = PlattformOrdManData.NO_ID;
            if (!dataReader.IsDBNull(PostData.SUPPLIER_ID))
            {
                MySupplierId = dataReader.GetInt32(PostData.SUPPLIER_ID);
            }
            MyIsInvoiceAbsent = dataReader.GetBoolean(PostData.INVOICE_ABSENT);
            MyDeliveryDeviation = dataReader.GetString(PostData.DELIVERY_DEVIATION);
            MyBooker = null;
            MyMerchandise = null;
            MySupplier = null;
            MyOrderer = null;
            MyArrivalSignUser = null;
            MyInvoicerUser = null;
            MyOrderDate = new DateTime();
            MyArrivalDate = new DateTime();
            MyApprPrize = PlattformOrdManData.NO_COUNT;
            MyArrivalSignUserId = PlattformOrdManData.NO_ID;
            MyOrdererUserId = PlattformOrdManData.NO_ID;
            MyInvoicerUserId = PlattformOrdManData.NO_ID;
            MyCurrency = null;
            MyCurrencyId = PlattformOrdManData.NO_ID;
            MyConfirmedOrderDate = new DateTime();
            if (!dataReader.IsDBNull(PostData.CURRENCY_ID))
            {
                MyCurrencyId = dataReader.GetInt32(PostData.CURRENCY_ID);
            }
            SetInvoiceStatus(dataReader.GetString(PostData.INVOICE_STATUS));
            MyInvoiceDate = new DateTime();
            MyPredictedArrival = new DateTime();
            MyInvoiceNumber = dataReader.GetString(PostData.INVOICE_NUMBER);
            MyFinalPrize = dataReader.GetDecimal(PostData.FINAL_PRIZE, PlattformOrdManData.NO_COUNT);
            MyConfirmedOrderUserId = dataReader.GetInt32(PostData.AUTHORITY_ID_CONFIRMED_ORDER, PlattformOrdManData.NO_ID);
            MyPurchaseOrderNo = "";
            MySalesOrderNo = "";
            if (!dataReader.IsDBNull(PostData.CONFIRMED_ORDER_DATE))
            {
                MyConfirmedOrderDate = dataReader.GetDateTime(PostData.CONFIRMED_ORDER_DATE);
            }
            if (!dataReader.IsDBNull(PostData.APPR_PRIZE))
            {
                MyApprPrize = dataReader.GetDecimal(PostData.APPR_PRIZE);
            }
            if (!dataReader.IsDBNull(PostData.ORDER_DATE))
            {
                MyOrderDate = dataReader.GetDateTime(PostData.ORDER_DATE);
            }
            if (!dataReader.IsDBNull(PostData.AUTHORITY_ID_ORDERER))
            {
                MyOrdererUserId = dataReader.GetInt32(PostData.AUTHORITY_ID_ORDERER);
            }
            if (!dataReader.IsDBNull(PostData.AUTHORITY_ID_INVOICER))
            {
                MyInvoicerUserId = dataReader.GetInt32(PostData.AUTHORITY_ID_INVOICER);
            }
            if (!dataReader.IsDBNull(PostData.INVOICE_DATE))
            {
                MyInvoiceDate = dataReader.GetDateTime(PostData.INVOICE_DATE);
            }
            if (!dataReader.IsDBNull(PostData.PREDICTED_ARRIVAL))
            {
                MyPredictedArrival = dataReader.GetDateTime(PostData.PREDICTED_ARRIVAL);
            }
            if (!dataReader.IsDBNull(PostData.INVOICE_INST))
            {
                MyInvoiceInst = dataReader.GetBoolean(PostData.INVOICE_INST);
            }
            if (!dataReader.IsDBNull(PostData.INVOICE_CLIN))
            {
                MyInvoiceClin = dataReader.GetBoolean(PostData.INVOICE_CLIN);
            }
            if (!dataReader.IsDBNull(PostData.ARRIVAL_DATE))
            {
                MyArrivalDate = dataReader.GetDateTime(PostData.ARRIVAL_DATE);
            }
            if (!dataReader.IsDBNull(PostData.ARRIVAL_SIGN))
            {
                MyArrivalSignUserId = dataReader.GetInt32(PostData.ARRIVAL_SIGN);
            }
            if (!dataReader.IsDBNull(PostData.AMOUNT))
            {
                MyAmount = dataReader.GetInt32(PostData.AMOUNT);
            }
            if (!dataReader.IsDBNull(PostData.PURCHASE_ORDER_NO))
            {
                MyPurchaseOrderNo = dataReader.GetString(PostData.PURCHASE_ORDER_NO);
            }

            if (!dataReader.IsDBNull(PostData.SALES_ORDER_NO))
            {
                MySalesOrderNo = dataReader.GetString(PostData.SALES_ORDER_NO);
            }

            if (!dataReader.IsDBNull(PostData.PLACE_OF_PURCHASE))
            {
                placeOfPurchase = dataReader.GetString(PostData.PLACE_OF_PURCHASE);
                MyPlaceOfPurchase = (PlaceOfPurchase)(Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase));
            }

            MyMerchandiseIdentifier = dataReader.GetString(PostData.MERCHANDISE_IDENTIFIER);
            MyMerchandiseAmount = dataReader.GetString(PostData.MERCHANDISE_AMOUNT);
            MyIsMerchandiseEnabled = dataReader.GetBoolean(PostData.MERCHANDISE_ENABLED);
            MyMerchandiseComment = dataReader.GetString(PostData.MERCHANDISE_COMMENT);
            MySupplierIdentifier = dataReader.GetString(PostData.SUPPLIER_IDENTIFIER);
            MyCustomerNumberDescription = dataReader.GetString(PostData.CUSTOMER_NUMBER_DESCRIPTION);
            MyCustomerNumberIdentifier = dataReader.GetString(PostData.CUSTOMER_NUMBER_IDENTIFIER);
            MyInvoiceCategoryNumber = dataReader.GetInt32(PostData.INVOICE_CATEGORY_NUMBER, NO_COUNT);

            MyCustomerNumberId = dataReader.GetInt32(PostData.CUSTOMER_NUMBER_ID, NO_ID);
            MyCustomerNumber = null;

            MyArticleNumber = null;
            MyArticleNumberId = dataReader.GetInt32(PostData.ARTICLE_NUMBER_ID, PlattformOrdManData.NO_ID);
            if (IsValidId(MyArticleNumberId))
            {
                LoadArticleNumber(dataReader);
            }

            SetPostStatus();
        }

        public CustomerNumber GetCustomerNumber()
        {
            if (MyCustomerNumber == null && MyCustomerNumberId != NO_ID)
            {
                MyCustomerNumber = CustomerNumberManager.GetCustomerNumberById(MyCustomerNumberId);
            }
            return MyCustomerNumber;
        }

        public bool HasCustomerNumber()
        {
            return IsNotNull(GetCustomerNumber());
        }

        public bool HasSupplierLoaded()
        {
            return IsNotNull(MySupplier);
        }

        public bool HasMerchandiseLoaded()
        {
            return IsNotNull(MyMerchandise);
        }

        public bool IsMerchandiseEnabled()
        {
            return MyIsMerchandiseEnabled;
        }

        public int GetCustomerNumberId()
        {
            return MyCustomerNumberId;
        }

        public int GetId()
        {
            return MyId;
        }

        public string GetIdentifier()
        {
            return "";
        }

        public String GetComment()
        {
            if (IsNull(MyComment))
            {
                return "";
            }
            return MyComment;
        }

        public void SetComment(String comment)
        {
            MyComment = comment;
        }

        public Boolean HasComment()
        {
            return IsNotEmpty(MyComment);
        }

        public PlaceOfPurchase GetPlaceOfPurchase()
        {
            return MyPlaceOfPurchase;
        }

        public GroupCategory GetGroupCategory()
        {
            return GetGroupCategory(MyPlaceOfPurchase);
        }

        public string GetPlaceOfPurchaseString()
        {
            return GetPlaceOfPurchaseString(MyPlaceOfPurchase);
        }

        public string GetDeliveryDeviation()
        {
            if (IsNull(MyDeliveryDeviation))
            {
                return "";
            }
            return MyDeliveryDeviation;
        }

        public string GetInvoiceNumber()
        {
            if (IsNull(MyInvoiceNumber))
            {
                return "";
            }
            return MyInvoiceNumber;
        }

        public string GetPurchaseOrderNo()
        {
            return MyPurchaseOrderNo;
        }

        public string GetSalesOrderNo()
        {
            return MySalesOrderNo;
        }

        public decimal GetFinalPrize()
        {
            return MyFinalPrize;
        }

        public int GetConfirmedOrderUserId()
        {
            return MyConfirmedOrderUserId;
        }

        public User GetConfirmedOrderUser()
        {
            if (MyConfirmedOrderUser == null && MyConfirmedOrderUserId != PlattformOrdManData.NO_ID)
            {
                MyConfirmedOrderUser = UserManager.GetUser(MyConfirmedOrderUserId);
            }
            return MyConfirmedOrderUser;
        }

        public DateTime GetConfirmedOrderDate()
        {
            return MyConfirmedOrderDate;
        }

        public ArticleNumber GetArticleNumber()
        {
            if (IsNull(MyArticleNumber) && IsValidId(MyArticleNumberId))
            {
                MyArticleNumber = MerchandiseManager.GetArticleNumberById(MyArticleNumberId);
            }
            return MyArticleNumber;
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
            reader.SetColumnNamePrefix(PostData.ARTICLE_NUMBER_PREFIX);
            MyArticleNumber = new ArticleNumber(reader);
            reader.ResetColumnNamePrefix();
        }

        private void SetPostStatus()
        {
            if (IsNull(GetOrderer()))
            {
                MyPostStatus = PostStatus.Booked;
            }
            else if(IsNull(GetConfirmedOrderUser()))
            {
                MyPostStatus = PostStatus.Ordered;
            }
            else if (IsNull(GetArrivalSignUser()))
            {
                MyPostStatus = PostStatus.ConfirmedOrder;
            }
            else if (IsNull(GetInvoicerUser()))
            {
                MyPostStatus = PostStatus.Confirmed;
            }
            else
            {
                MyPostStatus = PostStatus.Completed;
            }
        }

        public void ConfirmPostArrival(int arrivalSignUserId)
        {
            Post tmpPost;
            Database.ConfirmPostArrival(GetId(), arrivalSignUserId);
            MyArrivalSignUserId = arrivalSignUserId;
            MyArrivalSignUser = null;
            tmpPost = PostManager.GetPostById(GetId());
            MyArrivalDate = tmpPost.GetArrivalDate();
            MyPostStatus = PostStatus.Confirmed;
        }

        public Currency GetCurrency()
        {
            if (IsNull(MyCurrency))
            {
                MyCurrency = Data.CurrencyManager.GetCurrency(MyCurrencyId);
            }
            return MyCurrency;
        }

        public int GetCurrencyId()
        {
            return MyCurrencyId;
        }

        public User GetBooker()
        {
            if (IsNull(MyBooker))
            {
                MyBooker = UserManager.GetUser(MyBookerUserId);
            }
            return MyBooker;
        }

        public int GetBookerId()
        {
            return MyBookerUserId;
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
                case PostListViewColumn.CustomerNumber:
                    return GetCustomerNumberString2();
                default:
                    throw new Data.Exception.DataException("Unknwon post list view column: " + column.ToString());
            }
        }

        private string GetCustomerNumberString2()
        {
            string str = "";
            if (IsNotEmpty(MyCustomerNumberDescription))
            {
                str += MyCustomerNumberDescription + ", ";
            }
            if (IsNotEmpty(MyCustomerNumberIdentifier))
            {
                str += MyCustomerNumberIdentifier;
            }
            return str;
        }
        
        private string GetCustomerNumberString()
        {
            if (IsNotNull(GetCustomerNumber()))
            {
                return GetCustomerNumber().GetStringForCombobox();
            }
            return "";
        }

        private string GetCommentForListView()
        {
            string commentText;
            commentText = "";
            if (IsNotEmpty(MyMerchandiseComment))
            {
                commentText += MyMerchandiseComment + "; ";
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
            return MyInvoiceStatus;
        }

        public String GetInvoiceStatusString()
        {
            if (MyInvoiceStatus == InvoiceStatus.Incoming)
            {
                return "Not checked";
            }
            else if (MyInvoiceStatus == InvoiceStatus.NotOk)
            {
                return "Not OK";
            }
            else
            {
                if (MyIsInvoiceAbsent)
                {
                    return "Ok (no invoice)";
                }
                else
                {
                    return "Ok and sent";
                }
            }
        }

        public int GetInvoicerUserId()
        {
            return MyInvoicerUserId;
        }

        public User GetInvoicerUser()
        {
            if (IsNull(MyInvoicerUser))
            {
                MyInvoicerUser = UserManager.GetUser(MyInvoicerUserId);
            }
            return MyInvoicerUser;
        }

        public int GetInvoiceUserId()
        {
            return MyInvoicerUserId;
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
            return MyInvoiceDate;
        }

        public String GetInvoiceDateString()
        {
            if (MyInvoiceDate.Ticks == 0)
            {
                return "";
            }
            return MyInvoiceDate.Date.ToString("yyyy-MM-dd");
        }

        public DateTime GetBookDateDT()
        {
            return MyBookDate;
        }

        public String GetBookDate()
        {
            if (IsNull(MyBookDate))
            {
                return "";
            }
            return MyBookDate.Date.ToString("yyyy-MM-dd");
        }

        public Merchandise GetMerchandise()
        {
            if (IsNull(MyMerchandise) && MyMerchandiseId != PlattformOrdManData.NO_ID)
            {
                MyMerchandise = MerchandiseManager.GetMerchandiseById(MyMerchandiseId);
            }
            return MyMerchandise;
        }

        public int GetMerchandiseId()
        {
            return MyMerchandiseId;
        }

        public string GetMerchandiseName2()
        {
            String name;

            if (IsEmpty(MyMerchandiseIdentifier))
            {
                return "";
            }

            name = MyMerchandiseIdentifier;
            if (IsNotEmpty(MyMerchandiseAmount))
            {
                name += ", " + MyMerchandiseAmount;
            }
            return name;            
        }

        public String GetMerchandiseName()
        {
            String name;

            if (IsNull(GetMerchandise()))
            {
                return "";
            }
            name = GetMerchandise().GetIdentifier();
            if (IsNotEmpty(GetMerchandise().GetAmount()))
            {
                name = name + ", " + GetMerchandise().GetAmount();
            }
            return name;
        }

        public DateTime GetOrderDate()
        {
            return MyOrderDate;
        }

        public String GetOrderDateString()
        {
            if (MyOrderDate.Ticks == 0)
            {
                return "";
            }
            return MyOrderDate.Date.ToString("yyyy-MM-dd");
        }

        public string GetConfirmedOrderDateString()
        {
            if (MyConfirmedOrderDate.Ticks == 0)
            {
                return "";
            }
            return MyConfirmedOrderDate.Date.ToString("yyyy-MM-dd");
        }

        public PostStatus GetPostStatus()
        {
            return MyPostStatus;
        }

        public String GetPredictedArrival()
        {
            if (MyPredictedArrival.Ticks == 0)
            {
                return "";
            }
            else
            {
                return MyPredictedArrival.Date.ToString("yyyy-MM-dd");
            }
        }

        public bool HasSupplier()
        {
            return MySupplierId != PlattformOrdManData.NO_ID;
        }


        public void ResetSupplierLocal()
        {
            MySupplier = null;
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MySupplier = supplier;
            if (IsNotNull(supplier))
            {
                MySupplierId = supplier.GetId();
                MySupplierIdentifier = supplier.GetIdentifier();
            }
            else
            {
                MySupplierId = NO_ID;
                MySupplierIdentifier = "";
            }
        }

        public void ResetMerchandiseLocal()
        {
            MyMerchandise = null;
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            MyMerchandise = merchandise;
            if (IsNotNull(MyMerchandise))
            {
                MyMerchandiseAmount = MyMerchandise.GetAmount();
                MyMerchandiseComment = MyMerchandise.GetComment();
                MyMerchandiseId = MyMerchandise.GetId();
                MyMerchandiseIdentifier = MyMerchandise.GetIdentifier();
                MyIsMerchandiseEnabled = MyMerchandise.IsEnabled();
                ReloadSupplier(merchandise.GetSupplier());
            }
            else
            {
                MyMerchandiseAmount = "";
                MyMerchandiseComment = "";
                MyMerchandiseIdentifier = "";
                MyIsMerchandiseEnabled = false;
                MyMerchandiseId = NO_ID;
            }
        }

        public Supplier GetSupplier()
        {
            if (IsNull(MySupplier))
            {
                MySupplier = SupplierManager.GetSupplierById(MySupplierId);
            }
            return MySupplier;
        }

        public int GetSupplierId()
        {
            return MySupplierId;
        }

        public string GetSupplierName2()
        {
            if (IsEmpty(MySupplierIdentifier))
            {
                return "No supplier selected";                
            }
            return MySupplierIdentifier;
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
            if (obj is Post)
            {
                if (this >= (Post)obj)
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
            return MyAmount;
        }

        public string GetAmountString()
        {
            if (MyAmount == NO_COUNT)
            {
                return "";
            }
            else
            {
                return MyAmount.ToString();
            }
        }

        public decimal GetApprPrizeDecimal()
        {
            return MyApprPrize;
        }

        public String GetPriceWithCurrencyString()
        {
            if (MyApprPrize == PlattformOrdManData.NO_COUNT || IsNull(GetCurrency()))
            {
                return "";
            }
            return MyCurrency.GetPriceWithCurrencyString(MyApprPrize);        
        }

        public string GetFinalPrizeWithCurrencyString()
        {
            if (MyFinalPrize == PlattformOrdManData.NO_COUNT || IsNull(GetCurrency()))
            {
                return "";
            }
            return MyCurrency.GetPriceWithCurrencyString(MyFinalPrize);
        }



        public decimal GetTotalPrize()
        {
            if (MyAmount != NO_COUNT && MyFinalPrize != NO_COUNT && IsNotNull(GetCurrency()))
            {
                return ((decimal)MyAmount) * MyFinalPrize;
            }
            else if (MyAmount != NO_COUNT && MyApprPrize != NO_COUNT && IsNotNull(GetCurrency()))
            {
                return ((decimal)MyAmount) * MyApprPrize;
            }
            else
            {
                return NO_COUNT;
            }
        }

        public string GetTotalPrizeWithCurrencyString()
        {
            decimal prize;
            prize = GetTotalPrize();
            if (prize != NO_COUNT)
            {
                return MyCurrency.GetPriceWithCurrencyString(prize);
            }
            else
            {
                return "";
            }
        }
        
        public bool GetInvoiceClin()
        {
            return MyInvoiceClin;
        }

        public bool GetInvoiceInst()
        {
            return MyInvoiceInst;
        }

        public bool IsInvoceAbsent()
        {
            return MyIsInvoiceAbsent;
        }

        public String GetInvoiceInstString()
        {
            if (MyInvoiceInst)
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
            if (MyInvoiceCategoryNumber != NO_COUNT)
            {
                return MyInvoiceCategoryNumber.ToString();
            }
            else
            {
                return "";
            }
        }


        public String GetInvoiceCategoryCodeString()
        {
            if (IsNull(GetMerchandise()))
            {
                return "";
            }
            return GetMerchandise().GetInvoiceCategoryCodeString();
        }

        public String GetInvoiceClinString()
        {
            if (MyInvoiceClin)
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
            if (IsNull(MyOrderer))
            {
                MyOrderer = UserManager.GetUser(MyOrdererUserId);
            }
            return MyOrderer;
        }

        public int GetOrderUserId()
        {
            return MyOrdererUserId;
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
            return MyArrivalDate;
        }

        public String GetArrivalDateString()
        {
            if(MyArrivalDate.Ticks == 0)
            {
                return "";
            }
            return MyArrivalDate.Date.ToString("yyyy-MM-dd");
        }

        public User GetArrivalSignUser()
        {
            if (IsNull(MyArrivalSignUser))
            {
                MyArrivalSignUser = UserManager.GetUser(MyArrivalSignUserId);
            }
            return MyArrivalSignUser;
        }

        public int GetArrivalSignUserId()
        {
            return MyArrivalSignUserId;
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
            int activeANId = NO_ID;
            int selANId = NO_ID;
            if (GetMerchandise().HasArticleNumber())
            {
                activeANId = GetMerchandise().GetActiveArticleNumber().GetId();
            }
            if (IsNotNull(GetArticleNumber()))
            {
                selANId = GetArticleNumber().GetId();
            }

            return selANId == activeANId;
        }

        public void OrderPost(int ordererUserId)
        {
            Post tmpPost;
            Database.OrderPost(GetId(), ordererUserId);
            tmpPost = PostManager.GetPostById(GetId());
            MyOrderDate = tmpPost.GetOrderDate();
            MyOrdererUserId = ordererUserId;
            MyOrderer = null;
            SetPostStatus();
            if (MyIsInvoiceAbsent)
            {
                SignPostInvoice(UserManager.GetUser(ordererUserId), InvoiceStatus.Ok, MyIsInvoiceAbsent);
            }
        }

        public void ConfirmPostOrdered(int userId)
        {
            Post tempPost;
            Database.ConfirmPostOrdered(GetId(), userId);
            tempPost = PostManager.GetPostById(GetId());
            MyConfirmedOrderDate = tempPost.GetConfirmedOrderDate();
            MyConfirmedOrderUserId = tempPost.GetConfirmedOrderUserId();
            MyConfirmedOrderUser = null;
            SetPostStatus();
        }

        public bool IsCustomerNumberHandled()
        {
            if (IsNotNull(GetSupplier()) &&
                GetSupplier().GetCustomerNumbersForCurrentUserGroup().Count > 0 &&
                GetCustomerNumberId() == NO_ID)
            {
                return false;
            }
            return true;
        }

        public void SignPostInvoice(User signer, InvoiceStatus status, bool isInvoiceAbsent)
        {
            Post tmpPost;
            Database.SignPostInvoice(GetId(), signer.GetId(), status.ToString(), isInvoiceAbsent);
            tmpPost = PostManager.GetPostById(GetId());
            MyInvoiceDate = tmpPost.GetInvoiceDate();
            MyInvoiceStatus = status;
            MyInvoicerUserId = signer.GetId();
            MyInvoicerUser = null;
            MyIsInvoiceAbsent = isInvoiceAbsent;
            SetPostStatus();
        }

        public void SetApprPrizeLocal(decimal prize)
        {
            MyApprPrize = prize;
        }

        public void UpdateCustomerNumberId(int custNumId)
        {
            DateTime apprArrival;
            DateTime.TryParse(GetPredictedArrival(), out apprArrival);

            UpdatePost(GetComment(), GetApprPrizeDecimal(), GetAmount(), GetInvoiceClin(), GetInvoiceInst(),
                apprArrival, GetInvoiceStatus().ToString(), IsInvoceAbsent(), GetCurrencyId(), GetBookerId(),
                GetBookDateDT(), GetOrderUserId(), GetOrderDate(), GetArrivalSignUserId(), GetArrivalDate(),
                GetInvoicerUserId(), GetInvoiceDate(), MyArticleNumberId, GetSupplierId(), GetInvoiceNumber(), GetFinalPrize(),
                GetConfirmedOrderDate(), GetConfirmedOrderUserId(), GetDeliveryDeviation(), GetPurchaseOrderNo(), 
                GetSalesOrderNo(), GetPlaceOfPurchase().ToString(), custNumId);
        }

        public void UpdateInvoiceNumber(string invoiceNumber, int customerNumberId,
            bool isNoInvoice)
        { 
            DateTime apprArrival;
            DateTime.TryParse(GetPredictedArrival(), out apprArrival);
            
            UpdatePost(GetComment(), GetApprPrizeDecimal(), GetAmount(), GetInvoiceClin(), GetInvoiceInst(),
                apprArrival, GetInvoiceStatus().ToString(), isNoInvoice, GetCurrencyId(),
                GetBookerId(), GetBookDateDT(), GetOrderUserId(), GetOrderDate(), GetArrivalSignUserId(),
                GetArrivalDate(), GetInvoicerUserId(), GetInvoiceDate(), MyArticleNumberId, GetSupplierId(),
                invoiceNumber, GetFinalPrize(), GetConfirmedOrderDate(), GetConfirmedOrderUserId(),
                GetDeliveryDeviation(), GetPurchaseOrderNo(), GetSalesOrderNo(), GetPlaceOfPurchase().ToString(), customerNumberId);
        }

        public void UpdatePost(String comment, decimal apprPrize, int amount, bool invoiceClin, bool invoiceInst,
            DateTime apprArrival, String invoiceStatus, bool isInvoiceAbsent, int currencyId, int bookerUserId, 
            DateTime bookDate, int orderUserId, DateTime orderDate, int arrivalSignUserId, 
            DateTime arrivalDate, int invoiceCheckerUserId, DateTime invoiceDate, 
            int articleNumberId, int supplierId, string invoiceNumber, decimal finalPrize, 
            DateTime confirmedOrderDate, int confirmedOrderUserId, string deliveryDeviation,
            string purchaseOrderNo, string salesOrderNo, string placeOfPurchase, int customerNumberId)
        {
            Database.UpdatePost(GetId(), comment, apprPrize, amount, invoiceClin, invoiceInst, apprArrival, 
                invoiceStatus, isInvoiceAbsent, currencyId, bookerUserId, bookDate, orderUserId, orderDate,
                arrivalSignUserId, arrivalDate, invoiceCheckerUserId, invoiceDate, articleNumberId, supplierId,
                invoiceNumber, finalPrize, confirmedOrderDate, confirmedOrderUserId, deliveryDeviation,
                purchaseOrderNo, salesOrderNo, placeOfPurchase, customerNumberId);
            SetComment(comment);
            MyCustomerNumberId = customerNumberId;
            MyCustomerNumber = null;
            MyApprPrize = apprPrize;
            MyAmount = amount;
            MyInvoiceClin = invoiceClin;
            MyInvoiceInst = invoiceInst;
            MyPredictedArrival = apprArrival;
            MyIsInvoiceAbsent = isInvoiceAbsent;
            MyCurrencyId = currencyId;
            MyCurrency = null;
            SetBookerId(bookerUserId);
            SetOrderUserId(orderUserId);
            SetArrivalSignUserId(arrivalSignUserId);
            SetInvoicerUserId(invoiceCheckerUserId);
            MyBookDate = bookDate;
            MyOrderDate = orderDate;
            MyArrivalDate = arrivalDate;
            MyInvoiceDate = invoiceDate;
            SetInvoiceStatus(invoiceStatus);
            MyArticleNumberId = articleNumberId;
            MyArticleNumber = null;
            MySupplierId = supplierId;
            MySupplier = null;
            MyInvoiceNumber = invoiceNumber;
            MyFinalPrize = finalPrize;
            MyConfirmedOrderDate = confirmedOrderDate;
            MyConfirmedOrderUserId = confirmedOrderUserId;
            MyConfirmedOrderUser = null;
            MyDeliveryDeviation = deliveryDeviation;
            MyPurchaseOrderNo = purchaseOrderNo;
            MySalesOrderNo = salesOrderNo;
            MyPlaceOfPurchase = (PlaceOfPurchase)(Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase));

            // Metadata update
            // Merchandise + invoice number metadata need not to be updated
            MySupplierIdentifier = GetSupplierName();
            if (IsNotNull(GetCustomerNumber()))
            {
                MyCustomerNumberDescription = GetCustomerNumber().GetDescription();
                MyCustomerNumberIdentifier = GetCustomerNumber().GetIdentifier();
            }
            else
            {
                MyCustomerNumberDescription = "";
                MyCustomerNumberIdentifier = "";
            }

            SetPostStatus();
            OEventHandler.FirePostUpdate(this);
        }

        public void RegretOrderPost()
        {
            Database.RegretOrderPost(GetId());
            MyPostStatus = PostStatus.Booked;
            MyOrderDate = new DateTime();
            MyOrdererUserId = PlattformOrdManData.NO_ID;
            MyOrderer = null;
        }

        public void RegretOrderConfirmed()
        {
            Database.RegretOrderConfirmed(GetId());
            MyPostStatus = PostStatus.Ordered;
            MyConfirmedOrderDate = new DateTime();
            MyConfirmedOrderUserId = PlattformOrdManData.NO_ID;
            MyConfirmedOrderUser = null;
        }

        public void RegretArrivalConfirmation()
        {
            Database.RegretArrivalConfirmation(GetId());
            MyPostStatus = PostStatus.ConfirmedOrder;
            MyArrivalDate = new DateTime();
            MyArrivalSignUser = null;
            MyArrivalSignUserId = PlattformOrdManData.NO_ID;
        }

        public void RegretCompleted()
        {
            Database.RegretCompleted(GetId());
            MyPostStatus = PostStatus.Confirmed;
            MyIsInvoiceAbsent = false;
            MyInvoicerUser = null;
            MyInvoicerUserId = PlattformOrdManData.NO_ID;
            MyInvoiceStatus = InvoiceStatus.Incoming;
            MyInvoiceDate = new DateTime();
        }

        public void ResetMerchanidse()
        {
            MyMerchandise = null;
        }

        public void ResetPostInvoiceStatus()
        {
            Database.ResetPostInvoiceStatus(GetId());
        }

        private void SetBookerId(int bookerId)
        {
            MyBooker = null;
            MyBookerUserId = bookerId;
        }

        public void SetMerchandise(Merchandise merchandise)
        {
            MyMerchandise = merchandise;
        }

        private void SetOrderUserId(int orderUserId)
        {
            MyOrderer = null;
            MyOrdererUserId = orderUserId;
        }

        private void SetArrivalSignUserId(int arrivalSignUserId)
        {
            MyArrivalSignUser = null;
            MyArrivalSignUserId = arrivalSignUserId;
        }

        private void SetInvoicerUserId(int invoicerUserId)
        {
            MyInvoicerUser = null;
            MyInvoicerUserId = invoicerUserId;
        }

        public void SetInvoiceStatus(String status)
        {
            string str;
            switch (status)
            {
                case "Incoming":
                    MyInvoiceStatus = InvoiceStatus.Incoming;
                    break;
                case "Ok":
                    MyInvoiceStatus = InvoiceStatus.Ok;
                    break;
                case "NotOk":
                    MyInvoiceStatus = InvoiceStatus.NotOk;
                    break;
                default:
                    str = "Error, invoice status not set to proper value: ''" + status + "''";
                    throw new Data.Exception.DataException(str);
            }
        }

        public void SetSalesOrderNo(string salesOrderNo)
        {
            Database.UpdatePostSetSalesOrderNo(GetId(), salesOrderNo);
            MySalesOrderNo = salesOrderNo;
        }

        public void SetPostStatus(PostStatus postStatus)
        {
            MyPostStatus = postStatus;
        }

        private String TruncDecimals(String str)
        {
            int residue = 0;
            if (str.IndexOf(".") > -1)
            {
                if (str.Substring(str.IndexOf(".")).Contains("0000"))
                {
                    return str.Substring(0, str.IndexOf("."));
                }
                residue = Math.Min(3, str.Length - str.IndexOf("."));
                return str.Substring(0, str.IndexOf(".") + residue);
            }
            return str;
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

        public new Post this[String identifier]
        {
            get
            {
                return (Post)(base[identifier]);
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
