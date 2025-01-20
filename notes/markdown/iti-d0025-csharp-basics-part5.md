# 🔖 ITI - D0025 - C Sharp - Basics (Part5)

## Structures (Enhancements in C#9.0+)

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

## Interfaces

> An interface in C# is a **contract** that defines a set of `methods`, `properties`, `events`, or `indexers`. `Classes` and `structs` that implement the interface **must** provide implementations for its members. Interfaces enable **abstraction** and **polymorphism** in C#.

### Interfaces: Key Features

1. **Contract for Implementation**:

   - Interfaces define what a class or struct should do, not how to do it.
   - A class or struct can implement multiple interfaces.

2. **Members**:

   - Before C# 8.0: Interfaces could only contain **public abstract methods** and **properties**.
   - After C# 8.0: Interfaces can also contain:
     - **Methods with default implementations**.
     - **Static methods**.
     - **Private methods** (for internal use within the interface).

3. **No Constructors or Fields**:

   - Interfaces cannot have constructors or fields.
   - They can have properties, which are essentially methods behind the scene.

4. **Interface Segregation Principle**:

   - Interfaces should be small and focused, defining only the necessary members.
   - This is known as the Interface Segregation Principle (part of SOLID principles).

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
   public class Person : IComparable<Person>
   {
       public string Name { get; set; }
       public int Age { get; set; }

       public int CompareTo(Person other)
       {
           return Age.CompareTo(other.Age); // Compare by age
       }
   }

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

**Some Other Predefined Interfaces**:

1. `IFormattable`:

   - Provides functionality to format the value of an object into a string representation.
   - Contains the `ToString(string format, IFormatProvider formatProvider)` method.

2. `IDisposable`:

   - Used to release unmanaged resources (e.g., file handles, database connections).
   - Contains the `Dispose()` method.

3. `IEnumerable`:

   - Exposes an enumerator for iterating over a collection.
   - Contains the `GetEnumerator()` method.

### Explicit Interface Implementation

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

### Interfaces Best Practices

- **Follow Interface Segregation Principle**
- **Minimize using default method implementation in interfaces**
  - Default implementation should be used to extend interfaces without breaking existing code.
- **Prefer explicit interface implementation**:
  - Use explicit implementation to avoid naming conflicts when implementing multiple interfaces.
- **Document Interfaces Clearly**:
  - Clearly document the purpose and usage of each interface.

### Abstract Class Vs. Interface

#### Interface

- A contract that defines **what** class or struct should do.
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
- **Static Members**: can contain **static methods** and also **static fields**.
- **Purpose**: use for **code reuse** and **abstraction**

#### When to use

**Use Interfaces When**:

- You need to define a **contract** for multiple unrelated classes.
- You want to support **multiple inheritance** (via interfaces).
- You need to define **behavior** without implementation.

**Use Abstract Class When**:

- You need to provide a **base implementation** for related classes.
- You want to share **common code** among related classes.
- You need to define **partial implementation** and force derived classes to complete it.

## Design Pattern: Singleton

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

## Switch Expression in `C#`

- A **concise** way to handle multiple conditions.
- They are a modern alternative to traditional `switch` statements and can be used with:

  - **lambda expression**
  - **logical operators (`or`, `and`)**
  - **range expression**

### Switch Expression: Key Features

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

### `ref` return

## Exception

- `try`
- `catch`
- `finally`
- general exception handling
- specialized exception handling
- Create custom exception class
- Inner Exception

### `StreamWriter`

12:40

## `var` vs. `dynamic`

### `var`

- Declaring variable using `var`, must be initialized with declaration because based on this initialized the variable type will be defined
- `var` can be used as a parameter type for a function

### `dynamic`

- Type will be resolved at the runtime
- `dynamic` can be used as a return type and also as function parameter type

**Difference Between `object` and `dynamic`**:

- We should make an explicit casting with `object`
- `dynamic` already resolve type at runtime, and also it automatically cast reference to object type

## `record`

- Used for creating template model
- Used to create immutable type
- Used if type contains only properties

## object initializer using `new` without type name

```csharp
struct Student {}

Student std = new(){}; // here we didn't use `new Student()`
```

### `init` with properties

## Mutability (Mutable data-types vs Immutable data-types)

### String Builder

## Extension Methods

- Must be declared as static method inside an static class
- first parameter for this extension method should be `this type paramName`

```csharp
static class Until {
  public static void PrintStars(this string _dummy, int stars){}
}

// Calling
string str = "hello world";
str.PrintStars(3);
Util.PrintStars(str, 3);
```

## XML Docs
