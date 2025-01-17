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
    public class NullableShortConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var sut = new NullableShortConverter();
            Assert.AreEqual(string.Empty, sut.ConvertToCsvItem(CreateConvertToCsvItemContext(null)));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            var sut = new NullableShortConverter();
            Assert.IsTrue(sut.TryConvertToObjectItem(CreateConvertToObjectItemContext(string.Empty), out object? result, out string _));
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
            public short? Value { get; set; }
        }
    }
}
