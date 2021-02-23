using System;
using System.Data;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class CustomerNumberManager : PlattformOrdManData
    {
        public CustomerNumberManager()
        { 
        }

        public static CustomerNumber CreateCustomerNumber(String identifier, String description, PlaceOfPurchase placeOfPurchase, int supplierId)
        {
            DataReader dataReader = null;
            CustomerNumber custNum = null; 

            // Check parameters.
            CheckNotEmpty(identifier, "identifier");
            CheckLength(identifier, "identifier", CustomerNumber.GetIdentifierMaxLength());
            CheckNotEmpty(identifier, "description");
            CheckLength(identifier, "description", CustomerNumber.GetDescriptionMaxLength());

            try
            {
                // Create customer number in database.
                dataReader = Database.CreateCustomerNumber(identifier, description, placeOfPurchase.ToString(), supplierId);
                if (dataReader.Read())
                {
                    custNum = new CustomerNumber(dataReader);
                }
                else
                {
                    throw new Data.Exception.DataException("Failed to create customer number" + identifier);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return custNum;
        }

        public static void DeleteCustomerNumber(int custNumbId)
        {
            Database.DeleteCustomerNumber(custNumbId);
        }

        public static CustomerNumberList GetCustomerNumbers(int supplierId)
        {
            DataReader reader = null;
            CustomerNumberList custNumList = new CustomerNumberList();
            try
            {
                reader = Database.GetCustomerNumberForSupplier(supplierId);
                while (reader.Read())
                {
                    custNumList.Add(new CustomerNumber(reader));
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return custNumList;
        }

        public static CustomerNumberList GetCustomerNumbersAll()
        {
            DataReader reader = null;
            CustomerNumberList custNumList = new CustomerNumberList();
            try
            {
                reader = Database.GetCustomerNumbersAll();
                while (reader.Read())
                {
                    custNumList.Add(new CustomerNumber(reader));
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return custNumList;
        }

        public static DataTable GetCustomerNumberTable()
        {
            DataTable table;
            DataColumn column;

            table = new DataTable(CustomerNumberData.TABLE);

            column = new DataColumn(CustomerNumberData.CUSTOMER_NUMBER_ID, typeof(int));
            column.AllowDBNull = false;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn(CustomerNumberData.IDENTIFIER, typeof(string));
            column.AllowDBNull = false;
            table.Columns.Add(column);

            column = new DataColumn(CustomerNumberData.DESCRIPTION, typeof(string));
            column.AllowDBNull = true;
            table.Columns.Add(column);

            column = new DataColumn(CustomerNumberData.SUPPLIER_ID, typeof(int));
            column.AllowDBNull = false;
            table.Columns.Add(column);

            column = new DataColumn(CustomerNumberData.PLACE_OF_PURCHASE, typeof(string));
            column.AllowDBNull = false;
            table.Columns.Add(column);

            column = new DataColumn(CustomerNumberData.ENABLED, typeof(bool));
            column.AllowDBNull = false;
            table.Columns.Add(column);

            return table;
        }

        private static void FillCustomerNumberDataTable(CustomerNumberList custNums, ref DataTable table)
        {
            DataRow row;
            foreach (CustomerNumber cust in custNums)
            {
                row = table.NewRow();
                row[CustomerNumberData.CUSTOMER_NUMBER_ID] = cust.GetIdentifier();
                row[CustomerNumberData.IDENTIFIER] = cust.GetIdentifier();
                row[CustomerNumberData.DESCRIPTION] = cust.GetDescription();
                row[CustomerNumberData.SUPPLIER_ID] = cust.GetSupplierId();
                row[CustomerNumberData.PLACE_OF_PURCHASE] = cust.GetPlaceOfPurchase().ToString();
                row[CustomerNumberData.ENABLED] = cust.IsEnabled();
                table.Rows.Add(row);
            }
        }

        private static void GetCustomerNumbersForSupplier(int supplierId, ref DataView dView)
        { 
            
        }

        public static CustomerNumber GetCustomerNumberById(int customerNumberId)
        {
            DataReader reader = null;
            CustomerNumber custNum = null;
            try
            {
                reader = Database.GetCustomerNumberById(customerNumberId);
                while (reader.Read())
                {
                    custNum = new CustomerNumber(reader);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return custNum;
        }

    }
}
