using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.Dialog.OptionsDialog
{
    partial class OrderHistoryOptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderHistoryOptionsDialog));
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.PlaceOfPurchaseFilterListView = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.IncludedColumnsListView = new PlattformOrdMan.UI.View.Base.OrderManListView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(471, 308);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(86, 24);
            this.MyCancelButton.TabIndex = 15;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // MyOkButton
            // 
            this.MyOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MyOkButton.Location = new System.Drawing.Point(12, 308);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(86, 24);
            this.MyOkButton.TabIndex = 16;
            this.MyOkButton.Text = "OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButton_Click);
            // 
            // PlaceOfPurchaseFilterListView
            // 
            this.PlaceOfPurchaseFilterListView.CheckBoxes = true;
            this.PlaceOfPurchaseFilterListView.GridLines = true;
            this.PlaceOfPurchaseFilterListView.HideSelection = false;
            this.PlaceOfPurchaseFilterListView.Location = new System.Drawing.Point(6, 6);
            this.PlaceOfPurchaseFilterListView.Name = "PlaceOfPurchaseFilterListView";
            this.PlaceOfPurchaseFilterListView.Size = new System.Drawing.Size(265, 135);
            this.PlaceOfPurchaseFilterListView.TabIndex = 17;
            this.PlaceOfPurchaseFilterListView.UseCompatibleStateImageBehavior = false;
            this.PlaceOfPurchaseFilterListView.View = System.Windows.Forms.View.Details;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(545, 278);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.PlaceOfPurchaseFilterListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(537, 252);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filtering Options";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.MoveDownButton);
            this.tabPage2.Controls.Add(this.MoveUpButton);
            this.tabPage2.Controls.Add(this.IncludedColumnsListView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(537, 252);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Viewing Options";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(327, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 82);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 65);
            this.label1.TabIndex = 4;
            this.label1.Text = "Column widths can be \r\nlocked between sessions by\r\nright-clicking column headers\r" +
    "\nand selecting \r\n\'Lock column width\'.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PlattformOrdMan.Properties.Resources.Information_icon2;
            this.pictureBox1.Location = new System.Drawing.Point(7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 37);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDownButton.Image = global::PlattformOrdMan.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_down;
            this.MoveDownButton.Location = new System.Drawing.Point(327, 51);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(39, 39);
            this.MoveDownButton.TabIndex = 2;
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUpButton.Image = global::PlattformOrdMan.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_up;
            this.MoveUpButton.Location = new System.Drawing.Point(327, 6);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(39, 39);
            this.MoveUpButton.TabIndex = 1;
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // IncludedColumnsListView
            // 
            this.IncludedColumnsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IncludedColumnsListView.CheckBoxes = true;
            this.IncludedColumnsListView.EnableColumnSort = true;
            this.IncludedColumnsListView.FullRowSelect = true;
            this.IncludedColumnsListView.GridLines = true;
            this.IncludedColumnsListView.HideSelection = false;
            this.IncludedColumnsListView.Location = new System.Drawing.Point(6, 6);
            this.IncludedColumnsListView.MultiSelect = false;
            this.IncludedColumnsListView.Name = "IncludedColumnsListView";
            this.IncludedColumnsListView.Size = new System.Drawing.Size(315, 240);
            this.IncludedColumnsListView.TabIndex = 0;
            this.IncludedColumnsListView.UseCompatibleStateImageBehavior = false;
            this.IncludedColumnsListView.View = System.Windows.Forms.View.Details;
            // 
            // OrderHistoryOptionsDialog
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(569, 344);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MyOkButton);
            this.Controls.Add(this.MyCancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrderHistoryOptionsDialog";
            this.ShowInTaskbar = false;
            this.Text = "Viewing and Filtering Options";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.ListView PlaceOfPurchaseFilterListView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private OrderManListView IncludedColumnsListView;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}