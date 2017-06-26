namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class InvoiceCategoryDialog
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
            this.invoiceCategoryCombobox1 = new Molmed.PlattformOrdMan.UI.Component.InvoiceCategoryCombobox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.EditCategoryButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // invoiceCategoryCombobox1
            // 
            this.invoiceCategoryCombobox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.invoiceCategoryCombobox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.invoiceCategoryCombobox1.FormattingEnabled = true;
            this.invoiceCategoryCombobox1.Location = new System.Drawing.Point(12, 85);
            this.invoiceCategoryCombobox1.Name = "invoiceCategoryCombobox1";
            this.invoiceCategoryCombobox1.Size = new System.Drawing.Size(246, 21);
            this.invoiceCategoryCombobox1.TabIndex = 0;
            this.invoiceCategoryCombobox1.SelectedIndexChanged += new System.EventHandler(this.invoiceCategoryCombobox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(310, 42);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Select invoice category for the product, or press cancel and choose \"no invoice\" " +
                "in the update post form.\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Invoice category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Code:";
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Location = new System.Drawing.Point(49, 112);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ReadOnly = true;
            this.CodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.CodeTextBox.TabIndex = 4;
            // 
            // EditCategoryButton
            // 
            this.EditCategoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCategoryButton.Location = new System.Drawing.Point(264, 82);
            this.EditCategoryButton.Name = "EditCategoryButton";
            this.EditCategoryButton.Size = new System.Drawing.Size(86, 24);
            this.EditCategoryButton.TabIndex = 5;
            this.EditCategoryButton.Text = "Add/Edit";
            this.EditCategoryButton.UseVisualStyleBackColor = true;
            this.EditCategoryButton.Click += new System.EventHandler(this.EditCategoryButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkButton.Location = new System.Drawing.Point(9, 151);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(86, 24);
            this.OkButton.TabIndex = 6;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(264, 151);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(86, 24);
            this.MyCancelButton.TabIndex = 7;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // InvoiceCategoryDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(364, 189);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.EditCategoryButton);
            this.Controls.Add(this.CodeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.invoiceCategoryCombobox1);
            this.Name = "InvoiceCategoryDialog";
            this.Text = "Select invoice category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Molmed.PlattformOrdMan.UI.Component.InvoiceCategoryCombobox invoiceCategoryCombobox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Button EditCategoryButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}