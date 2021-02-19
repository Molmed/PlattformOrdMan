namespace PlattformOrdMan.UI.View
{
    partial class SortedListView
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
            this.MyListView = new System.Windows.Forms.ListView();
            this.MyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyselectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyListView
            // 
            this.MyListView.ContextMenuStrip = this.MyContextMenu;
            this.MyListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyListView.FullRowSelect = true;
            this.MyListView.GridLines = true;
            this.MyListView.Location = new System.Drawing.Point(0, 0);
            this.MyListView.Name = "MyListView";
            this.MyListView.Size = new System.Drawing.Size(237, 150);
            this.MyListView.TabIndex = 0;
            this.MyListView.UseCompatibleStateImageBehavior = false;
            this.MyListView.View = System.Windows.Forms.View.Details;
            this.MyListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MyListView_ColumnClick);
            // 
            // MyContextMenu
            // 
            this.MyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyallToolStripMenuItem,
            this.copyselectionToolStripMenuItem});
            this.MyContextMenu.Name = "MyContextMenu";
            this.MyContextMenu.Size = new System.Drawing.Size(156, 48);
            this.MyContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MyContextMenu_Opening);
            // 
            // copyallToolStripMenuItem
            // 
            this.copyallToolStripMenuItem.Name = "copyallToolStripMenuItem";
            this.copyallToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.copyallToolStripMenuItem.Text = "Copy &all";
            this.copyallToolStripMenuItem.Click += new System.EventHandler(this.copyallToolStripMenuItem_Click);
            // 
            // copyselectionToolStripMenuItem
            // 
            this.copyselectionToolStripMenuItem.Name = "copyselectionToolStripMenuItem";
            this.copyselectionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.copyselectionToolStripMenuItem.Text = "Copy &selection";
            this.copyselectionToolStripMenuItem.Click += new System.EventHandler(this.copyselectionToolStripMenuItem_Click);
            // 
            // SortedListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MyListView);
            this.Name = "SortedListView";
            this.Size = new System.Drawing.Size(237, 150);
            this.MyContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView MyListView;
        public System.Windows.Forms.ContextMenuStrip MyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyselectionToolStripMenuItem;
    }
}
