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
    public class ShortConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new ShortConverter();
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateConvertToCsvItemContext((short)1000)));
            Assert.AreEqual("1,000", c.ConvertToCsvItem(CreateConvertToCsvItemContext((short)1000, "FormattedValue")));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual((short)1000, ConvertToObjectItem("1000"));
            Assert.AreEqual((short)1000, ConvertToObjectItem("1,000"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new ShortConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.ValidationMessage.GetNumericConvertError(context), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new ShortConverter();
            var context = CreateConvertToObjectItemContext(string.Empty);
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.ValidationMessage.GetRequiredError(context), message);
        }

        private  short? ConvertToObjectItem(string csvItem)
        {
            var c = new ShortConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (short?)result;
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
            public short Value { get; set; }

            [CsvFormat("#,0")]
            public short FormattedValue { get; set; }
        }
    }
}
