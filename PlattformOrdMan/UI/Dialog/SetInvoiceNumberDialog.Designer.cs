namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class SetInvoiceNumberDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetInvoiceNumberDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.InvoiceNumberTextBox = new System.Windows.Forms.TextBox();
            this.NoInvoiceCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoice number:";
            // 
            // InvoiceNumberTextBox
            // 
            this.InvoiceNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InvoiceNumberTextBox.Location = new System.Drawing.Point(12, 60);
            this.InvoiceNumberTextBox.Name = "InvoiceNumberTextBox";
            this.InvoiceNumberTextBox.Size = new System.Drawing.Size(335, 20);
            this.InvoiceNumberTextBox.TabIndex = 1;
            // 
            // NoInvoiceCheckBox
            // 
            this.NoInvoiceCheckBox.AutoSize = true;
            this.NoInvoiceCheckBox.Location = new System.Drawing.Point(15, 86);
            this.NoInvoiceCheckBox.Name = "NoInvoiceCheckBox";
            this.NoInvoiceCheckBox.Size = new System.Drawing.Size(77, 17);
            this.NoInvoiceCheckBox.TabIndex = 4;
            this.NoInvoiceCheckBox.Text = "No invoice";
            this.NoInvoiceCheckBox.UseVisualStyleBackColor = true;
            this.NoInvoiceCheckBox.CheckedChanged += new System.EventHandler(this.NoInvoiceCheckBox_CheckedChanged);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.Location = new System.Drawing.Point(12, 127);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(272, 127);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 6;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SetInvoiceNumberDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 162);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.NoInvoiceCheckBox);
            this.Controls.Add(this.InvoiceNumberTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetInvoiceNumberDialog";
            this.Text = "SetInvoiceNumberDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InvoiceNumberTextBox;
        private System.Windows.Forms.CheckBox NoInvoiceCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}