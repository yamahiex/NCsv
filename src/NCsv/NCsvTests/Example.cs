using NCsv;
using NCsv.Converters;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCsv.Tests
{
    public class Example
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

        [CsvColumn(10)]
        [CsvConverter(typeof(ValueObjectConverter))]
        public ValueObject ValueObject { get; set; } = new ValueObject(string.Empty);

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

        public override bool Equals(object? obj)
        {
            return obj is Example example &&
                   StringValue == example.StringValue &&
                   DecimalValue == example.DecimalValue &&
                   DateTimeValue == example.DateTimeValue &&
                   BooleanValue == example.BooleanValue &&
                   IntValue == example.IntValue &&
                   NullableDecimalValue == example.NullableDecimalValue &&
                   NullableDateTimeValue == example.NullableDateTimeValue &&
                   NullableIntValue == example.NullableIntValue &&
                   SeparateIndex == example.SeparateIndex &&
                   EqualityComparer<ValueObject>.Default.Equals(ValueObject, example.ValueObject) &&
                   DoubleValue == example.DoubleValue &&
                   NullableDoubleValue == example.NullableDoubleValue &&
                   ShortValue == example.ShortValue &&
                   NullableShortValue == example.NullableShortValue &&
                   LongValue == example.LongValue &&
                   NullableLongValue == example.NullableLongValue;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(StringValue);
            hash.Add(DecimalValue);
            hash.Add(DateTimeValue);
            hash.Add(BooleanValue);
            hash.Add(IntValue);
            hash.Add(NullableDecimalValue);
            hash.Add(NullableDateTimeValue);
            hash.Add(NullableIntValue);
            hash.Add(SeparateIndex);
            hash.Add(ValueObject);
            hash.Add(DoubleValue);
            hash.Add(NullableDoubleValue);
            hash.Add(ShortValue);
            hash.Add(NullableShortValue);
            hash.Add(LongValue);
            hash.Add(NullableLongValue);
            return hash.ToHashCode();
        }
    }

    public class ValueObject
    {
        private readonly string value;

        public ValueObject(string value)
        {
            this.value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is ValueObject @object &&
                   value == @object.value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }

        public override string ToString()
        {
            return this.value;
        }
    }
    public class ValueObjectConverter : CsvConverter
    {
        // It's optional, but it's implemented for the sake of explanation.
        public override string ConvertToCsvItem(CsvConvertContext context, object? objectItem)
        {
            return $"\"{objectItem}\"";
        }

        public override bool TryConvertToObjectItem(CsvConvertContext context, string csvValue, out object? result, out string message)
        {
            result = new ValueObject(csvValue);
            message = string.Empty;
            return true;
        }
    }
}
