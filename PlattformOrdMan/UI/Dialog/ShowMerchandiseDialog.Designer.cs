namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class ShowMerchandiseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowMerchandiseDialog));
            this.CloseButton = new System.Windows.Forms.Button();
            this.AddNewButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchProductTextBox = new System.Windows.Forms.TextBox();
            this.SupplierCombobox = new Molmed.PlattformOrdMan.UI.Component.SearchingCombobox();
            this.RestoreSortingButton = new System.Windows.Forms.Button();
            this.MerchandiseListView2 = new Molmed.PlattformOrdMan.UI.View.MerchandiseListView(this.components);
            this.ShowOnlyEnabledProductsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(805, 447);
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
            this.AddNewButton.Location = new System.Drawing.Point(779, 72);
            this.AddNewButton.Name = "AddNewButton";
            this.AddNewButton.Size = new System.Drawing.Size(112, 24);
            this.AddNewButton.TabIndex = 2;
            this.AddNewButton.Text = "New product ...";
            this.AddNewButton.UseVisualStyleBackColor = true;
            this.AddNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.SearchButton);
            this.groupBox1.Controls.Add(this.SearchProductTextBox);
            this.groupBox1.Controls.Add(this.SupplierCombobox);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 81);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(490, 49);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(379, 49);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchProductTextBox
            // 
            this.SearchProductTextBox.Location = new System.Drawing.Point(6, 51);
            this.SearchProductTextBox.Name = "SearchProductTextBox";
            this.SearchProductTextBox.Size = new System.Drawing.Size(367, 20);
            this.SearchProductTextBox.TabIndex = 1;
            // 
            // SupplierCombobox
            // 
            this.SupplierCombobox.FormattingEnabled = true;
            this.SupplierCombobox.Location = new System.Drawing.Point(6, 24);
            this.SupplierCombobox.Name = "SupplierCombobox";
            this.SupplierCombobox.Size = new System.Drawing.Size(367, 21);
            this.SupplierCombobox.TabIndex = 0;
            // 
            // RestoreSortingButton
            // 
            this.RestoreSortingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreSortingButton.Location = new System.Drawing.Point(683, 72);
            this.RestoreSortingButton.Name = "RestoreSortingButton";
            this.RestoreSortingButton.Size = new System.Drawing.Size(90, 24);
            this.RestoreSortingButton.TabIndex = 5;
            this.RestoreSortingButton.Text = "Restore sorting";
            this.RestoreSortingButton.UseVisualStyleBackColor = true;
            this.RestoreSortingButton.Click += new System.EventHandler(this.RestoreSortingButton_Click);
            // 
            // MerchandiseListView2
            // 
            this.MerchandiseListView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MerchandiseListView2.FullRowSelect = true;
            this.MerchandiseListView2.GridLines = true;
            this.MerchandiseListView2.Location = new System.Drawing.Point(12, 102);
            this.MerchandiseListView2.Name = "MerchandiseListView2";
            this.MerchandiseListView2.Size = new System.Drawing.Size(879, 339);
            this.MerchandiseListView2.TabIndex = 6;
            this.MerchandiseListView2.UseCompatibleStateImageBehavior = false;
            this.MerchandiseListView2.View = System.Windows.Forms.View.Details;
            // 
            // ShowOnlyEnabledProductsCheckBox
            // 
            this.ShowOnlyEnabledProductsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowOnlyEnabledProductsCheckBox.AutoSize = true;
            this.ShowOnlyEnabledProductsCheckBox.Location = new System.Drawing.Point(731, 29);
            this.ShowOnlyEnabledProductsCheckBox.Name = "ShowOnlyEnabledProductsCheckBox";
            this.ShowOnlyEnabledProductsCheckBox.Size = new System.Drawing.Size(160, 17);
            this.ShowOnlyEnabledProductsCheckBox.TabIndex = 7;
            this.ShowOnlyEnabledProductsCheckBox.Text = "Show only enabled products";
            this.ShowOnlyEnabledProductsCheckBox.UseVisualStyleBackColor = true;
            this.ShowOnlyEnabledProductsCheckBox.CheckedChanged += new System.EventHandler(this.ShowOnlyEnabledProductsCheckBox_CheckedChanged);
            // 
            // ShowMerchandiseDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(903, 483);
            this.Controls.Add(this.ShowOnlyEnabledProductsCheckBox);
            this.Controls.Add(this.MerchandiseListView2);
            this.Controls.Add(this.RestoreSortingButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddNewButton);
            this.Controls.Add(this.CloseButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowMerchandiseDialog";
            this.Text = "Registered products";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button AddNewButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private Molmed.PlattformOrdMan.UI.Component.SearchingCombobox SupplierCombobox;
        private System.Windows.Forms.TextBox SearchProductTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button RestoreSortingButton;
        private Molmed.PlattformOrdMan.UI.View.MerchandiseListView MerchandiseListView2;
        private System.Windows.Forms.CheckBox ShowOnlyEnabledProductsCheckBox;
    }
}