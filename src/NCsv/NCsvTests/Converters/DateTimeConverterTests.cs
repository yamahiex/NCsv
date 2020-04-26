using System;
using System.Collections.Generic;
using System.Text;
using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class DateTimeConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new DateTimeConverter();
            Assert.AreEqual("\"2020/01/01 10:20:01\"", c.ConvertToCsvItem(CreateContext(), new DateTime(2020, 1, 1, 10, 20, 1)));
            Assert.AreEqual("\"2020/01/01\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), new DateTime(2020, 1, 1, 10, 20, 1)));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(new DateTime(2020, 1, 1, 10, 20, 1), ConvertToObjectItem("2020/01/01 10:20:01"));
            Assert.AreEqual(new DateTime(2020, 1, 1), ConvertToObjectItem("2020/01/01"));
            Assert.AreEqual(new DateTime(2020, 1, 1), ConvertToObjectItem("20200101"));
            Assert.AreEqual(new DateTime(2020, 1, 1), ConvertToObjectItem("2020/01"));
            Assert.AreEqual(new DateTime(2020, 1, 1), ConvertToObjectItem("202001"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new DateTimeConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetDateTimeConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new DateTimeConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private DateTime? ConvertToObjectItem(string csvItem)
        {
            var c = new DateTimeConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (DateTime?)result;
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
            public DateTime Value { get; set; }

            [CsvFormat("yyyy/MM/dd")]
            public DateTime FormattedValue { get; set; }
        }
    }
}
