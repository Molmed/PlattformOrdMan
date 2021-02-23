using System.Windows.Forms;
using PlattformOrdMan.UI.View.Post;

namespace PlattformOrdMan.UI.Dialog.OptionsDialog
{
    public class IncludedColumnsListViewItem : ListViewItem
    {
        private readonly PostColumn _column;

        public IncludedColumnsListViewItem(PostColumn column)
            : base( column.GetHeader())
        {
            _column = column;
        }

        public string EnumName => _column.ColEnum.ToString();

        public int GetColumnWith()
        {
            return _column.GetColumnWith();
        }
    }
}