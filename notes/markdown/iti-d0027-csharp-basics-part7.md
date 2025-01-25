# 🔖 ITI - D0027 - C Sharp - Basics (Part7)

> 📖 is used for notes covered in the lecture, 💡 for extra interesting notes.

## 📖 Events

- Events are a mechanism for communication between objects.
- They allow a class (publisher) to notify other classes (subscribers) when something happens.
- **Publisher**:

  - The class that **defines** (contains) the event.
  - The class that **raises** (fires) the event.

- **Subscriber**:

  - The class that **subscribes** to the event.
  - The class that **handles** the event.
  - It provides a method that will be called when the event is raised.

- **Event Declaration**:

  - Events are declared using the `event` keyword.
  - They are typically based on a delegate type.

- **Event Subscription**:

  - Subscribers can subscribe to an event using the `+=` operator to attach a method (event handler) to the event.
  - Subscribers can unsubscribe from an event using the `-=` operator.

- **Event Raising (Invocation or Firing)**: The publisher raises the event using the `Invoke` method.

### 📖 Events Best Practices in `C#`

1. **Use `EventHandler` Delegate**:

   - The `EventHandler` delegate is a predefined delegate in C# that simplifies event declaration.
   - It takes two parameters:
   - `object sender`: The object that raised the event.
   - `EventArgs e`: The event data (can be customized using a derived class).

2. **Naming Conventions**:

   - Use the `On` prefix for event handler methods (e.g., `OnButtonClick`).
   - Use meaningful names for events (e.g., `ButtonClicked`, `DataReceived`).

3. **Access Modifiers**

   - Declare events as `protected` and `virtual` to allow derived classes to override them.

4. **Custom Event Data**:

   - Use a class derived from `EventArgs` to pass custom data to event handlers.

5. **Null Checking**:

   - Always check if the event is `null` before invoking it to avoid `NullReferenceException`.
   - Use the null-conditional operator (`?.`) to safely invoke the event.

**Example: Basic Event Handling in C#**:

```csharp
using System;

class Program
{
    static void Main()
    {
        var publisher = new Publisher();
        var subscriber = new Subscriber();

        // Subscribe to the event
        publisher.MyEvent += subscriber.HandleEvent;

        // Raise the event
        publisher.RaiseEvent("Hello, World!");
    }
}

// Publisher class
public class Publisher
{
    // Declare the event
    public event EventHandler<string> MyEvent;

    // Method to raise the event
    public void RaiseEvent(string message)
    {
        MyEvent?.Invoke(this, message); // Null check before invoking
    }
}

// Subscriber class
public class Subscriber
{
    // Event handler method
    public void HandleEvent(object sender, string message)
    {
        Console.WriteLine($"Event received: {message}");
    }
}
```

**Practical Example: Using Events**:

```csharp
public class Location
{
    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj)
    {
        var other = obj as Location;
        if (other == null) return false;
        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"Location {{ X = {X}, Y = {Y} }}";
    }
}

public class Ball
{
    private Location _currentLocation;

    public Ball(int id, Location currentLocation)
    {
        Id = id;
        CurrentLocation = currentLocation;
    }

    public int Id { get; set; }

    public Location CurrentLocation
    {
        get => _currentLocation;
        set
        {
            if (_currentLocation != null && _currentLocation.Equals(value)) return;
            _currentLocation = value;
            OnLocationChanged(new LocationChangedEventArgs(value));
        }
    }

    // Declare the event using EventHandler and custom EventArgs
    public event EventHandler<LocationChangedEventArgs> LocationChanged;

    // Method to raise the event
    protected virtual void OnLocationChanged(LocationChangedEventArgs e)
    {
        LocationChanged?.Invoke(this, e);
    }

    public override string ToString()
    {
        return $"Ball {{ Id = {Id}, CurrentLocation = {CurrentLocation} }}";
    }
}

// Custom EventArgs class to pass event data
public class LocationChangedEventArgs : EventArgs
{
    public LocationChangedEventArgs(Location newLocation)
    {
        NewLocation = newLocation;
    }

    public Location NewLocation { get; }
}

public class Player
{
    public Player(int id, string name, string team)
    {
        Id = id;
        Name = name;
        Team = team;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Team { get; set; }

    // Event handler method
    public void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        Console.WriteLine($"Player {Id} is running to {e.NewLocation}...");
    }

    public override string ToString()
    {
        return $"Player {{ Id = {Id}, Name = {Name}, Team = {Team} }}";
    }
}

public class Referee
{
    public Referee(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    // Event handler method
    public void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        Console.WriteLine($"Referee {Name} is looking at {e.NewLocation}...");
    }

    public override string ToString()
    {
        return $"Referee {{ Name = {Name} }}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var ball = new Ball(5, new Location(1, 2));
        var p1 = new Player(1, "John", "X");
        var p2 = new Player(2, "Sheldon", "Y");
        var p3 = new Player(3, "Jemmy", "Z");
        var referee = new Referee("Referee Smith");

        // Register event handlers
        ball.LocationChanged += p1.OnLocationChanged;
        ball.LocationChanged += p2.OnLocationChanged;
        ball.LocationChanged += p3.OnLocationChanged;
        ball.LocationChanged += referee.OnLocationChanged;

        // Change the ball's location
        ball.CurrentLocation = new Location(20, 10);
        ball.CurrentLocation = new Location(5, 9);
    }
}

// Output
// Player 1 is running to Location { X = 20, Y = 10 }...
// Player 2 is running to Location { X = 20, Y = 10 }...
// Player 3 is running to Location { X = 20, Y = 10 }...
// Referee Referee Smith is looking at Location { X = 20, Y = 10 }...
// Player 1 is running to Location { X = 5, Y = 9 }...
// Player 2 is running to Location { X = 5, Y = 9 }...
// Player 3 is running to Location { X = 5, Y = 9 }...
// Referee Referee Smith is looking at Location { X = 5, Y = 9 }...
```

## 📖 Partial Classes

- A **partial class** allows you to **split** the definition of a class across multiple files.
- All parts of the class must use the `partial` keyword and be in **the same namespace and assembly**.

### 📖 Benefits of Partial Classes

- **Code Organization**: Split large classes into smaller, more manageable files.
- **Separation of Concerns**: Different parts of the class can be maintained by different developers.
- **Code Generation**: Allow tools (e.g., Visual Studio designers) to generate code in one file while you write custom code in another.

### 📖 Partial Class Syntax

```csharp
public partial class ClassName
{
    // Members (fields, properties, methods, etc.)
}
```

**Example**:

```csharp
// File: Person.cs (Part 1)
public partial class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

// File: PersonExtensions.cs (Part 2)
public partial class Person
{
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}

// Usage
class Program
{
    static void Main()
    {
        var person = new Person { FirstName = "John", LastName = "Doe" };
        Console.WriteLine(person.GetFullName()); // Output: John Doe
    }
}
```

### 💡 Partial Class Rules

- All parts of the class must have the same access modifier (e.g., `public`).
- If any part of the class is declared as `abstract`, the entire class is considered `abstract`.
- If any part of the class is declared as `sealed`, the entire class is considered `sealed`.

### 📖 Partial Methods

- A **partial method** allows you to declare a method in one part of a partial class and optionally implement it in another part.
- Partial methods are implicitly `private` and must return `void`.

**Syntax**:

```csharp
// Declaration
partial void MethodName();

// Implementation
partial void MethodName()
{
    // Implementation
}
```

**Rules**:

- Partial methods must be declared within a partial class.
- If a partial method is not implemented, the compiler removes the method signature and all calls to it.

**Example**:

```csharp
// File: Logger.cs (Part 1)
public partial class Logger
{
    partial void LogMessage(string message);

    public void Log(string message)
    {
        LogMessage(message); // Call the partial method
        Console.WriteLine("Logging: " + message);
    }
}

// File: LoggerExtensions.cs (Part 2)
public partial class Logger
{
    partial void LogMessage(string message)
    {
        Console.WriteLine("Custom Log: " + message);
    }
}

// Usage
class Program
{
    static void Main()
    {
        var logger = new Logger();
        logger.Log("Hello, World!");
    }
}

// Output
// Custom Log: Hello, World!
// Logging: Hello, World!
```

## Intro to WindowsForms

<!-- TODO: add WindowsForms notes -->

_Will be added soon..._
