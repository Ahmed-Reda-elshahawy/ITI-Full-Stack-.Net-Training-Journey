# 🔖 ITI - D0056 - Angular (Draft)

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
        import("./products/products.component").then((m) => m.ProductsModule),
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
