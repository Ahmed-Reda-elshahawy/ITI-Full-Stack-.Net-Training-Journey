# 🔖 ITI - D0027 - C Sharp - Basics (Part7)

## Delegates

- When attach multiple methods with the same delegate, thats call multi-cast

## Events

- Publisher: The class that contains the event, and also fire the event.
- Subscriber: The class that subscribe to the event, and also handle the event.

> [!Note]
>
> - Using `event` keyword to create an event.
> - Using `+=` to subscribe to an event.
> - Using `-=` to unsubscribe from an event.
> - Using `Invoke` to raise an event.
> - Using `delegate` to create a delegate.
> - Using `event` with `delegate` to create an event, and also prevent the subscriber from using `=` to assign a new method to the event.

### Event Best Practices in `C#`

- build events using `EventHandler` delegate.
- `EventHandler` delegate is a predefined delegate in `C#`, which receives two parameters:
  - `object sender` : The object that raised the event.
  - `EventArgs e` : The event data.
- `EventArgs` is a predefined class in `C#`, which is used to pass the event data.
- Make handler methods name to start with prefix `On` (e.g. `OnEventName`).
- Make the event `protected` and `virtual` to allow derived classes to override the event.

## Partial Classes

- Partial classes is a unique feature of C#.
- It can break the functionality of a single class into many files.

### Partial Methods

- Partial methods is a unique feature of C#.
- Partial methods before C#0.7 can only be defined as `private` and `void`.
