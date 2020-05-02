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
            Assert.IsTrue(a.Validate("12345", "foo", out string _));
        }

        [TestMethod()]
        public void ValidateeFailureTest()
        {
            var a = new CsvNumberOnlyAttribute();
            Assert.IsFalse(a.Validate("x", "foo", out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumberOnlyError("foo"), message);
        }
    }
}