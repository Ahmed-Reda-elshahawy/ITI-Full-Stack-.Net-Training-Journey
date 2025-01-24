# 🔖 ITI - D0026 - C Sharp - Basics (Part6)

> 📖 is used for notes covered in the lecture, 💡 for extra interesting notes.

## 📖 `is` and `as` Operators

### 📖 `is` Operator

- `is` operator is used to check if an object is compatible with a given type.
- if type is compatible so it will return `true`, otherwise it will return `false`.

```csharp
object obj = "Hello";

if (obj is string) // True
{
    // Do something
}

if (obj is int) // False
{
    // Do something
}
```

### 📖 `as` Operator

- `as operator` is used to perform conversions between compatible reference types or nullable types.
- If the conversion is not possible, `as` returns `null`, otherwise it will return the converted type.

```csharp
object obj = "Hello";

string str = obj as string; // "Hello"
int? num = obj as int; // null
```

## 📖 Collections

- Collection is a set of types (data structures) located inside namespace `System.Collections`
- Common types include:
  - `ArrayList`
  - `HashTable`
  - `SortedList`
  - `DictionaryEntry`
  - `Stack`
- All types in `System.Collections` are based on the `object` type.
- This leads to **boxing** (converting value types to `object`) and **unboxing** (converting `object` back to value types).
- Boxing and unboxing can cause **performance overhead** and **runtime errors** (e.g., invalid cast exceptions).

**Example: Boxing and Unboxing Issue**:

```csharp
ArrayList list = new ArrayList();
list.Add(10); // Boxing (int → object)
int number = (int)list[0]; // Unboxing (object → int)
```

### 📖 `ArrayList`

- `ArrayList` is a **non-generic collection** that stores objects and dynamically resizes as needed.
- It is similar to an **array** but can grow or shrink in size automatically.
- Stores elements as object types, making it **non-homogeneous** (can store any type).
- Supports **dynamic resizing**: When the capacity is reached, it creates a new internal array with **double the capacity**.

#### 📖 ArrayList Common Properties

- `Capacity`: Gets or sets the total number of elements the `ArrayList` can hold without resizing.

- `Count`: Gets the number of elements actually stored in the `ArrayList`.

#### 📖 ArrayList Common Methods

- `Add(item)`: Adds an item to the end of the `ArrayList`.
- `AddRange(collection)`: Adds multiple items from a collection (e.g., array, list) to the ArrayList.
- `Insert(index, item)`: Inserts an item at a specific index.
- `Remove(item)`: Removes the first occurrence of an item.
- `RemoveAt(index)`: Removes the item at a specific index.
- `RemoveRange(index, count)`: Removes a range of items starting from a specific index.
- `Clear()`: Removes all items from the `ArrayList`.

> [!IMPORTANT]
>
> - **Boxing and Unboxing**: Since `ArrayList` stores items as `object`, value types are **boxed** when added and **unboxed** when retrieved. This can cause performance overhead and runtime errors (e.g., invalid cast exceptions).
> - **Type Safety**: Use `var` or explicit casting when accessing items to avoid runtime errors.
> - **Generic Alternative**: Prefer `List<T>` from `System.Collections.Generic` for type safety and better performance.

**Example: Basic ArrayList Usage**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Create an ArrayList
        ArrayList list = new ArrayList();

        // Add items
        list.Add(10); // int
        list.Add("Hello"); // string
        list.Add(3.14); // double

        // Access items (use casting or var)
        int number = (int)list[0]; // Explicit casting
        var text = list[1]; // Use var to avoid casting
        Console.WriteLine(number); // Output: 10
        Console.WriteLine(text); // Output: Hello

        // Loop through items using foreach
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}
```

**Example: Dynamic Resizing**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        ArrayList list = new ArrayList(2); // Initial capacity: 2

        Console.WriteLine($"Initial Capacity: {list.Capacity}"); // Output: 2

        list.Add(1);
        list.Add(2);
        list.Add(3); // Capacity doubles to 4

        Console.WriteLine($"Updated Capacity: {list.Capacity}"); // Output: 4
    }
}
```

**Example: ReadOnly And Fixed Size**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        ArrayList list = new ArrayList { 1, 2, 3 };

        // Make ArrayList read-only
        ArrayList readOnlyList = ArrayList.ReadOnly(list);
        // readOnlyList.Add(4); // Error: Collection is read-only

        // Make ArrayList fixed-size
        ArrayList fixedSizeList = ArrayList.FixedSize(list);
        // fixedSizeList.Add(4); // Error: Collection has a fixed size
    }
}
```

### 📖 `Hashtable`

- A `HashTable` is a collection that stores **key-value pairs**.
- It uses a **hash table** data structure internally for fast lookups, insertions, and deletions.
- **Keys must be unique**.
- **Keys cannot** be `null`, but values can be.
- It is **not type-safe** (uses `object` for keys and values), so it is recommended to use `Dictionary<TKey, TValue>` in modern C# code.

#### 📖 Hashtable Common Properties

- `Count`: Gets the number of key-value pairs in the HashTable.
- `Keys`: Gets a collection of all keys in the HashTable.
- `Values`: Gets a collection of all values in the HashTable.

#### 📖 Hashtable Common Methods

- `Add(key, value)`: Adds a new key-value pair to the HashTable.
- `Remove(key)`: Removes the key-value pair with the specified key.
- `ContainsKey(key)`: Checks if the HashTable contains a specific key.
- `ContainsValue(value)`: Checks if the HashTable contains a specific value.

**Example: Using HashTable**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Create a HashTable
        Hashtable hashtable = new Hashtable();

        // Add key-value pairs
        hashtable.Add("name", "John");
        hashtable.Add("age", 30);
        hashtable.Add("city", "New York");

        // Access values
        Console.WriteLine(hashtable["name"]); // Output: John

        // Check if a key exists
        Console.WriteLine(hashtable.ContainsKey("age")); // Output: True

        // Remove a key-value pair
        hashtable.Remove("city");

        // Iterate through keys and values
        foreach (DictionaryEntry entry in hashtable)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}
```

**Example: Custom Key with Overridden GetHashCode and Equals**:

```csharp
using System.Collections;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Age.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is Person other) return Name == other.Name && Age == other.Age;
        return false;
    }
}

internal class Program
{
    private static void Main()
    {
        var hashtable = new Hashtable();

        var person1 = new Person { Name = "John", Age = 30 };
        var person2 = new Person { Name = "Jane", Age = 25 };

        hashtable.Add(person1, "Engineer");
        hashtable.Add(person2, "Doctor");

        // Retrieve value using custom key
        Console.WriteLine(hashtable[person1]); // Output: Engineer
    }
}
```

#### 💡 Why HashTable Uses `GetHashCode` and `Equals`

**1. `GetHashCode`**:

- The `GetHashCode` method computes a **hash code** for the key.
- The hash code determines the **bucket index** where the `key-value` pair will be stored.
- A good hash function distributes keys evenly across buckets, minimizing collisions.

**2. `Equals`**:

- The `Equals` method compares keys when a collision occurs (i.e., two keys have the same hash code).
- It ensures the correct `key-value` pair is retrieved or updated.

**3. Collision Handling**:

- When two keys produce the same hash code (collision), the `HashTable` uses **double hashing** to find the next available bucket.
- The `Equals` method verifies if the key in the bucket matches the key being searched.

**4. Structure of a HashTable**:

- A HashTable is an array of buckets.
- Each bucket stores:
  - **A key-value pair.**
  - The **hash code** of the key.
- A **collision bit** (stored by stealing the most significant bit of the hash code).
- The **collision bit** indicates whether a collision occurred during insertion, optimizing lookups and deletions.

**5. Custom Keys**:

- To use a custom class as a key, you must **override** `GetHashCode` and `Equals`.
- This ensures the hash table can correctly compute hash codes and compare keys.

### 📖 `DictionaryEntry`

- `DictionaryEntry` is a **struct** that represents a **key-value pair** in collections like `Hashtable` or `SortedList`.
- It is part of the `System.Collections` namespace.
- Used to store and retrieve **key-value pairs** in non-generic collections.
- Provides a simple way to iterate over **key-value pairs** in collections like `Hashtable`.

#### 📖 DictionaryEntry Common Properties

- **Key**: Gets or sets the key in the **key-value pair**.
- **Value**: Gets or sets the value in the **key-value pair**.

**Example: Using `DictionaryEntry` with `Hashtable`**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Create a Hashtable
        Hashtable hashtable = new Hashtable();

        // Add key-value pairs
        hashtable.Add("name", "John");
        hashtable.Add("age", 30);
        hashtable.Add("city", "New York");

        // Iterate over key-value pairs using DictionaryEntry
        foreach (DictionaryEntry entry in hashtable)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}

// Output:
// name: John
// age: 30
// city: New York
```

**Example: Using `DictionaryEntry` with `SortedList`**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Create a SortedList
        SortedList sortedList = new SortedList();

        // Add key-value pairs
        sortedList.Add("apple", 1.99);
        sortedList.Add("banana", 0.99);
        sortedList.Add("cherry", 2.99);

        // Iterate over key-value pairs using DictionaryEntry
        foreach (DictionaryEntry entry in sortedList)
        {
            Console.WriteLine($"{entry.Key}: ${entry.Value}");
        }
    }
}

// Output:
// apple: $1.99
// banana: $0.99
// cherry: $2.99
```

### 📖 `SortedList`

- `SortedList` is a **non-generic collection** that stores key-value pairs in **sorted order** based on the keys.
- It is part of the `System.Collections` namespace.
- Maintains elements in sorted order by key.
- Keys must be **unique** and **non-null**.
- Values can be `null` or duplicated.
- Internally uses two arrays: one for keys and one for values.

#### 📖 `SortedList` Common Properties

- `Capacity`: Gets or sets the total number of elements the `SortedList` can hold without resizing.
- `Count`: Gets the number of key-value pairs in the `SortedList`.
- `Keys`: Gets a collection of keys in the `SortedList`.
- `Values`: Gets a collection of values in the `SortedList`.

#### 📖 `SortedList` Common Methods

- `Add(key, value)`: Adds a key-value pair to the `SortedList`.
- `Remove(key)`: Removes the key-value pair with the specified key.
- `ContainsKey(key)`: Checks if the `SortedList` contains a specific key.
- `ContainsValue(value)`: Checks if the `SortedList` contains a specific value.
- `GetByIndex(index)`: Gets the value at the specified index.
- `GetKey(index)`: Gets the key at the specified index.
- `Clear()`: Removes all key-value pairs from the `SortedList`.

**Example: SortedList Basic Usage**:

```csharp
using System;
using System.Collections;

class Program
{
    static void Main()
    {
        // Create a SortedList
        SortedList sortedList = new SortedList();

        // Add key-value pairs
        sortedList.Add("apple", 1.99);
        sortedList.Add("banana", 0.99);
        sortedList.Add("cherry", 2.99);

        // Access values by key
        Console.WriteLine($"Price of apple: {sortedList["apple"]}"); // Output: 1.99

        // Iterate over key-value pairs
        foreach (DictionaryEntry entry in sortedList)
        {
            Console.WriteLine($"{entry.Key}: ${entry.Value}");
        }
    }
}

// Output
// apple: $1.99
// banana: $0.99
// cherry: $2.99
```

**Example: Using `GetByIndex` and `GetKey`**:

```csharp
using System.Collections;

internal class Program
{
    private static void Main()
    {
        var sortedList = new SortedList();
        sortedList.Add("zebra", 100);
        sortedList.Add("apple", 200);
        sortedList.Add("banana", 300);

        // Access value using indexer
        Console.WriteLine(sortedList["banana"]); // 300
        Console.WriteLine(sortedList["appla"]); // null, which print emtpy in console

        // Access key and value by index
        for (var i = 0; i < sortedList.Count; i++)
        {
            var key = (string)sortedList.GetKey(i);
            var value = (int)sortedList.GetByIndex(i);
            Console.WriteLine($"{key}: {value}");
        }

        // Output
        // 300
        //
        // apple: 200
        // banana: 300
        // zebra: 100
    }
}
```

#### 💡 `SortedList` Performance

- Lookup by key is `O(log n)` due to binary search.
- Insertion and deletion are `O(n)` because elements may need to be shifted to maintain sorted order.

## Generics

### Built-in Generics

- Generics is a new update of collections
- Located in `System.Collections.Generic`
- Have set of Types like:
  - `List<T>`
  - `Dictionary<Key, Value>`
  - `SortedList<Key, Value>`
  - `KeyValuePair<Key, Value>`: type used with looping like `DictionaryEntry` in collections.
  - `Stack<T>`
  - `Queue<T>`
- Generics is more safer than collections, because it's homogenous type so any issues will be raised at compile time.

### User Defined Generics

#### Generic Methods

<!--TODO: Add notes about Generic Methods-->

- With using Overloading, we can create multiple methods with the same name but with different types, which generate different methods at the IL code.
- With using Generics, we can create a single method that can accept any type.
- So Generic Methods work as a template for the method, which will be generate the appropriate method at the compile time.
- We can use `T` as a type for the method, and we can use it as a type for the parameters and return type.
- **Type Inference**: the compiler can infer the type of the method from the parameters passed to the method.

<!--TODO: Add generic methods examples to support key notes -->

#### Generic Classes

<!--TODO: Add notes about Generic Classes-->

<!--TODO: Add generic classes examples to support key notes -->

#### Generic Interfaces

<!--TODO: Add notes about Generic Interfaces-->

<!--TODO: Add generic interfaces examples to support key notes -->

### Type constraints

<!-- TODO: Add notes about Type constrains with Generic types like classes and Methods -->

- Primary Constrains: only one primary constrain
- Secondary Constrains
- Constructor Constrains
- Enum Constrains (at least in C# 7.3)

using `where` keyword

## Delegates

- Each delegate has a method signature, which is the same as the method signature of the method that the delegate will point to.
- Each delegate have a table called `Invocation List` which contains the methods that the delegate will point to.
- We can add multiple methods to the `Invocation List` of the delegate using `+=` operator.
- When delegate is called, it will call all methods in the `Invocation List` in the order they were added.

```csharp
public delegate [returnType For Function] [DelegateName]([parameters]);
```

```csharp
public delegate void PrintDelegate(string message);

public void PrintMessage(string message)
{
    Console.WriteLine(message);
}

PrintDelegate print = new PrintDelegate(PrintMessage);
// or   PrintDelegate print = PrintMessage;
```

**Anonymous Methods**:

```csharp
PrintDelegate print = delegate (string message)
{
    Console.WriteLine(message);
};
```

**Lambda Expressions**:

```csharp
PrintDelegate print = (string message) => Console.WriteLine(message);
// another way
PrintDelegate print = message => Console.WriteLine(message);
```

### Predefined Delegates

- `Action`: delegate that takes parameters but does not return a value.
- `Func`: delegate that takes parameters and returns a value.
- `Predicate`: delegate that takes a parameter and returns a boolean value.

```csharp
List<int> mynums = Enumerable.Range(1, 10).ToList();
mynums.FindAll(x => x % 2 == 0);
```
