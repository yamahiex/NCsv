using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvNumberOnlyAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var a = new CsvNumberOnlyAttribute();
            var context = new CsvValidationContext(1, "12345", "foo");
            Assert.IsTrue(a.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateeFailureTest()
        {
            var a = new CsvNumberOnlyAttribute();
            var context = new CsvValidationContext(1, "x", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.ValidationMessage.GetNumberOnlyError(context), message);
        }
    }
}