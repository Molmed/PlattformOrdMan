using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class Merchandise : DataComment
    {
        private int MySupplierId;
        private bool MyIsEnabled;
        private String MyAmount;
        private decimal MyApprPrize;
        private String MyStorage;
        private int MyCurrentArticleNumberId;
        private ArticleNumber MyCurrentArticleNumber;
        private ArticleNumberList MyArticleNumbers;
        private Supplier MySupplier;
        private String MyCategory;
        private int MyInvoiceCategoryId;
        private InvoiceCategory MyInvoiceCategory;
        private int MyCurrencyId;
        private Currency MyCurrency;
        // Meta data fields for faster loading times
        private string MySupplierIdentifier;

        public Merchandise(int id, string identifier, string comment, int supplierId, bool enabled, string amount,
            decimal apprPrize, string storage, int currentArtNumId, string category, int invoiceCategoryId, int currencyId,
            string supplierIdentifier)
            : base(comment, identifier, id)
        {
            MySupplierId = supplierId;
            MyIsEnabled = enabled;
            MyAmount = amount;
            MyApprPrize = apprPrize;
            MyStorage = storage;
            MyCurrentArticleNumberId = currentArtNumId;
            MyCategory = category;
            MyInvoiceCategoryId = invoiceCategoryId;
            MyCurrencyId = currencyId;
            MySupplierIdentifier = supplierIdentifier;
            MyCurrentArticleNumber = null;
            MyArticleNumbers = null;
            MySupplier = null;
            MyInvoiceCategory = null;
            MyCurrency = null;
        }

        public Merchandise Clone()
        {
            Merchandise merch = new Merchandise(GetId(), GetIdentifier(), GetComment(), 
                GetSupplierId(), IsEnabled(), GetAmount(), GetApprPrize(), GetStorage(), GetCurrentArticleNumberId(),
                GetCategory(), GetInvoiceCagegoryId(), GetCurrencyId(), GetSupplierName());
            return merch;
        }

        public Merchandise(DataReader dataReader)
            : base(dataReader)
        {
            MySupplier = null;
            MySupplierId = PlattformOrdManData.NO_ID;
            if(!dataReader.IsDBNull(MerchandiseData.SUPPLIER_ID))
            {
                MySupplierId = dataReader.GetInt32(MerchandiseData.SUPPLIER_ID);
            }
            MyIsEnabled = dataReader.GetBoolean(MerchandiseData.ENABLED);
            MyAmount = null;
            MyInvoiceCategory = null;
            if (!dataReader.IsDBNull(MerchandiseData.AMOUNT))
            {
                MyAmount = dataReader.GetString(MerchandiseData.AMOUNT);
            }
            MyApprPrize = PlattformOrdManData.NO_COUNT;
            if(!dataReader.IsDBNull(MerchandiseData.APPR_PRIZE))
            {
                MyApprPrize = dataReader.GetDecimal(MerchandiseData.APPR_PRIZE);
            }
            MyStorage = null;
            if (!dataReader.IsDBNull(MerchandiseData.STORAGE))
            {
                MyStorage = dataReader.GetString(MerchandiseData.STORAGE);
            }
            MyCategory = null;
            if (!dataReader.IsDBNull(MerchandiseData.CATEGORY))
            {
                MyCategory = dataReader.GetString(MerchandiseData.CATEGORY);
            }
            MyInvoiceCategoryId= PlattformOrdManData.NO_ID;
            if (!dataReader.IsDBNull(MerchandiseData.INVOICE_CATEGORY_ID))
            {
                MyInvoiceCategoryId= dataReader.GetInt32(MerchandiseData.INVOICE_CATEGORY_ID);
            }
            MyCurrencyId = PlattformOrdManData.NO_ID;
            if (!dataReader.IsDBNull(MerchandiseData.CURRENCY_ID))
            {
                MyCurrencyId = dataReader.GetInt32(MerchandiseData.CURRENCY_ID);
            }
            MyCurrentArticleNumberId = PlattformOrdManData.NO_ID;
            if (!dataReader.IsDBNull(MerchandiseData.ARTICLE_NUMBER_ID))
            {
                MyCurrentArticleNumberId = dataReader.GetInt32(MerchandiseData.ARTICLE_NUMBER_ID);
                LoadCurrentArticleNumber(dataReader);
            }
            MySupplierIdentifier = dataReader.GetString(MerchandiseData.SUPPLIER_IDENTIFIER);
            MyArticleNumbers = null;
        }

        private void LoadCurrentArticleNumber(DataReader reader)
        {
            reader.SetColumnNamePrefix(ArticleNumberData.ARTICLE_NUMBER_PREFIX);
            MyCurrentArticleNumber = new ArticleNumber(reader);
            reader.ResetColumnNamePrefix();
        }

        public int GetCurrencyId()
        {
            return MyCurrencyId;
        }

        public void ResetSupplierLocal()
        {
            MySupplier = null;
        }

        public void ReloadSupplier(Supplier supplier)
        {
            MySupplier = supplier;
            if (IsNotNull(MySupplier))
            {
                MySupplierId = supplier.GetId();
            }
        }

        public Currency GetCurrency()
        {
            CurrencyList currencies;
            if (IsNull(MyCurrency) && MyCurrencyId != PlattformOrdManData.NO_ID)
            { 
                currencies = Data.CurrencyManager.GetCurrencies();
                MyCurrency = currencies.GetById(MyCurrencyId);
            }
            return MyCurrency;
        }

        public ArticleNumberList GetArticleNumbers()
        {
            if (IsNull(MyArticleNumbers))
            {
                MyArticleNumbers = MerchandiseManager.GetArticleNumbersForMerchandise(GetId());
            }
            return MyArticleNumbers;
        }

        public ArticleNumber GetActiveArticleNumber()
        {
            foreach (ArticleNumber an in GetArticleNumbers())
            {
                if (an.IsActice())
                {
                    return an;
                }
            }
            return null;
        }

        public string GetActiveArticleNumberString()
        {
            ArticleNumber an = GetActiveArticleNumber();
            if (IsNotNull(an))
            {
                return an.GetIdentifier();
            }
            else
            {
                return "";
            }
        }

        public override DataType GetDataType()
        {
            return DataType.Merchandise;
        }

        public bool HasSupplier()
        {
            return MySupplierId != PlattformOrdManData.NO_ID;
        }

        public int GetSupplierId()
        {
            return MySupplierId;
        }

        public String GetInvoiceCategoryName()
        {
            if (IsNull(GetInvoiceCategory()))
            {
                return "";
            }
            return GetInvoiceCategory().GetIdentifier();
        }

        public String GetInvoiceCategoryCodeString()
        {
            if (IsNull(GetInvoiceCategory()))
            {
                return "Not given";
            }
            return GetInvoiceCategory().GetNumber().ToString();
        }

        public InvoiceCategory GetInvoiceCategory()
        {
            if (IsNull(MyInvoiceCategory) && MyInvoiceCategoryId != PlattformOrdManData.NO_ID)
            {
                MyInvoiceCategory = InvoiceCategoryManager.GetInvoiceCategoryById(MyInvoiceCategoryId);
            }
            return MyInvoiceCategory;
        }

        public int GetInvoiceCagegoryId()
        {
            return MyInvoiceCategoryId;
        }

        public Supplier GetSupplier()
        {
            if (IsNull(MySupplier))
            {
                if (GetSupplierId() == PlattformOrdManData.NO_ID)
                {
                    return null;
                }
                MySupplier = SupplierManager.GetSupplierById(GetSupplierId());
            }
            return MySupplier;
        }

        public String GetSupplierName()
        {
            if (IsNotEmpty(MySupplierIdentifier))
            {
                return MySupplierIdentifier;
            }
            else
            {
                return "";
            }
        }

        public String GetSupplierShortName()
        {
            if (IsNull(GetSupplier()))
            {
                return "No supplier specified";
            }
            return GetSupplier().GetShortName();
        }

        public String GetAmount()
        {
            if(IsNull(MyAmount))
            {
                return "";
            }
            return MyAmount;
        }

        public String GetEnabledString()
        {
            if (MyIsEnabled)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public decimal GetApprPrize()
        {
            return MyApprPrize;
        }

        public String GetApprPrizeString()
        {
            if (GetApprPrize() == PlattformOrdManData.NO_COUNT || IsNull(GetCurrency()))
            {
                return "";
            }
            return MyCurrency.GetPriceWithCurrencyString(GetApprPrize());
        }

        public String GetCategory()
        {
            if (IsNull(MyCategory))
            {
                return "";
            }
            return MyCategory;
        }

        public String GetStorage()
        {
            if (IsNull(MyStorage))
            {
                return "";
            }
            return MyStorage;
        }

        private ArticleNumber GetArticleNumberFromList(int id)
        {
            foreach (ArticleNumber ar in GetArticleNumbers())
            {
                if (ar.GetId() == id)
                {
                    return ar;
                }
            }
            return null;
        }

        private ArticleNumber GetArticleNumberFromList(string articleNumber)
        {
            foreach (ArticleNumber ar in GetArticleNumbers())
            {
                if (ar.GetIdentifier() == articleNumber)
                {
                    return ar;
                }
            }
            return null;
        }

        public int GetCurrentArticleNumberId()
        {
            return MyCurrentArticleNumberId;
        }

        public ArticleNumber GetCurrentArticleNumber()
        {
            if (IsNull(MyCurrentArticleNumber) && MyCurrentArticleNumberId != PlattformOrdManData.NO_ID)
            {
                MyCurrentArticleNumber = GetArticleNumberFromList(MyCurrentArticleNumberId);
            }
            return MyCurrentArticleNumber;
        }

        public String GetCurrentArticleNumberString()
        {
            if (IsNull(GetCurrentArticleNumber()))
            {
                return "";
            }
            return GetCurrentArticleNumber().GetIdentifier();
        }

        public bool IsEnabled()
        {
            return MyIsEnabled;
        }

        public void Set()
        {
            try
            {
                Database.UpdateMerchandise(GetId(), GetIdentifier(), GetComment(), GetSupplierId(), GetAmount(),
                    GetApprPrize(), GetStorage(), IsEnabled(), GetInvoiceCagegoryId(),
                    GetCurrencyId());
                OEventHandler.FireMerchandiseUpdate(this);
            }
            catch
            {
                throw;
            }
            finally
            {
                MerchandiseManager.RefreshCache();
            }
        }

        public void SetCurrentArticleNumberId(int articleNumberId)
        {
            MyCurrentArticleNumberId = articleNumberId;
        }

        public void SetSupplierId(int supplierId)
        {
            MySupplierId = supplierId;
            MySupplier = SupplierManager.GetSupplierById(supplierId);
            if (IsNotNull(MySupplier))
            {
                MySupplierIdentifier = MySupplier.GetIdentifier();
            }
            else
            {
                MySupplierIdentifier = "";
            }
        }

        public void SetEnabled(bool enabled)
        {
            MyIsEnabled = enabled;
        }

        public void SetAmount(String amount)
        {
            MyAmount = amount;
        }

        public void SetInvoiceCategoryId(int invoiceCategoryId)
        {
            MyInvoiceCategoryId = invoiceCategoryId;
            MyInvoiceCategory = null;
        }

        public void SetCurrencyId(int currencyId)
        {
            MyCurrencyId = currencyId;
            MyCurrency = null;
        }

        public void SetIdentifier(String identifier)
        {
            UpdateIdentifier(identifier);
        }

        public void SetApprPrize(decimal prize)
        {
            MyApprPrize = prize;
        }

        public void SetStorage(String storage)
        {
            MyStorage = storage;
        }

        public void SetCategory(String category)
        {
            MyCategory = category;
        }

        public void AddArticleNumber(string articleNumber, bool makeCurrent)
        {
            if (makeCurrent)
            {
                SetArticleNumberAsCurrent(articleNumber);
            }
            else
            {
                AddArticleNumberNotCurrent(articleNumber);
            }
        }

        public bool HasArticleNumber()
        {
            return IsNotEmpty(GetArticleNumbers());
        }

        public bool HasArticleNumber(string articleNumberString)
        {
            foreach (ArticleNumber ar in GetArticleNumbers())
            {
                if (articleNumberString.Trim() == ar.GetIdentifier().Trim())
                {
                    return true;
                }
            }
            return false;
        }

        private void SetArticleNumberAsCurrent(string articleNumber)
        {
            ArticleNumber ar = GetArticleNumberFromList(articleNumber);
            if (IsNull(ar) && !IsEmpty(articleNumber))
            {
                ar = MerchandiseManager.CreateArticleNumber(articleNumber, true, GetId());
                MyCurrentArticleNumberId = ar.GetId();
            }
            else if (IsEmpty(articleNumber))
            {
                Database.ResetArticleNumbersForMerchandise(GetId());
                MyCurrentArticleNumberId = PlattformOrdManData.NO_ID;                
            }
            else
            {
                ar.SetIsActive(true);
                ar.Set();
                MyCurrentArticleNumberId = ar.GetId();
            }
            MyCurrentArticleNumber = null;
            MyArticleNumbers = null;
        }

        private void AddArticleNumberNotCurrent(string articleNumber)
        {
            ArticleNumber ar = GetArticleNumberFromList(articleNumber);
            if (IsNull(ar) && !IsEmpty(articleNumber))
            {
                ar = MerchandiseManager.CreateArticleNumber(articleNumber, false, GetId());
            }
            MyArticleNumbers = null;
        }

    }
    public class MerchandiseList : DataIdentityList
    {
        public new Merchandise GetById(Int32 id)
        {
            return (Merchandise)(base.GetById(id));
        }

        public new Merchandise this[Int32 index]
        {
            get
            {
                return (Merchandise)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public new Merchandise this[String identifier]
        {
            get
            {
                return (Merchandise)(base[identifier]);
            }
        }
    }
}
