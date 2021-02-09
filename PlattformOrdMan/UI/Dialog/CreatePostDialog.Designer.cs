using System;

namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class CreatePostDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePostDialog));
            this.SaveButton = new System.Windows.Forms.Button();
            this.MyCloseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.ApprPrizeTextBox = new System.Windows.Forms.TextBox();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InvoiceInstCheckBox = new System.Windows.Forms.CheckBox();
            this.InvoiceClinCheckBox = new System.Windows.Forms.CheckBox();
            this.ApprArrivalLabel = new System.Windows.Forms.Label();
            this.ApprArrivalTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MerchandiseCommentTextBox = new System.Windows.Forms.TextBox();
            this.NoInvoiceCheckBox = new System.Windows.Forms.CheckBox();
            this.SupplierComboBox = new Molmed.PlattformOrdMan.UI.Component.SupplierCombobox();
            this.merchandiseCombobox1 = new Molmed.PlattformOrdMan.UI.Component.MerchandiseCombobox();
            this.label7 = new System.Windows.Forms.Label();
            this.CurrencyCombobox = new Molmed.PlattformOrdMan.UI.Component.CurrencyCombobox();
            this.EditCurrencyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AttentionCheckBox = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ConfirmedOrderDateTextBox = new System.Windows.Forms.TextBox();
            this.ConfirmedOrderUserComboBox = new Molmed.PlattformOrdMan.UI.Component.UserComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.InvoiceCheckDateTextBox = new System.Windows.Forms.TextBox();
            this.InvoiceCheckerUserComboBox = new Molmed.PlattformOrdMan.UI.Component.UserComboBox();
            this.ArrivalDateTextBox = new System.Windows.Forms.TextBox();
            this.ArrivalSignUserComboBox = new Molmed.PlattformOrdMan.UI.Component.UserComboBox();
            this.OrderDateTextBox = new System.Windows.Forms.TextBox();
            this.OrdererUserComboBox = new Molmed.PlattformOrdMan.UI.Component.UserComboBox();
            this.BookDateTextBox = new System.Windows.Forms.TextBox();
            this.BookerUserComboBox = new Molmed.PlattformOrdMan.UI.Component.UserComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.FinalPrizeTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.InvoiceNumberTextBox = new System.Windows.Forms.TextBox();
            this.DeliveryDeviationTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.StorageTextBox = new System.Windows.Forms.TextBox();
            this.ShowSupplierButton = new System.Windows.Forms.Button();
            this.ShowProductButton = new System.Windows.Forms.Button();
            this.ArticleNumberTextBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.TotalPrizeTextBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.PurchaseOrderNoTextBox = new System.Windows.Forms.TextBox();
            this.SalesOrdernoTextBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.OrderingUnitComboBox = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.CustomerNumberComboBox = new System.Windows.Forms.ComboBox();
            this.Periodization = new Molmed.PlattformOrdMan.UI.Component.EnquiryField();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(12, 816);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(86, 24);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Create";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MyCloseButton
            // 
            this.MyCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCloseButton.Location = new System.Drawing.Point(879, 816);
            this.MyCloseButton.Name = "MyCloseButton";
            this.MyCloseButton.Size = new System.Drawing.Size(86, 24);
            this.MyCloseButton.TabIndex = 1;
            this.MyCloseButton.Text = "Cancel";
            this.MyCloseButton.UseVisualStyleBackColor = true;
            this.MyCloseButton.Click += new System.EventHandler(this.MyCloseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Supplier:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Product:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Amount";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Appr. unit prize";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Location = new System.Drawing.Point(9, 709);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(51, 13);
            this.CommentLabel.TabIndex = 8;
            this.CommentLabel.Text = "Comment";
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AmountTextBox.Location = new System.Drawing.Point(129, 279);
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new System.Drawing.Size(443, 20);
            this.AmountTextBox.TabIndex = 9;
            this.AmountTextBox.TextChanged += new System.EventHandler(this.AmountTextBox_TextChanged);
            // 
            // ApprPrizeTextBox
            // 
            this.ApprPrizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApprPrizeTextBox.Location = new System.Drawing.Point(129, 305);
            this.ApprPrizeTextBox.Name = "ApprPrizeTextBox";
            this.ApprPrizeTextBox.ReadOnly = true;
            this.ApprPrizeTextBox.Size = new System.Drawing.Size(443, 20);
            this.ApprPrizeTextBox.TabIndex = 10;
            this.ApprPrizeTextBox.TextChanged += new System.EventHandler(this.ApprPrizeTextBox_TextChanged);
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentTextBox.Location = new System.Drawing.Point(9, 725);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(950, 74);
            this.CommentTextBox.TabIndex = 11;
            this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 252);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Article number";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // InvoiceInstCheckBox
            // 
            this.InvoiceInstCheckBox.AutoSize = true;
            this.InvoiceInstCheckBox.Location = new System.Drawing.Point(228, 304);
            this.InvoiceInstCheckBox.Name = "InvoiceInstCheckBox";
            this.InvoiceInstCheckBox.Size = new System.Drawing.Size(84, 17);
            this.InvoiceInstCheckBox.TabIndex = 14;
            this.InvoiceInstCheckBox.Text = "Invoice Inst.";
            this.InvoiceInstCheckBox.UseVisualStyleBackColor = true;
            this.InvoiceInstCheckBox.CheckedChanged += new System.EventHandler(this.InvoiceInstCheckBox_CheckedChanged);
            // 
            // InvoiceClinCheckBox
            // 
            this.InvoiceClinCheckBox.AutoSize = true;
            this.InvoiceClinCheckBox.Location = new System.Drawing.Point(228, 324);
            this.InvoiceClinCheckBox.Name = "InvoiceClinCheckBox";
            this.InvoiceClinCheckBox.Size = new System.Drawing.Size(84, 17);
            this.InvoiceClinCheckBox.TabIndex = 15;
            this.InvoiceClinCheckBox.Text = "Invoice Clin.";
            this.InvoiceClinCheckBox.UseVisualStyleBackColor = true;
            this.InvoiceClinCheckBox.CheckedChanged += new System.EventHandler(this.InvoiceClinCheckBox_CheckedChanged);
            // 
            // ApprArrivalLabel
            // 
            this.ApprArrivalLabel.AutoSize = true;
            this.ApprArrivalLabel.Location = new System.Drawing.Point(9, 466);
            this.ApprArrivalLabel.Name = "ApprArrivalLabel";
            this.ApprArrivalLabel.Size = new System.Drawing.Size(64, 13);
            this.ApprArrivalLabel.TabIndex = 16;
            this.ApprArrivalLabel.Text = "Appr. Arrival";
            this.ApprArrivalLabel.Click += new System.EventHandler(this.ApprArrivalLabel_Click);
            // 
            // ApprArrivalTextBox
            // 
            this.ApprArrivalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ApprArrivalTextBox.Location = new System.Drawing.Point(129, 463);
            this.ApprArrivalTextBox.Name = "ApprArrivalTextBox";
            this.ApprArrivalTextBox.Size = new System.Drawing.Size(443, 20);
            this.ApprArrivalTextBox.TabIndex = 17;
            this.ApprArrivalTextBox.TextChanged += new System.EventHandler(this.ApprArrivalTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Comment product";
            // 
            // MerchandiseCommentTextBox
            // 
            this.MerchandiseCommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MerchandiseCommentTextBox.Location = new System.Drawing.Point(129, 134);
            this.MerchandiseCommentTextBox.Multiline = true;
            this.MerchandiseCommentTextBox.Name = "MerchandiseCommentTextBox";
            this.MerchandiseCommentTextBox.ReadOnly = true;
            this.MerchandiseCommentTextBox.Size = new System.Drawing.Size(443, 57);
            this.MerchandiseCommentTextBox.TabIndex = 19;
            // 
            // NoInvoiceCheckBox
            // 
            this.NoInvoiceCheckBox.AutoSize = true;
            this.NoInvoiceCheckBox.Location = new System.Drawing.Point(110, 304);
            this.NoInvoiceCheckBox.Name = "NoInvoiceCheckBox";
            this.NoInvoiceCheckBox.Size = new System.Drawing.Size(77, 17);
            this.NoInvoiceCheckBox.TabIndex = 20;
            this.NoInvoiceCheckBox.Text = "No invoice";
            this.NoInvoiceCheckBox.UseVisualStyleBackColor = true;
            this.NoInvoiceCheckBox.CheckedChanged += new System.EventHandler(this.NoInvoiceCheckBox_CheckedChanged);
            // 
            // SupplierComboBox
            // 
            this.SupplierComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SupplierComboBox.FormattingEnabled = true;
            this.SupplierComboBox.Location = new System.Drawing.Point(129, 9);
            this.SupplierComboBox.Name = "SupplierComboBox";
            this.SupplierComboBox.Size = new System.Drawing.Size(406, 21);
            this.SupplierComboBox.TabIndex = 21;
            this.SupplierComboBox.SelectedIndexChanged += new System.EventHandler(this.SupplierComboBox_SelectedIndexChanged);
            // 
            // merchandiseCombobox1
            // 
            this.merchandiseCombobox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.merchandiseCombobox1.FormattingEnabled = true;
            this.merchandiseCombobox1.Location = new System.Drawing.Point(129, 57);
            this.merchandiseCombobox1.Name = "merchandiseCombobox1";
            this.merchandiseCombobox1.Size = new System.Drawing.Size(406, 21);
            this.merchandiseCombobox1.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 386);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Currency";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // CurrencyCombobox
            // 
            this.CurrencyCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrencyCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrencyCombobox.FormattingEnabled = true;
            this.CurrencyCombobox.Location = new System.Drawing.Point(129, 383);
            this.CurrencyCombobox.Name = "CurrencyCombobox";
            this.CurrencyCombobox.Size = new System.Drawing.Size(238, 21);
            this.CurrencyCombobox.TabIndex = 24;
            this.CurrencyCombobox.SelectedIndexChanged += new System.EventHandler(this.CurrencyCombobox_SelectedIndexChanged_1);
            // 
            // EditCurrencyButton
            // 
            this.EditCurrencyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditCurrencyButton.Location = new System.Drawing.Point(373, 381);
            this.EditCurrencyButton.Name = "EditCurrencyButton";
            this.EditCurrencyButton.Size = new System.Drawing.Size(75, 23);
            this.EditCurrencyButton.TabIndex = 25;
            this.EditCurrencyButton.Text = "Edit/Add";
            this.EditCurrencyButton.UseVisualStyleBackColor = true;
            this.EditCurrencyButton.Click += new System.EventHandler(this.EditCurrencyButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.AttentionCheckBox);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.ConfirmedOrderDateTextBox);
            this.groupBox1.Controls.Add(this.ConfirmedOrderUserComboBox);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.InvoiceCheckDateTextBox);
            this.groupBox1.Controls.Add(this.InvoiceCheckerUserComboBox);
            this.groupBox1.Controls.Add(this.ArrivalDateTextBox);
            this.groupBox1.Controls.Add(this.ArrivalSignUserComboBox);
            this.groupBox1.Controls.Add(this.OrderDateTextBox);
            this.groupBox1.Controls.Add(this.OrdererUserComboBox);
            this.groupBox1.Controls.Add(this.BookDateTextBox);
            this.groupBox1.Controls.Add(this.BookerUserComboBox);
            this.groupBox1.Controls.Add(this.NoInvoiceCheckBox);
            this.groupBox1.Controls.Add(this.InvoiceInstCheckBox);
            this.groupBox1.Controls.Add(this.InvoiceClinCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(589, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 386);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status information";
            // 
            // AttentionCheckBox
            // 
            this.AttentionCheckBox.AutoSize = true;
            this.AttentionCheckBox.Location = new System.Drawing.Point(110, 324);
            this.AttentionCheckBox.Name = "AttentionCheckBox";
            this.AttentionCheckBox.Size = new System.Drawing.Size(109, 17);
            this.AttentionCheckBox.TabIndex = 21;
            this.AttentionCheckBox.Text = "Mark for attention";
            this.AttentionCheckBox.UseVisualStyleBackColor = true;
            this.AttentionCheckBox.CheckedChanged += new System.EventHandler(this.AttentionCheckBox_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 155);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Confirmed order date";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Confirmed order";
            // 
            // ConfirmedOrderDateTextBox
            // 
            this.ConfirmedOrderDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmedOrderDateTextBox.Location = new System.Drawing.Point(110, 152);
            this.ConfirmedOrderDateTextBox.Name = "ConfirmedOrderDateTextBox";
            this.ConfirmedOrderDateTextBox.Size = new System.Drawing.Size(252, 20);
            this.ConfirmedOrderDateTextBox.TabIndex = 18;
            this.ConfirmedOrderDateTextBox.TextChanged += new System.EventHandler(this.ConfirmedOrderDateTextBox_TextChanged);
            // 
            // ConfirmedOrderUserComboBox
            // 
            this.ConfirmedOrderUserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmedOrderUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConfirmedOrderUserComboBox.FormattingEnabled = true;
            this.ConfirmedOrderUserComboBox.Location = new System.Drawing.Point(110, 125);
            this.ConfirmedOrderUserComboBox.Name = "ConfirmedOrderUserComboBox";
            this.ConfirmedOrderUserComboBox.Size = new System.Drawing.Size(252, 21);
            this.ConfirmedOrderUserComboBox.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 271);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Invoice check date";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 244);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Invoice checker";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 208);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Arrival date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Arrival sign.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Order date";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Orderer";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Book date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Booker";
            // 
            // InvoiceCheckDateTextBox
            // 
            this.InvoiceCheckDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InvoiceCheckDateTextBox.Location = new System.Drawing.Point(110, 268);
            this.InvoiceCheckDateTextBox.Name = "InvoiceCheckDateTextBox";
            this.InvoiceCheckDateTextBox.Size = new System.Drawing.Size(252, 20);
            this.InvoiceCheckDateTextBox.TabIndex = 7;
            this.InvoiceCheckDateTextBox.TextChanged += new System.EventHandler(this.InvoiceCheckDateTextBox_TextChanged);
            // 
            // InvoiceCheckerUserComboBox
            // 
            this.InvoiceCheckerUserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InvoiceCheckerUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InvoiceCheckerUserComboBox.FormattingEnabled = true;
            this.InvoiceCheckerUserComboBox.Location = new System.Drawing.Point(110, 241);
            this.InvoiceCheckerUserComboBox.Name = "InvoiceCheckerUserComboBox";
            this.InvoiceCheckerUserComboBox.Size = new System.Drawing.Size(252, 21);
            this.InvoiceCheckerUserComboBox.TabIndex = 6;
            // 
            // ArrivalDateTextBox
            // 
            this.ArrivalDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ArrivalDateTextBox.Location = new System.Drawing.Point(110, 205);
            this.ArrivalDateTextBox.Name = "ArrivalDateTextBox";
            this.ArrivalDateTextBox.Size = new System.Drawing.Size(252, 20);
            this.ArrivalDateTextBox.TabIndex = 5;
            this.ArrivalDateTextBox.TextChanged += new System.EventHandler(this.ArrivalDateTextBox_TextChanged);
            // 
            // ArrivalSignUserComboBox
            // 
            this.ArrivalSignUserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ArrivalSignUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ArrivalSignUserComboBox.FormattingEnabled = true;
            this.ArrivalSignUserComboBox.Location = new System.Drawing.Point(110, 178);
            this.ArrivalSignUserComboBox.Name = "ArrivalSignUserComboBox";
            this.ArrivalSignUserComboBox.Size = new System.Drawing.Size(252, 21);
            this.ArrivalSignUserComboBox.TabIndex = 4;
            // 
            // OrderDateTextBox
            // 
            this.OrderDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderDateTextBox.Location = new System.Drawing.Point(110, 99);
            this.OrderDateTextBox.Name = "OrderDateTextBox";
            this.OrderDateTextBox.Size = new System.Drawing.Size(252, 20);
            this.OrderDateTextBox.TabIndex = 3;
            this.OrderDateTextBox.TextChanged += new System.EventHandler(this.OrderDateTextBox_TextChanged);
            // 
            // OrdererUserComboBox
            // 
            this.OrdererUserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OrdererUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OrdererUserComboBox.FormattingEnabled = true;
            this.OrdererUserComboBox.Location = new System.Drawing.Point(110, 72);
            this.OrdererUserComboBox.Name = "OrdererUserComboBox";
            this.OrdererUserComboBox.Size = new System.Drawing.Size(252, 21);
            this.OrdererUserComboBox.TabIndex = 2;
            // 
            // BookDateTextBox
            // 
            this.BookDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookDateTextBox.Location = new System.Drawing.Point(110, 46);
            this.BookDateTextBox.Name = "BookDateTextBox";
            this.BookDateTextBox.Size = new System.Drawing.Size(252, 20);
            this.BookDateTextBox.TabIndex = 1;
            this.BookDateTextBox.TextChanged += new System.EventHandler(this.BookDateTextBox_TextChanged);
            // 
            // BookerUserComboBox
            // 
            this.BookerUserComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BookerUserComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BookerUserComboBox.FormattingEnabled = true;
            this.BookerUserComboBox.Location = new System.Drawing.Point(110, 19);
            this.BookerUserComboBox.Name = "BookerUserComboBox";
            this.BookerUserComboBox.Size = new System.Drawing.Size(252, 21);
            this.BookerUserComboBox.TabIndex = 0;
            this.BookerUserComboBox.SelectedIndexChanged += new System.EventHandler(this.BookerUserComboBox_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 334);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 28;
            this.label16.Text = "Final unit prize";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // FinalPrizeTextBox
            // 
            this.FinalPrizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FinalPrizeTextBox.Location = new System.Drawing.Point(129, 331);
            this.FinalPrizeTextBox.Name = "FinalPrizeTextBox";
            this.FinalPrizeTextBox.Size = new System.Drawing.Size(443, 20);
            this.FinalPrizeTextBox.TabIndex = 29;
            this.FinalPrizeTextBox.TextChanged += new System.EventHandler(this.FinalPrizeTextBox_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 413);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Invoice number";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // InvoiceNumberTextBox
            // 
            this.InvoiceNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InvoiceNumberTextBox.Location = new System.Drawing.Point(129, 410);
            this.InvoiceNumberTextBox.Name = "InvoiceNumberTextBox";
            this.InvoiceNumberTextBox.Size = new System.Drawing.Size(443, 20);
            this.InvoiceNumberTextBox.TabIndex = 31;
            this.InvoiceNumberTextBox.TextChanged += new System.EventHandler(this.InvoiceNumberTextBox_TextChanged);
            // 
            // DeliveryDeviationTextBox
            // 
            this.DeliveryDeviationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeliveryDeviationTextBox.Location = new System.Drawing.Point(9, 642);
            this.DeliveryDeviationTextBox.Multiline = true;
            this.DeliveryDeviationTextBox.Name = "DeliveryDeviationTextBox";
            this.DeliveryDeviationTextBox.Size = new System.Drawing.Size(950, 64);
            this.DeliveryDeviationTextBox.TabIndex = 32;
            this.DeliveryDeviationTextBox.TextChanged += new System.EventHandler(this.DeliveryDeviationTextBox_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 626);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 13);
            this.label20.TabIndex = 33;
            this.label20.Text = "Delivery deviation";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 200);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(85, 13);
            this.label21.TabIndex = 34;
            this.label21.Text = "Product storage:";
            // 
            // StorageTextBox
            // 
            this.StorageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StorageTextBox.Location = new System.Drawing.Point(129, 197);
            this.StorageTextBox.Multiline = true;
            this.StorageTextBox.Name = "StorageTextBox";
            this.StorageTextBox.ReadOnly = true;
            this.StorageTextBox.Size = new System.Drawing.Size(443, 46);
            this.StorageTextBox.TabIndex = 35;
            // 
            // ShowSupplierButton
            // 
            this.ShowSupplierButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowSupplierButton.Location = new System.Drawing.Point(541, 7);
            this.ShowSupplierButton.Name = "ShowSupplierButton";
            this.ShowSupplierButton.Size = new System.Drawing.Size(31, 23);
            this.ShowSupplierButton.TabIndex = 36;
            this.ShowSupplierButton.Text = "...";
            this.ShowSupplierButton.UseVisualStyleBackColor = true;
            this.ShowSupplierButton.Click += new System.EventHandler(this.ShowSupplierButton_Click);
            // 
            // ShowProductButton
            // 
            this.ShowProductButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowProductButton.Location = new System.Drawing.Point(541, 55);
            this.ShowProductButton.Name = "ShowProductButton";
            this.ShowProductButton.Size = new System.Drawing.Size(31, 23);
            this.ShowProductButton.TabIndex = 37;
            this.ShowProductButton.Text = "...";
            this.ShowProductButton.UseVisualStyleBackColor = true;
            this.ShowProductButton.Click += new System.EventHandler(this.ShowProductButton_Click);
            // 
            // ArticleNumberTextBox
            // 
            this.ArticleNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArticleNumberTextBox.Location = new System.Drawing.Point(129, 249);
            this.ArticleNumberTextBox.Name = "ArticleNumberTextBox";
            this.ArticleNumberTextBox.ReadOnly = true;
            this.ArticleNumberTextBox.Size = new System.Drawing.Size(443, 20);
            this.ArticleNumberTextBox.TabIndex = 38;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 360);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "Total prize";
            // 
            // TotalPrizeTextBox
            // 
            this.TotalPrizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalPrizeTextBox.Location = new System.Drawing.Point(129, 357);
            this.TotalPrizeTextBox.Name = "TotalPrizeTextBox";
            this.TotalPrizeTextBox.ReadOnly = true;
            this.TotalPrizeTextBox.Size = new System.Drawing.Size(443, 20);
            this.TotalPrizeTextBox.TabIndex = 40;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(595, 413);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(97, 13);
            this.label23.TabIndex = 41;
            this.label23.Text = "Purchase order no.";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(595, 439);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(78, 13);
            this.label24.TabIndex = 42;
            this.label24.Text = "Sales order no.";
            this.label24.Click += new System.EventHandler(this.label24_Click);
            // 
            // PurchaseOrderNoTextBox
            // 
            this.PurchaseOrderNoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PurchaseOrderNoTextBox.Location = new System.Drawing.Point(699, 410);
            this.PurchaseOrderNoTextBox.Name = "PurchaseOrderNoTextBox";
            this.PurchaseOrderNoTextBox.Size = new System.Drawing.Size(252, 20);
            this.PurchaseOrderNoTextBox.TabIndex = 43;
            this.PurchaseOrderNoTextBox.TextChanged += new System.EventHandler(this.PurchaseOrderNoTextBox_TextChanged);
            // 
            // SalesOrdernoTextBox
            // 
            this.SalesOrdernoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SalesOrdernoTextBox.Location = new System.Drawing.Point(699, 436);
            this.SalesOrdernoTextBox.Name = "SalesOrdernoTextBox";
            this.SalesOrdernoTextBox.Size = new System.Drawing.Size(252, 20);
            this.SalesOrdernoTextBox.TabIndex = 44;
            this.SalesOrdernoTextBox.TextChanged += new System.EventHandler(this.SalesOrdernoTextBox_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 110);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 13);
            this.label25.TabIndex = 45;
            this.label25.Text = "Group:";
            // 
            // OrderingUnitComboBox
            // 
            this.OrderingUnitComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderingUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OrderingUnitComboBox.FormattingEnabled = true;
            this.OrderingUnitComboBox.Location = new System.Drawing.Point(129, 107);
            this.OrderingUnitComboBox.Name = "OrderingUnitComboBox";
            this.OrderingUnitComboBox.Size = new System.Drawing.Size(443, 21);
            this.OrderingUnitComboBox.TabIndex = 46;
            this.OrderingUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.OrderingUnitComboBox_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 439);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(92, 13);
            this.label26.TabIndex = 47;
            this.label26.Text = "Customer number:";
            // 
            // CustomerNumberComboBox
            // 
            this.CustomerNumberComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CustomerNumberComboBox.FormattingEnabled = true;
            this.CustomerNumberComboBox.Location = new System.Drawing.Point(129, 436);
            this.CustomerNumberComboBox.Name = "CustomerNumberComboBox";
            this.CustomerNumberComboBox.Size = new System.Drawing.Size(443, 21);
            this.CustomerNumberComboBox.TabIndex = 48;
            this.CustomerNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.CustomerNumberComboBox_SelectedIndexChanged);
            // 
            // Periodization
            // 
            this.Periodization.Caption = "Periodization";
            this.Periodization.Location = new System.Drawing.Point(9, 489);
            this.Periodization.Name = "Periodization";
            this.Periodization.Size = new System.Drawing.Size(563, 77);
            this.Periodization.TabIndex = 49;
            this.Periodization.EnquiryChanged += new EventHandler(Periodization_Changed);
            // 
            // CreatePostDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCloseButton;
            this.ClientSize = new System.Drawing.Size(977, 852);
            this.Controls.Add(this.Periodization);
            this.Controls.Add(this.CustomerNumberComboBox);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.OrderingUnitComboBox);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.SalesOrdernoTextBox);
            this.Controls.Add(this.PurchaseOrderNoTextBox);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.TotalPrizeTextBox);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.ArticleNumberTextBox);
            this.Controls.Add(this.ShowProductButton);
            this.Controls.Add(this.ShowSupplierButton);
            this.Controls.Add(this.StorageTextBox);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.DeliveryDeviationTextBox);
            this.Controls.Add(this.InvoiceNumberTextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.FinalPrizeTextBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.EditCurrencyButton);
            this.Controls.Add(this.CurrencyCombobox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.merchandiseCombobox1);
            this.Controls.Add(this.SupplierComboBox);
            this.Controls.Add(this.MerchandiseCommentTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ApprArrivalTextBox);
            this.Controls.Add(this.ApprArrivalLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.ApprPrizeTextBox);
            this.Controls.Add(this.AmountTextBox);
            this.Controls.Add(this.CommentLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MyCloseButton);
            this.Controls.Add(this.SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreatePostDialog";
            this.ShowInTaskbar = false;
            this.Text = "Create Post";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button MyCloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.TextBox AmountTextBox;
        private System.Windows.Forms.TextBox ApprPrizeTextBox;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox InvoiceInstCheckBox;
        private System.Windows.Forms.CheckBox InvoiceClinCheckBox;
        private System.Windows.Forms.Label ApprArrivalLabel;
        private System.Windows.Forms.TextBox ApprArrivalTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MerchandiseCommentTextBox;
        private System.Windows.Forms.CheckBox NoInvoiceCheckBox;
        private Molmed.PlattformOrdMan.UI.Component.SupplierCombobox SupplierComboBox;
        private Molmed.PlattformOrdMan.UI.Component.MerchandiseCombobox merchandiseCombobox1;
        private System.Windows.Forms.Label label7;
        private Molmed.PlattformOrdMan.UI.Component.CurrencyCombobox CurrencyCombobox;
        private System.Windows.Forms.Button EditCurrencyButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox InvoiceCheckDateTextBox;
        private Molmed.PlattformOrdMan.UI.Component.UserComboBox InvoiceCheckerUserComboBox;
        private System.Windows.Forms.TextBox ArrivalDateTextBox;
        private Molmed.PlattformOrdMan.UI.Component.UserComboBox ArrivalSignUserComboBox;
        private System.Windows.Forms.TextBox OrderDateTextBox;
        private Molmed.PlattformOrdMan.UI.Component.UserComboBox OrdererUserComboBox;
        private System.Windows.Forms.TextBox BookDateTextBox;
        private Molmed.PlattformOrdMan.UI.Component.UserComboBox BookerUserComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox FinalPrizeTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox InvoiceNumberTextBox;
        private Molmed.PlattformOrdMan.UI.Component.UserComboBox ConfirmedOrderUserComboBox;
        private System.Windows.Forms.TextBox ConfirmedOrderDateTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox DeliveryDeviationTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox StorageTextBox;
        private System.Windows.Forms.Button ShowSupplierButton;
        private System.Windows.Forms.Button ShowProductButton;
        private System.Windows.Forms.TextBox ArticleNumberTextBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox TotalPrizeTextBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox PurchaseOrderNoTextBox;
        private System.Windows.Forms.TextBox SalesOrdernoTextBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox OrderingUnitComboBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox CustomerNumberComboBox;
        private System.Windows.Forms.CheckBox AttentionCheckBox;
        private Component.EnquiryField Periodization;
    }
}