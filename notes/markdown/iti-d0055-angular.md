# 🔖 ITI - D0055 - Angular (Draft)

## Forms in Angular

- Template-driven forms
- Reactive forms

### Template-driven forms

- Template-driven forms are built using directives in the template (HTML).
- These forms are rely **on two-way data binding** using `ngModel` directive.
- This form validation is done in the template (HTML) by using HTML5 validation attributes.

#### Template-driven Key Features

- Uses the `ngForm` directive to create a form.
- Binds input fields to the model using `[(ngModel)]`.
- Uses the `(ngSubmit)` event to handle form submission.
- Validates inputs using HTML5 attributes like `required`.
- Disables the submit button when the form is invalid using `[disabled]`.

#### Input Fields Tracking

Each input field has different states that help in validation:

- **`pristine`**: The field has not been changed.
- **`dirty`**: The field has been modified.
- **`touched`**: The field has been focused and then left.
- **`valid/invalid`**: The field passes or fails validation.

**Those states can be accessed by**:

1. set a template reference variable on the input field using `#name="ngModel"`.
2. Use the variable to access the field state. (e.g. `name.invalid`, `name.touched`, etc...)

```html
<input type="text" name="name" [(ngModel)]="product.name" #name="ngModel" />
```

#### Template-driven Form Setup

- Setup the form reference with `ngForm` directive. (e.g. `<form #productForm="ngForm">`)
- Use `(ngSubmit)` event to handle form submission. (e.g. `<form (ngSubmit)="createProduct(productForm)">`).
- Each input field **must** have a `name` attribute matching the model property. (e.g. `<input name="price">`)
- Bind input to the model using **two-way data binding** (e.g. `<input name="price" [(ngModel)]="product.price">`).
- Use `#reference="ngModel"` to track field state. (e.g. `<input name="price" [(ngModel)]="product.price" #price="ngModel">`).
- Use HTML5 validation attributes like `required`, `pattern`, etc... (e.g. `<input name="price" [(ngModel)]="product.price" required pattern="[0-9]+">`).
- Disable the submit button when the form is invalid. (e.g. `<button type="submit" [disabled]="productForm.invalid">Create</button>`).

#### Check Form Validity

- Check if the entire form is valid by using `#formRef.invalid` (e.g. `productForm.invalid`).
- Check if a specific field is valid by using `#fieldRef.invalid` (e.g. `name.invalid`) withe `*ngIf` directive or `@if` block.
- Check validation errors and show actual input values.

```html
<h2>Create a New Product</h2>

<form #productForm="ngForm" (ngSubmit)="createProduct(productForm)">
  <!-- Product Name Field -->
  <div>
    <label for="name">Product Name:</label>
    <input
      type="text"
      id="name"
      name="name"
      placeholder="Enter product name"
      required
      minlength="3"
      [(ngModel)]="product.name"
      #name="ngModel"
    />

    <!-- Validation Messages -->
    <div *ngIf="name.invalid && name.touched" style="color: red;">
      <p *ngIf="name.errors?.required">Name is required.</p>
      <p *ngIf="name.errors?.minlength">Minimum 3 characters required.</p>
    </div>
  </div>

  <!-- Product Price Field -->
  <div>
    <label for="price">Price:</label>
    <input
      type="number"
      id="price"
      name="price"
      placeholder="Enter product price"
      required
      min="1"
      [(ngModel)]="product.price"
      #price="ngModel"
    />

    <!-- Validation Messages -->
    <div *ngIf="price.invalid && price.touched" style="color: red;">
      <p *ngIf="price.errors?.required">Price is required.</p>
      <p *ngIf="price.errors?.min">Price must be greater than 0.</p>
    </div>
  </div>

  <!-- Live User Input Preview -->
  <div *ngIf="name.dirty || price.dirty">
    <h3>Live Preview:</h3>
    <p><strong>Name:</strong> {{ product.name }}</p>
    <p><strong>Price:</strong> {{ product.price }}</p>
  </div>

  <!-- Submit Button (Disabled when form is invalid) -->
  <button type="submit" [disabled]="productForm.invalid">Create</button>
</form>
```

```typescript
import { Component } from "@angular/core";
import { NgForm, FormsModule } from "@angular/forms";
import { RouterModule, Router } from "@angular/router";
import { CommonModule } from "@angular/common";

@Component({
  selector: "app-create-product-form",
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: "./create-product-form.component.html",
  providers: [ProductsService],
})
export class CreateProductFormComponent {
  product = { name: "", price: null }; // Ensure a valid default state

  constructor(
    private productsService: ProductsService,
    private router: Router
  ) {}

  createProduct(form: NgForm) {
    if (form.valid) {
      this.productsService.createProduct(this.product);
      this.router.navigateByUrl("/products"); // Redirect after creation
    }
  }
}
```
