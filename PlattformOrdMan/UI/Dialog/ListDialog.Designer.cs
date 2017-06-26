namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class ListDialog: OrdManForm
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
            this.MyPanel = new System.Windows.Forms.Panel();
            this.CounterLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.MyListView = new Molmed.PlattformOrdMan.UI.View.SortedListView();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.MessagePanel = new System.Windows.Forms.Panel();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.MaxRowsComboBox = new System.Windows.Forms.ComboBox();
            this.MaxRowsLabel = new System.Windows.Forms.Label();
            this.MyPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.MessagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyPanel
            // 
            this.MyPanel.Controls.Add(this.CounterLabel);
            this.MyPanel.Controls.Add(this.CloseButton);
            this.MyPanel.Controls.Add(this.OKButton);
            this.MyPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MyPanel.Location = new System.Drawing.Point(0, 298);
            this.MyPanel.Name = "MyPanel";
            this.MyPanel.Size = new System.Drawing.Size(512, 59);
            this.MyPanel.TabIndex = 0;
            // 
            // CounterLabel
            // 
            this.CounterLabel.AutoSize = true;
            this.CounterLabel.Location = new System.Drawing.Point(12, 3);
            this.CounterLabel.Name = "CounterLabel";
            this.CounterLabel.Size = new System.Drawing.Size(56, 13);
            this.CounterLabel.TabIndex = 4;
            this.CounterLabel.Text = "<Counter>";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(425, 28);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "&Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(15, 28);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "&OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MyListView
            // 
            this.MyListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyListView.Location = new System.Drawing.Point(0, 55);
            this.MyListView.Name = "MyListView";
            this.MyListView.Size = new System.Drawing.Size(512, 243);
            this.MyListView.TabIndex = 1;
            this.MyListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyListView_KeyDown);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.MessagePanel);
            this.TopPanel.Controls.Add(this.MaxRowsComboBox);
            this.TopPanel.Controls.Add(this.MaxRowsLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(512, 55);
            this.TopPanel.TabIndex = 3;
            // 
            // MessagePanel
            // 
            this.MessagePanel.Controls.Add(this.MessageLabel);
            this.MessagePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MessagePanel.Location = new System.Drawing.Point(0, 33);
            this.MessagePanel.Name = "MessagePanel";
            this.MessagePanel.Size = new System.Drawing.Size(512, 22);
            this.MessagePanel.TabIndex = 2;
            this.MessagePanel.Visible = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(6, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(62, 13);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "<Message>";
            // 
            // MaxRowsComboBox
            // 
            this.MaxRowsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaxRowsComboBox.FormattingEnabled = true;
            this.MaxRowsComboBox.Items.AddRange(new object[] {
            "1000",
            "5000",
            "10000",
            "50000",
            "100000",
            "500000"});
            this.MaxRowsComboBox.Location = new System.Drawing.Point(130, 6);
            this.MaxRowsComboBox.Name = "MaxRowsComboBox";
            this.MaxRowsComboBox.Size = new System.Drawing.Size(131, 21);
            this.MaxRowsComboBox.TabIndex = 1;
            this.MaxRowsComboBox.SelectedIndexChanged += new System.EventHandler(this.MaxRowsComboBox_SelectedIndexChanged);
            // 
            // MaxRowsLabel
            // 
            this.MaxRowsLabel.AutoSize = true;
            this.MaxRowsLabel.Location = new System.Drawing.Point(22, 9);
            this.MaxRowsLabel.Name = "MaxRowsLabel";
            this.MaxRowsLabel.Size = new System.Drawing.Size(102, 13);
            this.MaxRowsLabel.TabIndex = 0;
            this.MaxRowsLabel.Text = "Max displayed rows:";
            // 
            // ListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(512, 357);
            this.Controls.Add(this.MyListView);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.MyPanel);
            this.Name = "ListDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeneralList";
            this.Resize += new System.EventHandler(this.ListDialog_Resize);
            this.MyPanel.ResumeLayout(false);
            this.MyPanel.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.MessagePanel.ResumeLayout(false);
            this.MessagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MyPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button OKButton;
        protected Molmed.PlattformOrdMan.UI.View.SortedListView MyListView;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.ComboBox MaxRowsComboBox;
        private System.Windows.Forms.Label MaxRowsLabel;
        private System.Windows.Forms.Label CounterLabel;
        private System.Windows.Forms.Panel MessagePanel;
        private System.Windows.Forms.Label MessageLabel;
    }
}