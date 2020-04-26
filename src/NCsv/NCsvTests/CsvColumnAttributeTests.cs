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
            CsvColumnAttribute a = new CsvColumnAttribute(0) { Name = "foo" };
            Assert.AreEqual(a.Index, 0);
            Assert.AreEqual(a.Name, "foo");
        }
    }
}