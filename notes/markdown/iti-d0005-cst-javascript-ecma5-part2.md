# 🔖 ITI - D0005 - CST (JavaScript 5, ECMA5) (Part 2)

## Strings

- String is immutable

```js
var name = "John"; // literal declaration
var name2 = new String("Joe"); // Constructor declaration

console.log(typeof name); // 'string'
console.log(typeof name2); // 'object'
console.log(name.constructor.name); // 'String'
console.log(name2.constructor.name); // 'String'
```

### String Propitiates

- `length`

### String Methods

- **`[]`**
- **`charAt(index):char|undefined`**
- **`indexOf(char,start):index|-1`**
- **`lastIndexOf(char,start):index|-1`**
- **`subString(start?, end?):string|undefined`**
  - `end` will be excluded
  - Any passed negative values will be 0
  - If parameters are not sorted, it will sort them automatically
- **`subStr(start,length):string|undefined`**
  - `start` accepts negative values
- **`slice(start?,end?):string|undefined`**
  - `end` will be excluded
  - `start`, `end` accept negative values
- **`split(splitter):string[]`**
- **`trim():string`**
- **`trimLeft():string`** , **`trimStart():string`**
- **`trimRihgt():string`** , **`trimEnd():string`**
- **`replace(searchValue, newValue):string`**
- **`replaceAll(searchValue, newValue):string`**
- **`toLowerCase():string`**
- **`toUpperCase():string`**

### Methods related to HTML

- **`bold():string`**
- **`italics():string`**
- **`fontsize():string`**

## Arrays

- Collection of data
- Object data type
- Array is mutable

```js
var arr = []; // literal decleartoin
var arr2 = new Array(); // constructor decleartion
console.loge(arr.constructor.name); // 'Array'
```

### Array Types

- Dense
- Associative

#### Associative arrays

- Array is already an object
- You can set key and value pairs

```js
var arr = [100, 200, 300];
arr.name = "John";
console.log(arr[0]); // 100
console.log(arr["name"]); // John
console.log(arr.name); // John
```

### Array Properties

- `length`

### Array Methods

- **`[]`**
  - If we set item at $index > arr.length$, it will be added and slots between will be empty, and these empty slots called `holes`
- **`splice(start,deleteCount, ...items):Array**
  - Returns empty array, in case of insertion
  - Returns non-empty array, in case of deletion
- **`push(..items):length`**
- **`unshift(...items):length`**
- **`pop():popedItem`**
- **`shift():popedItem`**
- **`filter(predicate):Array`**
- **`forEach(cb):void`**
  - `cb(value, index, arr):void`
- **`sort(comp):void`**
  - `comp(a,b):negative|0|positive`
- **`reverse():void`**
- **`some(predicate):boolean`**
- **`every(predicate):boolean`**
- **`reduce(cb, initialValue):singleValue`** , **`reduceRight(cb, initialValue):singleValue`**
  - `cb(prevValue, currValue, currIndex, arr)`
- **`map(cb):Array`**
  - `cb(value, index, arr)`
- **`toString():string`**
- **`join(separator):string`**
- **`slice(start?,end?):string|undefined`**
  - `end` will be excluded
  - `start`, `end` accept negative values

### Terms & Definitions

- **predicate**: function accepts one ore more parameter and return boolean
- **callback**: function passed to another function as a parameter
- **anonymous function**: function without name

_**[Back to the Index](../../README.md#index)**_
