using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class DecimalConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new DecimalConverter();
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateContext(), 1000m));
            Assert.AreEqual("\"1,000\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), 1000m));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(1000m, ConvertToObjectItem("1000"));
            Assert.AreEqual(1000m, ConvertToObjectItem("1,000"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new DecimalConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetNumericConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new DecimalConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private decimal? ConvertToObjectItem(string csvItem)
        {
            var c = new DecimalConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (decimal?)result;
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
            public decimal Value { get; set; }

            [CsvFormat("#,0")]
            public decimal FormattedValue { get; set; }
        }
    }
}
