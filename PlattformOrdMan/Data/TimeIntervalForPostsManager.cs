using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;
using Molmed.PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Properties;

namespace Molmed.PlattformOrdMan.Data
{
    public class TimeIntervalForPostsManager : PlattformOrdManData
    {
        private static TimeIntervalsForPostsList MyTimeIntervalsList;
        public TimeIntervalForPostsManager()
        { }

        public static TimeIntervalsForPostsList GetTimeIntervalsForPosts()
        {
            LoadTimeIntervals();
            return MyTimeIntervalsList;
        }

        private static void LoadTimeIntervals()
        {
            DataReader dataReader = null;
            if (IsNull(MyTimeIntervalsList))
            {
                try
                {
                    dataReader = Database.GetTimeIntervalsForPosts();
                    MyTimeIntervalsList = new TimeIntervalsForPostsList();
                    while (dataReader.Read())
                    {
                        MyTimeIntervalsList.Add(new TimeIntervalForPosts(dataReader));
                    }

                }
                catch
                {
                    MyTimeIntervalsList = null;
                    throw;
                }
                finally
                {
                    CloseDataReader(dataReader);
                }
            }
        }
    }
}
