using System;

namespace Chiasma.SharedKernel.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        public string GetValue(string envVariable)
        {
            return Environment.GetEnvironmentVariable(envVariable);
        }
    }

    public interface IEnvironmentRepository
    {
        string GetValue(string envVariable);
    }
}
