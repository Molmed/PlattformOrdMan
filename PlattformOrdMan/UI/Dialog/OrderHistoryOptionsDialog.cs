using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public delegate void OrderHistoryOptionOK(bool isIncludedColumnsUpdated);

    public partial class OrderHistoryOptionsDialog : OrdManForm
    {
        public event OrderHistoryOptionOK OnOrderHistoryOptionsOK;
        private DataTable MyInitialIncludedColumns;

        public OrderHistoryOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            LoadTimeIntervalsCombobox();
            InitTimeRestrictionToCompletedPostsOnly();
            InitPlaceOfPurchaseFilteringListView();
            InitIncludedColumnsListView();
            MyInitialIncludedColumns = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Copy();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisableMoveButtons();
        }


        private void EnableMoveButtons()
        {
            MoveDownButton.Enabled = true;
            MoveUpButton.Enabled = true;
        }

        private void DisableMoveButtons()
        {
            MoveDownButton.Enabled = false;
            MoveUpButton.Enabled = false;
        }

        private void GetNewIncludedColumns(out bool isUpdated)
        {
            DataTable table;
            DataRow row;
            DataRow[] rowsNew;
            DataRow[] rowsFromConfig;
            string sort;
            int j = 0;
            isUpdated = false;


            // Create a new presumable config-table
            table = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Clone();
            foreach (IncludedColumnsListViewItem lvi in IncludedColumnsListView.Items)
            {
                if (lvi.Checked)
                {
                    row = table.NewRow();
                    row[Configuration.PostListViewConfColumns.ColEnumName.ToString()] = lvi.GetPostListViewColumn().ToString();
                    row[Configuration.PostListViewConfColumns.ColSortOrder.ToString()] = j++;
                    row[Configuration.PostListViewConfColumns.ColWidth.ToString()] = GetColumnWith(lvi.GetPostListViewColumn());
                    table.Rows.Add(row);
                }
            }

            // Check if the new presumable config-table is different from current config-table
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " asc";
            rowsNew = table.Select("", sort);
            rowsFromConfig = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            if (rowsFromConfig.Length != rowsNew.Length)
            {
                isUpdated = true;
            }
            else
            {
                for (int i = 0; i < rowsNew.Length; i++)
                {
                    if (rowsNew[i][Configuration.PostListViewConfColumns.ColEnumName.ToString()] !=
                        rowsFromConfig[i][Configuration.PostListViewConfColumns.ColEnumName.ToString()])
                    {
                        isUpdated = true;
                    }
                }
            }
            if (isUpdated)
            {
                PlattformOrdManData.Configuration.PostListViewSelectedColumns = table;
            }
        }

        private int GetColumnWith(PostListViewColumn col)
        {
            DataRow[] rows;
            string expression;
            expression = Configuration.PostListViewConfColumns.ColEnumName.ToString() + " = '" + col.ToString() + "'";
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
            if (rows.Length > 0)
            {
                return (int)rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()];
            }
            else
            {
                rows = Configuration.GetDefaultPostListViewColumns().Select(expression);
                return (int)rows[0][Configuration.PostListViewConfColumns.ColWidth.ToString()];
            }
        }

        private void InitIncludedColumnsListView()
        {
            DataRow[] rows;
            string sort, expression;
            IncludedColumnsListViewItem lvi;
            PostListViewColumn postListViewColumn;
            string colEnumName;
            sort = Configuration.PostListViewConfColumns.ColSortOrder.ToString() + " asc";
            rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);

            IncludedColumnsListView.EnableColumnSort = false;
            IncludedColumnsListView.Columns.Add("Columns to show", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);

            // Get columns already included in personal configuration
            foreach (DataRow row in rows)
            {
                colEnumName = row[Configuration.PostListViewConfColumns.ColEnumName.ToString()].ToString();
                postListViewColumn = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colEnumName);
                lvi = new IncludedColumnsListViewItem(postListViewColumn);
                lvi.Checked = true;
                IncludedColumnsListView.Items.Add(lvi);
            }

            // Get columns not included in personal configuration
            foreach (PostListViewColumn col in Enum.GetValues(typeof(PostListViewColumn)))
            {
                expression = Configuration.PostListViewConfColumns.ColEnumName.ToString() + " = '" + col.ToString() + "'";
                rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
                if (rows.Length == 0)
                {
                    lvi = new IncludedColumnsListViewItem(col);
                    lvi.Checked = false;
                    IncludedColumnsListView.Items.Add(lvi);
                }
            }
            DisableMoveButtons();
            IncludedColumnsListView.SelectedIndexChanged += IncludedColumnsListView_SelectedIndexChanged;
            IncludedColumnsListView.GotFocus += IncludedColumnsListView_GotFocus;
            IncludedColumnsListView.LostFocus += IncludedColumnsListView_LostFocus;
        }

        void IncludedColumnsListView_LostFocus(object sender, EventArgs e)
        {
            HandleMoveButtonStatus();
        }

        void IncludedColumnsListView_GotFocus(object sender, EventArgs e)
        {
            HandleMoveButtonStatus();
        }

        void IncludedColumnsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleMoveButtonStatus();
        }

        private void HandleMoveButtonStatus()
        {
            if (IncludedColumnsListView.SelectedIndices.Count == 1 &&
                IncludedColumnsListView.SelectedIndices[0] == 0)
            {
                MoveUpButton.Enabled = false;
                MoveDownButton.Enabled = true;
            }
            else if (IncludedColumnsListView.SelectedIndices.Count == 1 &&
                IncludedColumnsListView.SelectedIndices[0] == IncludedColumnsListView.Items.Count - 1)
            {
                MoveUpButton.Enabled = true;
                MoveDownButton.Enabled = false;
            }
            else if (IncludedColumnsListView.SelectedIndices.Count == 1)
            {
                EnableMoveButtons();
            }
            else
            {
                DisableMoveButtons();
            }        
        }

        private void InitPlaceOfPurchaseFilteringListView()
        {
            ListViewItem lvi;
            string popStr;
            PlaceOfPurchaseFilterListView.Columns.Add("Group", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            PlaceOfPurchaseFilterListView.BeginUpdate();
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                popStr = PlattformOrdManData.GetPlaceOfPurchaseString(pop);
                lvi = new ListViewItem(popStr);
                PlaceOfPurchaseFilterListView.Items.Add(lvi);
                if (PlattformOrdManData.Configuration.PlaceOfPurchaseFilter.Contains(pop.ToString()))
                {
                    lvi.Checked = true;
                }                
            }
            PlaceOfPurchaseFilterListView.EndUpdate();
        }

        private void InitTimeRestrictionToCompletedPostsOnly()
        {
            TimeRestrictionToCompletedPostsCheckbox.Checked = PlattformOrdManData.Configuration.TimeRestrictionForCompletedPostsOnly;
        }

        private void LoadTimeIntervalsCombobox()
        {
            TimeIntervalsForPostsList timeIntervals;
            int defaultTimeInterval;
            defaultTimeInterval = PlattformOrdManData.Configuration.TimeIntervalForPosts;
            timeIntervals = TimeIntervalForPostsManager.GetTimeIntervalsForPosts();
            foreach (TimeIntervalForPosts timeinterval in timeIntervals)
            {
                TimeIntervalsComboBox.Items.Add(timeinterval);
            }
            foreach (object item in TimeIntervalsComboBox.Items)
            {
                if (((TimeIntervalForPosts)item).GetMonths() == defaultTimeInterval)
                {
                    TimeIntervalsComboBox.SelectedItem = item;
                    break;
                }
            }
            if (TimeIntervalsComboBox.SelectedIndex == -1 && TimeIntervalsComboBox.Items.Count > 0)
            {
                TimeIntervalsComboBox.SelectedIndex = 0;
            }
            //LoadPosts();
            //LoadPostsTime();
        }
        private void SaveTimeSettings()
        {
            PlattformOrdManData.Configuration.TimeIntervalForPosts =
                ((TimeIntervalForPosts)TimeIntervalsComboBox.SelectedItem).GetMonths();
            PlattformOrdManData.Configuration.TimeRestrictionForCompletedPostsOnly =
                TimeRestrictionToCompletedPostsCheckbox.Checked;
        }

        private void SavePlaceOfPurchaseFiltering()
        {
            StringCollection popFilter = new StringCollection();
            PlaceOfPurchase pop;
            string popEnumStr;
            foreach (ListViewItem lvi in PlaceOfPurchaseFilterListView.Items)
            {
                if (lvi.Checked)
                {
                    pop = PlattformOrdManData.GetPlaceOfPurchaseFromString(lvi.Text);
                    popEnumStr = pop.ToString();
                    popFilter.Add(popEnumStr);
                }
            }
            PlattformOrdManData.Configuration.PlaceOfPurchaseFilter = popFilter;
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            bool isColumnsUpdated;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SaveTimeSettings();
                SavePlaceOfPurchaseFiltering();
                GetNewIncludedColumns(out isColumnsUpdated);
                if (OnOrderHistoryOptionsOK != null)
                {
                    OnOrderHistoryOptionsOK(isColumnsUpdated);
                }
                Close();
            }
            catch (Exception ex)
            {
                HandleError("Error when saving preferences", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            ListViewItem lvi;
            int selectedInd;
            if(IncludedColumnsListView.SelectedIndices.Count == 1)
            {
                selectedInd = IncludedColumnsListView.SelectedIndices[0];
                lvi = IncludedColumnsListView.Items[selectedInd];
                IncludedColumnsListView.Items.RemoveAt(selectedInd);
                IncludedColumnsListView.Items.Insert(selectedInd - 1, lvi);
                lvi.Selected = true;
                lvi.EnsureVisible();
                IncludedColumnsListView.Select();
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            ListViewItem lvi;
            int selectedInd;
            if (IncludedColumnsListView.SelectedIndices.Count == 1)
            {
                selectedInd = IncludedColumnsListView.SelectedIndices[0];
                lvi = IncludedColumnsListView.Items[selectedInd];
                IncludedColumnsListView.Items.RemoveAt(selectedInd);
                IncludedColumnsListView.Items.Insert(selectedInd + 1, lvi);
                lvi.Selected = true;
                lvi.EnsureVisible();
                IncludedColumnsListView.Select();
            }
        }

        private class IncludedColumnsListViewItem : ListViewItem
        {
            PostListViewColumn MyPostListViewColumn;

            public IncludedColumnsListViewItem(PostListViewColumn col)
                : base(PostListView.GetColumnHeaderName(col))
            {
                MyPostListViewColumn = col;
            }

            public PostListViewColumn GetPostListViewColumn()
            {
                return MyPostListViewColumn;
            }
        }
    }
}
