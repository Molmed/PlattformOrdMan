using System;
using Molmed.PlattformOrdMan.UI.Controller;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class LoginWithBarcodeDialog : OrdManForm
    {
        private delegate void BarcodeReceivedCallback(string barcode);

        private string _barcode;
        private int _shrinkDistance;

        public LoginWithBarcodeDialog()
        {
            _shrinkDistance = -1;
            InitializeComponent();
            _barcode = "";
            Init();
        }

        private void Init()
        {
            BarcodeTextBox.Select();
            //BarcodeCatcherTextBox.Width = 0;
            var barCodeController = new BarCodeController(this);
            barCodeController.BarCodeReceived += BarCodeReceived;
            var locA = ManualCheckBox.Location;
            var locB = BarcodeTextBox.Location;
            _shrinkDistance = (locB.Y + BarcodeTextBox.Height) - (locA.Y + ManualCheckBox.Height);
            MakeManualLoginInvisible();
        }

        private void MakeManualLoginInvisible()
        {
            BarcodeLabel.Visible = false;
            BarcodeTextBox.Visible = false;
            MyOkButton.Visible = false;
            AcceptButton = null;
            Height -= _shrinkDistance;
            ManualCheckBox.Select();
        }

        private void MakeManualLoginVisible()
        {
            BarcodeLabel.Visible = true;
            BarcodeTextBox.Visible = true;
            MyOkButton.Visible = true;
            AcceptButton = MyOkButton;
            Height += _shrinkDistance;
            BarcodeTextBox.Select();
        }

        private void BarCodeReceived(string barcode)
        {
            if (InvokeRequired)
            {
                BarcodeReceivedCallback c = BarCodeReceived;
                Invoke(c, barcode);
            }
            else
            {
                _barcode = barcode;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        public string Barcode => _barcode;

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty(BarcodeTextBox.Text.Trim()))
            {
                _barcode = BarcodeTextBox.Text.Trim();
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void MyCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ManualCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualCheckBox.Checked)
            {
                MakeManualLoginVisible();
            }
            else
            {
                MakeManualLoginInvisible();
            }
        }

        private void BarcodeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
