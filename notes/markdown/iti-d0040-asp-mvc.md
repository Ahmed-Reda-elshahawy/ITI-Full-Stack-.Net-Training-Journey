# 🔖 ITI - D0040 - ASP .NET MVC

## Overview

- **ASP .NET MVC** is a **web application framework** developed by Microsoft.
- It used to build maintainable, scalable, and testable web applications.
- **MVC** stands for **Model-View-Controller**.
  - **Model** represents the data.
  - **View** represents the user interface.
  - **Controller** handles the requests and responses.

## Client Server Model

- **Server** is a machine that provides data or services to other machines.
- **Client** is a machine that requests data or services from the server (e.g., web browser).
- Web application runs on the server on specific ports (e.g., 80, 443).

## HTTP

- **HTTP** stands for **HyperText Transfer Protocol**.
- It is a protocol used to transfer data between the client and server.

### HTTP Request

- **HTTP Request** is a message sent by the client to the server.
- It consists of:
  - **Request Line** (e.g., GET /index.html HTTP/1.1).
  - **Request Header** (e.g., Host, User-Agent).
  - **Request Body** (e.g., form data).

**Example**:

![HTTP Request Example](./imgs/http_request_example.png)

### HTTP Response

- **HTTP Response** is a message sent by the server to the client.
- It consists of:
  - **Status Line** (e.g., HTTP/1.1 200 OK).
  - **Response Header** (e.g., Content-Type).
  - **Response Body** (e.g., HTML content).

**Example**:

![HTTP Response Example](./imgs/http_response_example.png)

### HTTP Key Points

- **Stateless Protocol**: Each request is independent of the previous request.
- **Connectionless Protocol**: The connection is closed after the response is sent.
- **URL**: Uniform Resource Locator.
  - are usually case-insensitive.
- **State Management**: Cookies, Sessions.

## ASP .NET MVC Components

- **Model**: Represents the data. (e.g. Entities in the Entity Framework).
- **View**: Represents the user interface (e.g. HTML, CSS & JS Composed in Razor Views `.cshtml`).
  - **Razor View Engine**: Combines C# and HTML to create dynamic web pages.
- **Controller**: Handles the requests and responses (e.g. C# classes that inherit from `Controller`).

### Web Server Used in ASP .NET MVC

- **IIS**: Internet Information Services.
  - It is a web server provided by Microsoft.
  - It is used to host ASP .NET applications on Windows.
- **Kestrel**: Cross-platform web server.
  - It is used to host ASP .NET Core applications.

### ASP .NET Naming Conventions

- **Controller**: Ends with `Controller` (e.g. `HomeController`).
- **View**: Ends with `.cshtml` (e.g. `Index.cshtml`).
  - Each Controller has a folder with the same name in the `Views` folder.
  - Each Controller method has a corresponding view file in the folder with the same name as the method (e.g. `Index.cshtml` for the `Index` method).
- **Model**: Ends with `Model` (e.g. `User`).
- **Action Method**: Starts with a verb (e.g. `GetUsers`).

> [!Note]
>
> To Form full path of specific view, or asset file we can use `~` to start from the root of the application. (e.g. `~/Views/Home/Index.cshtml`).

## Bind Data to View

- **ViewData**: Dictionary object used to pass data from the controller to the view (e.g. `ViewData["Title"] = "Home"`).
  - It is a key-value pair.
  - It is a weakly typed.
- **ViewBag**: Dynamic property used to pass data from the controller to the view (e.g. `ViewBag.Title = "Home"`).
  - It is a wrapper around `ViewData`.
  - It provides a dynamic property to access the data (e.g. `ViewBag.Title`).
  - It will automatically convert the data type.
- **Model**: Strongly typed object used to pass data from the controller to the view (e.g. `return View(user)`).
  - It is a strongly typed.

**Example**:

```cshtml
@model User
@{
    Title = "Home";
}

<h1>@Title</h1>

<h2>@ViewBag.Message</h1>
<h3>@Model.Name</h2>
```

```csharp
public IActionResult Index()
{
    ViewBag.Message = "Welcome to the Home Page";
    User user = new User { Name = "John Doe" };
    return View(user);
}
```

## Access Request Data in Controller Action

- **Model Binder**: Automatically maps the data from the request to the action method parameters.
- Binds the data from
  - **Form Data**: Form fields.
  - **Route Data**: URL segments.
  - **Query String**: URL parameters.
  - **Request Body**: JSON data.
- We can use annotations to specify the source of the data.
  - **FromQuery**: Binds the data from the query string.
  - **FromRoute**: Binds the data from the route data.
  - **FromBody**: Binds the data from the request body.
  - **FromForm**: Binds the data from the form data.
- Access request data using the `Request` object.

**Example**:

```csharp
public IActionResult Index(string name)
{
    return Content($"Hello {name}");
}

public IActionResult Index(User user)
{
    return Content($"Hello {user.Name}");
}

public IActionResult Index([FromQuery] string name)
{
    return Content($"Hello {name}");
}

public IActionResult Index([FromRoute] string name)
{
    return Content($"Hello {name}");
}

// using Request object
public IActionResult Index()
{
    string name = Request.Query["name"];
    return Content($"Hello {name}");
}
```

**Note**: Model Binder is Smart to map the form-data to the object properties.

**Example**:

```cshtml
<form asp-action="Index" method="post">
    <input type="text" name="Name" />
    <button type="submit">Submit</button>
</form>
```

```csharp
public IActionResult Index(User user)
{
    return Content($"Hello {user.Name}");
}
```
