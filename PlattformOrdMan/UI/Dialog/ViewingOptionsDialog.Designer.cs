namespace PlattformOrdMan.UI.Dialog
{
    partial class ViewingOptionsDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.MyOkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.IncludedColumnsListView = new PlattformOrdMan.UI.View.Base.OrderManListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MyOkButton);
            this.panel1.Controls.Add(this.MyCancelButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.MoveDownButton);
            this.panel1.Controls.Add(this.MoveUpButton);
            this.panel1.Controls.Add(this.IncludedColumnsListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 325);
            this.panel1.TabIndex = 0;
            // 
            // MyOkButton
            // 
            this.MyOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MyOkButton.Location = new System.Drawing.Point(12, 289);
            this.MyOkButton.Name = "MyOkButton";
            this.MyOkButton.Size = new System.Drawing.Size(86, 24);
            this.MyOkButton.TabIndex = 18;
            this.MyOkButton.Text = "OK";
            this.MyOkButton.UseVisualStyleBackColor = true;
            this.MyOkButton.Click += new System.EventHandler(this.MyOkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(471, 289);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(86, 24);
            this.MyCancelButton.TabIndex = 17;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(376, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 82);
            this.panel2.TabIndex = 9;
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
            this.MoveDownButton.Location = new System.Drawing.Point(376, 57);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(39, 39);
            this.MoveDownButton.TabIndex = 8;
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUpButton.Image = global::PlattformOrdMan.Properties.Resources.Oxygen_Icons_org_Oxygen_Actions_go_up;
            this.MoveUpButton.Location = new System.Drawing.Point(376, 12);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(39, 39);
            this.MoveUpButton.TabIndex = 7;
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
            this.IncludedColumnsListView.Location = new System.Drawing.Point(12, 12);
            this.IncludedColumnsListView.MultiSelect = false;
            this.IncludedColumnsListView.Name = "IncludedColumnsListView";
            this.IncludedColumnsListView.Size = new System.Drawing.Size(358, 271);
            this.IncludedColumnsListView.TabIndex = 6;
            this.IncludedColumnsListView.UseCompatibleStateImageBehavior = false;
            this.IncludedColumnsListView.View = System.Windows.Forms.View.Details;
            // 
            // ViewingOptionsDialog
            // 
            this.AcceptButton = this.MyOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(578, 325);
            this.Controls.Add(this.panel1);
            this.Name = "ViewingOptionsDialog";
            this.Text = "Viewing Options";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.Button MoveUpButton;
        private View.Base.OrderManListView IncludedColumnsListView;
        private System.Windows.Forms.Button MyOkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}