using PlattformOrdMan.UI.Component;
using PlattformOrdMan.UI.View.Post;

namespace PlattformOrdMan.UI.Dialog
{
    partial class ShowOrderHistoryDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowOrderHistoryDialog));
            this.PostsListView = new PlattformOrdMan.UI.View.Post.PostListView(this.components);
            this.NewOrderButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PostOrderInfoLabel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductArrivalLabel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.FreeTextSearchTextBox = new System.Windows.Forms.TextBox();
            this.userComboBox1 = new PlattformOrdMan.UI.Component.UserComboBox();
            this.SupplierCombobox = new PlattformOrdMan.UI.Component.SupplierCombobox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.merchandiseCombobox1 = new PlattformOrdMan.UI.Component.MerchandiseCombobox();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.CompletedPostPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.InvoiceNotCheckedPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.RestoreSortingButton = new System.Windows.Forms.Button();
            this.ProductOrderConfirmedLabel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.AttentionPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PeriodizationPanel = new System.Windows.Forms.Panel();
            this.searchPanel1 = new PlattformOrdMan.UI.Component.SearchPanel();
            this.searchPanel2 = new PlattformOrdMan.UI.Component.SearchPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PostsListView
            // 
            this.PostsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostsListView.EnableColumnSort = true;
            this.PostsListView.FullRowSelect = true;
            this.PostsListView.GridLines = true;
            this.PostsListView.HideSelection = false;
            this.PostsListView.Location = new System.Drawing.Point(12, 170);
            this.PostsListView.Name = "PostsListView";
            this.PostsListView.Size = new System.Drawing.Size(1048, 390);
            this.PostsListView.TabIndex = 0;
            this.PostsListView.UseCompatibleStateImageBehavior = false;
            this.PostsListView.View = System.Windows.Forms.View.Details;
            // 
            // NewOrderButton
            // 
            this.NewOrderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NewOrderButton.Location = new System.Drawing.Point(974, 140);
            this.NewOrderButton.Name = "NewOrderButton";
            this.NewOrderButton.Size = new System.Drawing.Size(86, 24);
            this.NewOrderButton.TabIndex = 1;
            this.NewOrderButton.Text = "New Order";
            this.NewOrderButton.UseVisualStyleBackColor = true;
            this.NewOrderButton.Click += new System.EventHandler(this.NewOrderButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(974, 594);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(86, 24);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PostOrderInfoLabel
            // 
            this.PostOrderInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PostOrderInfoLabel.Location = new System.Drawing.Point(570, 8);
            this.PostOrderInfoLabel.Name = "PostOrderInfoLabel";
            this.PostOrderInfoLabel.Size = new System.Drawing.Size(38, 15);
            this.PostOrderInfoLabel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(614, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Newly created post";
            // 
            // ProductArrivalLabel
            // 
            this.ProductArrivalLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductArrivalLabel.Location = new System.Drawing.Point(570, 29);
            this.ProductArrivalLabel.Name = "ProductArrivalLabel";
            this.ProductArrivalLabel.Size = new System.Drawing.Size(38, 15);
            this.ProductArrivalLabel.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(614, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Product is ordered";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClearButton);
            this.groupBox1.Controls.Add(this.FreeTextSearchTextBox);
            this.groupBox1.Controls.Add(this.userComboBox1);
            this.groupBox1.Controls.Add(this.SupplierCombobox);
            this.groupBox1.Controls.Add(this.SearchButton);
            this.groupBox1.Controls.Add(this.merchandiseCombobox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 156);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(428, 126);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 24);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "Reset";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // FreeTextSearchTextBox
            // 
            this.FreeTextSearchTextBox.Location = new System.Drawing.Point(6, 102);
            this.FreeTextSearchTextBox.Name = "FreeTextSearchTextBox";
            this.FreeTextSearchTextBox.Size = new System.Drawing.Size(416, 20);
            this.FreeTextSearchTextBox.TabIndex = 8;
            // 
            // userComboBox1
            // 
            this.userComboBox1.FormattingEnabled = true;
            this.userComboBox1.Location = new System.Drawing.Point(6, 75);
            this.userComboBox1.Name = "userComboBox1";
            this.userComboBox1.Size = new System.Drawing.Size(416, 21);
            this.userComboBox1.TabIndex = 7;
            // 
            // SupplierCombobox
            // 
            this.SupplierCombobox.FormattingEnabled = true;
            this.SupplierCombobox.Location = new System.Drawing.Point(6, 21);
            this.SupplierCombobox.Name = "SupplierCombobox";
            this.SupplierCombobox.Size = new System.Drawing.Size(416, 21);
            this.SupplierCombobox.TabIndex = 6;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(428, 99);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 24);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // merchandiseCombobox1
            // 
            this.merchandiseCombobox1.FormattingEnabled = true;
            this.merchandiseCombobox1.Location = new System.Drawing.Point(6, 48);
            this.merchandiseCombobox1.Name = "merchandiseCombobox1";
            this.merchandiseCombobox1.Size = new System.Drawing.Size(416, 21);
            this.merchandiseCombobox1.TabIndex = 3;
            // 
            // OptionsButton
            // 
            this.OptionsButton.Location = new System.Drawing.Point(925, 29);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(75, 24);
            this.OptionsButton.TabIndex = 10;
            this.OptionsButton.Text = "Options ...";
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // CompletedPostPanel
            // 
            this.CompletedPostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CompletedPostPanel.Location = new System.Drawing.Point(570, 91);
            this.CompletedPostPanel.Name = "CompletedPostPanel";
            this.CompletedPostPanel.Size = new System.Drawing.Size(38, 14);
            this.CompletedPostPanel.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(614, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Invoice is OK, completed post";
            // 
            // InvoiceNotCheckedPanel
            // 
            this.InvoiceNotCheckedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoiceNotCheckedPanel.Location = new System.Drawing.Point(570, 71);
            this.InvoiceNotCheckedPanel.Name = "InvoiceNotCheckedPanel";
            this.InvoiceNotCheckedPanel.Size = new System.Drawing.Size(38, 14);
            this.InvoiceNotCheckedPanel.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(614, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Product arrival is confirmed";
            // 
            // RestoreSortingButton
            // 
            this.RestoreSortingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreSortingButton.Location = new System.Drawing.Point(877, 141);
            this.RestoreSortingButton.Name = "RestoreSortingButton";
            this.RestoreSortingButton.Size = new System.Drawing.Size(91, 23);
            this.RestoreSortingButton.TabIndex = 12;
            this.RestoreSortingButton.Text = "Restore sorting";
            this.RestoreSortingButton.UseVisualStyleBackColor = true;
            this.RestoreSortingButton.Click += new System.EventHandler(this.RestoreSortingButton_Click);
            // 
            // ProductOrderConfirmedLabel
            // 
            this.ProductOrderConfirmedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductOrderConfirmedLabel.Location = new System.Drawing.Point(570, 50);
            this.ProductOrderConfirmedLabel.Name = "ProductOrderConfirmedLabel";
            this.ProductOrderConfirmedLabel.Size = new System.Drawing.Size(38, 14);
            this.ProductOrderConfirmedLabel.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(614, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Product order is confirmed";
            // 
            // AttentionPanel
            // 
            this.AttentionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttentionPanel.Location = new System.Drawing.Point(570, 111);
            this.AttentionPanel.Name = "AttentionPanel";
            this.AttentionPanel.Size = new System.Drawing.Size(38, 14);
            this.AttentionPanel.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(614, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "This post is marked for attention";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(614, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Completed post with Periodization";
            // 
            // PeriodizationPanel
            // 
            this.PeriodizationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PeriodizationPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PeriodizationPanel.Location = new System.Drawing.Point(570, 134);
            this.PeriodizationPanel.Name = "PeriodizationPanel";
            this.PeriodizationPanel.Size = new System.Drawing.Size(38, 14);
            this.PeriodizationPanel.TabIndex = 10;
            // 
            // searchPanel1
            // 
            this.searchPanel1.Caption = "Search";
            this.searchPanel1.Location = new System.Drawing.Point(429, 273);
            this.searchPanel1.Name = "searchPanel1";
            this.searchPanel1.Size = new System.Drawing.Size(527, 206);
            this.searchPanel1.TabIndex = 17;
            // 
            // searchPanel2
            // 
            this.searchPanel2.Caption = "Search";
            this.searchPanel2.Location = new System.Drawing.Point(474, 186);
            this.searchPanel2.Name = "searchPanel2";
            this.searchPanel2.Size = new System.Drawing.Size(526, 212);
            this.searchPanel2.TabIndex = 17;
            // 
            // ShowOrderHistoryDialog
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1072, 630);
            this.Controls.Add(this.searchPanel2);
            this.Controls.Add(this.OptionsButton);
            this.Controls.Add(this.PeriodizationPanel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AttentionPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ProductOrderConfirmedLabel);
            this.Controls.Add(this.RestoreSortingButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InvoiceNotCheckedPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CompletedPostPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ProductArrivalLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PostOrderInfoLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NewOrderButton);
            this.Controls.Add(this.PostsListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowOrderHistoryDialog";
            this.ShowInTaskbar = false;
            this.Text = "ShowOrderHistoryDialog";
            this.Shown += new System.EventHandler(this.ShowOrderHistoryDialog_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.ListView PostsListView;
        private PostListView PostsListView;
        private System.Windows.Forms.Button NewOrderButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel PostOrderInfoLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ProductArrivalLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MerchandiseCombobox merchandiseCombobox1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Panel CompletedPostPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel InvoiceNotCheckedPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RestoreSortingButton;
        private SupplierCombobox SupplierCombobox;
        private UserComboBox userComboBox1;
        private System.Windows.Forms.TextBox FreeTextSearchTextBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Panel ProductOrderConfirmedLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Panel AttentionPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel PeriodizationPanel;
        private SearchPanel searchPanel1;
        private SearchPanel searchPanel2;
    }
}