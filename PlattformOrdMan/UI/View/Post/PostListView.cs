using System;
using System.Collections.Generic;
using System.ComponentModel;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.View.Post
{
    public partial class PostListView : OrderManListView
    {
        private Dictionary<int, List<PostViewItem>> _supplierDict;
        private Dictionary<int, List<PostViewItem>> _prodDict;
        private Dictionary<int, PostViewItem> _postDict;
        public PostListView()
        {
            InitializeComponent();
            _supplierDict = new Dictionary<int, List<PostViewItem>>();
            _prodDict = new Dictionary<int, List<PostViewItem>>();
            _postDict = new Dictionary<int, PostViewItem>();
        }

        public PostListView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            _supplierDict = new Dictionary<int, List<PostViewItem>>();
            _prodDict = new Dictionary<int, List<PostViewItem>>();
            _postDict = new Dictionary<int, PostViewItem>();
        }

        public static List<PostColumn> GetExcudedColumns()
        {
            List<PostColumn> excluded = new List<PostColumn>();
            foreach (PostListViewColumn col in Enum.GetValues(typeof(PostListViewColumn)))
            {
                var expression = PostListViewConfColumns.ColEnumName + " = '" + col + "'";
                var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
                if (rows.Length == 0)
                {
                    var column = new PostColumn(col, -1);
                    excluded.Add(column);
                }
            }

            return excluded;
        }
        public static List<PostColumn> GetColumns()
        {
            var sort = PostListViewConfColumns.ColSortOrder + " asc";
            // Loop through columns in personal config datatable
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            PostListViewColumn postListViewColumn;
            List<PostColumn> ret = new List<PostColumn>();
            foreach (var row in rows)
            {
                var colName = (string)row[PostListViewConfColumns.ColEnumName.ToString()];
                var colWidth = (int)row[PostListViewConfColumns.ColWidth.ToString()];
                try
                {
                    postListViewColumn = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colName);

                }
                catch (ArgumentException)
                {

                    continue;
                }

                var postColumn = new PostColumn(postListViewColumn, colWidth);
                ret.Add(postColumn);
            }

            return ret;
        }

        public void AddColumns()
        {
            BeginUpdate();
            GetColumns().ForEach(AddColumn);
            EndUpdate();

        }

        private void AddColumn(PostColumn col)
        {
            AddColumn(col.GetHeader(), col.Width, col.GetListDataType());
        }

        public override void BeginLoadChunk(int chunkSize)
        {
            base.BeginLoadChunk(chunkSize);
            _supplierDict = new Dictionary<int, List<PostViewItem>>();
            _prodDict = new Dictionary<int, List<PostViewItem>>();
            _postDict = new Dictionary<int, PostViewItem>();
        }


        public void AddViewItem(Data.PostData.Post post)
        {
            PostViewItem pViewItem = new PostViewItem(post);
            BeginAddItems(1);
            AddItem(pViewItem);
            EndAddItems();
            Sort();
            pViewItem.Selected = true;
            EnsureVisible(pViewItem.Index);
            Select();
        }

        public void ReloadPost(Data.PostData.Post post)
        { 
            if(IsNotNull(post))
            {
                if (_postDict.ContainsKey(post.GetId()))
                {
                    _postDict[post.GetId()].ReloadPost(post);
                }
            }
        }

        public void ReloadSupplier(Supplier supplier)
        {
            if (IsNotNull(supplier) && _supplierDict.ContainsKey(supplier.GetId()))
            {
                foreach (PostViewItem viewItem in _supplierDict[supplier.GetId()])
                {
                    viewItem.ReloadSupplier(supplier);
                }
            }
        }

        public void ReloadMerchandise(Merchandise merchandise)
        {
            if (IsNotNull(merchandise) && _prodDict.ContainsKey(merchandise.GetId()))
            {
                foreach (PostViewItem viewItem in _prodDict[merchandise.GetId()])
                {
                    viewItem.ReloadMerchandise(merchandise);
                }
            }
        }

        private void UpdateDictionaries()
        {
            foreach (var listViewItem in MyAddListViewItems)
            {
                var viewItem = (PostViewItem) listViewItem;
                if (viewItem == null)
                {
                    continue;
                }
                var supplierId = viewItem.GetPost().GetSupplierId();
                if (_supplierDict.ContainsKey(supplierId))
                {
                    _supplierDict[supplierId].Add(viewItem);
                }
                else
                {
                    _supplierDict.Add(supplierId, new List<PostViewItem> { viewItem });
                }
                
                var merchId = viewItem.GetPost().GetMerchandiseId();
                if (_prodDict.ContainsKey(merchId))
                {
                    _prodDict[merchId].Add(viewItem);
                }
                else
                {
                    _prodDict.Add(merchId, new List<PostViewItem> { viewItem });
                }

                var postId = viewItem.GetPost().GetId();
                if (_postDict.ContainsKey(postId))
                {
                    _postDict[postId] = viewItem;
                }
                else
                {
                    _postDict.Add(postId, viewItem);
                }
            }        
        }

        public override void BeginLoadItems(int itemCount)
        {
            _prodDict = new Dictionary<int, List<PostViewItem>>();
            _supplierDict = new Dictionary<int, List<PostViewItem>>();
            _prodDict = new Dictionary<int, List<PostViewItem>>();
            base.BeginLoadItems(itemCount);
        }

        public override void EndAddItems()
        {
            UpdateDictionaries();
            base.EndAddItems();
        }

        public override void EndLoadItems()
        {
            UpdateDictionaries();
            base.EndLoadItems();
        }

        public override void EndLoadChunk()
        {
            UpdateDictionaries();
            base.EndLoadChunk();
        }

        public override void InitList()
        {
            base.InitList();
            ListViewItemSorter = new ListViewComparerDefault();
        }

        public override void ResetSortOrder()
        {
            ListViewItemSorter = new ListViewComparerDefault();
            MySortColumnIndex = NO_COLUMN_INDEX;
        }
    }
}
