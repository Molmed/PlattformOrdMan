namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class EditMerchandiseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditMerchandiseDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DissableCheckBox = new System.Windows.Forms.CheckBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.PrizeTextBox = new System.Windows.Forms.TextBox();
            this.CategoryTextBox = new System.Windows.Forms.TextBox();
            this.StorageTextBox = new System.Windows.Forms.TextBox();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.invoiceCategoryCombobox1 = new Molmed.PlattformOrdMan.UI.Component.InvoiceCategoryCombobox();
            this.label10 = new System.Windows.Forms.Label();
            this.InvoiceCategoryNumberTextBox = new System.Windows.Forms.TextBox();
            this.EditInvoiceCategoryButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.EditCurrencyButton = new System.Windows.Forms.Button();
            this.CurrencyCombobox = new Molmed.PlattformOrdMan.UI.Component.CurrencyCombobox();
            this.SupplierCombobox = new Molmed.PlattformOrdMan.UI.Component.SupplierCombobox();
            this.ArticleNumberComboBox = new System.Windows.Forms.ComboBox();
            this.MakeOrderButton = new System.Windows.Forms.Button();
            this.ShowSupplierButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Amount in unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Supplier";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Approximate prize";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Category";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Storage";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Comment";
            // 
            // DissableCheckBox
            // 
            this.DissableCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DissableCheckBox.AutoSize = true;
            this.DissableCheckBox.Location = new System.Drawing.Point(39, 452);
            this.DissableCheckBox.Name = "DissableCheckBox";
            this.DissableCheckBox.Size = new System.Drawing.Size(124, 17);
            this.DissableCheckBox.TabIndex = 7;
            this.DissableCheckBox.Text = "Dissable this product";
            this.DissableCheckBox.UseVisualStyleBackColor = true;
            this.DissableCheckBox.CheckedChanged += new System.EventHandler(this.DissableCheckBox_CheckedChanged);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(169, 30);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(544, 20);
            this.NameTextBox.TabIndex = 8;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AmountTextBox.Location = new System.Drawing.Point(169, 56);
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new System.Drawing.Size(638, 20);
            this.AmountTextBox.TabIndex = 9;
            this.AmountTextBox.TextChanged += new System.EventHandler(this.AmountTextBox_TextChanged);
            // 
            // PrizeTextBox
            // 
            this.PrizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrizeTextBox.Location = new System.Drawing.Point(169, 135);
            this.PrizeTextBox.Name = "PrizeTextBox";
            this.PrizeTextBox.Size = new System.Drawing.Size(638, 20);
            this.PrizeTextBox.TabIndex = 11;
            this.PrizeTextBox.TextChanged += new System.EventHandler(this.PrizeTextBox_TextChanged);
            // 
            // CategoryTextBox
            // 
            this.CategoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoryTextBox.Location = new System.Drawing.Point(169, 188);
            this.CategoryTextBox.Name = "CategoryTextBox";
            this.CategoryTextBox.Size = new System.Drawing.Size(638, 20);
            this.CategoryTextBox.TabIndex = 12;
            this.CategoryTextBox.TextChanged += new System.EventHandler(this.CategoryTextBox_TextChanged);
            // 
            // StorageTextBox
            // 
            this.StorageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StorageTextBox.Location = new System.Drawing.Point(169, 239);
            this.StorageTextBox.Multiline = true;
            this.StorageTextBox.Name = "StorageTextBox";
            this.StorageTextBox.Size = new System.Drawing.Size(638, 65);
            this.StorageTextBox.TabIndex = 13;
            this.StorageTextBox.TextChanged += new System.EventHandler(this.StorageTextBox_TextChanged);
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentTextBox.Location = new System.Drawing.Point(169, 310);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(638, 159);
            this.CommentTextBox.TabIndex = 14;
            this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentTextBox_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(37, 501);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(86, 24);
            this.SaveButton.TabIndex = 15;
            this.SaveButton.Text = "Update";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(721, 501);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(86, 24);
            this.CloseButton.TabIndex = 16;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Article number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Invoice category";
            // 
            // invoiceCategoryCombobox1
            // 
            this.invoiceCategoryCombobox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.invoiceCategoryCombobox1.FormattingEnabled = true;
            this.invoiceCategoryCombobox1.Location = new System.Drawing.Point(169, 212);
            this.invoiceCategoryCombobox1.Name = "invoiceCategoryCombobox1";
            this.invoiceCategoryCombobox1.Size = new System.Drawing.Size(331, 21);
            this.invoiceCategoryCombobox1.TabIndex = 21;
            this.invoiceCategoryCombobox1.SelectedIndexChanged += new System.EventHandler(this.invoiceCategoryCombobox1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(506, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Invoice category number";
            // 
            // InvoiceCategoryNumberTextBox
            // 
            this.InvoiceCategoryNumberTextBox.Location = new System.Drawing.Point(636, 212);
            this.InvoiceCategoryNumberTextBox.Name = "InvoiceCategoryNumberTextBox";
            this.InvoiceCategoryNumberTextBox.ReadOnly = true;
            this.InvoiceCategoryNumberTextBox.Size = new System.Drawing.Size(79, 20);
            this.InvoiceCategoryNumberTextBox.TabIndex = 23;
            // 
            // EditInvoiceCategoryButton
            // 
            this.EditInvoiceCategoryButton.Location = new System.Drawing.Point(721, 212);
            this.EditInvoiceCategoryButton.Name = "EditInvoiceCategoryButton";
            this.EditInvoiceCategoryButton.Size = new System.Drawing.Size(86, 20);
            this.EditInvoiceCategoryButton.TabIndex = 24;
            this.EditInvoiceCategoryButton.Text = "Add/Edit";
            this.EditInvoiceCategoryButton.UseVisualStyleBackColor = true;
            this.EditInvoiceCategoryButton.Click += new System.EventHandler(this.EditInvoiceCategoryButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(38, 164);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Currency";
            // 
            // EditCurrencyButton
            // 
            this.EditCurrencyButton.Location = new System.Drawing.Point(359, 159);
            this.EditCurrencyButton.Name = "EditCurrencyButton";
            this.EditCurrencyButton.Size = new System.Drawing.Size(75, 23);
            this.EditCurrencyButton.TabIndex = 27;
            this.EditCurrencyButton.Text = "Edit/Add";
            this.EditCurrencyButton.UseVisualStyleBackColor = true;
            this.EditCurrencyButton.Click += new System.EventHandler(this.EditCurrencyButton_Click);
            // 
            // CurrencyCombobox
            // 
            this.CurrencyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrencyCombobox.FormattingEnabled = true;
            this.CurrencyCombobox.Location = new System.Drawing.Point(169, 161);
            this.CurrencyCombobox.Name = "CurrencyCombobox";
            this.CurrencyCombobox.Size = new System.Drawing.Size(184, 21);
            this.CurrencyCombobox.TabIndex = 29;
            // 
            // SupplierCombobox
            // 
            this.SupplierCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SupplierCombobox.FormattingEnabled = true;
            this.SupplierCombobox.Location = new System.Drawing.Point(169, 82);
            this.SupplierCombobox.Name = "SupplierCombobox";
            this.SupplierCombobox.Size = new System.Drawing.Size(607, 21);
            this.SupplierCombobox.TabIndex = 30;
            // 
            // ArticleNumberComboBox
            // 
            this.ArticleNumberComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArticleNumberComboBox.FormattingEnabled = true;
            this.ArticleNumberComboBox.Location = new System.Drawing.Point(169, 109);
            this.ArticleNumberComboBox.Name = "ArticleNumberComboBox";
            this.ArticleNumberComboBox.Size = new System.Drawing.Size(638, 21);
            this.ArticleNumberComboBox.TabIndex = 31;
            this.ArticleNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.ArticleNumberComboBox_SelectedIndexChanged);
            // 
            // MakeOrderButton
            // 
            this.MakeOrderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MakeOrderButton.Location = new System.Drawing.Point(719, 28);
            this.MakeOrderButton.Name = "MakeOrderButton";
            this.MakeOrderButton.Size = new System.Drawing.Size(88, 23);
            this.MakeOrderButton.TabIndex = 32;
            this.MakeOrderButton.Text = "Make order ...";
            this.MakeOrderButton.UseVisualStyleBackColor = true;
            this.MakeOrderButton.Click += new System.EventHandler(this.MakeOrderButton_Click);
            // 
            // ShowSupplierButton
            // 
            this.ShowSupplierButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowSupplierButton.Location = new System.Drawing.Point(782, 80);
            this.ShowSupplierButton.Name = "ShowSupplierButton";
            this.ShowSupplierButton.Size = new System.Drawing.Size(25, 23);
            this.ShowSupplierButton.TabIndex = 33;
            this.ShowSupplierButton.Text = "...";
            this.ShowSupplierButton.UseVisualStyleBackColor = true;
            this.ShowSupplierButton.Click += new System.EventHandler(this.ShowSupplierButton_Click);
            // 
            // EditMerchandiseDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(819, 540);
            this.Controls.Add(this.ShowSupplierButton);
            this.Controls.Add(this.MakeOrderButton);
            this.Controls.Add(this.ArticleNumberComboBox);
            this.Controls.Add(this.SupplierCombobox);
            this.Controls.Add(this.CurrencyCombobox);
            this.Controls.Add(this.EditCurrencyButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.EditInvoiceCategoryButton);
            this.Controls.Add(this.InvoiceCategoryNumberTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.invoiceCategoryCombobox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.StorageTextBox);
            this.Controls.Add(this.CategoryTextBox);
            this.Controls.Add(this.PrizeTextBox);
            this.Controls.Add(this.AmountTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.DissableCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditMerchandiseDialog";
            this.ShowInTaskbar = false;
            this.Text = "Update product";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox DissableCheckBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox AmountTextBox;
        private System.Windows.Forms.TextBox PrizeTextBox;
        private System.Windows.Forms.TextBox CategoryTextBox;
        private System.Windows.Forms.TextBox StorageTextBox;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Molmed.PlattformOrdMan.UI.Component.InvoiceCategoryCombobox invoiceCategoryCombobox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox InvoiceCategoryNumberTextBox;
        private System.Windows.Forms.Button EditInvoiceCategoryButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button EditCurrencyButton;
        private Molmed.PlattformOrdMan.UI.Component.CurrencyCombobox CurrencyCombobox;
        private Molmed.PlattformOrdMan.UI.Component.SupplierCombobox SupplierCombobox;
        private System.Windows.Forms.ComboBox ArticleNumberComboBox;
        private System.Windows.Forms.Button MakeOrderButton;
        private System.Windows.Forms.Button ShowSupplierButton;
    }
}