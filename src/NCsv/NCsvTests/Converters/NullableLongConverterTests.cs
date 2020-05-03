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
    public class NullableLongConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new NullableLongConverter();
            Assert.AreEqual(string.Empty, c.ConvertToCsvItem(CreateConvertToCsvItemContext(null)));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            var c = new NullableLongConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(string.Empty), out object? result, out string _));
            Assert.IsNull(result);
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
            public long? Value { get; set; }
        }
    }
}
