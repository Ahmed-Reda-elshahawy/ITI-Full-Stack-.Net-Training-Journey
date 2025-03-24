# 🔖 ITI - D0054 - Angular

## Routing in Angular

- **Routing** is a mechanism to navigate between different components.
- We can use the Angular Router to apply routing in Angular applications.

### Routing Setup

Create `app.routes.ts` file (if not exists) to define the routes and in this module we will do the following:

1. Import the `Routes` from `@angular/router`.
2. **Import Components** that we want to navigate to.
3. **Define & Export `routes`**: as an array of objects with the path and component.

   - `path`: URL path to navigate to the component.
   - `component`: Component to navigate to.

4. **Redirect routes**: Using `redirectTo`.

   - `pathMatch: "full"`: Redirects to the full path.
   - `pathMatch: "prefix"`: Redirects to the prefix path.

5. **Handle Unknown Routes** with a wildcard (`**`) to show error page.

```typescript
// app.routes.ts
import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { ErrorComponent } from "./components/error/error.component";
import { ContactComponent } from "./components/contact/contact.component";

export const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: "full" },
  { path: "home", component: HomeComponent },
  { path: "contact", component: ContactComponent },
  { path: "**", component: ErrorComponent },
];
```

**🌟 `routerLink` instead of `href`**

- We can use the `href` attribute with the `<a>` tag to navigate to a different page.
- We will use the `routerLink` directive to navigate between components in Angular to avoid reloading the page.
- To use `routerLink`, we need to import the `RouterModule` in the component module.

  ```html
  <!-- contact.component.html -->
  <p>contact works!</p>
  <a routerLink="/home">Back to Home!</a>
  ```

  ```typescript
  // contact.component.ts
  import { Component } from "@angular/core";
  import { RouterModule } from "@angular/router";

  @Component({
    selector: "app-contact",
    standalone: true,
    imports: [RouterModule],
    templateUrl: "./contact.component.html",
    styleUrl: "./contact.component.css",
  })
  export class ContactComponent {}
  ```

### Navigation from Component Class

- We can navigate to a different component from the component class using the `Router` service.
- Inject the `Router` service in the constructor.
- Use `navigateByUrl(path)` method to navigate to a different URL.

```typescript
// contact.component.ts
import { Component } from "@angular/core";
import { Router, RouterModule } from "@angular/router";

@Component({
  selector: "app-contact",
  standalone: true,
  imports: [RouterModule],
  templateUrl: "./contact.component.html",
  styleUrl: "./contact.component.css",
})
export class ContactComponent {
  constructor(private router: Router) {}

  back() {
    this.router.navigateByUrl("/home");
  }
}
```

### Dynamic Routing (Passing Parameters)

- Dynamic routing is used to pass parameters in the URL (e.g. `/products/:id` &rarr; `/products/1`).
- To use it we need to define path parameters in routes.

  ```typescript
  // app.routes.ts
  export const routes: Routes = [
    {path: 'products', component: ProductsComponent},
    {path: 'product/:id', component: ProductDetailComponent}
    {path: '**', component: ErrorComponent}
  ];
  ```

- Add a link with the parameter in the component template.

  ```html
  <!-- products.component.html -->
  <a [routerLink]="['/product', product.id]">{{product.name}}</a>
  ```

- Another way to navigate programmatically by using `navigateToUrl` method.

- Access parameters in component class, using `ActivatedRoute` service.

```typescript
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-product-details",
  imports: [],
  templateUrl: "./product-details.component.html",
  styleUrl: "./product-details.component.css",
})
export class ProductDetailsComponent implements OnInit {
  public productId!: number;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.productId = +(this.route.snapshot.paramMap.get("id") || 0);

    // another way
    // this.route.params.subscribe((params) => {
    //   this.productId = parseInt(params['id'] ?? 0);
    // });
  }
}
```

## Lifecycle Hooks

- **Lifecycle hooks** are methods which get called at specific stages of a component lifecycle.
- Hooks get called after the constructor.
- Each hook has a specific interface that the component class should implement.

### OnInit

- Runs after the constructor.
- Used to perform initialization tasks (which require some time to complete like fetching data from API).
- To use it we need to implement the `OnInit` interface.

  ```typescript
  // products.component.ts

  import { Component, OnInit } from "@angular/core";

  @Component({
    selector: "app-products",
    templateUrl: "./products.component.html",
    styleUrls: ["./products.component.css"],
  })
  export class ProductsComponent implements OnInit {
    constructor() {}

    ngOnInit(): void {
      // Initialization tasks (e.g. fetching product list from API)
    }
  }
  ```

### OnChanges

- Runs when the `input` properties (only) of the component change.
- To use it, we need to implement the `OnChanges` interface.

  ```typescript
  // product.component.ts

  import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";

  @Component({
    selector: "app-product",
    templateUrl: "./product.component.html",
    styleUrls: ["./product.component.css"],
  })
  export class ProductComponent implements OnChanges {
    @Input() product!: Product;

    constructor() {}

    ngOnChanges(changes: SimpleChanges): void {
      // Perform tasks when input properties change
    }
  }
  ```

## Services

- **Services** help share data and functionality across components.
- Used for fetching data, **sharing data**, and **business logic**.
- Generate new service using cli command `ng [generate | g] service [service-name]`.
- **Use** `@Injectable` decorator to make the service injectable (i.e. available for dependency injection).
- **Singleton by default** when provided in the root module (single object shared across the app).
- **Not used for state management**, use `ngrx` for state management.

  ```typescript
  // product.service.ts
  import { Injectable } from "@angular/core";

  @Injectable({
    providedIn: "root", // Makes it available app-wide
  })
  export class ProductService {
    constructor() {}

    getProducts() {
      // Fetch all products from API
    }

    getProduct(id: number) {
      // Fetch a product by ID
    }
  }
  ```

[← Prev](./iti-d0053-angular.md) | [🏠 Index](../../README.md#index) | Next →
