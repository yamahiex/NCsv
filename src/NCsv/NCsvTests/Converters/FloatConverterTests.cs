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
    public class FloatConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var sut = new FloatConverter();
            Assert.AreEqual("1000.1", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(1000.1f)));
            Assert.AreEqual("1,000.1", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(1000.1f, "FormattedValue")));
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
            var sut = new FloatConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(sut.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetNumericConvertError(context), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var sut = new FloatConverter();
            var context = CreateConvertToObjectItemContext(string.Empty);
            Assert.IsFalse(sut.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetRequiredError(context), message);
        }

        private float? ConvertToObjectItem(string csvItem)
        {
            var sut = new FloatConverter();
            Assert.IsTrue(sut.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (float?)result;
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
            public float Value { get; set; }

            [CsvFormat("#,0.0")]
            public float FormattedValue { get; set; }
        }
    }
}
