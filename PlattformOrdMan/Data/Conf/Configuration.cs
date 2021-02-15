using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using Molmed.PlattformOrdMan.Data;
using Molmed.PlattformOrdMan.Database;

namespace PlattformOrdMan.Data.Conf
{
    public class Configuration
    {
        private StringDictionary MyItems;
        private const string ConfigFileName = "Order_config.XML";

        private enum ConfTables
        { 
            Item,
            PlaceOfPurchaseFilter,
            PostListViewSelectedColumns
        }

        public Configuration()
        {
            MyItems = new StringDictionary();
            PlaceOfPurchaseFilter = new StringCollection();
            PostListViewSelectedColumns = GetPostListViewSelectedColumnsTable(ConfTables.PostListViewSelectedColumns.ToString());

            //			SAVED FOR CREATING THE CONFIGURATION FILE.
            //			MyItems.Add("VersionControlFlag", "true");
            //			MyItems.Add("AsyncTimeout", "0");
            //			MyItems.Add("SyncTimeout", "0");
            //			MyItems.Add("IsolationLevel", "read committed");
            //          MyItems.Add("AllowUnlockFlag", "false");
            //          MyItems.Add("ExperimentBatchSize", "100");
            //          MyItems.Add("ListRowLimit", "65536");
            //          MyItems.Add("ListColumnLimit", "256");
        }

        private static DataTable GetPostListViewSelectedColumnsTable(string name)
        {
            DataTable table;
            DataColumn column;
            table = new DataTable(name);
            column = new DataColumn(PostListViewConfColumns.ColEnumName.ToString(), typeof(string));
            table.Columns.Add(column);

            column = new DataColumn(PostListViewConfColumns.ColSortOrder.ToString(), typeof(int));
            table.Columns.Add(column);

            column = new DataColumn(PostListViewConfColumns.ColWidth.ToString(), typeof(int));
            table.Columns.Add(column);

            return table;
        }

        public static DataTable GetDefaultPostListViewColumns()
        {
            DataTable table;
            DataRow row;
            int width;
            table = GetPostListViewSelectedColumnsTable(ConfTables.PostListViewSelectedColumns.ToString());
            foreach (PostListViewColumn plvc in Enum.GetValues(typeof(PostListViewColumn)))
            {
                if (plvc == PostListViewColumn.Amount)
                {
                    width = 50;
                }
                else
                {
                    width = PlattformOrdManData.LIST_VIEW_COLUMN_CONTENTS_AUTO_WIDTH;
                }
                row = table.NewRow();
                row[PostListViewConfColumns.ColEnumName.ToString()] =  plvc.ToString();
                row[PostListViewConfColumns.ColSortOrder.ToString()] = (int)plvc;
                row[PostListViewConfColumns.ColWidth.ToString()] = width;
                table.Rows.Add(row);
            }
            return table;
        }

        public StringCollection PlaceOfPurchaseFilter { get; set; }

        public DataTable PostListViewSelectedColumns { get; set; }

        public int TimeIntervalForPosts { get; set; }

        public PlaceOfPurchase PlaceOfPurchase { get; set; }

        public bool TimeRestrictionForCompletedPostsOnly { get; set; }


        public bool ShowOnlyEnabledProducts { get; set; }

        public void SaveSettings()
        {
            DataSet dSet;
            object[] rowValues;

            UpdateMyItems();
            dSet = Configuration.CreateSerializeDataSet();

            foreach (string tempKey in MyItems.Keys)
            {
                rowValues = new object[2];
                rowValues[0] = tempKey;
                rowValues[1] = MyItems[tempKey];
                dSet.Tables[ConfTables.Item.ToString()].Rows.Add(rowValues);
            }

            foreach (string pop in PlaceOfPurchaseFilter)
            {
                rowValues = new object[1];
                rowValues[0] = pop;
                dSet.Tables[ConfTables.PlaceOfPurchaseFilter.ToString()].Rows.Add(rowValues);
            }

            if (dSet.Tables.Contains(ConfTables.PostListViewSelectedColumns.ToString()))
            {
                dSet.Tables.Remove(ConfTables.PostListViewSelectedColumns.ToString());
            }
            dSet.Tables.Add(PostListViewSelectedColumns.Copy());

            dSet.WriteXml(System.Windows.Forms.Application.StartupPath + "\\" + ConfigFileName, XmlWriteMode.IgnoreSchema);
        }

        private void UpdateMyItems()
        {
            Set(ConfigurationData.SHOW_ENABLED_PRODUCTS_ONLY, ShowOnlyEnabledProducts.ToString());
            Set(ConfigurationData.TIME_INTERVAL_FOR_POSTS, TimeIntervalForPosts.ToString());
            Set(ConfigurationData.TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY, TimeRestrictionForCompletedPostsOnly.ToString());
            Set(ConfigurationData.PLACE_OF_PURCHASE, PlaceOfPurchase.ToString());
        }

        private static Configuration GetDefaultSettings()
        {
            Configuration config;
            config = new Configuration();
            config.MyItems.Add(ConfigurationData.SHOW_ENABLED_PRODUCTS_ONLY, ConfigurationData.DEFAULT_SHOW_ENABLED_PRODUCTS_ONLY.ToString());
            config.MyItems.Add(ConfigurationData.TIME_INTERVAL_FOR_POSTS, ConfigurationData.DEFAULT_TIME_INTERVAL_FOR_POSTS.ToString());
            config.MyItems.Add(ConfigurationData.TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY, ConfigurationData.DEFAULT_TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY.ToString());
            config.MyItems.Add(ConfigurationData.PLACE_OF_PURCHASE, UserManager.GetCurrentUser().GetPlaceOfPurchaseString());
            config.PlaceOfPurchaseFilter.Add(UserManager.GetCurrentUser().GetPlaceOfPurchaseString());
            if (!config.PlaceOfPurchaseFilter.Contains(PlaceOfPurchase.Other.ToString()) &&
                UserManager.GetCurrentUser().GetPlaceOfPurchase() != PlaceOfPurchase.Research)
            {
                config.PlaceOfPurchaseFilter.Add(PlaceOfPurchase.Other.ToString());
            }
            config.PostListViewSelectedColumns = GetDefaultPostListViewColumns();
            return config;
        }

        public static Configuration GetSettingsFromFile()
        {
            DataSet dSet;
            Configuration config;
            string configFilePath;

            configFilePath = System.Windows.Forms.Application.StartupPath + "\\" + ConfigFileName;

            if (!File.Exists(configFilePath))
            {
                config = GetDefaultSettings();
            }
            else
            {
                config = new Configuration();
                dSet = Configuration.CreateSerializeDataSet();

                dSet.ReadXml(configFilePath, XmlReadMode.IgnoreSchema);
                foreach (DataRow tempRow in dSet.Tables[ConfTables.Item.ToString()].Rows)
                {
                    config.MyItems.Add(tempRow["Key"].ToString(), tempRow["Value"].ToString());
                }
                foreach (DataRow tempRow in dSet.Tables[ConfTables.PlaceOfPurchaseFilter.ToString()].Rows)
                {
                    config.PlaceOfPurchaseFilter.Add(tempRow["Value"].ToString());
                }
                if (config.PlaceOfPurchaseFilter.Count == 0)
                {
                    config.PlaceOfPurchaseFilter.Add(UserManager.GetCurrentUser().GetPlaceOfPurchaseString());
                    if (!config.PlaceOfPurchaseFilter.Contains(PlaceOfPurchase.Other.ToString()) &&
                        UserManager.GetCurrentUser().GetPlaceOfPurchase() != PlaceOfPurchase.Research)
                    {
                        config.PlaceOfPurchaseFilter.Add(PlaceOfPurchase.Other.ToString());
                    }
                }
                config.PostListViewSelectedColumns = dSet.Tables[ConfTables.PostListViewSelectedColumns.ToString()];
                if (config.PostListViewSelectedColumns.Rows.Count == 0)
                {
                    config.PostListViewSelectedColumns = GetDefaultPostListViewColumns();
                }
            }
            config.SetWorkingVariables();
            return config;
        }

        private void SetWorkingVariables()
        {
            ShowOnlyEnabledProducts = (bool)Get(ConfigurationData.SHOW_ENABLED_PRODUCTS_ONLY, typeof(bool));
            TimeIntervalForPosts = (int)Get(ConfigurationData.TIME_INTERVAL_FOR_POSTS, typeof(int));
            TimeRestrictionForCompletedPostsOnly = (bool)Get(ConfigurationData.TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY, typeof(bool));
            PlaceOfPurchase = (PlaceOfPurchase)Get(ConfigurationData.PLACE_OF_PURCHASE, typeof(PlaceOfPurchase));
        }

        private object Get(string key)
        {
            if (MyItems.ContainsKey(key))
            {
                return MyItems[key];
            }
            else
            {
                MyItems.Add(key, GetDefaultValue(key).ToString());
                return GetDefaultValue(key);
            }
        }

        private void Set(string key, string value)
        {
            if (!MyItems.ContainsKey(key))
            {
                throw new Molmed.PlattformOrdMan.Data.Exception.DataException("Attempting to retrieve unknown configuration item " + key + ".");            
            }
            MyItems[key] = value;
        }

        private object GetDefaultValue(string key)
        {
            if (key == ConfigurationData.SHOW_ENABLED_PRODUCTS_ONLY)
            {
                return ConfigurationData.DEFAULT_SHOW_ENABLED_PRODUCTS_ONLY;
            }
            else if (key == ConfigurationData.TIME_INTERVAL_FOR_POSTS)
            {
                return ConfigurationData.DEFAULT_TIME_INTERVAL_FOR_POSTS;
            }
            else if (key == ConfigurationData.TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY)
            {
                return ConfigurationData.DEFAULT_TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY;
            }
            else if (key == ConfigurationData.PLACE_OF_PURCHASE)
            {
                return UserManager.GetCurrentUser().GetPlaceOfPurchase();
            }
            else
            {
                throw new Molmed.PlattformOrdMan.Data.Exception.DataException("Attempting to retrieve unknown configuration item " + key + ".");
            }
        }

        private object Get(string key, Type type)
        {
            int intResult;
            bool boolResult;
            object result;
            if (!MyItems.ContainsKey(key))
            {
                result = GetDefaultValue(key);
                MyItems.Add(key, result.ToString());
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(MyItems[key], out intResult))
                {
                    result = intResult;
                }
                else
                {
                    throw new Molmed.PlattformOrdMan.Data.Exception.DataException("Unable to parse setting " + key + " to an int");
                }
            }
            else if (type == typeof(bool))
            {
                if (bool.TryParse(MyItems[key], out boolResult))
                {
                    result = boolResult;
                }
                else
                {
                    throw new Molmed.PlattformOrdMan.Data.Exception.DataException("Unable to parse setting " + key + "to a boolean");                
                }
            }
            else if (type == typeof(PlaceOfPurchase))
            {
                try
                {
                    result = (PlaceOfPurchase)Enum.Parse(typeof(PlaceOfPurchase), MyItems[key]);
                }
                catch (ArgumentException ex)
                {
                    throw new Molmed.PlattformOrdMan.Data.Exception.DataException("Unable to parse setting " + key + " to the enumerable PlaceOfPurchase", ex);
                }
            }
            else if (type == typeof(string))
            {
                result = MyItems[key];
            }
            else
            {
                throw new Molmed.PlattformOrdMan.Data.Exception.DataException("No handling for type: " + type.ToString());
            }
            return result;
        }

        private static DataSet CreateSerializeDataSet()
        {
            DataSet dSet;
            DataTable table;

            table = new DataTable(ConfTables.Item.ToString());
            table.Columns.Add("Key", typeof(System.String));
            table.Columns.Add("Value", typeof(System.String));

            dSet = new DataSet("Configuration");
            dSet.Tables.Add(table);

            table = new DataTable(ConfTables.PlaceOfPurchaseFilter.ToString());
            table.Columns.Add("Value", typeof(string));
            dSet.Tables.Add(table);

            dSet.Tables.Add(GetPostListViewSelectedColumnsTable(ConfTables.PostListViewSelectedColumns.ToString()));

            return dSet;
        }

    }
}
