using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PlattformOrdMan.Data;
using PlattformOrdMan.IO;

namespace PlattformOrdMan.UI.Controller
{
    public class CopyListViewMenu : PlattformOrdManBase
    {
        private ListView MyListView;
        private ToolStripMenuItem MyCopyAllRowsMenu;
        private ToolStripMenuItem MyCopyCheckedRowsMenu;
        private ToolStripMenuItem MyCopySelectedRowsMenu;
        private ToolStripMenuItem MySaveToFileMenu;

        public CopyListViewMenu(ListView listView)
        {
            MyListView = listView;

            // Init context menu.
            if (IsNull(MyListView.ContextMenuStrip))
            {
                MyListView.ContextMenuStrip = new ContextMenuStrip();
            }
            MyListView.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenu_Opening);

            // Add copy all rows menu.
            MyCopyAllRowsMenu = new ToolStripMenuItem("Copy all rows");
            MyCopyAllRowsMenu.Click += new EventHandler(ContextMenuCopyAll_Click);
            MyListView.ContextMenuStrip.Items.Add(MyCopyAllRowsMenu);

            // Add copy checked rows menu.
            if (MyListView.CheckBoxes)
            {
                MyCopyCheckedRowsMenu = new ToolStripMenuItem("Copy checked rows");
                MyCopyCheckedRowsMenu.Click += new EventHandler(ContextMenuCopyCheckedRows_Click);
                MyListView.ContextMenuStrip.Items.Add(MyCopyCheckedRowsMenu);
            }

            // Add copy selected rows menu
            MyCopySelectedRowsMenu = new ToolStripMenuItem("Copy selected rows");
            MyCopySelectedRowsMenu.Click += new EventHandler(ContextMenuCopySelectedRows_Click);
            MyListView.ContextMenuStrip.Items.Add(MyCopySelectedRowsMenu);

            // Add save to file menu
            MySaveToFileMenu = new ToolStripMenuItem("Save to file ...");
            MySaveToFileMenu.Click += new EventHandler(ContextMenuSaveToFileMenu_Click);
            MyListView.ContextMenuStrip.Items.Add(MySaveToFileMenu);
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MyCopyAllRowsMenu.Enabled = IsNotEmpty(MyListView.Items);
                if (MyListView.CheckBoxes)
                {
                    MyCopyCheckedRowsMenu.Enabled = IsNotEmpty(MyListView.CheckedItems);
                }
                MyCopySelectedRowsMenu.Enabled = IsNotEmpty(MyListView.SelectedItems);
                MySaveToFileMenu.Enabled = IsNotEmpty(MyListView.Items);
            }
            catch (Exception exception)
            {
                OrdManForm.HandleError("Failed to open copy context menu", exception);
            }
        }

        private void ContextMenuCopyAll_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(ListViewToString(false, false));
            }
            catch (Exception exception)
            {
                OrdManForm.HandleError("Failed to copy list view", exception);
            }
        }

        private void ContextMenuCopyCheckedRows_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(ListViewToString(false, true));
            }
            catch (Exception exception)
            {
                OrdManForm.HandleError("Failed to copy list view", exception);
            }
        }

        private void ContextMenuCopySelectedRows_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(ListViewToString(true, false));
            }
            catch (Exception exception)
            {
                OrdManForm.HandleError("Failed to copy list view", exception);
            }
        }

        private void ContextMenuSaveToFileMenu_Click(object sender, EventArgs e)
        {
            try
            {
                SaveToFile();
            }
            catch (Exception exception)
            {
                OrdManForm.HandleError("Failed to saving to file", exception);
            }
        }

        private Int32 GetColumnIndex(Int32 displayIndex)
        {
            Int32 columnIndex;

            for (columnIndex = 0; columnIndex < MyListView.Columns.Count; columnIndex++)
            {
                if (displayIndex == MyListView.Columns[columnIndex].DisplayIndex)
                {
                    return columnIndex;
                }
            }
            throw new Exception("Display index not found in column header list");
        }

        private String ListViewToString(Boolean onlySelected, Boolean onlyChecked)
        {
            ArrayList listViewItems;
            Int32 itemIndex, displayColumnIndex;
            ListViewItem listViewItem;
            String headerLine;
            String[] textLines;
            String[] tempColumns;
            String[] headers;
            String finalString = "";
            string replacedNLCRText;
            string replaceCRNLString = "<newline>";

            headers = new String[MyListView.Columns.Count];
            for (displayColumnIndex = 0; displayColumnIndex < MyListView.Columns.Count; displayColumnIndex++)
            {
                headers[displayColumnIndex] = MyListView.Columns[GetColumnIndex(displayColumnIndex)].Text;
            }
            headerLine = String.Join("\t", headers);

            listViewItems = new ArrayList();
            if (onlySelected)
            {
                listViewItems.AddRange(MyListView.SelectedItems);
            }
            else
            {
                if (onlyChecked)
                {
                    listViewItems.AddRange(MyListView.CheckedItems);
                }
                else
                {
                    listViewItems.AddRange(MyListView.Items);
                }
            }
            textLines = new String[listViewItems.Count];
            for (itemIndex = 0; itemIndex < listViewItems.Count; itemIndex++)
            {
                listViewItem = (ListViewItem)(listViewItems[itemIndex]);
                tempColumns = new String[listViewItem.SubItems.Count];
                for (displayColumnIndex = 0; displayColumnIndex < listViewItem.SubItems.Count; displayColumnIndex++)
                {
                    replacedNLCRText = listViewItem.SubItems[GetColumnIndex(displayColumnIndex)].Text;
                    replacedNLCRText = replacedNLCRText.Replace(Environment.NewLine, replaceCRNLString);
                    tempColumns[displayColumnIndex] = replacedNLCRText;
                }
                textLines[itemIndex] = String.Join("\t", tempColumns);
            }

            finalString = headerLine + Environment.NewLine;
            finalString += String.Join(Environment.NewLine, textLines);
            return finalString;
        }

        private void SaveToFile()
        {
            // Export list view items to text file.
            ExportDelimitedText file;

            // Open file.
            file = new ExportDelimitedText();
            if (file.OpenFile())
            {
                try
                {
                    // Write list view to file.
                    file.Write(ListViewToString(false, false));
                }
                finally
                {
                    file.CloseFile();
                }
            }
            // else The user canceled the operation.
        }
    }
}
