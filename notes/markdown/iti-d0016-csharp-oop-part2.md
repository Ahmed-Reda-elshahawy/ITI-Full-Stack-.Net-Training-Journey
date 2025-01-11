# 🔖 ITI - D0016 - C Sharp - OOP (Part2)

## Parsing

- Convert string into another datatype
- `int.Parse(string)`
- `float.Parse(string)`

```cs
int x = int.Parse('5');
Console.WriteLine(x);
```

## Read Input From Console

```cs
int x = int.Parse(Console.ReadLine());
```

## Clear Console

```csharp
Console.Clear();
```

## Control Flow In C#

### Conditions

- `if`, `else if`, `else`
- `switch`

```cs
if (condition1)
{
  // block of code to be executed if condition1 is True
}
else if (condition2)
{
  // block of code to be executed if the condition1 is false and condition2 is True
}
else
{
  // block of code to be executed if the condition1 is false and condition2 is False
}
```

```cs
switch(expression)
{
  case x:
    // code block
    break;
  case y:
    // code block
    break;
  default:
    // code block
    break;
}
```

> [!Tip]
>
> **Regions**
>
> Regions in C# is a useful feature that helps manage code in areas that can be hidden or visible.
>
> ```cs
> #region MyRegion
> // your code ...
> #endregion
> ```

### Loops

- `for`
- `while`
- `do/while`

**For: Example**:

```csharp
for (int i = 0; i < 5; i++)
{
  Console.WriteLine(i);
} // 0 1 2 3 4
```

**While: Example**:

```csharp
int i = 0;
while (i < 5)
{
  Console.WriteLine(i);
  i++;
} // 0 1 2 3 4
```

**do/while: Example**:

```csharp
int i = 0;
do
{
  Console.WriteLine(i);
  i++;
}
while (i < 5); // 0 1 2 3 4
```

#### Jump Statements

- `break`
- `continue`
- `return`

##### Break

- Break loop and jump out it

```csharp
for (int i = 0; i < 6; i++)
{
  if (i == 2)
  {
    break;
  }
  Console.WriteLine(i);
} // 0 1
```

##### Continue

- skip current iteration

```csharp
for (int i = 0; i < 6; i++)
{
  if (i == 4)
  {
    continue;
  }
  Console.WriteLine(i);
} // 0 1 2 3 5
```

## Arrays

- Collection of `homogeneous data types` (the same type)
- Fixed size

**Examples: One Dimension array**

```csharp
int[] arr;
Console.WriteLine(arr); // Error: [CS0165] Use of unassigned local variable 'arr'
```

```csharp
int[] arr = null; // Allocate reference to array and initialize it with null
Console.WriteLine(arr); // Correct, it will output nothing
```

```csharp
int[] arr = new int[5]; // Allocate array with 5 elements
Console.WriteLine(arr[0]); // 0
```

```csharp
int [] arr = new int[3] {10, 20, 30}; // Allocate array with initialization
Console.WriteLine(arr[1]); // 20
```

```csharp
int[] arr = {10, 20, 30}; // Allocate array with initialization
Console.WriteLine(arr[0]); // 10
```

**Examples: 2D Array**:

```csharp
int[,] mat = new int[2, 3] { { 10, 20, 30 }, { 40, 50, 60 } };

Console.WriteLine(mat[0, 0]);  // 10
Console.WriteLine(mat.Length);  // 6
Console.WriteLine(mat.Rank);  // 2
Console.WriteLine(mat.GetLength(1)); // 2
Console.WriteLine(mat.GetLength(2)); // 3
```

### Array Common Properties

- `Length` returns the array length
- `Rank` returns array's number of dimensions

### Array Common Methods

- `GetLength(dimension)` return length for specific array `dimension`

## Functions (Methods)

```csharp
AccessModifier static methodName(parameter list) // function header
{
	// function body
}
```

### Access Modifier

- public
- private
- protected
- internal
- protected internal

> [!Note]
> All code paths inside a function in C# must return a value
>
> ```cs
> class Program {
> 	public static int sum(int a, int b) {
> 		if (a < 0 or b < 0) {
> 			return 0;
> 		}
> 		// here you must return value from this path or you will have a compilation error
> 	}
> }
> ```

> [!Note]
>
> **Function returns `void`**
> Function which returns `void`, that means we didn't need to return anything.
>
> ```csharp
> public static void DoSomething() {
> 	Console.WriteLine("Working...");
> }
> ```
