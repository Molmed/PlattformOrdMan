using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.UI.View.Post;

namespace PlattformOrdMan.UI.Dialog.OptionsDialog
{
    public partial class ViewingOptionsDialog : OrdManForm
    {
        public ViewingOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitIncludedColumnsListView();
        }
        private void GetNewIncludedColumns()
        {
            int j = 0;
            var isUpdated = false;


            // Create a new presumable config-table
            var table = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Clone();
            foreach (IncludedColumnsListViewItem lvi in IncludedColumnsListView.Items)
            {
                if (lvi.Checked)
                {
                    var row = table.NewRow();
                    row[PostListViewConfColumns.ColEnumName.ToString()] = lvi.EnumName;
                    row[PostListViewConfColumns.ColSortOrder.ToString()] = j++;
                    row[PostListViewConfColumns.ColWidth.ToString()] = lvi.GetColumnWith();
                    table.Rows.Add(row);
                }
            }

            // Check if the new presumable config-table is different from current config-table
            var sort = PostListViewConfColumns.ColSortOrder + " asc";
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
                    if (rowsNew[i][PostListViewConfColumns.ColEnumName.ToString()] !=
                        rowsFromConfig[i][PostListViewConfColumns.ColEnumName.ToString()])
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

        private void InitIncludedColumnsListView()
        {
            var sort = PostListViewConfColumns.ColSortOrder + " asc";
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);

            IncludedColumnsListView.EnableColumnSort = false;
            IncludedColumnsListView.Columns.Add("Columns to show", PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH);
            var personalColumns = PostListView.GetColumns();
            personalColumns.ForEach(c =>
            {
                var lvi2 = new IncludedColumnsListViewItem(c) { Checked = true };
                IncludedColumnsListView.Items.Add(lvi2);
            });

            // Get columns not included in personal configuration
            var excludedColumns = PostListView.GetExcudedColumns();
            excludedColumns.ForEach(c =>
            {
                var lvi = new IncludedColumnsListViewItem(c) { Checked = false };
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

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            if (IncludedColumnsListView.SelectedIndices.Count == 1)
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

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                GetNewIncludedColumns();
                PlattformOrdManData.OEventHandler.FireViewingOptionsChanged();
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
    }
}
