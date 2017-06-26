namespace Molmed.PlattformOrdMan.UI.Dialog
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.lApplicationVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lCurrentUser = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application version: ";
            // 
            // lApplicationVersion
            // 
            this.lApplicationVersion.AutoSize = true;
            this.lApplicationVersion.Location = new System.Drawing.Point(120, 9);
            this.lApplicationVersion.Name = "lApplicationVersion";
            this.lApplicationVersion.Size = new System.Drawing.Size(91, 13);
            this.lApplicationVersion.TabIndex = 1;
            this.lApplicationVersion.Text = "<version number>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current user:";
            // 
            // lCurrentUser
            // 
            this.lCurrentUser.AutoSize = true;
            this.lCurrentUser.Location = new System.Drawing.Point(120, 46);
            this.lCurrentUser.Name = "lCurrentUser";
            this.lCurrentUser.Size = new System.Drawing.Size(75, 13);
            this.lCurrentUser.TabIndex = 3;
            this.lCurrentUser.Text = "<current user>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Developed by Edvard Englund";
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 152);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lCurrentUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lApplicationVersion);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutDialog";
            this.Text = "About PlattformOrdMan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lApplicationVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lCurrentUser;
        private System.Windows.Forms.Label label3;
    }
}