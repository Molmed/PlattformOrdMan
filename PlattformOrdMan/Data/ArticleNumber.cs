using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;


namespace Molmed.PlattformOrdMan.Data
{
    public class ArticleNumber : DataIdentity
    {
        private bool MyIsActive;
        private int MyMerchandiseId;
        private Merchandise MyMerchandise;

        public ArticleNumber(DataReader reader)
            : base(reader)
        {
            MyIsActive = reader.GetBoolean(ArticleNumberData.ACTIVE);
            MyMerchandiseId = reader.GetInt32(ArticleNumberData.MERCHANDISE_ID);
        }

        public bool IsActice()
        {
            return MyIsActive;
        }

        public int GetProductId()
        {
            return MyMerchandiseId;
        }

        public override DataType GetDataType()
        {
            return DataType.ArticleNumber;
        }

        public Merchandise GetProduct()
        {
            if (MyMerchandiseId != PlattformOrdManData.NO_ID && IsNull(MyMerchandise))
            {
                MyMerchandise = MerchandiseManager.GetMerchandiseById(MyMerchandiseId);
            }
            return MyMerchandise;
        }

        public void Set()
        {
            try
            {
                Database.BeginTransaction();
                Database.UpdateArticleNumber(GetId(), GetIdentifier(), IsActice(), GetProductId());
                Database.CommitTransaction();
            }
            catch
            {
                Database.RollbackTransaction();
                throw;
            }
            finally
            {
                //MerchandiseManager.RefreshCache();
            }
        }

        public void SetIsActive(bool active)
        {
            MyIsActive = active;
        }

    }

    public class ArticleNumberList : DataIdentityList
    {
        public new ArticleNumber GetById(int id)
        {
            return (ArticleNumber)base.GetById(id);
        }

        public new ArticleNumber this[int index]
        {
            get
            {
                return (ArticleNumber)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public new ArticleNumber this[string identifier]
        {
            get
            {
                return (ArticleNumber)(base[identifier]);
            }
        }
    }
}
