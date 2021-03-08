using System;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Component
{
    public partial class UserComboBox : SearchingCombobox
    {
        public UserComboBox()
        {
            InitializeComponent();
        }

        public User GetSelectedUser()
        {
            return ((UserViewItem)(this.SelectedItem)).GetUser();
        }

        public void Init(bool showNoSelectionString, String searchType)
        {
            UserList users;
            DataIdentityList identities = new DataIdentityList();
            users = UserManager.GetActiveUsers();
            foreach (User user in users)
            {
                identities.Add(new UserViewItem(user));
            }
            base.Init(identities, searchType, showNoSelectionString);
        }

        private class UserViewItem : DataIdentity
        {
            private User MyUser;
            public UserViewItem(User user)
                : base(user.GetId(), user.GetName())
            {
                MyUser = user;
            }

            public override DataType GetDataType()
            {
                return DataType.User;
            }

            public User GetUser()
            {
                return MyUser;
            }
        }

    }
}
