namespace PlattformOrdMan.UI.Dialog.OptionsDialog
{
    partial class FilteringOptionsDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlaceOfPurchaseFilterListView = new System.Windows.Forms.ListView();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MyOkButton);
            this.panel1.Controls.Add(this.MyCancelButton);
            this.panel1.Controls.Add(this.PlaceOfPurchaseFilterListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 279);
            this.panel1.TabIndex = 0;
            // 
            // PlaceOfPurchaseFilterListView
            // 
            this.PlaceOfPurchaseFilterListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceOfPurchaseFilterListView.CheckBoxes = true;
            this.PlaceOfPurchaseFilterListView.GridLines = true;
            this.PlaceOfPurchaseFilterListView.HideSelection = false;
            this.PlaceOfPurchaseFilterListView.Location = new System.Drawing.Point(12, 12);
            this.PlaceOfPurchaseFilterListView.Name = "PlaceOfPurchaseFilterListView";
            this.PlaceOfPurchaseFilterListView.Size = new System.Drawing.Size(423, 200);
            this.PlaceOfPurchaseFilterListView.TabIndex = 18;
            this.PlaceOfPurchaseFilterListView.UseCompatibleStateImageBehavior = false;
            this.PlaceOfPurchaseFilterListView.View = System.Windows.Forms.View.Details;
            // 
            // MyOkButton
            // 
            this.MyOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MyOkButton.Location = new System.Drawing.Point(12, 243);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(86, 24);
            this.MyOkButton.TabIndex = 20;
            this.MyOkButton.Text = "OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(349, 243);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(86, 24);
            this.MyCancelButton.TabIndex = 19;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // FilteringOptionsDialog
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(447, 279);
            this.Controls.Add(this.panel1);
            this.Name = "FilteringOptionsDialog";
            this.Text = "Filtering Options";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView PlaceOfPurchaseFilterListView;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}