using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;

namespace Molmed.PlattformOrdMan.Data
{
    public class TimeIntervalForPosts : PlattformOrdManData
    {
        private String MyDescription;
        private int MyMonths;
        public TimeIntervalForPosts(DataReader dataReader)
        {
            MyDescription = dataReader.GetString(TimeIntervalsForPostsData.DESCRIPTION);
            MyMonths = dataReader.GetInt32(TimeIntervalsForPostsData.MONTHS);
        }

        public override string ToString()
        {
            return MyDescription;
        }

        public int GetMonths()
        {
            return MyMonths;
        }
    }

    public class TimeIntervalsForPostsList : ArrayList
    {
        public override int Add(object value)
        {
            if (value != null)
            {
                return base.Add(value);
            }
            return -1;
        }

        public override void AddRange(ICollection collection)
        {
            if (collection != null)
            {
                base.AddRange(collection);
            }
        }

        public new TimeIntervalForPosts this[Int32 index]
        {
            get
            {
                return (TimeIntervalForPosts)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }
    }

}
