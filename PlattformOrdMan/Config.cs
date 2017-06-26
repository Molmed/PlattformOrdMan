using Microsoft.Win32;
using System;
using System.Collections;
using System.Windows.Forms;
using PlattformOrdMan.Properties;
using Molmed.PlattformOrdMan.IO;

namespace Molmed.PlattformOrdMan
{
    public class Config
    {
        public enum ApplicationMode
        {
            Office,
            Lab
        }

        public static ApplicationMode GetApplicationMode()
        {
            if (Settings.Default.ApplicationMode.ToUpper() == "LAB")
            {
                return ApplicationMode.Lab;
            }
            else if (Settings.Default.ApplicationMode.ToUpper() == "OFFICE")
            {
                return ApplicationMode.Office;
            }
            else
            {
                throw new Exception("Unknown application mode setting.");
            }
        }

        public static String GetLatestDirectoryPath()
        {
            // Returns the most recent file dialog path for the current user.
            RegistryKey currentUserKey;
            RegistryKey subKey;
            String path;

            currentUserKey = Registry.CurrentUser;
            subKey = currentUserKey.OpenSubKey(Settings.Default.RegisterRecentPathSubKey, false);
            if (subKey == null)
            {
                // If no setting could be found in the registry, use "C:\"
                path = @"C:\";
            }
            else
            {
                // Get the setting value.
                path = subKey.GetValue(Settings.Default.RegisterRecentPathKey, @"C:\").ToString();
            }

            return path;
        }

        public static void SetLatestDirectoryPath(String path)
        {
            // Sets the most recent file dialog path for the current user.
            RegistryKey currentUserKey;
            RegistryKey subKey;

            currentUserKey = Registry.CurrentUser;
            subKey = currentUserKey.OpenSubKey(Settings.Default.RegisterRecentPathSubKey, true);
            if (subKey == null)
            {
                // Create the subkey if it does not exist.
                currentUserKey.CreateSubKey(Settings.Default.RegisterRecentPathSubKey);

                // Now open the newly created subkey.
                subKey = currentUserKey.OpenSubKey(Settings.Default.RegisterRecentPathSubKey, true);
                if (subKey == null)
                {
                    // Could not open the subkey anyway, no idea to continue.
                    return;
                }
            }

            // Set the value.
            subKey.SetValue(Settings.Default.RegisterRecentPathKey, path);
        }

        public static String GetApplicationPath()
        {
            return Application.StartupPath;
        }

        public static Int32 GetAutomaticLogoutTimeLimit()
        {
            //The number of seconds without (mouse) activity
            //before automatic logout.
            return Settings.Default.AutomaticLogoutTimeLimit;
        }

        public static String GetDialogTitleStandard()
        {
            return Settings.Default.DialogTitleStandard;
        }

    }
}
