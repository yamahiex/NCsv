using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvRequiredAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var a = new CsvRequiredAttribute();
            var context = new CsvValidationContext(1, "x", "foo");
            Assert.IsTrue(a.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateFailureTest()
        {
            var a = new CsvRequiredAttribute();
            var context = new CsvValidationContext(1, string.Empty, "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError(context), message);
        }

        [TestMethod()]
        public void ValidateZeroFailureTest()
        {
            var a = new CsvRequiredAttribute()
            {
                ZeroIsEmpty = true,
            };

            var context = new CsvValidationContext(1, "0", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError(context), message);
        }
    }
}