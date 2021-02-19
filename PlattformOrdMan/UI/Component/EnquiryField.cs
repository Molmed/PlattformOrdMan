using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PlattformOrdMan.Data;

namespace PlattformOrdMan.UI.Component
{
    public partial class EnquiryField : UserControl
    {
        [Category("Action")]
        public event EventHandler EnquiryChanged;

        public EnquiryField()
        {
            InitializeComponent();
            ValueTextBox.GotFocus += RemoveText;
            ValueTextBox.LostFocus += AddPlaceholder;
            Load += AddPlaceholder;
        }

        public string Caption
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public void SetMarkColor(Color value)
        {
            YesRadioButton.ForeColor = value;
            NoRadioButton.ForeColor = value;
            groupBox1.ForeColor = value;
        }

        public string PlaceholderText { get; set; }

        private void RemoveText(object sender, EventArgs e)
        {
            ResetColors();
            if (ValueTextBox.Text == PlaceholderText)
            {
                ValueTextBox.Text = "";
                ValueTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            HandlePlaceholder();
        }
        public Enquiry GetEnquiry()
        {
            var hasAnswered = NoRadioButton.Checked || YesRadioButton.Checked;
            var hasValue = YesRadioButton.Checked;
            var text = ValueTextBox.Text == PlaceholderText ? "" : ValueTextBox.Text;
            return new Enquiry(hasAnswered, hasValue, text);
        }

        public void SetEnquiry(Enquiry value)
        {
            InitEnquiry(value);
        }

        private void InitEnquiry(Enquiry enquiry)
        {
            if (enquiry.HasAnswered)
            {
                YesRadioButton.Checked = enquiry.HasValue;
                NoRadioButton.Checked = !enquiry.HasValue;
                ValueTextBox.Text = enquiry.Value;
                ValueTextBox.ForeColor = SystemColors.WindowText;
            }
            HandlePlaceholder();
        }

        private void HandlePlaceholder()
        {
            if (string.IsNullOrEmpty(ValueTextBox.Text) && !NoRadioButton.Checked)
            {
                ValueTextBox.Text = PlaceholderText;
                ValueTextBox.ForeColor = SystemColors.ControlDark;

            }
            else if(ValueTextBox.Text != PlaceholderText)
            {
                ValueTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void NoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                ValueTextBox.Text = "";
                ValueTextBox.ReadOnly = true;
                ValueTextBox.Enabled = false;
                EnquiryChanged?.Invoke(sender, e);
                ResetColors();
            }
        }

        private void YesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                ValueTextBox.ReadOnly = false;
                ValueTextBox.Enabled = true;
                ActiveControl = ValueTextBox;
                EnquiryChanged?.Invoke(sender, e);
                HandlePlaceholder();
                ResetColors();
            }
        }

        private void ResetColors()
        {
            YesRadioButton.ResetForeColor();
            NoRadioButton.ResetForeColor();
            groupBox1.ResetForeColor();
        }
        private void ValueTextbox_Keydown(object sender, EventArgs e)
        {
            var cmp = PlaceholderText ?? "";
            if (ValueTextBox.Text == cmp)
            {
                ValueTextBox.Text = "";
                ValueTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void ValueTextbox_Keyup(object sender, EventArgs e)
        {
            EnquiryChanged?.Invoke(sender, e);
        }
    }
}
