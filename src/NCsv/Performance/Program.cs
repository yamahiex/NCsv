using NCsv;
using NCsv.Converters;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var cs = new CsvSerializer<Example>() { HasHeader = true };
            var csv = cs.Serialize(CreateExamples(100000).ToArray());
            cs.Deserialize(csv);
        }

        private static IEnumerable<Example> CreateExamples(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Example()
                {
                    StringValue = "foo",
                    DecimalValue = 123456,
                    DateTimeValue = new DateTime(2020, 1, 1),
                    BooleanValue = true,
                    IntValue = 100,
                    NullableDecimalValue = null,
                    NullableDateTimeValue = null,
                    NullableIntValue = null,
                    SeparateIndex = "bar",
                    DoubleValue = 10.123,
                    NullableDoubleValue = 111.111,
                    ShortValue = 100,
                    NullableShortValue = 200,
                    LongValue = 10000,
                    NullableLongValue = 20000,
                    FloatValue = 1.1f,
                    NullableFloatValue = 1.2f,
                };
            }
        }
        private class Example
        {
            [CsvColumn(0, Name = "CustomName")]
            [CsvMaxLength(10)]
            [CsvRegularExpression("^[a-zA-Z0-9,]+$")]
            public string StringValue { get; set; } = string.Empty;

            [CsvColumn(1)]
            [CsvNumber(10, 3, MinValue = "0", MaxValue = "9999999.999")]
            [CsvFormat("#,0")]
            public decimal DecimalValue { get; set; }

            [CsvColumn(2)]
            [CsvFormat("yyyy/MM/dd")]
            public DateTime DateTimeValue { get; set; }

            [CsvColumn(3)]
            public bool BooleanValue { get; set; }

            [CsvColumn(4)]
            public int IntValue { get; set; }

            [CsvColumn(5)]
            public decimal? NullableDecimalValue { get; set; }

            [CsvColumn(6)]
            [CsvFormat("yyyy/MM/dd")]
            public DateTime? NullableDateTimeValue { get; set; }

            [CsvColumn(7)]
            public int? NullableIntValue { get; set; }

            [CsvColumn(9)]
            public string SeparateIndex { get; set; } = string.Empty;

            [CsvColumn(11)]
            public double DoubleValue { get; set; }

            [CsvColumn(12)]
            public double? NullableDoubleValue { get; set; }

            [CsvColumn(13)]
            public short ShortValue { get; set; }

            [CsvColumn(14)]
            public short? NullableShortValue { get; set; }

            [CsvColumn(15)]
            public long LongValue { get; set; }

            [CsvColumn(16)]
            public long? NullableLongValue { get; set; }

            [CsvColumn(17)]
            public float FloatValue { get; set; }

            [CsvColumn(18)]
            public float? NullableFloatValue { get; set; }
        }
    }
}
