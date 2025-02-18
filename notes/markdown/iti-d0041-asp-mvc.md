# 🔖 ITI - D0041 - ASP .NET MVC

_Review [Binding Data to View](./iti-d0040-asp-mvc.md#bind-data-to-view)_

## Returned Types from Controller Actions

- **ViewResult**: Represents HTML and markup.
- **ContentResult**: Represents plain text.
  - Returned types like **string**, **int**, **bool** are automatically converted to **ContentResult**.
- **IActionResult**: Base interface for **ViewResult** and **ContentResult**.
- **JsonResult**: Represents JSON.
- **FileResult**: Represents a file.
- **RedirectResult**: Represents a redirection to a URL.
- **RedirectToActionResult**: Represents a redirection to an action.

> We can use **IActionResult** as a return type for a controller action to return any of the above types.

### Commonly Used Methods

- **View()**: Returns a **ViewResult**.
- **Content()**: Returns a **ContentResult**.
- **Json()**: Returns a **JsonResult**.
- **File()**: Returns a **FileResult**.
- **Redirect()**: Returns a **RedirectResult**.
- **RedirectToAction()**: Returns a **RedirectToActionResult**.
- **RedirectPermanent()**: Returns a **RedirectResult** with a status code of 301.
- **NotFound()**: Returns a **NotFoundResult** with a status code of 404.
- **BadRequest()**: Returns a **BadRequestResult** with a status code of 400.

**Example**:

```csharp
public IActionResult Index()
{
    return View();
}

public IActionResult About()
{
    return Content("About page");
}

public IActionResult Contact()
{
    return Json(new { Name = "John", Age = 25 });
}

public IActionResult Download()
{
    return File("~/assets/file.txt", "text/plain", "file.txt");
}

public IActionResult Redirect()
{
    return Redirect("https://www.google.com");
}

public IActionResult RedirectToAbout()
{
    return RedirectToAction("About");
}
```

## EF Core With ASP .NET MVC

### Install Entity Framework Core

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

**Using Package Manager Console**:

```bash
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```

### Create `DbContext` Class

```csharp
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=MyDb;Trusted_Connection=True;");
    }

    public DbSet<Student> Students { get; set; }
}
```

### Create Model Class

```csharp
public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [NotNull]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [NotNull]
    [Range(18, 60)]
    public int Age { get; set; }
}
```

### Create Migration

```bash
dotnet ef migrations add CreateStudentsTable
```

**Using Package Manager Console**:

```bash
Add-Migration CreateStudentsTable
```

### Update Database

```bash
dotnet ef database update
```

**Using Package Manager Console**:

```bash
Update-Database
```

### Use `DbContext` in Controller

```csharp
public class StudentController : Controller
{
    // We will use dependency injection to inject the DbContext later.
    private readonly AppDbContext _context = new AppDbContext();

    public IActionResult Index()
    {
        var students = _context.Students.ToList();
        return View(students);
    }
}
```

### Add `Index.cshtml` View

```html
@model IEnumerable<Student>
  @{ ViewData["Title"] = "Index"; }

  <h1>Index</h1>

  <table class="table table-bordered table-hover">
    <thead>
      <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Age</th>
      </tr>
    </thead>
    <tbody>
      @if (!Model.Any()) // Better way to check if list is empty {
      <tr>
        <td colspan="4" style="text-align: center;" class="fw-bold">
          No Students Found!
        </td>
      </tr>
      } else { @foreach (var std in Model) {
      <tr>
        <td>@std.Id</td>
        <td>@std.Name</td>
        <td>@std.Age</td>
      </tr>
      } }
    </tbody>
  </table></Student
>
```

## Controller Action Route Patterns

We can change the default route pattern from `Program.cs`:

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
```

## Map Action to Specific HTTP Methods (Action Selector)

We can annotate the controller action with the `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]` attributes to map the action to a specific HTTP method.

```csharp
[HttpGet]
public IActionResult Index()
{
    return View();
}

[HttpPost]
public IActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(student);
}
```

## ASP Tag Helpers

Tag Helpers are used to generate HTML elements in Razor views.

### Form Tag Helper

- **asp-action**: The action method to be called.
- **asp-controller**: The controller to be called.
- **asp-route-id**: The route parameter.
- **asp-for**: The model property to bind
- **asp-validation-summary**: Displays validation errors.
- **asp-validation-for**: Displays validation errors for a specific property.
- **asp-items**: Used with dropdown lists.
- **asp-route-**: Used to pass query string parameters.
- **asp-**: Used to generate HTML attributes.

**Example**:

```cshtml
@model Student
@{
    ViewData["Title"] = "Create";
    var selectDeptList = new SelectList(ViewBag.Departments, "Id", "Name", 1);
}

<h1>Create</h1>


<form method="post" asp-action="Create">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <div class="d-flex flex-column gap-3 w-75">
        <div>
            <label asp-for="Name"></label>
            <input asp-for="Name" class="form-control" />
            <span asp-validation-for="Name" class="text-danger"></span>
        </div>
        <div>
            <label asp-for="Email"></label>
            <input asp-for="Email" class="form-control" />
            <span asp-validation-for="Email" class="text-danger"></span>
        </div>
        <div>
            <label asp-for="Age"></label>
            <input asp-for="Age" class="form-control" />
            <span asp-validation-for="Age" class="text-danger"></span>
        </div>
        <div>
            <label asp-for="DeptId">Department</label>
            <select asp-for="DeptId" asp-items="@selectDeptList" class="form-control"></select>
            <span asp-validation-for="DeptId" class="text-danger"></span>
        </div>
        <div>
            <input type="submit" value="Create" class="btn btn-primary" />
        </div>
    </div>
</form>
<script src="~/lib/jquery/dist/jquery.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js"></script>
```
