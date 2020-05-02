using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class IntConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new IntConverter();
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateContext(), 1000));
            Assert.AreEqual("\"1,000\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), 1000));
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
            var c = new IntConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumericConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new IntConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private int? ConvertToObjectItem(string csvItem)
        {
            var c = new IntConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (int?)result;
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
            public int Value { get; set; }

            [CsvFormat("#,0")]
            public int FormattedValue { get; set; }
        }
    }
}
