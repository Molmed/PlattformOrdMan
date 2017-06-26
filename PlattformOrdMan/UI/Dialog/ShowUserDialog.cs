using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class ShowUserDialog : OrdManForm
    {
        private const String PROPERTIES = "Properties";
        private const String DELETE = "Delete";
        private const String ENTER_FILTER_TEXT = "Enter filter text ...";

        public ShowUserDialog()
        {
            InitializeComponent();
            InitListView();
            UsersListView.DoubleClick += new EventHandler(PropertiesMenuItem_Click);
            FilterTextBox.Text = ENTER_FILTER_TEXT;
            FilterTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.FilterTextBox.TextChanged += new System.EventHandler(this.FilterTextBox_TextChanged);
            this.FilterTextBox.Enter += FilterTextBox_Enter;
            
        }

        private void FilterTextBox_Enter(object sender, EventArgs e)
        {
            if (FilterTextBox.Text == ENTER_FILTER_TEXT)
            {
                FilterTextBox.Text = "";
                FilterTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            }

        }

        private void InitListView()
        {
            int width;
            width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            UsersListView.Columns.Add("User", width);
            UsersListView.Columns.Add("User type", width);
            UsersListView.Columns.Add("Enabled", width);
            UsersListView.Columns.Add("Default Group", width);
            UsersListView.Columns.Add("Comment", width);
            UpdateListView();
            AddMenuItem(UsersListView, DELETE, DeleteMenuItem_Click);
            AddMenuItem(UsersListView, PROPERTIES, PropertiesMenuItem_Click);
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        { 
            
        }

        private User GetSelectedUser()
        {
            if (UsersListView.SelectedItems.Count > 0)
            {
                return ((UserViewItem)UsersListView.SelectedItems[0]).GetUser();
            }
            else
            {
                return null;
            }
        }


        private void PropertiesMenuItem_Click(object sender, EventArgs e)
        {
            EditUserDialog editUserDialog;
            editUserDialog = new EditUserDialog(GetSelectedUser(), UpdateMode.Edit);
            if (editUserDialog.ShowDialog() == DialogResult.OK)
            {
                RefreshListView();
            }
        }

        private bool IsWithinSearchCriteria(User user)
        {
            if (FilterTextBox.Text.Trim() != "" &&
                !user.GetName().ToLower().Contains(FilterTextBox.Text.Trim().ToLower()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateListView()
        {
            UsersListView.Items.Clear();
            UsersListView.BeginUpdate();
            foreach (User user in UserManager.GetUsers())
            {
                if (IsWithinSearchCriteria(user))
                {
                    UsersListView.Items.Add(new UserViewItem(user));
                }
            }
            UsersListView.EndUpdate();        
        }

        private void RefreshListView()
        {
            foreach (UserViewItem uViewItem in UsersListView.Items)
            {
                uViewItem.Update();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private class UserViewItem : ListViewItem
        {
            private enum ListIndex : int
            { 
                name = 0,
                UserType = 1,
                Enabled = 2,
                PlaceOfPurchase = 3,
                Comment = 4
            }
            private User MyUser;
            public UserViewItem(User user)
                : base(user.GetName())
            {
                this.SubItems.Add(user.GetUserType().ToString());
                this.SubItems.Add(user.IsAccountActiveString());
                this.SubItems.Add(user.GetPlaceOfPurchaseString());
                this.SubItems.Add(user.GetComment());
                MyUser = user;
            }

            public User GetUser()
            {
                return MyUser;
            }

            public void Update()
            { 
                this.SubItems[(int)ListIndex.name].Text = MyUser.GetName();
                this.SubItems[(int)ListIndex.UserType].Text = MyUser.GetUserType().ToString();
                this.SubItems[(int)ListIndex.Enabled].Text = MyUser.IsAccountActiveString();
                this.SubItems[(int)ListIndex.PlaceOfPurchase].Text = MyUser.GetPlaceOfPurchaseString();
                this.SubItems[(int)ListIndex.Comment].Text = MyUser.GetComment();
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            EditUserDialog editUserDialog;
            try
            {
                editUserDialog = new EditUserDialog(null, UpdateMode.Create);
                if (editUserDialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateListView();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when creating user. ", ex);
            }
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            //this.AcceptButton = FilterButton;
            UpdateListView();
        }
    }
}