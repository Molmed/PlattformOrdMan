using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class InvoiceCategory : DataIdentity
    {
        private int MyNumber;

        public InvoiceCategory()
            :base()
        {
            MyNumber = PlattformOrdManData.NO_COUNT;
        }

        public InvoiceCategory(DataReader dataReader)
            : base(dataReader)
        {
            MyNumber = dataReader.GetInt32(InvoiceCategoryData.NUMBER);
        }

        public void MakeNoSelectionIC()
        {
            UpdateIdentifier("No invoice category selected");
        }

        public int GetNumber()
        {
            return MyNumber;
        }

        public override DataType GetDataType()
        {
            return DataType.InvoiceCategory;
        }

        public void Set(String identifier, int number)
        {
            Database.UpdateInvoiceCategory(GetId(), identifier, number);
            MyNumber = number;
            UpdateIdentifier(identifier);
        }

        public void SetIdentifier(String identifier)
        {
            UpdateIdentifier(identifier);
        }

        public void SetCode(int code)
        {
            MyNumber = code;
        }

    }
    public class InvoiceCategoryList : DataIdentityList
    {
        public new InvoiceCategory GetById(Int32 id)
        {
            return (InvoiceCategory)(base.GetById(id));
        }

        public new InvoiceCategory this[Int32 index]
        {
            get
            {
                return (InvoiceCategory)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public new InvoiceCategory this[String identifier]
        {
            get
            {
                return (InvoiceCategory)(base[identifier]);
            }
        }

    }

}
