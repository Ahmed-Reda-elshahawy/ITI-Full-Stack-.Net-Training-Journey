# 🔖 ITI - D0023 - C Sharp - Basics (Part3)

## Bitwise Operators

- Bitwise operators are used to perform bit-level operations on integers.
- Bitwise operators work on bits and perform bit-by-bit operations.
- Operators:
  - `&` (AND)
  - `|` (OR)
  - `^` (XOR)
  - `~` (NOT)
  - `<<` (Left Shift)
  - `>>` (Right Shift)

**Example**:

```csharp
int x = 0b1;
int y = 0b0;

Console.WriteLine(x&y);  // 0
Console.WriteLine(x|y);  // 1
Console.WriteLine(x^y);  // 1
Console.WriteLine(~x);   // -2
Console.WriteLine(x<<1); // 2
Console.WriteLine(x>>1); // 0
```

## Enum: Bit Flag Enumerations

_Review Enumerations in the [previous Lecture](./iti-d0022-csharp-basics-part2.md#enums)._

- Bit Flag Enumerations are used to store multiple values in a single variable.
- Using bitwise operators, we can combine multiple enum values into a single variable.
- To define a Bit Flag Enum, we need to use the `Flags` attribute.
  - This attribute makes the enum values behave like bit flags, and show descriptive names for the combined values rather than the integral value.
- To combine multiple enum values, we use the `|` operator.
- To check if a flag is set, we use the `&` operator.
- To remove a flag, we use the `&` operator with the `~` operator.
