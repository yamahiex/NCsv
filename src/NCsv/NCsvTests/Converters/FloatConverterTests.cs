using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsvTests.Converters
{
    [TestClass]
    public class FloatConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new FloatConverter();
            Assert.AreEqual("1000.1", c.ConvertToCsvItem(CreateContext(), 1000.1f));
            Assert.AreEqual("\"1,000.1\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), 1000.1f));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(1000.1f, ConvertToObjectItem("1000.1"));
            Assert.AreEqual(1000.1f, ConvertToObjectItem("1,000.1"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new FloatConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumericConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new FloatConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private float? ConvertToObjectItem(string csvItem)
        {
            var c = new FloatConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (float?)result;
        }

        private CsvConvertContext CreateContext(string name = nameof(Foo.Value))
        {
            var p = typeof(Foo).GetProperty(name);

            if (p == null)
            {
                throw new AssertFailedException();
            }

            return new CsvConvertContext(p, p.Name);
        }

        private class Foo
        {
            public float Value { get; set; }

            [CsvFormat("#,0.0")]
            public float FormattedValue { get; set; }
        }
    }
}
