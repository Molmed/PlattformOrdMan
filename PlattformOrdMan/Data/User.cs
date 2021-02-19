using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class User : DataComment
    {
        public enum UserType
        {
            Administrator,
            Developer,
            User
        }

        private Boolean MyIsAccountActive;
        private String MyName;
        private UserType MyUserType;
        private PlaceOfPurchase MyPlaceOfPurchase;

        public User(DataReader dataReader)
            : base(dataReader)
        {
            String userTypeString;
            string placeOfPurchase;
            MyName = dataReader.GetString(UserData.NAME);
            MyIsAccountActive = dataReader.GetBoolean(UserData.ACCOUNT_STATUS);
            userTypeString = dataReader.GetString(UserData.USER_TYPE);
            MyUserType = (UserType)(Enum.Parse(typeof(UserType), userTypeString));
            if (!dataReader.IsDBNull(UserData.PLACE_OF_PURCHASE))
            {
                placeOfPurchase = dataReader.GetString(PlattformOrdMan.Database.PostData.PLACE_OF_PURCHASE);
                MyPlaceOfPurchase = (PlaceOfPurchase)(Enum.Parse(typeof(PlaceOfPurchase), placeOfPurchase));
            }

        }

        public GroupCategory GetGroupCategory()
        {
            return PlattformOrdManData.GetGroupCategory(GetPlaceOfPurchase());
        }

        public PlaceOfPurchase GetPlaceOfPurchase()
        {
            return MyPlaceOfPurchase;
        }

        public string GetPlaceOfPurchaseString()
        {
            return MyPlaceOfPurchase.ToString();
        }

        public string GetPlaceOfPurchaseStringForUser()
        {
            return PlattformOrdManData.GetPlaceOfPurchaseString(GetPlaceOfPurchase());
        }

        public override int CompareTo(object obj)
        {
            if (obj is User)
            {
                return MyName.CompareTo(((User)obj).GetName());
            }
            else
            {
                return base.CompareTo(obj);
            }
        }

        public static Int32 GetCommentMaxLength()
        {
            return GetColumnLength(UserData.TABLE, UserData.COMMENT);
        }

        public override DataType GetDataType()
        {
            return DataType.User;
        }

        public static Int32 GetIdentifierMaxLength()
        {
            return GetColumnLength(UserData.TABLE, UserData.IDENTIFIER);
        }

        public String GetName()
        {
            return MyName;
        }

        public static Int32 GetNameMaxLength()
        {
            return GetColumnLength(UserData.TABLE, UserData.NAME);
        }

        public bool IsDeveloper()
        {
            return GetUserType() == UserType.Developer;
        }

        public UserType GetUserType()
        {
            return MyUserType;
        }

        public Boolean HasAdministratorRights()
        {
            return (MyUserType == UserType.Administrator) ||
                  (MyUserType == UserType.Developer);
        }

        public Boolean IsAccountActive()
        {
            return MyIsAccountActive;
        }

        public string IsAccountActiveString()
        {
            if (IsAccountActive())
            {
                return "X";
            }
            else
            {
                return "";
            }
        }

        public void Set(String identifier, String name, UserType userType, Boolean active, 
            PlaceOfPurchase placeOfPurchase, String comment)
        {
            // Check parameters.
            if (identifier != GetIdentifier())
            {
                UserManager.CheckNotAldreadyDefined(identifier);
            }
            comment = TrimString(comment);
            CheckLength(comment, "comment", GetCommentMaxLength());

            // Update database.
            Database.UpdateUser(GetId(), identifier, name, userType.ToString(), active, placeOfPurchase.ToString(), comment);

            // Update this object.
            base.UpdateIdentifier(identifier);
            MyName = name;
            MyUserType = userType;
            MyIsAccountActive = active;
            MyPlaceOfPurchase = placeOfPurchase;
            base.SetComment(comment);
        }
    }

    public class UserList : DataIdentityList
    {
        public new User GetById(Int32 id)
        {
            return (User)(base.GetById(id));
        }

        public new User this[Int32 index]
        {
            get
            {
                return (User)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }

        public new User this[String identifier]
        {
            get
            {
                return (User)(base[identifier]);
            }
        }
    }
}