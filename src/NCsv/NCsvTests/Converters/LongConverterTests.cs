using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsvTests.Converters
{
    [TestClass]
    public class LongConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new LongConverter();
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateContext(), 1000L));
            Assert.AreEqual("\"1,000\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), 1000L));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(1000, ConvertToObjectItem("1000"));
            Assert.AreEqual(1000, ConvertToObjectItem("1,000"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new LongConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetNumericConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new LongConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private long? ConvertToObjectItem(string csvItem)
        {
            var c = new LongConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (long?)result;
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
            public long Value { get; set; }

            [CsvFormat("#,0")]
            public long FormattedValue { get; set; }
        }
    }
}
