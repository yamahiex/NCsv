using System;
using System.Collections.Generic;
using System.Text;
using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class BoolConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new BoolConverter();
            Assert.AreEqual("\"True\"", c.ConvertToCsvItem(CreateContext(), true));
            Assert.AreEqual("\"False\"", c.ConvertToCsvItem(CreateContext(), false));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(true, ConvertToObjectItem("True"));
            Assert.AreEqual(true, ConvertToObjectItem("1"));
            Assert.AreEqual(false, ConvertToObjectItem(string.Empty));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new BoolConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetBooleanConvertError(nameof(Foo.Value)), message);
        }

        private bool? ConvertToObjectItem(string csvItem)
        {
            var c = new BoolConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (bool?)result;
        }

        private CsvConvertContext CreateContext()
        {
            var p = typeof(Foo).GetProperty(nameof(Foo.Value));

            if (p == null)
            {
                throw new AssertFailedException();
            }

            return new CsvConvertContext(p, p.Name);
        }

        private class Foo
        {
            public bool Value { get; set; }
        }
    }
}
