using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.UI.Controller;

namespace Molmed.PlattformOrdMan.UI.View
{
    public partial class OrderManListView : ListView, IScrollableView
    {
        public delegate void SortOrderSet(object sender, EventArgs e);
        public event SortOrderSet OnSortOrderSet;
        public enum ListDataType
        {
            DateTime,
            Double,
            Int32,
            String,
            Currency
        }

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

        private class ListViewComparerCurrency : ListViewComparerChiasma
        {
            private Int32 MySortColumnIndex;

            public ListViewComparerCurrency(Int32 sortColumnIndex)
                : base()
            {
                MySortColumnIndex = sortColumnIndex;
            }

            private bool GetPriceAndCurrency(String priceString, out String currencyString, out decimal price)
            {
                price = PlattformOrdManData.ParsePrice(priceString, out currencyString);

                if (price == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public override int Compare(Object object1, Object object2)
            {
                Boolean hasValue1, hasValue2;
                int compareValue = 0;
                String currencyString1 = "", currencyString2 = "", priceString1, priceString2;
                decimal price1 = 0, price2 = 0;
                ListViewItem listViewItem1, listViewItem2;

                listViewItem1 = (ListViewItem)object1;
                listViewItem2 = (ListViewItem)object2;
                priceString1 = listViewItem1.SubItems[MySortColumnIndex].Text.Trim();
                priceString2 = listViewItem2.SubItems[MySortColumnIndex].Text.Trim();
                hasValue1 = GetPriceAndCurrency(priceString1, out currencyString1, out price1);
                hasValue2 = GetPriceAndCurrency(priceString2, out currencyString2, out price2);
                if (hasValue1 && hasValue2)
                {
                    if (String.Compare(currencyString1, currencyString2) == 0)
                    {
                        compareValue = price1.CompareTo(price2);
                    }
                    else
                    {
                        compareValue = String.Compare(currencyString1, currencyString2);
                    }
                }
                else
                {
                    if (hasValue1)
                    {
                        compareValue = 1;
                    }
                    if (hasValue2)
                    {
                        compareValue = -1;
                    }
                    if (!hasValue1 && !hasValue2)
                    {
                        compareValue = 0;
                    }
                }
                return compareValue * GetSortOrder();
            }
        }

        private class ListViewComparerInt32 : ListViewComparerChiasma
        {
            private Int32 MySortColumnIndex;

            public ListViewComparerInt32(Int32 sortColumnIndex)
                : base()
            {
                MySortColumnIndex = sortColumnIndex;
            }

            public override int Compare(Object object1, Object object2)
            {
                Boolean hasValue1, hasValue2;
                int compareValue = 0;
                Int32 value1, value2;
                ListViewItem listViewItem1, listViewItem2;

                listViewItem1 = (ListViewItem)object1;
                listViewItem2 = (ListViewItem)object2;
                hasValue1 = Int32.TryParse(listViewItem1.SubItems[MySortColumnIndex].Text, out value1);
                hasValue2 = Int32.TryParse(listViewItem2.SubItems[MySortColumnIndex].Text, out value2);
                if (hasValue1 && hasValue2)
                {
                    compareValue = value1.CompareTo(value2);
                }
                else
                {
                    if (hasValue1)
                    {
                        compareValue = 1;
                    }
                    if (hasValue2)
                    {
                        compareValue = -1;
                    }
                    if (!hasValue1 && !hasValue2)
                    {
                        compareValue = 0;
                    }
                }
                return compareValue * GetSortOrder();
            }
        }

        private class ListViewComparerDouble : ListViewComparerChiasma
        {
            private Int32 MySortColumnIndex;

            public ListViewComparerDouble(Int32 sortColumnIndex)
                : base()
            {
                MySortColumnIndex = sortColumnIndex;
            }

            public override int Compare(Object object1, Object object2)
            {
                Boolean hasValue1, hasValue2;
                int compareValue = 0;
                ListViewItem listViewItem1, listViewItem2;
                Double value1, value2;

                listViewItem1 = (ListViewItem)object1;
                listViewItem2 = (ListViewItem)object2;
                hasValue1 = Double.TryParse(listViewItem1.SubItems[MySortColumnIndex].Text, out value1);
                hasValue2 = Double.TryParse(listViewItem2.SubItems[MySortColumnIndex].Text, out value2);
                if (hasValue1 && hasValue2)
                {
                    compareValue = value1.CompareTo(value2);
                }
                else
                {
                    if (hasValue1)
                    {
                        compareValue = 1;
                    }
                    if (hasValue2)
                    {
                        compareValue = -1;
                    }
                    if (!hasValue1 && !hasValue2)
                    {
                        compareValue = 0;
                    }
                }
                return compareValue * GetSortOrder();
            }
        }

        private class ListViewComparerString : ListViewComparerChiasma
        {
            private Int32 MySortColumnIndex;

            public ListViewComparerString(Int32 sortColumnIndex)
                : base()
            {
                MySortColumnIndex = sortColumnIndex;
            }

            public override int Compare(Object object1, Object object2)
            {
                int compareValue;
                ListViewItem listViewItem1, listViewItem2;

                listViewItem1 = (ListViewItem)object1;
                listViewItem2 = (ListViewItem)object2;
                compareValue = String.Compare(listViewItem1.SubItems[MySortColumnIndex].ToString(),
                                           listViewItem2.SubItems[MySortColumnIndex].ToString(), true);
                return compareValue * GetSortOrder();
            }
        }
        
        protected abstract class ListViewComparerChiasma : IComparer
        {
            private SortOrder MySortOrder;

            public ListViewComparerChiasma()
                : base()
            {
                MySortOrder = SortOrder.Ascending;
            }

            public abstract int Compare(Object object1, Object object2);

            protected Int32 GetSortOrder()
            {
                if (MySortOrder == SortOrder.Ascending)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            public virtual void SwitchSortOrder()
            {
                if (MySortOrder == SortOrder.Ascending)
                {
                    MySortOrder = SortOrder.Descending;
                }
                else
                {
                    MySortOrder = SortOrder.Ascending;
                }
            }
        }
    }

}
