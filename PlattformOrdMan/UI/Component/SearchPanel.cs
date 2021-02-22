using System.Windows.Forms;

namespace PlattformOrdMan.UI.Component
{
    public partial class SearchPanel : UserControl
    {
        public delegate void ExpandEvent();

        public delegate void CollapseEvent();

        public event ExpandEvent SearchboxExpanded;
        public event CollapseEvent SearchboxCollapsed;

        private readonly int _panel2OrigHeight;
        private readonly int _splitterDistance;

        public string Caption
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public SearchPanel()
        {
            InitializeComponent();
            _panel2OrigHeight = splitContainer1.Panel2.Height;
            _splitterDistance = splitContainer1.SplitterDistance;
            LinrPanel.Visible = false;
            toggleButton1.SplitterExpanded += OnSplitterExpanded;
            toggleButton1.SplitterCollapsed += OnSplitterCollapsed;
            toggleButton1.OnClick(null, null);
        }

        private void OnSplitterCollapsed()
        {
            splitContainer1.Panel2Collapsed = true;
            groupBox1.Height -= _panel2OrigHeight;
            splitContainer1.SplitterDistance = _splitterDistance;
            LinrPanel.Visible = true;
            SearchboxCollapsed?.Invoke();
        }

        private void OnSplitterExpanded()
        {
            splitContainer1.Panel2Collapsed = false;
            groupBox1.Height += _panel2OrigHeight;
            splitContainer1.SplitterDistance = _splitterDistance;
            LinrPanel.Visible = false;
            SearchboxExpanded?.Invoke();
        }

    }
}
