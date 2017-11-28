﻿using System;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.UI.Controller
{
    public delegate void BarCodeEventHandler(String barCode);

    public class BarCodeController : PlattformOrdManData
    {
        private String _barCodeString;
        private Boolean _barCodeFlag;
        private DateTime _barCodeReadTime;
        private readonly System.Timers.Timer _activityTimer;

        public event BarCodeEventHandler BarCodeReceived;

        public BarCodeController(Form form)
        {
            _barCodeFlag = false;
            _barCodeString = null;
            form.KeyPreview = true;
            form.KeyDown += Form_KeyDown;
            QuitAtInternalBarcodeLength = true;
            _activityTimer = new System.Timers.Timer {Interval = 200};
            _activityTimer.Elapsed += ActivityTimer_Elapsed;
            _activityTimer.Enabled = false;

        }

        private bool QuitAtInternalBarcodeLength { get; }

        private void ActivityTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            BarCodeReceived?.Invoke(_barCodeString);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (_barCodeFlag)
            {
                // Check how long it was since the bar code reading began.
                var elapsedTime = DateTime.Now.Subtract(_barCodeReadTime);
                // If it was more than two secondes ago, it is probably a manual input
                // and should not be regarded as a bar code reading.

                if (elapsedTime.Seconds > Settings.Default.BarCodeMaxTimeToRead)
                {
                    _barCodeFlag = false;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    if (!_barCodeFlag)
                    {
                        // Start saving digits.
                        _barCodeFlag = true;
                        _barCodeReadTime = DateTime.Now;
                        _barCodeString = "";
                    }
                    // Add digit.
                    _barCodeString += e.KeyCode.ToString().Substring(1);

                    if (QuitAtInternalBarcodeLength &&
                        _barCodeString.Length == Settings.Default.BarCodeLengthInternal &&
                        IsNotNull(BarCodeReceived))
                    {
                        // Make a break to read the last termination character (if any) before barcode-received event
                        //BarCodeReceived(MyBarCodeString);
                        _activityTimer.Enabled = true;
                    }

                    break;

                default:
                    _barCodeFlag = false;
                    break;
            }
        }
    }
}
