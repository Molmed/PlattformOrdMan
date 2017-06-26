using System;

namespace Molmed.PlattformOrdMan.Data.Exception
{
    public class DataArgumentNullException : DataArgumentException
    {
        public DataArgumentNullException(String parameterName)
            : base(parameterName)
        {
        }

        public override string Message
        {
            get
            {
                return GetMessageBase() + "can not be null";
            }
        }
    }
}
