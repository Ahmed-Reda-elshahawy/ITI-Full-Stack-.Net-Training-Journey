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

  ```html
  <input type="number" name="price" [(ngModel)]="product.price" />
  ```

- Uses the `(ngSubmit)` event to handle form submission.

  ```html
  <form action="" (ngSubmit)="createProduct()"></form>
  ```

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

### Reactive forms

- Reactive forms depends on validation logic in the component class.
- Reactive forms is mostly used for complex forms.
- Reactive forms security is better than template-driven forms.

**Example: Registration Form**:

```html
<h2>Register</h2>

<!-- Display Form Values for Debugging -->
<h3>Live Form Value:</h3>
<pre>{{ registeredUser.value | json }}</pre>

<form [formGroup]="registeredUser" (ngSubmit)="onSubmit()">
  <!-- Username Field -->
  <div>
    <label for="userName">Username</label>
    <input
      type="text"
      id="userName"
      formControlName="userName"
      placeholder="Enter your username"
    />
    <!-- Validation Messages -->
    <div *ngIf="userName.invalid && userName.touched" style="color: red;">
      <p *ngIf="userName.errors?.required">Username is required.</p>
      <p *ngIf="userName.errors?.minlength">
        Minimum {{ userName.errors?.minlength.requiredLength }} characters
        required.
      </p>
      <p *ngIf="userName.errors?.pattern">Only letters are allowed.</p>
    </div>
  </div>

  <!-- Password Field -->
  <div>
    <label for="password">Password</label>
    <input
      type="password"
      id="password"
      formControlName="password"
      placeholder="Enter your password"
    />
    <!-- Validation Messages -->
    <div *ngIf="password.invalid && password.touched" style="color: red;">
      <p *ngIf="password.errors?.required">Password is required.</p>
      <p *ngIf="password.errors?.minlength">
        Minimum {{ password.errors?.minlength.requiredLength }} characters
        required.
      </p>
    </div>
  </div>

  <button type="submit" [disabled]="registeredUser.invalid">Register</button>
</form>
```

```typescript
import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";

@Component({
  selector: "app-register-form",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./register-form.component.html",
})
export class RegisterFormComponent {
  registeredUser: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.registeredUser = this.formBuilder.group({
      userName: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.pattern("^[A-Za-z]{3,}$"),
        ],
      ],
      password: ["", [Validators.required, Validators.minLength(6)]],
    });
  }

  // Getter methods for easy access in template
  get userName() {
    return this.registeredUser.get("userName");
  }

  get password() {
    return this.registeredUser.get("password");
  }

  onSubmit() {
    if (this.registeredUser.valid) {
      console.log("Form Submitted:", this.registeredUser.value);
    }
  }
}
```

#### Reactive forms key features

- Can access form values using `formGroup.value`. (e.g. `registeredUser.value`).
- Also can access single value with `formGroup.get('controlName')`. (e.g. `registeredUser.get('userName')`).
- We can enhance accessing with `get` by adding a getter method in the component class.

```typescript
get userName() {
  return this.registeredUser.get("userName");
}
```

```html
<h4>Username: {{ userName.value }}</h4>
```

- Now we can access also access state of property like `userName.invalid`, `userName.touched`, etc...

#### Working with Nested From Groups

- We can create nested form groups to group related form controls.

```typescript
export class RegisterFormComponent {
  registeredUser: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.registeredUser = this.formBuilder.group({
      userName: [
        "",
        [
          Validators.required,
          Validators.minLength(3),
          Validators.pattern("^[A-Za-z]{3,}$"),
        ],
      ],
      password: ["", [Validators.required, Validators.minLength(6)]],
      address: this.formBuilder.group({
        street: ["", Validators.required],
        city: ["", Validators.required],
        state: ["", Validators.required],
        zip: ["", Validators.required],
      }),
    });
  }
```

```html
<h2>Register</h2>

<form [formGroup]="registeredUser" (ngSubmit)="onSubmit()">
  <!-- Username Field -->
  <div>
    <label for="userName">Username</label>
    <input
      type="text"
      id="userName"
      formControlName="userName"
      placeholder="Enter your username"
    />
    <!-- Validation Messages -->
    <div *ngIf="userName.invalid && userName.touched" style="color: red;">
      <p *ngIf="userName.errors?.required">Username is required.</p>
      <p *ngIf="userName.errors?.minlength">
        Minimum {{ userName.errors?.minlength.requiredLength }} characters
        required.
      </p>
      <p *ngIf="userName.errors?.pattern">Only letters are allowed.</p>
    </div>
  </div>

  <!-- Address Field Group -->
  <div formGroupName="address">
    <h3>Address</h3>
    <!-- Street Field -->
    <div>
      <label for="street">Street</label>
      <input
        type="text"
        id="street"
        formControlName="street"
        placeholder="Enter your street"
      />
      <!-- Validation Messages -->
      <div
        *ngIf="address.street.invalid && address.street.touched"
        style="color: red;"
      >
        <p *ngIf="address.street.errors?.required">Street is required.</p>
      </div>
    </div>

    <!-- City Field -->
    <div>
      <label for="city">City</label>
      <input
        type="text"
        id="city"
        formControlName="city"
        placeholder="Enter your city"
      />
      <!-- Validation Messages -->
      <div
        *ngIf="address.city.invalid && address.city.touched"
        style="color: red;"
      >
        <p *ngIf="address.city.errors?.required">City is required.</p>
      </div>
    </div>

    <!-- State Field -->
    <div>
      <label for="state">State</label>
      <input
        type="text"
        id="state"
        formControlName="state"
        placeholder="Enter your state"
      />
      <!-- Validation Messages -->
      <div
        *ngIf="address.state.invalid && address.state.touched"
        style="color: red;"
      >
        <p *ngIf="address.state.errors?.required">State is required.</p>
      </div>
    </div>
  </div>
</form>
```

#### Cross-Field Validation with Reactive Forms

- We can create custom validation logic to validate multiple fields together.
- We can create a custom validator function and add it to the form control.

```typescript
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordMatched(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.get("password");
    const confirmPassword = control.get("confirmPassword");

    return password &&
      confirmPassword &&
      password.value !== confirmPassword.value
      ? { passwordMatch: true }
      : null;
  };
}
```
