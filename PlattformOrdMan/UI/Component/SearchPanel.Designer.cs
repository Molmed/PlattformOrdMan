namespace PlattformOrdMan.UI.Component
{
    partial class SearchPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.FreeTextSearchTextBox = new System.Windows.Forms.TextBox();
            this.SupplierCombobox = new PlattformOrdMan.UI.Component.SupplierCombobox();
            this.label1 = new System.Windows.Forms.Label();
            this.toggleButton1 = new PlattformOrdMan.UI.Component.ToggleButton(this.components);
            this.LinrPanel = new System.Windows.Forms.Panel();
            this.userComboBox1 = new PlattformOrdMan.UI.Component.UserComboBox();
            this.merchandiseCombobox1 = new PlattformOrdMan.UI.Component.MerchandiseCombobox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 209);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ClearButton);
            this.splitContainer1.Panel1.Controls.Add(this.SearchButton);
            this.splitContainer1.Panel1.Controls.Add(this.FreeTextSearchTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.SupplierCombobox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.toggleButton1);
            this.splitContainer1.Panel1.Controls.Add(this.LinrPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.userComboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.merchandiseCombobox1);
            this.splitContainer1.Size = new System.Drawing.Size(514, 187);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 0;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(423, 66);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 24);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Reset";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(423, 36);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 24);
            this.SearchButton.TabIndex = 10;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FreeTextSearchTextBox
            // 
            this.FreeTextSearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FreeTextSearchTextBox.Location = new System.Drawing.Point(7, 39);
            this.FreeTextSearchTextBox.Name = "FreeTextSearchTextBox";
            this.FreeTextSearchTextBox.Size = new System.Drawing.Size(410, 20);
            this.FreeTextSearchTextBox.TabIndex = 9;
            // 
            // SupplierCombobox
            // 
            this.SupplierCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SupplierCombobox.FormattingEnabled = true;
            this.SupplierCombobox.Location = new System.Drawing.Point(7, 12);
            this.SupplierCombobox.Name = "SupplierCombobox";
            this.SupplierCombobox.Size = new System.Drawing.Size(410, 21);
            this.SupplierCombobox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Advanced";
            // 
            // toggleButton1
            // 
            this.toggleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toggleButton1.Location = new System.Drawing.Point(7, 87);
            this.toggleButton1.Name = "toggleButton1";
            this.toggleButton1.Size = new System.Drawing.Size(25, 23);
            this.toggleButton1.TabIndex = 0;
            this.toggleButton1.Text = "\\/";
            this.toggleButton1.UseVisualStyleBackColor = true;
            // 
            // LinrPanel
            // 
            this.LinrPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinrPanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LinrPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LinrPanel.Location = new System.Drawing.Point(105, 103);
            this.LinrPanel.Name = "LinrPanel";
            this.LinrPanel.Size = new System.Drawing.Size(403, 2);
            this.LinrPanel.TabIndex = 6;
            // 
            // userComboBox1
            // 
            this.userComboBox1.FormattingEnabled = true;
            this.userComboBox1.Location = new System.Drawing.Point(7, 30);
            this.userComboBox1.Name = "userComboBox1";
            this.userComboBox1.Size = new System.Drawing.Size(416, 21);
            this.userComboBox1.TabIndex = 8;
            // 
            // merchandiseCombobox1
            // 
            this.merchandiseCombobox1.FormattingEnabled = true;
            this.merchandiseCombobox1.Location = new System.Drawing.Point(7, 3);
            this.merchandiseCombobox1.Name = "merchandiseCombobox1";
            this.merchandiseCombobox1.Size = new System.Drawing.Size(416, 21);
            this.merchandiseCombobox1.TabIndex = 4;
            // 
            // SearchPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SearchPanel";
            this.Size = new System.Drawing.Size(526, 212);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel LinrPanel;
        private ToggleButton toggleButton1;
        private System.Windows.Forms.Label label1;
        private SupplierCombobox SupplierCombobox;
        private System.Windows.Forms.TextBox FreeTextSearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ClearButton;
        private MerchandiseCombobox merchandiseCombobox1;
        private UserComboBox userComboBox1;
    }
}
