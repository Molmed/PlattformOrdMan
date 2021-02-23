using System;
using PlattformOrdMan.Database;

namespace PlattformOrdMan.Data
{
    public interface IDataComment
    {
        String GetComment();
        Boolean HasComment();
    }

    public abstract class DataComment : DataIdentity, IDataComment
    {
        private String MyComment;

        public DataComment(string comment, string identifier, int id)
            : base(id, identifier)
        {
            MyComment = comment;
        }

        public DataComment(DataReader dataReader)
            : base(dataReader)
        {
            MyComment = dataReader.GetString(DataCommentData.COMMENT);
        }

        public String GetComment()
        {
            if (IsNull(MyComment))
            {
                return "";
            }
            return MyComment;
        }

        public void SetComment(String comment)
        {
            MyComment = comment;
        }

        public Boolean HasComment()
        {
            return IsNotEmpty(MyComment);
        }
    }
}
