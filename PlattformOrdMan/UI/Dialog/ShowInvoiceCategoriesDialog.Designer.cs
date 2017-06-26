namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class ShowInvoiceCategoriesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowInvoiceCategoriesDialog));
            this.InvoiceCategoriesListView = new System.Windows.Forms.ListView();
            this.AddNewButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InvoiceCategoriesListView
            // 
            this.InvoiceCategoriesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InvoiceCategoriesListView.FullRowSelect = true;
            this.InvoiceCategoriesListView.GridLines = true;
            this.InvoiceCategoriesListView.Location = new System.Drawing.Point(12, 41);
            this.InvoiceCategoriesListView.Name = "InvoiceCategoriesListView";
            this.InvoiceCategoriesListView.Size = new System.Drawing.Size(535, 312);
            this.InvoiceCategoriesListView.TabIndex = 0;
            this.InvoiceCategoriesListView.UseCompatibleStateImageBehavior = false;
            this.InvoiceCategoriesListView.View = System.Windows.Forms.View.Details;
            // 
            // AddNewButton
            // 
            this.AddNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewButton.Location = new System.Drawing.Point(472, 12);
            this.AddNewButton.Name = "AddNewButton";
            this.AddNewButton.Size = new System.Drawing.Size(75, 23);
            this.AddNewButton.TabIndex = 1;
            this.AddNewButton.Text = "Add new";
            this.AddNewButton.UseVisualStyleBackColor = true;
            this.AddNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(472, 359);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ShowInvoiceCategoriesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(559, 394);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.AddNewButton);
            this.Controls.Add(this.InvoiceCategoriesListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowInvoiceCategoriesDialog";
            this.ShowInTaskbar = false;
            this.Text = "Invoice Categories";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView InvoiceCategoriesListView;
        private System.Windows.Forms.Button AddNewButton;
        private System.Windows.Forms.Button CloseButton;
    }
}