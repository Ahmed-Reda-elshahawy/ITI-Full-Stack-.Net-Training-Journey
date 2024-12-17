# 🔖 ITI - D0006 - CST (JavaScript 5, ECMA5) (Part 3)

## Functions

- Function in JS is a **first class object**, which means
  - you can declare function as a variable
  - you can pass function to another function as parameter (called **callback**)
  - you can return function from a function (called **closure**)
  - you can declare a function as a property inside an object
- JS **doesn't** support **function overloading**
- JS functions are actually **objects** that are **invokable**

### Function Deceleration

- Function statement
- Function expression
- Arrow function
- Anonymous function

#### Function Statement

```js
function functionName(param1, param2, ....) {
	return returnedValue; // by default it will return undefined, if we didn't return value explicitly
}
```

#### Function Expression

- Define function as a variable
- Can't be called before it's declaration (because of **Hoisting** [^1])

```js
var variableName = function functionName(param1, param2, ...) {
	return returnVAlue;
}

// functionName is optoinal
```

### Call Functions

```js
functionName(arg1, arg2,...);
```

### `this`, `arguments` Function Members

- Function expression and function declaration has 2 members:
  - **this**: refers to caller object
  - **arguments**: collection which contains all passed values
- These members only accessible inside the function

**Example using `arguments`**

```js
function func(a, b) {
  console.log(arguments);
}

func(1, 2, 3); // will print on console arguments object data, which will be [1, 2, 3]
```

**Example using `this`**

```js
var id = 0,
  name = omar;
std = {
  id: 1,
  name: john,
  print: function () {
    return "id: " + id + ", name: " + name;
  },
};
std.print(); // 'id: 0, name: omar'
```

```js
var id = 0,
  name = omar;
std = {
  id: 1,
  name: john,
  print: function () {
    return "id: " + this.id + ", name: " + this.name;
  },
};
std.print(); // 'id: 1, name: john'
```

### Function Patterns

#### Constructor Function

- Workaround to create object, but constructor function is not a pure class
- Define fields by using:
  - **`var`**: for private fields
  - **`this`**: for public fields
- Use **`new`** keyword before calling constructor function

```js
function Person(id, name, age) {
  // fields:
  // private: using var
  var Age = age;
  // private: using this
  this.ID = id;
  this.Name = name;
}

// to use constructor function, use new keywords before calling method
var person1 = new Person(100, "John");
console.log(typeof person1); // 'object'
console.log(person1.constructor.name); // 'Person'
```

#### Factory Function

```js
function getStudent = (std) {
	return {
		ID: std.ID,
		Name: std.Name
	};
}

var std1 = {ID: 0, Name: 'John'};
var std2 = getStudent(std1);
```

## Boolean

- JS supports truthy and falsy expressions
- These expressions will be evaluated as false
  - `false`
  - `undefined`
  - `0`
  - `NaN`
  - `null`
  - `""` (empty string)
- Other expressions will be evaluated to true

### Logical Operators `&&`, `||`

#### AND (`&&`)

- if the whole expression is evaluated to be `truthy`, so `AND` will return last `truthy` value
- if the expression is evaluated to be `falsy`, so `AND` will return first `falsy` value

#### OR (`||`)

- if the whole expression is evaluated to be `falsy`, so `OR` will return last `falsy` value
- if the expression is evaluated to be `truthy`, so `OR` will return first `truty` value

## Date

```js
var date = new Date(); // holds current date
var date = new Date(dateString); // e.g. "October 13,2014 11:13:00"
var date = new Date(year, month, day, hours, minutes, seconds, milliseconds);
```

### Date Attributes

- seconds, minutes (0-59)
- hours (0-23)
- day (0-6) (_starting from Sunday with index 0_)
- date (1-31)
- month (0-11)
- year

### Date Methods

#### Get Methods

```js
var myDate = new Date("November 25, 2006 11:13:55");
```

- **getDate()** (result from above example: 25)
- **getMonth()** (10)
- **getYear()** (2006-1900 = 106)
- **getFullYear()** (2006)
- **getDay()** (0 (refers to sudnay))
- **getHours()** (11)
- **getMinutes()** (13)
- **getSeconds()** (55)
- **getTime()** (11:13:55)

#### Set Methods

```js
var myDate = new Date("November 25, 2006 11:13:55");
```

- **setDate(number)**
- **setHours(number)**
- **setMinutes(number)**
- **setMonth(number)**
- **setSeconds(number)**
- **setTime(number)**
- **setFullYear(number)**

#### to Methods

```js
var myDate = new Date("November 25, 2006 11:13:55");
```

- **toUTCString()** ("Sat,25 Nov 2006 09:13:00 UTC")
- **toLocaleString()** (based on date format in your OS)
- **toLocaleTimeString()**
- **toString()**
- **toDateString()** (Sun Nov 1 2006)

## Math

- Math object contains set of `static` Methods and set of `properties`

### Properties

- `Math.PI`

### Methods

- **`Math.round(number):number`**
- **`Math.ceil(number):number`**
- **`Math.float(number):number`**
- **`Math.pow(base, power):number`**
- **`Math.random():number`** (returned number is $]0, 1[$)

```js
console.log(Math.floor(Math.random() * 10)); // 9 (ouput will be range [0,9])
console.log(Math.floor(Math.random() * 11)); // 10 (output will be in range [0,10])

/** Randomize within range
(Math.floor(Math.random() * (max - min + 1)) + min) = value in range [min, max]
*/
```

## Side Note

JS runtime, creates AST (Abstract Syntax Tree)

[^1]: **Hoisting** is JS default behavior of moving declarations to the top of the current scope, applies to variable declarations and to function declarations

_**[Back to the Index](../../README.md#index)**_
