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
            var sut = new LongConverter();
            Assert.AreEqual("1000", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(1000L)));
            Assert.AreEqual("1,000", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(1000L, "FormattedValue")));
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
            var sut = new LongConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(sut.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetNumericConvertError(context), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var sut = new LongConverter();
            var context = CreateConvertToObjectItemContext(string.Empty);
            Assert.IsFalse(sut.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetRequiredError(context), message);
        }

        private long? ConvertToObjectItem(string csvItem)
        {
            var sut = new LongConverter();
            Assert.IsTrue(sut.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (long?)result;
        }

        private ConvertToCsvItemContext CreateConvertToCsvItemContext(object? objectItem, string name = nameof(Foo.Value))
        {
            var p = GetProperty(name);
            return new ConvertToCsvItemContext(p, p.Name, objectItem);
        }

        private ConvertToObjectItemContext CreateConvertToObjectItemContext(string csvItem, string name = nameof(Foo.Value))
        {
            var p = GetProperty(name);
            return new ConvertToObjectItemContext(p, p.Name, 1, csvItem);
        }

        private CsvProperty GetProperty(string name)
        {
            return new CsvProperty(typeof(Foo), name);
        }

        private class Foo
        {
            public long Value { get; set; }

            [CsvFormat("#,0")]
            public long FormattedValue { get; set; }
        }
    }
}
