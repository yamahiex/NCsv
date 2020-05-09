# NCsv

.NET Standard 2.0 class library for object to CSV serialization and CSV to object deserialization.

[NuGet Package](https://www.nuget.org/packages/NCsv/)

## Usage

### Serialization and Deserialization

Set the `CsvColumnAttribute` to the property of the class to be serialized.  
You can also use the `Attribute` to validate the value.

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
        Name = "Foo,Bar",
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

// CustomName,Birthday,Age
// Jackson,2000/01/01,20
// "Foo,Bar",2001/01/01,19
Debug.WriteLine(csv);

try
{
    // Deserialize.
    var deserializedUsers = cs.Deserialize(csv);
}
catch (CsvValidationException ex)
{
    // Handle validation errors.
    Debug.WriteLine(ex.Message);
}

// You can also retrieve all validation errors beforehand
// with the CsvSerializer.GetErrors method.
var errors = cs.GetErrors(csv);
errors.ForEach(error => Debug.WriteLine(error));

// Serialize to file.
using (var writer = new StreamWriter(@"C:\users.csv"))
{
    cs.Serialize(writer, users);
}

// Deserialize from a file.
using (var reader = new StreamReader(@"C:\users.csv"))
{
    var fileUsers = cs.Deserialize(reader);
}
```

### Customize message

Implement `ICsvValidationMessage` or inherit `CsvValidationDefaultMessage`.

```c#
class CustomMessage : ICsvValidationMessage
{
    public string GetNumericConvertError(ICsvItemContext context)
    {
        return $"The {context.Name} on line {context.LineNumber} must be set to a numeric value.";
    }
}
```

Use it.

```c#
CsvConfig.Current.ValidationMessage = new CustomMessage();
```

### Custom validation attributes

Inherit the `CsvValidationAttribute` to create your own validation attribute.

```c#
[AttributeUsage(AttributeTargets.Property)]
public class ExampleValidationAttribute : CsvValidationAttribute
{
    public override bool Validate(CsvValidationContext context, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrEmpty(context.Value))
        {
            errorMessage = $"{context.Name} is error.";
            return false;
        }

        return true;
    }
}
```

Use it.

```c#
[CsvColumn(0)]
[ExampleValidation]
public string PropertyA { get; set; }
```

You can also inherit the `CsvRegularExpressionAttribute`.

```c#
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CsvNumberOnlyAttribute : CsvRegularExpressionAttribute
{
    public CsvNumberOnlyAttribute()
    {
        this.Pattern = "^[0-9]+$";
    }

    protected override string GetErrorMessage(ICsvItemContext context)
    {
        return $"{context.Name} must be set to a number only."; 
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

Create a converter by inheriting `CsvConverter`.

```c#
class ValueObjectConverter : CsvConverter
{
    public bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object result, out string errorMessage)
    {
        result = new ValueObject(context.CsvItem);
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
