using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.View
{
    public partial class SortedListView : UserControl
    {
        public SortedListView()
        {
            InitializeComponent();
            TheListView.DoubleClick += new EventHandler(TheListView_DoubleClick);
        }

        private void TheListView_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        public ListView TheListView
        {
            get
            {
                return MyListView;
            }
            set
            {
                MyListView = value;
            }
        }

        public String ListViewToString(Boolean includeHeaders, Boolean onlySelected)
        {
            Int32 i;
            Int32 j;
            String headerLine;
            String[] textLines;
            String[] tempColumns;
            String[] headers;
            String finalString = "";

            headers = new String[TheListView.Columns.Count];
            for (i = 0; i < TheListView.Columns.Count; i++)
            {
                headers[i] = TheListView.Columns[i].Text;
            }
            headerLine = String.Join("\t", headers);

            if (onlySelected)
            {
                textLines = new String[TheListView.SelectedItems.Count];
                for (i = 0; i < TheListView.SelectedItems.Count; i++)
                {
                    tempColumns = new String[TheListView.SelectedItems[i].SubItems.Count];
                    for (j = 0; j < TheListView.SelectedItems[i].SubItems.Count; j++)
                    {
                        tempColumns[j] = TheListView.SelectedItems[i].SubItems[j].Text;
                    }
                    textLines[i] = String.Join("\t", tempColumns);
                }
            }
            else
            {
                textLines = new String[TheListView.Items.Count];
                for (i = 0; i < TheListView.Items.Count; i++)
                {
                    tempColumns = new String[TheListView.Items[i].SubItems.Count];
                    for (j = 0; j < TheListView.Items[i].SubItems.Count; j++)
                    {
                        tempColumns[j] = TheListView.Items[i].SubItems[j].Text;
                    }
                    textLines[i] = String.Join("\t", tempColumns);
                }
            }

            if (includeHeaders)
            {
                finalString = headerLine + Environment.NewLine;
            }
            finalString += String.Join(Environment.NewLine, textLines);
            return finalString;
        }

        public void InitList(DataTable table, Int32 maxRows)
        {
            DataRow row;
            Int32 x, y;
            ListViewItem listViewItem;
            int width;

            MyListView.BeginUpdate();
            MyListView.Columns.Clear();

            width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
            // Add the columns.
            for (x = 0; x < table.Columns.Count; x++)
            {
                MyListView.Columns.Add(table.Columns[x].ColumnName, width);
            }

            // Add the rows.
            for (y = 0; y < table.Rows.Count; y++)
            {
                row = table.Rows[y];
                listViewItem = new ListViewItem(row[0].ToString());
                for (x = 1; x < table.Columns.Count; x++)
                {
                    listViewItem.SubItems.Add(row[x].ToString().Trim());
                }
                MyListView.Items.Add(listViewItem);

                if (y >= maxRows - 1)
                {
                    break;
                }
            }

            // Set the column width to the widest row in the column.
            for (x = 0; x < MyListView.Columns.Count; x++)
            {
                MyListView.Columns[x].Width = width;
            }
            MyListView.EndUpdate();
        }

        public void AddColumn(String columnName, Int32 columnWidth)
        {

            MyListView.Columns.Add(columnName, columnWidth);

        }

        public void ResetSortColumn()
        {
            MyListView.ListViewItemSorter = new ListViewComparer(0, MyListView.Sorting);
        }

        private void MyListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            MyListView.ListViewItemSorter = new ListViewComparer(e.Column, MyListView.Sorting);
        }

        public void Sort(int sortColumn)
        {
            MyListView.ListViewItemSorter = new ListViewComparer(sortColumn, MyListView.Sorting);
        }

        private void copyallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(ListViewToString(true, false), true);
        }

        private void copyselectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(ListViewToString(false, true), true);
        }

        private void MyContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            copyallToolStripMenuItem.Enabled = (MyListView.Items.Count > 0);
            this.copyselectionToolStripMenuItem.Enabled = (MyListView.SelectedItems.Count > 0);
        }

        private class ListViewComparer : IComparer
        {
            private Int32 MySortColumnIndex;
            private int MySortOrder;

            public ListViewComparer(Int32 sortIndex, SortOrder sortOrder)
                : base()
            {
                MySortColumnIndex = sortIndex;
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        MySortOrder = 1;
                        break;
                    case SortOrder.Descending:
                        MySortOrder = -1;
                        break;
                    case SortOrder.None:
                        MySortOrder = 0;
                        break;
                }
            }

            int IComparer.Compare(Object object1, Object object2)
            {
                ListViewItem listViewItem1, listViewItem2;

                listViewItem1 = (ListViewItem)object1;
                listViewItem2 = (ListViewItem)object2;
                return MySortOrder * PlattformOrdManData.CompareStringWithNumbers(
                    listViewItem1.SubItems[MySortColumnIndex].ToString(),
                    listViewItem2.SubItems[MySortColumnIndex].ToString());
            }
        }

    }
}
