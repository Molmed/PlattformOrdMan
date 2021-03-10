using System;
using System.Windows.Forms;

namespace PlattformOrdMan.UI.Controller
{
    // Made for showing tool tips for list view items
    // In list view item:
    // set tool tip text for item
    // 
    // Preprations to list view:
    // ShowItemToolTips = false
    // ItemMouseHover += ...
    //void PostsListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
    //{
    //    if (IsNotEmpty(e.Item.ToolTipText))
    //    {
    //        MyToolTipHandler = new ToolTipHandler(this, e.Item.ToolTipText);
    //        MyToolTipHandler.StartTimer();
    //    }
    //}



    public class ToolTipHandler
    {
        private delegate void TimerElapsedEventHandler();

        private System.Timers.Timer MyActivityTimer;
        private int MyLastPosX;
        private int MyLastPosY;
        private Form MyForm;
        ToolTip MyToolTip;
        private string MyText;
        private int MyElapseInterval;
        private int MyPixelInterval;
        System.Timers.ElapsedEventHandler MyElapsedEventHandler;
        private TimerElapsedEventHandler MyTimerElapsedEventHandler;

        public ToolTipHandler(Form form, string text)
        {
            MyForm = form;
            MyText = text;
            MyElapsedEventHandler = new System.Timers.ElapsedEventHandler(MyActivityTimer_Elapsed);
            MyActivityTimer = new System.Timers.Timer();
            MyElapseInterval = 1000;
            MyActivityTimer.Interval = Convert.ToDouble(MyElapseInterval);
            MyActivityTimer.Enabled = false;
            MyTimerElapsedEventHandler += new TimerElapsedEventHandler(Timer_Elapsed);
            MyActivityTimer.Elapsed += MyElapsedEventHandler;
            MyToolTip = new ToolTip();
            MyPixelInterval = 3;
            MyLastPosX = -1;
            MyLastPosY = -1;
        }

        private void Timer_Elapsed()
        {
            try
            {
                if (MyLastPosX >= (Control.MousePosition.X + MyPixelInterval) || MyLastPosX <= (Control.MousePosition.X - MyPixelInterval) ||
                    MyLastPosY >= (Control.MousePosition.Y + MyPixelInterval) || MyLastPosY <= (Control.MousePosition.Y - MyPixelInterval))
                {
                    MyActivityTimer.Enabled = false;
                    MyToolTip.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidOperationException))
                {
                    throw ex;
                }
            }        
        }

        private void MyActivityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (MyTimerElapsedEventHandler != null)
                {
                    MyForm.Invoke(MyTimerElapsedEventHandler);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidOperationException))
                {
                    throw ex;
                }
            }
        }

        public void StartTimer()
        {
            MyToolTip.Show(MyText, MyForm, MyForm.PointToClient(Control.MousePosition).X,
                MyForm.PointToClient(Control.MousePosition).Y, 20000);
            MyLastPosX = Control.MousePosition.X;
            MyLastPosY = Control.MousePosition.Y;
            MyActivityTimer.Enabled = true;
        }


    }
}
