using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Timers;
using Molmed.PlattformOrdMan.UI.Dialog;
using Molmed.PlattformOrdMan.Data;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.UI
{
    public partial class MainForm : Form
    {

        private LoginDialog MyLoginForm;
        private LoginWithBarcodeDialog MyLoginWithBarcodeDialog;
        private MerchandiseList MyMerchandiseList;
        private const string RESEARCH_TAG = "Research";
        private const string PRACTICE_TAG = "practice";
        private const string DEVEL_TAG = "devel";
        private System.Timers.Timer ActivityTimer;

        private Int32 MyActivityCounter;
        private Point MyLastMousePos;

        public MainForm()
        {
            InitializeComponent();
            Init();
            PlattformOrdManData.OEventHandler = new OrdManEventHandler();
            PlattformOrdManData.OEventHandler.MyOnSupplierUpdate += new OrdManEventHandler.SupplierUpdateReporter(ReloadSupplierForMDIChildren);
            PlattformOrdManData.OEventHandler.MyOnSupplierCreate += new OrdManEventHandler.SupplierCreatedReporter(AddCreatedSupplierToMDIChildren);
            PlattformOrdManData.OEventHandler.MyOnMerchandiseUpdate += new OrdManEventHandler.MerchandiseUpdateReporter(ReloadMerchandiseForMDIChildren);
            PlattformOrdManData.OEventHandler.MyOnMerchandiseCreate += new OrdManEventHandler.MerchandiseCreatedReporter(AddCreatedMerchandiseToMDIChildren);
            PlattformOrdManData.OEventHandler.MyOnPostUpdate += new OrdManEventHandler.PostUpdateReporter(ReloadPostForMDIChildren);
            PlattformOrdManData.OEventHandler.MyOnPostCreate += new OrdManEventHandler.PostCreatedReporter(AddCreatedPostForMDIChildren);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReloadSupplierForMDIChildren(Supplier supplier)
        { 
            // Loop through all open forms
            // Check if the form has to be updated
            foreach (Form form in this.MdiChildren)
            {
                if (form is ISupplierForm)
                {
                    ((ISupplierForm)form).ReloadSupplier(supplier);
                }
            }
        }

        private void AddCreatedPostForMDIChildren(Post post)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is IPostForm)
                {
                    ((IPostForm)form).AddCreatedPost(post);
                }
            }
        }

        private void ReloadPostForMDIChildren(Post post)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is IPostForm)
                {
                    ((IPostForm)form).ReloadPost(post);
                }
            }
        }

        private void AddCreatedSupplierToMDIChildren(Supplier supplier)
        {
            // Loop through all open forms
            // Check if the form has to be updated
            foreach (Form form in this.MdiChildren)
            {
                if (form is ISupplierForm)
                {
                    ((ISupplierForm)form).AddCreatedSupplier(supplier);
                }
            }            
        }

        private void ActivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Update activity information.
            if (MousePosition == MyLastMousePos)
            {
                MyActivityCounter++;
            }
            else
            {
                MyLastMousePos = MousePosition;
                MyActivityCounter = 0;
            }

            // Check if it is time for automatic logout.
            if (MyActivityCounter > Config.GetAutomaticLogoutTimeLimit())
            {
                // Logout.
                Logout();

                // Wait for next login.
                if (!Login())
                {
                    // Close application.
                    Close();
                }
                // else: Keep running application.
            }
        }


        private void ReloadMerchandiseForMDIChildren(Merchandise merchandise)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is IMerchandiseForm)
                {
                    ((IMerchandiseForm)form).ReloadMerchandise(merchandise);
                }
            }
        }

        private void AddCreatedMerchandiseToMDIChildren(Merchandise merchandise)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is IMerchandiseForm)
                {
                    ((IMerchandiseForm)form).AddCreatedMerchandise(merchandise);
                }
            }
        }

        private void Init()
        {
            if (IsNotNull(PlattformOrdManData.Database))
            {
                MerchandiseManager.RefreshCache();
                MyMerchandiseList = MerchandiseManager.GetMerchandiseFromCache();
                authorityManagementToolStripMenuItem.Visible = UserManager.GetCurrentUser().HasAdministratorRights();
                toolStripSeparator1.Visible = UserManager.GetCurrentUser().HasAdministratorRights();
            }
            this.Text = Config.GetDialogTitleStandard();
            if (Settings.Default.DataServerInitialCatalog.Contains(RESEARCH_TAG))
            {
                this.Text = this.Text + " (Research)";
            }
            else if(Settings.Default.DataServerInitialCatalog.Contains(PRACTICE_TAG))                
            {
                this.Text += " (VALIDATAION)";
                this.BackgroundImage = Resources.ValidationBackground;
            }
            else if (Settings.Default.DataServerInitialCatalog.Contains(DEVEL_TAG))
            {
                this.Text += " (DEVELOPMENT)";
                this.BackgroundImage = Resources.DevelBackground;
            }
            else
            {
            }
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            ActivityTimer = new System.Timers.Timer();
            ActivityTimer.Interval = 1000;
            ActivityTimer.SynchronizingObject = this;
            this.ActivityTimer.Elapsed += new System.Timers.ElapsedEventHandler(ActivityTimer_Elapsed);
            this.KeyUp += MainForm_KeyUp;
        }

        void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ReloadAllChildren();
            }
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            if (IsNull(PlattformOrdManData.Configuration))
            {
                throw new Data.Exception.DataException("Trying to save configuration but configuration is not initialized!");
            }
            PlattformOrdManData.Configuration.SaveSettings();
        }

        public static String GetApplicationPath()
        {
            return Application.StartupPath;
        }

        public static void HandleError(String message, Exception exception)
        {
            ShowErrorDialog errorDialog;

            errorDialog = new ShowErrorDialog(message, exception);
            errorDialog.ShowDialog();
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

        private Boolean Login()
        {

            Boolean isLoginOk = false;
            string userBarcode = "";

            if (Config.GetApplicationMode() == Config.ApplicationMode.Lab)
            {
                // LAB MODE
                // Enable background bar code listening.
                KeyPreview = true;

                // Login to the database with Windows Authentication, 
                // that is the current windows user (is not the same as the Chiasma user)
                if (!LoginDataBase())
                {
                    return false;
                }
                do
                {
                    // Get login information.
                    MyLoginWithBarcodeDialog = new LoginWithBarcodeDialog();

                    if (MyLoginWithBarcodeDialog.ShowDialog() == DialogResult.OK)
                    {
                        userBarcode = MyLoginWithBarcodeDialog.Barcode;
                        MyLoginWithBarcodeDialog = null;
                    }
                    else
                    {
                        LogoutDatabase();
                        return false;
                    }

                }   // Try to login to the database.
                while (!SetAuthorityMappingForBarcode(userBarcode));

                UserManager.Refresh();
                Text += " - " + UserManager.GetCurrentUser().GetName();

                // Start the activity timer. It is used for automatic logout in lab mode.
                this.ActivityTimer.Enabled = true;
                MyActivityCounter = 0;
                isLoginOk = true;
            }
            else
            {
                // OFFICE MODE
                isLoginOk = LoginDataBase();
                SetAuthorityMappingFromSysUser();
            }

            // Check that user account is active.
            if (!UserManager.GetCurrentUser().IsAccountActive())
            {
                throw new Exception("User " + UserManager.GetCurrentUser().GetName() + " has been inactivated");
            }

            if (isLoginOk)
            {
                // Cache data.
                // This is done in order to avoid DateReader already open exception.
                PlattformOrdManData.Refresh();
            }
            return isLoginOk;

        }

        private Boolean Login_old_with_user_name()
        {
            // BarCodeController barCodeController;
            Boolean isLoginOk = false;
            String userName;
            String password;

            if (Config.GetApplicationMode() == Config.ApplicationMode.Lab)
            {
                // LAB MODE
                // Enable background bar code listening.
                KeyPreview = true;

                do
                {
                    // Get login information.
                    MyLoginForm = new LoginDialog();

                    // Login by bar code is currently not supported.
                    // barCodeController = new BarCodeController(MyLoginForm);
                    // barCodeController.BarCodeReceived += ExecuteBarCode;
                    if (MyLoginForm.ShowDialog() == DialogResult.OK)
                    {
                        userName = MyLoginForm.GetUserName();
                        password = MyLoginForm.GetPassword();
                        MyLoginForm = null;
                    }
                    else
                    {
                        // The user cancelled.
                        return false;
                    }
                }   // Try to login to the database.
                while (!LoginDataBase(userName, password));

                Text = Config.GetDialogTitleStandard() + " - " + UserManager.GetCurrentUser().GetName();

                // Start the activity timer. It is used for automatic logout in lab mode.
                isLoginOk = true;
            }
            else
            {
                // OFFICE MODE
                isLoginOk = LoginDataBase();
            }

            if (isLoginOk)
            {
                // Cache data.
                // This is done in order to avoid DateReader already open exception.

                PlattformOrdManData.Refresh();
            }
            return isLoginOk;
        }

        private bool SetAuthorityMappingForBarcode(string userBarcode)
        {
            if (!UserManager.IsUserBarcode(userBarcode))
            {
                MessageBox.Show("This barcode could not be linked to a Chiasma user!", "Login failure", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            try
            {
                PlattformOrdManData.BeginTransaction();
                UserManager.SetAuthorityMappingFromBarcode(userBarcode);
                PlattformOrdManData.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                PlattformOrdManData.RollbackTransaction();
                HandleError("Could not logon to database", ex);
            }

            return true;
        }

        private void SetAuthorityMappingFromSysUser()
        {
            UserManager.SetAuthorityMappingFromSysUser();
        }


        public Boolean Login(Boolean versionControl)
        {
            Boolean isLoginOk = false;
            string barcode;
            String version;
            String applicationName;

            if (Config.GetApplicationMode() == Config.ApplicationMode.Lab)
            {
                // LAB MODE
                // Enable background bar code listening.
                KeyPreview = true;

                if (!LoginDataBase())
                {
                    return false;
                }

                do
                {
                    // Get login information.
                    MyLoginWithBarcodeDialog = new LoginWithBarcodeDialog();

                    if (MyLoginWithBarcodeDialog.ShowDialog() == DialogResult.OK)
                    {
                        barcode = MyLoginWithBarcodeDialog.Barcode;
                        MyLoginWithBarcodeDialog = null;
                    }
                    else
                    {
                        // The user cancelled.
                        LogoutDatabase();
                        return false;
                    }
                }   // Try to login to the database.
                while (!SetAuthorityMappingForBarcode(barcode));

                // In lab, a user can be disconnected and another log in while Chiasma is never closed down
                // Current user has to be reloaded ...
                UserManager.Refresh();
                Text +=  " - " + UserManager.GetCurrentUser().GetName();

                // Start the activity timer. It is used for automatic logout in lab mode.
                this.ActivityTimer.Enabled = true;
                MyActivityCounter = 0;
                isLoginOk = true;
            }
            else
            {
                // OFFICE MODE
                isLoginOk = LoginDataBase();
                SetAuthorityMappingFromSysUser();
            }

            if (!UserManager.GetCurrentUser().IsAccountActive())
            {
                throw new Exception("User " + UserManager.GetCurrentUser().GetName() + " has been inactivated");
            }
            PlattformOrdManData.Configuration = Configuration.GetSettingsFromFile();

            if (isLoginOk)
            {
                if (versionControl)
                {
                    // Get version number of the current assembly.
                    version = Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Minor + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Build + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Revision;

                    // Get the name of the current assembly.
                    applicationName = Assembly.GetExecutingAssembly().GetName().Name;

                    // Make sure the current application is allowed to connect.
                    if (!PlattformOrdManData.Database.AuthenticateApplication(applicationName, version))
                    {
                        HandleError("The current version of this program is not allowed to connect to the database.", null);
                        return false;
                    }
                }

                // Cache data.
                // This is done in order to avoid DateReader already open exception.
                PlattformOrdManData.Refresh();
            }

            return isLoginOk;
            
        }


        public Boolean Login_old_with_user_name(Boolean versionControl)
        {
            // BarCodeController barCodeController;
            Boolean isLoginOk = false;
            String userName;
            String password;
            String version;
            String applicationName;

            if (Config.GetApplicationMode() == Config.ApplicationMode.Lab)
            {
                // LAB MODE
                // Enable background bar code listening.
                KeyPreview = true;

                do
                {
                    // Get login information.
                    MyLoginForm = new LoginDialog();

                    // Login by bar code is currently not supported.
                    // barCodeController = new BarCodeController(MyLoginForm);
                    // barCodeController.BarCodeReceived += ExecuteBarCode;
                    if (MyLoginForm.ShowDialog() == DialogResult.OK)
                    {
                        userName = MyLoginForm.GetUserName();
                        password = MyLoginForm.GetPassword();
                        MyLoginForm = null;
                    }
                    else
                    {
                        // The user cancelled.
                        return false;
                    }
                }   // Try to login to the database.
                while (!LoginDataBase(userName, password));

                Text = Config.GetDialogTitleStandard() + " - " + UserManager.GetCurrentUser().GetName();

                // Start the activity timer. It is used for automatic logout in lab mode.
                isLoginOk = true;
            }
            else
            {
                // OFFICE MODE
                isLoginOk = LoginDataBase();
            }

            if (isLoginOk)
            {
                // Cache data.
                // This is done in order to avoid DateReader already open exception.

                if (versionControl)
                {
                    // Get version number of the current assembly.
                    version = Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Minor + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Build + "." +
                              Assembly.GetExecutingAssembly().GetName().Version.Revision;

                    // Get the name of the current assembly.
                    applicationName = Assembly.GetExecutingAssembly().GetName().Name;

                    // Make sure the current application is allowed to connect.
                    if (!PlattformOrdManData.Database.AuthenticateApplication(applicationName, version))
                    {
                        HandleError("The current version of this program is not allowed to connect to the database.", null);
                        return false;
                    }
                }

                PlattformOrdManData.Refresh();
            }
            return isLoginOk;
        }

        private Boolean LoginDataBase()
        {
            return LoginDataBase(null, null);
        }

        private Boolean LoginDataBase(String userName, String password)
        {
            // Set newLoginInfo to "null" for integrated security, or to a user name and password for manual login.

            try
            {
                // Try to connect to the database.
                PlattformOrdManData.Database = new Database.Dataserver(userName, password);
                if (!PlattformOrdManData.Database.Connect())
                {
                    throw new Exception("Could not connect user " + userName + " to database");
                }

            }
            catch (Exception exception)
            {
                HandleError("Unable to connect to the database.", exception);
                return false;
            }
            return true;
        }

        private void Logout()
        {
            // Close the child forms.
            foreach (Form child in MdiChildren)
            {
                child.Close();
            }

            LogoutDatabase();
            Text = Config.GetDialogTitleStandard();
            ActivityTimer.Enabled = false;
            // Reset the login information
        }

        public static void LogoutDatabase()
        {
            // Disconnect the data server.
            if (IsNotNull(PlattformOrdManData.Database))
            {
                UserManager.ReleaseAuthorityMapping();
                PlattformOrdManData.Database.Disconnect();
                PlattformOrdManData.Database = null;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.Show();
        }

        private Boolean SetChildFocus(Type childType)
        {
            foreach (Form child in MdiChildren)
            {
                if (child.GetType().Name == childType.Name)
                {
                    child.Focus();
                    return true;
                }
            }
            return false;
        }

        private void suppliersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSuppliersDialog showSuppliersDialog;
            try
            {
                if (!SetChildFocus(typeof(ShowSuppliersDialog)))
                {
                    this.Cursor = Cursors.WaitCursor;
                    showSuppliersDialog = new ShowSuppliersDialog();
                    showSuppliersDialog.MdiParent = this;
                    this.Cursor = Cursors.Default;
                    showSuppliersDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier list", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void merchandiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMerchandiseDialog showMerchandiseDialog;
            try
            {
                if (!SetChildFocus(typeof(ShowMerchandiseDialog)))
                {
                    this.Cursor = Cursors.WaitCursor;
                    MyMerchandiseList = MerchandiseManager.GetMerchandiseFromCache();
                    showMerchandiseDialog = new ShowMerchandiseDialog(MyMerchandiseList);
                    this.Cursor = Cursors.Default;
                    showMerchandiseDialog.MdiParent = this;
                    showMerchandiseDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing product list", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void authorityManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserDialog showUserDialog;
            if (!SetChildFocus(typeof(ShowUserDialog)))
            {
                showUserDialog = new ShowUserDialog();
                showUserDialog.MdiParent = this;
                showUserDialog.Show();
            }
        }

        private void orderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOrderHistoryDialog showOrderHistoryDialog;
            try
            {
                if (!SetChildFocus(typeof(ShowOrderHistoryDialog)))
                {
                    this.Cursor = Cursors.WaitCursor;
                    showOrderHistoryDialog = new ShowOrderHistoryDialog();
                    this.Cursor = Cursors.Default;
                    showOrderHistoryDialog.MdiParent = this;
                    showOrderHistoryDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing order history", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void ReloadAllChildren()
        { 
            foreach(Form form in this.MdiChildren)
            {
                if (form is OrdManForm)
                {
                    ((OrdManForm)form).ReloadForm();
                }
            }
        }

        private void refreshAllListsF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ReloadAllChildren();
            }
            catch (Exception ex)
            {
                HandleError("Error when reloading windows", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}