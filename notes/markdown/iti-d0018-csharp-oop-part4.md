# 🔖 ITI - D0018 - C Sharp - OOP (Part4)

## Class

Classes in C# can have:

- fields
  - support field initialization
- constructors
  - support default constructor
  - support explicit default constructor
  - support parameterized constructors
  - support copy constructor
- properties
- indexers
- methods
- finalizers: destructors

> [!Note]
>
> - In case we deal with struct, which all its properties are public so we can declare it without its constructor
>
> ```cs
> public struct Employee {
> 	public int id;
> 	public string name;
> }
>
> Employee emp; // that will only work if id, name is already public fields in struct otherwise will be raise an error
> emp.id = 20;
> emp.name = "john";
> Console.WriteLine($"{emp.name}, ${emp.id}"); // john, 20
> ```
>
> - But if some of their fields are private, so we must declare it with it's default constructor i.e. `Employee emp = new Employee()`.
> - So using `new` keyword with struct, it just for initialization but with classes it uses for creation and also initialization.

> [!Note]
> Classes are a reference type, so reference can be initialized with `null`.
>
> ```cs
> Employee emp = null; // will pass
> emp = new Employee(); // allocation, now we create object in heap and assign its address to emp reference
> ```

### Class Constructor

```cs
public class Employee {
	int _id;
	string _name;
	public Employee(int id, string name) { // parametrized constructor
		this._id = id;
		this._name = name;
	}
	public Employee(Employee emp) { // copy construcotr
		this._id = emp._id;
		this._name = emp._name;
	}
	public employee(int id): this(id, "New Employee"){} // constructor chaining (redirection)
}
```

> [!Note]
> As we observe in the example above, that inside the copy constructor we can access `emp` fields which is already private, but that is valid because we are in the same type, so this situation is valid.

### Properties

- A *property* is a member that provides a flexible mechanism to read, write, or compute the value of a data field.
- One expression with getter (`get`) and setter (`set`)
- Property can have only `get` without `set` and vice versa.

```cs
public class Employee {
    int _id;
    string _name;
    public Employee(int id, string name) { // parametrized constructor
        this._id = id;
        this._name = name;
    }
    public int Id {
        set {
            if (value >= 0 && value <= 100) {
                this._id = value;
            }
        }
        get {
            return this._id;
        }
    }
    public string Name {
        set {
            this._name = value;
        }
        get {
            return this._name;
        }
    }
}
```

### Object Initializers

Object initializers let you assign values to any accessible fields or properties of an object at creation time without having to invoke a constructor followed by lines of assignment statements.

```cs
public class Cat
{
    public int Age;
    public string Name;

    public Cat()
    {
    }

    public Cat(string name)
    {
	    this.Age = 0;
        this.Name = name;
    }
}

Cat cat = new Cat("Fluffy") {Age = 10};
```

### Finalizers (Destructors)

- Destructors are used to perform any necessary final clean-up when a class instance is being collected by the garbage
- Use cases, closes the database connection within an object before deleting it.
- Each class can have only single destructors, also destructors can't have any parameters

```cs
class Car
{
    ~Car()  // finalizer
    {
        // cleanup statements...
    }
}
```

### Array of Objects

```cs
Class Cat {
	public string Name;
	public Car(string name) {
		this.Name = name;
	}
}

Cat[] cats = new Cat[10]; // now we have array of object_references and each reference initialized with null value
Cat[] cats2 = new Cat[2] {
	new Cat("Kitty"),
	new Cat("Fluffy"),
}; // now in this array we have an array of object references and already initialized with their objects
```
