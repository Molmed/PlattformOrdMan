﻿using PlattformOrdMan.UI.Component;

namespace PlattformOrdMan.UI.Dialog
{
    partial class SignOrderDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.periodizationField1 = new PeriodizationField();
            this.accountField1 = new AccountField();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.Location = new System.Drawing.Point(12, 163);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(391, 163);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // periodizationField1
            // 
            this.periodizationField1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.periodizationField1.Caption = "Periodization";
            this.periodizationField1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.periodizationField1.Location = new System.Drawing.Point(12, 62);
            this.periodizationField1.Name = "periodizationField1";
            this.periodizationField1.PlaceholderText = null;
            this.periodizationField1.Size = new System.Drawing.Size(454, 44);
            this.periodizationField1.TabIndex = 3;
            // 
            // accountField1
            // 
            this.accountField1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accountField1.Caption = "Account to be used for payment";
            this.accountField1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.accountField1.Location = new System.Drawing.Point(12, 12);
            this.accountField1.Name = "accountField1";
            this.accountField1.PlaceholderText = "Fill in if other than SNP&SEQ standard operational";
            this.accountField1.Size = new System.Drawing.Size(454, 44);
            this.accountField1.TabIndex = 2;
            // 
            // SignOrderDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(478, 198);
            this.Controls.Add(this.periodizationField1);
            this.Controls.Add(this.accountField1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Name = "SignOrderDialog";
            this.Text = "SignOrderDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private AccountField accountField1;
        private PeriodizationField periodizationField1;
    }
}