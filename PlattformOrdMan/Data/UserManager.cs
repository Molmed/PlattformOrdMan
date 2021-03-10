using System;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class UserManager : PlattformOrdManData
    {
        private static UserList MyUsers = null;
        private static User MyCurrentUser = null;

        private UserManager()
            : base()
        {
        }

        public static void CheckNotAldreadyDefined(String identifier)
        {
            if (GetUser(identifier) != null)
            {
                throw new DataAlreadyDefinedException(identifier);
            }
        }

        public static User CreateUser(String identifier, String name, User.UserType userType, Boolean active, 
            PlaceOfPurchase placeOfPurchase, String comment)
        {
            DataReader dataReader = null;
            User user = null;

            // Check parameters.
            CheckNotEmpty(identifier, "identifier");
            CheckLength(identifier, "identifier", User.GetIdentifierMaxLength());
            CheckNotEmpty(name, "name");
            CheckLength(name, "name", User.GetNameMaxLength());
            comment = TrimString(comment);
            CheckLength(comment, "comment", User.GetCommentMaxLength());
            CheckNotAldreadyDefined(identifier);

            try
            {
                // Create user in database.
                dataReader = Database.CreateUser(identifier, name, userType.ToString(), active,
                    placeOfPurchase.ToString(), comment);
                if (dataReader.Read())
                {
                    user = new User(dataReader);
                }
                else
                {
                    throw new DataException("Failed to create user " + name);
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }

            // Add the user to in-memory list.
            if (MyUsers != null)
            {
                MyUsers.Add(user);
            }

            return user;
        }

        private static User GetUserFromBarcode(string barcode)
        {
            DataReader reader = null;
            User user = null;
            try
            {
                reader = Database.GetUserFromBarcode(barcode);
                if (reader.Read())
                {
                    user = new User(reader);
                }
            }
            finally
            {
                CloseDataReader(reader);
            }
            return user;
        }

        public static bool IsUserBarcode(string barcode)
        {
            User user = null;

            user = GetUserFromBarcode(barcode);

            return IsNotNull(user);
        }

        public static void ReleaseAuthorityMapping()
        {
            Database.ReleaseAuthorityMapping();
        }

        public static void SetAuthorityMappingFromBarcode(string userBarcode)
        {
            DataReader reader = null;
            try
            {
                reader = Database.SetAuthorityMappingFromBarcode(userBarcode);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseDataReader(reader);
            }
        }

        public static void SetAuthorityMappingFromSysUser()
        {
            DataReader reader = null;
            try
            {
                reader = Database.SetAuthorityMappingFromSysUser();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseDataReader(reader);
            }
        }

        public static UserList GetActiveUsers()
        {
            UserList activeUsers;

            LoadUsers();
            activeUsers = new UserList();
            foreach (User user in MyUsers)
            {
                if (user.IsAccountActive())
                {
                    activeUsers.Add(user);
                }
            }
            return activeUsers;
        }

        public static User GetCurrentUser()
        {
            LoadUsers();
            return MyCurrentUser;
        }

        public static User GetUser(Int32 userId)
        {
            LoadUsers();
            return MyUsers.GetById(userId);
        }

        public static User GetUser(String identifier)
        {
            LoadUsers();
            return MyUsers[identifier];
        }

        public static UserList GetUsers()
        {
            LoadUsers();
            return MyUsers;
        }

        private static void LoadUsers()
        {
            DataReader dataReader = null;

            if (IsNull(MyUsers))
            {
                try
                {
                    // Get information about all users from database.
                    dataReader = Database.GetUsers();
                    MyUsers = new UserList();
                    while (dataReader.Read())
                    {
                        MyUsers.Add(new User(dataReader));
                    }
                    dataReader.Close();

                    // Get current user from database.
                    dataReader = Database.GetUserCurrent();
                    if (dataReader.Read())
                    {
                        MyCurrentUser = new User(dataReader);

                        // Return same user object as in MyUsers.
                        MyCurrentUser = MyUsers.GetById(MyCurrentUser.GetId());
                    }
                    else
                    {
                        throw new DataException("Could not retrieve current user from database!");
                    }
                }
                catch
                {
                    MyUsers = null;
                    MyCurrentUser = null;
                    throw;
                }
                finally
                {
                    CloseDataReader(dataReader);
                }
            }
        }

        public static new void Refresh()
        {
            MyUsers = null;
            MyCurrentUser = null;
            LoadUsers();
        }
    }
}
