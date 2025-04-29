## About
This package includes utilities such as `StringGenerator`, `CreditCardNumberGenerator`, `EmailGenerator`, `ClassGenerator`, `EnumerableGenerator`, `EnumGenerator` and many more to help you generate values, collections and classes for your test code.

## How to Use
After installing the package and adding `Cayd.Test.Generators` namespace to your test code, you can start using the static utility classes to generate values, collections or classes.

A few example of how to use the library:
```csharp
using Cayd.Test.Generators;

public class MyEmailCheckerTest
{
    // ... setup other needs

    [Fact]
    public void Check_WhenEmailIsValid_ShouldReturnTrue()
    {
        // Arrange
        var email = EmailGenerator.Generate(); // Returns a valid random email address.

        // Act
        var result = MyEmailChecker.Check(email);

        // Assert
        Assert.True(result);
    }
}
```

Or when you have an entity class(es) to be generated:
```csharp
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; }
}

public class Book
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public EStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }

    // This would normally cause infinite recursive calls, however, ClassGenerator can detect them at one level of depth. This variable will be set to null
    public Author Author { get; set; }
}

public enum EStatus
{
    Discontinued = -100,
    Undefined = 0,
    Available = 1,
    OutOfStock = 2
}

// You can simple call Generate method in ClassGenerator and the instance will be populated with values
Author testAuthor = ClassGenerator.Generate<Author>();
```

If you want to set your own generator for specific properties or not to generate values for them, you can defined them as follows:
```csharp
/** For .Net 8 and above */
Author testAuthor = ClassGenerator.Generate<Author>([
    (a => a.Id, 5),                          // You can give exact values for properties instead of random values
    (a => a.Books, () => new List<Book>())   // For class types, you can give exact values using anonymous functions
    (a => a.Name, MyOwnStringGenerator),     // You can use your own value generators for properties
    (a => a.DateOfBirth, () => {             // You can define your own value generator using anonymous functions
        var dateTime = DateTime.UtcNow;
        // ... do some calculations
        return dateTime;
    })
]);

/** For versions lower than .Net 8 */
Author testAuthor = ClassGenerator.Generate(new (Expression<Func<Author, object?>>, Func<object?>)[] {
    (a => a.Id, 5),                          // You can give exact value for properties instead of random values
    (a => a.Books, () => new List<Book>())   // For class types, you can give exact values using anonymous functions
    (a => a.Name, MyOwnStringGenerator),     // You can use your own value generators for properties
    (a => a.DateOfBirth, () => {             // You can define your own value generator using anonymous functions
        var dateTime = DateTime.UtcNow;
        // ... do some calculations
        return dateTime;
    })
});
```

## Generators
The package includes the following static utility classes:
- `ClassGenerator`
- `CreditCardNumberGenerator`
- `DateTimeGenerator`
- `DictionaryGenerator`
- `EmailGenerator`
- `EnumerableGenerator`
- `EnumGenerator`
- `GuidGenerator`
- `IpAddressGenerator`
- `PasswordGenerator`
- `PhoneNumberGenerator`
- `StringGenerator`
- `TimeSpanGenerator`

To learn more about their usages in detail, you can check the [GitHub Wiki](https://github.com/c-ayd/Cayd.Test.Generators/wiki).

## Limitations

The current version has the following limitations when it comes to `ClassGenerator`.
- `ClassGenerator` generates values only for properties of a class. For other types, they need to be assigned manually.
- `ClassGenerator` does not populate `struct` types with values but initialize them with their default values, except `Guid`, `DateTime` and `TimeSpan`. They are generated automatically.
- The expression parameter in the `Generate` method of `ClassGenerator` does not support more than 1 depth. For instance, this expression will not work: `x => x.Property1.Property2`. Only expressions like `x => x.Property1` are supported.