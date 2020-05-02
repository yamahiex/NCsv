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
            Assert.IsTrue(a.Validate("x", "foo", out string _));
        }

        [TestMethod()]
        public void ValidateFailureTest()
        {
            var a = new CsvRequiredAttribute();
            Assert.IsFalse(a.Validate("", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError("foo"), message);
        }

        [TestMethod()]
        public void ValidateZeroFailureTest()
        {
            var a = new CsvRequiredAttribute()
            {
                ZeroIsEmpty = true,
            };

            Assert.IsFalse(a.Validate("0", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError("foo"), message);
        }
    }
}