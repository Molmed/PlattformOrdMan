using System;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.View.Base
{
    public class ListViewComparerCurrency : ListViewComparerChiasma
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
}