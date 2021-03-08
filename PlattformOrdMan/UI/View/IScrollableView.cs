using System.Windows.Forms;

namespace PlattformOrdMan.UI.View
{
    public interface IScrollableView
    {
        Form GetForm();
        void ScrollDown();
        void ScrollUp();
    }
}
