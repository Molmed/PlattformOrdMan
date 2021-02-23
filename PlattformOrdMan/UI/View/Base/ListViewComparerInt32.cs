using System;
using System.Windows.Forms;

namespace PlattformOrdMan.UI.View.Base
{
    public class ListViewComparerInt32 : ListViewComparerChiasma
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
}