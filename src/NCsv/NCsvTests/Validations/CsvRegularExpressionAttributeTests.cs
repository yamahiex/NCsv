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

            var context = new CsvValidationContext(1, "halfAlphanumericOnly", "foo");
            Assert.IsTrue(a.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateeFailureTest()
        {
            var a = new CsvRegularExpressionAttribute("^[0-9]+$");

            var context = new CsvValidationContext(1, "halfAlphanumericOnly", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.ValidationMessage.GetInvalidFormatError(context), message);
        }
    }
}