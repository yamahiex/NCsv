using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCsv.Validations.Tests
{
    [TestClass()]
    public class CsvMaxLengthAttributeTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            var sut = new CsvMaxLengthAttribute(3);
            var context = new CsvValidationContext("foo", 1, "123");
            Assert.IsTrue(sut.Validate(context, out _));
        }

        public void ValidateFailureTest()
        {
            var sut = new CsvMaxLengthAttribute(3);
            var context = new CsvValidationContext("foo", 1, "1234");
            Assert.IsFalse(sut.Validate(context, out string message));
            Assert.AreEqual(CsvMessages.GetMaxLengthError(context, 3), message);
        }
    }
}