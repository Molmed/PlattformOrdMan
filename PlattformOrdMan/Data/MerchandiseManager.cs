using System;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    class MerchandiseManager : PlattformOrdManData
    {
        private static MerchandiseList MyMerchandiseCache;
        private static ArticleNumberList MyArticleNumberCache;
        public MerchandiseManager()
            : base()
        { 
        }

        public static ArticleNumber CreateArticleNumber(string identifier, bool active, int merchandiseid)
        {
            DataReader reader = null;
            ArticleNumber articleNumber = null;
            try
            {
                reader = Database.CreateArticleNumber(identifier, active, merchandiseid);
                if (reader.Read())
                {
                    articleNumber = new ArticleNumber(reader);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseDataReader(reader);
            }
            return articleNumber;
        }

        public static Merchandise CreateMerchandise(String identifier, int supplierId, String amount, decimal apprPrize, String storage,
                                    String comment, String articleNumber, String category, int invoiceCategoryId, int currencyId)
        {
            DataReader dataReader = null;
            Merchandise merchandise = null;
            Database.BeginTransaction();
            try
            {
                dataReader = Database.CreateMerchandise(identifier, supplierId, amount, apprPrize, storage,
                    comment, articleNumber, category, invoiceCategoryId, currencyId);
                if (dataReader.Read())
                {
                    merchandise = new Merchandise(dataReader);
                }
                CloseDataReader(dataReader);
                Database.CommitTransaction();
            }
            catch
            {
                CloseDataReader(dataReader);
                Database.RollbackTransaction();
                throw;
            }
            finally
            {
                RefreshCache();
            }
            if (IsNotNull(merchandise))
            {
                OEventHandler.FireMerchandiseCreate(merchandise);
            }
            return merchandise;
        }

        public static void DeleteMerchandise(MerchandiseList merchandiseList)
        {
            Database.BeginTransaction();
            try
            {
                foreach (Merchandise merchandise in merchandiseList)
                {
                    Database.DeleteMerchandise(merchandise.GetId());
                }
                Database.CommitTransaction();
            }
            catch
            {
                Database.RollbackTransaction();
                throw;
            }
            finally
            {
                RefreshCache();
            }
        }

        public static Merchandise GetMerchandiseById(int merchandiseId)
        {
            DataReader reader = null;
            Merchandise merchandise = null;
            try
            {
                reader = Database.GetMerchandiseById(merchandiseId);
                if (reader.Read())
                {
                    merchandise = new Merchandise(reader);
                }
            }
            finally
            {
                CloseDataReader(reader);
            }
            return merchandise;
        }

        public static MerchandiseList GetMerchandiseForSupplier(int supplierId)
        {
            MerchandiseList products = new MerchandiseList();
            if (IsNull(MyMerchandiseCache))
            {
                RefreshCache();
            }
            foreach (Merchandise merchandise in MyMerchandiseCache)
            {
                if (merchandise.GetSupplierId() == supplierId)
                {
                    products.Add(merchandise);
                }
            }
            return products;
        }

        public static MerchandiseList GetActiveMerchandiseOnly()
        {
            MerchandiseList merchandise = new MerchandiseList();

            foreach (Merchandise tempMerchandise in GetMerchandiseFromCache())
            {
                if (tempMerchandise.IsEnabled())
                {
                    merchandise.Add(tempMerchandise);
                }
            }
            return merchandise;
        }

        public static MerchandiseList GetMerchandiseFromCache()
        {
            if (MyMerchandiseCache == null)
            {
                RefreshCache();
            }
            return MyMerchandiseCache;
        }

        public static Merchandise GetMerchandiseFromCache(Int16 merchId)
        {
            if (MyMerchandiseCache != null)
            {
                return MyMerchandiseCache.GetById(merchId);
            }
            else
            {
                throw new DataException("Merchandise cache not initialized.");
            }
        }

        public static ArticleNumber GetArticleNumberById(int articleNumberId)
        {
            DataReader reader = null;
            ArticleNumber articleNumber = null;
            try
            {
                reader = Database.GetArticleNumberById(articleNumberId);
                if (reader.Read())
                {
                    articleNumber = new ArticleNumber(reader);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseDataReader(reader);
            }
            return articleNumber;
        }

        public static ArticleNumber GetArticleNumberFromCache(string identifier)
        {
            if (IsNull(MyArticleNumberCache))
            {
                RefreshCache();
            }

            foreach (ArticleNumber ar in MyArticleNumberCache)
            {
                if (ar.GetIdentifier() == identifier)
                {
                    return ar;
                }
            }
            return null;
        }

        public static ArticleNumberList GetArticleNumbersForMerchandise(int merchandiseId)
        {
            DataReader reader = null;
            ArticleNumberList articleNumbers = new ArticleNumberList();
            try
            {
                reader = Database.GetArticleNumberByMerchandiseId(merchandiseId);
                while (reader.Read())
                {
                    articleNumbers.Add(new ArticleNumber(reader));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseDataReader(reader);                
            }
            return articleNumbers;
        }

        public static void RefreshCache()
        {
            DataReader dataReader = null;
            try
            {
                MyMerchandiseCache = new MerchandiseList();
                // Get information from database.
                dataReader = Database.GetMerchandise();
                while (dataReader.Read())
                {
                    MyMerchandiseCache.Add(new Merchandise(dataReader));
                }
                CloseDataReader(dataReader);

                MyArticleNumberCache = new ArticleNumberList();

                dataReader = Database.GetArticleNumbers();
                while (dataReader.Read())
                {
                    MyArticleNumberCache.Add(new ArticleNumber(dataReader));
                }

                CloseDataReader(dataReader);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseDataReader(dataReader);
            }
        }

    }
}
