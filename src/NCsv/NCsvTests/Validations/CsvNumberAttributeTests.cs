using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvNumberAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MinValue = "0",
                MaxValue = "9999999.999",
            };

            Assert.IsTrue(a.Validate("1234567.123", "foo", out string _));
        }

        [TestMethod()]
        public void ValidateOutOfRangeErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MinValue = "0",
                MaxValue = "999",
            };

            Assert.IsFalse(a.Validate("1000", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberOutOfRangeError("foo", 0, 999), message);
        }

        [TestMethod()]
        public void ValidateUnderErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MinValue = "0",
            };

            Assert.IsFalse(a.Validate("-1", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberMinValueError("foo", 0), message);
        }

        [TestMethod()]
        public void ValidateOverErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MaxValue = "999",
            };

            Assert.IsFalse(a.Validate("1000", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberMaxValueError("foo", 999), message);
        }

        [TestMethod()]
        public void ValidatePrecisionErrorTest()
        {
            var a = new CsvNumberAttribute(10, 0);

            Assert.IsFalse(a.Validate("12345678901", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetPrecisionError("foo", 10), message);
        }

        [TestMethod()]
        public void ValidateScaleErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3);

            Assert.IsFalse(a.Validate("1.1234", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetPrecisionAndScaleError("foo", 10, 3), message);
        }
    }
}