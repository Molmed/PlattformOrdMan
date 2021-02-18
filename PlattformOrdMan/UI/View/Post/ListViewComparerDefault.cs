using Molmed.PlattformOrdMan.UI.View;
using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.View.Post
{
    public class ListViewComparerDefault : ListViewComparerChiasma
    {
        public override int Compare(object object1, object object2)
        {
            Data.PostData.Post post1 = null;

            var listViewItem1 = (PostViewItem)object1;
            var listViewItem2 = (PostViewItem)object2;

            if (listViewItem1 != null) post1 = listViewItem1.GetPost();
            if (listViewItem2 == null) return 0;
            var post2 = listViewItem2.GetPost();


            if (post1 != null && post2 != null)
            {
                if (post1 > post2)
                {
                    return 1;
                }
                else if (post1 == post2)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                // post1 and/or post2 is null
                if (post1 != null && post2 == null)
                {
                    return 1;
                }
                else if (post1 == null && post2 != null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}