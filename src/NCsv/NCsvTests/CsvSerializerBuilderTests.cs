using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCsv;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NCsv.Tests
{
    [TestClass()]
    public class CsvSerializerBuilderTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var sut = new CsvSerializerBuilder<User>();
            sut.AddColumn(3, x => x.ValueObject)
                .Converter(new ValueObjectConverter());
            sut.AddColumn(0, x => x.Name)
                .Name("CustomName")
                .AddValidation(new CsvRequiredAttribute())
                .AddValidation(new CsvMaxLengthAttribute(10));
            sut.AddColumn(1, x => x.Birthday)
                .Format("yyyy/MM/dd");

            var cs = sut.ToCsvSerializer();
            cs.HasHeader = true;

            var user = CreateUser();

            var csv = cs.Serialize(user);
            Assert.AreEqual("CustomName,Birthday,,ValueObject\r\nabc,2000/01/01,,foo\r\n", csv);

            var users = cs.Deserialize(csv);
            CollectionAssert.AreEqual(new User[] { user }, users);
        }

        [TestMethod()]
        public void ValidationTest()
        {
            var sut = new CsvSerializerBuilder<User>();
            sut.AddColumn(0, x => x.Name)
                .Name("CustomName")
                .AddValidation(new CsvRequiredAttribute())
                .AddValidation(new CsvMaxLengthAttribute(1));

            var cs = sut.ToCsvSerializer();

            Assert.ThrowsException<CsvValidationException>(() => cs.Deserialize("abc,2000/01/01,foo\r\n"));
        }

        [TestMethod()]
        public void ValidationMultipleErrorTest()
        {
            var sut = new CsvSerializerBuilder<User>();
            sut.AddColumn(0, x => x.Name)
                .Name("CustomName")
                .AddValidation(new CsvMaxLengthAttribute(1))
                .AddValidation(new CsvMaxLengthAttribute(2));

            Assert.ThrowsException<InvalidOperationException>(() => sut.ToCsvSerializer());
        }

        [TestMethod()]
        public void ValidationMultipleSuccesTest()
        {
            var sut = new CsvSerializerBuilder<User>();
            sut.AddColumn(0, x => x.Name)
                .Name("CustomName")
                .AddValidation(new CsvRegularExpressionAttribute(string.Empty))
                .AddValidation(new CsvRegularExpressionAttribute(string.Empty));

            Assert.IsNotNull(sut.ToCsvSerializer());
        }

        [TestMethod()]
        public void ColumnNotFoundTest()
        {
            var sut = new CsvSerializerBuilder<User>();
            Assert.ThrowsException<InvalidOperationException>(() => sut.ToCsvSerializer());
        }

        private User CreateUser()
        {
            return new User()
            {
                Name = "abc",
                Birthday = new DateTime(2000, 1, 1),
                ValueObject = new ValueObject("foo"),
            };
        }

        private class User
        {
            public string Name { get; set; } = string.Empty;

            public DateTime Birthday { get; set; }

            public ValueObject ValueObject { get; set; } = new ValueObject(string.Empty);

            public override bool Equals(object? obj)
            {
                return obj is User user &&
                       Name == user.Name &&
                       Birthday == user.Birthday &&
                       EqualityComparer<ValueObject>.Default.Equals(ValueObject, user.ValueObject);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Birthday, ValueObject);
            }
        }

    }
}