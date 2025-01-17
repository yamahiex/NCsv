using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Tests
{
    [TestClass()]
    public class CsvColumnAttributeTests
    {
        [TestMethod()]
        public void CsvColumnAttributeTest()
        {
            var sut = new CsvColumnAttribute(0) { Name = "foo" };
            Assert.AreEqual(sut.Index, 0);
            Assert.AreEqual(sut.Name, "foo");
        }
    }
}