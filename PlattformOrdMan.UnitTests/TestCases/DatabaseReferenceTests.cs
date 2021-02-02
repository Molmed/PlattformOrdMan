using Molmed.PlattformOrdMan.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.DatabaseReferencing;
using Molmed.PlattformOrdMan.DbConnection.Repositories;
using NUnit.Framework;

namespace PlattformOrdMan.UnitTests.TestCases
{

    [TestFixture]
    class DatabaseReferenceTests
    {

        [Test]
        public void CheckDataServerInitialCatalogValue()
        {
            var dataServerInitialCatalog = DatabaseReference.DataServerInitialCatalogFromSettings();
            Assert.AreEqual("GTDB2_devel_{INITIALS}", dataServerInitialCatalog);
        }

        private InitialsProvider CreateMockInitialsProvider(string retValue)
        {
            var environmentMock = new Mock<IEnvironmentRepository>();
            environmentMock.Setup(foo => foo.GetValue("INITIALS")).Returns(retValue);
            return new InitialsProvider(environmentMock.Object);
        }

        [Test]
        public void TestGeneratedDatabaseName()
        {
            DatabaseReference dbReference =
                new DatabaseReference(CreateMockInitialsProvider("test"), "GTDB2_devel_{INITIALS}");
            Assert.AreEqual("GTDB2_devel_test", dbReference.GenerateDatabaseName());
        }

        [Test]
        public void TestNoInitialsVariableIntroduced()
        {
            DatabaseReference dbReference =
                new DatabaseReference(CreateMockInitialsProvider(null), "GTDB2_devel_{INITIALS}");
            Assert.Throws<NoEnvironentVariableForInitialsException>(() => dbReference.GenerateDatabaseName());
        }

        [Test]
        public void TestInitialsVariableEmpty()
        {
            DatabaseReference dbReference =
                new DatabaseReference(CreateMockInitialsProvider(""), "GTDB2_devel_{INITIALS}");
            Assert.Throws<NoEnvironentVariableForInitialsException>(() => dbReference.GenerateDatabaseName());
        }

        [Test]
        public void TestHardCodedDatabaseReference()
        {
            DatabaseReference dbReference =
                new DatabaseReference(CreateMockInitialsProvider(""), "GTDB2_devel");
            Assert.AreEqual("GTDB2_devel", dbReference.GenerateDatabaseName());
        }

    }
}
