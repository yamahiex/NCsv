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
            var sut = new BoolConverter();
            Assert.AreEqual("True", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(true)));
            Assert.AreEqual("False", sut.ConvertToCsvItem(CreateConvertToCsvItemContext(false)));
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
            var sut = new BoolConverter();
            var context = CreateConvertToObjectItemContext("x");
            Assert.IsFalse(sut.TryConvertToObjectItem(context, out object? _, out string message));
            Assert.AreEqual(CsvMessages.GetBooleanConvertError(context), message);
        }

        private bool? ConvertToObjectItem(string csvItem)
        {
            var sut = new BoolConverter();
            Assert.IsTrue(sut.TryConvertToObjectItem(CreateConvertToObjectItemContext(csvItem), out object? result, out string _));
            return (bool?)result;
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
            public bool Value { get; set; }
        }
    }
}
