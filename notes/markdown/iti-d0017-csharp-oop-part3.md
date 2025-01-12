# 🔖 ITI - D0017 - C Sharp - OOP (Part3)

## Difference between Value Type and Reference Types

- **Value types** stored in `stack`, directly hold data in memory.
- **Reference types** Reference types stored in `heap` and hold a reference in the `stack` which points to the data (object) in the `heap`

### GetHashCode() method

- Generates a hash code that uniquely identifies the object instance.
- For value types, it computes the hash based on the values.

```csharp
int [] arr = { 1, 2, 3, 4, 5 };
int num = 20;
Console.WriteLine(arr.GetHashCode());  // 32854180
Console.WriteLine(num.GetHashCode()); // 20
```

### Knowing type of variable using `GetType`

- Use `GetType` method to get type of variable
- Use `MinValue`, `MaxValue` properties to know min and max values of this type

```csharp
int x = 10;
Console.WriteLine(x);  // 10
Console.WriteLine(x.GetType()); // System.Int32
Console.WriteLine(sizeof(Int32));  // 4
Console.WriteLine(Int32.MinValue);  // -2147483648
Console.WriteLine(Int32.MaxValue); // 2147483647
```

## Structures

- **Structs are a `value type`**: Structs store data directly, as opposed to reference types that store references to data.
- **Stored in the `stack`**: Structs are typically stored on the `stack`, which contributes to faster memory allocation and deallocation compared to heap-based reference types.
- **Default constructor**: Structs have a default constructor, without any parameters provided by C#, which initializes struct members with their default types.
- Structs in C# **can't** have an **explicit parameterless constructor**.
- Structs in C# can have an **explicit parameterized constructor**, but you **must initialize all fields** within the constructor.
- Structs in C# can have more than one **explicit parameterized constructor** with different parameter lists.
- Structs members can be initialized manually.
- Structs in C# can't support inheritance, i.e. (structs can't derive from other structs, and also can't be a base for other structs).
- Structs implicitly inherited from the base `Object` class, so it has some behaviors like (`ToString()`, `GetHashCode()`, `Equals()`).
- Structs have a good performance because it's fully stored in the `stack`.
- Structs members and methods by default **private**.

```csharp
public struct Coords
{
	public double X;
	public double Y;
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }
}
```

```csharp
Coords c1 = new Coords();
Console.WriteLine(c1.X); // 0
Console.WriteLine(c1.Y); // 0
c1.X = 20;
Console.WriteLine(c1.X); // 20
```

> [!Tip]
>
> - Use `cw` snippet to quickly write `Console.WriteLine`
> - Use `ctor` snippet to quickly create default constructor for struct

### Indexer

- Indexer is a special type of property that allows a class or struct to be accessed like an array for its instances
- Structs can have an indexer but only if this struct contains a property or field that is an array
- Indexer is defined using `this` keyword
- Structs can have only one indexer

**Syntax**:

```csharp
public return_value_type this[indexer_data_type index_name] {
	set {
		// handle setter case
		// use `value` => refers to assigned value to the indexer
	}
	get {
		// handle getter case
	}
}
```

**Example**:

```cs
class MyCollection
{
    private string[] items = new string[10];

    // Indexer definition
    public string this[int index]
    {
        get { return items[index]; }   // Retrieve item at the given index
        set { items[index] = value; }  // Set item at the given index
    }
}

class Program
{
    static void Main()
    {
        MyCollection collection = new MyCollection();

        // Using the indexer to assign and retrieve values
        collection[0] = "Hello";  // Set value at index 0
        collection[1] = "World";  // Set value at index 1

        Console.WriteLine(collection[0]);  // Output: Hello
        Console.WriteLine(collection[1]);  // Output: World
    }
}
```
