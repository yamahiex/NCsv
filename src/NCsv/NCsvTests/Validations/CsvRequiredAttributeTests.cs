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
            var sut = new CsvRequiredAttribute();
            var context = new CsvValidationContext("foo", 1, "x");
            Assert.IsTrue(sut.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateFailureTest()
        {
            var sut = new CsvRequiredAttribute();
            var context = new CsvValidationContext("foo", 1, string.Empty);
            Assert.IsFalse(sut.Validate(context, out string message));
            Assert.AreEqual(CsvMessages.GetRequiredError(context), message);
        }

        [TestMethod()]
        public void ValidateZeroFailureTest()
        {
            var sut = new CsvRequiredAttribute()
            {
                ZeroIsEmpty = true,
            };

            var context = new CsvValidationContext("foo", 1, "0");
            Assert.IsFalse(sut.Validate(context, out string message));
            Assert.AreEqual(CsvMessages.GetRequiredError(context), message);
        }
    }
}