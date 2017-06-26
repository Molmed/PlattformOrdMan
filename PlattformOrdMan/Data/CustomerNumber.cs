using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Molmed.PlattformOrdMan.Database;


namespace Molmed.PlattformOrdMan.Data
{
    public class CustomerNumber : DataIdentity
    {
        private string MyDescription;
        private PlaceOfPurchase MyPlaceOfPurchase;
        private int MySupplierId;
        private Supplier MySupplier;
        private bool MyIsEnabled;
        private bool MyIsUpdatedLocal;

        public CustomerNumber(DataReader reader)
            : base(reader)
        {
            string placeOfPurchase;
            MyDescription = reader.GetString(CustomerNumberData.DESCRIPTION);
            placeOfPurchase = reader.GetString(CustomerNumberData.PLACE_OF_PURCHASE);
            MySupplierId = reader.GetInt32(CustomerNumberData.SUPPLIER_ID);
            MyIsEnabled = reader.GetBoolean(CustomerNumberData.ENABLED);
            MySupplier = null;
            MyPlaceOfPurchase = (PlaceOfPurchase)Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase);
            MyIsUpdatedLocal = false;
        }

        public CustomerNumber(string identifier, string description, PlaceOfPurchase placeOfPurchase,
            Supplier supplier, bool isEnabled)
        { 
            // Used for maybe-to-be-created customer number
            SetTempId();
            UpdateIdentifier(identifier);
            MyPlaceOfPurchase = placeOfPurchase;
            MyDescription = description;
            MySupplier = supplier;
            if (IsNotNull(supplier))
            {
                MySupplierId = supplier.GetId();
            }
            else
            {
                MySupplierId = NO_ID;
            }
            MyIsEnabled = isEnabled;
            MyIsUpdatedLocal = false;
        }

        public CustomerNumber(int id, string identifier, string description, PlaceOfPurchase placeOfPurchase, int supplierId, bool enabled)
            : base(id, identifier)
        {
            MyDescription = description;
            MyPlaceOfPurchase = placeOfPurchase;
            MySupplierId = supplierId;
            MyIsEnabled = enabled;
            MyIsUpdatedLocal = false;
            MySupplier = null;
        }
        public bool IsEnabled()
        {
            return MyIsEnabled;
        }

        public bool IsAnyInfoFieldUpdated(CustomerNumber updatedCustNum)
        {
            if (updatedCustNum.GetId() != GetId())
            {
                throw new Data.Exception.DataException("Comparison of different customer numbers when it should be the same");
            }
            return updatedCustNum.GetId() == GetId() &&
                (updatedCustNum.GetIdentifier() != GetIdentifier() || updatedCustNum.GetDescription() != GetDescription() ||
                updatedCustNum.GetPlaceOfPurchase() != GetPlaceOfPurchase());
        }

        public string GetIsArchivedString()
        {
            if (!IsEnabled())
            {
                return "X";
            }
            else
            {
                return "";
            }
        }

        public CustomerNumber Clone()
        {
            CustomerNumber custNum = new CustomerNumber(GetId(), GetIdentifier(), GetDescription(), GetPlaceOfPurchase(), GetSupplierId(), IsEnabled());
            return custNum;
        
        }

        public string GetDescription()
        {
            return MyDescription;
        }

        public override DataType GetDataType()
        {
            return DataType.CustomerNumber;
        }

        public GroupCategory GetGroupCategory()
        {
            return GetGroupCategory(GetPlaceOfPurchase());
        }

        public PlaceOfPurchase GetPlaceOfPurchase()
        {
            return MyPlaceOfPurchase;
        }

        public bool IsUpdateLocal()
        {
            return MyIsUpdatedLocal;
        }

        public string GetStringForCombobox()
        {
            string str = "";
            str = GetDescription();
            if (IsNotEmpty( GetDescription()))
            {
                str += ", ";
            }
            str += GetIdentifier();
            return str;
        }

        public override int CompareTo(object obj)
        {
            CustomerNumber cust;
            if (obj is CustomerNumber)
            {
                cust = (CustomerNumber)obj;
                if (IsEnabled() && !cust.IsEnabled())
                {
                    return 0;
                }
                else if (!IsEnabled() && cust.IsEnabled())
                {
                    return 1;
                }
                else
                {
                    return CompareStringWithNumbers(GetStringForCombobox(), cust.GetStringForCombobox());
                }
            }
            else
            {
                return base.CompareTo(obj);
            }
        }

        public int GetSupplierId()
        {
            return MySupplierId;
        }

        public static int GetIdentifierMaxLength()
        {
            return GetColumnLength(CustomerNumberData.TABLE, CustomerNumberData.IDENTIFIER);            
        }

        public static int GetDescriptionMaxLength()
        {
            return GetColumnLength(CustomerNumberData.TABLE, CustomerNumberData.DESCRIPTION);
        }

        public Supplier GetSupplier()
        {
            if (MySupplier == null)
            {
                MySupplier = SupplierManager.GetSupplierById(MySupplierId);
            }
            return MySupplier;
        }

        public void SetEnabled(bool enabled)
        {
            Set(GetIdentifier(), GetDescription(), GetPlaceOfPurchase(), enabled);
        }

        public void Set(string identifier, string description, PlaceOfPurchase placeOfPurchase, bool isEnabled)
        {
            Database.UpdateCustomerNumber(GetId(), identifier, description, placeOfPurchase.ToString(), GetSupplierId(), isEnabled);
            MyIsEnabled = isEnabled;
            MyDescription = description;
            UpdateIdentifier(identifier);
            MyPlaceOfPurchase = placeOfPurchase;
        }

        public void SetLocal(string identifier, string description, PlaceOfPurchase placeOfPurchase, bool isEnabled)
        {
            // If new fields are added, these must be included here as well
            MyIsEnabled = isEnabled;
            MyDescription = description;
            UpdateIdentifier(identifier);
            MyPlaceOfPurchase = placeOfPurchase;
            MyIsUpdatedLocal = true;
        }

        public void Set()
        {
            Database.UpdateCustomerNumber(GetId(), GetIdentifier(), GetDescription(), GetPlaceOfPurchase().ToString(), GetSupplierId(), IsEnabled());
            MyIsUpdatedLocal = false;
        }

        public override string ToString()
        {
            return GetStringForCombobox();
        }

    }

    public class CustomerNumberList : DataIdentityList
    {
        public new CustomerNumber GetById(Int32 id)
        {
            return (CustomerNumber)(base.GetById(id));
        }

        public new CustomerNumber this[Int32 index]
        {
            get
            {
                return (CustomerNumber)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }
    }

}
