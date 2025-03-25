# 🔖 ITI - D0056 - Angular

## Component Lazy Loading In Angular

- In `routes` array, we can use load components directly by adding `path` and `component` properties.
- But, if we want to load components lazily, we can use `loadComponent` method.

  ```typescript
  const routes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent },
    {
      path: "products",
      loadComponent: () =>
        import("./products/products.component").then(
          (m) => m.ProductsComponent
        ),
    },
    { path: "**", component: ErrorComponent },
  ];
  ```

- In the above code, we are loading `ProductsModule` lazily when the user navigates to `/products` path.

## Data Mocking using JSON Server

- JSON Server is a simple Node.js package that allows us to create a REST API using a JSON file.
- We can use JSON Server to mock data for our Angular application.

### Setup JSON Server

1. Install JSON Server globally using npm.

   ```bash
   npm install -g json-server
   ```

2. Create a `data.json` file with some data.

   ```json
   {
     "products": [
       { "id": 1, "name": "Product 1", "price": 100 },
       { "id": 2, "name": "Product 2", "price": 200 }
     ]
   }
   ```

3. Add npm script to `package.json` file.

   ```json
   // Replace [json-file] with the path to the JSON file.
   // Replace [port-number] with the port number you want to use.
   "scripts": {
     "json-server": "json-server --watch [json-file] --port [port-number]"
   }
   ```

4. Run npm script.

   ```bash
   npm run json-server
   ```

## Observables in Angular

- Observables are a powerful way to manage asynchronous data in Angular.
- Reactive Extensions (RxJS) library provides Observables in Angular.
- We can use Observables to handle events, HTTP requests, and other asynchronous operations.

### Observables vs. Promises

- Observables are cancellable, while Promises are not.
- Observables can emit multiple values over time, while Promises can only emit a single value.
- Can subscribe and unsubscribe to Observables, while Promises cannot.

## Services Using HttpClient in Angular

_Review Services in Angular [here](./iti-d0054-angular.md#services)_.

### 1. Create Service class

- `HttpClient` helps to make HTTP requests in Angular.
- It works with **Observables** to handle responses.
- We need to **provide `HttpClient` globally** (Angular 19 feature)

  ```typescript
  import { HttpClient } from "@angular/common/http";
  import { Injectable } from "@angular/core";
  import { Observable } from "rxjs";

  @Injectable({
    providedIn: "root", // registers service globally
  })
  export class ProductService {
    private apiUrl = "http://localhost:3000/products";

    constructor(private http: HttpClient) {}

    getProducts(): Observable<Product[]> {
      return this.http.get<Product[]>(this.apiUrl);
    }
  }
  ```

- In the above code, Service is registered globally so there's no need to **manually add `providers: [ProductService]` in components**.

### 2. Provide `HttpClient` globally in `main.ts`

- With this way, we didn't need to import `HttpClientModule` in each component anymore!
- Because this registers `HttpClients` once for the entire app.

  ```typescript
  import { bootstrapApplication } from "@angular/platform-browser";
  import { appConfig } from "./app/app.config";
  import { AppComponent } from "./app/app.component";
  import { provideHttpClient, withFetch } from "@angular/common/http";

  bootstrapApplication(AppComponent, {
    ...appConfig,
    providers: [...(appConfig.providers || []), provideHttpClient(withFetch())],
  });
  ```

### 3. Use the Service in a Component

```typescript
import { Component, OnInit } from "@angular/core";
import { ProductService } from "../../services/product.service";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.css"],
})
export class ProductsComponent implements OnInit {
  products!: any[];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });
  }
}
```

## Life Cycle Hooks in Angular

_Review Life Cycle Hooks [here](./iti-d0054-angular.md#lifecycle-hooks)_.

### ngDoCheck

- Runs every time this component is checked for change.
- Used for **custom change detection logic** (e.g. detecting changes in an objects not tracked by angular).

```typescript
@Component({
  selector: "app-cart",
  template: `<p>Cart Items: {{ cart.length }}</p>`,
})
export class CartComponent implements DoCheck {
  cart = ["Apple", "Banana"];

  addItem() {
    this.cart.push("Orange"); // UI won't update because array reference is the same
  }

  ngDoCheck() {
    console.log("ngDoCheck: Manually detecting changes");
    this.cart = [...this.cart]; // Creating a new array reference so Angular detects it
  }
}
```

### ngOnDestroy

- Runs once before the component is destroyed.
- Used for clean-up tasks like:
  - **Unsubscribing** from observables.
  - **Closing Websockets**.
  - **Clearing timers and intervals**.

[← Prev](./iti-d0055-angular.md) | [🏠 Index](../../README.md#index) | Next →
