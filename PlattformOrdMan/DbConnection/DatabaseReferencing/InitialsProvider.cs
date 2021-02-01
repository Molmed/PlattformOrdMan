using System;
using Chiasma.SharedKernel.Repositories;

namespace Chiasma.SharedKernel.DatabaseReferencing
{
    public class InitialsProvider
    {
        const string InitialsEnvVar = "INITIALS";

        private readonly IEnvironmentRepository repository;

        public InitialsProvider(IEnvironmentRepository repository)
        {
            this.repository = repository;
        }

        public string ProvideInitials()
        {
            var devInitials = repository.GetValue(InitialsEnvVar);
            if (String.IsNullOrEmpty(devInitials))
            {
                throw new NoEnvironentVariableForInitialsException(InitialsEnvVar);
            }
            return devInitials;
        }
    }
}
