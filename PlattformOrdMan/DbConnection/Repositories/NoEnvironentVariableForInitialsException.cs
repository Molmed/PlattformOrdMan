using System;

namespace PlattformOrdMan.DbConnection.Repositories
{
    public class NoEnvironentVariableForInitialsException : ApplicationException
    {

        public string EnvironmentVariableName { get; private set; }

        public NoEnvironentVariableForInitialsException(string environmentVariableName)
        {
            EnvironmentVariableName = environmentVariableName;
        }
    }
}
