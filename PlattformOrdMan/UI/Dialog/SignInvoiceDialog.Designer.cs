namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class SignInvoiceDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignInvoiceDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.InvoiceCategoryLabel = new System.Windows.Forms.Label();
            this.EditMerchandiseButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.InvoiceNotOkRadioButton = new System.Windows.Forms.RadioButton();
            this.InvoiceOKAndSentRadioButton = new System.Windows.Forms.RadioButton();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.InvoiceCategoryCodeLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NoInvoiceRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice category: ";
            // 
            // InvoiceCategoryLabel
            // 
            this.InvoiceCategoryLabel.AutoSize = true;
            this.InvoiceCategoryLabel.Location = new System.Drawing.Point(110, 18);
            this.InvoiceCategoryLabel.Name = "InvoiceCategoryLabel";
            this.InvoiceCategoryLabel.Size = new System.Drawing.Size(98, 13);
            this.InvoiceCategoryLabel.TabIndex = 1;
            this.InvoiceCategoryLabel.Text = "<Invoice category>";
            // 
            // EditMerchandiseButton
            // 
            this.EditMerchandiseButton.Location = new System.Drawing.Point(161, 58);
            this.EditMerchandiseButton.Name = "EditMerchandiseButton";
            this.EditMerchandiseButton.Size = new System.Drawing.Size(117, 24);
            this.EditMerchandiseButton.TabIndex = 2;
            this.EditMerchandiseButton.Text = "Edit Merchandise";
            this.EditMerchandiseButton.UseVisualStyleBackColor = true;
            this.EditMerchandiseButton.Click += new System.EventHandler(this.EditMerchandiseButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NoInvoiceRadioButton);
            this.panel1.Controls.Add(this.InvoiceNotOkRadioButton);
            this.panel1.Controls.Add(this.InvoiceOKAndSentRadioButton);
            this.panel1.Location = new System.Drawing.Point(15, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 78);
            this.panel1.TabIndex = 3;
            // 
            // InvoiceNotOkRadioButton
            // 
            this.InvoiceNotOkRadioButton.AutoSize = true;
            this.InvoiceNotOkRadioButton.Location = new System.Drawing.Point(3, 26);
            this.InvoiceNotOkRadioButton.Name = "InvoiceNotOkRadioButton";
            this.InvoiceNotOkRadioButton.Size = new System.Drawing.Size(114, 17);
            this.InvoiceNotOkRadioButton.TabIndex = 1;
            this.InvoiceNotOkRadioButton.Text = "Invoice is NOT OK";
            this.InvoiceNotOkRadioButton.UseVisualStyleBackColor = true;
            // 
            // InvoiceOKAndSentRadioButton
            // 
            this.InvoiceOKAndSentRadioButton.AutoSize = true;
            this.InvoiceOKAndSentRadioButton.Checked = true;
            this.InvoiceOKAndSentRadioButton.Location = new System.Drawing.Point(3, 3);
            this.InvoiceOKAndSentRadioButton.Name = "InvoiceOKAndSentRadioButton";
            this.InvoiceOKAndSentRadioButton.Size = new System.Drawing.Size(122, 17);
            this.InvoiceOKAndSentRadioButton.TabIndex = 0;
            this.InvoiceOKAndSentRadioButton.TabStop = true;
            this.InvoiceOKAndSentRadioButton.Text = "Invoice OK and sent";
            this.InvoiceOKAndSentRadioButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(12, 209);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(86, 24);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Sign";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(203, 209);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 24);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Invoice category code:";
            // 
            // InvoiceCategoryCodeLabel
            // 
            this.InvoiceCategoryCodeLabel.AutoSize = true;
            this.InvoiceCategoryCodeLabel.Location = new System.Drawing.Point(131, 31);
            this.InvoiceCategoryCodeLabel.Name = "InvoiceCategoryCodeLabel";
            this.InvoiceCategoryCodeLabel.Size = new System.Drawing.Size(125, 13);
            this.InvoiceCategoryCodeLabel.TabIndex = 7;
            this.InvoiceCategoryCodeLabel.Text = "<Invoice category code>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Change invoice category --->";
            // 
            // NoInvoiceRadioButton
            // 
            this.NoInvoiceRadioButton.AutoSize = true;
            this.NoInvoiceRadioButton.Location = new System.Drawing.Point(4, 49);
            this.NoInvoiceRadioButton.Name = "NoInvoiceRadioButton";
            this.NoInvoiceRadioButton.Size = new System.Drawing.Size(115, 17);
            this.NoInvoiceRadioButton.TabIndex = 2;
            this.NoInvoiceRadioButton.TabStop = true;
            this.NoInvoiceRadioButton.Text = "There is no invoice";
            this.NoInvoiceRadioButton.UseVisualStyleBackColor = true;
            // 
            // SignInvoiceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(300, 245);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InvoiceCategoryCodeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EditMerchandiseButton);
            this.Controls.Add(this.InvoiceCategoryLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SignInvoiceDialog";
            this.ShowInTaskbar = false;
            this.Text = "Sign invoice OK and sent";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InvoiceCategoryLabel;
        private System.Windows.Forms.Button EditMerchandiseButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton InvoiceNotOkRadioButton;
        private System.Windows.Forms.RadioButton InvoiceOKAndSentRadioButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label InvoiceCategoryCodeLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton NoInvoiceRadioButton;
    }
}