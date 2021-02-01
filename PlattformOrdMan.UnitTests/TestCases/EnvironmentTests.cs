using NUnit.Framework;
using Chiasma.SharedKernel.Repositories;

namespace Chiasma.UnitTest.TestCases
{
    [TestFixture]
    class EnvironmentTests
    {
        [Test]
        public void TestEnvironmentVariableExist()
        {
            var rep = new EnvironmentRepository();
            var initials = rep.GetValue("INITIALS");
            Assert.IsNotNull(initials, "You must declare an environment variable INITIALS on " +
                "your computer in order to make the dev-environment to work!");
        }
    }
}
