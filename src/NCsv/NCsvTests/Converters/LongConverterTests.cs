using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateConvertToCsvItemContext(1000L)));
            Assert.AreEqual("\"1,000\"", c.ConvertToCsvItem(CreateConvertToCsvItemContext(1000L, "FormattedValue")));
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
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetNumericConvertError(context), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new LongConverter();
            var context = CreateConvertToObjectItemContext(string.Empty);
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetRequiredError(context), message);
        }

        private long? ConvertToObjectItem(string csvItem)
        {
            var c = new LongConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (long?)result;
        }

        private ConvertToCsvItemContext CreateConvertToCsvItemContext(object? objectItem, string name = nameof(Foo.Value))
        {
            var p = GetPropertyInfo(name);
            return new ConvertToCsvItemContext(p, p.Name, objectItem);
        }

        private ConvertToObjectItemContext CreateConvertToObjectItemContext(string csvItem, string name = nameof(Foo.Value))
        {
            var p = GetPropertyInfo(name);
            return new ConvertToObjectItemContext(p, p.Name, 1, csvItem);
        }

        private PropertyInfo GetPropertyInfo(string name)
        {
            var p = typeof(Foo).GetProperty(name);

            if (p == null)
            {
                throw new AssertFailedException();
            }

            return p;
        }

        private class Foo
        {
            public long Value { get; set; }

            [CsvFormat("#,0")]
            public long FormattedValue { get; set; }
        }
    }
}
