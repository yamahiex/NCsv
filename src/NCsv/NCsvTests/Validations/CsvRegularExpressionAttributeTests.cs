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
            var sut = new CsvRegularExpressionAttribute("^[a-zA-Z0-9]+$");

            var context = new CsvValidationContext("foo", 1, "halfAlphanumericOnly");
            Assert.IsTrue(sut.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateeFailureTest()
        {
            var sut = new CsvRegularExpressionAttribute("^[0-9]+$");

            var context = new CsvValidationContext("foo", 1, "halfAlphanumericOnly");
            Assert.IsFalse(sut.Validate(context, out string message));
            Assert.AreEqual(CsvMessages.GetInvalidFormatError(context), message);
        }
    }
}