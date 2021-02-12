using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Molmed.PlattformOrdMan.UI.View;

namespace Molmed.PlattformOrdMan.Data
{
    public class ColumnList
    {
        public void Get()
        {
            var sort = Configuration.PostListViewConfColumns.ColSortOrder + " ASC";
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select("", sort);
            PostListViewColumn postListViewColumn;
            foreach (DataRow row in rows)
            {
                var colEnumName = row[Configuration.PostListViewConfColumns.ColEnumName.ToString()].ToString();
                try
                {
                    postListViewColumn = (PostListViewColumn)Enum.Parse(typeof(PostListViewColumn), colEnumName);
                }
                catch (ArgumentException)
                {
                    //This happens when a column is removed from code but still exists in
                    //user's personal config file
                    continue;
                }
                var colHeader = PostListView.GetColumnHeaderName(postListViewColumn);
                var colWidth = (int)row[Configuration.PostListViewConfColumns.ColWidth.ToString()];
                var listDataType = PostListView.GetListDataType(postListViewColumn);
            }

        }
    }
}
