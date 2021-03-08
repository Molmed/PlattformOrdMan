using System;

namespace PlattformOrdMan.Data.Exception
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message): base(message)
        {
            
        }
    }
}
