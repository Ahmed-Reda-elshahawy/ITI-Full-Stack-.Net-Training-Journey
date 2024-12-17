# 🔖 ITI - D0010 - CST (JavaScript 6, ECMA6, ECMA.NEXT) (Part 2)

## Classes

- Used to create **user defined types**
- Create classes using **`class`** keyword
- JS classes can have **just one Constructor**
- JS classes **can't** have a **Destructor**
- Can't call class as a function
- Use **`new`** keyword to **create** an object from a class
- Can't create an object from class before declaration (not Hoisted)
- Can define class as **class statement** or **class expression**
- JS supports method **overriding** without **`override`** key word
- JS **doesn't** support method **overloading**

### Fields

- **public**
  - Accessible inside class and outside class using its own object
  - Declare it inside the constructor
- **private**
  - Accessible only inside class
  - Define it inside class direct using `#`

```js
class User {
  id;
  name;
  #age;
  constructor(id, name, age) {
    this.id = id;
    this.name = name;
    this.#age = age;
  }
}

const user = new User(10, "John", 20);
console.log(user.id); // 10
console.log(user.name); // 'John'
console.log(user.#age); // error [cannot access private field]
```

### Properties

- Special type of methods
- Defined as a function, but consumed as fields
- Gives the advantages of **protection** and **accessibility**
- Use keywords
  - **set**
  - **get**

```js
class User {
  #id;
  constructor(id, name) {
    this.ID = id;
    this.name = name;
  }

  set ID(id) {
    // set property
    if (id >= 0) {
      this.#id = id;
    }
  }

  get ID() {
    // get property
    return this.#id;
  }
}

const user = new User(10, "John");
console.log(user.ID); // 10 [note: here property used as a field]
user.ID = 20;
console.log(user.ID); // 20
console.log(user.name); // 'John'
```

> [!Note]
>
> **Check Assigned values to class fields**
>
> - It's a bad practice to check or validate class fields inside the constructor
> - Make fields private and use `set` property to set and make your own custom validation on values, and use `get` property to access it

### Methods

- **Instance Methods** (Object Methods)
  - Will be added to the Object **prototype**
- **Type Methods** (Class Methods, Static Methods) using `static` keyword

```js
class User {
  constructor(id, name) {
    this.id = id;
    this.name = name;
  }

  printInfo() {
    // instance method , object method
    console.log(`User: ${this.name}, ID: ${this.id}`);
  }

  static printHello() {
    // type method, class method
    console.log("Hello, user!");
  }
}
```

> [!Note]
> In the example above, We used [**Concise Function**](./iti-d0009-cst-javascript-ecma6-part1.md#concise-function)

## OOP

### Inheritance

- **`is a`** relationship
- JS supports **multi-level inheritance**
- JS **doesn't** support **multiple inheritance** [^1]
- Apply inheritance in JS using **`extends`** keyword, Internally uses the **prototypal inheritance** [^2]

```js
class User {
  constructor(id, name) {
    this.id = id;
    this.name = name;
  }

  printInfo() {
    // instance method , object method
    console.log(`User: ${this.name}, ID: ${this.id}`);
  }

  sayHello() {
    console.log(`Hello, ${this.name}`);
  }

  static printHello() {
    // type method, class method
    console.log("Hello, user!");
  }
}

class Student extends User {
  constructor(id, name, grade) {
    super(id, name);
    this.grade = grade;
  }

  printInfo() {
    console.log(`Student: ${this.name}, ID: ${this.id}, Grade: ${this.grade}`);
  }
}

const student = new Student(20, "Jane", 10);
console.log(student.id); // 20
console.log(student.name); // 'Jane'
console.log(student.grade); // 10
console.log(student instanceof User); // true
student.sayHello(); // 'Hello, John'
student.printInfo(); // 'Student: John, ID: 20, Grade: 10'
```

### Abstraction

- Doesn't `abstract` keyword in JS
- Can use `new.target` to explicitly prevent creating object from this class

```js
class Shape {
  // abstract class cannot be instantiated
  constructor() {
    if (new.target.name === "Shape") {
      throw new Error("Can't create an object from Shape class");
    }
  }

  getArea() {
    return undefined;
  }
}

class Circle extends Shape {
  constructor(radius) {
    super();
    this.radius = radius;
  }

  getArea() {
    return Math.PI * Math.pow(this.radius, 2);
  }
}

// const shape = new Shape(); // error
const circle = new Circle(10);
console.log(circle.getArea()); // 314.1592653589793
```

> [!Note]
>
> **`new.target.name`** meta-property lets you detect whether a function or constructor was called using the new operator

## JS Modules

- Allow you to break your code into separated files
- Avoid **naming collision**
- Dependencies and Relationships
- Avoid **namespace pollution**
- By default all variables inside module are **private**
- Use **`export`** keyword, to export variable outside module
  - each module, can have multiple **named export** but only single **default export**
  - Can re-export imported variables, functions, classes,...
  - export everything inside module using `export * from 'module-name'`
- Use **`import`** keyword, to import other modules
  - Can import with aliases
  - Namespace import with `import * from 'module-name'`
  - All imported members are read-only (i.e. protected by the imported module) but we can change it through function inside the imported module
- **`export`** and **`import`** should be used only at the **Module scope** (i.e. can't be used inside function scope or block scope) just module scope

```js
// module1.js
export var names = ["John", "Omar"];
```

```js
// module2.js
import { names } from "./module2.js";
// import {names as namesArr} from './module2.js' // import with aliases to avoid naming collision
console.log(names);
```

> [!Note]
>
> **JS Modules with HTML**
> In this case we should define `type` attribute as a `module`, in `<script></script>` tag

```html
<script src="./module2.js" type="module"></script>
```

### IIFE (Immediately Invoked Function Expression)

```js
(function () {
  console.log("Hello world");
})();
```

> [!Note]
>
> **IIFE** is commonly used in 3rd part libraries like jQuery

### External JS files loading Strategies

**default (traditional)**:

![default loading](./imgs/pasted_image_20241216224049.png)

**async**:

- Don't rely on it
- Will work with small HTML files, as it can be fully interpreted, before the JS script fully downloaded

![async loading](./imgs/pasted_image_20241216224115.png)

**defer**:

- JS modules, by default it `defer` executed

![defer loading](./imgs/pasted_image_20241216224129.png)

> [!Note]
>
> **async & defer**
>
> `async` & `defer` only for external `src` scripts (because it needs to be downloaded).

[^1]: C++ supports multiple inheritance, but multiple inheritance have **diamond problem**, which happens when a child class inherits from multiple classes and there are some parent classes shares the same base class.
[^2]: Prototypal Inheritance is the linking of prototypes of a parent object to a child object to share and utilize the properties of a parent class using a child class.

_**[Back to the Index](../../README.md#index)**_
