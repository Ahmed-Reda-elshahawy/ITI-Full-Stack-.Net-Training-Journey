# 🔖 ITI - D0052 - ASP .NET MVC

## Identity in ASP .NET MVC

**Identity** is a membership system that provides authentication, authorization, and user management functionality.

### Identity Key Features

- User Management
- Roles Management
- Password Management
- User Authentication
- Role-based Authentication
- Claims-based Authentication
- Claims-based Authentication
- External Login Providers (Google, Facebook, Twitter, etc.)
- Two-Factor Authentication

### Setting Up Identity

1. Create a new ASP .NET MVC project.
2. Choose the MVC template.
3. Choose the Authentication type as Individual User Accounts.
4. Apply the migrations to the database.
5. Run the application.

**By Default**, We will have `ApplicationDbContext` which is derived from `IdentityDbContext`.

## `IdentityUser` class

- `IdentityUser` class is a class that represents a user in the identity system.
- We can extend the `IdentityUser` class to add more properties by creating a new class that inherits from `IdentityUser`. (Ex: `ApplicationUser`).
- `IdentityUser` class is a Generic class that takes a type parameter that represents the type of the primary key for the user (by default, it is a string `GUID`).
- If we extends `IdentityUser` class, we need to update the default `ApplicationDbContext` class to use the new class.

  ```csharp
  // ApplicationDbContext.cs
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }
  }

  // Program.cs
  builder.services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddEntityFrameworkStores<ApplicationDbContext>();
  ```

- We can also add extra configuration when registering the service in `Program.cs` file.

  ```csharp
  builder.services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true
    // options.Password.RequireDigit = true
    // options.Password.RequireLowercase = true
    // options.Password.RequireNonAlphanumeric = true
    // options.Password.RequireUppercase = true
    // options.Password.RequiredLength = 8
    // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5)
    // options.Lockout.MaxFailedAccessAttempts = 5
    // options.Lockout.AllowedForNewUsers = true
    })
      .AddEntityFrameworkStores<ApplicationDbContext>();
  ```

### Create new User

```csharp
// UserManager<ApplicationUser> userManager
var user = new ApplicationUser { UserName = "JohnDoe", Email = "doe@example.com" };
await userManager.CreateAsync(user, "password");
```

### Retrieve all Users

```csharp
// UserManager<ApplicationUser> userManager
var users = userManager.Users.ToList();
// var users = await userManager.Users.ToListAsync();
```

### Retrieve User by Id

```csharp
// UserManager<ApplicationUser> userManager
var user = await userManager.FindByIdAsync("user-id");
```

## `IdentityRole` class

- `IdentityRole` class is a class that represents a role in the identity system.
- Add RoleManager service to the application by calling `AddRoles<IdentityRole>()` method in the `Program.cs` file.

  ```csharp
  // Program.cs
  builder.services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddRoles<IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>();

  // AddDefaultIdentity<ApplicationUser> => generates UserManager<ApplicationUser>
  // AddRoles<IdentityRole> => generates RoleManager<IdentityRole>
  ```

### Create new Role

We can use the `RoleManager` service to create, update, delete, and retrieve roles.

```csharp
// RoleManager<IdentityRole> roleManager
var role = new IdentityRole { Name = "Admin" };
await roleManager.CreateAsync(role);
```

### Retrieve all Roles

```csharp
// RoleManager<IdentityRole> roleManager
var roles = roleManager.Roles.ToList();
```

### Assign Role to User

```csharp
// UserManager<ApplicationUser> userManager
await userManager.AddToRoleAsync(user, "Admin");
```

### Check if User has Role

```csharp
// UserManager<ApplicationUser> userManager
var hasRole = await userManager.IsInRoleAsync(user, "Admin");
```

### Retrieve all Users in a Role

```csharp
// UserManager<ApplicationUser> userManager
var users = userManager.GetUsersInRole("Admin");
```

### Remove Role from User

```csharp
// UserManager<ApplicationUser> userManager
await userManager.RemoveFromRoleAsync(user, "Admin");
```

### Delete Role

```csharp
// RoleManager<IdentityRole> roleManager
var role = await roleManager.FindByNameAsync("Admin");
await roleManager.DeleteAsync(role);
```

## Identity UI

- Identity has built-in UI pages for user registration, login, logout, forgot password, reset password, etc.
- We can generate the Identity UI in visual studio by right-clicking on the project and selecting `Add` -> `New Scaffolded Item` -> `Identity`.

[← Prev](./iti-d0046-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0047-asp-api.md)
