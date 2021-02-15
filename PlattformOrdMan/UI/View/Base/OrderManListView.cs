using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.UI.View;

namespace PlattformOrdMan.UI.View.Base
{
    public partial class OrderManListView : ListView, IScrollableView
    {
        public event SortOrderSet OnSortOrderSet;

        protected const Int32 NO_COLUMN_INDEX = -1;
        private const Int16 WM_VSCROLL = 0x0115;

        private ArrayList MyColumnDataType;
        protected Int32 MyAddListViewItemsIndex;
        protected Int32 MySortColumnIndex;
        protected ListViewItem[] MyAddListViewItems;
        private Point MyLastMousePosition;
        private int MyChunkSize;
        private bool MyIsColumnSortEnabled;

        public OrderManListView()
        {
            InitializeComponent();
            this.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ChiasmaListView_ColumnClick);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.ContextMenuStrip = new ContextMenuStrip();
            this.ContextMenuStrip.ShowImageMargin = false;

            MyLastMousePosition = Control.MousePosition;
            MyAddListViewItems = null;
            MyAddListViewItemsIndex = 0;
            MyIsColumnSortEnabled = true;
            InitList();
            MyChunkSize = 0;
        }

        public bool EnableColumnSort
        {
            get
            {
                return MyIsColumnSortEnabled;
            }
            set
            {
                MyIsColumnSortEnabled = value;
            }
        }

        public void AddColumn(String columnName, Int32 columnWidth, ListDataType type)
        {
            this.Columns.Add(columnName, columnWidth);
            MyColumnDataType.Add(type);
        }      

        public void AddItem(ListViewItem listViewItem)
        {
            MyAddListViewItems[MyAddListViewItemsIndex++] = listViewItem;
        }

        public void BeginAddItems(Int32 itemCount)
        {
            MyAddListViewItems = new ListViewItem[itemCount];
            MyAddListViewItemsIndex = 0;
            BeginUpdate();
        }

        public virtual void BeginLoadItems(Int32 itemCount)
        {
            BeginAddItems(itemCount);
            Items.Clear();
        }

        public virtual void EndAddItems()
        {
            Items.AddRange(MyAddListViewItems);
            MyAddListViewItems = null;
            EndUpdate();
        }

        public virtual void BeginLoadChunk(int chunkSize)
        { 
            // Load list in steps in cases of long loading times,
            // prevent a blank screen during loading time. 
            Items.Clear();
            MyAddListViewItems = new ListViewItem[chunkSize];
            MyAddListViewItemsIndex = 0;
            MyChunkSize = chunkSize;
        }

        public void AddItemInChunk(ListViewItem listViewItem)
        {
            MyAddListViewItems[MyAddListViewItemsIndex++] = listViewItem;
            if (MyAddListViewItemsIndex > MyChunkSize - 1)
            {
                LoadChunk();
                this.Update();
            }
        }

        protected virtual void LoadChunk()
        {
            MyAddListViewItemsIndex = 0;
            BeginUpdate();
            Items.AddRange(MyAddListViewItems);
            EndUpdate();
            MyAddListViewItems = new ListViewItem[MyChunkSize];
        }

        public virtual void EndLoadChunk()
        {
            ListViewItem[] residue = new ListViewItem[MyAddListViewItemsIndex];
            for (int i = 0; i < MyAddListViewItemsIndex; i++)
            {
                residue[i] = MyAddListViewItems[i];
            }
            MyAddListViewItemsIndex = 0;
            BeginUpdate();
            Items.AddRange(residue);
            EndUpdate();
            MyAddListViewItems = null;
        }

        public virtual void EndLoadItems()
        {
            Items.AddRange(MyAddListViewItems);
            MyAddListViewItems = null;
            //SetColumnWidth(-2);
            EndUpdate();
        }

        public void SetColumnWidth(Int32 columnWidth)
        {
            // Set the column width to requested value.
            foreach (ColumnHeader columnHeader in Columns)
            {
                columnHeader.Width = columnWidth;
            }
        }

        private void ChiasmaListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (MyIsColumnSortEnabled)
            {
                SetSortingColumn(e.Column);
            }
        }

        public virtual void InitList()
        {
            MyColumnDataType = new ArrayList();
            MySortColumnIndex = NO_COLUMN_INDEX;
            this.Items.Clear();
        }

        protected Boolean IsNotEmpty(ICollection collection)
        {
            return ((collection != null) && (collection.Count > 0));
        }

        protected Boolean IsNotNull(Object testObject)
        {
            return (testObject != null);
        }

        protected Boolean IsNull(Object testObject)
        {
            return (testObject == null);
        }

        public Form GetForm()
        {
            Control parent;

            parent = this.Parent;
            while (IsNotNull(parent))
            {
                if (parent is Form)
                {
                    break;
                }
                parent = parent.Parent;
            }
            return (Form)parent;
        }

        public void ScrollDown()
        {
            Int32 borderHeight, scrollIndex;
            ListViewItem listViewItem;

            if ((Math.Abs(MyLastMousePosition.X - Control.MousePosition.X) > 3) ||
                 (Math.Abs(MyLastMousePosition.Y - Control.MousePosition.Y) > 3))
            {
                MyLastMousePosition = Control.MousePosition;

                // Retrieve the last visible list view item.
                borderHeight = 0;
                do
                {
                    listViewItem = GetItemAt(5, Height - borderHeight);
                    borderHeight += 5;
                } while (IsNull(listViewItem) && ((Height - borderHeight) > 0));

                if (IsNull(listViewItem))
                {
                    scrollIndex = 0;
                }
                else
                {
                    scrollIndex = listViewItem.Index;
                }
                if (scrollIndex < (Items.Count - 1))
                {
                    scrollIndex++;
                }
                EnsureVisible(scrollIndex);
            }
        }

        public void ScrollUp()
        {
            Int32 scrollIndex;
            ListViewItem listViewItem;

            if ((Math.Abs(MyLastMousePosition.X - Control.MousePosition.X) > 3) ||
                 (Math.Abs(MyLastMousePosition.Y - Control.MousePosition.Y) > 3))
            {
                MyLastMousePosition = Control.MousePosition;

                // Retrieve the first visible list view item.
                listViewItem = GetItemAt(5, 5);
                if (IsNull(listViewItem))
                {
                    scrollIndex = 0;
                }
                else
                {
                    scrollIndex = listViewItem.Index;
                }
                if (scrollIndex > 0)
                {
                    scrollIndex--;
                }
                EnsureVisible(scrollIndex);
            }
        }

        public virtual void ResetSortOrder()
        {
            this.ListViewItemSorter = null;
            MySortColumnIndex = NO_COLUMN_INDEX;
        }


        private void SetSortingColumn(int columnIndex)
        {
            if ((MySortColumnIndex == columnIndex) &&
                 IsNotNull(this.ListViewItemSorter) &&
                 columnIndex > NO_COLUMN_INDEX)
            {
                ((ListViewComparerChiasma)(this.ListViewItemSorter)).SwitchSortOrder();
                this.Sort();
            }
            else
            {
                if (MyColumnDataType.Count > columnIndex)
                {
                    switch ((ListDataType)(MyColumnDataType[columnIndex]))
                    {
                        case ListDataType.Currency:
                            this.ListViewItemSorter = new ListViewComparerCurrency(columnIndex);
                            break;
                        case ListDataType.Double:
                            this.ListViewItemSorter = new ListViewComparerDouble(columnIndex);
                            break;
                        case ListDataType.Int32:
                            this.ListViewItemSorter = new ListViewComparerInt32(columnIndex);
                            break;
                        case ListDataType.String:
                        default:
                            this.ListViewItemSorter = new ListViewComparerString(columnIndex);
                            break;
                    }
                }
                else
                {
                    // Default sorter is string.
                    this.ListViewItemSorter = new ListViewComparerString(columnIndex);
                }
            }
            MySortColumnIndex = columnIndex;
            if (OnSortOrderSet != null)
            {
                OnSortOrderSet(this, new EventArgs());
            }
        }
    }
}
