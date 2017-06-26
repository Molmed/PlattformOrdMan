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
    public partial class EditUserDialog : OrdManForm
    {
        User MyUser;
        UpdateMode MyUpdateMode;
        public EditUserDialog(User user, UpdateMode updateMode)
        {
            InitializeComponent();
            MyUser = user;
            MyUpdateMode = updateMode;
            foreach (User.UserType userType in Enum.GetValues(typeof(User.UserType)))
            {
                // Don't show the developer user type.
                if (!Enum.Equals(userType, User.UserType.Developer))
                {
                    UserTypeComboBox.Items.Add(userType);
                }
            }
            InitOrderingUnitCombobox();
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    InitCreateMode();
                    break;
                case UpdateMode.Edit:
                    InitEditMode();
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitOrderingUnitCombobox()
        {
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                OrderingUnitComboBox.Items.Add(pop);
            }
            OrderingUnitComboBox.SelectedIndex = 0;
        }


        private void CreateUser()
        {
            String identifier, comment, name;
            identifier = LoginTtextBox.Text.Trim();
            comment = CommentTextBox.Text.Trim();
            name = NameTextBox.Text.Trim();
            if (OrderingUnitComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Group for the user before saving!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MyUser = UserManager.CreateUser(identifier, name, (User.UserType)UserTypeComboBox.SelectedItem, 
                true, (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem, comment);
        }

        private void InitEditMode()
        {
            LoginTtextBox.Text = MyUser.GetIdentifier();
            NameTextBox.Text = MyUser.GetName();
            CommentTextBox.Text = MyUser.GetComment();
            DisableCheckBox.Checked = !MyUser.IsAccountActive();
            UserTypeComboBox.SelectedItem = MyUser.GetUserType();
            OrderingUnitComboBox.SelectedItem = MyUser.GetPlaceOfPurchase();
        }

        private void InitCreateMode()
        {
            this.Text = "Create User";
            SaveButton.Text = "Create";
            OrderingUnitComboBox.SelectedIndex = -1;
            DisableCheckBox.Visible = false;
        }

        private bool IsUpdated()
        {
            if (UserTypeComboBox.SelectedIndex == -1)
            {
                return false;
            }
            return (MyUser.GetIdentifier() != LoginTtextBox.Text.Trim() ||
                    MyUser.GetName() != NameTextBox.Text.Trim() ||
                    MyUser.GetComment() != CommentTextBox.Text.Trim() ||
                    MyUser.GetUserType() != (User.UserType)UserTypeComboBox.SelectedItem ||
                    MyUser.IsAccountActive() == DisableCheckBox.Checked ||
                    IsPlaceOfPurchaseUpdated());
        }

        private bool IsPlaceOfPurchaseUpdated()
        {
            if (OrderingUnitComboBox.SelectedIndex == PlattformOrdManData.NO_COUNT)
            {
                return false;
            }
            else
            {
                return (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem != MyUser.GetPlaceOfPurchase();
            }
        }

        private bool IsReadyToCreate()
        {
            return (IsNotEmpty(LoginTtextBox.Text) &&
                    IsNotEmpty(NameTextBox.Text) &&
                    UserTypeComboBox.SelectedIndex > -1);
        }

        public User GetUser()
        {
            return MyUser;
        }

        private User.UserType GetUserType()
        {
            return (User.UserType)(UserTypeComboBox.SelectedItem);
        }

        private void UpdateUser()
        { 
            String identifier, comment, name;
            identifier = LoginTtextBox.Text.Trim();
            comment = CommentTextBox.Text.Trim();
            name = NameTextBox.Text.Trim();
            MyUser.Set(identifier, name, (User.UserType)UserTypeComboBox.SelectedItem, !DisableCheckBox.Checked,
                (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem, comment);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (MyUpdateMode)
            { 
                case UpdateMode.Create:
                    CreateUser();
                    break;
                case UpdateMode.Edit:
                    UpdateUser();
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        private void LoginTtextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }

        }

        private void UserTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }

        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }
        }

        private void DisableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
        }

        private void OrderingUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
            else
            {
                SaveButton.Enabled = IsReadyToCreate();
            }
        }
    }
}