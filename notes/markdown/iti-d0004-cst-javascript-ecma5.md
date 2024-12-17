# 🔖 ITI - D0004 - CST (JavaScript 5, ECMA5)

## JavaScript

- JavaScript is a **scripting** language (not standalone language), so it needs a host(HTML) to run
- **Loosely typed** language, so inputs should be validated
- **Interpreted** language
- **Object based** language (_not pure object-oriented_)
- **Case-sensitive** language
- **Integrated with HTML**
- **Event handling**

> [!Info] Loosely types vs. Strongly typed languages
>
> - **Strongly Typed**: according to variable type, you must assign a correct value to it (like C/C++).
>
> ```c
> int x = "iti"; // compilation error
> int x = 10; // correct declaration
> ```
>
> - **Loosely Typed**: according to assigned value for variable, type of this variable will be determined. (like JS)
>
> ```js
> var x; // variable with type undefined, value undefined
> x = true; // now variable with type boolean, value true
> ```

## How to use JS with HTML

- Internal (_using:_ `<script></script>` inside `head` or `body` tag)
- External (_using_: separated script file, and include it using `<script src="./index.js"></script>`)

## Dealing with not-allowed JS on Browsers

Use `<noscript></noscript>`

## Variables and Datatypes

- Primitive (_datatype takes single atomic value_) datatypes in JS is **immutable**
- **null** datatype is an object

### Preemptive Datatypes

- Number
- Boolean
- String
- Null
- Undefined

> [!Info]
>
> - **Number** is represented under the hood as _double_ (8 bytes) followed IEEE 754 standards
>   - **Issue:** summation of floating number with it's double value will not have the exact expected answer (e.g. $0.1+0.2=0.30000000000004$)
> - Max **Number** in JS is quadrillion $10^{16}$

#### `undefined` in JS

- `undefined` is a datatype and a value also
- Uninitialized variable in JS by default will take type and value of `undefined`

### Objects

- **User defined** objects
- **Language** objects (Number, Date, Math, String, ...)
- **Browser** objects (navigator, window, History, ...): BOM
- **HTML** objects: DOM

### Variable Declaration

- Use `var` keyword
- To check variable type, use `typeof` keyword

```js
var x = 10;
console.log(typeof x); // number
```

> [!Tip]
> We can declare variable without `var` keyword but it must be initialized, but it's not recommended
>
> ```js
> x = 20;
> ```
>
> if we defined a variable without `var` keyword locally inside a function, it will be exposed globally after first function call
>
> ```js
> function myfunc() {
>   y = 20;
> }
>
> console.log(y); // error
> myfunc();
> console.log(y); // 20
> ```

> [!Note] > `typeof` is not the best way to check type of variable, especially when dealing with objects so we will use _prototype_ name to know exact type of variables

#### Variable Declaration Type

##### Literal declaration

```js
var x = 10; // creation on the fly for object from type number (wrapper object), after call this object
```

##### Constructor declaration

```js
var x = new Number(10);
var y = new Number(10);
console.log(x == y); // false
console.log(x.valueOf() == y.valueOf()); // true
```

### Variable Characteristics

- name
- type
- value
- address

> [!Note]
> Variables will be allocated in **stack** or **heap** memory.

### Object Declaration

```js
class Person {
  id;
  salary;
}

var ali = new Person(10, 1000); // ali is reference, variable value represnets the object it self
ali = null;

var obj = {}; // literal object
```

- A reference will be stored on the stack and point to the object's location on the heap.
- object will be stored on the heap and contains object values.

#### Object Characteristics

- **Object identity**: object address
- **Object state**: object values
  > [!Note]
  > Objects with identical identities share the same state.

> [!Tip] > `hasOwnProperty("prop")` is a method returns true if the current object instance has the property defined in its constructor or in a related constructor function.

### Variable Scopes

- Global Scope (script scope, namespace scope)
- Local Scope (function scope)

> [!Info]
> We didn't have a **block scope** in ECMA5 using **var**
>
> ```js
> for (var i = 0; i < 10; ++i) {}
> console.log(i); // 10
> ```

### Type Coercion

- Implicit type conversion

```js
console.log(5 == "5"); // true , interpurter made implicit type convertion from number to string "5"=="5"
```

To avoid above behavior use

```js
console.log(5 === "5"); // false
```

_More Examples:_

```js
console.log(1 + "abc"); // 1abc;
console.log(true + "abc"); // trueabc;
console.log(1 * "2"); // 2 as a number
```

### Explicit type coercion

Use `parseInt`, `parseFloat`

**Using Algorithm**

```txt
trim string from both sides

if (string length is 0):
	return NaN

if (string not start with digit):
	return NaN

while (current character is digit):
	convert digit to integer and add it to result

return result
```

> [!INFO]
>
> - `NaN` is a special value in JS from type Number, not equal anything even itself
>
> ```js
> console.log(typeof NaN); // number
> console.log(NaN === NaN); // false
> console.log(undefined + 10); // NaN
> console.log(10 / 0); // Infinity
> console.log(0 / 0); // NaN
> console.log(10 / -0); // -Infinity
> console.log(Infinity / Infinity); // NaN
> console.log(Infinity * Infinity); // NaN
> console.log(Infinity + 5); // Infinity
> ```
>
> - `isNaN`function checks it's input is number or not
>
> ```js
> console.log(isNaN(NaN)); // true
> console.log(isNaN("abc")); // true
> console.log(isNaN(123)); // false
> console.log(isNaN("123")); // false
> ```
>
> - `Number.isNaN()` checks it's input is `NaN` or not
>
> ```js
> Number.isNaN(NaN); // true
> Number.isNaN(123); // false
> Number.isNaN("123"); // false
> ```

#### `parseInt` with radix

- radix represents base of number system (e.g. (binary = 2), (decimal = 10), (octal = 8))

```js
console.log(parseInt(1024, 2)); // 10
console.log(parseInt(1024, 10)); // 1024
```

### Casting: `Number`, `+` have the same algorithm

**Using Algorithm**:

```text
trim string from both sides

if (string length is 0):
	return 0;

if (all string characters is digits):
	return number
else:
	return NaN
```

_Examples:_

```js
console.log(Number("   123   ")); // 123
console.log(Number("12.5")); // 12.5
console.log(Number(null)); // 0
console.log(Number("")); // 0
console.log(Number("20") + "30"); // '2030'
console.log(Number("20") + +"30"); // 50
console.log(Number(undefined)); // NaN
console.log(Number(NaN)); // NaN
```

### Number: Built-in functions

#### Convert: number to string using `tostring`

You can use radix with `tostring` function

```js
console.log((123).tostring()); // '123'
console.log((123).tostring(2)); // '111101'
```

#### `toFixed`

Rounds number to specific floating point places

```js
console.log((123.549).toFixed(2)); // '123.55'
```

#### `toPercision`

Formats a number to a specified length.

```js
console.log((123.5).toPercision(6)); // '123.500'
```

#### `toLocalString`

Returns a number as a string, using local language format.

```js
console.log((1235235).toLocalString("ar-EG"));
console.log((new Date()).toLocalString("en-US))
```

### Number literals

```js
var d = 1024;
var b = 0b101; // binary
var h = 0x400; // hexadecimal
var o = 0200; // octal
var n = 1e6; // 1000000
var x = 1e-6; // 0.000001
```

## Dialog

- alert
- prompt
- confirm

> [!Note]
> All functions in JS will return `undefined` by default, unless we have an explicit return statement.

### alret

```js
prompt("message"); // will return undefined
```

### prompt

```js
prompt("message", "default_value"); // will return string or null
```

Returned value will be null, if user clicked `cancel` or `esc`.

### confirm

```js
confirm("message"); // will return boolean value
```

## Write on HTML document

Use `document.write()`, `document.writeln()`

```js
document.write("<p>Hello<p/>");
document.writeln("welcome");
```

_**[Back to the Index](../../README.md#index)**_
