using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class NullableDateTimeConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new NullableDateTimeConverter();
            Assert.AreEqual(string.Empty, c.ConvertToCsvItem(CreateConvertToCsvItemContext(null)));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            var c = new NullableDateTimeConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(string.Empty), out object? result, out string _));
            Assert.IsNull(result);
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
            public DateTime? Value { get; set; }
        }
    }
}
