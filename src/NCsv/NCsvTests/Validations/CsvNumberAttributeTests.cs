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

            var context = new CsvValidationContext(1, "1234567.123", "foo");
            Assert.IsTrue(a.Validate(context, out string _));
        }

        [TestMethod()]
        public void ValidateOutOfRangeErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MinValue = "0",
                MaxValue = "999",
            };

            var context = new CsvValidationContext(1, "1000", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberOutOfRangeError(context, 0, 999), message);
        }

        [TestMethod()]
        public void ValidateUnderErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MinValue = "0",
            };

            var context = new CsvValidationContext(1, "-1", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberMinValueError(context, 0), message);
        }

        [TestMethod()]
        public void ValidateOverErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3)
            {
                MaxValue = "999",
            };

            var context = new CsvValidationContext(1, "1000", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberMaxValueError(context, 999), message);
        }

        [TestMethod()]
        public void ValidatePrecisionErrorTest()
        {
            var a = new CsvNumberAttribute(10, 0);

            var context = new CsvValidationContext(1, "12345678901", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetPrecisionError(context, 10), message);
        }

        [TestMethod()]
        public void ValidateScaleErrorTest()
        {
            var a = new CsvNumberAttribute(10, 3);

            var context = new CsvValidationContext(1, "1.1234", "foo");
            Assert.IsFalse(a.Validate(context, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetPrecisionAndScaleError(context, 10, 3), message);
        }
    }
}