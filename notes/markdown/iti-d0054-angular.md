# 🔖 ITI - D0054 - Angular (Draft)

## Routing in Angular

- **Routing** is a mechanism to navigate between different components.
- We can use the Angular Router to apply routing in Angular applications.

### Routing Setup

Create `app.routes.ts` file to define the routes and in this module we will do the following:

- Import `Routes` from `@angular/router`.
- Import the components that we want to navigate.
- export a constant array of routes, each route is an object with the following properties:
  - `path`: the URL path.
  - `component`: the component to navigate to.
- Error route: if the user navigates to a route that does not exist, we can redirect them to a default route.

  ```typescript
  // app.routes.ts
  import { Routes } from "@angular/router";
  import { HomeComponent } from "./home/home.component";
  import { AboutComponent } from "./about/about.component";
  import { ContactComponent } from "./contact/contact.component";
  import { ErrorComponent } from "./error/error.component";

  export const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "about", component: AboutComponent },
    { path: "contact", component: ContactComponent },
    { path: "**", redirectTo: ErrorComponent },
  ];
  ```

**🌟 `routerLink` instead of `href`**

- We can use the `href` attribute with the `<a>` tag to navigate to a different page.
- We will use the `routerLink` directive to navigate between components in Angular to avoid reloading the page.

```html
<!-- Note: Must import  RouterModule in component module -->

<!-- app.component.html -->
<nav>
  <a routerLink="/">Home</a>
  <a routerLink="/about">About</a>
  <a routerLink="/contact">Contact</a>
</nav>
```

### Navigation from Component Class

- We can navigate to a different component from the component class using the `Router` service.
- Inject the `Router` service in the constructor.
- Use the `navigate()` method to navigate to a different component.
- Also we can use the `navigateByUrl()` method to navigate to a different URL.

```typescript
// home.component.ts
import { Router, RouterModule } from "@angular/router";
import { Component } from "@angular/core";

@Component({
  selector: "app-home",
  imports: [RouterModule],
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent {
  constructor(private router: Router) {}

  navigateToAbout() {
    this.router.navigate(["/about"]);
  }
}
```

### Dynamic Routing

- We can use dynamic routing to navigate to a different component based on the user's input.
- Use the `routerLink` directive with the `[]` to bind the path dynamically.

**Full Example:**

```typescript
// app.routes.ts
import { Routes } from "@angular/router";
import { provideRouter } from "@angular/router";
import { ProductComponent } from "./product/product.component";
import { ProductDetailsComponent } from "./product-details/product-details.component";

export const routes: Routes = [
  { path: "product/:id", component: ProductDetailsComponent }, // Dynamic Route
  { path: "", component: ProductComponent }, // Default Page
];

export const appRoutingProviders = [provideRouter(routes)];
```

```typescript
// product.component.ts
import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";

@Component({
  selector: "app-product",
  standalone: true,
  imports: [CommonModule, RouterModule], // No need for NgModule
  template: `
    <h2>Products</h2>
    <ul>
      <li *ngFor="let product of products">
        <a [routerLink]="['/product', product.id]">{{ product.name }}</a>
      </li>
    </ul>
  `,
})
export class ProductComponent {
  products = [
    { id: 1, name: "Product A" },
    { id: 2, name: "Product B" },
  ];
}
```

```typescript
// product-details.component.ts
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

@Component({
  selector: "app-product-details",
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <h2>Product Details</h2>
    <p>Product ID: {{ productId }}</p>
    <a routerLink="/">Go Back</a>
  `,
})
export class ProductDetailsComponent implements OnInit {
  productId: number | null = null;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.productId = Number(params.get("id")); // Get the dynamic ID from URL
    });
  }
}
```

```typescript
// app.component.ts
import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [RouterOutlet],
  template: `
    <h1>Angular 19 Dynamic Routing Example</h1>
    <router-outlet></router-outlet>
  `,
})
export class AppComponent {}
```
