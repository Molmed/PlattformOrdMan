using System;
using System.Windows.Forms;

namespace Molmed.PlattformOrdMan.UI.View
{
    public interface IScrollableView
    {
        Form GetForm();
        void ScrollDown();
        void ScrollUp();
    }
}
