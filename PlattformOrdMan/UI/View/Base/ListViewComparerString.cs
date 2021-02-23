using System;
using System.Windows.Forms;

namespace PlattformOrdMan.UI.View.Base
{
    public class ListViewComparerString : ListViewComparerChiasma
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
}