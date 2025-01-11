# 🔖 ITI - D0015 - C Sharp - OOP

## Programming Paradigms

Programming paradigm is a style, or "way" of programming

### Most Common Paradigms

- Assembly
- Procedural
- Functional
- Declarative
- Imperative
- Object-Oriented Programming (OOP)
- Multiple Paradigms

### Imperative Programming Vs. Declarative Programming

- **Imperative programming** describes how program executes
- **Declarative programming** describes what program executed

_For more details read this [🔗article](https://cs.lmu.edu/~ray/notes/paradigms/)_

## `C#`

- CS is a pure OOP language
- CS can be used to build desktop, mobile, web app, game, console, etc.

## OOP

- OOP is a programming paradigm based on imperative paradigm
- OOP based on classes, and objects
- Class is a blueprint

### OOP Pillars

- **Encapsulation** methods and attributes must be contained together in a single container (`class`)
- **Inheritance**
- **Abstraction** hide complexity, show what is necessary only
- **Polymorphism** (`poly` = `many`, `morphism` = `forms`), more than one form
  - **Overload** same name, with different signature, at compile time
  - **Override** only with inheritance, different implementation, at runtime

> [!Note]
> You can't create a standalone function in CS, i.e. functions(methods), fields (attributes) must be wrapped inside a class

> [!Note]
> C# with visual studio, we create solution and solution can have a multiple projects.

**Basic CS Console Program**:

```cs
using System;

namespace ConsoleApp1 {
	class Program {
		public static void main(string[] args) {
			Console.log("Hello, World!");
		}
	}
}
```

### key Points

- `Main` Function is the entry-point of execution.
- Compilation output from cs compiler, not a binary, it's `IL` (Indeterminate Language)
- `BCL` (Base Class Library) like `System` in the code above

## Namespace in CS

- Namespace is a container of `classes`, `structs`, `interfaces`, `enums`, and `delegates`.
- Declared using the `namespace` keyword
- Benefits
  - Avoid `naming collision`
  - Group related types logically

## DataTypes in CS

- **Value types** stored in stack, directly hold data in memory.
- **Reference types** stored in heap, hold a reference to the data in heap.

### Value types

- Numeric types
  - `byte` (8 bit)
  - `short` (16 bit), `ushort` (16 bit unsigned)
  - `int` (32 bit), `uint` (32 bit unsigned)
  - `long` (64 bit), `ulong` (64 bit unsigned)
- Floating-Point types
  - `float` (32 bit)
  - `double` (64 bit)
  - `decimal` (128 bits, high precision for financial calculations)
- Some other types
  - `bool`: Represents `true` or `false`.
  - `char`: Represents a single Unicode character (`'A'`).
  - `struct`: A composite value type.
  - `enum`: Represents a set of named constants.

### Reference types

- `string`: Represents a sequence of characters.
- `object`: Base type of all types in C#.
- Class Types:
  - `class`: A blueprint for creating objects.
- Array Types:
  - Fixed-size or dynamic collection of elements of the same type.
- `interface`: Defines a contract that a class or struct can implement.
- `delegate`: Represents a reference to a method.

### String Representation

#### String Concatenation

```cs
int x = 10;
int y = 20;
Console.WriteLine("sum = " + x + " + " + y + " = " + (x + y)");
```

#### String Holder

```cs
int x = 10;
int y = 20;
Console.WriteLine("sum = {0:c} + {1:c} = {2:c}", x, y, x+y);
```

#### String Interpolation

```cs
int x = 10;
int y = 20;
Console.WriteLine($"sum: {x:c} + {y:c} = {x+y:c}")
```
