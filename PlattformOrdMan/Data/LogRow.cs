using System;
using System.Collections;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public class LogRow
    {
        private String MyIdentifier;
        private String MyOperation;
        private DateTime MyDateTime;
        private User MyUser;

        public LogRow(DataReader dataReader)
        {
            MyDateTime = dataReader.GetDateTime(LogRowData.DATE_TIME);
            MyIdentifier = dataReader.GetString(LogRowData.IDENTIFIER);
            MyOperation = dataReader.GetString(LogRowData.OPERATION);
            MyUser = UserManager.GetUser(dataReader.GetInt32(LogRowData.USER_ID));
        }

        public DateTime GetDateTime()
        {
            return MyDateTime;
        }

        public String GetIdentifier()
        {
            return MyIdentifier;
        }

        public String GetOperation()
        {
            return MyOperation;
        }

        public User GetUser()
        {
            return MyUser;
        }
    }

    public class LogList : ArrayList
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

        public new LogRow this[Int32 index]
        {
            get
            {
                return (LogRow)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }
    }
}