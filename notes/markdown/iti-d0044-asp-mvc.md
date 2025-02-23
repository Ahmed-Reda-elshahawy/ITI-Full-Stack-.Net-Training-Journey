# 🔖 ITI - D0044 - ASP .NET MVC

## Action Filters in ASP.NET MVC

- Action filters are used to implement logic that gets executed before or after an action method is called.
- We can use action filters to filter execution of an action method.
- Action filters can be applied to an action method or to a controller.

### `ActionFilterAttribute` Class

- The `ActionFilterAttribute` class is an abstract class that provides the base class for action filters.
- The `ActionFilterAttribute` class implements the `IActionFilter` and `IResultFilter` interfaces.
- The `ActionFilterAttribute` class provides the following methods:
  - `OnActionExecuting` method: This method is called before an action method is called.
  - `OnActionExecuted` method: This method is called after an action method is called.
  - `OnResultExecuting` method: This method is called before a result is executed.
  - `OnResultExecuted` method: This method is called after a result is executed.

**Example:**

```csharp
// CustomActionFilter.cs
namespace App.Filters

public class CustomActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Logic before action method is called
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // Logic after action method is called
    }
}

namespace App.Controllers
{
    [CustomActionFilter] // Apply action filter to all action methods in the controller
    public class HomeController : Controller
    {
      // [CustomActionFilter] // Apply action filter to a specific action method
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

## Middleware in ASP.NET Core

- Middleware is a function that sits in the request pipeline and processes requests and responses.
- Each middleware can:
  - Handle an incoming HTTP request.
  - Pass the request to the next middleware in the pipeline.
  - Perform an action before or after the next middleware in the pipeline.

### How Middleware Works

- Middleware is executed in the order it is added to the pipeline in the `Program.cs` file.
- Each middleware can either:
  - **Short-circuit** the pipeline and return a response.
  - Pass the request to the next middleware in the pipeline using the `next` parameter.
  - `next` is a delegate that represents the next middleware in the pipeline.

### Request Basic Flow

1. The request is received by the server.
2. The request is passed to the first middleware in the pipeline.
3. The middleware processes the request and passes it to the next middleware in the pipeline.
4. The last middleware in the pipeline generates a response.
5. The response is passed back through the pipeline (in reverse order) to the client.

### Middleware Types

- **Built-in Middleware**: Provided by ASP.NET Core.
  - **UseStaticFiles**: Serves static files.
  - **UseRouting**: Routes requests to controllers.
  - **UseAuthorization**: Authorizes requests.
  - **MapStaticFiles**: Maps static files.
  - **UseExceptionHandler**: Handles exceptions.
- **Custom Middleware**: Developed by the developer.

### Middleware Different Behaviors

- **Terminal Middleware**: Short-circuits the pipeline and returns a response. (e.g. `app.Run()`)
- **Non-terminal Middleware**: Passes the request to the next middleware in the pipeline. (e.g. `app.Use()`)

**Example (Terminal Middleware):**

```csharp
// Program.cs

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello, World!");
});
```

**Example (Non-Terminal Middleware):**

```csharp
// Program.cs

app.Use(async (context, next) =>
{
    // Logic before the next middleware
    await next.Invoke();
    // Logic after the next middleware
});
```

## Layouts in ASP.NET Core

- Layouts are used to define the common structure of a web page.
- We can define a layout in a Razor view file (like `_Layout.cshtml`).
- Layouts can contain placeholders for content that is defined in other views.
  - `@RenderBody()`: Renders the content of the view.
  - `@RenderSection()`: Renders the content of a section.
- We can define sections in a view using the `@section` directive.

**Example:**

```html
<!-- _Layout.cshtml -->
<!DOCTYPE html>
<html>
  <head>
    <title>@ViewBag.Title</title>
  </head>
  <body>
    <header>@RenderSection("Header", required: false)</header>
    <main>@RenderBody()</main>
    <footer>@RenderSection("Footer", required: false)</footer>
  </body>
</html>
```

```html
<!-- Index.cshtml -->
@{ Layout = "_Layout"; ViewBag.Title = "Home Page"; } @section Header {
<h1>Welcome to the Home Page</h1>
} @section Footer {
<p>&copy; 2021</p>
}

<p>This is the content of the Home Page.</p>
```

- Use the `Layout` property to specify the layout for a view.
- Use the `ViewBag.Title` property to set the title of the page.
- If the layout is already defined in the `_ViewStart.cshtml` file, you don't need to specify the layout in the view.
- If we need to add scripts or styles to a specific view, we can use the `@section Scripts` directive in the layout and the `@RenderSection("Scripts", required: false)` in the layout file.

## Partial Views in ASP.NET Core

- Partial views are reusable views that can be rendered in other views.
- Partial views can be used to render common UI elements.

### Creating a Partial View

- Create a partial view file (e.g., `_PartialView.cshtml`). The name of the partial view file should start with an underscore.
- Use the `Partial` method in a view to render the partial view.

**Example:**

```html
<!-- _PartialView.cshtml -->
<h2>Partial View</h2>
<p>This is a partial view.</p>
```

```html
<!-- Index.cshtml -->
@{ Layout = "_Layout"; ViewBag.Title = "Home Page"; }

<h1>Welcome to the Home Page</h1>
@Html.Partial("_PartialView")
<!-- or -->
<partial name="_PartialView" />
```

## View Imports in ASP.NET Core

- View imports are used to import namespaces and define common directives for views.
- View imports are defined in a `_ViewImports.cshtml` file.
- The `_ViewImports.cshtml` file is automatically applied to all views in the folder where it is located.
- Use the `@using` directive to import namespaces.

**Example:**

```html
<!-- _ViewImports.cshtml -->
@using App.Models @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

## Scaffolded Controllers & Views in ASP.NET Core

- Scaffolded controllers and views are generated automatically by Visual Studio.
- Scaffolded controllers and views are generated based on a model class.

### Steps to Scaffold a Controller & Views

- Add new Controller
- Choose MVC Controller with views, using Entity Framework.
- Choose the Model class and Data context class.
- Choose the views to generate (Create, Read, Update, Delete).
- Click Add.
- The controller and views are generated automatically.

[← Prev](./iti-d0043-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0045-asp-mvc.md)
