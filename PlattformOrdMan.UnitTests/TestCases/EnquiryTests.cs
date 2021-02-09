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
    }
}
