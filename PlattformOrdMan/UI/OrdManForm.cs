using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Molmed.PlattformOrdMan;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.PostData;
using PlattformOrdMan.UI.Dialog;

namespace PlattformOrdMan.UI
{

    public interface ISupplierForm
    {
        bool HasSupplierLoaded(int supplierId);
        void ReloadSupplier(Supplier supplier);
        void AddCreatedSupplier(Supplier supplier);
    }

    public interface IMerchandiseForm
    {
        bool HasMerchandiseLoaded(int merchandiseId);
        void ReloadMerchandise(Merchandise merchandise);
        void AddCreatedMerchandise(Merchandise merchandise);
    }

    public interface IPostForm
    {
        bool HasPostLoaded(int postId);
        void ReloadPost(Post post);
        void AddCreatedPost(Post post);
    }

    public interface IViewingOptionsForm
    {
        void OnViewingOptionsChanged();
    }

    public partial class OrdManForm : Form
    {
        public enum UpdateMode
        {
            Create,
            Edit
        }

        public enum PostUpdateMode
        {
            Create,
            Edit,
            OrderSign
        }

        protected OrdManForm()
        {
            this.KeyPreview = true;
            this.KeyUp += OrdManForm_KeyUp;
        }

        void OrdManForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && this.IsMdiChild)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    ((MainForm)this.MdiParent).ReloadAllChildren();
                }
                catch (Exception ex)
                {
                    HandleError("Error when relaoding forms", ex);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        internal static void AddMenuItem(Control control,
                              String menuIdentifier,
                              EventHandler eventHandler)
        {
            ToolStripItem menuItem;

            if (IsNull(control.ContextMenuStrip))
            {
                control.ContextMenuStrip = new ContextMenuStrip();
                control.ContextMenuStrip.ShowImageMargin = false;
            }

            if (IsNull(eventHandler))
            {
                // Add separator menu.
                menuItem = new ToolStripSeparator();
                menuItem.Name = menuIdentifier;
                control.ContextMenuStrip.Items.Add(menuItem);
            }
            else
            {
                // Add menu.
                menuItem = new ToolStripMenuItem(menuIdentifier);
                menuItem.Click += new EventHandler(eventHandler);
                menuItem.Name = menuIdentifier;
                control.ContextMenuStrip.Items.Add(menuItem);
            }
        }

        internal static ToolStripMenuItem AddMenuItem2(Control control,
                                      String menuIdentifier,
                                      EventHandler eventHandler)
        {
            // User is given the choice to add submenuitems
            ToolStripMenuItem menuItem = new ToolStripMenuItem(menuIdentifier);

            if (IsNull(control.ContextMenuStrip))
            {
                control.ContextMenuStrip = new ContextMenuStrip();
                control.ContextMenuStrip.ShowImageMargin = false;
            }

            if (IsNull(eventHandler))
            {
                // This menu item has subitems, no eventhandler is declared
                menuItem.Name = menuIdentifier;
                control.ContextMenuStrip.Items.Add(menuItem);
            }
            else
            {
                // Add menu.
                menuItem.Click += new EventHandler(eventHandler);
                menuItem.Name = menuIdentifier;
                control.ContextMenuStrip.Items.Add(menuItem);
            }
            return menuItem;
        }

        internal static ToolStripMenuItem AddMenuItem2(Control control,
                                     String menuIdentifier,
                                     EventHandler eventHandler,
                                     Boolean Separator)
        {
            // User is given the choice to add submenuitems
            ToolStripMenuItem menuItem = new ToolStripMenuItem(menuIdentifier);
            ToolStripSeparator separator;
            if (IsNull(control.ContextMenuStrip))
            {
                control.ContextMenuStrip = new ContextMenuStrip();
                control.ContextMenuStrip.ShowImageMargin = false;
            }

            if (IsNull(eventHandler))
            {
                switch (Separator)
                {
                    case true:
                        // Add separator menu.
                        separator = new ToolStripSeparator();
                        separator.Name = menuIdentifier;
                        control.ContextMenuStrip.Items.Add(separator);
                        break;
                    case false:
                        // This menu item has subitems, no eventhandler is declared
                        menuItem.Name = menuIdentifier;
                        control.ContextMenuStrip.Items.Add(menuItem);
                        break;
                }
            }
            else
            {
                // Add menu.
                menuItem.Click += new EventHandler(eventHandler);
                menuItem.Name = menuIdentifier;
                control.ContextMenuStrip.Items.Add(menuItem);
            }
            return menuItem;
        }

        internal static ToolStripMenuItem AddMenuItem2(ToolStripMenuItem parent,
                                      String menuIdentifier,
                                      EventHandler eventHandler)
        {
            // User is given the choice to add submenuitems
            ToolStripMenuItem menuItem = new ToolStripMenuItem(menuIdentifier);

            if (parent.DropDown.Items.Count == 0)
            {
                parent.DropDown = new ContextMenuStrip();
                ((ContextMenuStrip)parent.DropDown).ShowImageMargin = false;
            }

            if (IsNull(eventHandler))
            {
                // This menu item has subitems, no eventhandler is declared
                menuItem.Name = menuIdentifier;
            }
            else
            {
                // Add menu.
                menuItem.Click += new EventHandler(eventHandler);
                menuItem.Name = menuIdentifier;
            }
            parent.DropDownItems.AddRange(new ToolStripMenuItem[]{
            menuItem
            });
            return menuItem;
        }

        internal static ToolStripMenuItem AddMenuItem2(ToolStripMenuItem parent,
                                     String menuIdentifier,
                                     EventHandler eventHandler,
                                     Boolean Separator)
        {
            // User is given the choice to add submenuitems
            ToolStripMenuItem menuItem = new ToolStripMenuItem(menuIdentifier);
            ToolStripSeparator sep;

            if (parent.DropDown.Items.Count == 0)
            {
                parent.DropDown = new ContextMenuStrip();
                ((ContextMenuStrip)parent.DropDown).ShowImageMargin = false;
            }

            if (IsNull(eventHandler))
            {
                switch (Separator)
                {
                    case true:
                        // Add separator menu.
                        sep = new ToolStripSeparator();
                        sep.Name = menuIdentifier;
                        parent.DropDownItems.AddRange(new ToolStripItem[]{
                            sep
                        });
                        break;
                    case false:
                        // This menu item has subitems, no eventhandler is declared
                        menuItem.Name = menuIdentifier;
                        parent.DropDownItems.AddRange(new ToolStripMenuItem[]{
                            menuItem
                        });
                        break;
                }
            }
            else
            {
                // Add menu.
                menuItem.Click += new EventHandler(eventHandler);
                menuItem.Name = menuIdentifier;
                parent.DropDownItems.AddRange(new ToolStripMenuItem[]{
                    menuItem
                });

            }

            return menuItem;
        }

        protected bool GetPrizeDecimal(String prizeStr, out decimal prize)
        {
            string decimalSymbol = PlattformOrdManData.MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            NumberStyles style = NumberStyles.Float;
            prize = 0;
            prizeStr = prizeStr.Replace(",", decimalSymbol);
            prizeStr = prizeStr.Replace(".", decimalSymbol);
            if (!Decimal.TryParse(prizeStr, style, PlattformOrdManData.MyCultureInfo, out prize))
            {
                if (!IsEmpty(prizeStr))
                {
                    return false;
                }
            }
            return true;
        }

        public static void HandleError(String message, Exception exception)
        {
            ShowErrorDialog errorDialog;

            errorDialog = new ShowErrorDialog(message, exception);
            errorDialog.ShowDialog();
        }


        protected String GetPrizeString(String priceString)
        {
            int firstInd = -1, lastInd = -1;
            // Find indices for numeric characters
            // Extract the numeric sequence and parse it to decimal
            for (int i = 0; i < priceString.Length; i++)
            {
                if (firstInd == -1 && (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ','))
                {
                    firstInd = i;
                }
                if (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ',')
                {
                    lastInd = i;
                }
            }
            if (firstInd > -1 && lastInd > -1)
            {
                return priceString.Substring(firstInd, lastInd - firstInd + 1);
            }
            return "";
        }


        protected static Boolean AreEqual(String testString1, String testString2)
        {
            if (IsNull(testString1))
            {
                testString1 = "";
            }
            if (IsNull(testString2))
            {
                testString2 = "";
            }
            return String.Compare(testString1, testString2, true) == 0;
        }

        protected static Boolean AreNotEqual(String testString1, String testString2)
        {
            if (IsNull(testString1))
            {
                testString1 = "";
            }
            if (IsNull(testString2))
            {
                testString2 = "";
            }
            return String.Compare(testString1, testString2, true) != 0;
        }

        protected static Boolean IsEmpty(ICollection collection)
        {
            return ((collection == null) || (collection.Count == 0));
        }

        protected static Boolean IsEmpty(String testString)
        {
            return (testString == null) || (testString.Trim().Length == 0);
        }

        protected static Boolean IsNotEmpty(ICollection collection)
        {
            return ((collection != null) && (collection.Count > 0));
        }

        protected static Boolean IsNotEmpty(String testString)
        {
            return (testString != null) && (testString.Trim().Length > 0);
        }

        protected static Boolean IsNotNull(Object testObject)
        {
            return (testObject != null);
        }

        protected static Boolean IsNull(Object testObject)
        {
            return (testObject == null);
        }

        protected Boolean IsPasteKeys(KeyEventArgs e)
        {
            return (e.Control && (e.KeyCode == Keys.V));
        }

        internal static void SetSeparatorVisibility(Object sender)
        {
            ContextMenuStrip contextMenu;
            ToolStripItem item;
            List<Int32> hideIndex;
            Int32 currentSeparatorIndex;
            Boolean visibleMenu;

            contextMenu = (ContextMenuStrip)sender;
            hideIndex = new List<Int32>();

            //First reset the visibility of all separators.
            foreach (ToolStripItem tempItem in contextMenu.Items)
            {
                if (tempItem is ToolStripSeparator)
                {
                    tempItem.Visible = true;
                }
            }

            //Find indexes of any separators before the first visible menu item.
            //Go from the top down until the first visible menu item is found.
            for (int i = 0; i < contextMenu.Items.Count; i++)
            {
                item = contextMenu.Items[i];
                if (item is ToolStripMenuItem)
                {
                    if (item.Available)
                    {
                        //A visible menu item is found.
                        break;
                    }
                }
                else if (item is ToolStripSeparator)
                {
                    //Found a separator before any visible menu items,
                    //remember to make it invisible later on.
                    hideIndex.Add(i);
                }
            }

            //Find indexes of any separators after the last visible menu item.
            //Go from the bottom up until the first visible menu item is found.
            for (int i = contextMenu.Items.Count - 1; i >= 0; i--)
            {
                item = contextMenu.Items[i];
                if (item is ToolStripMenuItem)
                {
                    if (item.Available)
                    {
                        //A visible menu item is found.
                        break;
                    }
                }
                else if (item is ToolStripSeparator)
                {
                    //Found a separator before any visible menu items,
                    //remember to make it invisible later on.
                    hideIndex.Add(i);
                }
            }

            //Find indexes of any separators located next to each other without
            //visible menu items inbetween.
            visibleMenu = false;
            currentSeparatorIndex = -1;
            for (int i = 0; i < contextMenu.Items.Count; i++)
            {
                item = contextMenu.Items[i];
                if (item is ToolStripMenuItem)
                {
                    if (item.Available)
                    {
                        //A visible menu item is found.
                        visibleMenu = true;
                    }
                }
                else if (item is ToolStripSeparator)
                {
                    if (!visibleMenu)
                    {
                        if (currentSeparatorIndex > 0)
                        {
                            //Found two consecutive separators, remove the previous one.
                            hideIndex.Add(currentSeparatorIndex);
                        }
                    }
                    currentSeparatorIndex = i;
                    visibleMenu = false;
                }
            }

            //Hide the separators.
            foreach (Int32 i in hideIndex)
            {
                contextMenu.Items[i].Visible = false;
            }
        }

        internal static void SetVisible(Object sender,
                                     String menyKey,
                                     Boolean isVisible)
        {
            ContextMenuStrip contextMenu;

            contextMenu = (ContextMenuStrip)sender;
            contextMenu.Items.Find(menyKey, true)[0].Visible = isVisible;
        }

        internal static void SetTag(Object sender,
                             String menyKey,
                             object tag)
        {
            ContextMenuStrip contextMenu;

            contextMenu = (ContextMenuStrip)sender;
            contextMenu.Items.Find(menyKey, true)[0].Tag = tag;
        }

        public virtual void ReloadForm()
        { 
        
        }

        internal static void SetEnable(Object sender,
                             String menyKey,
                             Boolean isEnabled)
        {
            ContextMenuStrip contextMenu;

            contextMenu = (ContextMenuStrip)sender;
            contextMenu.Items.Find(menyKey, true)[0].Enabled = isEnabled;
        }

        protected void ShowWarning(String message)
        {
            MessageBox.Show(message,
                           Config.GetDialogTitleStandard(),
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation);
        }

        protected static String TrimString(String trimString)
        {
            if (IsEmpty(trimString))
            {
                return null;
            }
            else
            {
                return trimString;
            }
        }

        private struct MenuSeparatorGroups
        {
            private Int32 MyFirstGroup, MySecondGroup;

            public MenuSeparatorGroups(Int32 first, Int32 second)
            {
                MyFirstGroup = first;
                MySecondGroup = second;
            }

            public Int32 First
            {
                get
                {
                    return MyFirstGroup;
                }
            }

            public Int32 Second
            {
                get
                {
                    return MySecondGroup;
                }
            }
        }
    }
}