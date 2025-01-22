# 🔖 ITI - D0022 - C Sharp - Basics (Part2)

## Object Base

- `System.Object` is the base class for all classes in C#.
- All types, either value types or reference types, inherit from `System.Object`.
- So, all types can be treated as objects, and also can be casted to `System.Object`.
- Also all types will have the following methods:
  - `ToString()`
  - `GetHashCode()`
  - `GetType()`
  - `Equals()`

### Boxing and Unboxing

- Boxing is the process of converting a value type to a reference type.
- Unboxing is the process of converting a reference type to a value type, but this is not safe and can cause runtime errors.

**Example**:

```csharp
int x = 10;
object obj = x; // Boxing
int y = (int)obj; // Unboxing
```

> [!Note]
>
> Boxing and unboxing are not recommended because they are slow and can cause runtime errors (exceptions).

## Casting

- Implicit Casting (Widening) automatically done by the compiler.
- Explicit Casting (Narrowing)

**Example**:

```csharp
int x = 10;
double y = x; // Implicit Casting
int z = (int)y; // Explicit Casting
```

### Operators Default Behavior for Types

- Operators treat all integral types as `int` by default.
- Operators treat all floating-point types as `double` by default.

**Example**:

```csharp
short x = 10;
short y = 20;
short z = x + y; // Error
```

**Solution**:

```csharp
short z = (short)(x + y);
// or
int z = x + y;
```

> [!Note]
>
> Assignment operator `=` with initialization does not require casting, because it does it automatically with initializations like here `short z = 10` the `10` is an `int` but it will be implicitly casted to `short`.

## Parsing

- Parsing is the process of converting a string to a value type.
- Available methods for parsing:
  - `int.Parse()`
  - `int.TryParse()` is recommended because it does not throw an exception if the parsing fails.
  - `Convert.ToInt32()`
- Those methods are available for all value types like `int`, `double`, `float`, `bool`, etc.
- **Exceptions with Parse**:
  - `FormatException`: If the string is not in the correct format.
  - `OverflowException`: If the string represents a number that is less than `MinValue` or greater than `MaxValue`.
  - `ArgumentNullException`: If the string is `null`.

**Example: `Parse` and `TryParse`**:

```csharp
string str = "10";
int x = int.Parse(str);
int y;
if (int.TryParse(str, out y))
{
    Console.WriteLine(y);
}
```

**Example: `Convert`**:

```csharp
string str = "10";
int x = Convert.ToInt32(str);
```

## Number Formatting

**Example: Different Radixes**:

```csharp
int b = 0b1010; // Binary
int o = 010; // Octal
int h = 0x10; // Hexadecimal

Console.WriteLine(b); // 10
Console.WriteLine(o); // 8
Console.WriteLine(h); // 16
```

**Example: Number delimiters**:

```csharp
int x = 1_000_000;
Console.WriteLine(x); // 1000000
```

## Equality with Reference Types

- `==` operator compares the references of the objects.
- `Equals()` method compares the values of the objects.

**Example**:

```csharp
class Student
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Student(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;
        Student std = (Student)obj;
        return Id == std.Id && Name == std.Name;
    }
}

Student std1 = new Student(1, "John");
Student std2 = new Student(2, "John");
Student std3 = new Student(1, "John");

Console.WriteLine(std1 == std2); // False
Console.WriteLine(std1.Equals(std2)); // False
Console.WriteLine(std1 == std3); // False
Console.WriteLine(std1.Equals(std3)); // True
```

## Enums

- Enums are used to create a group of named constants.
- Enums are value types.
- Enums are strongly typed constants.
- Enums are used to create a list of named integer constants.
- Enums by default start from `0` and increment by `1`.
- Enums can be assigned to any integral value
  - But we should cast it to the enum type, only if the value is `0` we can assign it without casting.

**Example**:

```csharp
enum Gender
{
  MALE,
  FEMALE
}

Gender gen0 = Gender.MALE; // 0 (Preferred)
Gender gen1 = Gender.FEMALE; // 1
Gender gen2 = 0; // will work because it is 0
// Gender gen3 = 1; // Error
Gender gen4 = (Gender)1; // 1
Gender gen5 = (Gender)2; // Will work but not recommended

Console.WriteLine(gen0); // MALE
Console.WriteLine(gen0.ToString()); // MALE

Console.WriteLine(gen1); // FEMALE
Console.WriteLine(gen1.ToString()); // FEMALE

Console.WriteLine(gen2); // MALE
Console.WriteLine(gen2.ToString()); // MALE

Console.WriteLine(gen4); // FEMALE
Console.WriteLine(gen4.ToString()); // FEMALE

Console.WriteLine(gen5); // 2
Console.WriteLine(gen5.ToString()); // 2
```

### Enum with TryParse

- `Enum.TryParse()` is used to parse a string to an enum.
- It returns a boolean value to indicate if the parsing was successful or not.
- If the passed string is a valid constant name in the enum, it will return `true` and the out parameter will have the parsed value, otherwise, it will return `false`.
- If the passed string is a valid integral value, it will return `true` and the out parameter will have the parsed value (label) and if it the value is not in the enum it will return this (integral) value back in the out parameter.

```csharp
Enum Gender {
  MALE,
  FEMALE
}

object res;
bool test = Enum.TryParse(typeof(Gender), "1", out res);
if (test)
{
    Console.WriteLine(res); // FEMALE
} else
{
    Console.WriteLine("Failed to parse");
}

test = Enum.TryParse(typeof(Gender), "MALE", out res);
if (test)
{
    Console.WriteLine(res); // MALE
} else
{
    Console.WriteLine("Failed to parse");
}

test = Enum.TryParse(typeof(Gender), "2", out res);
if (test)
{
    Console.WriteLine(res); // 2
} else
{
    Console.WriteLine("Failed to parse");
}


test = Enum.TryParse(typeof(Gender), "MALe", out res);
if (test)
{
    Console.WriteLine(res);
} else
{
    Console.WriteLine("Failed to parse"); // Failed to parse will be printed
}
```

### Validate Integral Values is defined in the Enum

As we see in the example above, the `Enum.TryParse()` will return the integral value if it is not defined in the enum, so we can validate the integral value to check if it is defined in the enum or not.

So we can use the `Enum.IsDefined()` method to check if the integral value is defined in the enum or not.

```csharp
enum Gender {
  MALE,
  FEMALE
}

object res;

// Example with a string representation of an integral value
bool test = Enum.TryParse(typeof(Gender), "1", out res) && Enum.IsDefined(typeof(Gender), res);
if (test)
{
    Console.WriteLine(res); // FEMALE
}
else
{
    Console.WriteLine("Invalid value");
}

// Example with a valid string name
test = Enum.TryParse(typeof(Gender), "MALE", out res) && Enum.IsDefined(typeof(Gender), res);
if (test)
{
    Console.WriteLine(res); // MALE
}
else
{
    Console.WriteLine("Invalid value");
}

// Example with an out-of-range integral value
test = Enum.TryParse(typeof(Gender), "2", out res) && Enum.IsDefined(typeof(Gender), res);
if (test)
{
    Console.WriteLine(res);
}
else
{
    Console.WriteLine("Invalid value"); // Invalid value will be printed
}

// Example with an incorrect case for a valid name
test = Enum.TryParse(typeof(Gender), "MALe", out res) && Enum.IsDefined(typeof(Gender), res);
if (test)
{
    Console.WriteLine(res);
}
else
{
    Console.WriteLine("Invalid value"); // Invalid value will be printed
}
```

## Visual Studio: Creating Console C# Application

You will face some options when creating a new console application project in Visual Studio like:

- `Do not use top-level statements`. (This is a new feature in C# 9.0, it allows you to write a C# program without a `Main` method).
- `Enable native AOT publish`. (Ahead of Time) compilation.
