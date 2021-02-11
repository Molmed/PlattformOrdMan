using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PlattformOrdMan.Properties;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.Repositories;

namespace Molmed.PlattformOrdMan.Database
{
    public delegate void TransactionCommitedEventHandler();
    public delegate void TransactionRollbackedEventHandler();

    public class Dataserver : PlattformOrdManBase
    {
        private Int32 MyCommandTimeout;
        private SqlConnection MyConnection;
        private String MyConnectionString;
        private SqlTransaction MyTransaction;
        private SqlCommand MyDataReaderCommand;

        public event TransactionCommitedEventHandler TransactionCommited;
        public event TransactionRollbackedEventHandler TransactionRollbacked;

        public Dataserver(String userName, String password)
            : this(userName, password, Settings.Default.DatabaseName)
        {
        }

        public Dataserver(String userName, String password, String database)
        {
            MyCommandTimeout = Settings.Default.DatabaseCommandTimeout;
            SetConnectionString(userName, password, database);
        }

        public Boolean AuthenticateApplication(String applicationName,
                                   String applicationVersion)
        {
            //Returns true if the application with name appName and version
            //appVersion is allowed to connect.
            String cmdText;

            cmdText = "SELECT COUNT(*) FROM application_version " +
                      "WHERE identifier = '" + applicationName + "' AND " +
                      "version = '" + applicationVersion + "'";
            return (this.ExecuteScalar(cmdText) > 0);
        }


        public Int32 CommandTimeout
        {
            get
            {
                return MyCommandTimeout;
            }
            set
            {
                MyCommandTimeout = value;
            }
        }

        private void AssertDatabaseConnection()
        {
            //If the database connection is broken, try to reconnect.
            if (MyConnection.State == ConnectionState.Closed || MyConnection.State == ConnectionState.Broken)
            {
                Connect();
            }
        }

        public void BeginTransaction()
        {
            AssertDatabaseConnection();

            if (IsNull(MyTransaction))
            {
                MyTransaction = MyConnection.BeginTransaction();
            }
            else
            {
                throw new Exception("Transaction already active.");
            }
        }

        public void CommitTransaction()
        {
            if (IsNotNull(MyTransaction))
            {
                MyTransaction.Commit();
                MyTransaction = null;

                if (IsNotNull(TransactionCommited))
                {
                    TransactionCommited();
                }
            }
            else
            {
                throw new Exception("Unable to commit inactive transaction.");
            }
        }

        public Int32 ConfirmPostArrival(int postId, int userId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_ConfirmPostArrival");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.ARRIVAL_SIGN, userId);
            return ExecuteCommand(commandBuilder);
        }

        public Boolean Connect()
        {
            //Opens the database connection.
            MyConnection = new SqlConnection(MyConnectionString);
            MyConnection.Open();

            return (MyConnection.State == ConnectionState.Open);
        }

        public DataReader CreateMerchandise(String identifier, int supplierId, String amount, decimal apprPrize, String storage,
                                    String comment, string articleNumber, String category, int invoiceCategoryId, int currencyId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateMerchandise");
            commandBuilder.AddParameter(DataIdentifierData.IDENTIFIER, identifier);
            if (supplierId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(MerchandiseData.SUPPLIER_ID, supplierId);
            }
            commandBuilder.AddParameter(MerchandiseData.AMOUNT, amount);
            commandBuilder.AddParameter(MerchandiseData.APPR_PRIZE, apprPrize);
            if (IsNotNull(storage))
            {
                commandBuilder.AddParameter(MerchandiseData.STORAGE, storage);
            }
            if (!IsEmpty(articleNumber))
            {
                commandBuilder.AddParameter(MerchandiseData.ARTICLE_NUMBER, articleNumber);
            }
            if (IsNotNull(comment))
            {
                commandBuilder.AddParameter(DataCommentData.COMMENT, comment);            
            }
            if (IsNotNull(category))
            {
                commandBuilder.AddParameter(MerchandiseData.CATEGORY, category);
            }
            commandBuilder.AddParameter(MerchandiseData.CURRENCY_ID, currencyId);

            if (invoiceCategoryId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(MerchandiseData.INVOICE_CATEGORY_ID, invoiceCategoryId);
            }
            return GetRow(commandBuilder);
        }

        public DataReader CreateArticleNumber(string identifier, bool active, int merchandiseId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateArticleNumber");
            if (IsNotNull(identifier))
            {
                commandBuilder.AddParameter(ArticleNumberData.IDENTIFIER, identifier);
            }
            if (merchandiseId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(ArticleNumberData.MERCHANDISE_ID, merchandiseId);
            }
            commandBuilder.AddParameter(ArticleNumberData.ACTIVE, active);
            return GetRow(commandBuilder);
        }

        public DataReader CreateCurrency(String description, String symbol, String currency_code)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateCurrency");
            if (IsNotNull(description))
            {
                commandBuilder.AddParameter(CurrencyData.IDENTIFIER, description);
            }
            if (IsNotNull(symbol))
            {
                commandBuilder.AddParameter(CurrencyData.SYMBOL, symbol);
            }
            if (IsNotNull(currency_code))
            {
                commandBuilder.AddParameter(CurrencyData.CURRENCY_CODE, currency_code);
            }
            return GetRow(commandBuilder);
        }

        public DataReader CreateCustomerNumber(string identifier, String description, string placeOfPurchase, int supplierId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateCustomerNumber");
            commandBuilder.AddParameter(CustomerNumberData.IDENTIFIER, identifier);
            if (IsNull(description))
            {
                description = "";
            }
            commandBuilder.AddParameter(CustomerNumberData.DESCRIPTION, description);
            commandBuilder.AddParameter(CustomerNumberData.PLACE_OF_PURCHASE, placeOfPurchase);
            commandBuilder.AddParameter(CustomerNumberData.SUPPLIER_ID, supplierId);
            return GetRow(commandBuilder);
        }

        public DataReader CreateInvoiceCategory(String identifier, int number)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateInvoiceCategory");
            commandBuilder.AddParameter(InvoiceCategoryData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(InvoiceCategoryData.NUMBER, number);
            return GetRow(commandBuilder);
        }

        public DataReader CreatePost(int articleNumberId, int bookerId, String comment, int merchandiseId, int supplierId, 
            int amount, decimal apprPrize, int currencyId, bool invoiceInst, bool invoiceClin, bool invoiceAbsent,
            string invoiceNumber, decimal finalPrize, string deliveryDeviation, string purchaseOrderNo, 
            string salesOrderNo, string placeOfPurchase, bool periodizationAnswered,
            bool hasPeriodization, string periodizationValue, bool accountAnswered, bool hasAccount, 
            string accountValue)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreatePost");
            if (PlattformOrdManData.IsValidId(articleNumberId))
            {
                commandBuilder.AddParameter(PostData.ARTICLE_NUMBER_ID, articleNumberId);
            }
            commandBuilder.AddParameter(PostData.PERIODIZATION_ANSWERED, periodizationAnswered);
            commandBuilder.AddParameter(PostData.HAS_PERIODIZATION, hasPeriodization);
            commandBuilder.AddParameter(PostData.PERIODIZATION, periodizationValue);
            commandBuilder.AddParameter(PostData.ACCOUNT_ANSWERED, accountAnswered);
            commandBuilder.AddParameter(PostData.HAS_ACCOUNT, hasAccount);
            commandBuilder.AddParameter(PostData.ACCOUNT, accountValue);
            commandBuilder.AddParameter(PostData.AUTHORITY_ID_BOOKER, bookerId);
            commandBuilder.AddParameter(PostData.COMMENT, comment);
            commandBuilder.AddParameter(PostData.MERCHANDISE_ID, merchandiseId);
            if (supplierId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.SUPPLIER_ID, supplierId);
            }
            commandBuilder.AddParameter(PostData.APPR_PRIZE, apprPrize);
            commandBuilder.AddParameter(PostData.AMOUNT, amount);
            commandBuilder.AddParameter(PostData.CURRENCY_ID, currencyId);
            commandBuilder.AddParameter(PostData.INVOICE_INST, invoiceInst);
            commandBuilder.AddParameter(PostData.INVOICE_CLIN, invoiceClin);
            commandBuilder.AddParameter(PostData.INVOICE_ABSENT, invoiceAbsent);
            commandBuilder.AddParameter(PostData.INVOICE_NUMBER, invoiceNumber);
            commandBuilder.AddParameter(PostData.FINAL_PRIZE, finalPrize);
            commandBuilder.AddParameter(PostData.PLACE_OF_PURCHASE, placeOfPurchase);
            if (IsNotEmpty(purchaseOrderNo))
            {
                commandBuilder.AddParameter(PostData.PURCHASE_ORDER_NO, purchaseOrderNo);
            }
            if (IsNotEmpty(salesOrderNo))
            {
                commandBuilder.AddParameter(PostData.SALES_ORDER_NO, salesOrderNo);
            }
            if (IsNotEmpty(deliveryDeviation))
            {
                commandBuilder.AddParameter(PostData.DELIVERY_DEVIATION, deliveryDeviation);
            }
            return GetRow(commandBuilder);
        }


        public DataReader CreateSupplier(String identifier, String shortName, String telNr, 
                                    String comment, String contractTerminate)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_CreateSupplier");
            commandBuilder.AddParameter(SupplierData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(SupplierData.SHORT_NAME, shortName);
            commandBuilder.AddParameter(SupplierData.TEL_NR, telNr);
            commandBuilder.AddParameter(DataCommentData.COMMENT, comment);
            commandBuilder.AddParameter(SupplierData.CONTRACT_TERMINATE, contractTerminate);
            return GetRow(commandBuilder);
        }

        public DataReader CreateUser(String identifier, String name, String userType,
            Boolean active, string placeOfPurchase, String comment)
        {
            SqlCommandBuilder commandBuilder;
            DataReader dataReader;

            commandBuilder = new SqlCommandBuilder("p_CreateUser");
            commandBuilder.AddParameter(UserData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(UserData.NAME, name);
            commandBuilder.AddParameter(UserData.USER_TYPE, userType);
            commandBuilder.AddParameter(UserData.ACCOUNT_STATUS, active);
            commandBuilder.AddParameter(UserData.COMMENT, comment);
            commandBuilder.AddParameter(UserData.PLACE_OF_PURCHASE, placeOfPurchase);
            dataReader = GetReader(commandBuilder, CommandBehavior.Default);
            return dataReader;
        }


        public Int32 DeleteMerchandise(Int32 merchandiseId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeleteMerchandise");
            commandBuilder.AddParameter(MerchandiseData.MERCHANDISE_ID, merchandiseId);

            return ExecuteCommand(commandBuilder);
        }

        public Int32 DeletePost(Int32 postId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeletePost");
            commandBuilder.AddParameter(PostData.POST_ID, postId);

            return ExecuteCommand(commandBuilder);
        }

        public int DeleteArticleNumber(int articleNumberId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_DeleteArticleNumber");
            commandBuilder.AddParameter(ArticleNumberData.ARTICLE_NUMBER_ID, articleNumberId);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 DeleteCurrency(Int32 currencyId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeleteCurrency");
            commandBuilder.AddParameter(CurrencyData.CURRENCY_ID, currencyId);

            return ExecuteCommand(commandBuilder);
        }

        public Int32 DeleteCustomerNumber(Int32 customerNumberId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeleteCustomerNumber");
            commandBuilder.AddParameter(CustomerNumberData.CUSTOMER_NUMBER_ID, customerNumberId);

            return ExecuteCommand(commandBuilder);
        }

        public Int32 DeleteInvoiceCategory(Int32 invoiceCategoryId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeleteInvoiceCategory");
            commandBuilder.AddParameter(InvoiceCategoryData.INVOICE_CATEGORY_ID, invoiceCategoryId);

            return ExecuteCommand(commandBuilder);
        }

        public Int32 DeleteSupplier(Int32 supplierId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_DeleteSupplier");
            commandBuilder.AddParameter(SupplierData.SUPPLIER_ID, supplierId);

            return ExecuteCommand(commandBuilder);
        }

        public void Disconnect()
        {
            //Closes the current database connection.
            if ((MyConnection.State == ConnectionState.Open) ||
                 (MyConnection.State == ConnectionState.Fetching))
            {
                MyConnection.Close();
            }
        }

        private Int32 ExecuteCommand(SqlCommandBuilder commandBuilder)
        {
            return GetCommand(commandBuilder).ExecuteNonQuery();
        }

        private Int32 ExecuteCommand(String sqlQuery)
        {
            return GetCommand(sqlQuery).ExecuteNonQuery();
        }

        public Int32 ExecuteScalar(SqlCommandBuilder commandBuilder)
        {
            return ExecuteScalar(commandBuilder.GetCommand());
        }

        public Int32 ExecuteScalar(String sqlQuery)
        {
            return Convert.ToInt32(GetCommand(sqlQuery).ExecuteScalar());
        }

        public DataReader GetArticleNumberById(int articleNumberId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetArticleNumberById");
            commandBuilder.AddParameter(ArticleNumberData.ARTICLE_NUMBER_ID, articleNumberId);
            return GetReader(commandBuilder);
        }

        public DataReader GetArticleNumberByMerchandiseId(int merchandiseId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetArticleNumberByMerchandiseId");
            commandBuilder.AddParameter(ArticleNumberData.MERCHANDISE_ID, merchandiseId);
            return GetReader(commandBuilder);
        }

        public Int32 GetColumnLength(String tableName, String columnName)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetColumnLength");
            commandBuilder.AddParameter("table", tableName);
            commandBuilder.AddParameter("column", columnName);

            return ExecuteScalar(commandBuilder);
        }

        public SqlCommand GetCommand(SqlCommandBuilder commandBuilder)
        {
            return GetCommand(commandBuilder.GetCommand());
        }

        public SqlCommand GetCommand(String sqlQuery)
        {
            SqlCommand command;

            AssertDatabaseConnection();

            if (IsNull(MyTransaction))
            {
                command = new SqlCommand(sqlQuery, MyConnection);
            }
            else
            {
                command = new SqlCommand(sqlQuery, MyConnection, MyTransaction);
            }
            command.CommandTimeout = CommandTimeout;
            return command;
        }

        private DataReader GetRow(SqlCommandBuilder commandBuilder)
        {
            return GetReader(commandBuilder, CommandBehavior.SingleRow |
                            CommandBehavior.SingleResult);
        }


        private DataReader GetReader(SqlCommandBuilder commandBuilder)
        {
            return GetReader(commandBuilder, CommandBehavior.SingleResult);
        }

        private DataReader GetReader(SqlCommandBuilder commandBuilder,
                          CommandBehavior commandBehavior)
        {
            MyDataReaderCommand = GetCommand(commandBuilder);

            return new DataReader(MyDataReaderCommand.ExecuteReader(commandBehavior), MyDataReaderCommand);
        }

        public DataReader GetArticleNumbers()
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetArticleNumbers");
            return GetReader(commandBuilder);
        }

        public DataReader GetMerchandise()
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetMerchandise");

            return GetReader(commandBuilder);
        }

        public DataReader GetMerchandiseById(int merchandiseId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetMerchandiseById");
            commandBuilder.AddParameter(MerchandiseData.MERCHANDISE_ID, merchandiseId);
            return GetRow(commandBuilder);
        }

        public DataReader GetSupplierById(int supplierId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetSupplierById");
            commandBuilder.AddParameter(SupplierData.SUPPLIER_ID, supplierId);
            return GetRow(commandBuilder);
        }

        public DataReader GetSuppliers()
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetSuppliers");

            return GetReader(commandBuilder);
        }

        public DataReader GetInvoiceCategoryById(int id)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetInvoiceCategoryById");
            commandBuilder.AddParameter(InvoiceCategoryData.INVOICE_CATEGORY_ID, id);
            return GetReader(commandBuilder);
        }

        public DataReader GetPostById(int id)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetPostById");
            commandBuilder.AddParameter(PostData.POST_ID, id);
            return GetReader(commandBuilder);
        }

        public DataReader GetPostsByCustomerNumberId(int customerNumberId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetPostsByCustomerNumberId");
            commandBuilder.AddParameter(PostData.CUSTOMER_NUMBER_ID, customerNumberId);
            return GetReader(commandBuilder);
        }

        public DataReader GetPosts(DateTime fromDate, bool timeRestrictionOn, bool timeRestrictionToCompletedPosts)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetPosts");
            if (timeRestrictionOn)
            {
                commandBuilder.AddParameter(PostData.BOOKER_FROM_DATE, fromDate);
            }
            commandBuilder.AddParameter(PostData.TIME_RESTRICTION_TO_COMPLETED_POSTS, timeRestrictionToCompletedPosts);
            return GetReader(commandBuilder);
        }

        public DataReader GetCurrencies()
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetCurrencies");

            return GetReader(commandBuilder);
        }

        public DataReader GetCustomerNumberForSupplier(int supplierId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetCustomerNumberBySupplierId");
            commandBuilder.AddParameter(CustomerNumberData.SUPPLIER_ID, supplierId);

            return GetReader(commandBuilder);
        }

        public DataReader GetCustomerNumberById(int customerNumberId)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetCustomerNumberById");
            commandBuilder.AddParameter(CustomerNumberData.CUSTOMER_NUMBER_ID, customerNumberId);

            return GetRow(commandBuilder);
        }

        public DataReader GetCustomerNumbersAll()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetCustomerNumbersAll");

            return GetReader(commandBuilder);
        }

        public DataReader GetInvoiceCategories()
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_GetInvoiceCategories");

            return GetReader(commandBuilder);
        }

        public DataReader GetTimeIntervalsForPosts()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetTimeIntervalsForPosts");

            return GetReader(commandBuilder);
        }

        public DataReader GetUserCurrent()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetUserCurrent");

            return GetRow(commandBuilder);
        }

        public DataReader GetUserFromBarcode(string barcode)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetUserFromBarcode");
            commandBuilder.AddParameter(LoginData.CHIASMA_BARCODE, barcode);

            return GetReader(commandBuilder);
        }


        public DataReader GetUsers()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_GetUsers");

            return GetReader(commandBuilder);
        }

        public Boolean HasPendingTransaction()
        {
            return (MyTransaction != null);
        }

        public Int32 OrderPost(int postId, int userId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_OrderPost");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.AUTHORITY_ID_ORDERER, userId);
            return ExecuteCommand(commandBuilder);            
        }

        public Int32 ConfirmPostOrdered(int postId, int userId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_ConfirmPostOrdered");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.AUTHORITY_ID_CONFIRMED_ORDER, userId);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 RegretArrivalConfirmation(int postId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_RegretArrivalConfirmation");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 RegretCompleted(int postId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_RegretCompleted");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            return ExecuteCommand(commandBuilder);
        }

        public int RegretOrderConfirmed(int postId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_RegretOrderConfirmed");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 RegretOrderPost(int postId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_RegretOrderPost");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            return ExecuteCommand(commandBuilder);
        }

        public int ReleaseAuthorityMapping()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_ReleaseAuthorityMapping");

            return ExecuteCommand(commandBuilder);
        }

        public int ResetArticleNumbersForMerchandise(int merchandiseId)
        {
            SqlCommandBuilder commandeBuilder;
            commandeBuilder = new SqlCommandBuilder("p_ResetArticleNumbersForMerchandise");
            commandeBuilder.AddParameter(ArticleNumberData.MERCHANDISE_ID, merchandiseId);
            return ExecuteCommand(commandeBuilder);
        }

        public Int32 ResetPostInvoiceStatus(int postId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_ResetPostInvoiceStatus");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            return ExecuteCommand(commandBuilder);
        }

        public void RollbackTransaction()
        {
            if (IsNotNull(MyTransaction))
            {
                try
                {
                    MyTransaction.Rollback();
                }
                catch
                {
                }
                MyTransaction = null;

                if (IsNotNull(TransactionRollbacked))
                {
                    TransactionRollbacked();
                }
            }
            else
            {
                throw new Exception("Unable to rollback inactive transaction.");
            }
        }

        public DataReader SetAuthorityMappingFromBarcode(string userBarcode)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_SetAuthorityMappingFromBarcode");
            commandBuilder.AddParameter(LoginData.CHIASMA_BARCODE, userBarcode);

            return GetRow(commandBuilder);
        }

        public DataReader SetAuthorityMappingFromSysUser()
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_SetAuthorityMappingFromSysUser");

            return GetRow(commandBuilder);
        }


        private void SetConnectionString(String userName, String password, String database)
        {
            if (IsEmpty(userName) || IsEmpty(password))
            {
                MyConnectionString = "data source=" + Settings.Default.DataServerAddress +
                                   ";integrated security=true;" +
                                   "initial catalog=" + database + ";";
            }
            else
            {
                MyConnectionString = "data source=" + Settings.Default.DataServerAddress +
                                   ";integrated security=false;" +
                                   "initial catalog=" + database + ";" +
                                   "user id=" + userName + ";" +
                                   "pwd=" + password + ";";
            }
        }

        public Int32 SignPostInvoice(Int32 postId, int invoiceSignerId, String invoiceStatus, bool invoiceAbsent)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_SignPostInvoice");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.AUTHORITY_ID_INVOICER, invoiceSignerId);
            commandBuilder.AddParameter(PostData.INVOICE_STATUS, invoiceStatus);
            commandBuilder.AddParameter(PostData.INVOICE_ABSENT, invoiceAbsent);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdatePostSetSalesOrderNo(Int32 postId, string salesOrderNo)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdatePostSalesOrderNumber");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.SALES_ORDER_NO, salesOrderNo);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateMerchandise(Int32 id, String identifier, String comment, int supplierId, String amount, decimal apprPrize,
                                    String storage, bool enabled, int invoiceCategoryId, int currencyId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateMerchandise");
            commandBuilder.AddParameter(MerchandiseData.MERCHANDISE_ID, id);
            commandBuilder.AddParameter(DataIdentifierData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(DataCommentData.COMMENT, comment);
            if (supplierId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(MerchandiseData.SUPPLIER_ID, supplierId);
            }
            commandBuilder.AddParameter(MerchandiseData.AMOUNT, amount);
            commandBuilder.AddParameter(MerchandiseData.APPR_PRIZE, apprPrize);
            commandBuilder.AddParameter(MerchandiseData.STORAGE, storage);
            commandBuilder.AddParameter(MerchandiseData.ENABLED, enabled);
            commandBuilder.AddParameter(MerchandiseData.CURRENCY_ID, currencyId);
            if (invoiceCategoryId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(MerchandiseData.INVOICE_CATEGORY_ID, invoiceCategoryId);
            }
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdatePost(int postId, String comment, decimal apprPrize, int amount, bool invoiceClin, bool invoiceInst,
            DateTime apprArrival, String invoiceStatus, bool isInvoiceAbsent, int currencyId, 
            int bookerUserId, DateTime bookDate, int orderUserId, DateTime orderDate, 
            int arrivalSignUserId, DateTime arrivalDate, int invoiceCheckerUserId, 
            DateTime invoiceDate, int articleNumberId, int supplierId, string invoiceNumber, decimal finalPrize, 
            DateTime confirmedOrderDate, int confirmedOrderUserId, string deliveryDeviation, 
            string purchaseOrderNo, string salesOrderNo, string placeOfPurchase, 
            bool attentionFlag, string periodizationValue, bool hasPeriodization, bool periodizationAnswered, 
            string accountValue, bool hasAccount, bool accountAnswered
            )
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdatePost");
            commandBuilder.AddParameter(PostData.POST_ID, postId);
            commandBuilder.AddParameter(PostData.COMMENT, comment);
            commandBuilder.AddParameter(PostData.APPR_PRIZE, apprPrize);
            commandBuilder.AddParameter(PostData.AMOUNT, amount);
            commandBuilder.AddParameter(PostData.INVOICE_CLIN, invoiceClin);
            commandBuilder.AddParameter(PostData.INVOICE_INST, invoiceInst);
            commandBuilder.AddParameter(PostData.INVOICE_STATUS, invoiceStatus);
            commandBuilder.AddParameter(PostData.INVOICE_ABSENT, isInvoiceAbsent);
            commandBuilder.AddParameter(PostData.CURRENCY_ID, currencyId);
            commandBuilder.AddParameter(PostData.AUTHORITY_ID_BOOKER, bookerUserId);
            commandBuilder.AddParameter(PostData.BOOK_DATE, bookDate);
            commandBuilder.AddParameter(PostData.FINAL_PRIZE, finalPrize);
            commandBuilder.AddParameter(PostData.INVOICE_NUMBER, invoiceNumber);
            commandBuilder.AddParameter(PostData.PLACE_OF_PURCHASE, placeOfPurchase);
            commandBuilder.AddParameter(PostData.PERIODIZATION, periodizationValue);
            commandBuilder.AddParameter(PostData.PERIODIZATION_ANSWERED, periodizationAnswered);
            commandBuilder.AddParameter(PostData.HAS_PERIODIZATION, hasPeriodization);
            commandBuilder.AddParameter(PostData.ACCOUNT, accountValue);
            commandBuilder.AddParameter(PostData.ACCOUNT_ANSWERED, accountAnswered);
            commandBuilder.AddParameter(PostData.HAS_ACCOUNT, hasAccount);
            if (IsNotEmpty(purchaseOrderNo))
            {
                commandBuilder.AddParameter(PostData.PURCHASE_ORDER_NO, purchaseOrderNo);
            }
            if (IsNotEmpty(salesOrderNo))
            {
                commandBuilder.AddParameter(PostData.SALES_ORDER_NO, salesOrderNo);
            }
            if (orderUserId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.AUTHORITY_ID_ORDERER, orderUserId);
                commandBuilder.AddParameter(PostData.ORDER_DATE, orderDate);
            }
            if (confirmedOrderUserId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.AUTHORITY_ID_CONFIRMED_ORDER, confirmedOrderUserId);
                commandBuilder.AddParameter(PostData.CONFIRMED_ORDER_DATE, confirmedOrderDate);
            }
            if (arrivalSignUserId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.ARRIVAL_SIGN, arrivalSignUserId);
                commandBuilder.AddParameter(PostData.ARRIVAL_DATE, arrivalDate);
            }
            if (invoiceCheckerUserId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.AUTHORITY_ID_INVOICER, invoiceCheckerUserId);
                commandBuilder.AddParameter(PostData.INVOICE_DATE, invoiceDate);
            }
            if (apprArrival.Ticks > 0)
            {
                commandBuilder.AddParameter(PostData.PREDICTED_ARRIVAL, apprArrival);
            }
            if (articleNumberId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(PostData.ARTICLE_NUMBER_ID, articleNumberId);
            }
            if (PlattformOrdManData.IsValidId(supplierId))
            {
                commandBuilder.AddParameter(PostData.SUPPLIER_ID, supplierId);
            }
            if (IsNotEmpty(deliveryDeviation))
            {
                commandBuilder.AddParameter(PostData.DELIVERY_DEVIATION, deliveryDeviation);
            }
            commandBuilder.AddParameter(PostData.ATTENTION_FLAG, attentionFlag);
            return ExecuteCommand(commandBuilder);
        }

        public int UpdateArticleNumber(int articleNumberId, string identifier, bool active, int merchandiseId)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateArticleNumber");
            commandBuilder.AddParameter(ArticleNumberData.ARTICLE_NUMBER_ID, articleNumberId);
            if (IsNotNull(identifier))
            {
                commandBuilder.AddParameter(ArticleNumberData.IDENTIFIER, identifier);
            }
            commandBuilder.AddParameter(ArticleNumberData.ACTIVE, active);

            if (merchandiseId != PlattformOrdManData.NO_ID)
            {
                commandBuilder.AddParameter(ArticleNumberData.MERCHANDISE_ID, merchandiseId);
            }
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateCurrency(Int32 currencyId, String description, String symbol, String currencyCode)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateCurrency");
            commandBuilder.AddParameter(CurrencyData.CURRENCY_ID, currencyId);
            if (IsNotNull(description))
            {
                commandBuilder.AddParameter(CurrencyData.IDENTIFIER, description);
            }
            if (IsNotNull(symbol))
            {
                commandBuilder.AddParameter(CurrencyData.SYMBOL, symbol);
            }
            if (IsNotNull(currencyCode))
            {
                commandBuilder.AddParameter(CurrencyData.CURRENCY_CODE, currencyCode);
            }
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateCustomerNumber(int customerNumberId, string identifier, String description, String placeOfPurchase, 
            int supplierId, bool enabled)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateCustomerNumber");
            commandBuilder.AddParameter(CustomerNumberData.CUSTOMER_NUMBER_ID, customerNumberId);
            commandBuilder.AddParameter(CustomerNumberData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(CustomerNumberData.DESCRIPTION, description);
            commandBuilder.AddParameter(CustomerNumberData.PLACE_OF_PURCHASE, placeOfPurchase);
            commandBuilder.AddParameter(CustomerNumberData.SUPPLIER_ID, supplierId);
            commandBuilder.AddParameter(CustomerNumberData.ENABLED, enabled);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateInvoiceCategory(int invoiceCategoryId, String identifier, int number)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateInvoiceCategory");
            commandBuilder.AddParameter(InvoiceCategoryData.INVOICE_CATEGORY_ID, invoiceCategoryId);
            commandBuilder.AddParameter(InvoiceCategoryData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(InvoiceCategoryData.NUMBER, number);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateSupplier(Int32 id, String identifier, String shortName, String telNr, 
                                    String comment, String contractTerminate, bool enabled)
        {
            SqlCommandBuilder commandBuilder;
            commandBuilder = new SqlCommandBuilder("p_UpdateSupplier");
            commandBuilder.AddParameter(SupplierData.SUPPLIER_ID, id);
            commandBuilder.AddParameter(SupplierData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(SupplierData.SHORT_NAME, shortName);
            commandBuilder.AddParameter(SupplierData.TEL_NR, telNr);
            commandBuilder.AddParameter(DataCommentData.COMMENT, comment);
            commandBuilder.AddParameter(SupplierData.CONTRACT_TERMINATE, contractTerminate);
            commandBuilder.AddParameter(SupplierData.ENABLED, enabled);
            return ExecuteCommand(commandBuilder);
        }

        public Int32 UpdateUser(Int32 userId, String identifier, String name, String userType,
            Boolean active, string placeOfPurchase, String comment)
        {
            SqlCommandBuilder commandBuilder;

            commandBuilder = new SqlCommandBuilder("p_UpdateUser");
            commandBuilder.AddParameter(UserData.USER_ID, userId);
            commandBuilder.AddParameter(UserData.IDENTIFIER, identifier);
            commandBuilder.AddParameter(UserData.NAME, name);
            commandBuilder.AddParameter(UserData.USER_TYPE, userType);
            commandBuilder.AddParameter(UserData.ACCOUNT_STATUS, active);
            commandBuilder.AddParameter(UserData.COMMENT, comment);
            commandBuilder.AddParameter(UserData.PLACE_OF_PURCHASE, placeOfPurchase);

            return ExecuteCommand(commandBuilder);
        }
    }
}
