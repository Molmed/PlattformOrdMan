using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data.PostData
{
    class PostManager : PlattformOrdManData
    {
        public PostManager()
            : base()
        { 
        }

        public static Post CreatePost(int articleNumberId, int bookerUserId, String comment, int merchandiseId, int supplierId,
                        int amount, decimal apprPrize, int currencyId, bool invoiceInst, bool invoiceClin, 
                        bool invoiceAbsent, string invoiceNumber, decimal finalPrize, string deliveryDeviation,
                        string purchaseOrderNo, string salesOrderNo, string purchaseAndsalesOrderNo, string placeOfPurchase,
                        Enquiry periodization, Enquiry account)
        {
            DataReader dataReader = null;
            Post post = null;
            try
            {
                dataReader = Database.CreatePost(articleNumberId, bookerUserId, comment, merchandiseId, supplierId, amount, 
                    apprPrize, currencyId, invoiceInst, invoiceClin, invoiceAbsent, invoiceNumber, finalPrize, 
                    deliveryDeviation, purchaseOrderNo, salesOrderNo, purchaseAndsalesOrderNo, placeOfPurchase, 
                    periodization.HasAnswered, periodization.HasValue, periodization.Value, 
                    account.HasAnswered, account.HasValue, account.Value);
                if (dataReader.Read())
                {
                    post = new Post(dataReader);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }
            OEventHandler.FirePostCreated(post);
            return post;
        }

        public static void DeletePost(int postId)
        {
            Database.DeletePost(postId);
        }

        public static void DeletePosts(PostList posts)
        {
            Database.BeginTransaction();
            try
            {
                foreach (Post post in posts)
                {
                    Database.DeletePost(post.GetId());
                }
                Database.CommitTransaction();
            }
            catch
            {
                Database.RollbackTransaction();
                throw;
            }            
        }

        public static Post GetPostById(int id)
        {
            DataReader dataReader = null;
            Post tmpPost = null;
            try
            {
                dataReader = Database.GetPostById(id);
                if (dataReader.Read())
                {
                    tmpPost = new Post(dataReader);
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return tmpPost;            
        }

        public static PostList GetPostsByCustomerNumberId(int customerNumberId)
        {
            DataReader dataReader = null;
            PostList posts = new PostList();
            try
            {
                dataReader = Database.GetPostsByCustomerNumberId(customerNumberId);
                while (dataReader.Read())
                {
                    posts.Add(new Post(dataReader));
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return posts;
        }

        public static PostList GetNotFinishedPosts()
        {
            DataReader dataReader = null;
            PostList posts;
            Post post;
            try
            {
                posts = new PostList();
                dataReader = Database.GetPosts(DateTime.Today, false, true);
                while (dataReader.Read())
                {
                    post = new Post(dataReader);
                    if (post.GetInvoiceStatus() == Post.InvoiceStatus.Incoming)
                    {
                        posts.Add(post);
                    }
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return posts;
        }

        public static PostList GetPosts(DateTime fromDate, bool timeRestrictionOn, bool timeRestrictionToCompletedPosts)
        {
            DataReader dataReader = null;
            PostList post;
            try
            {
                post = new PostList();
                dataReader = Database.GetPosts(fromDate, timeRestrictionOn, timeRestrictionToCompletedPosts);
                while (dataReader.Read())
                {
                    post.Add(new Post(dataReader));
                }

            }
            finally
            {
                CloseDataReader(dataReader);
            }
            return post;
        }


    }
}
