using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;

namespace PlattformOrdMan.UI.View.Post
{
    public class PostViewItem : ListViewItem
    {
        private Molmed.PlattformOrdMan.Data.Post _post;

        public PostViewItem(Molmed.PlattformOrdMan.Data.Post post)
            : base("")
        {
            _post = post;
            var columns = PostListView.GetColumns();
            Text = columns.First().GetString(post);
            columns.Skip(1).ToList().ForEach((c) => { SubItems.Add(c.GetString(post));});
            SetStatusColor();
        }

        public void ReloadPost(Molmed.PlattformOrdMan.Data.Post post)
        {
            _post = post;
            UpdateViewItem();
        }

        public void ReloadSupplier(Supplier supplier)
        {
            _post.ReloadSupplier(supplier);
            UpdateViewItem();
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            _post.ReloadMerchandise(merchandise);
            UpdateViewItem();
        }

        public Molmed.PlattformOrdMan.Data.Post GetPost()
        {
            return _post;
        }

        private void SetStatusColor()
        {
            if (_post.AttentionFlag)
            {
                ForeColor = Color.Black;
                BackColor = Color.Red;
                return;
            }
            if (!_post.IsMerchandiseEnabled())
            {
                ForeColor = Color.Red;
                ToolTipText = "This product is not up to date";
            }
            switch (_post.GetPostStatus())
            {
                case Molmed.PlattformOrdMan.Data.Post.PostStatus.Booked:
                    BackColor = Color.LightCoral;
                    ForeColor = Color.Black;
                    break;
                case Molmed.PlattformOrdMan.Data.Post.PostStatus.Ordered:
                    BackColor = Color.Yellow;
                    ForeColor = Color.Black;
                    break;
                case Molmed.PlattformOrdMan.Data.Post.PostStatus.ConfirmedOrder:
                    BackColor = Color.LightBlue;
                    ForeColor = Color.Black;
                    break;
                case Molmed.PlattformOrdMan.Data.Post.PostStatus.Confirmed:
                    BackColor = Color.Lime;
                    ForeColor = Color.Black;
                    break;
                case Molmed.PlattformOrdMan.Data.Post.PostStatus.Completed:
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                    break;
            }
        }

        public void UpdateViewItem()
        {
            var sort = PostListViewConfColumns.ColSortOrder + " asc";

            // Loop through columns in personal config datatable
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            for (int i = 0; i < rows.Length; i++)
            {
                var colName = (string)rows[i][PostListViewConfColumns.ColEnumName.ToString()];
                var col = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colName);
                
                SubItems[(int)rows[i][PostListViewConfColumns.ColSortOrder.ToString()]].Text = 
                    _post.GetStringForListViewColumn(col);
            }
            SetStatusColor();
        }
    }
}