# 🔖 ITI - D0050 - TypeScript

## Introduction

- TypeScript is a JS with static typing.
- TS transpile to JS using `tsc` compiler.
- TS used Annotations to declare the type of the variable.
- TS supports some extra OOP features that doesn't supported in JS like Interfaces, abstract classes, etc.
- TS supports Generics.

## Installation

- Install Node.js
- Install TypeScript

  ```bash
  npm install -g typescript
  ```

- Check the version

  ```bash
  tsc -v
  ```

### Compile

- Create a file with `.ts` extension
- Compile the file

  ```bash
  tsc filename.ts
  ```

- It will create a `.js` file

### 🌟 Compile using Watch (Dev Environment)

It will watch the file and compile it automatically

```bash
tsc filename.ts --watch --outDir dist --target es6
```

## Type Annotations

- `let` and `const` are used to declare variables.
- use `:type` to declare the type of the variable.

  ```ts
  let name: string = "John";
  ```

- `?` is used to make the variable optional.

  ```ts
  let age?: number;
  ```

- Permeative Types Annotations (`number`, `string`, `boolean`, `null`, `undefined`, `void`, `symbol`, `BigInt`)

  ```ts
  let isTrue: boolean = true;
  let age: number = 30;
  let name: string = "John";
  ```

- Array Types Annotations

  ```ts
  let names: string[] = ["John", "Doe"];
  let items: []; // empty array, can't add any item to it.
  ```

- Union Types Annotations

  ```ts
  let mixed: (string | number)[] = ["John", 30];
  let uid: string | number;
  ```

- Tuple Types Annotations

  ```ts
  let user: [string, number] = ["John", 30];
  ```

- Type `any` Annotations

  ```ts
  let age: any = 30;
  ```

- Type `void` Annotations

  ```ts
  let greet = (): void => {
    console.log("Hello");
  };
  ```

- Type `never` Annotations

  ```ts
  let throwError = (message: string): never => {
    throw new Error(message);
  };
  ```

- Type `object` Annotations

  ```ts
  let user: object = {
    name: "John",
    age: 30,
  };
  ```

- Type `null` and `undefined` Annotations

  ```ts
  let age: null = null;
  let name: undefined = undefined;
  ```

- Object Types Annotations

  ```ts
  let user: {
    name: string;
    age: number;
  } = {
    name: "John",
    age: 30,
  };
  ```

- Interface Types Annotations

  ```ts
  interface User {
    name: string;
    age: number;
  }

  let user: User = {
    name: "John",
    age: 30,
  };
  ```

- Function Types Annotations

  ```ts
  let add = (a: number, b: number, c?: number | string): number => {
    return a + b;
  };

  // optional parameter
  let add = (a: number, b: number, c?: number): number => {
    return a + b + (c ?? 0);
  };
  ```

- Generics Types Annotations

  ```ts
  // Generic Array
  let names: Array<string>;

  // Generic Function
  let addId = <T>(id: T) => {
    return id;
  };

  let numId = addId<number>(10); // or let numId = addId(10);
  let strId = addId<string>("a234"); // or let strId = addId('a234');
  ```

- Type Assertion

  ```ts
  let name: any = "John";
  let nameLength = (name as string).length;
  ```

- `unknown` Type

```ts
let data: unknown = "john";
let strLength = (data as string).length;
```

### Common Operators

- Definite Assignment Assertions (`!`)

  ```ts
  let userName!: string;
  console.log(userName); // no error
  userName = "John";
  console.log(userName); // John
  ```

- Nullish Coalescing Operator (`??`)

  ```ts
  let age: number | null = null;
  let userAge = age ?? 30;
  console.log(userAge); // 30
  ```

- Optional Chaining Operator (`?.`)

  ```ts
  let user = {
    name: "John",
    age: 30,
  };

  console.log(user?.name); // John
  console.log(user?.address?.city); // undefined
  ```

- Logical Operators `||`, `&&`

  - falsy values: `false`, `0`, `""`, `null`, `undefined`, `NaN`

  ```ts
  let name: string = "";
  let userName = name || "John";
  console.log(userName); // John

  let age: number = 0;
  let userAge = age && 30;
  console.log(userAge); // 0
  ```

## OOP With TS

### Classes

- Create a class

  ```ts
  class User {
    name: string;
    age: number;

    constructor(name: string, age: number) {
      this.name = name;
      this.age = age;
    }

    greet() {
      return `Hello, ${this.name}`;
    }
  }

  let user = new User("John", 30);
  console.log(user.greet());
  ```

- Access Modifiers (`public`, `private`, `protected`)

  - `public` is the default access modifier, can be accessed anywhere.
  - `private` can't be accessed outside the class.
  - `protected` can be accessed in the class and its subclasses.

  ```ts
  class User {
    private name: string;
    protected age: number;

    constructor(name: string, age: number) {
      this.name = name;
      this.age = age;
    }

    greet() {
      return `Hello, ${this.name}`;
    }
  }
  ```

- Define Properties within the Constructor

  ```ts
  class User {
    constructor(public name: string, public age: number) {}

    greet() {
      return `Hello, ${this.name}`;
    }
  }
  ```

### Inheritance

- Create a Baseclass

  ```ts
  class User {
    name: string;
    age: number;

    constructor(name: string, age: number) {
      this.name = name;
      this.age = age;
    }

    greet() {
      return `Hello, ${this.name}`;
    }
  }
  ```

- Create a Subclass

  ```ts
  class Admin extends User {
    role: string;

    constructor(name: string, age: number, role: string) {
      super(name, age);
      this.role = role;
    }

    greet() {
      return `Hello, ${this.name} as ${this.role}`;
    }
  }

  let admin = new Admin("John", 30, "Admin");
  console.log(admin.greet());
  ```

- TS like JS doesn't support multiple inheritance.
- TS supports multi-level inheritance.
- TS supports multiple interfaces implementation.

### Polymorphism

#### Overloading

Overloading is a feature that allows us to define multiple methods with the same name but different parameters and return types.

```ts
class User {
  name: string;
  age: number;

  constructor(name: string, age: number) {
    this.name = name;
    this.age = age;
  }

  // define overloaded method signature with different parameters and return types with the same name and single implementation
  greet(): string;
  greet(message: string): string;
  greet(message?: string): string {
    return message ? `${message}, ${this.name}` : `Hello, ${this.name}`;
  }
}

let user = new User("John", 30);
console.log(user.greet());
console.log(user.greet("Hi"));
```

#### Overriding

Override is a feature that allows a subclass to provide a specific implementation of a method that is already provided by one of its superclasses.

```ts
class User {
  name: string;
  age: number;

  constructor(name: string, age: number) {
    this.name = name;
    this.age = age;
  }

  greet() {
    return `Hello, ${this.name}`;
  }
}

class Admin extends User {
  role: string;

  constructor(name: string, age: number, role: string) {
    super(name, age);
    this.role = role;
  }

  greet() {
    return `Hello, ${this.name} as ${this.role}`;
  }
}
```

### Interfaces

- Create an Interface

  ```ts
  interface User {
    name: string;
    age: number;
  }

  let user: User = {
    name: "John",
    age: 30,
  };
  ```

- Implement an Interface

  ```ts
  interface Service {
    greet(): string;
  }

  class UserService implements Service {
    greet() {
      return "Hello";
    }
  }
  ```

- TS interfaces doesn't support default method implementation.
- **Interface Segregation Principle (ISP)**: A client should never be forced to implement an interface that it doesn't use.

### Abstract Classes

- Create an Abstract Class

  ```ts
  abstract class Service {
    abstract greet(): string;
  }

  class UserService extends Service {
    greet() {
      return "Hello";
    }
  }
  ```

### Generics

- Create a Generic Class

  ```ts
  class User<T> {
    name: T;
    age: T;

    constructor(name: T, age: T) {
      this.name = name;
      this.age = age;
    }
  }

  let user = new User<string>("John", "30");
  ```

- Create a Generic Function

  ```ts
  let addId = <T>(id: T): T => {
    return id;
  };
  ```

[← Prev](./iti-d0049-asp-api.md) | [🏠 Index](../../README.md#index) | Next →
