using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Molmed.PlattformOrdMan.Database;
using Molmed.PlattformOrdMan.Data.Exception;

namespace Molmed.PlattformOrdMan.Data
{
    public delegate void DataDeletedEventHandler(IDataIdentifier data);
    public delegate void DataUpdatedEventHandler(IDataIdentifier data);

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
        PurchaseOrderNo,
        SalesOrderNo,
        Comment,
        PlaceOfPurchase,
        CustomerNumber
    }

    public interface ICustomerNumberTemplate
    {
        string GetIdentifier();
        string GetDescription();
        PlaceOfPurchase GetPlaceOfPurchase();
        GroupCategory GetGroupCategory();
        void SetIdentifier(string identifier);
        void SetDescription(string description);
        void SetIsEnabled(bool enabled);
        void SetPlaceOfPurchase(PlaceOfPurchase placeOfPurchase);
    }

    public class PlattformOrdManData : PlattformOrdManBase
    {
        public const Int32 NO_COUNT = -1;
        public const Int32 NO_ID = -1;
        public const Byte NO_ID_TINY = 0;
        public const int LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH = -2;
        public const int LIST_VIEW_COLUMN_HEADER_AUTO_WIDTH = -1;
        // Search for file winuser.h for constant values 
        // In different folders under C:\Program Files (x86)\
        public const Int32 SC_MAXIMIZE = 0xF030;
        public const Int32 WM_SYSCOMMAND = 0x0112;
        public const Int32 SC_RESTORE = 0xF120;
        public const Int32 SC_MINIMIZE = 0XF020;

        private static Database.Dataserver MyDatabase = null;
        private static Hashtable MyColumnLenghtTable = null;
        private static Hashtable MyEventHandlerTable = null;
        private static OrdManEventHandler MyEventHandler = null;
        private static TransactionCommitedEventHandler MyTransactionCommitedEventHandler = null;
        private static TransactionRollbackedEventHandler MyTransactionRollbackedEventHandler = null;
        private static CultureInfo MyCulture = new CultureInfo("sv-SE");
        private static Configuration MyConfiguration;
        private static DateTime MyCustomerNumberReformDate = new DateTime(2013, 12, 1);
        private static int MyTempIdCounter;

        static PlattformOrdManData()
        {
            MyTempIdCounter = NO_ID;
        }

        public static int GetNewTempId()
        {
            MyTempIdCounter--;
            return MyTempIdCounter;
        }

        public static DateTime CustomerNumberReformDate
        {
            get
            {
                return MyCustomerNumberReformDate;
            }
        }

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
            throw new Data.Exception.DataException("String ''" + popStr + "'' does not match a valid defined PlaceOfPurchase enum");
        }

        public static CultureInfo MyCultureInfo
        {
            get
            {
                return MyCulture;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                return MyConfiguration;
            }
            set
            {
                MyConfiguration = value;
            }
        }

        public static OrdManEventHandler OEventHandler
        {
            get
            {
                return MyEventHandler;
            }
            set
            {
                MyEventHandler = value;
            }
        }

        public static Database.Dataserver Database
        {
            get { return MyDatabase; }
            set
            {
                if (IsNotNull(MyDatabase))
                {
                    MyDatabase.TransactionCommited -= MyTransactionCommitedEventHandler;
                    MyDatabase.TransactionRollbacked -= MyTransactionRollbackedEventHandler;
                }
                MyDatabase = value;
                if (IsNotNull(MyDatabase))
                {
                    MyTransactionCommitedEventHandler = new TransactionCommitedEventHandler(TransactionCommited);
                    MyDatabase.TransactionCommited += MyTransactionCommitedEventHandler;
                    MyTransactionRollbackedEventHandler = new TransactionRollbackedEventHandler(TransactionRollbacked);
                    MyDatabase.TransactionRollbacked += MyTransactionRollbackedEventHandler;
                    MyEventHandlerTable = new Hashtable();
                }
            }
        }

        public static decimal ParsePrice(String priceString)
        {
            int firstInd = -1, lastInd = -1;
            string decimalSymbol = PlattformOrdManData.MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            // Extract the numeric part of str
            NumberStyles style = NumberStyles.Float;
            priceString.Replace(".", decimalSymbol);
            priceString.Replace(",", decimalSymbol);
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
                PlattformOrdManData.MyCultureInfo, out price))
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
                    throw new Data.Exception.DataException("Unknwon place of purchase: " + placeOfPurchase.ToString());
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
                    throw new Data.Exception.DataException("Unknown group category: " + groupCategory.ToString());
            }
        }

        public static decimal ParsePrice(String priceString, out string currencyString)
        {
            int firstInd = -1, lastInd = -1;
            string decimalSymbol = PlattformOrdManData.MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            // Extract the numeric part of str, return as decimal
            // Extract the non-numeric part as currency string
            // Look for currency string at beginning and at end
            NumberStyles style = NumberStyles.Float;
            priceString.Replace(".", decimalSymbol);
            priceString.Replace(",", decimalSymbol);
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
                PlattformOrdManData.MyCultureInfo, out price))
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

        public static Boolean AreEqual(IDataIdentifier object1, IDataIdentifier object2)
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

        public static Boolean AreEqual(IDataIdentity object1, IDataIdentity object2)
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

        public static Boolean AreNotEqual(IDataIdentifier object1, IDataIdentifier object2)
        {
            return !AreEqual(object1, object2);
        }

        public static Boolean AreNotEqual(IDataIdentity object1, IDataIdentity object2)
        {
            return !AreEqual(object1, object2);
        }

        protected static void CheckNotEmpty(String value, String argumentName)
        {
            if (IsEmpty(value))
            {
                throw new DataArgumentEmptyException(argumentName);
            }
        }

        protected static void CheckNotNull(Object value, String argumentName)
        {
            if (IsNull(value))
            {
                throw new DataArgumentNullException(argumentName);
            }
        }

        protected static void CloseDataReader(DataReader dataReader)
        {
            if (dataReader != null)
            {
                dataReader.Close();
            }
        }

        private static Int32 LoopWithCharFirst(String string1, String string2)
        {
            // Belongs to CompareStringWithNumbers
            int i = 0, j = 0, startInd1, startInd2;
            String subString1, subString2, numberStr;
            int output;
            Int64 number1, number2;
            while (i < string1.Length && j < string2.Length)
            {
                startInd1 = i;
                startInd2 = j;
                //separate the string part
                while (++i < string1.Length && !char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && !char.IsNumber(string2[j]))
                { }
                subString1 = string1.Substring(startInd1, i - startInd1);
                subString2 = string2.Substring(startInd2, j - startInd2);
                output = subString1.CompareTo(subString2);
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
                numberStr = string1.Substring(startInd1, i - startInd1);
                number1 = Convert.ToInt64(numberStr);
                numberStr = string2.Substring(startInd2, j - startInd2);
                number2 = Convert.ToInt64(numberStr);
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
            int i = 0, j = 0, startInd1, startInd2;
            String subString1, subString2, numberStr;
            int output;
            Int64 number1, number2;
            while (i < string1.Length && j < string2.Length)
            {
                startInd1 = i;
                startInd2 = j;
                //handle the number part
                while (++i < string1.Length && char.IsNumber(string1[i]))
                { }
                while (++j < string2.Length && char.IsNumber(string2[j]))
                { }
                numberStr = string1.Substring(startInd1, i - startInd1);
                number1 = Convert.ToInt64(numberStr);
                numberStr = string2.Substring(startInd2, j - startInd2);
                number2 = Convert.ToInt64(numberStr);
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
                subString1 = string1.Substring(startInd1, i - startInd1);
                subString2 = string2.Substring(startInd2, j - startInd2);
                output = subString1.CompareTo(subString2);
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
            return string1.CompareTo(string2);


        }

        protected static Int32 GetColumnLength(String tableName, String columnName)
        {
            Int32 columnLength = -1;
            String hashKey;

            if (IsNull(MyColumnLenghtTable))
            {
                // Create column length table.
                MyColumnLenghtTable = new Hashtable();
            }

            hashKey = "Table:" + tableName + "Column:" + columnName;
            if (MyColumnLenghtTable.Contains(hashKey))
            {
                // Get cached value.
                columnLength = (Int32)(MyColumnLenghtTable[hashKey]);
            }
            else
            {
                // Get value from database.
                columnLength = Database.GetColumnLength(tableName, columnName);
                MyColumnLenghtTable.Add(hashKey, columnLength);
            }
            return columnLength;
        }

        protected static Int32 GetId(IDataIdentity dataIdentity)
        {
            if (IsNull(dataIdentity))
            {
                return NO_ID;
            }
            else
            {
                return dataIdentity.GetId();
            }
        }


        public static Boolean HasPendingTransaction()
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
            foreach (EventHandler eventHandler in MyEventHandlerTable.Values)
            {
                eventHandler.TransactionCommited();
            }
        }

        private static void TransactionRollbacked()
        {
            foreach (EventHandler eventHandler in MyEventHandlerTable.Values)
            {
                eventHandler.TransactionRollbacked();
            }
        }

        private class EventFire
        {
            private IDataIdentifier MyData;
            private Object[] MyArgs;

            public EventFire(IDataIdentifier data, Object[] args)
            {
                MyArgs = args;
                MyData = data;
            }

            public Object[] GetArgs()
            {
                return MyArgs;
            }

            public IDataIdentifier GetData()
            {
                return MyData;
            }
        }

        private class EventSubscription
        {
            private Delegate MyDataDelegate;
            private IDataIdentifier MyData;

            public EventSubscription(IDataIdentifier data, Delegate dataDelegate)
            {
                MyDataDelegate = dataDelegate;
                MyData = data;
            }

            public IDataIdentifier GetData()
            {
                return MyData;
            }

            public Delegate GetDelegate()
            {
                return MyDataDelegate;
            }
        }


        private class EventHandler
        {
            private Type MyDelegateType;
            private ArrayList MyEventSubscriptions;
            private ArrayList MyPendingEvents;

            public EventHandler(Type delegateType)
            {
                MyDelegateType = delegateType;
                MyEventSubscriptions = new ArrayList();
                MyPendingEvents = new ArrayList();
            }

            public void AddEventSubscription(IDataIdentifier data, Delegate dataDelegate)
            {
                MyEventSubscriptions.Add(new EventSubscription(data, dataDelegate));
            }

            public void FireEvent(IDataIdentifier data, Object[] args)
            {
                if (HasPendingTransaction())
                {
                    MyPendingEvents.Add(new EventFire(data, args));
                }
                else
                {
                    foreach (EventSubscription eventSubscription in (ArrayList)(MyEventSubscriptions.Clone()))
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

            public Type GetDelegateType()
            {
                return MyDelegateType;
            }

            public void RemoveEventSubscription(IDataIdentifier data, Delegate dataDelegate)
            {
                foreach (EventSubscription eventSubscription in MyEventSubscriptions)
                {
                    if (eventSubscription.GetDelegate() == dataDelegate)
                    {
                        MyEventSubscriptions.Remove(eventSubscription);
                        break;
                    }
                }
            }

            public void TransactionCommited()
            {
                // Fire pending events.
                foreach (EventFire eventFire in MyPendingEvents)
                {
                    FireEvent(eventFire.GetData(), eventFire.GetArgs());
                }
                MyPendingEvents.Clear();
            }

            public void TransactionRollbacked()
            {
                MyPendingEvents.Clear();
            }
        }
    }

    public abstract class UpdateField
    {
        String MyName;

        public UpdateField(String name)
        {
            MyName = name;
        }

        public String GetName()
        {
            return MyName;
        }

        public abstract void SetSize(Int32 size);
        public abstract Int32 GetSize();
    }
}
