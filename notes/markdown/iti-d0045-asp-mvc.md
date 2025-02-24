# 🔖 ITI - D0045 - ASP .NET MVC

## HTTP Request State Management in ASP .NET MVC

- HTTP is a stateless protocol, which means that each request is independent of the previous request.
- So if you want to maintain state between requests, you need to use some kind of state management.
- There are three ways to manage state in ASP .NET MVC:
  - **Hidden Fields**: It is used to store data that is not displayed on the page.
  - **Cookies**: It is used to store data on the client's machine.
  - **Session**: It is used to store data on the server.
  - **TempData**: It is used to store data for a short period of time.

### Hidden Fields

- Hidden fields are used to store data that is not displayed on the page.
- using html form input tag with type="hidden".

**Example in Razor View:**

```html
<input type="text" name="name" value="value" hidden />
```

### Cookies

- Cookies are used to store data on the client's machine.
- Cookies are stored in the client's browser.
- Browser sends cookies with each request to the server.
- Cookies types:
  - **Session Cookie**: It is stored in the browser's memory and is deleted when the browser is closed.
  - **Persistent Cookie**: It is stored on the client's machine and is not deleted when the browser is closed.

**Example in ASP .NET MVC:**

```csharp
// Set Cookie in Response from Controller Action
public ActionResult Index()
{
    Response.Cookies.Append("name", "value"); // session cookie
    Response.Cookies.Append("name", "value", new CookieOptions { Expires = DateTime.Now.AddDays(1) }); // persistent cookie
    return View();
}

// Get Cookie in Request in Controller Action
public ActionResult Index()
{
    var name = Request.Cookies["name"];
    return View();
}
```

### Sessions

- Sessions are used to store data on the server memory.
- Server sends a unique session id to the client's browser using cookies.
- Browser sends the session id with each request to the server to identify the session.

**Example in ASP .NET MVC:**

```csharp
// Set Session in Controller Action
public ActionResult Index()
{
    HttpContext.Session.SetString("name", "value");
    HttpContext.Session.SetInt32("age", 25);
    return View();
}

// Get Session in Controller Action
public ActionResult Index()
{
    var name = HttpContext.Session.GetString("name");
    var age = HttpContext.Session.GetInt32("age");
    return View();
}
```

> [!IMPORTANT]
>
> To Enable Session in ASP .NET MVC, you need to
>
> 1. Register Session Service in ConfigureServices method in `Program.cs`
> 2. Add Session Middleware in Configure method in `Program.cs`

```csharp
// Program.cs

// Register Session Service
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // default is 20 minutes
    options.Cookie.HttpOnly = true; // default is true
    options.Cookie.IsEssential = true; // default is false, used to store essential data only (GDPR)
});

// Add Session Middleware after Routing Middleware
app.UseSession();
```

### TempData

- `TempData` is used to store data **temporarily** between two consecutive requests.
- It is ideal for scenarios like **redirects** where you need to pass data from one action to another.
- It uses **session storage** under the hood but automatically removes the data after it's read (unless explicitly kept).
- TempData once read is removed from the session.
- Common use cases: **One-time messages**, like success notifications after form submission.

**Example in ASP .NET MVC:**

```csharp
// Set TempData in Controller Action
public ActionResult Index()
{
    TempData["Message"] = "Data saved successfully!";
    return RedirectToAction("Success");
}

// Get TempData in Another Action
public ActionResult Success()
{
    var message = TempData["Message"] as string; // Data is removed after reading
    return View();
}
```

**Accessing TempData in Razor View:**

```cshtml
@{
    var message = TempData["Message"] as string;
}
```

**Keep TempData for Next Request:**

```csharp
// Keep TempData for Next Request
public ActionResult Index()
{
    TempData.Keep("Message");
    return RedirectToAction("Success");
}
```

## Authentication and Authorization in ASP .NET MVC

- **Authentication**: It is the process of verifying the identity of a user.
- **Authorization**: It is the process of verifying what a user has access to.

### Authentication Mechanisms in ASP .NET MVC

- ASP .NET MVC provides built-in authentication mechanisms.
- **Cookie Authentication**: It is used to authenticate users using cookies.
- **Bearer Token Authentication**: It is used to authenticate users using bearer tokens. (e.g., JWT)

### Setting up Authentication & Authorization in ASP .NET MVC

1. **Configure Authentication Service in `Program.cs`**

   ```csharp
   // register authentication services
   builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.LoginPath = "/Account/Login";
           options.AccessDeniedPath = "/Account/AccessDenied";
       });
   ```

2. **Register Authentication Middleware in `Program.cs`**

   ```csharp
   // register authentication middleware after routing
   app.UseAuthentication();
   ```

3. **Register Authorization Middleware in `Program.cs`**

   ```csharp
   // register authorization middleware after authentication
   app.UseAuthorization();
   ```

4. **Secure Controller Actions using `[Authorize]` Attribute**

   Can be applied to the whole controller or specific action methods.

   ```csharp
   [Authorize]
   public class HomeController : Controller
   {
       public IActionResult Index()
       {
           return View();
       }
   }
   ```

### Login in ASP .NET MVC

- **Login Form**: Create a login form in the view.
- **Login Action**: Create a login action in the controller.
- **Authenticate User**: Authenticate the user using the login action.
- **Set Authentication Cookie**: Set the authentication cookie if the user is authenticated.

**Example of Login Action in ASP .NET MVC:**

```csharp
// Login Action in Account Controller
[HttpGet]
public ActionResult Login()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> Login(LoginModel model)
{
    if (ModelState.IsValid)
    {
        if (model.Username == "admin" && model.Password == "admin")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("", "Invalid username or password.");
    }
    return View(model);
}
```

### Render Razor View based on User Authentication

- Use `User.Identity.IsAuthenticated` to check if the user is authenticated.
- Use `User.Identity.Name` to get the username of the authenticated user.
- Use `User.IsInRole("RoleName")` to check if the user is in a specific role.

**Example of Rendering View based on User Authentication:**

```cshtml
@if (User.Identity.IsAuthenticated)
{
    <p>Welcome, @User.Identity.Name!</p>
    @if (User.IsInRole("Admin"))
    {
        <p>You are an Admin!</p>
    }
}
else
{
    <p>Please login to access the content.</p>
}
```

### Authentication and Authorization Roles in ASP .NET MVC

- **Roles**: Roles are used to group users based on their permissions.
- **Role-based Authorization**: It is used to restrict access to users based on their roles.

**Example of Role-based Authorization in ASP .NET MVC:**

```csharp
// Secure Controller Action using Role-based Authorization
[Authorize
(Roles = "Admin")]
public ActionResult Index()
{
    return View();
}
```

We should add the role to the user claims when the user logs in. [Refer to the Login Action Example](#login-in-asp-net-mvc).

#### Conventions for Role-based Authorization

- Create a Roles Model for Role-based Authorization (Maps to Database Table).
- Add Relationship between User and Roles (Many-to-Many).
- Secure Controller Actions using `[Authorize
(Roles = "RoleName")]` Attribute.
- Render Views based on User Roles.

### Logout in ASP .NET MVC

- **Logout Action**: Create a logout action in the controller.
- **Sign Out**: Sign out the user by removing the authentication cookie.

**Example of Logout Action in ASP .NET MVC:**

```csharp
// Logout Action in Account Controller
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Login");
}
```

## Display Model Errors in Razor View

_Review [ASP .NET Validation](./iti-d0042-asp-mvc.md#asp-validation)_

- Use `asp-validation-summary="ModelOnly"` to display model errors.
- Use `asp-validation-summary="All"` to display all errors.
- Use `asp-validation-for` to display errors for a specific model property.

**Example of using Model Errors in Razor View:**

```csharp
// Controller Action
public ActionResult Index()
{
    ModelState.AddModelError("name", "Name is required.");
    return View();
}
```

```cshtml
<!-- Razor View -->
<form asp-action="Index" method="post">
  <div asp-validation-summary="ModelOnly"></div>
  <input asp-for="name" />
  <span asp-validation-for="name"></span>
  <button type="submit">Submit</button>
</form>
```

[← Prev](./iti-d0044-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0046-asp-mvc.md)
