using System;
using System.Collections.Generic;
using System.Text;
using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class DateTimeConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new DateTimeConverter();
            Assert.AreEqual("2020/01/01 10:20:01", c.ConvertToCsvItem(CreateConvertToCsvItemContext(new DateTime(2020, 1, 1, 10, 20, 1))));
            Assert.AreEqual("2020/01/01", c.ConvertToCsvItem(CreateConvertToCsvItemContext(new DateTime(2020, 1, 1, 10, 20, 1), "FormattedValue")));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(new DateTime(2020, 1, 1, 10, 20, 1), ConvertToObjectItem("2020/01/01 10:20:01"));
            Assert.AreEqual(new DateTime(2020, 1, 1), ConvertToObjectItem("2020/01/01", "FormattedValue"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new DateTimeConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetDateTimeConvertError(context), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemFormatFailureTest()
        {
            var c = new DateTimeConverter();
            var context = CreateConvertToObjectItemContext("2020/01/01 10:00:00", "FormattedValue");
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetDateTimeFormatError(context, "yyyy/MM/dd"), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new DateTimeConverter();
            var context = CreateConvertToObjectItemContext(string.Empty);
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetRequiredError(context), message);
        }

        private DateTime? ConvertToObjectItem(string csvItem, string name = nameof(Foo.Value))
        {
            var c = new DateTimeConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem, name), out object? result, out string _));
            return (DateTime?)result;
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
            public DateTime Value { get; set; }

            [CsvFormat("yyyy/MM/dd")]
            public DateTime FormattedValue { get; set; }
        }
    }
}
