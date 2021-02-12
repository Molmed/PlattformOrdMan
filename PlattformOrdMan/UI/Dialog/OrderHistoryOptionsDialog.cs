using System;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.View;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public delegate void OrderHistoryOptionOK(bool isIncludedColumnsUpdated);

    public partial class OrderHistoryOptionsDialog : OrdManForm
    {
        public event OrderHistoryOptionOK OnOrderHistoryOptionsOK;

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
            int j = 0;
            isUpdated = false;


            // Create a new presumable config-table
            var table = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Clone();
            foreach (IncludedColumnsListViewItem lvi in IncludedColumnsListView.Items)
            {
                if (lvi.Checked)
                {
                    var row = table.NewRow();
                    row[Configuration.PostListViewConfColumns.ColEnumName.ToString()] = lvi.GetPostListViewColumn().ToString();
                    row[Configuration.PostListViewConfColumns.ColSortOrder.ToString()] = j++;
                    row[Configuration.PostListViewConfColumns.ColWidth.ToString()] = GetColumnWith(lvi.GetPostListViewColumn());
                    table.Rows.Add(row);
                }
            }

            // Check if the new presumable config-table is different from current config-table
            var sort = Configuration.PostListViewConfColumns.ColSortOrder + " asc";
            var rowsNew = table.Select("", sort);
            var rowsFromConfig = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
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
            var expression = Configuration.PostListViewConfColumns.ColEnumName + " = '" + col + "'";
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
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
            var sort = Configuration.PostListViewConfColumns.ColSortOrder + " asc";
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);

            IncludedColumnsListView.EnableColumnSort = false;
            IncludedColumnsListView.Columns.Add("Columns to show", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            var personalColumns = PostListView.GetColumns();
            personalColumns.ForEach(c =>
            {
                var lvi2 = new IncludedColumnsListViewItem(c.ColEnum){Checked = true};
                IncludedColumnsListView.Items.Add(lvi2);
            });

            // Get columns not included in personal configuration
            var excludedColumns = PostListView.GetExcudedColumns();
            excludedColumns.ForEach(c =>
            {
                var lvi = new IncludedColumnsListViewItem(c.ColEnum){Checked = false};
                IncludedColumnsListView.Items.Add(lvi);
            });
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
            PlaceOfPurchaseFilterListView.Columns.Add("Group", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            PlaceOfPurchaseFilterListView.BeginUpdate();
            foreach (PlaceOfPurchase pop in Enum.GetValues(typeof(PlaceOfPurchase)))
            {
                var popStr = PlattformOrdManData.GetPlaceOfPurchaseString(pop);
                var lvi = new ListViewItem(popStr);
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
            var defaultTimeInterval = PlattformOrdManData.Configuration.TimeIntervalForPosts;
            var timeIntervals = TimeIntervalForPostsManager.GetTimeIntervalsForPosts();
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
            foreach (ListViewItem lvi in PlaceOfPurchaseFilterListView.Items)
            {
                if (lvi.Checked)
                {
                    var pop = PlattformOrdManData.GetPlaceOfPurchaseFromString(lvi.Text);
                    var popEnumStr = pop.ToString();
                    popFilter.Add(popEnumStr);
                }
            }
            PlattformOrdManData.Configuration.PlaceOfPurchaseFilter = popFilter;
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SaveTimeSettings();
                SavePlaceOfPurchaseFiltering();
                bool isColumnsUpdated;
                GetNewIncludedColumns(out isColumnsUpdated);
                OnOrderHistoryOptionsOK?.Invoke(isColumnsUpdated);
                Close();
            }
            catch (Exception ex)
            {
                HandleError("Error when saving preferences", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if(IncludedColumnsListView.SelectedIndices.Count == 1)
            {
                var selectedInd = IncludedColumnsListView.SelectedIndices[0];
                var lvi = IncludedColumnsListView.Items[selectedInd];
                IncludedColumnsListView.Items.RemoveAt(selectedInd);
                IncludedColumnsListView.Items.Insert(selectedInd - 1, lvi);
                lvi.Selected = true;
                lvi.EnsureVisible();
                IncludedColumnsListView.Select();
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            if (IncludedColumnsListView.SelectedIndices.Count == 1)
            {
                var selectedInd = IncludedColumnsListView.SelectedIndices[0];
                var lvi = IncludedColumnsListView.Items[selectedInd];
                IncludedColumnsListView.Items.RemoveAt(selectedInd);
                IncludedColumnsListView.Items.Insert(selectedInd + 1, lvi);
                lvi.Selected = true;
                lvi.EnsureVisible();
                IncludedColumnsListView.Select();
            }
        }

        private class IncludedColumnsListViewItem : ListViewItem
        {
            PostListViewColumn _postListViewColumn;

            public IncludedColumnsListViewItem(PostListViewColumn col)
                : base(PostListView.GetColumnHeaderName(col))
            {
                _postListViewColumn = col;
            }

            public PostListViewColumn GetPostListViewColumn()
            {
                return _postListViewColumn;
            }
        }
    }
}
