# NCsv

.NET Standard class library to serialize the object to CSV.

## Usage

### Serialization and Deserialization

It is a class to be serialized.  
Serializes the property with CsvColumnAttribute.

``` c#
class User
{
    [CsvColumn(0, Name = "CustomName")]
    [CsvMaxLength(100)]
    [CsvRequired]
    public string Name { get; set; } = string.Empty;

    [CsvColumn(1)]
    [CsvFormat("yyyy/MM/dd")]
    public DateTime Birthday { get; set; }

    [CsvColumn(2)]
    [CsvNumber(3, 0, MinValue = "0")]
    public int Age { get; set; }
}
```

This is the code to serialize.

```c#
var users = new User[]
{
    new User()
    {
        Name = "Jackson",
        Birthday = new DateTime(2000, 1, 1),
        Age = 20,
    },
    new User()
    {
        Name = "Sophia",
        Birthday = new DateTime(2001, 1, 1),
        Age = 19,
    },
};

var cs = new CsvSerializer<User>()
{
    HasHeader = true,
};

// Serialize.
var csv = cs.Serialize(users);

// "CustomName","Birthday","Age"
// "Taro","2000/01/01",20
// "Jiro","2001/01/01",19
Debug.WriteLine(csv);

// Deserialize.
var deserializedUsers = cs.Deserialize(csv);

// Serialize to file.
using (var writer = new StreamWriter(@"C:\users.csv"))
{
    cs.Serialize(writer, users);
}

// Deserialize from a file
using var reader = new StreamReader(@"C:\users.csv");
var fileUsers = cs.Deserialize(reader);
```

### Customize message

```c#
class CustomMessage : CsvMessage
{
    public override string GetNumericConvertError(string columnName)
    {
        return $"{columnName} must be set to a numeric value.";
    }
}
```

```c#
CsvConfig.Current.Message = new CustomMessage();
```

### Custom validation attributes

Inherit the CsvValidationAttribute to create your own validation attribute.

```c#
[AttributeUsage(AttributeTargets.Property)]
public class ExampleValidationAttribute : CsvValidationAttribute
{
    public override bool Validate(string value, string name, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrEmpty(value))
        {
            errorMessage = $"{name} is error.";
            return false;
        }

        return true;
    }
}
```

You can also inherit the CsvRegularExpressionAttribute.

```c#
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class AlphanumericOnlyAttribute : CsvRegularExpressionAttribute
{
    public AlphanumericOnlyAttribute()
    {
        this.Pattern = "^[a-zA-Z0-9]+$";
    }

    protected override string GetErrorMessage(string name)
    {
        return $"{name} must be set to a alphanumeric only.";
    }
}
```

### Customize the conversion

You can customize the conversion process, so you can support your own types.

Create a class to type a property.

```c#
class ValueObject
{
    private readonly string value;

    public ValueObject(string value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return this.value;
    }
}
```

Create a converter by inheriting CsvConverter.

```c#
class ValueObjectConverter : CsvConverter
{
    // It's optional, but it's implemented for the sake of explanation.
    public string ConvertToCsvItem(CsvConvertContext context, object? objectItem)
    {
        return $"\"{objectItem}\"";
    }

    public bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string errorMessage)
    {
        result = new ValueObject(csvItem);
        errorMessage = string.Empty;
        return true;
    }
}
```

It is a class to serialize.

```c#
class Foo
{
    [CsvColumn(0)]
    [CsvConverter(typeof(ValueObjectConverter))]
    public ValueObject PropertyA { get; set; }
}
```
