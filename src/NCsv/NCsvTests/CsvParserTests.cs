using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NCsv.Tests
{
    [TestClass()]
    public class CsvParserTests
    {
        [TestMethod()]
        public void CreateHeaderTest()
        {
            var c = CsvParser<Example>.FromType();
            var actual = c.CreateHeader();

            var expected = @"CustomName,DecimalValue,DateTimeValue,BooleanValue,IntValue,NullableDecimalValue,NullableDateTimeValue,NullableIntValue,,SeparateIndex,ValueObject,DoubleValue,NullableDoubleValue,ShortValue,NullableShortValue,LongValue,NullableLongValue,FloatValue,NullableFloatValue";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateCsvLineTest()
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

            var c = CsvParser<Example>.FromType();
            var actual = c.CreateCsvLine(example);

            var expected = "foo,\"123,456\",2020/01/01,False,100,1000,2020/01/01,123,,bar,abc,10.123,111.111,100,200,10000,20000,1.1,1.2";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateCsvLineSpecialValueTest()
        {
            var c = CsvParser<SpecialValue>.FromType();
            var actual = c.CreateCsvLine(new SpecialValue());

            var expected = "\"fo\"\"o\",\"fo\"\"\"\"o\",\"fo\ro\",\"fo\no\",\"fo\r\no\"";

            Assert.AreEqual(expected, actual);
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

            var c = CsvParser<Example>.FromType();
            var actual = c.CreateObject(c.CreateCsvLine(expected));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateObjectSpecialValueTest()
        {
            var expected = new SpecialValue();

            var c = CsvParser<SpecialValue>.FromType();
            var actual = c.CreateObject(c.CreateCsvLine(expected));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateObjectIndexNotFoundTest()
        {
            var c = CsvParser<Example>.FromType();
            Assert.ThrowsException<CsvValidationException>(() => c.CreateObject("foo,123"));
        }

        [TestMethod()]
        public void CreateObjectColumnNotFoundTest()
        {
            var x = new ColumnNotFound()
            {
                Value = "foo",
            };

            Assert.ThrowsException<InvalidOperationException>(() => CsvParser<ColumnNotFound>.FromType());
        }

        [TestMethod()]
        public void CreateObjectIndexDuplicateTest()
        {
            var x = new IndexDuplicate()
            {
                Value1 = "foo",
                Value2 = "bar",
            };

            Assert.ThrowsException<InvalidOperationException>(() => CsvParser<IndexDuplicate>.FromType());
        }

        [TestMethod()]
        public void CreateObjectFailureTest()
        {
            var c = CsvParser<Foo>.FromType();
            Assert.ThrowsException<CsvValidationException>(() => c.CreateObject("x"));
        }

        private class Foo
        {
            [CsvColumn(0)]
            public decimal Value { get; set; }
        }

        private class ColumnNotFound
        {
            public string Value { get; set; } = string.Empty;
        }

        private class IndexDuplicate
        {
            [CsvColumn(0)]
            public string Value1 { get; set; } = string.Empty;

            [CsvColumn(0)]
            public string Value2 { get; set; } = string.Empty;
        }

        private class SpecialValue
        {
            [CsvColumn(0)]
            public string Value1 { get; set; } = "fo\"o";

            [CsvColumn(1)]
            public string Value2 { get; set; } = "fo\"\"o";

            [CsvColumn(2)]
            public string Value3 { get; set; } = "fo\ro";

            [CsvColumn(3)]
            public string Value4 { get; set; } = "fo\no";

            [CsvColumn(4)]
            public string Value5 { get; set; } = "fo\r\no";

            public override bool Equals(object? obj)
            {
                return obj is SpecialValue value &&
                       Value1 == value.Value1 &&
                       Value2 == value.Value2 &&
                       Value3 == value.Value3 &&
                       Value4 == value.Value4 &&
                       Value5 == value.Value5;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Value1, Value2, Value3, Value4, Value5);
            }
        }
    }
}