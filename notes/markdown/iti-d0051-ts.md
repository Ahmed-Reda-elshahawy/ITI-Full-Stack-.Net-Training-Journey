# 🔖 ITI - D0051 - TypeScript

## Interface vs. Type

### Interface

- **Interfaces** are used to define the structure of an object.
- Multiple Interfaces with the same name are merged into one.

```typescript
interface Person {
  id: number;
  name: string;
}

interface Person {
  age: number;
}

const person: Person = {
  id: 1,
  name: "John",
  age: 30,
};
```

### Type

- **Type** is used to define a type.
- Multiple Types with the same name are not merged and will throw an error.

```typescript
type Person = {
  id: number;
  name: string;
};

// Error: Duplicate identifier 'Person'.
// type Person = {
//   age: number;
// };

const person: Person = {
  id: 1,
  name: "John",
};

// another example
type Role = "Admin" | "User";

const role: Role = "Admin";
```

## Union (`|`) vs. Intersection (`&`)

- **Union(`|`)** - Either one type or more.
- **Intersection(`&`)** - Combine multiple types.

```typescript
// union
type StringOrNumber = string | number;
let value: StringOrNumber = "Hello";
value = 10;
// value = false;  // Error (not string or number)

// intersection
type Person = {
  id: number;
  name: string;
};

const person: Person & { age: number } = {
  id: 1,
  name: "John Doe",
  age: 20,
};
```

## Type Utility

### Record

**Record** is used to create an object type whose property keys are keys of `K` and whose property values are of type `T`.

```typescript
type Role = "Admin" | "User";
type Account = {
  id: number;
  name: string;
};

type AccountRole = Record<Role, Account>;

const accountRole: AccountRole = {
  Admin: { id: 1, name: "John" },
  User: { id: 2, name: "Doe" },
};
// object here is { Admin: Account, User: Account }
```

### Pick

**Pick** is used to create a type by picking some properties from another type.

```typescript
type User = {
  id: number;
  name: string;
  email: string;
  password: string;
};

type UserDto = Pick<User, "id" | "name" | "email">;
```

### Omit

**Omit** is used to create type by omitting some properties from another type.

```typescript
type User = {
  id: number;
  name: string;
  email: string;
  password: string;
};

type UserDto = Omit<User, "password">;

const dto: UserDto = {
  id: 1,
  name: "John",
  email: "john@example.com",
};
```

### Partial

**Partial** is used to make all properties of a type optional.

```typescript
type User = {
  id: number;
  name: string;
  email: string;
  password: string;
};

type UserDto = Partial<User>;

const dto: UserDto = {
  id: 1,
  email: "john@example.com",
};
```

### Required

**Required** is used to make all properties of a type required (opposite of `Partial`).

```typescript
type User = {
  id?: number;
  name?: string;
  email?: string;
  password?: string;
};

type UserDto = Required<User>;

const dto: UserDto = {
  id: 1,
  name: "John",
  email: "john@example.com",
  password: "password",
};
```

### Readonly

**Readonly** is used to make all properties of a type read-only.

```typescript
type User = {
  id: number;
  name: string;
};

type ReadonlyUser = Readonly<User>;

const user: ReadonlyUser = {
  id: 1,
  name: "John",
};

// Error: Cannot assign to 'id' because it is a read-only property.
// user.id = 2;
```

#### How to achieve the same with `readonly` keyword?

- Seal the object with `as const`.

```typescript
const user = {
  id: 1,
  name: "John",
} as const;
```

- Use defined properties as `readonly`.

```typescript
const user = {
  id: 1,
  name: "John",
};

user.defineProperty(user, "id", { writable: false });
```

## Enums

- Enums allow us to define a set of named constants.

```typescript
enum Role {
  Admin,
  User,
  Guest,
}

const role: Role = Role.Admin;
```

- Enums in TypeScript can be numeric or string-based.
- Numeric enums are auto-incremented from 0.

```typescript
enum Role {
  Admin = 1,
  User,
  Guest,
}

const role: Role = Role.Admin; // 1
const role2: Role = Role.User; // 2
```

- String-based enums are not auto-incremented.

```typescript
enum Role {
  Admin = "ADMIN",
  User = "USER",
  Guest = "GUEST",
}

const role: Role = Role.Admin; // "ADMIN"
```

[← Prev](./iti-d0050-ts.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0051-angular.md)
