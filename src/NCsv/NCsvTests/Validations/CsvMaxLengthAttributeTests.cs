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
            var c = new CsvValidationContext("foo", 1, "123");
            Assert.IsTrue(x.Validate(c, out _));
        }

        public void ValidateFailureTest()
        {
            var x = new CsvMaxLengthAttribute(3);
            var context = new CsvValidationContext("foo", 1, "1234");
            Assert.IsFalse(x.Validate(context, out string message));
            Assert.AreEqual(CsvMessages.GetMaxLengthError(context, 3), message);
        }
    }
}