# 🔖 ITI - D0026 - C Sharp - Basics (Part6)

## `is` and `as` Operators

### `is` Operator

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

### `as` Operator

- `as operator` is used to perform conversions between compatible reference types or nullable types.
- If the conversion is not possible, `as` returns `null`, otherwise it will return the converted type.

```csharp
object obj = "Hello";

string str = obj as string; // "Hello"
int? num = obj as int; // null
```

## Collections

- Collection is a set of types (data structures) located inside namespace `System.Collections`
  - ArrayList
  - HashTable
  - SortedList
  - Stack
- All types in collections is based on `Object-base` type
- So it have issues with `boxing` and `unboxing`, so it can cause errors at runtime `exceptions`

### `ArrayList`

- `ArrayList` is a non-generic collection of objects whose size increases dynamically.
- It is the same as `Array` except that its size increases dynamically.
- It's `constructor` accept `capacity` of the `ArrayList`.
- When we reach this `capacity` so it will create new array with doubled `capacity`.
- **Common Properties**:
  - `Capacity`: to know the `capacity` of the `ArrayList`.
  - `Count`: to know number of elements in the `ArrayList`.
- **Common Methods**:
  - `Add(item)`: append item to the `ArrayList`
  - `AddRange(array)`: append array items to the `ArrayList`
  - `Insert`

<!-- TODO: Enhance ArrayList by adding common properties and Common methods -->

<!-- TODO: Support ArrayList notes by examples, with using properties and methods and also use case with looping with `foreach` with `var` -->

> [!IMPORTANT]
>
> - `ArrayList` accepts any type of items, because collections is based on `Object-base` type
> - So `ArrayList` is a non-homogenous type
> - So be careful when you access item in the `ArrayList` so you must cast it to the correct type
> - Or You can use `var` to easily recognize types without casting. <!--WARN: Check correctness of this point, because it may not correct with all cases-->

### Add Extra MetaData to ArrayList

<!--TODO: Enhance extra methods of ArrayList and support it with examples -->

- `ArrayList.ReadOnly(arrayList)`: will return ArrayList and make it readonly.
- `ArrayList.FixedSize(arrayList)`: will return ArrayList and make it with fixedSize like normal Array.

### `HashTable`

<!-- REVIEW: HashTable Key Notes -->

- HashTable is a collection of Key-Value pairs.
- All keys must be **unique**.
- HashTable uses `GetHashCode` method and `Equals` method to compare keys.
- **Common Properties**:
  - `Count`: to know number of elements in the `HashTable`.
  - `Keys`: to get all keys in the `HashTable`.
  - `Values`: to get all values in the `HashTable`.
- **Common Methods**:
  - `Add(key, value)`: add new key-value pair to the `HashTable`.
  - `Remove(key)`: remove key-value pair from the `HashTable`.
  - `ContainsKey(key)`: check if the `HashTable` contains the key.
  - `ContainsValue(value)`: check if the `HashTable` contains the value.

<!-- TODO: Add Example to support this tip -->

> [!TIP]
>
> - We can override `GetHashCode` and `Equals` methods to make our custom class as a key in the `HashTable`.

### `SortedList`

<!-- TODO: Add key notes about SortedList -->

- Looping on it using `foreach...in` with type `DictionaryEntry` to get key and value.
- `SortedList` is a collection of Key-Value pairs.

<!-- TODO: Add example for SortedList to support notes -->

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
