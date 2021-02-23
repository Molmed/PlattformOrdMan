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
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.searchPanel2 = new PlattformOrdMan.UI.Component.SearchPanel();
            this.InfoPanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
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
            this.NewOrderButton.Location = new System.Drawing.Point(121, 3);
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
            this.PostOrderInfoLabel.Location = new System.Drawing.Point(6, 0);
            this.PostOrderInfoLabel.Name = "PostOrderInfoLabel";
            this.PostOrderInfoLabel.Size = new System.Drawing.Size(38, 15);
            this.PostOrderInfoLabel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Newly created post";
            // 
            // ProductArrivalLabel
            // 
            this.ProductArrivalLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductArrivalLabel.Location = new System.Drawing.Point(6, 21);
            this.ProductArrivalLabel.Name = "ProductArrivalLabel";
            this.ProductArrivalLabel.Size = new System.Drawing.Size(38, 15);
            this.ProductArrivalLabel.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Product is ordered";
            // 
            // CompletedPostPanel
            // 
            this.CompletedPostPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CompletedPostPanel.Location = new System.Drawing.Point(6, 83);
            this.CompletedPostPanel.Name = "CompletedPostPanel";
            this.CompletedPostPanel.Size = new System.Drawing.Size(38, 14);
            this.CompletedPostPanel.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Invoice is OK, completed post";
            // 
            // InvoiceNotCheckedPanel
            // 
            this.InvoiceNotCheckedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InvoiceNotCheckedPanel.Location = new System.Drawing.Point(6, 63);
            this.InvoiceNotCheckedPanel.Name = "InvoiceNotCheckedPanel";
            this.InvoiceNotCheckedPanel.Size = new System.Drawing.Size(38, 14);
            this.InvoiceNotCheckedPanel.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Product arrival is confirmed";
            // 
            // RestoreSortingButton
            // 
            this.RestoreSortingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreSortingButton.Location = new System.Drawing.Point(24, 4);
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
            this.ProductOrderConfirmedLabel.Location = new System.Drawing.Point(6, 42);
            this.ProductOrderConfirmedLabel.Name = "ProductOrderConfirmedLabel";
            this.ProductOrderConfirmedLabel.Size = new System.Drawing.Size(38, 14);
            this.ProductOrderConfirmedLabel.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Product order is confirmed";
            // 
            // AttentionPanel
            // 
            this.AttentionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AttentionPanel.Location = new System.Drawing.Point(6, 103);
            this.AttentionPanel.Name = "AttentionPanel";
            this.AttentionPanel.Size = new System.Drawing.Size(38, 14);
            this.AttentionPanel.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "This post is marked for attention";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Completed post with Periodization";
            // 
            // PeriodizationPanel
            // 
            this.PeriodizationPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PeriodizationPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PeriodizationPanel.Location = new System.Drawing.Point(6, 126);
            this.PeriodizationPanel.Name = "PeriodizationPanel";
            this.PeriodizationPanel.Size = new System.Drawing.Size(38, 14);
            this.PeriodizationPanel.TabIndex = 10;
            // 
            // InfoPanel
            // 
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Controls.Add(this.PostOrderInfoLabel);
            this.InfoPanel.Controls.Add(this.ProductArrivalLabel);
            this.InfoPanel.Controls.Add(this.label2);
            this.InfoPanel.Controls.Add(this.PeriodizationPanel);
            this.InfoPanel.Controls.Add(this.CompletedPostPanel);
            this.InfoPanel.Controls.Add(this.label7);
            this.InfoPanel.Controls.Add(this.label3);
            this.InfoPanel.Controls.Add(this.label5);
            this.InfoPanel.Controls.Add(this.InvoiceNotCheckedPanel);
            this.InfoPanel.Controls.Add(this.AttentionPanel);
            this.InfoPanel.Controls.Add(this.label4);
            this.InfoPanel.Controls.Add(this.label6);
            this.InfoPanel.Controls.Add(this.ProductOrderConfirmedLabel);
            this.InfoPanel.Location = new System.Drawing.Point(544, 12);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(296, 152);
            this.InfoPanel.TabIndex = 18;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.NewOrderButton);
            this.ButtonPanel.Controls.Add(this.RestoreSortingButton);
            this.ButtonPanel.Location = new System.Drawing.Point(853, 131);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(207, 33);
            this.ButtonPanel.TabIndex = 19;
            // 
            // searchPanel2
            // 
            this.searchPanel2.Caption = "Search";
            this.searchPanel2.Location = new System.Drawing.Point(12, 12);
            this.searchPanel2.Name = "searchPanel2";
            this.searchPanel2.Size = new System.Drawing.Size(526, 289);
            this.searchPanel2.TabIndex = 20;
            // 
            // ShowOrderHistoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1072, 630);
            this.Controls.Add(this.PostsListView);
            this.Controls.Add(this.searchPanel2);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.InfoPanel);
            this.Controls.Add(this.CloseButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowOrderHistoryDialog";
            this.ShowInTaskbar = false;
            this.Text = "ShowOrderHistoryDialog";
            this.Shown += new System.EventHandler(this.ShowOrderHistoryDialog_Shown);
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel CompletedPostPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel InvoiceNotCheckedPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RestoreSortingButton;
        private System.Windows.Forms.Panel ProductOrderConfirmedLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel AttentionPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel PeriodizationPanel;
        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Panel ButtonPanel;
        private SearchPanel searchPanel2;
    }
}