using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using NotVisualBasic.FileIO;

namespace NCsv.Tests
{
    [TestClass()]
    public class CsvSerializerTests
    {
        [TestMethod()]
        public void SerializeTest()
        {
            var examples = CreateExamples();

            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var csv = cs.Serialize(examples);
            Assert.AreEqual(CreateExamplesCsv(), csv);
        }

        [TestMethod()]
        public async Task SerializeAsyncTest()
        {
            var examples = CreateExamples();

            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var csv = await cs.SerializeAsync(examples);
            Assert.AreEqual(CreateExamplesCsv(), csv);
        }

        [TestMethod()]
        public void SerializeWriterTest()
        {
            var examples = CreateExamples();

            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var writer = new StringWriter();
            cs.Serialize(writer, examples);

            Assert.AreEqual(CreateExamplesCsv(), writer.ToString());
        }

        [TestMethod()]
        public async Task SerializeWriterAsyncTest()
        {
            var examples = CreateExamples();

            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var writer = new StringWriter();
            await cs.SerializeAsync(writer, examples);

            Assert.AreEqual(CreateExamplesCsv(), writer.ToString());
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var examples = cs.Deserialize(CreateExamplesCsv());
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public async Task DeserializeAsncTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var examples = await cs.DeserializeAsync(CreateExamplesCsv());
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public void DeserializeReaderTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var reader = new StringReader(CreateExamplesCsv());
            var examples = cs.Deserialize(reader);
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public async Task DeserializeReaderAsyncTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var reader = new StringReader(CreateExamplesCsv());
            var examples = await cs.DeserializeAsync(reader);
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public void DeserializeParserTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var reader = new StringReader(CreateExamplesCsv());
            var examples = cs.Deserialize(new CsvTextFieldParser(reader));
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public async Task DeserializeParserAsyncTest()
        {
            var cs = new CsvSerializer<Example>
            {
                HasHeader = true
            };

            var reader = new StringReader(CreateExamplesCsv());
            var examples = await cs.DeserializeAsync(new CsvTextFieldParser(reader));
            CollectionAssert.AreEqual(CreateExamples(), examples);
        }

        [TestMethod()]
        public void DeserializeErrorTest()
        {
            var cs = new CsvSerializer<ForDesirializeError>();
            Assert.ThrowsException<CsvValidationException>(() => cs.Deserialize("x"));
        }

        [TestMethod()]
        public void ParseErrorTest()
        {
            var cs = new CsvSerializer<Example>();
            Assert.ThrowsException<CsvParseException>(() => cs.Deserialize("\"foo\",\"ba\"r\""));
        }

        private Example[] CreateExamples()
        {
            return new Example[]
            {
                new Example()
                {
                    StringValue = "\"\"foo",
                    DecimalValue = 123456,
                    DateTimeValue = new DateTime(2020, 1, 1),
                    BooleanValue = true,
                    IntValue = 100,
                    NullableDecimalValue = 1000,
                    NullableDateTimeValue = new DateTime(2020, 1, 2),
                    NullableIntValue = 111,
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
                },
                new Example()
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
                    ValueObject = new ValueObject("xyz"),
                    DoubleValue = 10.123,
                    NullableDoubleValue = 111.111,
                    ShortValue = 100,
                    NullableShortValue = 200,
                    LongValue = 10000,
                    NullableLongValue = 20000,
                    FloatValue = 1.1f,
                    NullableFloatValue = 1.2f,
                },
            };
        }

        private string CreateExamplesCsv()
        {
            return
@"CustomName,DecimalValue,DateTimeValue,BooleanValue,IntValue,NullableDecimalValue,NullableDateTimeValue,NullableIntValue,,SeparateIndex,ValueObject,DoubleValue,NullableDoubleValue,ShortValue,NullableShortValue,LongValue,NullableLongValue,FloatValue,NullableFloatValue
""""""""""foo"",""123,456"",2020/01/01,True,100,1000,2020/01/02,111,,bar,abc,10.123,111.111,100,200,10000,20000,1.1,1.2
foo,""123,456"",2020/01/01,True,100,,,,,bar,xyz,10.123,111.111,100,200,10000,20000,1.1,1.2
";
        }

        private class ForDesirializeError
        {
            [CsvColumn(0)]
            public int Value { get; set; }
        }

    }
}