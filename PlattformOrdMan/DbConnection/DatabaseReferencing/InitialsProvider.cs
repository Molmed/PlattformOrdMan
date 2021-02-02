using System;
using Molmed.PlattformOrdMan.DbConnection.Repositories;

namespace Molmed.PlattformOrdMan.DatabaseReferencing
{
    public class InitialsProvider
    {
        const string INITIALS_ENV_VAR = "INITIALS";

        private readonly IEnvironmentRepository _repository;

        public InitialsProvider(IEnvironmentRepository repository)
        {
            _repository = repository;
        }

        public string ProvideInitials()
        {
            var devInitials = _repository.GetValue(INITIALS_ENV_VAR);
            if (String.IsNullOrEmpty(devInitials))
            {
                throw new NoEnvironentVariableForInitialsException(INITIALS_ENV_VAR);
            }
            return devInitials;
        }
    }
}
