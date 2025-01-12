# 🔖 ITI - D0019 - C Sharp - OOP (Part5)

## Automatic Properties

_Review Properties in the [previous lecture](./iti-d0018-csharp-oop-part4.md#properties)_

- Used to declare property when no logic is required in the property accessors (`get`, `set`)
- Behind the scenes, the compiler will create a private field for this property, which will be accessed through accessors (`get`, `set`)
- Automatic property can be initialized similar to other fields
  _Example:_

```csharp
public class User
{
    public int Id { get; set; }  // automatic property
}

public class Cat
{
    public string Name { get; set; } = "Kitty";  // automatic property with initialization
}
```

## OOP Pillar: Inheritance

- Inheritance is `is-a` relationship
  - **Derived Class** (child) - the class that inherits from another class
  - **Base Class** (parent) - the class being inherited from

### Key Points

- C# supports:
  - **Multilevel inheritance** (class inherits from a derived class).
  - **Polymorphic behavior** using `base` class references.
- C# does **not** support **multiple inheritance** (but supports multiple interface implementation).
- Derived class does **not inherit constructors** or **finalizers**.
- A derived class contains an instance of its base class, accessible via the `base` keyword.
- **Base class reference** can point to an object of the derived class.
- **Casting**:
  - Safe casting is required when accessing derived class members from a base class reference.
  - Invalid casting results in a runtime error.

```csharp
A a = new B();   // Base class reference pointing to child object.
B b = (B)a;      // Casting base class reference to derived class.
B bObj = new A(); // Error: Base object cannot be assigned to derived reference.
```

**_Example_**

```csharp
class Vehicle  // base class (parent)
{
  public string brand = "Ford";  // Vehicle field
  public void honk()             // Vehicle method
  {
    Console.WriteLine("Tuut, tuut!");
  }
}

class Car : Vehicle  // derived class (child)
{
  public string modelName = "Mustang";  // Car field
}

Car car = new Car(); // Create an object from Car, Vehicle default constructor will be executed, then Car default constructor will be executed

Car c = new Vehicle(); // Create an object from Vehicle hold by reference from type Car
```

## Virtuality and Method Resolution

### `new` keyword

- **Static binding** (early binding).
- Hides the base class method without affecting runtime behavior.
- The method resolution is based on the **reference type**, not the runtime object type.
- Useful for explicitly hiding members from the base class.

```csharp
class A {
    public void DoSomething() => Console.WriteLine("A");
}

class B : A {
    public new void DoSomething() => Console.WriteLine("B");
}

A a = new B();
a.DoSomething(); // Output: "A" (resolved based on reference type)

```

### Virtual and Override

- **Dynamic binding** (late binding).
- **Enables polymorphism**: Method resolution happens at runtime.
- **Requirements** for overriding:
  - Base method must be marked as `virtual`, `abstract`, or `override`.
  - Derived class method must use `override` to provide a new implementation.
- Applicable to **methods**, **properties**, and **indexers**.

```csharp
using System;

class A
{
    public virtual void DoSomething()
    {
        Console.WriteLine("A");
    }
}

class B : A
{
    public override void DoSomething()
    {
        Console.WriteLine("B");
    }
}

class C : B
{
    public new virtual void DoSomething()
    {
        Console.WriteLine("C");
    }
}

class D : C
{
    public void DoSomething() // Non-virtual method
    {
        Console.WriteLine("D");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Level 1: Reference and object of the same type
        A a = new A();
        a.DoSomething(); // Output: "A"

        // Level 2: Base reference pointing to a derived object
        A ab = new B();
        ab.DoSomething(); // Output: "B"

        // Level 3: Base reference pointing to a deeper derived object
        A ac = new C();
        ac.DoSomething(); // Output: "B" (C hides but does not override B's method)

        // Level 4: Base reference pointing to the most derived object
        A ad = new D();
        ad.DoSomething(); // Output: "B" (D does not override, A resolves to B)

        // Intermediate Levels
        B bc = new C();
        bc.DoSomething(); // Output: "B" (C hides but does not override B's method)

        C cd = new D();
        cd.DoSomething(); // Output: "C" (D hides C's method but doesn't override it)
    }
}

```
