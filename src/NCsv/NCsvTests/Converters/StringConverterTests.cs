using System;
using System.Collections.Generic;
using System.Text;
using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class StringConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new StringConverter();
            Assert.AreEqual("\"abc\"", c.ConvertToCsvItem(CreateContext(), "abc"));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual("abc", ConvertToObjectItem("abc"));
        }

        private string? ConvertToObjectItem(string csvItem)
        {
            var c = new StringConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (string?)result;
        }

        private CsvConvertContext CreateContext()
        {
            var p = typeof(Foo).GetProperty(nameof(Foo.Value));

            if (p == null)
            {
                throw new AssertFailedException();
            }

            return new CsvConvertContext(p, p.Name);
        }

        private class Foo
        {
            public string Value { get; set; } = string.Empty;
        }
    }
}
