# 🔖 ITI - D0053 - Angular

## Decorators

- Decorators are used to add extra meta-data to the classes, properties, and methods.
- Decorators are prefixed with `@` symbol.

### Types of Decorators

1. Class Decorators
2. Property Decorators
3. Method Decorators

### Class Decorators

- Class decorators are used to add extra meta-data to the classes.
- Example:
  - `@Component()`
  - `@Directive()`
  - `@Pipe()`

### Method Decorators

- Method decorators are used to add extra meta-data to the methods.
- Example:
  - `@HostListener()`
  - `@Input()`
  - `@Output()`

## Directives in Angular

- Directives are used to add behavior to the HTML elements.

### Types of Directives

1. Structural Directives
2. Attribute Directives

### Structural Directives

- Structural directives are used to add or remove the elements from the DOM.
- Example:
  - `*ngIf` (new syntax `@if`)
  - `*ngFor` (new syntax `@for`)
  - `*ngSwitch` (new syntax `@switch`)

### Attribute Directives

- Attribute directives are used to add or remove the attributes of the HTML elements.
- Example:
  - `ngStyle` (new syntax `@attr`)
  - `ngClass` (new syntax `@ngStyle`)

```html
<div [ngStyle]="{'color': 'red', 'font-size': '20px'}">Hello World</div>

<div [ngClass]="{'text-success': true, 'text-danger': false}">Hello World</div>

<!-- Style based on condition | `show` is a boolean property in the component -->
<div [ngStyle]="{'background-color': show ? 'red' : 'green'}">Hello World</div>
```

### Custom Directives

- Custom directives (User defined directives) are used to add custom behavior to the HTML elements.
- Create custom directive using cli command `ng generate directive directive-name` or `ng g d directive-name`.

**Example:**

```typescript
import { Directive, ElementRef } from "@angular/core";

@Directive({
  selector: "[appHighlight]",
})
export class LightBoxDirective {
  @HostListener("mouseover")
  onMouseOver() {
    this.el.nativeElement.style.border = "3px solid green";
  }

  @HostListener("mouseout")
  onMouseOut() {
    this.el.nativeElement.style.border = "3px solid red";
  }

  constructor(private el: ElementRef) {
    el.nativeElement.style.border = "3px solid red";
  }
}
```

```html
<div appHighlight>Highlight Me</div>
```

**Key Points:**

- Don't forget to import and add the directive to the `imports` array in the component module.
- `ElementRef` is used to access the DOM element.
- `@Directive` decorator is used to define the directive.
- `@HostListener` decorator is used to listen to the events.

## Pipes in Angular

- Pipes are used to transform the data before displaying it in the view.
- Pipes are prefixed with `|` symbol.

### Types of Pipes

1. Built-in Pipes
2. Custom Pipes

### Built-in Pipes

- Angular provides some built-in pipes.
- Example:
  - `uppercase`
  - `lowercase`
  - `currency`
  - `date`
  - `json`

```html
<h1>{{ 'hello world' | uppercase }}</h1>
<h1>{{ 'HELLO WORLD' | lowercase }}</h1>
<h1>{{ 100 | currency }}</h1>
<h1>{{ today | date: 'short' }}</h1>
<h1>{{ { name: 'John' } | json }}</h1>
```

### Custom Pipes

- Custom pipes (User defined pipes) are used to transform the data.
- Create custom pipe using cli command `ng generate pipe pipe-name` or `ng g p pipe-name`.

**Example:**

```typescript
import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "reverse",
})
export class ReversePipe implements PipeTransform {
  transform(value: string): string {
    return value.split("").reverse().join("");
  }
}
```

```html
<h1>{{ 'hello world' | reverse }}</h1>
```

**Send Parameters to Custom Pipe:**

```typescript
import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "concat",
})
export class ConcatPipe implements PipeTransform {
  transform(value: string, append: string): string {
    return value + append;
  }
}
```

```html
<h1>{{ 'hello' | concat: ' world' }}</h1>
```

## Input Reference in Angular

- Input reference is used to get the reference of the input element.
- Use `#` symbol to create the reference.

**Example:**

```html
<input type="text" #nameInput />
<button (click)="nameInput.value = ''">Clear</button>
```

## Two-way Data Binding in Angular

- **Two-way Data Binding**: `[(ngModel)]="data"`
- `[()]` is called the **banana in a box**.
- Import `FormsModule` in the module to use `ngModel`.

**Example:**

`demo.component.html`

```html
<select [(ngModel)]="name">
  <option value="John">John</option>
  <option value="Doe">Doe</option>
</select>
```

`demo.component.ts`

```typescript
export class DemoComponent {
  name = "";
}
```

## Dynamic Components in Angular

- Dynamic components are used to create components dynamically.
- Use Property Decorator `@Input()` to pass the data from the parent component to the dynamic component (child).
- Use Property Decorator `@Output()` to emit the data from the dynamic component (child) to the parent component.

**Example: Send data from Parent to Child `@Input()`:**

```ts
// parent.component.ts
import { Component } from "@angular/core";

@Component({
  selector: "app-parent",
  template: `
    <h2>Parent Component</h2>
    <app-child [childMessage]="messageFromParent"></app-child>
  `,
})
export class ParentComponent {
  messageFromParent = "Hello, Child!";
}
```

```ts
// child.component.ts
import { Component, Input } from "@angular/core";

@Component({
  selector: "app-child",
  template: `
    <h3>Child Component</h3>
    <p>Message from Parent: {{ childMessage }}</p>
  `,
})
export class ChildComponent {
  @Input() childMessage!: string;
}
```

**Example: Send data from Child to Parent `@Output()`:**

```ts
import { Component } from "@angular/core";

@Component({
  selector: "app-parent",
  template: `
    <h2>Parent Component</h2>
    <app-child (messageEvent)="receiveMessage($event)"></app-child>
    <p>Message from Child: {{ messageFromChild }}</p>
  `,
})
export class ParentComponent {
  messageFromChild = "";

  receiveMessage(message: string) {
    this.messageFromChild = message;
  }
}
```

```ts
// child.component.ts
import { Component, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-child",
  template: `
    <h3>Child Component</h3>
    <button (click)="sendMessage()">Send Message to Parent</button>
  `,
})
export class ChildComponent {
  @Output() messageEvent = new EventEmitter<string>();

  sendMessage() {
    this.messageEvent.emit("Hello Parent!");
  }
}
```

[← Prev](./iti-d0051-angular.md) | [🏠 Index](../../README.md#index) | Next →
