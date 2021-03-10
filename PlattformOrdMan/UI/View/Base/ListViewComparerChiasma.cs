using System;
using System.Collections;
using System.Windows.Forms;

namespace PlattformOrdMan.UI.View.Base
{
    public abstract class ListViewComparerChiasma : IComparer
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