# 🔖 ITI - D0009 - CST (JavaScript 6, ECMA6, ECMA.NEXT) (Part 1)

## ECMA6

- In 2015
- New features
  - Modules
  - Classes
  - Arrows
  - Promises
  - Block Scoping
  - Enhanced Object literals

## Block Scoping using (`let`, `const`)

### `var`

- hoisted
- can be redeclared
- can be reassigned
- by default initialized by `undefined`

```js
console.log(x); // undefined (hoisting)
var x = 10;
console.log(x); // 10
```

> [!Note]
>
> - Function statement is hoisted
> - Function expression is not hoisted (only variable which points to the function will be hoisted)

```js
myFunc1(); // will run correctly
function myFunc1() {
  // some code
}
myFunc1(); // will run correctly
```

```js
myFunc(); // error becuase now variable myFunc is not initlaized yet so it has an undefined value
var myFunc = function () {
  // some code
};
myFunc(); // will run correctly
```

> [!Note] > `let` and `const` are not completely hoisted, which means that engine already sees them (setting them in the **temporal dead zone**) but, they are not initialized yet, on the other hand `var` will be initialized with `undefined`

### `let`

- block scoped
- not completely hoisted (setting in the **temporal dead zone**)
- can be reassigned
- cannot be redeclared

```js
console.log(x); // error (cannot access x before initialziation)
let x = 10;
let y;
console.log(x); // 10;
conosle.log(y); // undefined
```

```js
let x = 1000;
if (5 > 2) {
  console.log(x); // error
  let x = 10;
}
```

### `const`

- block scoped
- not completely hoisted (setting in **temporal dead zone**)
- constant variable, cannot be reassigned
- cannot be redeclared
- cannot be declared without initialization

```js
const x; // error (missing initializer)
console.lgo(y); // error (cannot access x before initialziation)
const y = 10;
console.log(y); // 10
y = 20; // error (assignment to constant variable)
```

### `freez` Vs. `seal`

- `freez`
  - control on object state
  - so with using freeze we restrict adding, or deleting or modifying object property (i.e. makes object state **immutable**)
  - but you can change object identity (reference)
- `seal`
  - is the same as freeze, but we can modify any value of existing property on object but still we cannot add or delete and property to the object

## String Updates: String literals using ` (``) `: string interpolation

```js
var name = `john doe`;
var message = `hello
world`;

var num = 10;
var outMessage = `num: ${num}`;
```

## Function Updates

### Function Default Parameters

```js
function sum(a, b, c) {
  return a + b + c;
}

sum(10); // error

function sum2(a, b = 1, c = b * 2) {
  return a + b + c;
}

sum2(10); // correct
```

### Arrow Functions

- arrow functions doesn't have `this`, and `arguments`
- arrow function should be declared as function expression
- remove curly braces, if and only if function have just one statement which is return
- remove `()`braces if we have just single parameter
- not hoisted (cannot be called before declaration)
- in arrow function, `this` maps (binds) to the closest outer `this`
- cannot be used as a constructor function

**Example:**

```js
const sum = (a, b) => {
  return a + b;
};

const sum = (a, b) => a + b;

const isEven = (num) => num % 2 == 0;
```

```js
function Person(id, name) {
  this.ID = id;
  this.Name = name;
  setTimeout(function () {
    console.log(`${this.ID}: ${this.Name}`);
  }); // undefined: undefined- here this refer to window
}

function Person2(id, name) {
  this.ID = id;
  this.Name = name;
  var that = this;
  setTimeout(function () {
    console.log(`${that.ID}: ${that.Name}`);
  }); // id_value: name_value
}

function Person3(id, name) {
  this.ID = id;
  this.Name = name;
  setTimeout(() => {
    console.log(`${this.ID}: ${this.Name}`);
  }); // id_value: name_value - here this refer to Person3 object
}
```

### `...` rest and spread operators

- rest
  - with function parameters
  - any function can have only one rest parameter
  - must be last parameter on function
  - will be represented as array
- spread

**Example:**

```js
const calc = (op, ...operands) => eval(operands.join(op)); // rest operator
calc("+", 10, 20, 30, 40, 50);
calc("+", ...[10, 20, 30, 40]); // spread operator
```

> [!Note]
>
> `min` and `max` methods in `Math` accept spreaded values

### Destruction

```js
let myObj = {
  id: 100,
  name: john,
  address: tanta,
};

const { id, name } = myObj;
console.log(id); // 100
console.log(name); // john

// destruct with different naming
const { id: ID, name: Name } = myObj;
console.log(ID); // 100
console.log(Name); // john

const arr = [100, 200, 300, 400, 500];
const [x, y, z, ...rest] = arr;
console.log(x); // 100
console.log(y); // 200
console.lgo(z); // 300
console.log(rest); // [400, 500]
```

> [!Note]
> Concise function is a function must be written inside an object declaration

### Concise Function

- Must be written inside an object declaration

```js
const myObj = {
  id: 100,
  name: "John",
  print() {
    return `id: ${this.id}, name: ${this.name}`;
  },
};
```

### Computed Property

```js
const person = {};
const firstName = "first name";
person[firstName] = "John";
console.log(person[firstName]); // "John"
```

```js
var titleParameter = "Title";
var titleValue = "ES6";

var Book = {
	[titleParameter]: titleValue
	["Book"+titleParameter]: titleValue;
	[titleValue+" printing"]: function() {
		return this[titleParameter] + ": " + this["Book" + titleParameter];
	}
}
```

_**[Back to the Index](../../README.md#index)**_
