using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Dialog
{

    public partial class EditSupplierDialog : OrdManForm, ISupplierForm
    {
        private Supplier MySupplier;
        private UpdateMode MyUpdateMode;
        private CustomerNumberLocalList MyLocalCustomerNumbers;
        private const string DELETE = "Delete";
        private const string SEND_TO_ARCHIVE = "Send to archive";
        private const string UPDATE = "Update";
        private const string ADD = "Add";

        public EditSupplierDialog(Supplier supplier, UpdateMode updateMode)
        {
            InitializeComponent();
            MySupplier = supplier;
            MyUpdateMode = updateMode;
            SaveButton.Enabled = false;
            MyLocalCustomerNumbers = new CustomerNumberLocalList();
            InitFilterCheckBox();
            InitCustomerNumberListView();
            switch (updateMode)
            { 
                case UpdateMode.Create:
                    InitCreateMode();
                    break;
                case UpdateMode.Edit:
                    InitEditMode();
                    break;
            }
        }

        private void DividePosts(PostList posts, out PostList nonComplPosts, out PostList complPosts)
        {
            nonComplPosts = new PostList();
            complPosts = new PostList();
            foreach (Post post in posts)
            {
                if (post.GetPostStatus() != Post.PostStatus.Completed)
                {
                    nonComplPosts.Add(post);
                }
                else
                {
                    complPosts.Add(post);
                }
            }
        }

        private void DeleteCustomerNumber_MenuClick(object sender, EventArgs e)
        { 
            // Decide if the selected customer number should be deleted, archived or if
            // it deletion not permitted
            PostList posts, complPosts, nonComplPosts;
            CustomerNumber custNum;
            CustomerNumberLocal custNumLocal;
            try
            {
                foreach (CustomerNumberViewItem viewItem in CustomerNumberListView.SelectedItems)
                {
                    custNum = viewItem.GetCustomerNumberLocal();
                    if (IsNotNull(custNum))
                    {
                        posts = PostManager.GetPostsByCustomerNumberId(custNum.GetId());
                        DividePosts(posts, out nonComplPosts, out complPosts);
                        if (nonComplPosts.Count > 0)
                        {
                            MessageBox.Show("There are non-completed posts with customer number " + custNum.GetIdentifier() + 
                                ", deletion is canceled! (Search in order history for selected customer number)",
                                "Deletion canceled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (complPosts.Count > 0)
                        {
                            MessageBox.Show("There are completed posts with customer number " + custNum.GetIdentifier() +
                                ", selected customer number will be disabled! (not deleted)", "Customer number disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            custNum.SetLocal(custNum.GetIdentifier(), custNum.GetDescription(), custNum.GetPlaceOfPurchase(), false);
                            MyLocalCustomerNumbers.GetById(custNum.GetId()).SetIsUpdated(true);
                            LoadCustomerNumberListView();
                            if (MyUpdateMode == UpdateMode.Edit)
                            {
                                SaveButton.Enabled = true;
                            }
                        }
                        else
                        {
                            custNumLocal = MyLocalCustomerNumbers.GetById(custNum.GetId());
                            MyLocalCustomerNumbers.Remove(custNumLocal);
                            
                            RemoveFromCustomerNumberList(custNum);
                            if (MyUpdateMode == UpdateMode.Edit)
                            {
                                SaveButton.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                HandleError("Error when deleting customer number", ex);
            }
        }

        private void RemoveFromCustomerNumberList(CustomerNumber custNum)
        {
            foreach (CustomerNumberViewItem viewItem in CustomerNumberListView.Items)
            {
                if (viewItem.GetCustomerNumberLocal().GetId() == custNum.GetId())
                {
                    CustomerNumberListView.Items.Remove(viewItem);
                    ReInitCustNumColumnWidths();
                    break;
                }
            }
        }

        private void ArchiveCustomerNumber_MenuClick(object sender, EventArgs e)
        { 
            
        }

        private void UpdateCustomerNumberList(CustomerNumber custNum)
        {
            foreach (CustomerNumberViewItem viewItem in CustomerNumberListView.Items)
            {
                if (viewItem.GetCustomerNumberLocal().GetId() == custNum.GetId())
                {
                    viewItem.Update();
                    break;
                }
            }
        }

        private void SelectCustomerNumberInList(CustomerNumber cust)
        {
            foreach (CustomerNumberViewItem viewItem in CustomerNumberListView.Items)
            {
                if (cust.GetId() == viewItem.GetCustomerNumberLocal().GetId())
                {
                    viewItem.Selected = true;
                    CustomerNumberListView.EnsureVisible(viewItem.Index);
                    break;
                }
            }
        }

        private void UpdateCustomerNumber_MenuClick(object sender, EventArgs e)
        {
            CreateEditCustomerNumber createEditCustomerNumber;
            CustomerNumber updatedCustomerNumber, createdCustomerNumber;
            CustomerNumberLocal updatedCustNumLocal, createdCustNumLocal;
            CustomerNumber origCustNum;
            PostList posts, nonComplPosts, complPosts;
            try
            {
                if (CustomerNumberListView.SelectedItems.Count > 0)
                {
                    updatedCustomerNumber = ((CustomerNumberViewItem)CustomerNumberListView.SelectedItems[0]).GetCustomerNumberLocal();
                    origCustNum = updatedCustomerNumber.Clone();
                    createEditCustomerNumber = new CreateEditCustomerNumber(UpdateMode.Edit, updatedCustomerNumber, MySupplier);
                    if (createEditCustomerNumber.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        
                        posts = PostManager.GetPostsByCustomerNumberId(updatedCustomerNumber.GetId());
                        DividePosts(posts, out nonComplPosts, out complPosts);
                        if (complPosts.Count > 0 && origCustNum.IsAnyInfoFieldUpdated(updatedCustomerNumber))
                        {
                            MessageBox.Show("There are completed posts with customer number " + origCustNum.GetIdentifier() +
                                ", selected customer number will be disabled and a new customer number will be created! ", "Customer number update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            // Create new customer number
                            createdCustomerNumber = new CustomerNumber(updatedCustomerNumber.GetIdentifier(), updatedCustomerNumber.GetDescription(),
                                updatedCustomerNumber.GetPlaceOfPurchase(), MySupplier, true);
                            // Set the updated customer number back to original and set enabled = false
                            updatedCustomerNumber.SetLocal(origCustNum.GetIdentifier(), origCustNum.GetDescription(), origCustNum.GetPlaceOfPurchase(), false);
                            MyLocalCustomerNumbers.GetById(updatedCustomerNumber.GetId()).SetIsUpdated(true);
                            createdCustNumLocal = new CustomerNumberLocal(createdCustomerNumber, false);
                            MyLocalCustomerNumbers.Add(createdCustNumLocal);
                            // Check if there are any posts in-line that has to be updated with the created customer number
                            if (nonComplPosts.Count > 0)
                            {
                                createdCustNumLocal.SetUpdatePosts(nonComplPosts);
                            }

                            // To make the created customer number selected in list...
                            updatedCustomerNumber = createdCustomerNumber;
                        }
                        else
                        {
                            updatedCustNumLocal = MyLocalCustomerNumbers.GetById(updatedCustomerNumber.GetId());
                            updatedCustNumLocal.SetIsUpdated(true);
                        }

                        LoadCustomerNumberListView();
                        SelectCustomerNumberInList(updatedCustomerNumber);
                        if (MyUpdateMode == UpdateMode.Edit)
                        {
                            SaveButton.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when updating customer number", ex);
            }        
        }

        private void AddCustomerNumber_MenuClick(object sender, EventArgs e)
        {
            CreateEditCustomerNumber createEditCustomerNumber;
            CustomerNumber tempCN;
            CustomerNumberLocal newCustNum;
            try
            {
                tempCN = new CustomerNumber("", "", PlaceOfPurchase.Other, MySupplier, true);
                createEditCustomerNumber = new CreateEditCustomerNumber(UpdateMode.Create, tempCN, MySupplier);
                if (createEditCustomerNumber.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    newCustNum = new CustomerNumberLocal(tempCN, false);
                    MyLocalCustomerNumbers.Add(newCustNum);
                    CustomerNumberListView.Items.Add(new CustomerNumberViewItem(newCustNum));
                    ReInitCustNumColumnWidths();
                    SelectCustomerNumberInList(newCustNum);
                    if (MyUpdateMode == UpdateMode.Edit)
                    {
                        SaveButton.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when creating customer number", ex);
            }
        }

        private void InitCustomerNumberListView()
        {
            InitCustomerNumberListViewColumns(false);

            AddMenuItem2(CustomerNumberListView, ADD, AddCustomerNumber_MenuClick);
            AddMenuItem2(CustomerNumberListView, UPDATE, UpdateCustomerNumber_MenuClick);
            AddMenuItem2(CustomerNumberListView, DELETE, DeleteCustomerNumber_MenuClick);
            CustomerNumberListView.ContextMenuStrip.Opening +=ContextMenuStrip_Opening;
            CustomerNumberListView.DoubleClick += UpdateCustomerNumber_MenuClick;
        }

        private void InitCustomerNumberListViewColumns(bool showArchived)
        {
            if (CustomerNumberListView.Columns.Count > 0)
            {
                CustomerNumberListView.Columns.Clear();
            }
            CustomerNumberListView.AddColumn("Customer number", -2, View.OrderManListView.ListDataType.String);
            CustomerNumberListView.AddColumn("Description", -2, View.OrderManListView.ListDataType.String);
            CustomerNumberListView.AddColumn("Used by Group", -2, View.OrderManListView.ListDataType.String);
            if (showArchived)
            {
                CustomerNumberListView.AddColumn("Archived", -2, View.OrderManListView.ListDataType.String);
            }

        }

        private void ReInitCustNumColumnWidths()
        {
            int colHeaderWidth;
            CustomerNumberListView.BeginUpdate();
            foreach (ColumnHeader col in CustomerNumberListView.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                colHeaderWidth = col.Width;
                col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                if (col.Width < colHeaderWidth)
                {
                    col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            CustomerNumberListView.EndUpdate();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            SetVisible(sender, UPDATE, false);
            SetVisible(sender, DELETE, false);

            if (CustomerNumberListView.SelectedItems.Count > 0)
            {
                SetVisible(sender, UPDATE, true);
                SetVisible(sender, DELETE, true);                
            }
        }

        private void InitFilterCheckBox()
        {
            string str;
            str = "Only show customer numbers for the ";
            str += PlattformOrdManData.GetGroupCategory(UserManager.GetCurrentUser().GetPlaceOfPurchase()).ToString();
            str += " group";
            CustomerNumberFilterCheckBox.Text = str;
        }

        private bool HasArchivedCustomerNumbers(CustomerNumberList custList)
        {
            if (IsNotNull(custList))
            {
                foreach (CustomerNumber custNum in custList)
                {
                    if (!custNum.IsEnabled())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private CustomerNumberList GetCustomerNumbers(bool onlyCurrentUserGroup)
        {
            CustomerNumberList custNumbrs = new CustomerNumberList();
            foreach (CustomerNumberLocal custN in MyLocalCustomerNumbers)
            {
                if (!onlyCurrentUserGroup ||
                    custN.GetGroupCategory() == UserManager.GetCurrentUser().GetGroupCategory())
                {
                    custNumbrs.Add(custN);
                }
            }
            return custNumbrs;
        }

        private void LoadCustomerNumberListView()
        {
            CustomerNumberList custNumbers;
            
            custNumbers = GetCustomerNumbers(CustomerNumberFilterCheckBox.Checked);
            if (HasArchivedCustomerNumbers(custNumbers) && CustomerNumberListView.Columns.Count == 3)
            {
                InitCustomerNumberListViewColumns(true);
            }
            else if (!HasArchivedCustomerNumbers(custNumbers) && CustomerNumberListView.Columns.Count == 4)
            {
                InitCustomerNumberListViewColumns(false);
            }
            custNumbers.Sort();
            CustomerNumberListView.BeginLoadItems(custNumbers.Count);
            foreach (CustomerNumberLocal custNum in custNumbers)
            {
                CustomerNumberListView.AddItem(new CustomerNumberViewItem(custNum));
            }
            CustomerNumberListView.EndAddItems();
            ReInitCustNumColumnWidths();
        }

        public bool HasSupplierLoaded(int supplierId)
        {
            return IsNotNull(MySupplier) && supplierId == MySupplier.GetId();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            if (IsNotNull(supplier) && HasSupplierLoaded(supplier.GetId()))
            {
                MySupplier = supplier;
                if (IsNotNull(MySupplier))
                {
                    UpdateForm();
                }
                else
                {
                    this.Close();
                }
            }
        }

        public void AddCreatedSupplier(Supplier supplier)
        { 
            // Do nothing
        }

        private void InitCreateMode()
        {
            SaveButton.Text = "Create";
            this.Text = "Create supplier";
            DissableCheckBox.Visible = false;
        }

        private void InitEditMode()
        {
            foreach (CustomerNumber custNum in MySupplier.GetCustomerNumbers())
            {
                MyLocalCustomerNumbers.Add(new CustomerNumberLocal(custNum, true));
            }
            UpdateForm();
            CustomerNumberFilterCheckBox.CheckedChanged += CustomerNumberFilterCheckBox_CheckedChanged;
        }

        void CustomerNumberFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerNumberListView();
            }
            catch (Exception ex)
            {
                HandleError("Error when updating customer number list", ex);
            }
        }

        private void UpdateForm()
        {
            IdentifierTextBox.Text = MySupplier.GetIdentifier();
            DissableCheckBox.Checked = !MySupplier.IsEnabled();
            TelNrTextBox.Text = MySupplier.GetTelNr();
            ContractTerminationTextBox.Text = MySupplier.GetContractTerminate();
            CommentTextBox.Text = MySupplier.GetComment();
            ShortNameTextBox.Text = MySupplier.GetShortName();
            LoadCustomerNumberListView();
        }

        public Supplier GetSupplier()
        {
            return MySupplier;
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsUpdated()
        {
            string identifier = "", telnr = "", contractTerminate = "", comment = "", shortName = "";
            if (IsNotNull(MySupplier.GetIdentifier()))
            {
                identifier = MySupplier.GetIdentifier();
            }
            if (IsNotNull(MySupplier.GetTelNr()))
            {
                telnr = MySupplier.GetTelNr();
            }

            if (IsNotNull(MySupplier.GetContractTerminate()))
            {
                contractTerminate = MySupplier.GetContractTerminate();
            }
            if (MySupplier.HasComment())
            {
                comment = MySupplier.GetComment();
            }
            if (IsNotNull(MySupplier.GetShortName()))
            {
                shortName = MySupplier.GetShortName();
            }
            return (identifier != IdentifierTextBox.Text ||
                    telnr != TelNrTextBox.Text ||
                    contractTerminate != ContractTerminationTextBox.Text ||
                    comment != CommentTextBox.Text ||
                    MySupplier.IsEnabled() == DissableCheckBox.Checked||
                    shortName != ShortNameTextBox.Text ||
                    IsCustomerNumberUpdated());
        }

        private bool IsCustomerNumberUpdated()
        {
            if (MyLocalCustomerNumbers.Count < MySupplier.GetCustomerNumbers().Count)
            {
                return true;
            }
            foreach (CustomerNumberLocal custNumLocal in MyLocalCustomerNumbers)
            {
                if (!custNumLocal.IsExistent() || custNumLocal.IsUpdated())
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateCustomerNumbers(bool fireSupplierCreated)
        {
            CustomerNumber newCustNum;
            foreach (CustomerNumberLocal custLocal in MyLocalCustomerNumbers)
            {
                if (!custLocal.IsExistent())
                {
                    newCustNum = CustomerNumberManager.CreateCustomerNumber(custLocal.GetIdentifier(),
                        custLocal.GetDescription(), custLocal.GetPlaceOfPurchase(), MySupplier.GetId());
                    MySupplier.AddCustomerNumberLocal(newCustNum);
                    custLocal.SetId(newCustNum.GetId());
                }
            }
            if (fireSupplierCreated)
            {
                PlattformOrdManData.OEventHandler.FireSupplierCreate(MySupplier);
            }
        }

        private void UpdateCustomerNumbers()
        {
            foreach (CustomerNumberLocal custNLocal in MyLocalCustomerNumbers)
            {
                if (custNLocal.IsUpdated())
                {
                    custNLocal.Set();
                }
            }

        }

        private void DeleteCustomerNumbers()
        {
            DataIdentifierList clonedCustNums;
            clonedCustNums = (DataIdentifierList)MySupplier.GetCustomerNumbers().Clone();
            foreach (CustomerNumber custNum in clonedCustNums)
            {
                if (MyLocalCustomerNumbers.GetById(custNum.GetId()) == null)
                {
                    CustomerNumberManager.DeleteCustomerNumber(custNum.GetId());
                    MySupplier.RemoveCustomerNumberLocal(custNum);                    
                }
            }
        }

        private void UpdateSupplier()
        {
            HandleEnableStatus();
            MySupplier.SetIdentifier(IdentifierTextBox.Text);
            MySupplier.SetTelNr(TelNrTextBox.Text);
            MySupplier.SetContractTermination(ContractTerminationTextBox.Text);
            MySupplier.SetEnabled(!DissableCheckBox.Checked);
            MySupplier.SetComment(CommentTextBox.Text);
            MySupplier.SetShortName(ShortNameTextBox.Text);
            UpdateCustomerNumbers();
            DeleteCustomerNumbers();
            CreateCustomerNumbers(false);
            MySupplier.ResetCustomerNumberLocal();
            MySupplier.Set();
        }

        private void HandleEnableStatus()
        {
            string str;
            if (MySupplier.IsEnabled() && DissableCheckBox.Checked)
            { 
                str = "All products associated with this supplier are dissabled as well!";
                MessageBox.Show(str, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (Merchandise merchandise in MerchandiseManager.GetMerchandiseForSupplier(MySupplier.GetId()))
                {
                    merchandise.SetEnabled(false);
                    merchandise.Set();
                }
                MerchandiseManager.RefreshCache();
            }
            else if (!MySupplier.IsEnabled() && !DissableCheckBox.Checked)
            {
                str = "Products for this supplier may still be dissabled!";
                MessageBox.Show(str, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateSupplier()
        {
            String identifier, telNr, comment, contracteTermination, shortName;
            identifier = IdentifierTextBox.Text;
            telNr = TelNrTextBox.Text;
            comment = CommentTextBox.Text;
            contracteTermination = ContractTerminationTextBox.Text;
            shortName = ShortNameTextBox.Text;
            MySupplier = SupplierManager.CreateSupplier(identifier, shortName, telNr, comment, 
                contracteTermination, false);
            CreateCustomerNumbers(true);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                PlattformOrdManData.BeginTransaction();
                switch (MyUpdateMode)
                {
                    case UpdateMode.Create:
                        CreateSupplier();
                        break;
                    case UpdateMode.Edit:
                        UpdateSupplier();
                        break;
                }
                PlattformOrdManData.CommitTransaction();
            }
            catch(Exception ex)
            {
                PlattformOrdManData.RollbackTransaction();
                HandleError("Error when updating/creating a supplier", ex);
            }
            finally
            {
                Close();
            }
        }

        private void IdentifierTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveButton.Enabled = IsNotEmpty(IdentifierTextBox.Text);
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void TelNrTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void ContractTerminationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void CommentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void DissableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private void ShortNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MyUpdateMode == UpdateMode.Edit)
            {
                SaveButton.Enabled = IsUpdated();
            }
        }

        private class CustomerNumberViewItem : ListViewItem
        {
            private CustomerNumberLocal MyCustomerNumber;
            private enum ListIndex
            { 
                Identifier,
                Description,
                Group,
                Enabled
            }

            public CustomerNumberViewItem(CustomerNumberLocal custNum)
                : base(custNum.GetIdentifier())
            {
                GroupCategory group;
                group = PlattformOrdManData.GetGroupCategory(custNum.GetPlaceOfPurchase());
                MyCustomerNumber = custNum;
                this.SubItems.Add(custNum.GetDescription());
                this.SubItems.Add(group.ToString());
                if (!custNum.IsEnabled())
                {
                    this.SubItems.Add(custNum.GetIsArchivedString());
                    this.ForeColor = Color.Red;
                }
            }

            public CustomerNumberLocal GetCustomerNumberLocal()
            {
                return MyCustomerNumber;
            }

            public void Update()
            {
                this.SubItems[(int)ListIndex.Identifier].Text = MyCustomerNumber.GetIdentifier();
                this.SubItems[(int)ListIndex.Description].Text = MyCustomerNumber.GetDescription();
                this.SubItems[(int)ListIndex.Group].Text = MyCustomerNumber.GetGroupCategory().ToString();
                if (this.SubItems.Count > (int)ListIndex.Enabled && !MyCustomerNumber.IsEnabled())
                {
                    this.SubItems[(int)ListIndex.Enabled].Text = MyCustomerNumber.GetIsArchivedString();
                    this.ForeColor = Color.Red;
                }
                else if(this.SubItems.Count > (int)ListIndex.Enabled)
                { 
                    this.SubItems[(int)ListIndex.Enabled].Text = "";
                    this.ForeColor = Color.Black;
                }
            }

        }

        private class CustomerNumberLocal : CustomerNumber
        {
            // Class for local Customer number, user have defined it but not yet saved it to db
            // Clone an existing customer number
            private bool MyIsExistent;
            private bool MyIsUpdated;
            private PostList MyPendingUpdatePosts;
            private bool MyHasPostsToBeUpdated;

            public CustomerNumberLocal(CustomerNumber custNum, bool isExistent)
                : base(custNum.GetId(), custNum.GetIdentifier(), custNum.GetDescription(), 
                custNum.GetPlaceOfPurchase(), custNum.GetSupplierId(), custNum.IsEnabled())
            {
                MyIsExistent = isExistent;
                MyIsUpdated = false;
                MyHasPostsToBeUpdated = false;
                MyPendingUpdatePosts = null;
            }

            public bool IsExistent()
            {
                return MyIsExistent;
            }

            public bool IsUpdated()
            {
                return MyIsUpdated;
            }

            public PostList GetPendingUpdatePosts()
            {
                return MyPendingUpdatePosts;
            }

            public bool HasPendingPostsUpdate()
            {

                return MyHasPostsToBeUpdated;
                
            }

            public void SetId(int newId)
            {
                base.SetId(newId);
            }

            public void SetUpdatePosts(PostList posts)
            {
                MyPendingUpdatePosts = posts;
                MyHasPostsToBeUpdated = true;
            }

            public void SetIsUpdated(bool isUpdated)
            {
                MyIsUpdated = isUpdated;
            }
        }

        private class CustomerNumberLocalList : DataIdentityList
        {
            public new CustomerNumberLocal GetById(Int32 id)
            {
                return (CustomerNumberLocal)(base.GetById(id));
            }

            public new CustomerNumberLocal this[Int32 index]
            {
                get
                {
                    return (CustomerNumberLocal)(base[index]);
                }
                set
                {
                    base[index] = value;
                }
            }
        }

        private void CustomerNumberFilterCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}