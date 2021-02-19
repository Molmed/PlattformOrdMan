using System;
using System.Windows.Forms;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    class InvoiceCategoryManager : PlattformOrdManData
    {
        public InvoiceCategoryManager()
            : base()
        { }

        public static InvoiceCategory CreateInvoiceCategory(String identifier, int code)
        {
            DataReader dataReader = null;
            InvoiceCategory invoiceCategory = null;
            try
            {
                dataReader = Database.CreateInvoiceCategory(identifier, code);
                if (dataReader.Read())
                {
                    invoiceCategory = new InvoiceCategory(dataReader);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return invoiceCategory;
        }

        public static void DeleteInvoiceCategory(Int32 InvoiceCategoryId)
        {
            Database.DeleteInvoiceCategory(InvoiceCategoryId);
        }

        public static bool DeleteInvoiceCategories(InvoiceCategoryList invoiceCategories)
        {
            string str;
            Database.BeginTransaction();
            try
            {
                foreach (InvoiceCategory invoiceCategory in invoiceCategories)
                {
                    Database.DeleteInvoiceCategory(invoiceCategory.GetId());
                }
                Database.CommitTransaction();
                return true;
            }
            catch
            {
                str = "One or more of the selected invoice categories are (probably) referenced by a product, deletion aborted!";
                Database.RollbackTransaction();
                MessageBox.Show(str, "Error delete invoice categories", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static InvoiceCategoryList GetInvoiceCategories()
        {
            DataReader dataReader = null;
            InvoiceCategoryList invoiceCategories;
            try
            {
                invoiceCategories = new InvoiceCategoryList();
                dataReader = Database.GetInvoiceCategories();
                while (dataReader.Read())
                {
                    invoiceCategories.Add(new InvoiceCategory(dataReader));
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return invoiceCategories;
        }

        public static InvoiceCategory GetInvoiceCategoryById(int invoiceCategoryId)
        {
            DataReader dataReader = null;
            InvoiceCategory invoiceCategory = null;
            try
            {
                dataReader = Database.GetInvoiceCategoryById(invoiceCategoryId);
                if (dataReader.Read())
                {
                    invoiceCategory = new InvoiceCategory(dataReader);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return invoiceCategory;
        }
    }
}
