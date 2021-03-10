using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlattformOrdMan.UI.Component
{
    public partial class ToggleButton : Button
    {

        public delegate void ExpandEvent();

        public delegate void CollapseEvent();

        public event ExpandEvent SplitterExpanded;
        public event CollapseEvent SplitterCollapsed;
        private enum toggleState
        {
            Expanded,
            Collapsed
        }

        private toggleState _state;
        public ToggleButton()
        {
            InitializeComponent();
            Init();
        }

        public ToggleButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }
        private void Init()
        {
            Text = @"\/";
            _state = toggleState.Expanded;
            Click += OnClick;
        }

        public void OnClick(object sender, EventArgs e)
        {
            if (_state == toggleState.Expanded)
            {
                SwitchToCollapsed();
            }
            else
            {
                SwitchToExpanded();
            }
        }

        private void SwitchToCollapsed()
        {
            Text = @"/\";
            _state = toggleState.Collapsed;
            SplitterCollapsed?.Invoke();
        }

        private void SwitchToExpanded()
        {
            Text = @"\/";
            _state = toggleState.Expanded;
            SplitterExpanded?.Invoke();
        }
    }
}
