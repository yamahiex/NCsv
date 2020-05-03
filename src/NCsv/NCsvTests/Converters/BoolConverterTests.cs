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
    public class BoolConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new BoolConverter();
            Assert.AreEqual("\"True\"", c.ConvertToCsvItem(CreateConvertToCsvItemContext(true)));
            Assert.AreEqual("\"False\"", c.ConvertToCsvItem(CreateConvertToCsvItemContext(false)));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual(true, ConvertToObjectItem("True"));
            Assert.AreEqual(true, ConvertToObjectItem("1"));
            Assert.AreEqual(false, ConvertToObjectItem(string.Empty));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new BoolConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(c.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvConfig.Current.Message.GetBooleanConvertError(context), message);
        }

        private bool? ConvertToObjectItem(string csvItem)
        {
            var c = new BoolConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (bool?)result;
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
            public bool Value { get; set; }
        }
    }
}
