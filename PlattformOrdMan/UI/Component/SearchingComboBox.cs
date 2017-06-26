using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Molmed.PlattformOrdMan.Data;

namespace Molmed.PlattformOrdMan.UI.Component
{
    public partial class SearchingCombobox : ComboBox
    {
        // Classes reffering to this class is advised to use the event
        //      MyControlledSelectedIndexChanged
        //                  instead of
        //      SelectedIndexChanged
        // Because SearchingCombobox_SelectedIndexChanged triggers the event
        // SelectedIndexChanged and a chain of unwanted SelectedIndexChanged events in subscribing
        // classes will follow. 
        public delegate void MyControlledSelectedIndexChanged
            (
            );
        public event MyControlledSelectedIndexChanged OnMyControlledSelectedIndexChanged;
        protected DataIdentityList MyIdentities;
        protected String MyNoSelectionString;
        protected String MySearchInfoString;
        private EventHandler MySelectedIndexChangedEventHandler;
        private KeyEventHandler MyKeyUpEventHandler;
        private bool MyIsSelectedIndexChangeOn;
        protected bool MyShowNoSelectionString;

        public SearchingCombobox()
        {
            InitializeComponent();
            MyIdentities = null;
            MyNoSelectionString = "";
            MySearchInfoString = "";
            MyIsSelectedIndexChangeOn = true;
            MyShowNoSelectionString = true;
        }

        public virtual void Init(DataIdentityList identities, String objectName, bool showNoSelectionString)
        {
            MyShowNoSelectionString = showNoSelectionString;
            MyIdentities = identities;
            MyIdentities.Sort();
            MyNoSelectionString = "No " + objectName + " selected";
            MySearchInfoString = "Search " + objectName + "s ...";
            MySelectedIndexChangedEventHandler = new EventHandler(SearchingCombobox_SelectedIndexChanged);
            this.SelectedIndexChanged += MySelectedIndexChangedEventHandler;
            MyKeyUpEventHandler = new KeyEventHandler(SearchingCombobox_KeyUp);
            this.KeyUp += MyKeyUpEventHandler;
            this.KeyDown += new KeyEventHandler(SearchingCombobox_KeyDown);
            this.MouseClick += new MouseEventHandler(SearchingCombobox_MouseClick);
            this.MouseUp += new MouseEventHandler(SearchingCombobox_MouseUp);
            this.MouseDown += new MouseEventHandler(SearchingCombobox_MouseDown);
            this.Leave +=new EventHandler(SearchingCombobox_Leave);
        }

        private void SearchingCombobox_Leave(object sender, EventArgs e)
        {
            this.OnSelectedIndexChanged(new EventArgs());
        }

        private void SearchingCombobox_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Text != "" && this.SelectedIndex == -1)
            {
                this.Text = "";
            }
        }

        void SearchingCombobox_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.SelectedIndex == -1 && this.DroppedDown == false)
            {
                this.Text = "";
                Cursor.Current = Cursors.Default;
                this.DroppedDown = true;
            }
        }


        private void SearchingCombobox_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.SelectedIndex == -1)
            {
                //this.Text = "";
                //Cursor.Current = Cursors.Default;
                //this.DroppedDown = true;
            }
            if (!MyIsSelectedIndexChangeOn)
            {
                //this.SelectedIndexChanged += MySelectedIndexChangedEventHandler;
                //MyIsSelectedIndexChangeOn = true;
                //this.OnSelectedIndexChanged(new EventArgs());
            }
        }

        private void SearchingCombobox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right)
            {
                MyIsSelectedIndexChangeOn = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DroppedDown = false;
                this.Parent.Select();
                MyIsSelectedIndexChangeOn = true;
                this.LoadIdentitiesWithInfoText();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                MyIsSelectedIndexChangeOn = true;
                this.OnSelectedIndexChanged(new EventArgs());
            }
            else
            {
                MyIsSelectedIndexChangeOn = true;
            }
        }

        private void SearchingCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataIdentity selectedIdentity;
            int id = PlattformOrdManData.NO_ID;
            EventHandlerList eventHandlerList = new EventHandlerList();
            // When an item is chosed, the combobox is reloaded
            // in case the items in the combobox have been reduced because of user searchstring
            if (this.SelectedIndex > -1 && MyIsSelectedIndexChangeOn)
            {
                this.SelectedIndexChanged -= MySelectedIndexChangedEventHandler;
                selectedIdentity = GetSelectedIdentity();
                if (selectedIdentity != null)
                {
                    id = selectedIdentity.GetId();
                }
                this.DroppedDown = false;
                this.LoadIdentities();
                this.SetSelectedIdentity(id);
                // Raise event
                if (OnMyControlledSelectedIndexChanged != null)
                {
                    OnMyControlledSelectedIndexChanged();
                }
                this.Parent.Select();
                this.SelectedIndexChanged += MySelectedIndexChangedEventHandler;
            }
            else if (MyIsSelectedIndexChangeOn)
            {
                // Raise event
                if (OnMyControlledSelectedIndexChanged != null)
                {
                    OnMyControlledSelectedIndexChanged();
                }                
            }
        }

        protected virtual bool IsWithinSearchingCriteria(DataIdentity identity, String searchString)
        {
            if (identity.GetIdentifier().ToLower().Contains(searchString.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void SearchingCombobox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.XButton1 && e.KeyCode != Keys.XButton2 &&
                e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left &&
                e.KeyCode != Keys.Right && e.KeyCode != Keys.Escape && e.KeyCode != Keys.Enter)
            {
                this.Items.Clear();
                this.BeginUpdate();
                if (MyShowNoSelectionString)
                {
                    this.Items.Add(MyNoSelectionString);
                }
                foreach (DataIdentity identity in MyIdentities)
                {
                    if (IsWithinSearchingCriteria(identity, this.Text))
                    {
                        this.Items.Add(identity);
                    }
                }
                this.EndUpdate();
                this.Select(this.Text.Length, 0);
                Cursor.Current = Cursors.Default;
                this.DroppedDown = true;
                this.SelectedIndex = -1;
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.OnSelectedIndexChanged(new EventArgs());
                }
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right)
            {
                MyIsSelectedIndexChangeOn = true;
            }
        }

        public DataIdentity GetSelectedIdentity()
        {
            if ((MyShowNoSelectionString && this.SelectedIndex == 0) ||
                (!MyShowNoSelectionString && this.SelectedIndex == -1))
            {
                return null;
            }
            return (DataIdentity)(this.SelectedItem);
        }

        public int GetSelectedIdentityId()
        {
            if ((MyShowNoSelectionString && this.SelectedIndex == 0) ||
                this.SelectedIndex == -1)
            {
                return PlattformOrdManData.NO_ID;
            }
            return ((DataIdentity)(this.SelectedItem)).GetId();            
        }

        public Boolean HasSelectedIdentity()
        {
            int firstIdentifyIndex;
            if (MyShowNoSelectionString)
            {
                firstIdentifyIndex = 1;
            }
            else
            {
                firstIdentifyIndex = 0;
            }
            return (this.SelectedItem != null && this.SelectedIndex >= firstIdentifyIndex);
        }

        public virtual void LoadIdentities()
        {
            bool triggerDelegates = false;
            if (this.SelectedIndex > -1)
            {
                triggerDelegates = true;
            }
            this.BeginUpdate();
            this.Items.Clear();
            if (MyShowNoSelectionString)
            {
                this.Items.Add(MyNoSelectionString);
            }
            foreach (DataIdentity identity in MyIdentities)
            {
                this.Items.Add(identity);
            }
            this.EndUpdate();
            this.SelectedIndex = -1;
            if (triggerDelegates && OnMyControlledSelectedIndexChanged != null)
            {
                //OnMyControlledSelectedIndexChanged();
            }
        }

        public virtual void LoadIdentitiesWithInfoText()
        {
            LoadIdentities();
            this.Text = MySearchInfoString;
        }

        public void SetSelectedIdentity(DataIdentity identity)
        {
            if (identity != null)
            {
                SetSelectedIdentity(identity.GetId());
            }
            else
            {
                this.SelectedIndex = -1;
            }
        }

        public void ReloadIdentities(DataIdentityList identities)
        {
            int selId;
            selId = this.GetSelectedIdentityId();
            
            MyIdentities = identities;
            LoadIdentities();
            if (selId != PlattformOrdManData.NO_ID)
            {
                this.SetSelectedIdentity(selId);
            }
        }

        public void SetSelectedIdentity(Int32 id)
        {
            DataIdentity s;
            bool itemFound = false;
            int startInd;
            if (MyShowNoSelectionString)
            {
                startInd = 1;
            }
            else
            {
                startInd = 0;
            }
            for(int i = startInd; i < this.Items.Count; i++)
            {
                s = (DataIdentity)this.Items[i];
                if (s.GetId() == id)
                {
                    this.SelectedItem = s;
                    itemFound = true;
                    break;
                }
            }
            if (!itemFound && MyShowNoSelectionString)
            {
                this.SelectedIndex = 0;
            }
            else if (!itemFound && !MyShowNoSelectionString)
            {
                this.SelectedIndex = -1;
            }
        }
    }
}
