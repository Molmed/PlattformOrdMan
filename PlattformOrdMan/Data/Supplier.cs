using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.Data
{
    public class Supplier : DataComment 
    {
        private String MyTelNr;
        private String MyContractTerminate;
        private String MyShortName;
        private bool MyEnabled;
        private MerchandiseList MyProducts;
        private CustomerNumberList MyCustomerNumbers;

        public Supplier(DataReader datareader)
            : base(datareader)
        {
            MyTelNr = datareader.GetString(SupplierData.TEL_NR, "");
            MyContractTerminate = datareader.GetString(SupplierData.CONTRACT_TERMINATE, "");
            MyShortName = datareader.GetString(SupplierData.SHORT_NAME, "");
            MyEnabled = datareader.GetBoolean(SupplierData.ENABLED);
            MyProducts = null;
            MyCustomerNumbers = null;
        }

        public Supplier(int id, string identifier, string comment, string telnr, 
            string contractTerminate, string shortName, bool isEnalbed, 
            MerchandiseList products, CustomerNumberList customerNumbers)
            : base(comment, identifier, id)
        {
            MyTelNr = telnr;
            MyContractTerminate = contractTerminate;
            MyShortName = shortName;
            MyEnabled = isEnalbed;
            MyProducts = products;
            MyCustomerNumbers = customerNumbers;
        }

        public CustomerNumberList GetCustomerNumbers()
        {
            if (MyCustomerNumbers == null)
            {
                MyCustomerNumbers = CustomerNumberManager.GetCustomerNumbers(GetId());
            }
            return MyCustomerNumbers;
        }

        public void ResetCustomerNumberLocal()
        {
            MyCustomerNumbers = null;
        }

        public void AddCustomerNumberLocal(CustomerNumber custNumber)
        {
            GetCustomerNumbers().Add(custNumber);
        }

        public Supplier Clone()
        {
            MerchandiseList products = null;
            CustomerNumberList custNums = null;

            if (MyProducts != null)
            {
                products = new MerchandiseList();
                foreach (Merchandise merch in MyProducts)
                {
                    products.Add(merch.Clone());
                }
            }

            if (MyCustomerNumbers != null)
            {
                custNums = new CustomerNumberList();
                foreach (CustomerNumber custNum in MyCustomerNumbers)
                {
                    custNums.Add(custNum.Clone());
                }
            }

            Supplier supplier = new Supplier(GetId(), GetIdentifier(),
                GetComment(), GetTelNr(), GetContractTerminate(), 
                GetShortName(), IsEnabled(), products, custNums);

            return supplier;
        }

        public void RemoveCustomerNumberLocal(CustomerNumber custNumber)
        {
            GetCustomerNumbers().Remove(custNumber);
        }

        public CustomerNumberList GetCustomerNumbersForCurrentUserGroup()
        {
            CustomerNumberList custNumbers = new CustomerNumberList();
            GroupCategory currentGroup, group;
            currentGroup = GetGroupCategory(UserManager.GetCurrentUser().GetPlaceOfPurchase());
            foreach (CustomerNumber custNum in GetCustomerNumbers())
            {
                group = GetGroupCategory(custNum.GetPlaceOfPurchase());
                if (currentGroup == group)
                {
                    custNumbers.Add(custNum);
                }
            }
            return custNumbers;
        }

        public CustomerNumberList GetCustomerNumbersForUserGroup(PlaceOfPurchase placeOfPurchase)
        {
            CustomerNumberList custNumbers = new CustomerNumberList();
            GroupCategory currentGroup, group;
            currentGroup = GetGroupCategory(placeOfPurchase);
            foreach (CustomerNumber custNum in GetCustomerNumbers())
            {
                group = GetGroupCategory(custNum.GetPlaceOfPurchase());
                if (currentGroup == group)
                {
                    custNumbers.Add(custNum);
                }
            }
            return custNumbers;
        }

        public MerchandiseList GetProducts()
        {
            if (IsNull(MyProducts))
            {
                MyProducts = MerchandiseManager.GetMerchandiseForSupplier(GetId());
            }
            return MyProducts;
        }

        public String GetTelNr()
        {
            return MyTelNr;
        }

        public bool HasMatchInCustomerNumber(string searchStr)
        {
            foreach (CustomerNumber cust in GetCustomerNumbers())
            {
                if (cust.GetIdentifier().Contains(searchStr) || cust.GetDescription().Contains(searchStr))
                {
                    return true;
                }
            }
            return false;
        }

        public String GetContractTerminate()
        {
            return MyContractTerminate;
        }

        public String GetShortName()
        {
            return MyShortName;
        }

        public String GetEnabledString()
        {
            if (MyEnabled)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public bool IsEnabled()
        {
            return MyEnabled;
        }

        public override DataType GetDataType()
        {
            return DataType.Supplier;
        }

        public void Set()
        {
            Database.UpdateSupplier(GetId(), GetIdentifier(), GetShortName(), GetTelNr(), 
                                        GetComment(), GetContractTerminate(), IsEnabled());
            PlattformOrdManData.OEventHandler.FireSupplierUpdate(this);
        }

        public void SetIdentifier(String identifier)
        {
            UpdateIdentifier(identifier);  
        }

        public void SetTelNr(String telNr)
        {
            MyTelNr = telNr;
        }

        public void SetCustomerNumbersLocal(CustomerNumberList custNumbs)
        {
            MyCustomerNumbers = custNumbs;
        }

        public void SetContractTermination(String contractTermination)
        {
            MyContractTerminate = contractTermination;
        }

        public void SetShortName(String shortName)
        {
            MyShortName = shortName;
        }

        public void SetEnabled(bool enabled)
        {
            MyEnabled = enabled;
        }
    }

    public class SupplierList : DataIdentityList
    {
        public new Supplier GetById(Int32 id)
        {
            return (Supplier)(base.GetById(id));
        }

        public new Supplier this[Int32 index]
        {
            get
            {
                return (Supplier)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public new Supplier this[String identifier]
        {
            get
            {
                return (Supplier)(base[identifier]);
            }
        }
    
    }
}
