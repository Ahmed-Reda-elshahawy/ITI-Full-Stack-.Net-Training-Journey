# 🔖 ITI - D0024 - C Sharp - Basics (Part4)

_Review Bit Flag ENUMS in C# From the [previous lecture](./iti-d0023-csharp-basics-part3.md#enum-bit-flag-enumerations)_

## Enums (Bit Flag)

> **Bit Flag Enums** are used to represent a set of flags (options or states) that can be combined, removed, or checked individually. They are ideal for scenarios where multiple options can be enabled simultaneously.

### Bit Flag ENUM: Key Concepts

1. **Combining Flags**: using Bitwise OR (`|`)

   ```csharp
   Permission permissions = Permission.Read | Permission.Write;
   ```

2. **Removing Flags**: using Bitwise(`^`)

   ```csharp
   permissions ^= Permission.Write;
   ```

3. **Checking Flags**: using Bitwise(`&`)
4. **[Flags] Attribute**:

   - Add the `[Flags]` attribute to the enum to enable meaningful `ToString()` output and make it clear that the enum represents a set of flags.
   - Without `[Flags]`, combined values will not display correctly when converted to a string.

   ```csharp
   bool canRead = (permissions & Permission.Read) == Permission.Read;
   ```

5. **Specify Underlying Type**

   - You can specify the underlying type of the enum (e.g., byte, int, long) using a colon (`:`) followed by the type.
   - This is useful to control the range of values the enum can hold.

   ```csharp
   enum Permission : byte { ... }
   ```

6. **Power of Two Values**:

   - Each flag should be assigned a value that is a **power of two** (e.g., 1, 2, 4, 8, etc.) to ensure unique bit positions.
   - This allows combining and checking flags without conflicts.

**Example**:

```csharp
[Flags]
enum Permission : byte
{
    None = 0,       // No permissions
    Read = 1,       // 0001
    Write = 2,      // 0010
    Delete = 4,     // 0100
    Execute = 8,    // 1000
    All = Read | Write | Delete | Execute // 1111
}

class Program
{
    static void Main()
    {
        // Combining flags
        Permission permissions = Permission.Read | Permission.Write;

        // Checking flags
        if ((permissions & Permission.Read) == Permission.Read)
        {
            Console.WriteLine("Read permission is granted.");
        }

        // Removing a flag
        permissions ^= Permission.Write;

        // Displaying combined flags
        Console.WriteLine(permissions); // Output: Read
    }
}
```

## Nullable Variables in `C#`

- In `C#`, **nullable variables** allow value types to hold `null` values, which is useful when a value might be missing or undefined.
- Reference types can already hold `null` by default, but value types (e.g. `int`, `bool`, `double`) cannot unless explicitly marked as `nullable`.

### Nullable Value Types

- Value types (e.g., `int`, `bool`, `double`) cannot hold `null` by default.
- To make a value type nullable, append a `?` to the type declaration.

```csharp
int? x = null; // Valid: x is a nullable int
//  int y = null;  // Error: int cannot be null

x = 10;
int z = (int)x; // Casting nullable value to non-nullable
Console.WriteLine(z); // Output: 10
```

### Safe Casting for nullable values

- Directly casting a nullable value to a non-nullable type is not safe because it can throw an exception if the nullable value is null.
- Use the following properties to safely handle nullable values:

  - `HasValue`: Returns true if the nullable variable has a value, otherwise false.
  - `Value`: Returns the value of the nullable variable. Throws an exception if the value is null.

**Example: Nullable values Safe Casting**:

```csharp
int? x = 10;
int y = 0;

if (x.HasValue) // Check if x has a value
{
    y = x.Value; // Safe to access the value
}

Console.WriteLine(y); // Output: 10
```

### Null Coalescing Operator (`??`)

- The **null coalescing operator (`??`)** provides a shorthand way to handle `null` values by supplying a default value if the nullable variable is `null`.
- Syntax: `nullableVariable ?? defaultValue`

**Example: `null` Coalescing Operator**

```csharp
int? x = 10;
int y = x ?? 0; // If x is null, use 0 as the default value

int? m = null;
int n = m ?? 0; // Since m is null, n will be 0

Console.WriteLine(y); // Output: 10
Console.WriteLine(n); // Output: 0
```

### Null Propagation Operator (`?.`)

- The null propagation operator (`?.`) simplifies accessing members (properties, methods, or indexers) of an object that might be `null`.
- If the object is `null`, the entire expression evaluates to `null` instead of throwing an exception.
- Often used in combination with the null coalescing operator (`??`) to provide a default value.

**Example: Null Propagation Operator**

```csharp
int[] arr = null;

// Console.WriteLine(arr.Length); // Throws NullReferenceException
Console.WriteLine(arr?.Length); // Output: null (no exception)
Console.WriteLine(arr?.Length ?? 0); // Output: 0 (default value)
```

### Nullable Reference Types (C# 8.0+)

- Starting with C# 8.0, nullable reference types were introduced to help reduce null-related runtime errors.
- Reference types (e.g., `string`, `object`) are non-nullable by default, and you must explicitly mark them as nullable using `?`.
- Enable nullable reference types in your project by adding the following to your `.csproj` file:
  ```xml
  <Nullable>enable</Nullable>
  ```

**Example: Nullable Reference Types**

```csharp
string? nullableString = null; // Explicitly nullable
string nonNullableString = null; // Warning: non-nullable reference type cannot be null
```

### Best Practices with Nullable values

1. **Use Nullable Types for Optional Values**:

   - Use nullable types when a value might be missing or undefined (e.g., database fields, user input).

2. **Always Check for null**:

   - Use `HasValue` or the null coalescing operator (`??`) to avoid `null`-related exceptions.

3. **Combine Null Propagation and Null Coalescing**:

   - Use `?.` and `??` together to safely access members and provide default values.

4. **Enable Nullable Reference Types**:

   - Use nullable reference types (C# 8.0+) to catch potential `null` issues at compile time.

### Common Pitfalls with Nullable values

1. **Direct Casting Without Checking**:

   - Avoid directly casting nullable values to non-nullable types without checking for null.

2. **Ignoring Compiler Warnings**:

   - Pay attention to compiler warnings when using nullable reference types to avoid potential runtime errors.

3. **Overusing Nullable Types**:

   - Only use nullable types when necessary.
   - Overusing them can make your code harder to understand.

**Full Example: Nullable Values**

```csharp
int? x = null;
int y = x ?? 0; // Use 0 if x is null

string? name = null;
Console.WriteLine(name?.ToUpper() ?? "Unknown"); // Output: Unknown

int[]? numbers = null;
Console.WriteLine(numbers?.Length ?? 0); // Output: 0
```

## Advanced Array Topics in `C#`

### Range Access for Arrays

> C# provides powerful features for accessing elements and ranges within arrays using **indexers** and **ranges**.

#### Backward Indexing

- Use the `^` operator to access elements from the end of the array.
- `^1` refers to the last element, `^2` refers to the second-to-last element, and so on.
- `^0` is **invalid** because it represents the length of the array, which is out of bounds.

**Example: Backward Indexing**

```csharp
int[] arr = [100, 200, 300, 400, 500];

Console.WriteLine(arr[^2]); // 400 (second-to-last element)
Console.WriteLine(arr[^1]); // 500 (last element)
// Console.WriteLine(arr[^0]); // Error: Index out of bounds
```

#### Range Access

- Use the `..` operator to specify a range of indices.
- The start index is **inclusive**, and the end index is **exclusive**.
- You can combine ranges with backward indexing.

**Example: Range Access**

```csharp
int[] arr = [100, 200, 300, 400, 500];

int[] arr2 = arr[1..];    // [200, 300, 400, 500] (from index 1 to end)
int[] arr3 = arr[1..^2];  // [200, 300] (from index 1 to second-to-last, exclusive)
int[] arr4 = arr[..3];    // [100, 200, 300] (from start to index 3, exclusive)
int[] arr5 = arr[^3..];   // [300, 400, 500] (from third-to-last to end)
```

### Judge Array (Array of Arrays)

- A **jagged array** is an array whose elements are also arrays.
- Unlike multi-dimensional arrays, each "row" in a jagged array can have a different length.

#### One-Dimensional Jugged Array

- The first dimension (number of rows) is fixed, but the second dimension (length of each row) can vary.
- Each row must be initialized separately.

**Example: One-Dimensional Jagged Array**

```csharp
int[][] degrees = new int[3][]; // Create a jagged array with 3 rows (all initially null)

degrees[0] = new int[4]; // First row has 4 elements (all initialized to 0)
degrees[1] = new int[3]; // Second row has 3 elements
degrees[2] = new int[2]; // Third row has 2 elements

Console.WriteLine(degrees.Length);       // 3 (number of rows)
Console.WriteLine(degrees[0].Length);    // 4 (length of first row)
Console.WriteLine(degrees[1].Length);    // 3 (length of second row)
Console.WriteLine(degrees[2].Length);    // 2 (length of third row)
```

#### Two-Dimensional Jagged Array

- A jagged array can also have multiple dimensions for its outer array.

**Example: Two-Dimensional Jagged Array**

```csharp
int[,][,] degrees = new int[2, 3][,]; // Create a 2x3 jagged array of 2D arrays

degrees[0, 0] = new int[2, 2]; // Initialize first element as a 2x2 array
degrees[0, 1] = new int[3, 3]; // Initialize second element as a 3x3 array

Console.WriteLine(degrees[0, 0].Length); // 4 (2x2 array)
Console.WriteLine(degrees[0, 1].Length); // 9 (3x3 array)
```

## Pass by Value vs. Pass by Reference in `C#`

Let's explain the differences between **pass by value** and **pass by reference**, including **value types**, **reference types**, and the effects of passing them in different ways.

### Passing Value Types

#### Passing Value by Value

- When a value type (e.g., `int`, `bool`, `double`) is passed by value, a **copy** of the value is passed to the function.
- Changes made to the parameter inside the function do **not affect** the original variable.

**Example: Passing Value by Value**

```csharp
public class Program {
    public static void ModifyValue(int val)
    {
        val += 10; // Modifies the copy, not the original
    }

    public static void Main(string[] args)
    {
        int x = 10;
        Console.WriteLine(x); // 10
        ModifyValue(x); // Pass by value
        Console.WriteLine(x); // 10 (unchanged)
    }
}
```

#### Passing Value by Reference

- When a **value type** is passed **by reference**, the **memory address** of the variable is passed to the function.
- Changes made to the parameter inside the function **affect** the original variable.
- Use the `ref` keyword to pass by reference.

**Example: Passing Value by Reference**

```csharp
public class Program {
    public static void ModifyValue(ref int val)
    {
        val += 10; // Modifies the original variable
    }

    public static void Main(string[] args)
    {
        int x = 10;
        Console.WriteLine(x); // 10
        ModifyValue(ref x); // Pass by reference
        Console.WriteLine(x); // 20 (modified)
    }
}
```

### Passing Reference Types

#### Passing Reference by Value

- When a reference type (e.g., an `array`, object, or `string`) is passed by value, a **copy** of the reference (memory address) is passed to the function.
- Changes to the **object's data** (e.g., modifying array elements) **affect** the original object.
- However, **reassigning the reference** itself (e.g., assigning it to a new object) does **not affect** the original reference.

**Example: Passing Reference by Value**

```csharp
public class Program {
    public static void ModifyArray(int[] arr)
    {
        arr[0] = 100; // Modifies the original array
        arr = new int[] { 200, 300 }; // Reassigns the reference (does not affect the original)
    }

    public static void Main(string[] args)
    {
        int[] myArray = { 1, 2, 3 };
        ModifyArray(myArray); // Pass by value
        Console.WriteLine(myArray[0]); // 100 (modified)
    }
}
```

#### Passing Reference by Reference

- When a **reference type** is passed **by reference**, the **memory address of the reference** itself is passed to the function.
- Changes to **the object's data** and **reassigning** the reference **both affect** the original reference.
- Use the `ref` keyword to pass by reference.

**Example: Passing Reference by Reference**

```csharp
public class Program {
    public static void ModifyArray(ref int[] arr)
    {
        arr[0] = 100; // Modifies the original array
        arr = new int[] { 200, 300 }; // Reassigns the reference (affects the original)
    }

    public static void Main(string[] args)
    {
        int[] myArray = { 1, 2, 3 };
        ModifyArray(ref myArray); // Pass by reference
        Console.WriteLine(myArray[0]); // 200 (reference reassigned)
    }
}
```

## Difference Between `ref` and `out`

| **Feature**        | `ref`                                                | `out`                                                                           |
| ------------------ | ---------------------------------------------------- | ------------------------------------------------------------------------------- |
| **Initialization** | The variable **must be initialized** before passing. | The variable does **not need to be initialized** before passing.                |
| **Assignment**     | No obligation to assign a value inside the function. | **Must assign a value** inside the function.                                    |
| **Use Case**       | Used when the function may modify the value.         | Used when the function is expected to **return a value** through the parameter. |

**Example**

Using `ref`:

```csharp
public class Program {
    public static void ModifyValue(ref int val)
    {
        val += 10; // Modifies the original variable
    }

    public static void Main(string[] args)
    {
        int x = 10; // Must be initialized
        ModifyValue(ref x); // Pass by reference
        Console.WriteLine(x); // 20 (modified)
    }
}
```

Using `out`:

```csharp
public class Program {
    public static void GetValue(out int val)
    {
        val = 20; // Must assign a value
    }

    public static void Main(string[] args)
    {
        int x; // No need to initialize
        GetValue(out x); // Pass by reference
        Console.WriteLine(x); // 20 (assigned inside the function)
    }
}
```

## `params` Keyword

- The `params` keyword allows a method to accept a **variable number of arguments** as an array.
- It must be the last parameter in the method signature.

**Example: `params` Keyword**

```csharp
public class Program {
    public static int SumArr(params int[] arr)
    {
        int sum = 0;
        foreach (int num in arr)
        {
            sum += num;
        }
        return sum;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(SumArr(10, 20, 30)); // 60
        Console.WriteLine(SumArr(1, 2, 3, 4, 5)); // 15
    }
}
```

## Default and Optional Parameters in `C#`

- **Default and optional parameters** allow you to define methods with parameters that have **default values**.
- This makes the method more flexible and reduces the need for method overloading.

### Default Parameters

- A **default parameter** is a parameter with a predefined value in the method signature.
- If the caller does not provide a value for the parameter, the default value is used.
- Default parameters must be **at the end of the parameter list**.

**Example: Default Parameters**

```csharp
public class Program {
    public static int Sum(int x, int y, int z = 10, int w = 20)
    {
        return x + y + z + w;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(Sum(5, 10)); // 5 + 10 + 10 + 20 = 45
        Console.WriteLine(Sum(5, 10, 15)); // 5 + 10 + 15 + 20 = 50
        Console.WriteLine(Sum(5, 10, 15, 25)); // 5 + 10 + 15 + 25 = 55
    }
}
```

### Optional Parameters

- An **optional parameter** is similar to a default parameter but is explicitly marked with the `Optional` attribute.
- Optional parameters also require a default value and must be at the **end of the parameter list**.

**Example: Optional Parameters**

```csharp
public class Program {
    public static int Sum(int x, int y, [System.Runtime.InteropServices.Optional] int z, int w = 20)
    {
        return x + y + z + w;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(Sum(5, 10)); // 5 + 10 + 0 + 20 = 35 (z is optional and defaults to 0)
        Console.WriteLine(Sum(5, 10, 15)); // 5 + 10 + 15 + 20 = 50
    }
}
```
