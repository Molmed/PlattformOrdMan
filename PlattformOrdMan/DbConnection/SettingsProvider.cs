using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Molmed.PlattformOrdMan.DbConnection
{
    /// <summary>
    /// Explicitly reads the (appname).exe.config file, instead of using
    /// the Properties.Settings.Default values, in order to make it testable
    /// </summary>
    public class SettingsProvider
    {
        public Dictionary<string, string> Settings { get; }

        public SettingsProvider()
        {
            Settings = new Dictionary<string, string>();
            GetSettingsFromFile();
        }

        private void GetSettingsFromFile()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var configFilePath = Path.Combine(dir, "Order.exe.config");
            var doc = XDocument.Load(configFilePath);
            var entry = doc.Root.Element("applicationSettings").Element("PlattformOrdMan.Properties.Settings");
            var settingEntries = entry.Descendants("setting");

            foreach (var settingEntry in settingEntries)
            {
                Settings.Add(settingEntry.Attribute("name").Value, settingEntry.Value);
            }
        }
    }
}
