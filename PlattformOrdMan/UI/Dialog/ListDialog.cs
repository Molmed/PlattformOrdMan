using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Dialog
{
    public partial class ListDialog
    {
        public enum DialogMode
        {
            AcceptanceList,
            SelectList,
            ShowList
        }

        private DialogMode MyDialogMode;
        private ICollection MyRows;
        private LogList MyLogList;
        private String MyTitle, MyColumnName;
        private int MyTotalRows;
        private const int FULL_PANEL_HEIGHT = 55;
        private const int SHRINKED_PANEL_HEIGHT = 35;

        public ListDialog()
        {
            InitializeComponent();
            MaxRowsComboBox.SelectedIndex = 0;
            MyTotalRows = 0;
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;
        }

        public ListDialog(LogList log, String title)
            : this()
        {
            MyLogList = log;
            MyTotalRows = MyLogList.Count;
            MyTitle = title;
            SetDialogMode(DialogMode.ShowList);
            SetRowLimitFilterStatus(true);
            ShowLogList(MyTitle);
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;

        }

        public ListDialog(String columnName, ICollection rows, String title, DialogMode dialogMode)
            : this()
        {
            MyColumnName = columnName;
            MyRows = rows;
            MyTotalRows = MyRows.Count;
            MyTitle = title;
            SetDialogMode(dialogMode);
            SetRowLimitFilterStatus(true);
            ShowRowCollection(MyTitle);
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;

        }

        public ListDialog(String columnName, ICollection rows, String title, DialogMode dialogMode, string message)
            : this()
        {
            int extraHeight;
            int originalHeight;
            MyColumnName = columnName;
            MyRows = rows;
            MyTotalRows = MyRows.Count;
            MyTitle = title;
            SetDialogMode(dialogMode);
            SetRowLimitFilterStatus(true);
            ShowRowCollection(MyTitle);
            TopPanel.Height = FULL_PANEL_HEIGHT;
            MessagePanel.Visible = true;
            originalHeight = MessageLabel.Height;
            MessageLabel.Text = message;
            extraHeight = MessageLabel.Height - originalHeight;
            if (extraHeight > 0)
            {
                TopPanel.Height = TopPanel.Height + extraHeight;
                MessagePanel.Height = MessagePanel.Height + extraHeight;
            }

        }

        public ListDialog(String columnName, ICollection rows, String title, DialogMode dialogMode, int totalRows)
            : this()
        {
            MyColumnName = columnName;
            MyRows = rows;
            MyTotalRows = totalRows;
            MyTitle = title;
            SetDialogMode(dialogMode);
            SetRowLimitFilterStatus(true);
            ShowRowCollection(MyTitle);
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;

        }

        public ListDialog(ColumnHeader[] columnHeaders, ListViewItem[] listViewItems, String title, DialogMode dialogMode)
            : this()
        {
            MyTitle = title;
            SetDialogMode(dialogMode);
            MyTotalRows = listViewItems.Length;
            SetRowLimitFilterStatus(false);
            InitList(columnHeaders, listViewItems, MyTitle);
            TopPanel.Height = 0;

        }

        public ListDialog(ColumnHeader[] columnHeaders, ListViewItem[] listViewItems, String title, DialogMode dialogMode,
            bool executeSorting)
            : this()
        {
            MyTitle = title;
            SetDialogMode(dialogMode);
            MyTotalRows = listViewItems.Length;
            SetRowLimitFilterStatus(false);
            InitList(columnHeaders, listViewItems, MyTitle, executeSorting);
            TopPanel.Height = 0;

        }

        public ListDialog(ColumnHeader[] columnHeaders, ListViewItem[] listViewItems, String title, DialogMode dialogMode,
            string messsage)
            : this()
        {
            int extraHeight;
            int originalHeight;
            MyTitle = title;
            SetDialogMode(dialogMode);
            MyTotalRows = listViewItems.Length;
            SetRowLimitFilterStatus(false);
            InitList(columnHeaders, listViewItems, MyTitle);
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;
            MessagePanel.Visible = true;
            originalHeight = MessageLabel.Height;
            MessageLabel.Text = messsage;
            extraHeight = MessageLabel.Height - originalHeight;
            if (extraHeight > 0)
            {
                TopPanel.Height = TopPanel.Height + extraHeight;
                MessagePanel.Height = MessagePanel.Height + extraHeight;
            }

        }

        public ListDialog(ColumnHeader[] columnHeaders, ListViewItem[] listViewItems, ICollection tagObjects, String title, DialogMode dialogMode)
            : this()
        {
            MyTitle = title;
            SetDialogMode(dialogMode);
            MyTotalRows = tagObjects.Count;
            SetRowLimitFilterStatus(true);
            ShowList(columnHeaders, listViewItems, title, tagObjects);
            TopPanel.Height = SHRINKED_PANEL_HEIGHT;
        }

        public ListDialog(ColumnHeader[] columnHeaders, ListViewItem[] listViewItems, ICollection tagObjects, String title, DialogMode dialogMode,
                    string message)
            : this()
        {
            int extraHeight;
            int originalHeight;
            MyTitle = title;
            SetDialogMode(dialogMode);
            MyTotalRows = tagObjects.Count;
            SetRowLimitFilterStatus(true);
            ShowList(columnHeaders, listViewItems, title, tagObjects);
            TopPanel.Height = FULL_PANEL_HEIGHT;
            MessagePanel.Visible = true;
            originalHeight = MessageLabel.Height;
            MessageLabel.Text = message;
            extraHeight = MessageLabel.Height - originalHeight;
            if (extraHeight > 0)
            {
                TopPanel.Height = TopPanel.Height + extraHeight;
                MessagePanel.Height = MessagePanel.Height + extraHeight;
            }

        }

        public ListDialog(DataTable dataTable, string title, string message)
            : this()
        {
            int extraHeight;
            int originalHeight;
            MyTotalRows = dataTable.Rows.Count;
            if (IsNotEmpty(message))
            {
                TopPanel.Height = FULL_PANEL_HEIGHT;
                MessagePanel.Visible = true;
                originalHeight = MessageLabel.Height;
                MessageLabel.Text = message;
                extraHeight = MessageLabel.Height - originalHeight;
                if (extraHeight > 0)
                {
                    TopPanel.Height = TopPanel.Height + extraHeight;
                    MessagePanel.Height = MessagePanel.Height + extraHeight;
                }
            }
            else
            {
                TopPanel.Height = SHRINKED_PANEL_HEIGHT;
            }
            MyListView.InitList(dataTable, int.MaxValue);
            CounterLabel.Text = GetCounterText(dataTable.Rows.Count, MyTotalRows);

        }


        private String GetCounterText(Int32 displayedRows, Int32 actualRows)
        {
            return "Showing " + displayedRows.ToString() + " of " + actualRows + ".";
        }

        public Object GetSelectedItem()
        {
            // Get selected identifier.
            if (MyListView.TheListView.SelectedIndices.Count > 0)
            {
                if (MyListView.TheListView.SelectedItems[0].Tag == null)
                {
                    // Return identifier.
                    return MyListView.TheListView.SelectedItems[0].Text;
                }
                else
                {
                    // Return associated object.
                    return MyListView.TheListView.SelectedItems[0].Tag;
                }
            }
            else
            {
                // No selection made.
                return null;
            }
        }

        protected void InitList(ColumnHeader[] columnHeaders, ListViewItem[] listItems, String title)
        {
            MyListView.TheListView.Clear();
            MyListView.TheListView.Items.AddRange(listItems);
            MyListView.TheListView.Columns.AddRange(columnHeaders);
            MyListView.TheListView.Sorting = SortOrder.Ascending;
            MyListView.Sort(0);

            // Set the column width to the widest row in the column.
            foreach (ColumnHeader columnHeader in MyListView.TheListView.Columns)
            {
                columnHeader.Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            }

            Text = title;
        }

        protected void InitList(ColumnHeader[] columnHeaders, ListViewItem[] listItems, String title,
            bool executeSorting)
        {
            MyListView.TheListView.Clear();
            MyListView.TheListView.Items.AddRange(listItems);
            MyListView.TheListView.Columns.AddRange(columnHeaders);
            if (executeSorting)
            {
                MyListView.TheListView.Sorting = SortOrder.Ascending;
                MyListView.TheListView.Sort();
            }

            // Set the column width to the widest row in the column.
            foreach (ColumnHeader columnHeader in MyListView.TheListView.Columns)
            {
                columnHeader.Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            }

            Text = title;
        }

        public bool MultiSelect
        {
            get
            {
                return MyListView.TheListView.MultiSelect;
            }
            set
            {
                MyListView.TheListView.MultiSelect = value;
            }
        }

        private void MyListView_DoubleClick(object sender, EventArgs e)
        {
            OKButton_Click(null, null);
        }

        public void SetDoubleClickToOk()
        {
            MyListView.DoubleClick += new EventHandler(MyListView_DoubleClick);

        }

        public void InitSmallImageList(ImageList imageList)
        {
            MyListView.TheListView.SmallImageList = imageList;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (MyDialogMode == DialogMode.SelectList)
            {
                if ((MyListView.TheListView.SelectedItems == null) ||
                     (MyListView.TheListView.SelectedItems.Count == 0))
                {
                    ShowWarning("You must make a selection in the list first!");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void SetDialogMode(DialogMode dialogMode)
        {
            MyDialogMode = dialogMode;
            switch (MyDialogMode)
            {
                case DialogMode.AcceptanceList:
                case DialogMode.SelectList:
                    CloseButton.Visible = true;
                    break;

                case DialogMode.ShowList:
                    CloseButton.Visible = false;
                    // Centralize OK button.
                    OKButton.Location = new Point((Width - OKButton.Width) / 2, OKButton.Location.Y);
                    break;
            }
        }

        public void SetRowLimitFilterStatus(bool status)
        {
            MaxRowsLabel.Visible = status;
            MaxRowsComboBox.Visible = status;
            CounterLabel.Visible = status;
        }

        private void ShowList(ColumnHeader[] columnHeaders,
            ListViewItem[] inputListViewItems, String title, ICollection tagObjects)
        {
            int maxRows, index;
            ListViewItem[] listViewItems;
            IEnumerator rowsEnumerator;
            maxRows = Convert.ToInt32(MaxRowsComboBox.Text);
            listViewItems = new ListViewItem[Math.Min(tagObjects.Count, maxRows)];
            rowsEnumerator = tagObjects.GetEnumerator();
            for (index = 0; index < listViewItems.Length; index++)
            {
                if (rowsEnumerator.MoveNext())
                {
                    listViewItems[index] = inputListViewItems[index];
                    listViewItems[index].Tag = rowsEnumerator.Current;
                }
                if (index >= maxRows - 1)
                {
                    break;
                }
            }
            CounterLabel.Text = GetCounterText(inputListViewItems.GetLength(0), MyTotalRows);
            InitList(columnHeaders, listViewItems, title);
        }

        private void ShowRowCollection(String title)
        {
            ColumnHeader[] columnHeaders;
            IEnumerator rowsEnumerator;
            Int32 index, maxRows;
            ListViewItem[] listViewItems;

            maxRows = Convert.ToInt32(MaxRowsComboBox.Text);
            columnHeaders = new ColumnHeader[1];
            columnHeaders[0] = new ColumnHeader();
            columnHeaders[0].Text = MyColumnName;
            listViewItems = new ListViewItem[Math.Min(MyRows.Count, maxRows)];
            rowsEnumerator = MyRows.GetEnumerator();
            for (index = 0; index < listViewItems.Length; index++)
            {
                if (rowsEnumerator.MoveNext())
                {
                    listViewItems[index] = new ListViewItem(rowsEnumerator.Current.ToString());
                    listViewItems[index].Tag = rowsEnumerator.Current;
                }
                if (index >= maxRows - 1)
                {
                    break;
                }
            }
            CounterLabel.Text = GetCounterText(listViewItems.GetLength(0), MyTotalRows);
            InitList(columnHeaders, listViewItems, title);
        }

        private void ShowLogList(String title)
        {
            ColumnHeader[] columnHeaders;
            Int32 index, maxRows;
            ListViewItem[] listViewItems;

            maxRows = Convert.ToInt32(MaxRowsComboBox.Text);

            columnHeaders = new ColumnHeader[4];
            columnHeaders[0] = new ColumnHeader();
            columnHeaders[0].Text = "Date";
            columnHeaders[0].Width = PlattformOrdManData.LIST_VIEW_COLUMN_HEADER_AUTO_WIDTH;
            columnHeaders[1] = new ColumnHeader();
            columnHeaders[1].Text = "Item";
            columnHeaders[1].Width = PlattformOrdManData.LIST_VIEW_COLUMN_HEADER_AUTO_WIDTH;
            columnHeaders[2] = new ColumnHeader();
            columnHeaders[2].Text = "Operation";
            columnHeaders[2].Width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            columnHeaders[3] = new ColumnHeader();
            columnHeaders[3].Text = "User";
            columnHeaders[3].Width = PlattformOrdManData.LIST_VIEW_COLUMN_HEADER_AUTO_WIDTH;

            listViewItems = new ListViewItem[Math.Min(MyLogList.Count, maxRows)];
            for (index = 0; index < listViewItems.Length; index++)
            {
                listViewItems[index] = new ListViewItem(MyLogList[index].GetDateTime().ToString());
                listViewItems[index].SubItems.Add(MyLogList[index].GetIdentifier());
                listViewItems[index].SubItems.Add(MyLogList[index].GetOperation());
                listViewItems[index].SubItems.Add(MyLogList[index].GetUser().GetName());
                if (index >= maxRows - 1)
                {
                    break;
                }
            }
            CounterLabel.Text = GetCounterText(listViewItems.GetLength(0), MyTotalRows);
            InitList(columnHeaders, listViewItems, title);
        }

        private void MyListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetDataObject(MyListView.ListViewToString(false, true), true);
            }
        }

        private void MaxRowsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyLogList != null)
            {
                ShowLogList(MyTitle);
            }
            else if (MyRows != null)
            {
                ShowRowCollection(MyTitle);
            }
        }

        private void ListDialog_Resize(object sender, EventArgs e)
        {
            if (MyDialogMode == DialogMode.ShowList)
            {
                // Centralize OK button.
                OKButton.Location = new Point((Width - OKButton.Width) / 2, OKButton.Location.Y);
            }
        }
    }
}