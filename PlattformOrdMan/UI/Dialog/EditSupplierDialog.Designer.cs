using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.Dialog
{
    partial class EditSupplierDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSupplierDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DissableCheckBox = new System.Windows.Forms.CheckBox();
            this.IdentifierTextBox = new System.Windows.Forms.TextBox();
            this.TelNrTextBox = new System.Windows.Forms.TextBox();
            this.ContractTerminationTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.ShortNameTextBox = new System.Windows.Forms.TextBox();
            this.CustomerNumberListView = new OrderManListView();
            this.CustomerNumberFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tel. nr/fax:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Contract termination";
            // 
            // DissableCheckBox
            // 
            this.DissableCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DissableCheckBox.AutoSize = true;
            this.DissableCheckBox.Location = new System.Drawing.Point(15, 393);
            this.DissableCheckBox.Name = "DissableCheckBox";
            this.DissableCheckBox.Size = new System.Drawing.Size(119, 17);
            this.DissableCheckBox.TabIndex = 5;
            this.DissableCheckBox.Text = "Disable this supplier";
            this.DissableCheckBox.UseVisualStyleBackColor = true;
            this.DissableCheckBox.CheckedChanged += new System.EventHandler(this.DissableCheckBox_CheckedChanged);
            // 
            // IdentifierTextBox
            // 
            this.IdentifierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IdentifierTextBox.Location = new System.Drawing.Point(122, 53);
            this.IdentifierTextBox.Name = "IdentifierTextBox";
            this.IdentifierTextBox.Size = new System.Drawing.Size(591, 20);
            this.IdentifierTextBox.TabIndex = 6;
            this.IdentifierTextBox.TextChanged += new System.EventHandler(this.IdentifierTextBox_TextChanged);
            // 
            // TelNrTextBox
            // 
            this.TelNrTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TelNrTextBox.Location = new System.Drawing.Point(122, 105);
            this.TelNrTextBox.Name = "TelNrTextBox";
            this.TelNrTextBox.Size = new System.Drawing.Size(591, 20);
            this.TelNrTextBox.TabIndex = 7;
            this.TelNrTextBox.TextChanged += new System.EventHandler(this.TelNrTextBox_TextChanged);
            // 
            // ContractTerminationTextBox
            // 
            this.ContractTerminationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContractTerminationTextBox.Location = new System.Drawing.Point(122, 131);
            this.ContractTerminationTextBox.Name = "ContractTerminationTextBox";
            this.ContractTerminationTextBox.Size = new System.Drawing.Size(592, 20);
            this.ContractTerminationTextBox.TabIndex = 10;
            this.ContractTerminationTextBox.TextChanged += new System.EventHandler(this.ContractTerminationTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Comment";
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentTextBox.Location = new System.Drawing.Point(122, 157);
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(591, 83);
            this.CommentTextBox.TabIndex = 12;
            this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentTextBox_TextChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Location = new System.Drawing.Point(15, 434);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(86, 24);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Update";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(628, 434);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(86, 24);
            this.MyCancelButton.TabIndex = 14;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Short name";
            // 
            // ShortNameTextBox
            // 
            this.ShortNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortNameTextBox.Location = new System.Drawing.Point(122, 79);
            this.ShortNameTextBox.Name = "ShortNameTextBox";
            this.ShortNameTextBox.Size = new System.Drawing.Size(591, 20);
            this.ShortNameTextBox.TabIndex = 16;
            this.ShortNameTextBox.TextChanged += new System.EventHandler(this.ShortNameTextBox_TextChanged);
            // 
            // CustomerNumberListView
            // 
            this.CustomerNumberListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerNumberListView.EnableColumnSort = true;
            this.CustomerNumberListView.FullRowSelect = true;
            this.CustomerNumberListView.GridLines = true;
            this.CustomerNumberListView.Location = new System.Drawing.Point(122, 269);
            this.CustomerNumberListView.MultiSelect = false;
            this.CustomerNumberListView.Name = "CustomerNumberListView";
            this.CustomerNumberListView.Size = new System.Drawing.Size(591, 96);
            this.CustomerNumberListView.TabIndex = 17;
            this.CustomerNumberListView.UseCompatibleStateImageBehavior = false;
            this.CustomerNumberListView.View = System.Windows.Forms.View.Details;
            // 
            // CustomerNumberFilterCheckBox
            // 
            this.CustomerNumberFilterCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CustomerNumberFilterCheckBox.AutoSize = true;
            this.CustomerNumberFilterCheckBox.Checked = true;
            this.CustomerNumberFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CustomerNumberFilterCheckBox.Location = new System.Drawing.Point(122, 246);
            this.CustomerNumberFilterCheckBox.Name = "CustomerNumberFilterCheckBox";
            this.CustomerNumberFilterCheckBox.Size = new System.Drawing.Size(221, 17);
            this.CustomerNumberFilterCheckBox.TabIndex = 19;
            this.CustomerNumberFilterCheckBox.Text = "Show only customer numbers for <group>";
            this.CustomerNumberFilterCheckBox.UseVisualStyleBackColor = true;
            this.CustomerNumberFilterCheckBox.CheckedChanged += new System.EventHandler(this.CustomerNumberFilterCheckBox_CheckedChanged_1);
            // 
            // EditSupplierDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(726, 470);
            this.Controls.Add(this.CustomerNumberFilterCheckBox);
            this.Controls.Add(this.CustomerNumberListView);
            this.Controls.Add(this.ShortNameTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ContractTerminationTextBox);
            this.Controls.Add(this.TelNrTextBox);
            this.Controls.Add(this.IdentifierTextBox);
            this.Controls.Add(this.DissableCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditSupplierDialog";
            this.ShowInTaskbar = false;
            this.Text = "Update supplier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox DissableCheckBox;
        private System.Windows.Forms.TextBox IdentifierTextBox;
        private System.Windows.Forms.TextBox TelNrTextBox;
        private System.Windows.Forms.TextBox ContractTerminationTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CommentTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ShortNameTextBox;
        private OrderManListView CustomerNumberListView;
        private System.Windows.Forms.CheckBox CustomerNumberFilterCheckBox;
    }
}