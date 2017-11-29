using System;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.Data
{
    public class Supplier : DataComment 
    {
        private String _telNr;
        private String _contractTerminate;
        private String _shortName;
        private bool _enabled;
        private CustomerNumberList _customerNumbers;

        public Supplier(DataReader datareader)
            : base(datareader)
        {
            _telNr = datareader.GetString(SupplierData.TEL_NR, "");
            _contractTerminate = datareader.GetString(SupplierData.CONTRACT_TERMINATE, "");
            _shortName = datareader.GetString(SupplierData.SHORT_NAME, "");
            _enabled = datareader.GetBoolean(SupplierData.ENABLED);
            _customerNumbers = null;
        }

        public CustomerNumberList GetCustomerNumbers()
        {
            return _customerNumbers ?? (_customerNumbers = CustomerNumberManager.GetCustomerNumbers(GetId()));
        }

        public void ResetCustomerNumberLocal()
        {
            _customerNumbers = null;
        }

        public void AddCustomerNumberLocal(CustomerNumber custNumber)
        {
            GetCustomerNumbers().Add(custNumber);
        }

        public void RemoveCustomerNumberLocal(CustomerNumber custNumber)
        {
            GetCustomerNumbers().Remove(custNumber);
        }

        public CustomerNumberList GetCustomerNumbersForCurrentUserGroup()
        {
            CustomerNumberList custNumbers = new CustomerNumberList();
            var currentGroup = GetGroupCategory(UserManager.GetCurrentUser().GetPlaceOfPurchase());
            foreach (CustomerNumber custNum in GetCustomerNumbers())
            {
                var group = GetGroupCategory(custNum.GetPlaceOfPurchase());
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
            var currentGroup = GetGroupCategory(placeOfPurchase);
            foreach (CustomerNumber custNum in GetCustomerNumbers())
            {
                var group = GetGroupCategory(custNum.GetPlaceOfPurchase());
                if (currentGroup == group)
                {
                    custNumbers.Add(custNum);
                }
            }
            return custNumbers;
        }

        public String GetTelNr()
        {
            return _telNr;
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
            return _contractTerminate;
        }

        public String GetShortName()
        {
            return _shortName;
        }

        public String GetEnabledString()
        {
            if (_enabled)
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
            return _enabled;
        }

        public override DataType GetDataType()
        {
            return DataType.Supplier;
        }

        public void Set()
        {
            Database.UpdateSupplier(GetId(), GetIdentifier(), GetShortName(), GetTelNr(), 
                                        GetComment(), GetContractTerminate(), IsEnabled());
            OEventHandler.FireSupplierUpdate(this);
        }

        public void SetIdentifier(String identifier)
        {
            UpdateIdentifier(identifier);  
        }

        public void SetTelNr(String telNr)
        {
            _telNr = telNr;
        }

        public void SetCustomerNumbersLocal(CustomerNumberList custNumbs)
        {
            _customerNumbers = custNumbs;
        }

        public void SetContractTermination(String contractTermination)
        {
            _contractTerminate = contractTermination;
        }

        public void SetShortName(String shortName)
        {
            _shortName = shortName;
        }

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
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
            set
            {
                base[index] = value;
            }
        }
    }
}
