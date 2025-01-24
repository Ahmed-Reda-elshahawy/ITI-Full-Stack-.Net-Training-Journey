# 🔖 ITI - D0025 - C Sharp - Basics (Part5)

> 📖 is used for notes covered in the lecture, 💡 for extra interesting notes.

## 📖 Structures (Enhancements in C#9.0+)

- **Explicit Parameterless Constructor**: Can define a custom parameterless constructor.

  ```csharp
  public struct Point
  {
      public int X;
      public int Y;

      // Explicit parameterless constructor (C# 9.0+)
      public Point()
      {
          X = 0;
          Y = 0;
      }

      public Point(int x, int y)
      {
          X = x;
          Y = y;
      }
  }
  ```

- **Implicit Default Constructor**: Always available, even with parameterized constructors.

  ```csharp
  public struct Point
  {
      public int X;
      public int Y;

      public Point(int x, int y)
      {
          X = x;
          Y = y;
      }
  }

  // Usage
  Point p1 = new Point(); // Implicit default constructor (C# 9.0+)
  Point p2 = new Point(10, 20); // Parameterized constructor
  ```

- **Field Initializers**: Can initialize fields directly in the struct definition.

  ```csharp
  public struct Point
  {
      public int X = 10; // Field initializer (C# 9.0+)
      public int Y = 20; // Field initializer (C# 9.0+)
  }
  ```

## 📖 Target-Typed `new` Expressions

- With target-typed `new`, you can omit the type name after the `new` keyword when the type can be inferred from the context (e.g., from the variable declaration).
- We can use it with `structs` and `classes`.
- Good for reducing redundancy and improving code readability.

```csharp
public struct A
{
    public int X;
}

A a1 = new A();

// Target-typed new syntax
A a2 = new();
```

## 📖 Interfaces

- An interface in C# is a **contract** that defines a set of `methods`, `properties`, `events`, or `indexers`. `Classes` and `structs` that implement the interface **must** provide implementations for its members.
- Interfaces enable **abstraction** and **polymorphism** in C#.

### Interfaces: Key Features

1. **Contract for Implementation**:

   - Interfaces define **what** a class or struct **should do**, not how to do it.
   - A class or struct can implement multiple interfaces.

2. **Members**:

   - Before C# 8.0: Interfaces could only contain **public abstract methods** and **properties**.
   - After C# 8.0: Interfaces can also contain:
     - **Methods with default implementations**.
     - **Static methods**.
     - **Private methods** (for internal use within the interface).

3. **No Constructors or Fields**:

   - Interfaces **cannot** have constructors or fields.
   - They can have properties, which are essentially methods behind the scene.

4. **Interface Segregation Principle**:

   - Interfaces should be small and focused, defining only the necessary members.
   - This is known as the Interface Segregation Principle (part of **SOLID** principles).

**Example: Basic Interfaces**:

```csharp
public interface IShape
{
    double Area(); // Abstract method
    double Perimeter(); // Abstract method
}

public class Circle : IShape
{
    public double Radius { get; set; }

    public double Area() => Math.PI * Radius * Radius;
    public double Perimeter() => 2 * Math.PI * Radius;
}

public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public double Area() => Width * Height;
    public double Perimeter() => 2 * (Width + Height);
}
```

**Example: Interface With Default Method Implementation**:

```csharp
public interface ILogger
{
    void Log(string message); // Abstract method
    void LogError(string error) => Log($"Error: {error}"); // Default implementation
}

public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);
}
```

**Example: Interface With Static Methods**:

```csharp
public interface IMathUtility
{
    static int Add(int a, int b) => a + b;
}

// Usage
int result = IMathUtility.Add(5, 10); // 15
```

### Predefined Interfaces in .NET

1. `IComparable`

   - The `IComparable` interface is used to define a generalized comparison method for sorting or ordering objects.
   - It contains a single method: `CompareTo(object obj)`.

   ```csharp
    // Using IComparable Interface
    public class Person : IComparable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int CompareTo(object? other)
        {
            if (other is Person) return Age.CompareTo(((Person)other).Age);
            throw new ArgumentException();
        }
    }

    // 💡 Using Generic IComparable Interface
    // public class Person : IComparable<Person>
    // {
    //     public string Name { get; set; }
    //     public int Age { get; set; }
    //
    //     public int CompareTo(Person other)
    //     {
    //         return Age.CompareTo(other.Age); // Compare by age
    //     }
    // }


   // Usage
   Person[] people = {
       new Person { Name = "Alice", Age = 30 },
       new Person { Name = "Bob", Age = 25 },
       new Person { Name = "Charlie", Age = 35 }
   };

   Array.Sort(people); // Sorts by age

   foreach (var person in people)
   {
       Console.WriteLine($"{person.Name} - {person.Age}");
   }
   // Output:
   // Bob - 25
   // Alice - 30
   // Charlie - 35
   ```

2. `ICloneable`

   - The ICloneable interface provides a method to create a copy of an object.
   - It contains a single method: `Clone()`.

   ```csharp
   public class Person : ICloneable
   {
       public string Name { get; set; }
       public int Age { get; set; }

       public object Clone()
       {
           return new Person { Name = Name, Age = Age }; // Deep copy
       }
   }

   // Usage
   Person person1 = new Person { Name = "Alice", Age = 30 };
   Person person2 = (Person)person1.Clone();

   Console.WriteLine(person2.Name); // Alice
   Console.WriteLine(person2.Age); // 30
   ```

**💡 Some Other Predefined Interfaces**:

1. `IFormattable`:

   - Provides functionality to format the value of an object into a string representation.
   - Contains the `ToString(string format, IFormatProvider formatProvider)` method.

2. `IDisposable`:

   - Used to release unmanaged resources (e.g., file handles, database connections).
   - Contains the `Dispose()` method.

3. `IEnumerable`:

   - Exposes an enumerator for iterating over a collection.
   - Contains the `GetEnumerator()` method.

### 📖 Explicit Interface Implementation

Explicit interface implementation is used when a class implements multiple interfaces that have members with the **same name**. It allows you to provide separate implementations for each interface, **avoiding naming conflicts**.

```csharp
public interface ILogger
{
    void Log(string message); // Method in ILogger
}

public interface IFileLogger
{
    void Log(string message); // Method in IFileLogger
}

public class Logger : ILogger, IFileLogger
{
    // Explicit implementation for IFileLogger's Log method
    void IFileLogger.Log(string message)
    {
        Console.WriteLine($"FileLogger: {message}");
    }

    // Default implementation for ILogger's Log method
    public void Log(string message)
    {
        Console.WriteLine($"Logger: {message}");
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = new Logger();

        // Default implementation (ILogger's Log method)
        logger.Log("This is a log message for ILogger.");

        // Explicit implementation (IFileLogger's Log method)
        ((IFileLogger)logger).Log("This is a log message for IFileLogger.");
    }
}
```

### 📖 Interfaces Best Practices

- **Follow Interface Segregation Principle**
- **Minimize using default method implementation in interfaces**: Default implementation should be used to extend interfaces without breaking existing code.
- **Prefer explicit interface implementation**: Use explicit implementation to avoid naming conflicts when implementing multiple interfaces.
- **Document Interfaces Clearly**: Clearly document the purpose and usage of each interface.

### 💡 Abstract Class Vs. Interface

#### Interface

- A contract that defines **what** class or struct **should do**.
- **Inheritance**: A class or struct can implement **multiple** interfaces.
- **Members**:
  - Can contains methods, properties, events, and indexers.
  - Cannot contain fields or constructors.
- **Default Implementation**: Can provide **default implementation** for methods (C# 8.0+).
- **Access Modifiers**: Members ar **public** by default.
- **Static Members**: Can contain **static methods** (C# 8.0+).
- **Purpose** Used for **abstraction** and **polymorphism**.

#### Abstract Class

- A class that cannot be instantiated and may contain **partial implementation** (contains abstract methods)
- **Inheritance**: A class can inherit from **only one abstract class**.
- **Members**: Can contain methods, properties, fields, constructors, indexers, and fields.
- **Static Members**: Can contain **static methods** and also **static fields**.
- **Purpose**: Use for **code reuse** and **abstraction**

#### When to use

**Use Interfaces When**:

- You need to define a **contract** for multiple unrelated classes.
- You want to support **multiple interface implementation**.
- You need to define **behavior** without implementation (but you can also provide default implementation in C# interfaces, starting from C#8.0).

**Use Abstract Class When**:

- You need to provide a **base implementation** for related classes.
- You want to share **common code** among related classes.
- You need to define **partial implementation** and force derived classes to complete it.

## 📖 Design Pattern: Singleton

The Singleton pattern ensures that a class has **only one instance**. It is commonly used for `logging`, `database connections`, etc.

```csharp
public class Singleton
{
    // Private static instance
    private static Singleton _instance;

    // Private constructor
    private Singleton()
    {
        // Initialization code (if needed)
    }

    // Public static method to access the instance
    public static Singleton Instance
    {
        get
        {
            // Create the instance if it doesn't exist
            if (_instance == null) _instance = new Singleton();
            return _instance;
        }
    }

    // Example method
    public void DoSomething()
    {
        Console.WriteLine("Singleton is doing something!");
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        // Access the Singleton instance
        var singleton = Singleton.Instance;
        singleton.DoSomething(); // Output: Singleton is doing something!

        // Try to create another instance (will return the same instance)
        var anotherSingleton = Singleton.Instance;
        Console.WriteLine(singleton == anotherSingleton); // Output: True
    }
}
```

## 📖 Switch Expression in `C#`

- A **concise** way to handle multiple conditions.
- They are a modern alternative to traditional `switch` statements and can be used with:

  - **lambda expression**
  - **logical operators (`or`, `and`)**
  - **range expression**

### 📖 Switch Expression: Key Features

1. **Lambda Expression**:

   - Use the `=>` (lambda) syntax to define cases.
   - Each case returns a value directly.

2. **Logical Operators**: Use `or` and `and` to combine multiple conditions in a single case.

3. **Range Expression**: Use ranges (e.g., `>= 1 and <= 5`) to match values within a specific range.

4. **Discard Pattern(`_`)**: Use `_` as the default case to handle unmatched values.

5. **Expression-Based**: Switch expressions return a value, making them ideal for assignments or return statements.

**Example: Matching Strings**:

```csharp
string GetName(string name)
{
    return name switch
    {
        "a" or "A" => "Ahmed",
        "j" or "J" => "John",
        _ => "Not Found" // Default case
    };
}

// Usage
Console.WriteLine(GetName("A")); // Output: Ahmed
Console.WriteLine(GetName("j")); // Output: John
Console.WriteLine(GetName("x")); // Output: Not Found
```

**Example: Matching Numbers with Ranges**:

```csharp
string GetNumberCategory(int num)
{
    return num switch
    {
        >= 1 and <= 5 => "First range", // Range expression
        >= 6 and <= 10 => "Second range", // Range expression
        _ => "Out of range" // Default case
    };
}

// Another syntax: the same as the code above
string GetNumberCategory2(int num)
{
    return num switch
    {
        var a when a>= 1 && a<= 5 => "First range", // Range expression
        var a when a>= 6 && a <= 10 => "Second range", // Range expression
        _ => "Out of range" // Default case
    };
}

// Usage
Console.WriteLine(GetNumberCategory(3)); // Output: First range
Console.WriteLine(GetNumberCategory(7)); // Output: Second range
Console.WriteLine(GetNumberCategory(15)); // Output: Out of range
```

**Another Example: Switch Expression**:

```csharp
string GetGrade(int score)
{
    return score switch
    {
        >= 90 and <= 100 => "A",
        >= 80 and < 90 => "B",
        >= 70 and < 80 => "C",
        >= 60 and < 70 => "D",
        _ => "F" // Default case
    };
}

// Usage
Console.WriteLine(GetGrade(95)); // Output: A
Console.WriteLine(GetGrade(85)); // Output: B
Console.WriteLine(GetGrade(55)); // Output: F
```

> [!IMPORTANT]
>
> Above examples are written using top-level statements.

## `in` keyword and return `ref`

### `in` keyword

- The `in` keyword is used to pass arguments to methods by reference but ensures that the argument **cannot be modified** inside the method.
- It is useful for passing large structs efficiently without copying them, while also guaranteeing **immutability**.

**Example: Using `in` Keyword**:

```csharp
struct Point
{
    public int X;
    public int Y;
}

void PrintPoint(in Point p)
{
    // p.X = 10; // Error
    Console.WriteLine($"X: {p.X}, Y: {p.Y}");
}

Point point = new Point { X = 5, Y = 10 };
PrintPoint(point); // Pass by reference (read-only)
```

### `ref` return

- The `ref` return allows a method to return a reference to a variable (instead of a value).
- This enables the caller to modify the original variable directly.

**Example: Using `ref` Return**:

```csharp
int[] numbers = { 1, 2, 3, 4, 5 };

ref int FindNumber(int value)
{
    for (var i = 0; i < numbers.Length; i++)
        if (numbers[i] == value)
            return ref numbers[i]; // Return reference to array element

    throw new ArgumentException("Value not found");
}

ref var numRef = ref FindNumber(3); // Get reference to the number 3
numRef = 10; // Modify the original array

Console.WriteLine(string.Join(", ", numbers)); // Output: 1, 2, 10, 4, 5
```

## `StreamWriter`

- The `StreamWriter` class is used to write text data to a stream (e.g., a file, memory, or network stream).
- It is part of the `System.IO` namespace.

**Example: Writing to a File**:

```csharp
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\current\Desktop\example.txt";

        // Write text to a file
        StreamWriter writer = new StreamWriter(filePath)
        // StreamWriter writer = new StreamWriter(filePath, true) // append option to true
        writer.WriteLine("Hello, World!");
        writer.WriteLine("This is a sample text.");
        writer.Close():

        Console.WriteLine("Text written to file.");
    }
}
```

### 📖 `@` Symbol with Strings in `C#`

- The `@` symbol is used to create a verbatim string literal in C#.
- It tells the compiler to treat the string exactly as written, without interpreting escape sequences (e.g., `\n`, `\t`).
- **Use Cases**:
  - File Paths
  - Multi-line Strings
  - Regex Patterns

## 📖 Exception

- An **exception** is an error that occurs at runtime, causing the program to terminate abnormally if not handled.
- Exceptions are represented by classes derived from the `System.Exception` base class.

### 📖 Exception Handling

- Use `try`, `catch`, and `finally` blocks to handle exceptions gracefully.
- The `try` block contains code that might throw an exception.
- The `catch` block handles the exception.
- The `finally` block contains code that always executes, regardless of whether an exception occurs.

**Example: Basic Exception Handling**:

```csharp
try
{
    int numerator = 10;
    int denominator = 0;
    int result = numerator / denominator; // This will throw DivideByZeroException
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Error: Cannot divide by zero.");
    Console.WriteLine(ex.Message); // Output: Attempted to divide by zero.
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred.");
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("This block always executes.");
}
```

### 📖 Throwing Exceptions

- Use the `throw` keyword to throw an exception manually.
- You can `throw` built-in exceptions or custom exceptions.

**Example: Throwing an Exception**:

```csharp
void ValidateAge(int age)
{
    if (age < 18)
    {
        throw new ArgumentException("Age must be 18 or older.");
    }
}

try
{
    ValidateAge(15); // This will throw ArgumentException
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message); // Output: Age must be 18 or older.
}
```

### 📖 Exception Hierarchy

- Catch the most specific exceptions first, followed by more general ones.
- This is because a derived class exception can be caught by a base class exception handler.

### 📖 Custom Exceptions

- Create custom exceptions by deriving from the `System.Exception` class.
- Custom exceptions can include additional properties or methods for better error handling.

**Example: Custom Exceptions**:

```csharp
// Custom exception class
public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message) : base(message) { }
}

void ValidateEmail(string email)
{
    if (string.IsNullOrEmpty(email) || !email.Contains("@"))
    {
        throw new InvalidEmailException("Invalid email address.");
    }
}

try
{
    ValidateEmail("invalid-email"); // This will throw InvalidEmailException
}
catch (InvalidEmailException ex)
{
    Console.WriteLine(ex.Message); // Output: Invalid email address.
}
```

### 💡 Inner Exception

- The `InnerException` property is used to capture the original exception that caused the current exception.
- This is useful for debugging and logging.

**Example: InnerException**:

```csharp
try
{
    try
    {
        int[] numbers = { 1, 2, 3 };
        Console.WriteLine(numbers[5]); // This will throw IndexOutOfRangeException
    }
    catch (IndexOutOfRangeException ex)
    {
        throw new Exception("An error occurred while accessing the array.", ex);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message); // Output: An error occurred while accessing the array.
    Console.WriteLine(ex.InnerException?.Message); // Output: Index was outside the bounds of the array.
}
```

## 📖 `var` vs. `dynamic` vs. `object`

### 📖 `var`

- The `var` keyword is used for **implicitly typed variables**.
- The type of the variable is inferred at **compile time** based on the assigned value.
- The variable **must be initialized** at the time of declaration.
- Once the type is inferred, it cannot be changed (strongly typed).
- **Use Cases**:
  - Simplifies code when the type is obvious from the context.
  - Commonly used with `LINQ` queries or complex types.
- **Limitations**:
  - Cannot be used for fields or properties in a class.
  - Cannot be used as a return type for methods (except with anonymous types).

**Example: Using `var`**:

```csharp
var number = 10; // Compiler infers 'int'
var name = "Alice"; // Compiler infers 'string'
var list = new List<int>(); // Compiler infers 'List<int>'

// var invalid; // Error: must be initialized
```

### 📖 `dynamic`

- The `dynamic` keyword is used for **dynamically typed variables**.
- The type is resolved at **runtime**, not at compile time.
- No type checking is performed at compile time, which can lead to runtime errors if the type is used incorrectly.
- Can be used as a return type or parameter type for methods.
- Allows flexibility when the type is not known at compile time
- **Limitations**:
  - No IntelliSense support in Visual Studio (since the type is unknown at compile time).
  - Performance overhead due to runtime type resolution.

**Example: Using `dynamic`**:

```csharp
dynamic value = 10; // Type resolved at runtime
Console.WriteLine(value); // Output: 10

value = "Hello"; // Type can change at runtime
Console.WriteLine(value); // Output: Hello

value = new List<int> { 1, 2, 3 }; // Type can change at runtime
Console.WriteLine(value.Count); // Output: 3

// Runtime error if used incorrectly
// value.Fun(); // Throws RuntimeBinderException
```

### 📖 `object`

- The `object` keyword is the base type for all types in C#.
- It is a **strongly typed** variable, but it can hold any type because of boxing.
- Requires **explicit casting** to access the actual type or methods.
- **Use Cases**:
  - Used when you need to store any type of data (e.g., in collections like `ArrayList`).
  - Commonly used in legacy code or when working with non-generic collections.
- **Limitations**:
  - Requires explicit casting, which can lead to runtime errors if the cast is invalid.
  - Performance overhead due to boxing and unboxing.

**Example: Using `object`**:

```csharp
object obj = 10; // Boxing: int is stored as object
Console.WriteLine(obj); // Output: 10

obj = "Hello"; // Can hold any type
Console.WriteLine(obj); // Output: Hello

// Explicit casting required
string str = (string)obj; // Unboxing: object is cast to string
Console.WriteLine(str); // Output: Hello

// Invalid cast throws InvalidCastException
// int number = (int)obj; // Throws InvalidCastException
```

## 💡 Mutability (Mutable data-types vs Immutable data-types)

- **Mutability**: Refers to whether an object's state (data) can be changed after it is created.
- **Mutable**: The object's state can be modified after creation.
  - **Examples in C#** : `List<T>`, `Dictionary<TKey, TValue>`, `StringBuilder`, `Arrays (int[], string[], etc.)`, `Custom classes with public setters.`
- **Immutable**: The object's state cannot be modified after creation.
  - **Examples in C#**: `string`, `DateTime`,`TimeSpan`,`Tuple<T1, T2>`,`Records(record types in C# 9+)`.

### 💡 Mutability Use Cases

- **Mutable objects** are useful when you need to modify the object's state frequently.
- **Immutable objects** are safer in multi-threaded environments and help prevent unintended side effects.

## 📖 String Builder

- The `StringBuilder` class is used to efficiently manipulate strings, especially when performing multiple modifications (e.g., concatenations, replacements, or insertions).
- Unlike `string`, which is **immutable**, `StringBuilder` is **mutable**, meaning it modifies the same instance in memory.
- Exists under the `System.Text` namespace.
- **Performance**: Using `StringBuilder` is more efficient than repeatedly concatenating strings with`+` or `string.Concat`, as it avoids creating multiple intermediate string objects.

### 📖 String Builder Common Methods

- `Append`: Adds text to the end of the current StringBuilder.
- `Insert`: Inserts text at a specified position.
- `Remove`: Removes a specified number of characters.
- `Replace`: Replaces occurrences of a specified string or character.
- `Clear`: Clears all the content
- `ToString`: Converts the StringBuilder to a string.

### 📖 String Properties

- `Length`: represents the **number of characters** currently in the `StringBuilder`.
- `Capacity`: represents the maximum number of characters that can be stored without resizing its internal buffer.
  - It automatically increases as needed when the content exceeds the current capacity.

**Example: String Builder**:

```csharp
using System;
using System.Text;

class Program
{
    static void Main()
    {
        // Create a StringBuilder
        StringBuilder sb = new StringBuilder();

        // Append text
        sb.Append("Hello");
        sb.Append(" ");
        sb.Append("World!");

        // Insert text
        sb.Insert(6, "C# ");

        // Replace text
        sb.Replace("World", "Universe");

        // Convert to string
        string result = sb.ToString();
        Console.WriteLine(result); // Output: Hello C# Universe!
    }
}
```

## 📖 `init` Accessors in Properties

- The `init` accessor is used to create **immutable properties** that can only be set during object initialization.
- Introduced in **C# 9** as part of the **init-only properties** feature.
- **Usage**:
  - The `init` accessor allows a property to be set **only during object creation** (e.g., in the constructor or object initializer).
  - Once the object is initialized, the property becomes **read-only**.
- **Benefits**:
  - Ensures immutability for properties after object creation.
  - Provides a cleaner and safer way to define immutable objects compared to using constructors or private setters.
- **Limitations**:
  - Cannot be used with `set` accessor.
  - Cannot be used in interfaces or abstract properties.

**Syntax**:

```csharp
public datatype PropertyName { get; init; }
```

**Example: Using `inti` Accessors**:

```csharp
public class Person
{
    // Explicit Default Constructor
    public Person()
    {
    }

    // Constructor (optional)
    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    // Properties with 'init' accessor
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Age { get; init; }
}

internal class Program
{
    private static void Main()
    {
        // Object initializer
        Person person1 = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30
        };

        // Constructor
        var person2 = new Person("Jane", "Smith", 25);

        // Attempting to modify after initialization (will cause a compile-time error)
        // person1.FirstName = "Alice"; // Error: Init-only property can only be assigned in an object initializer or constructor
    }
}
```

## 📖 `record` Types in `C#`

- A `record` is a `class` or `struct` that provides a special syntax and behavior for working with data models.
- Records are designed to simplify the creation of immutable types with value-based equality.
- They automatically implement useful methods like `ToString()`, `Equals()`, and `GetHashCode()`.
- `record` contains only properties.
- **Value-Based Equality**: Records automatically implement **value-based equality**, meaning two records are equal if their properties have the same values.
- **Use cases**: Records uses incase we need to define data model which is store data in it and we can have 2 different scenarios which are:

  - We need to define data model that depends on `value equality`. (so use `struct record`)
  - We need to define a type which objects are immutable. (so use `class record`)

**Syntax**:

```csharp
// 💡 define struct record
[Access Modifier] record struct [Record Name](...positional_parameters);

// define class record
[Access Modifier] record [Record Name](...positional_parameters);
```

> [!Note]
>
> - The **primary constructor** syntax automatically generates properties with `init` (for record classes) or `readonly` (for record structs) accessors.
> - You can also define records with explicit properties if needed.

**💡 Example: Struct Record**:

```csharp
public record struct Point(int X, int Y);

class Program
{
    static void Main()
    {
        var point1 = new Point(10, 20);
        var point2 = point1 with { X = 15 }; // Create a new record struct with modified X

        Console.WriteLine(point1); // Output: Point { X = 10, Y = 20 }
        Console.WriteLine(point2); // Output: Point { X = 15, Y = 20 }
    }
}
```

**📖 Example: Class Record**:

```csharp
public record Person(string FirstName, string LastName, int Age);

class Program
{
    static void Main()
    {
        var person1 = new Person("John", "Doe", 30);
        var person2 = person1 with { Age = 31 }; // Create a new record with modified Age

        Console.WriteLine(person1); // Output: Person { FirstName = John, LastName = Doe, Age = 30 }
        Console.WriteLine(person2); // Output: Person { FirstName = John, LastName = Doe, Age = 31 }
    }
}
```

### 💡 Records Additional Notes

1. **Inheritance**:
   - Record classes support **inheritance**, allowing you to create hierarchies of immutable types.
   - Record structs do not **support inheritance**.
2. **Mutable Records**: Records can be made mutable by explicitly defining properties with set.

   ```csharp
    public record struct MutablePoint(int X, int Y)
    {
        public int X { get; set; } = X;
        public int Y { get; set; } = Y;
    }
   ```

## 📖 Extension Methods

### 📖 Extension Methods: Purpose

- Extension methods allow you to **add new methods** to existing types without modifying their source code or creating a derived type.
- They are particularly useful for extending **sealed classes** or types you don't have control over (e.g., `string`, `int`, or framework classes).

### 📖 Extension Methods: Syntax

- Extension methods must be declared as **static methods** inside a static class.
- The first parameter of the method specifies **which type the method operates on** and must be prefixed with the `this` keyword.

```csharp
static class [ClassName]
{
    public static [ReturnType] [MethodName](this [TypeToExtend] paramName, [OtherParameters])
    {
        // Method implementation
    }
}
```

### 📖 Extension Methods: Usage

- Extension methods can be called as if they were instance methods of the extended type.
- They can also be called statically, but this is less common.

```csharp
static class StringExtensions
{
    // Extension method to print stars after a string
    public static void PrintStars(this string text, int stars)
    {
        Console.WriteLine(text + new string('*', stars));
    }
}

class Program
{
    static void Main()
    {
        string str = "Hello, World!";
        str.PrintStars(5); // Output: Hello, World!*****
        StringExtensions.PrintStars(str, 5); // Output: Hello, World!*****
    }
}
```

### 📖 Extension Methods: Limitations

- Cannot access `private` or `protected` members of the extended type.
- Cannot `override` existing methods (they only add new methods).

## XML Docs

- XML Docs are special comments in C# that allow you to **document your code** in a structured way.
- They are used to generate **API documentation** and provide **IntelliSense** support in Visual Studio.
- XML Docs start with `///` and use XML tags to describe code elements like classes, methods, properties, and parameters.

### 📖 Common XML Tags

- `<summary>`: Provides a brief description of the code element.
- `<param>`: Describes a method parameter.
- `<returns>`: Describes the return value of a method.
- `<remarks>`: Provides additional information about the code element.
- `<example>`: Provides an example of how to use the code element.
- `<exception>`: Documents exceptions that a method might throw.

**Example: XML Docs**:

```csharp
/// <summary>
/// Adds two integers and returns the result.
/// </summary>
/// <param name="a">The first integer.</param>
/// <param name="b">The second integer.</param>
/// <returns>The sum of the two integers.</returns>
/// <example>
/// <code>
/// int result = Add(2, 3); // Returns 5
/// </code>
/// </example>
/// <exception cref="System.OverflowException">Thrown when the result is too large.</exception>
public int Add(int a, int b)
{
    return a + b;
}
```
