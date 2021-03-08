using System;
using System.Collections;
using System.Globalization;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public enum PlaceOfPurchase
    {
        SNP,
        SEQ,
        FoU,
        Research,
        Other
    }

    public enum GroupCategory
    { 
        Plattform,
        Research
    }

    public enum PostListViewColumn
    {
        BookDate,
        Booker,
        Product,
        ArtNr,
        Amount,
        Supplier,
        InvoiceCategoryCode,
        ApprPrize,
        FinalPrize,
        TotalPrize,
        ApprArrival,
        InvoiceInst,
        InvoiceClin,
        InvoiceNumber,
        InvoiceStatus,
        DeliveryDeviation,
        OrderDate,
        OrderSign,
        ArrivalDate,
        ArrivalSign,
        InvoiceDate,
        InvoiceSender,
        PurchaseSalesOrderNo,
        Comment,
        PlaceOfPurchase,
        Account,
        Periodization,
        VangenSummary
    }

    public class PlattformOrdManData : PlattformOrdManBase
    {
        public const Int32 NO_COUNT = -1;
        public const Int32 NO_ID = -1;
        public const int LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH = -2;
        public const int LIST_VIEW_COLUMN_HEADER_AUTO_WIDTH = -1;
        // Search for file winuser.h for constant values 
        // In different folders under C:\Program Files (x86)\
        public const Int32 SC_MAXIMIZE = 0xF030;
        public const Int32 WM_SYSCOMMAND = 0x0112;
        public const Int32 SC_RESTORE = 0xF120;
        public const Int32 SC_MINIMIZE = 0XF020;

        private static Dataserver _database;
        private static Hashtable _columnLenghtTable;
        private static Hashtable _eventHandlerTable;
        private static TransactionCommitedEventHandler _transactionCommitedEventHandler;
        private static TransactionRollbackedEventHandler _transactionRollbackedEventHandler;
        private static int _tempIdCounter;

        static PlattformOrdManData()
        {
            _tempIdCounter = NO_ID;
        }

        protected static int GetNewTempId()
        {
            _tempIdCounter--;
            return _tempIdCounter;
        }

        public static DateTime CustomerNumberReformDate { get; } = new DateTime(2013, 12, 1);

        public static string GetPlaceOfPurchaseString(PlaceOfPurchase pop)
        {
            if (pop == PlaceOfPurchase.Other)
            {
                return "Plattform unspec.";
            }
            else
            {
                return pop.ToString();
            }
        }

        public static PlaceOfPurchase GetPlaceOfPurchaseFromString(string popStr)
        { 
            foreach(PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                if (popStr == GetPlaceOfPurchaseString(pop))
                {
                    return pop;
                }
            }
            throw new DataException("String ''" + popStr + "'' does not match a valid defined PlaceOfPurchase enum");
        }

        public static CultureInfo MyCultureInfo { get; } = new CultureInfo("sv-SE");

        public static Configuration Configuration { get; set; }

        public static OrdManEventHandler OEventHandler { get; set; }

        public static Dataserver Database
        {
            get { return _database; }
            set
            {
                if (IsNotNull(_database))
                {
                    _database.TransactionCommited -= _transactionCommitedEventHandler;
                    _database.TransactionRollbacked -= _transactionRollbackedEventHandler;
                }
                _database = value;
                if (IsNotNull(_database))
                {
                    _transactionCommitedEventHandler = TransactionCommited;
                    _database.TransactionCommited += _transactionCommitedEventHandler;
                    _transactionRollbackedEventHandler = TransactionRollbacked;
                    _database.TransactionRollbacked += _transactionRollbackedEventHandler;
                    _eventHandlerTable = new Hashtable();
                }
            }
        }

        protected static decimal ParsePrice(String priceString)
        {
            int firstInd = -1, lastInd = -1;
            string decimalSymbol = MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            // Extract the numeric part of str
            NumberStyles style = NumberStyles.Float;
            priceString = priceString.Replace(".", decimalSymbol);
            priceString =  priceString.Replace(",", decimalSymbol);
            decimal price;
            // Find indices for numeric characters
            // Extract the numeric sequence and parse is to decimal
            for (int i = 0; i < priceString.Length; i++)
            {
                if (firstInd == -1 && (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ','))
                {
                    firstInd = i;
                }
                if (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ',')
                {
                    lastInd = i;
                }
            }
            if (firstInd > -1 && lastInd > -1 &&
                decimal.TryParse(priceString.Substring(firstInd, lastInd - firstInd + 1), style,
                MyCultureInfo, out price))
            {
                return price;
            }
            return -1;
        }

        public static GroupCategory GetGroupCategory(PlaceOfPurchase placeOfPurchase)
        {
            switch (placeOfPurchase)
            { 
                case PlaceOfPurchase.Research:
                    return GroupCategory.Research;
                case PlaceOfPurchase.Other:
                case PlaceOfPurchase.SEQ:
                case PlaceOfPurchase.SNP:
                case PlaceOfPurchase.FoU:
                    return GroupCategory.Plattform;
                default:
                    throw new DataException("Unknwon place of purchase: " + placeOfPurchase);
            }
        }

        public static PlaceOfPurchase GetDefaultPlateOfPurchase(GroupCategory groupCategory)
        {
            switch (groupCategory)
            { 
                case GroupCategory.Plattform:
                    return PlaceOfPurchase.Other;
                case GroupCategory.Research:
                    return PlaceOfPurchase.Research;
                default:
                    throw new DataException("Unknown group category: " + groupCategory);
            }
        }

        public static decimal ParsePrice(String priceString, out string currencyString)
        {
            int firstInd = -1, lastInd = -1;
            string decimalSymbol = MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            // Extract the numeric part of str, return as decimal
            // Extract the non-numeric part as currency string
            // Look for currency string at beginning and at end
            NumberStyles style = NumberStyles.Float;
            priceString = priceString.Replace(".", decimalSymbol);
            priceString = priceString.Replace(",", decimalSymbol);
            decimal price;
            currencyString = "";
            priceString = priceString.Trim();
            // Find indices for numeric characters
            // Extract the numeric sequence and parse is to decimal
            for (int i = 0; i < priceString.Length; i++)
            {
                if (firstInd == -1 && (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ','))
                {
                    firstInd = i;
                }
                if (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ',')
                {
                    lastInd = i;
                }
            }
            if (firstInd > -1 && lastInd > -1 &&
                decimal.TryParse(priceString.Substring(firstInd, lastInd - firstInd + 1), style,
                MyCultureInfo, out price))
            {
                if (firstInd > 0)
                {
                    currencyString = priceString.Substring(0, firstInd);
                }
                else if (lastInd < priceString.Length - 1)
                {
                    currencyString = priceString.Substring(lastInd + 1).Trim();
                }
                return price;
            }
            return -1;
        }

        private static Boolean AreEqual(IDataIdentifier object1, IDataIdentifier object2)
        {
            // Check referenses.
            if (IsNull(object1) && IsNull(object2))
            {
                return true;
            }
            if (IsNull(object1) || IsNull(object2))
            {
                return false;
            }

            // Check type.
            if (object1.GetDataType() != object2.GetDataType())
            {
                return false;
            }

            // Check identifier.
            return AreEqual(object1.GetIdentifier(), object2.GetIdentifier());
        }

        private static Boolean AreEqual(IDataIdentity object1, IDataIdentity object2)
        {
            // Check referenses.
            if (IsNull(object1) && IsNull(object2))
            {
                return true;
            }
            if (IsNull(object1) || IsNull(object2))
            {
                return false;
            }

            // Check type.
            if (object1.GetDataType() != object2.GetDataType())
            {
                return false;
            }

            // Check id.
            return object1.GetId() == object2.GetId();
        }

        public static void BeginTransaction()
        {
            Database.BeginTransaction();
        }

        protected static void CheckLength(String value,
                               String argumentName,
                               Int32 maxLength)
        {
            if (IsToLong(value, maxLength))
            {
                throw new DataArgumentLengthException(argumentName, maxLength);
            }
        }

        protected static void CheckNotEmpty(String value, String argumentName)
        {
            if (IsEmpty(value))
            {
                throw new DataArgumentEmptyException(argumentName);
            }
        }

        protected static void CloseDataReader(DataReader dataReader)
        {
            dataReader?.Close();
        }

        private static Int32 LoopWithCharFirst(String string1, String string2)
        {
            // Belongs to CompareStringWithNumbers
            int i = 0, j = 0;
            while (i < string1.Length && j < string2.Length)
            {
                var startInd1 = i;
                var startInd2 = j;
                //separate the string part
                while (++i < string1.Length && !char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && !char.IsNumber(string2[j]))
                { }
                var subString1 = string1.Substring(startInd1, i - startInd1);
                var subString2 = string2.Substring(startInd2, j - startInd2);
                var output = String.Compare(subString1, subString2, StringComparison.Ordinal);
                if (output != 0)
                {
                    return output;
                }
                if (i == string1.Length || j == string2.Length)
                {
                    break;
                }
                startInd1 = i;
                startInd2 = j;
                //handle the number part
                while (++i < string1.Length && char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && char.IsNumber(string2[j]))
                { }
                var numberStr = string1.Substring(startInd1, i - startInd1);
                var number1 = Convert.ToInt64(numberStr);
                numberStr = string2.Substring(startInd2, j - startInd2);
                var number2 = Convert.ToInt64(numberStr);
                if (number1 > number2)
                {
                    return 1;
                }
                else if (number1 < number2)
                {
                    return -1;
                }
            }
            return 0;
        }

        private static Int32 LoopWithNumberFirst(String string1, String string2)
        {
            // Belongs to CompareStringWithNumbers
            int i = 0, j = 0;
            while (i < string1.Length && j < string2.Length)
            {
                var startInd1 = i;
                var startInd2 = j;
                //handle the number part
                while (++i < string1.Length && char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && char.IsNumber(string2[j]))
                { }
                var numberStr = string1.Substring(startInd1, i - startInd1);
                var number1 = Convert.ToInt64(numberStr);
                numberStr = string2.Substring(startInd2, j - startInd2);
                var number2 = Convert.ToInt64(numberStr);
                if (number1 > number2)
                {
                    return 1;
                }
                else if (number1 < number2)
                {
                    return -1;
                }
                if (i == string1.Length || j == string2.Length)
                {
                    break;
                }
                startInd1 = i;
                startInd2 = j;
                //separate the string part
                while (++i < string1.Length && !char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && !char.IsNumber(string2[j]))
                { }
                var subString1 = string1.Substring(startInd1, i - startInd1);
                var subString2 = string2.Substring(startInd2, j - startInd2);
                var output = String.Compare(subString1, subString2, StringComparison.Ordinal);
                if (output != 0)
                {
                    return output;
                }
            }
            return 0;
        }

        public static void CommitTransaction()
        {
            Database.CommitTransaction();
        }

        public static Int32 CompareStringWithNumbers(String string1, String string2)
        {
            // Handles a string with numbers in it, and handles string and number separately
            // e.g. string10 comes after string9, and string10string20 comes after string10string3
            int output;
            if (string1.Length > 0 && char.IsNumber(string1[0]) &&
                string2.Length > 0 && char.IsNumber(string2[0]))
            {
                output = LoopWithNumberFirst(string1, string2);
                if (output != 0)
                {
                    return output;
                }
            }
            else if (string1.Length > 0 && !char.IsNumber(string1[0]) &&
                string2.Length > 0 && !char.IsNumber(string2[0]))
            {
                output = LoopWithCharFirst(string1, string2);
                if (output != 0)
                {
                    return output;
                }
            }
            return String.Compare(string1, string2, StringComparison.Ordinal);


        }

        protected static Int32 GetColumnLength(String tableName, String columnName)
        {
            Int32 columnLength;

            if (IsNull(_columnLenghtTable))
            {
                // Create column length table.
                _columnLenghtTable = new Hashtable();
            }

            var hashKey = "Table:" + tableName + "Column:" + columnName;
            if (_columnLenghtTable.Contains(hashKey))
            {
                // Get cached value.
                columnLength = (Int32)(_columnLenghtTable[hashKey]);
            }
            else
            {
                // Get value from database.
                columnLength = Database.GetColumnLength(tableName, columnName);
                _columnLenghtTable.Add(hashKey, columnLength);
            }
            return columnLength;
        }


        private static Boolean HasPendingTransaction()
        {
            return Database.HasPendingTransaction();
        }

        public static Boolean IsValidId(Int32 id)
        {
            return id != NO_ID;
        }

        public static void Refresh()
        {
            UserManager.Refresh();
        }

        public static void RollbackTransaction()
        {
            if (Database.HasPendingTransaction())
            {
                Database.RollbackTransaction();
            }
        }

        private static void TransactionCommited()
        {
            foreach (EventHandler eventHandler in _eventHandlerTable.Values)
            {
                eventHandler.TransactionCommited();
            }
        }

        private static void TransactionRollbacked()
        {
            foreach (EventHandler eventHandler in _eventHandlerTable.Values)
            {
                eventHandler.TransactionRollbacked();
            }
        }

        private class EventFire
        {
            private readonly IDataIdentifier _data;
            private readonly Object[] _args;

            public EventFire(IDataIdentifier data, Object[] args)
            {
                _args = args;
                _data = data;
            }

            public Object[] GetArgs()
            {
                return _args;
            }

            public IDataIdentifier GetData()
            {
                return _data;
            }
        }

        private class EventSubscription
        {
            private readonly Delegate _dataDelegate;
            private readonly IDataIdentifier _data;

            public EventSubscription(IDataIdentifier data, Delegate dataDelegate)
            {
                _dataDelegate = dataDelegate;
                _data = data;
            }

            public IDataIdentifier GetData()
            {
                return _data;
            }

            public Delegate GetDelegate()
            {
                return _dataDelegate;
            }
        }


        private class EventHandler
        {
            private readonly Type _delegateType;
            private readonly ArrayList _eventSubscriptions;
            private readonly ArrayList _pendingEvents;

            public EventHandler(Type delegateType)
            {
                _delegateType = delegateType;
                _eventSubscriptions = new ArrayList();
                _pendingEvents = new ArrayList();
            }

            private void FireEvent(IDataIdentifier data, Object[] args)
            {
                if (HasPendingTransaction())
                {
                    _pendingEvents.Add(new EventFire(data, args));
                }
                else
                {
                    foreach (EventSubscription eventSubscription in (ArrayList)(_eventSubscriptions.Clone()))
                    {
                        // Compare if fired data object is same as
                        // subscribed data object.
                        // Try to compare with the IDataIdentity interface if possible.
                        // The IDataIdentity interface works independet
                        // from changes of the identifier.
                        if (((data is IDataIdentity) &&
                             (eventSubscription.GetData() is IDataIdentity) &&
                             (AreEqual((IDataIdentity)data, (IDataIdentity)(eventSubscription.GetData())))) ||
                            AreEqual(data, eventSubscription.GetData()))
                        {
                            // Notify subscriber of event.
                            eventSubscription.GetDelegate().DynamicInvoke(args);
                        }
                    }
                }
            }

            public void TransactionCommited()
            {
                // Fire pending events.
                foreach (EventFire eventFire in _pendingEvents)
                {
                    FireEvent(eventFire.GetData(), eventFire.GetArgs());
                }
                _pendingEvents.Clear();
            }

            public void TransactionRollbacked()
            {
                _pendingEvents.Clear();
            }
        }
    }
}
