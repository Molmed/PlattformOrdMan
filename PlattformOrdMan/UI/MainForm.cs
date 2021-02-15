using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Timers;
using Molmed.PlattformOrdMan.UI.Dialog;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.Repositories;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.UI
{
    public partial class MainForm : Form
    {

        private LoginDialog _loginForm;
        private LoginWithBarcodeDialog _loginWithBarcodeDialog;
        private MerchandiseList _merchandiseList;
        private const string RESEARCH_TAG = "Research";
        private const string PRACTICE_TAG = "practice";
        private const string DEVEL_TAG = "devel";
        private System.Timers.Timer _activityTimer;

        private Int32 _activityCounter;
        private Point _lastMousePos;

        public MainForm()
        {
            InitializeComponent();
            Init();
            PlattformOrdManData.OEventHandler = new OrdManEventHandler();
            PlattformOrdManData.OEventHandler.MyOnSupplierUpdate += ReloadSupplierForMDIChildren;
            PlattformOrdManData.OEventHandler.MyOnSupplierCreate += AddCreatedSupplierToMDIChildren;
            PlattformOrdManData.OEventHandler.MyOnMerchandiseUpdate += ReloadMerchandiseForMDIChildren;
            PlattformOrdManData.OEventHandler.MyOnMerchandiseCreate += AddCreatedMerchandiseToMDIChildren;
            PlattformOrdManData.OEventHandler.MyOnPostUpdate += ReloadPostForMDIChildren;
            PlattformOrdManData.OEventHandler.MyOnPostCreate += AddCreatedPostForMDIChildren;
        }

        private void ReloadSupplierForMDIChildren(Supplier supplier)
        { 
            // Loop through all open forms
            // Check if the form has to be updated
            foreach (Form form in MdiChildren)
            {
                var supplierForm = form as ISupplierForm;
                supplierForm?.ReloadSupplier(supplier);
            }
        }

        private void AddCreatedPostForMDIChildren(Post post)
        {
            foreach (Form form in MdiChildren)
            {
                var postForm = form as IPostForm;
                postForm?.AddCreatedPost(post);
            }
        }

        private void ReloadPostForMDIChildren(Post post)
        {
            foreach (Form form in MdiChildren)
            {
                var postForm = form as IPostForm;
                postForm?.ReloadPost(post);
            }
        }

        private void AddCreatedSupplierToMDIChildren(Supplier supplier)
        {
            // Loop through all open forms
            // Check if the form has to be updated
            foreach (Form form in MdiChildren)
            {
                var supplierForm = form as ISupplierForm;
                supplierForm?.AddCreatedSupplier(supplier);
            }            
        }

        private void ActivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Update activity information.
            if (MousePosition == _lastMousePos)
            {
                _activityCounter++;
            }
            else
            {
                _lastMousePos = MousePosition;
                _activityCounter = 0;
            }

            // Check if it is time for automatic logout.
            if (_activityCounter > Config.GetAutomaticLogoutTimeLimit())
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
            foreach (Form form in MdiChildren)
            {
                var merchandiseForm = form as IMerchandiseForm;
                merchandiseForm?.ReloadMerchandise(merchandise);
            }
        }

        private void AddCreatedMerchandiseToMDIChildren(Merchandise merchandise)
        {
            foreach (Form form in MdiChildren)
            {
                var merchandiseForm = form as IMerchandiseForm;
                merchandiseForm?.AddCreatedMerchandise(merchandise);
            }
        }

        private void Init()
        {
            if (IsNotNull(PlattformOrdManData.Database))
            {
                MerchandiseManager.RefreshCache();
                _merchandiseList = MerchandiseManager.GetMerchandiseFromCache();
                authorityManagementToolStripMenuItem.Visible = UserManager.GetCurrentUser().HasAdministratorRights();
                toolStripSeparator1.Visible = UserManager.GetCurrentUser().HasAdministratorRights();
            }
            Text = Config.GetDialogTitleStandard();
            if (Settings.Default.DatabaseName.Contains(RESEARCH_TAG))
            {
                Text = Text + " (Research)";
            }
            else if(Settings.Default.DatabaseName.Contains(PRACTICE_TAG))                
            {
                Text += " (VALIDATAION)";
                BackgroundImage = Resources.ValidationBackground;
            }
            else if (Settings.Default.DatabaseName.Contains(DEVEL_TAG))
            {
                Text += " (DEVELOPMENT)";
                BackgroundImage = Resources.DevelBackground;
            }
            else
            {
            }
            FormClosed += MainForm_FormClosed;
            _activityTimer = new System.Timers.Timer
            {
                Interval = 1000,
                SynchronizingObject = this
            };
            _activityTimer.Elapsed += ActivityTimer_Elapsed;
            KeyUp += MainForm_KeyUp;
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

        public static void HandleError(String message, Exception exception)
        {
            var errorDialog = new ShowErrorDialog(message, exception);
            errorDialog.ShowDialog();
        }

        private static Boolean IsNotNull(Object testObject)
        {
            return (testObject != null);
        }

        private static Boolean IsNull(Object testObject)
        {
            return (testObject == null);
        }

        private Boolean Login()
        {

            Boolean isLoginOk;

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
                string userBarcode;
                do
                {
                    // Get login information.
                    _loginWithBarcodeDialog = new LoginWithBarcodeDialog();

                    if (_loginWithBarcodeDialog.ShowDialog() == DialogResult.OK)
                    {
                        userBarcode = _loginWithBarcodeDialog.Barcode;
                        _loginWithBarcodeDialog = null;
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
                _activityTimer.Enabled = true;
                _activityCounter = 0;
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
            catch (Exception ex)
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
            Boolean isLoginOk;

            if (Config.GetApplicationMode() == Config.ApplicationMode.Lab)
            {
                // LAB MODE
                // Enable background bar code listening.
                KeyPreview = true;

                if (!LoginDataBase())
                {
                    return false;
                }

                string barcode;
                do
                {
                    // Get login information.
                    _loginWithBarcodeDialog = new LoginWithBarcodeDialog();

                    if (_loginWithBarcodeDialog.ShowDialog() == DialogResult.OK)
                    {
                        barcode = _loginWithBarcodeDialog.Barcode;
                        _loginWithBarcodeDialog = null;
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
                _activityTimer.Enabled = true;
                _activityCounter = 0;
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
                    var version = Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
                                     Assembly.GetExecutingAssembly().GetName().Version.Minor + "." +
                                     Assembly.GetExecutingAssembly().GetName().Version.Build + "." +
                                     Assembly.GetExecutingAssembly().GetName().Version.Revision;

                    // Get the name of the current assembly.
                    var applicationName = Assembly.GetExecutingAssembly().GetName().Name;

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


        private Boolean LoginDataBase(String userName = null, String password = null)
        {
            // Set newLoginInfo to "null" for integrated security, or to a user name and password for manual login.

            try
            {
                // Try to connect to the database.
                var dbProvider = new DatabaseReference(
                    new InitialsProvider(new EnvironmentRepository()),
                    Settings.Default.DatabaseName);

                PlattformOrdManData.Database = new Database.Dataserver(
                    userName, password, dbProvider.GenerateDatabaseName());
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
            _activityTimer.Enabled = false;
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
            try
            {
                if (!SetChildFocus(typeof(ShowSuppliersDialog)))
                {
                    Cursor = Cursors.WaitCursor;
                    var showSuppliersDialog = new ShowSuppliersDialog {MdiParent = this};
                    Cursor = Cursors.Default;
                    showSuppliersDialog.Show();
                }
            }
            catch (Exception ex)
            {
                HandleError("Error when showing supplier list", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void merchandiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SetChildFocus(typeof(ShowMerchandiseDialog)))
                {
                    Cursor = Cursors.WaitCursor;
                    _merchandiseList = MerchandiseManager.GetMerchandiseFromCache();
                    var showMerchandiseDialog = new ShowMerchandiseDialog(_merchandiseList);
                    Cursor = Cursors.Default;
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
                Cursor = Cursors.Default;
            }
        }

        private void authorityManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SetChildFocus(typeof(ShowUserDialog)))
            {
                var showUserDialog = new ShowUserDialog {MdiParent = this};
                showUserDialog.Show();
            }
        }

        private void orderHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SetChildFocus(typeof(ShowOrderHistoryDialog)))
                {
                    Cursor = Cursors.WaitCursor;
                    var showOrderHistoryDialog = new ShowOrderHistoryDialog();
                    Cursor = Cursors.Default;
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
                Cursor = Cursors.Default;
            }
        }

        public void ReloadAllChildren()
        { 
            foreach(Form form in MdiChildren)
            {
                var manForm = form as OrdManForm;
                manForm?.ReloadForm();
            }
        }

        private void refreshAllListsF5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ReloadAllChildren();
            }
            catch (Exception ex)
            {
                HandleError("Error when reloading windows", ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}