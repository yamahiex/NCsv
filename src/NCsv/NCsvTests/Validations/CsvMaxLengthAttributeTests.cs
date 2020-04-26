using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvMaxLengthAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var x = new CsvMaxLengthAttribute(3);
            Assert.IsTrue(x.Validate("123", "foo", out _));
        }

        public void ValidateFailureTest()
        {
            var x = new CsvMaxLengthAttribute(3);
            Assert.IsFalse(x.Validate("1234", "foo", out string message));
            Assert.AreEqual(NCsvConfig.Current.Message.GetMaxLengthError("foo", 3), message);
        }
    }
}