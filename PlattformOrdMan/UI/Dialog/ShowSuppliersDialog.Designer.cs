using PlattformOrdMan.UI.View;

namespace PlattformOrdMan.UI.Dialog
{
    partial class ShowSuppliersDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowSuppliersDialog));
            this.CloseButton = new System.Windows.Forms.Button();
            this.AddNewButton = new System.Windows.Forms.Button();
            this.SuppliersListView = new SupplierListView();
            this.RestoreSortingButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FreeTextSearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(774, 516);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(86, 24);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AddNewButton
            // 
            this.AddNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewButton.Location = new System.Drawing.Point(748, 71);
            this.AddNewButton.Name = "AddNewButton";
            this.AddNewButton.Size = new System.Drawing.Size(112, 24);
            this.AddNewButton.TabIndex = 2;
            this.AddNewButton.Text = "New Supplier ...";
            this.AddNewButton.UseVisualStyleBackColor = true;
            this.AddNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // SuppliersListView
            // 
            this.SuppliersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SuppliersListView.FullRowSelect = true;
            this.SuppliersListView.GridLines = true;
            this.SuppliersListView.Location = new System.Drawing.Point(12, 101);
            this.SuppliersListView.Name = "SuppliersListView";
            this.SuppliersListView.Size = new System.Drawing.Size(848, 409);
            this.SuppliersListView.TabIndex = 3;
            this.SuppliersListView.UseCompatibleStateImageBehavior = false;
            this.SuppliersListView.View = System.Windows.Forms.View.Details;
            // 
            // RestoreSortingButton
            // 
            this.RestoreSortingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreSortingButton.Location = new System.Drawing.Point(652, 71);
            this.RestoreSortingButton.Name = "RestoreSortingButton";
            this.RestoreSortingButton.Size = new System.Drawing.Size(90, 24);
            this.RestoreSortingButton.TabIndex = 4;
            this.RestoreSortingButton.Text = "Restore sorting";
            this.RestoreSortingButton.UseVisualStyleBackColor = true;
            this.RestoreSortingButton.Click += new System.EventHandler(this.RestoreSortingButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.SearchButton);
            this.groupBox1.Controls.Add(this.FreeTextSearchTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 83);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // FreeTextSearchTextBox
            // 
            this.FreeTextSearchTextBox.Location = new System.Drawing.Point(6, 57);
            this.FreeTextSearchTextBox.Name = "FreeTextSearchTextBox";
            this.FreeTextSearchTextBox.Size = new System.Drawing.Size(318, 20);
            this.FreeTextSearchTextBox.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(330, 54);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(80, 24);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(494, 54);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(80, 24);
            this.ResetButton.TabIndex = 2;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ShowSuppliersDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(872, 552);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RestoreSortingButton);
            this.Controls.Add(this.SuppliersListView);
            this.Controls.Add(this.AddNewButton);
            this.Controls.Add(this.CloseButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowSuppliersDialog";
            this.ShowInTaskbar = false;
            this.Text = "ShowSuppliersDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button AddNewButton;
        private SupplierListView SuppliersListView;
        private System.Windows.Forms.Button RestoreSortingButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox FreeTextSearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ResetButton;
    }
}