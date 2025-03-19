# 🔖 ITI - D0046 - ASP .NET MVC

## Razor Pages

- Razor Pages is a kind of APS.NET Projects that makes coding page-focused.
- Each Razor Page is a page that has a `.cshtml` file and a `.cshtml.cs` file.
- The `.cshtml` file contains the HTML markup and the C# code.
- The `.cshtml.cs` file contains the C# code that is used to handle the page events.
- **Page Events** are methods that are called when the page is requested.
  - The `OnGet` method is called when the page is requested using the `GET` method.
  - The `OnPost` method is called when the page is requested using the `POST` method.

## How to Structure a Razor Pages Project

### Major Files and Folders

- The `Pages` folder contains all the Razor Pages.
- The `Shared` folder contains the layout pages and partial views.

### Structuring the `Pages` Folder

- Create a folder for each entity in the project (e.g. `Pages/Departments`).
- Inside the folder, create page per action (e.g. `Pages/Departments/Index.cshtml`, `Pages/Departments/Create.cshtml`, `Pages/Departments/Edit.cshtml`, `Pages/Departments/Delete.cshtml`).

Look at the example in the [appendix](#full-example-of-a-razor-page) for a better understanding.

### Using the `@page` Directive

- The `@page` directive is used to specify the URL of the page.
- Can be used with a route template to specify the URL pattern (e.g. `@page "{id:int}"`).

### Using `_ValidationScriptsPartial` Partial View

- The `_ValidationScriptsPartial` partial view is used to include the client-side validation scripts.

```cshtml
<!-- embed the _ValidationScriptsPartial in your page, usually at the end of the page if you have a form -->
@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

### Using the `asp-for` Tag Helper

- The `asp-for` tag helper is used to bind the input element to a model property.
- e.g. `<input asp-for="Department.Name" class="form-control" />`.

## Generic Repository Pattern

- The Generic Repository Pattern is a design pattern that is used to create a generic repository that can be used to perform CRUD operations on any entity.

### Creating the Generic Repository

- Create an interface called `IRepository` that contains the CRUD methods.
- Create a class called `Repository` that implements the `IRepository` interface.

```csharp
// IRepository.cs

public interface IRepository<T> where T : class
{
    List<T> GetAll();
    T GetById(object id);
    void Create(T entity);
    void Update(T entity);
    T DeleteById(object id);
    void Delete(T entity);
    void Save();
}
```

```csharp
// Repository.cs
using WebApplication1.Database;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public class Repository<T>(MVCD07DbContext _dbContext) : IRepository<T> where T : class
{
    public void Create(T entity)
    {
        _dbContext.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public T DeleteById(object id)
    {
        T entity = GetById(id);
        if (entity == null)
        {
            return null;
        }
        Delete(entity);
        return entity;
    }

    public List<T> GetAll()
    {
        return _dbContext.Set<T>().ToList();
    }

    public T GetById(object id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
```

## Appendix

### Full Example of a Razor Page

**Department.cs**:

```csharp
// Models/Department.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(10, 50)]
    public int Capacity { get; set; }
}
```

**Index.cshtml**:

```cshtml
<!-- Departments/Index.cshtml -->
@page
@model WebApplication1.Pages.Departments.IndexModel
@{
    ViewData["Title"] = "Departments Index";
}

<table class="table table-bordered table-hover">
    <thead>
        <tr class="table-dark">
            <th>Id</th>
            <th>Name</th>
            <th>Capacity</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var dept in Model.Departments)
        {
            <tr>
                <td>@dept.Id</td>
                <td>@dept.Name</td>
                <td>@dept.Capacity</td>
                <td>
                    <a asp-page="/Departments/Update" asp-route-id="@dept.Id" class="btn btn-primary">Update</a>
                    <a asp-page="/Departments/Delete" asp-route-id="@dept.Id" class="btn btn-danger">Delete</a>
                </td>
            </tr>
        }
    </tbody>
</table>
<a asp-page="/Departments/Create" class="btn btn-success w-100">Create</a>
```

```csharp
// Departments/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Pages.Departments
{
    public class IndexModel(MVCD07DbContext _dbContext) : PageModel
    {
        public List<Department> Departments { get; set; }
        public void OnGet()
        {
            Departments = _dbContext.Departments.ToList();
        }
    }
}
```

**Create.cshtml**:

```cshtml
<!-- Departments/Create.cshtml -->
@page
@model WebApplication1.Pages.Departments.CreateModel
@{
    ViewData["Title"] = "Create Department";
}

<form method="post">
    <div class="d-flex flex-column gap-3 w-50 shadow p-5 m-auto mt-5">
        <div>
            <label asp-for="Department.Name"></label>
            <input asp-for="Department.Name" class="form-control" />
            <span asp-validation-for="Department.Name" class="text-danger"></span>
        </div>
        <div>
            <label asp-for="Department.Capacity"></label>
            <input asp-for="Department.Capacity" class="form-control" />
            <span asp-validation-for="Department.Capacity" class="text-danger"></span>
        </div>
        <div asp-validation-summary="ModelOnly"></div>
        <div>
            <input type="submit" value="Create" class="btn btn-primary form-control" />
        </div>
    </div>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

```csharp
// Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Pages.Departments
{
    public class CreateModel(MVCD07DbContext _dbContext) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Add(Department);
                _dbContext.SaveChanges();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
```

**Update.cshtml**:

```cshtml
<!-- Departments/Update.cshtml -->
@page "{id:int}"
@model WebApplication1.Pages.Departments.UpdateModel
@{
    ViewData["Title"] = "Update Department";
}

<form method="post">
    <div class="d-flex flex-column gap-3 w-50 shadow p-5 m-auto mt-5">
        <input asp-for="Department.Id" hidden />
        <div>
            <label asp-for="Department.Name"></label>
            <input asp-for="Department.Name" class="form-control" />
            <span asp-validation-for="Department.Name" class="text-danger"></span>
        </div>
        <div>
            <label asp-for="Department.Capacity"></label>
            <input asp-for="Department.Capacity" class="form-control" />
            <span asp-validation-for="Department.Capacity" class="text-danger"></span>
        </div>
        <div asp-validation-summary="ModelOnly"></div>
        <div>
            <input type="submit" value="Update" class="btn btn-primary form-control" />
        </div>
    </div>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

```csharp
// Update.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Pages.Departments
{
    public class UpdateModel(MVCD07DbContext _dbContext) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }
        public void OnGet([FromRoute] int id)
        {
            Department = _dbContext.Departments.SingleOrDefault(d => d.Id == id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(Department);
                _dbContext.SaveChanges();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
```

**Delete.cshtml**:

```cshtml
<!-- Departments/Delete.cshtml -->
@page "{id:int}"
@model WebApplication1.Pages.Departments.DeleteModel
@{
    ViewData["Title"] = "Delete Department";
}

<div class="alert alert-danger d-flex flex-column gap-3 m-auto p-5 shadow rounded-3 w-50">
    <h3 class="text-center">Are you sure you want to delete <strong>@Model.Department.Name</strong>?</h3>

    <form method="post" class="d-flex flex-row justify-content-around">
        <button type="submit" class="btn btn-danger w-25">Yes</button>
        <a asp-page="./Index" class="btn btn-dark w-25">No</a>
    </form>
</div>
```

```csharp
// Delete.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Pages.Departments
{
    public class DeleteModel(MVCD07DbContext _dbContext) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }

        public IActionResult OnGet(int id)
        {
            Department = _dbContext.Departments.SingleOrDefault(d => d.Id == id);

            if (Department == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _dbContext.Departments.Remove(Department);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
```

[← Prev](./iti-d0045-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0047-asp-mvc.md)
