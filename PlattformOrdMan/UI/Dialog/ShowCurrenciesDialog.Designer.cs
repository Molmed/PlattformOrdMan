namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class ShowCurrenciesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowCurrenciesDialog));
            this.CurrencyListView = new Molmed.PlattformOrdMan.UI.View.OrderManListView();
            this.label1 = new System.Windows.Forms.Label();
            this.MyCloseButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrencyListView
            // 
            this.CurrencyListView.FullRowSelect = true;
            this.CurrencyListView.GridLines = true;
            this.CurrencyListView.Location = new System.Drawing.Point(12, 42);
            this.CurrencyListView.Name = "CurrencyListView";
            this.CurrencyListView.Size = new System.Drawing.Size(333, 279);
            this.CurrencyListView.TabIndex = 0;
            this.CurrencyListView.UseCompatibleStateImageBehavior = false;
            this.CurrencyListView.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available currencies";
            // 
            // MyCloseButton
            // 
            this.MyCloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCloseButton.Location = new System.Drawing.Point(259, 327);
            this.MyCloseButton.Name = "MyCloseButton";
            this.MyCloseButton.Size = new System.Drawing.Size(86, 24);
            this.MyCloseButton.TabIndex = 2;
            this.MyCloseButton.Text = "Close";
            this.MyCloseButton.UseVisualStyleBackColor = true;
            this.MyCloseButton.Click += new System.EventHandler(this.MyCloseButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(259, 12);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(86, 24);
            this.CreateButton.TabIndex = 3;
            this.CreateButton.Text = "Add new";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // ShowCurrenciesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCloseButton;
            this.ClientSize = new System.Drawing.Size(357, 363);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.MyCloseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrencyListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowCurrenciesDialog";
            this.ShowInTaskbar = false;
            this.Text = "ShowCurrenciesDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Molmed.PlattformOrdMan.UI.View.OrderManListView CurrencyListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MyCloseButton;
        private System.Windows.Forms.Button CreateButton;
    }
}