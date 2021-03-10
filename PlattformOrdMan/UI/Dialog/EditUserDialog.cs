using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class EditUserDialog : OrdManForm
    {
        User _user;
        readonly UpdateMode _updateMode;
        public EditUserDialog(User user, UpdateMode updateMode)
        {
            InitializeComponent();
            _user = user;
            _updateMode = updateMode;
            foreach (User.UserType userType in Enum.GetValues(typeof(User.UserType)))
            {
                // Don't show the developer user type.
                if (!Equals(userType, User.UserType.Developer))
                {
                    UserTypeComboBox.Items.Add(userType);
                }
            }
            InitOrderingUnitCombobox();
            switch (_updateMode)
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
            var identifier = LoginTtextBox.Text.Trim();
            var comment = CommentTextBox.Text.Trim();
            var name = NameTextBox.Text.Trim();
            if (OrderingUnitComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Group for the user before saving!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _user = UserManager.CreateUser(identifier, name, (User.UserType)UserTypeComboBox.SelectedItem, 
                true, (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem, comment);
        }

        private void InitEditMode()
        {
            LoginTtextBox.Text = _user.GetIdentifier();
            NameTextBox.Text = _user.GetName();
            CommentTextBox.Text = _user.GetComment();
            DisableCheckBox.Checked = !_user.IsAccountActive();
            UserTypeComboBox.SelectedItem = _user.GetUserType();
            OrderingUnitComboBox.SelectedItem = _user.GetPlaceOfPurchase();
        }

        private void InitCreateMode()
        {
            Text = "Create User";
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
            return (_user.GetIdentifier() != LoginTtextBox.Text.Trim() ||
                    _user.GetName() != NameTextBox.Text.Trim() ||
                    _user.GetComment() != CommentTextBox.Text.Trim() ||
                    _user.GetUserType() != (User.UserType)UserTypeComboBox.SelectedItem ||
                    _user.IsAccountActive() == DisableCheckBox.Checked ||
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
                return (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem != _user.GetPlaceOfPurchase();
            }
        }

        private bool IsReadyToCreate()
        {
            return (IsNotEmpty(LoginTtextBox.Text) &&
                    IsNotEmpty(NameTextBox.Text) &&
                    UserTypeComboBox.SelectedIndex > -1);
        }

        private void UpdateUser()
        {
            var identifier = LoginTtextBox.Text.Trim();
            var comment = CommentTextBox.Text.Trim();
            var name = NameTextBox.Text.Trim();
            _user.Set(identifier, name, (User.UserType)UserTypeComboBox.SelectedItem, !DisableCheckBox.Checked,
                (PlaceOfPurchase)OrderingUnitComboBox.SelectedItem, comment);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (_updateMode)
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
            if (_updateMode == UpdateMode.Edit)
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
            if (_updateMode == UpdateMode.Edit)
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
            if (_updateMode == UpdateMode.Edit)
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
            if (_updateMode == UpdateMode.Edit)
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
            if (_updateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated() && IsReadyToCreate();
            }
        }

        private void OrderingUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_updateMode == UpdateMode.Edit)
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