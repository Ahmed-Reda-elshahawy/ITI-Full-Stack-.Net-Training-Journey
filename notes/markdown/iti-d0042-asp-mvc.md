# 🔖 ITI - D0042 - ASP .NET MVC

_Review [Tag Helpers](./iti-d0041-asp-mvc.md#asp-tag-helpers)_

## Annotations With Controller Actions

- Set http method with **[HttpGet]**, **[HttpPost]**, **[HttpPut]**, **[HttpDelete]**.
- Alias for action method with **[ActionName]**.
- **[Route]** attribute to set the route pattern.

**Examples**:

```csharp
[HttpGet]
public IActionResult Index()
{
    return View();
}

[HttpPost]
[ActionName("Create")]
public IActionResult CreateStd(Student student)
{
    if (ModelState.IsValid)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(student);
}

[Route("students/{id}")]
public IActionResult Details(int id)
{
    var student = _context.Students.Find(id);
    return View(student);
}
```

## ASP Tag Helpers with Form and Hidden Fields

_Review [Tag Helpers](./iti-d0041-asp-mvc.md#asp-tag-helpers)_

**Hidden Field**: used to store the value of a property that is not displayed in the form.

```html
<input asp-for="Id" type="hidden" />
```

## ASP Validation

Validation in done by 3 steps:

- Add validation attributes to the model properties.
- Check the model state in the controller action.
- Display validation errors in the view.

### Validation Attributes on Model Properties

- **[Required]**: The property is required.
- **[Range]**: The property value is in a specific range.
- **[StringLength]**: The property value length is in a specific range.
- **[RegularExpression]**: The property value matches a specific pattern.
- **[MinLength]**: The property value length is at least a specific length.
- **[MaxLength]**: The property value length is at most a specific length.
- **[EmailAddress]**: The property value is a valid email address.
- **[Remote]**: The property value is unique.
  - accepts 4 parameters: action, controller, error message and also the additional fields to check uniqueness.
  - e.g. `[Remote("IsEmailUnique", "Students", ErrorMessage = "Email already exists", AdditionalFields = "Id")]`
- **[Compare]**: The property value is equal to another property value.
- **[NotMapped]**: The property is not mapped to a database column.

### Validation in Controller Action

When the model state is not valid, the controller action returns the view with the model.

```csharp
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

### Display Validation Errors in View

- Use Tag Helpers like **asp-for**, **asp-validation-summary**, **asp-validation-for**.
- Use JQuery Validation Library.

### Validation Full Example

**Model:**

```cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Remote("CheckNameExistence", "Department", AdditionalFields = "Id", ErrorMessage = "Invalid Name!")]
    public string Name { get; set; }

    [Required]
    [Range(5, 100)]
    public int Capacity { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

    public override string ToString()
    {
        return $"Department {{Id = {Id}, Name = {Name}, Capacity = {Capacity}}}";
    }
}
```

**Controller Action**:

```cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private MVCD03DbContext _db = new MVCD03DbContext();
        public IActionResult Index()
        {
            return View(_db.Departments.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (!ModelState.IsValid)
            {
                return View(dept);
            }
            _db.Departments.Add(dept);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult CheckNameExistence(string name, int? id)
    {
        bool nameExists = id == null
            ? _db.Departments.Any(d => d.Name == name)
            : _db.Departments.Any(d => d.Name == name && d.Id != id);

        if (nameExists)
        {
            return Json($"The name '{name}' is already exist.");
        }

        return Json(true);
    }
}
```

**Create View**:

```cshtml
@model Department
@{
    ViewData["Title"] = "Create";
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
            <label asp-for="Capacity"></label>
            <input asp-for="Capacity" class="form-control" />
            <span asp-validation-for="Capacity" class="text-danger"></span>
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
