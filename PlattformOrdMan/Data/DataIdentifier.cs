using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.Data
{
    // Data type in the database.
    public enum DBKind
    {
        AUTHORITY,
        SUPPLIER,
        MERCHANDISE,
        ARTICLE_NUMBER
    }

    // Data type in the program.
    public enum DataType
    {
        CustomerNumber,
        User,
        Merchandise,
        Supplier,
        Post,
        InvoiceCategory,
        ArticleNumber
    }

    public interface IDataIdentifier
    {
        DataType GetDataType();
        String GetIdentifier();
    }

    public abstract class DataIdentifier : PlattformOrdManData, IDataIdentifier, IComparable
    {
        private String MyIdentifier;

        public DataIdentifier()
            : base()
        {
            MyIdentifier = "";
        }

        public DataIdentifier(string identifier)
            : base()
        {
            MyIdentifier = identifier;
        }

        public DataIdentifier(DataReader dataReader)
            : base()
        {
            MyIdentifier = dataReader.GetString(DataIdentifierData.IDENTIFIER);
        }

        public virtual int CompareTo(object obj)
        {
            if (obj is DataIdentifier)
            {
                DataIdentifier dataIdentifier = (DataIdentifier)obj;

                if (GetDataType() != dataIdentifier.GetDataType())
                {
                    return GetDataType().ToString().CompareTo(dataIdentifier.GetDataType().ToString());
                }
                else
                {
                    return PlattformOrdManData.CompareStringWithNumbers(MyIdentifier, dataIdentifier.GetIdentifier());
                }
            }

            throw new ArgumentException("Object is not a DataIdentifier");
        }

        public abstract DataType GetDataType();

        public String GetIdentifier()
        {
            return MyIdentifier;
        }

        protected void UpdateIdentifier(String identifier)
        {
            MyIdentifier = identifier;
        }

        public override string ToString()
        {
            return MyIdentifier;
        }
    }

    public class DataIdentifierList : ArrayList
    {
        protected const int NO_INDEX = -1;
        public override int Add(object value)
        {
            if (value != null)
            {
                return base.Add(value);
            }
            return -1;
        }

        public override void AddRange(ICollection collection)
        {
            if (collection != null)
            {
                base.AddRange(collection);
            }
        }

        public Int32 GetIndex(String identifier)
        {
            Int32 index;

            // Check parameter.
            if (identifier == null)
            {
                return -1;
            }

            // This search is case insensitive.
            identifier = identifier.ToLower();
            for (index = 0; index < this.Count; index++)
            {
                if (this[index].GetIdentifier().ToLower() == identifier)
                {
                    return index;
                }
            }
            return -1;
        }

        public new IDataIdentifier this[Int32 index]
        {
            get
            {
                return (IDataIdentifier)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public IDataIdentifier this[String identifier]
        {
            get
            {
                // Check parameter.
                if (identifier == null)
                {
                    return null;
                }

                // This search is case insensitive.
                identifier = identifier.ToLower();
                foreach (IDataIdentifier dataIdentifier in this)
                {
                    if (dataIdentifier.GetIdentifier().ToLower() == identifier)
                    {
                        return dataIdentifier;
                    }
                }
                return null;
            }
        }
    }
}
