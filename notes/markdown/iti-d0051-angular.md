# 🔖 ITI - D0051 - Angular

## Server-side Rendering (SSR) vs. Client-side Rendering (CSR)

- **Server-side Rendering (SSR)**
  - The server generates the full HTML page and sends it to the client.
  - The client receives the full HTML page and displays it (refreshes the page).
- **Client-side Rendering (CSR)**
  - The server sends a minimal HTML page and the client generates the full HTML page.
  - Changes to the page are done without refreshing the page (changing the DOM).

## Single Page Application (SPA)

- A web application consisting of a single HTML page that is dynamically updated as the user interacts with the app.
- Updating page by changing the DOM.

## JS Bundlers

- A tool that puts your code and all its dependencies together in one file, including CSS, images, etc.
- It bundles all the dependencies together in one file with minification and uglification.
- It helps to reduce the number of requests to the server.
- Examples:
  - WebPack (Bundler - bundle all the dependencies together) `Used in Angular`
  - Parcel (Bundler - bundle all the dependencies together)
  - BabelJS (Transpiler - convert from ES6 to ES5) `Used in Angular`

## What is Angular?

- A platform and framework for building client applications in HTML and TypeScript.
- Angular is written in TypeScript.
- Angular is a complete rewrite from the same team that built AngularJS.

## Installation

1. Install node.js.

2. Install typescript.

   ```bash
   npm i -g typescript
   ```

3. Install Angular CLI.

   ```bash
   npm i -g @angular/cli
   ```

4. Create new Project using Angular CLI `ng`.

   ```bash
   ng new new-app
   ```

5. Install Packages.

   ```bash
   npm i
   ```

6. Start App.

   ```bash
   ng [serve | s] -o

   # or
   npm start
   ```

## Difference between `package.json` and `package-lock.json`

- **`package.json`**
  - Contains the metadata of the project.
  - Contains the list of dependencies.
  - Contains the list of scripts.
- **`package-lock.json`**
  - Contains the exact version of the dependencies that are installed and it's (sub)dependencies.

## Angular Structure

- Angular is a **component-based framework**.
- Each component has its own HTML, CSS, and TypeScript file.
  - `app.component.html`: HTML file for the component.
  - `app.component.css`: CSS file for the component.
  - `app.component.ts`: TS file contains class which have properties and methods for the component.

### Standalone Components

- Components that work **independently** (not part of a module).
- Introduced as the main approach in **Angular 16**.
- Simplifies development by removing the need for `NgModule`.

## Angular CLI Common Commands

- **Create new Angular Project**

  ```bash
  ng new [project-name]
  ```

- **Create new Angular Component**

  ```bash
  ng [generate | g] [component | c] [component-name]
  ```

- **Create new Model**

  ```bash
  # model as a class
  ng [generate | g] class [model-name]

  # model as interface
  ng [generate | g] [interface | i] [model-name]
  ```

- **Generate new Directive**

  ```bash
  ng [generate | g] [directive | d] [directive-name]
  ```

- **Generate new Pipe**

  ```bash
  ng [generate | g] [pipe | p] [pipe-name]
  ```

- **Generate new Service**

  ```bash
  ng [generate | g] [service | s] [service-name]
  ```

- **Start Angular App**

  ```bash
  ng [serve | s] -o
  ```

## Use Bootstrap in Angular

1. Install Bootstrap.

   ```bash
   npm i bootstrap
   ```

2. Add Bootstrap CSS in `angular.json`.

   ```json
   "styles": [
     "src/styles.css",
     "node_modules/bootstrap/dist/css/bootstrap.min.css"
   ]
   ```

3. Use Bootstrap classes in the HTML file.

## Enhance Angular Project File Structure

- **components**: Contains all the components.
- **models**: Contains all the models (classes/interfaces).

```text
src
├── app
│   ├── components
│   │   ├── component1
│   │   │   ├── component1.component.html
│   │   │   ├── component1.component.css
│   │   │   ├── component1.component.ts
│   │   │   ├── component1.component.spec.ts
│   ├── models
│   │   ├── model1.ts
```

## Data Binding in Angular

### One-way Data Binding

- **Interpolation**: `{{ data }}`
- **Property Binding**: `[property]="data"`
- **Event Binding**: `(event)="expression"`
- **Attribute Binding**: `[attr.attributeName]="data"`

**Example:**

`demo.component.html`

```html
<!-- Interpolation -->
<h1>{{ title }}</h1>

<!-- Property Binding -->
<img [src]="image" alt="Image" />

<!-- Event Binding -->
<button (click)="onClick()">Click Me</button>

<!-- Attribute Binding -->
<a [attr.href]="url">Link</a>
```

`demo.component.ts`

```typescript
export class DemoComponent {
  title = "Hello World";
  image = "https://example.com/image.jpg";
  url = "https://example.com";

  onClick() {
    console.log("Button Clicked");
  }
}
```

### Two-way Data Binding

- **Two-way Data Binding**: `[(ngModel)]="data"`
- `[()]` is called the **banana in a box**.

**Example:**

`demo.component.html`

```html
<input type="text" [(ngModel)]="name" />
```

`demo.component.ts`

```typescript
export class DemoComponent {
  name = "";
}
```

## Control Flow Blocks in Angular

- **`@if(condition){...} @else{...}`** - Replaces `*ngIf`.
- **`@for(let item of items; track item.id){...}`** - Replaces `*ngFor`.
- **`@switch(condition){ @case condition1: ... @case condition2: ... @default: ... }`** - Replaces `*ngSwitch`.

**Example:**

```html
<!-- @if @else -->
@if(show){
<h1>Hello</h1>
} @else {
<h1>World</h1>
}

<!-- @for -->
@for(let item of items; track item.id; let index = $index){
<li>{{ item.name }}</li>
}

<!-- @switch -->
@switch(condition) { @case 1:
<h1>One</h1>
@case 2:
<h1>Two</h1>
@default:
<h1>Default</h1>
}
```

**🌟 `track` & `$index` with `@for**:

- `track` is used to avoid re-rendering.
- `track` must have a unique value (like `item.id`).
- `$index` is used to get the index of the item.

**🌟 Toggling Example**:

```typescript
import { Component } from "@angular/core";

@Component({
  selector: "app-toggle",
  standalone: true,
  template: `
    <button (click)="toggle()">Toggle</button>

    @if(show) {
    <h1>Hello</h1>
    } @else {
    <h1>World</h1>
    }
  `,
  styles: ["h1 { color: blue; }"],
})
export class ToggleComponent {
  show = true;

  toggle() {
    this.show = !this.show;
  }
}
```

[← Prev](./iti-d0051-ts.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0053-angular.md)
