﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsvTests.Converters
{
    [TestClass]
    public class ShortConverterTests
    {
        [TestMethod]
        public void ConvertToCsvItemTest()
        {
            var c = new ShortConverter();
            Assert.AreEqual("1000", c.ConvertToCsvItem(CreateContext(), (short)1000));
            Assert.AreEqual("\"1,000\"", c.ConvertToCsvItem(CreateContext("FormattedValue"), (short)1000));
        }

        [TestMethod]
        public void TryConvertToObjectItemTest()
        {
            Assert.AreEqual((short)1000, ConvertToObjectItem("1000"));
            Assert.AreEqual((short)1000, ConvertToObjectItem("1,000"));
        }

        [TestMethod]
        public void TryConvertToObjectItemFailureTest()
        {
            var c = new ShortConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), "x", out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetNumericConvertError(nameof(Foo.Value)), message);
        }

        [TestMethod]
        public void TryConvertToObjectItemRequireTest()
        {
            var c = new ShortConverter();
            Assert.IsFalse(c.TryConvertToObjectItem(CreateContext(), string.Empty, out object? _, out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetRequiredError(nameof(Foo.Value)), message);
        }

        private  short? ConvertToObjectItem(string csvItem)
        {
            var c = new ShortConverter();
            Assert.IsTrue(c.TryConvertToObjectItem(CreateContext(), csvItem, out object? result, out string _));
            return (short?)result;
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
            public short Value { get; set; }

            [CsvFormat("#,0")]
            public short FormattedValue { get; set; }
        }
    }
}
