using System;
using System.Data;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class SupplierManager : PlattformOrdManData
    {
        private static SupplierList MySupplierCache;
        private static string SUPPLIER_ID = "supplier_id";
        private static string CUST_NUM_ARRAY_INDEX = "cust_num_array_index";
        private SupplierManager()
            : base()
        { 
        }

        public static Supplier CreateSupplier(String identifier, String shortName, String telNr, String customerNrInst, String customerNrClin,
                            String comment, String contractTerminate)
        {
            return CreateSupplier(identifier, shortName, telNr, comment, contractTerminate, true);
        }

        public static Supplier CreateSupplier(String identifier, String shortName, String telNr, 
                                    String comment, String contractTerminate, bool fireCreate)
        {
            DataReader dataReader = null;
            Supplier supplier = null;
            try
            {
                dataReader = Database.CreateSupplier(identifier, shortName, telNr, comment, contractTerminate);
                if (dataReader.Read())
                {
                    supplier = new Supplier(dataReader);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            if (IsNotNull(supplier) && fireCreate)
            {
                OEventHandler.FireSupplierCreate(supplier);
            }
            return supplier;
        }

        public static void DeleteSupplier(Int32 supplierId)
        {
            Database.DeleteSupplier(supplierId);
        }

        public static void DeleteSuppliers(SupplierList suppliers)
        {
            Database.BeginTransaction();
            try
            {
                foreach (Supplier supplier in suppliers)
                {
                    Database.DeleteSupplier(supplier.GetId());
                }
                Database.CommitTransaction();
            }
            catch
            {
                Database.RollbackTransaction();
                throw;
            }
        }

        public static Supplier GetSupplierById(int supplierId)
        {
            DataReader dataReader = null;
            Supplier supplier = null;
            try
            {
                dataReader = Database.GetSupplierById(supplierId);
                if (dataReader.Read())
                {
                    supplier = new Supplier(dataReader);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return supplier;
        }

        public static SupplierList GetSuppliers()
        {
            // the code with dataview and initializing customer number from client is 
            // a test and may possibly be used elsewhere
            DataReader dataReader = null;
            SupplierList suppliers = null;
            CustomerNumberList allCustomerNumbers, localCustNumbs;
            DataView dView;

            Supplier supplier;
            try
            {
                allCustomerNumbers = CustomerNumberManager.GetCustomerNumbersAll();
                dView = GetSupplierCustomerNumberSyncView(allCustomerNumbers);
                suppliers = new SupplierList();
                dataReader = Database.GetSuppliers();
                while (dataReader.Read())
                {
                    supplier = new Supplier(dataReader);
                    suppliers.Add(supplier);
                    localCustNumbs = GetCustomerNumbersForSupplierInternal(supplier.GetId(),
                        dView, allCustomerNumbers);
                    supplier.SetCustomerNumbersLocal(localCustNumbs);
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return suppliers;
        }

        private static DataTable GetSupplierCustomerNumberSyncTable()
        {
            DataTable table;
            DataColumn column;

            table = new DataTable();

            column = new DataColumn("supplier_id", typeof(int));
            table.Columns.Add(column);

            column = new DataColumn("cust_num_array_index", typeof(int));
            table.Columns.Add(column);

            return table;
        }

        private static DataView GetSupplierCustomerNumberSyncView(CustomerNumberList allCustNumbs)
        {
            DataView dView;
            DataTable table;
            DataColumn column;
            DataRow row;
            int i = 0;

            table = new DataTable();

            column = new DataColumn(SUPPLIER_ID, typeof(int));
            table.Columns.Add(column);

            column = new DataColumn(CUST_NUM_ARRAY_INDEX, typeof(int));
            table.Columns.Add(column);

            foreach (CustomerNumber cust in allCustNumbs)
            {
                row = table.NewRow();
                row[SUPPLIER_ID] = cust.GetSupplierId();
                row[CUST_NUM_ARRAY_INDEX] = i;
                i++;
                table.Rows.Add(row);
            }

            dView = new DataView(table, "", SUPPLIER_ID, DataViewRowState.CurrentRows);

            return dView;
        }

        private static CustomerNumberList GetCustomerNumbersForSupplierInternal(int supplierId, DataView dView,
            CustomerNumberList allCustNums)
        {
            CustomerNumberList custNums;
            DataRowView[] foundRows;
            custNums = new CustomerNumberList();
            foundRows = dView.FindRows(supplierId);
            foreach (DataRowView rowView in foundRows)
            {
                custNums.Add(allCustNums[(int)rowView[CUST_NUM_ARRAY_INDEX]]);
            }

            return custNums;
        }

        public static SupplierList GetActiveSuppliersOnly()
        {
            DataReader dataReader = null;
            SupplierList suppliers = null;
            Supplier suplier = null;
            try
            {
                suppliers = new SupplierList();
                dataReader = Database.GetSuppliers();
                while (dataReader.Read())
                {
                    suplier = new Supplier(dataReader);
                    if (suplier.IsEnabled())
                    {
                        suppliers.Add(suplier);
                    }
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return suppliers;
        }


        public static SupplierList GetSuppliersFromCache()
        {
            return MySupplierCache;
        }

        public static void RefreshCache()
        {
            DataReader dataReader = null;

            try
            {
                MySupplierCache = new SupplierList();
                // Get information from database.
                dataReader = Database.GetSuppliers();
                while (dataReader.Read())
                {
                    MySupplierCache.Add(new Supplier(dataReader));
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
        }

    }
}
