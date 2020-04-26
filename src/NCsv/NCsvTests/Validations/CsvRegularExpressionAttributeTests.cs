using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvRegularExpressionAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var a = new CsvRegularExpressionAttribute("^[a-zA-Z0-9]+$");

            Assert.IsTrue(a.Validate("halfAlphanumericOnly", "foo", out string _));
        }

        [TestMethod()]
        public void ValidateeFailureTest()
        {
            var a = new CsvRegularExpressionAttribute("^[0-9]+$");

            Assert.IsFalse(a.Validate("halfAlphanumericOnly", "foo", out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetInvalidFormatError("foo"), message);
        }
    }
}