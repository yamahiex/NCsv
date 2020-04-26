using NCsv;
using NCsv.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvSerializerTests.Converters
{
    [TestClass]
    public class NullableDateTimeConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new NullableDateTimeConverter();
            Assert.AreEqual("\"\"", c.ConvertToCsvItem(CreateContext(), null));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            var c = new NullableDateTimeConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? result, out string _));
            Assert.IsNull(result);
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
            public DateTime? Value { get; set; }
        }
    }
}
