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
    public class StringConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new StringConverter();
            Assert.AreEqual("\"abc\"", c.ConvertToCsvItem(CreateConvertToCsvItemContext("abc")));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual("abc", ConvertToObjectItem("abc"));
        }

        private string? ConvertToObjectItem(string csvItem)
        {
            var c = new StringConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (string?)result;
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
            public string Value { get; set; } = string.Empty;
        }
    }
}
