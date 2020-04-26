using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsvTests.Converters
{
    [TestClass]
    public class NullableIntConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new NullableIntConverter();
            Assert.AreEqual(string.Empty, c.ConvertToCsvItem(CreateContext(), null));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            var c = new NullableIntConverter();
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
            public int? Value { get; set; }
        }
    }
}
