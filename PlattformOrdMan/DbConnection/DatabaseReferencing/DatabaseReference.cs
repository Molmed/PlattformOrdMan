using PlattformOrdMan.DbConnection.Repositories;
using PlattformOrdMan.Properties;

namespace PlattformOrdMan.DbConnection.DatabaseReferencing
{
    public class DatabaseReference
    {
        private readonly InitialsProvider _initialsProvider;
        private string _dataServerInitialCatalog;
        private static readonly SettingsProvider SettingsProvider = new SettingsProvider();

        public DatabaseReference() :
            this(new InitialsProvider(new EnvironmentRepository()),
            SettingsProvider.Settings["DataServerInitialCatalog"])
        {
        }

        /// <summary>
        /// This constructor is directly called in a test environment only
        /// </summary>
        public DatabaseReference(InitialsProvider initialsProvider, string dataServerInitialCatalog)
        {
            _initialsProvider = initialsProvider;
            _dataServerInitialCatalog = dataServerInitialCatalog;
        }

        public string GenerateDatabaseName()
        {
            const string template = "{INITIALS}";

            if (_dataServerInitialCatalog.Contains(template))
            {
                var devInitials = _initialsProvider.ProvideInitials();
                _dataServerInitialCatalog = _dataServerInitialCatalog.Replace(template, devInitials);
            }
            return _dataServerInitialCatalog;
        }

        public static string DataServerInitialCatalogFromSettings()
        {
            return Settings.Default.DatabaseName;
        }
    }
}
