using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Exception;

namespace PlattformOrdMan.UnitTests.TestCases
{
    [TestFixture]
    class EnquiryTests
    {
        [Test]
        public void NotHasAnswered_and_HasValue_Throws()
        {
            Assert.Throws<ValidationException>(() =>
            {
                new Enquiry(false, true, null);
            });
        }

        [Test]
        public void NotHasAnswered_and_Value_Throws()
        {
            Assert.Throws<ValidationException>(() => { new Enquiry(false, false, "value"); });
        }

        [Test]
        public void NotHasValue_and_Value_Throws()
        {
            Assert.Throws<ValidationException>(() => { new Enquiry(true, false, "value"); });
        }

        [Test]
        public void TestEqualityTrue()
        {
            var e1 = new Enquiry(true, false, "");
            var e2 = new Enquiry(true, false, "");
            Assert.IsTrue(e1 == e2);
        }

        [Test]
        public void TestEqualityFalse()
        {
            var e1 = new Enquiry(true, false, "");
            var e2 = new Enquiry(false, false, "");
            Assert.IsFalse(e1 == e2);
        }

        [Test]
        public void TestInEqualityTrue()
        {
            var e1 = new Enquiry(true, false, "");
            var e2 = new Enquiry(false, false, "");
            Assert.IsTrue(e1 != e2);
        }

        [Test]
        public void TestInEqualityFalse()
        {
            var e1 = new Enquiry(true, false, "");
            var e2 = new Enquiry(true, false, "");
            Assert.IsFalse(e1 != e2);
        }
    }
}
