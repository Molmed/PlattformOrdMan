using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.UI.Controller;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    public partial class LoginWithBarcodeDialog : OrdManForm
    {
        private delegate void BarcodeReceivedCallback(string barcode);

        private string MyBarcode;
        private int MyShrinkDistance;

        public LoginWithBarcodeDialog()
        {
            MyShrinkDistance = -1;
            InitializeComponent();
            MyBarcode = "";
            Init();
        }

        private void Init()
        {
            Point locA, locB;
            BarCodeController barCodeController;
            BarcodeTextBox.Select();
            //BarcodeCatcherTextBox.Width = 0;
            barCodeController = new BarCodeController(this);
            barCodeController.BarCodeReceived += new BarCodeEventHandler(BarCodeReceived);
            locA = ManualCheckBox.Location;
            locB = BarcodeTextBox.Location;
            MyShrinkDistance = (locB.Y + BarcodeTextBox.Height) - (locA.Y + ManualCheckBox.Height);
            MakeManualLoginInvisible();
        }

        private void MakeManualLoginInvisible()
        {
            BarcodeLabel.Visible = false;
            BarcodeTextBox.Visible = false;
            MyOkButton.Visible = false;
            this.AcceptButton = null;
            this.Height -= MyShrinkDistance;
            ManualCheckBox.Select();
        }

        private void MakeManualLoginVisible()
        {
            BarcodeLabel.Visible = true;
            BarcodeTextBox.Visible = true;
            MyOkButton.Visible = true;
            this.AcceptButton = MyOkButton;
            this.Height += MyShrinkDistance;
            BarcodeTextBox.Select();
        }

        private void BarCodeReceived(string barcode)
        {
            if (InvokeRequired)
            {
                BarcodeReceivedCallback c = new BarcodeReceivedCallback(BarCodeReceived);
                Invoke(c, barcode);
            }
            else
            {
                MyBarcode = barcode;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        public string Barcode
        {
            get
            {
                return MyBarcode;
            }
        }

        private void MyOkButton_Click(object sender, EventArgs e)
        {
            if (IsNotEmpty(BarcodeTextBox.Text.Trim()))
            {
                MyBarcode = BarcodeTextBox.Text.Trim();
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
