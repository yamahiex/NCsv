using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NCsv.Tests
{
    [TestClass()]
    public class CsvColumnsTests
    {
        [TestMethod()]
        public void CreateHeaderTest()
        {
            var c = new CsvColumns<Example>();
            var actual = c.CreateHeader();

            var expected = @"""CustomName"",""DecimalValue"",""DateTimeValue"",""BooleanValue"",""IntValue"",""NullableDecimalValue"",""NullableDateTimeValue"",""NullableIntValue"","""",""SeparateIndex"",""ValueObject"",""DoubleValue"",""NullableDoubleValue"",""ShortValue"",""NullableShortValue"",""LongValue"",""NullableLongValue"",""FloatValue"",""NullableFloatValue""";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateCsvRowTest()
        {
            var example = new Example()
            {
                StringValue = "foo",
                DecimalValue = 123456,
                DateTimeValue = new DateTime(2020, 1, 1),
                BooleanValue = false,
                IntValue = 100,
                NullableDecimalValue = 1000,
                NullableDateTimeValue = new DateTime(2020, 1, 1),
                NullableIntValue = 123,
                SeparateIndex = "bar",
                ValueObject = new ValueObject("abc"),
                DoubleValue = 10.123,
                NullableDoubleValue = 111.111,
                ShortValue = 100,
                NullableShortValue = 200,
                LongValue = 10000,
                NullableLongValue = 20000,
                FloatValue = 1.1f,
                NullableFloatValue = 1.2f,
            };

            var c = new CsvColumns<Example>();
            var actual = c.CreateCsvLine(example);

            var expected = @"""foo"",""123,456"",""2020/01/01"",""False"",100,1000,""2020/01/01"",123,,""bar"",""abc"",10.123,111.111,100,200,10000,20000,1.1,1.2";

            Assert.AreEqual(expected, actual.ToString());
        }
        [TestMethod()]
        public void CreateObjectTest()
        {
            var expected = new Example()
            {
                StringValue = "foo",
                DecimalValue = 1000,
                DateTimeValue = new DateTime(2020, 1, 1),
                IntValue = 123,
                NullableDecimalValue = 2000,
                NullableDateTimeValue = new DateTime(2020, 1, 2),
                NullableIntValue = 456,
                ValueObject = new ValueObject("abc"),
                DoubleValue = 10.123,
                NullableDoubleValue = 111.111,
            };

            var c = new CsvColumns<Example>();
            var actual = c.CreateObject(ToCsv(expected));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateObjectIndexNotFoundTest()
        {
            var x = new Foo()
            {
                Value = 0m,
            };

            var c = new CsvColumns<Example>();
            Assert.ThrowsException<CsvParseException>(() => c.CreateObject(ToCsv(x)));
        }

        [TestMethod()]
        public void CreateObjectIndexDuplicateTest()
        {
            var x = new IndexDuplicate()
            {
                Value1 = "foo",
                Value2 = "bar",
            };

            var c = new CsvColumns<Example>();
            Assert.ThrowsException<InvalidOperationException>(() => c.CreateObject(ToCsv(x)));
        }

        [TestMethod()]
        public void CreateObjectFailureTest()
        {
            var c = new CsvColumns<Foo>();
            Assert.ThrowsException<CsvParseException>(() => c.CreateObject("x"));
        }

        private string ToCsv<T>(T obj)
        {
            var cs = new CsvSerializer<T>();
            return cs.Serialize(obj);
        }

        private class Foo
        {
            [CsvColumn(0)]
            public decimal Value { get; set; }
        }

        private class IndexDuplicate
        {
            [CsvColumn(0)]
            public string Value1 { get; set; } = string.Empty;

            [CsvColumn(0)]
            public string Value2 { get; set; } = string.Empty;
        }
    }
}