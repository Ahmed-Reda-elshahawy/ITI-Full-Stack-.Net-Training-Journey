# 🔖 ITI - D0039 - Entity Framework

## Overview

- **Entity Framework (EF)** is an Object-Relational Mapper (ORM) that enables .NET developers to work with a database using .NET objects.
- **ORM** is a technique that lets developers interact with a database using object-oriented programming languages.
- **Entity Framework** is an open-source ORM framework for .NET applications.

## EF Core Installation for SQL Server

You can install EF Core using the following methods:

### Using NuGet Package Manager

```bash
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```

### Using .NET CLI

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Using GUI

1. Right-click on the project.
2. Click on Manage NuGet Packages.
3. Search for `Microsoft.EntityFrameworkCore.SqlServer` and `Microsoft.EntityFrameworkCore.Tools`.
4. Click on Install.

> [!Tip]
>
> Disable `Nullable` Reference Types in the project file to avoid warnings.

## Development Approaches in EF Core

### Code-First Approach

- In the **Code-First** approach, you write the classes first and then create the database from these classes.
- You can use **Data Annotations** or **Fluent API** to configure the model classes.

### Database-First Approach

- In the **Database-First** approach, you create the database first and then generate the classes from the database.
- Use the **Scaffold-DbContext** command to generate the classes.

#### Using Package Manager Console

```bash
scaffold-dbcontext "Server=.;Database=SchoolDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

- After running the above command, the classes will be generated in the `Models` folder, and the `DbContext` class will be generated in the root directory.
- All model classes will be partial classes, so you can add custom properties in another file.

#### Using Visual Studio Power Tools

1. Install the [Entity Framework Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) extension.
2. Right-click on the project.
3. Click on `Entity Framework` and then `Reverse Engineer Code First`.
4. Enter the connection string and click OK.
5. The classes will be generated in the `Models` folder, and the `DbContext` class will be generated in the root directory.

- Click on `Entity Framework` and then `Reverse Engineer Code First`.
- Enter the connection string and click on OK.
- The classes will be generated in the `Models` folder and the `DbContext` class will be generated in the root directory.
- All Model classes will be partial classes, so we can add our custom properties in another file.

## Code First Approach Workflow

### Create DbContext Class

- Create a class that inherits from `DbContext`.
- Override the `OnConfiguring` method to configure the database provider.
- Create a `DbSet` property for each model class.
- Add the `DbSet` properties in the `OnModelCreating` method if you want to configure the model using Fluent API.

**Example:**

```csharp
public class SchoolDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=SchoolDB;Trusted_Connection=True;");
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne<Course>(s => s.Course)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CourseId);
    }
}
```

### Create Model Classes

- Create a class for each table in the database.
- Add properties for each column in the table.
- Add navigation properties if there is a relationship between tables.

**Example:**

```csharp
public class User
{
    [Key]
    public int Id { get; set; }

    [NotNull]
    [MaxLength(50)]
    public string Name { get; set; }

    [NotNull]
    public int Age { get; set; }
}
```

### Migrations

**Commands Using Package Manager Console:**

- Run the following command to create a migration:

```bash
Add-Migration InitialCreate
```

- Run the following command to apply the migration:

```bash
Update-Database

# To apply a specific migration
Update-Database MigrationName

# Rollback all migrations
Update-Database 0
```

- Run the following command to remove the last migration:

```bash
Remove-Migration
```

## Add Constrains on Entity Properties

### Add Constrains Using Naming Convention

- We can add constraints using naming conventions.
- In EF Core, We can define the **primary key** using the `Id` or `ClassNameId` property.
- We can define the **foreign key** using the `NavigationPropertyNameId` property.

### Add Constrains Using Data Annotations

- We can add constraints using data annotations.
- We can use data annotations like `[Key]`, `[Required]`, `[MaxLength]`, `[MinLength]`, `[Column]`, `[ForeignKey]`, etc.

**Example:**

```csharp
public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public int Age { get; set; }

    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
}
```

### Add Constrains Using Fluent API

- We can add constraints using Fluent API.
- We can use methods like `HasKey`, `Property`, `IsRequired`, `HasMaxLength`, `HasOne`, `WithMany`, `HasForeignKey`, etc.

**Example:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>()
        .HasKey(s => s.Id);

    modelBuilder.Entity<Student>()
        .Property(s => s.Name)
        .IsRequired()
        .HasMaxLength(50);

    modelBuilder.Entity<Student>()
        .Property(s => s.Age)
        .IsRequired();

    modelBuilder.Entity<Student>()
        .HasOne<Department>(s => s.Department)
        .WithMany(d => d.Students)
        .HasForeignKey(s => s.DepartmentId);
}

// Another way to configure the model
modelBuilder.Entity<Student>((entity) =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
    entity.Property(e => e.Age).IsRequired();
    entity.HasOne(e => e.Department).WithMany(d => d.Students).HasForeignKey(e => e.DepartmentId);
});
```

> [!Note]
>
> - Fluent API is very useful for configuring POCO (Plain Old CLR Objects).
> - POCOs are classes in some legacy code, and we need to use EF in this legacy code. To make those old types act like entities with constraints, we use Fluent API to configure those classes.

## Navigation Properties

- **Navigation properties** are used to define relationships between entities.
- They allow you to navigate from one entity to related entities.
- There are three types of relationships in EF Core:
  - **One-to-One**: A single entity is associated with a single related entity.
  - **One-to-Many**: A single entity is associated with multiple related entities.
  - **Many-to-Many**: Multiple entities are associated with multiple related entities.

### Example of One-to-One Relationship

**Person.cs:**

```csharp
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation Property
    public Passport Passport { get; set; }
}
```

**Passport.cs:**

```csharp
public class Passport
{
    public int Id { get; set; }
    public string PassportNumber { get; set; }

    // Foreign Key
    public int PersonId { get; set; }

    // Navigation Property
    public Person Person { get; set; }
}
```

**DbContext Configuration:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Person>()
        .HasOne(p => p.Passport)
        .WithOne(pass => pass.Person)
        .HasForeignKey<Passport>(pass => pass.PersonId);
}
```

### Example of One-to-Many Relationship

**Student.cs:**

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
}
```

**Course.cs:**

```csharp
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Student> Students { get; set; } = new HashSet<Student>();
}
```

**DbContext Configuration:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>()
        .HasOne(s => s.Course)
        .WithMany(c => c.Students)
        .HasForeignKey(s => s.CourseId);
}
```

### Example of One-to-Many Recursive Relationship

**Employee.cs:**

```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Foreign Key (Self-Referencing)
    public int? ManagerId { get; set; }

    // Navigation Properties
    public Employee Manager { get; set; } // One Manager
    public ICollection<Employee> Subordinates { get; set; } = new HashSet<Employee>(); // Many Employees
}
```

**DbContext Configuration:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>()
        .HasOne(e => e.Manager)                   // Each employee has one manager
        .WithMany(m => m.Subordinates)            // Each manager has many subordinates
        .HasForeignKey(e => e.ManagerId)          // Foreign key stored in Employee table
        .OnDelete(DeleteBehavior.Restrict);       // Prevent circular delete issues
}
```

### Example of Implicit Many-to-Many Relationship (Without a Third Entity)

if there is no extra data on relationship, So EF core can handle the **junction table** implicitly without requiring a third entity.

**Student.cs:**

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
```

**Course.cs:**

```csharp
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
```

**DbContext Configuration (No Need for Manual Configuration!):**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Courses)
        .WithMany(c => c.Students);
}
```

EF will automatically create a junction table (`StudentCourse`) with only `StudentId` and `CourseId` as composite keys.

### Example of Explicit Many-to-Many Relationship (With a Third Entity)

if the **junction table** requires extra data, So we need to create a third entity for the junction table.

**Student.cs:**

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
```

**Course.cs:**

```csharp
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
```

**StudentCourse.cs (Junction Table with Extra Data):**

```csharp
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public DateTime EnrollmentDate { get; set; }  // Extra column
    public double Grade { get; set; }  // Extra column
}
```

**DbContext Configuration:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<StudentCourse>()
        .HasKey(sc => new { sc.StudentId, sc.CourseId });

    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Student)
        .WithMany(s => s.StudentCourses)
        .HasForeignKey(sc => sc.StudentId);

    modelBuilder.Entity<StudentCourse>()
        .HasOne(sc => sc.Course)
        .WithMany(c => c.StudentCourses)
        .HasForeignKey(sc => sc.CourseId);
}
```

## Creational Strategies in EF Core

### Inheritance Mapping

- **Inheritance mapping** is used to map the inheritance hierarchy to the database.
- There are three types of inheritance mapping in EF Core:
  - **Table Per Hierarchy (TPH)**: All classes are mapped to a single table.
  - **Table Per Type (TPT)**: Each class is mapped to a separate table.
  - **Table Per Concrete Type (TPC)**: Each class is mapped to a separate table.
- EF Core by default uses `TPH`.
- So EF will create single table for the whole hierarchy and add extra column in this table, called `Discriminator` which will contains the TypeName (e.g. `Student`, `Instructor`).
- We can change strategy at `OnModelCreating` method.

  ```csharp
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      modelBuilder.Entity<Person>(entityBuilder => {
          entityBuilder.UseTptMappingStrategy(); // here we will use TPT Strategy, so we expected that we will have table for each entity class
      })
  }
  ```

## Loading Strategies of Related Data in EF Core

- **Loading strategies** are used to load related entities (Navigation Properties) in EF Core.
- **Eager Loading**: Load related entities along with the main entity.
- **Lazy Loading**: Load related entities when they are accessed.
- **Explicit Loading**: Load related entities on demand.

### Eager Loading

- **Eager loading** is used to load related entities along with the main entity.
- We use the `Include` method to load related entities.

**Usage:**

```csharp
var students = context.Students
    .Include(s => s.Course)
    .ToList();
```

### Lazy Loading

- **Lazy loading** is used to load related entities when they are accessed.
- We need to install the `Microsoft.EntityFrameworkCore.Proxies` package to enable lazy loading.
- We must mark **Navigation Properties** as **virtual**.

**Usage:**

```csharp
public class SchoolDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.;Database=SchoolDB;Trusted_Connection=True;");

}
```

```csharp
// Program.cs
var student = context.Students.Find(1);
var course = student.Course; // Lazy loading
```

### Explicit Loading

- **Explicit loading** is used to load related entities on demand.
- We use the `Entry` method to load related entities.

**Usage:**

```csharp
var student = context.Students.Find(1);
context.Entry(student).Collection(s => s.Course).Load(); // in-case student has a collection of courses
//  context.Entry(student).Reference(s => s.Course).Load(); // in-case student has single course
```

## DbContext is a Disposable Object

### What is Disposable Object

- A **disposable object** is an object that **uses system resources** (like **database connections**, **files**, or **memory**) and **must be cleaned up** when no longer needed.
- In C#, these objects **implement** the `IDisposable` interface to **free resources** and **prevent memory leaks**.

### What is `IDisposable`?

- IDisposable is an interface that provides a `Dispose()` method to release resources manually.
- **Any class that manages unmanaged resources** (like database connections or file streams) **should** implement `IDisposable`.
- **DbContext** in Entity Framework implements `IDisposable` because it holds **a connection to the database**.

### Using `using` to Dispose Automatically

Instead of calling `Dispose()` manually, we can use the `using` keyword, which **automatically disposes** of the object when it goes out of scope.

```csharp
using (var context = new MyDbContext())
{
    var students = context.Students.ToList();
} // Context is automatically disposed here
```

### How `using` Works Internally

- The `using` statement **wraps** your code in a **try-finally block**.
- In the `finally` block, it **calls the** `Dispose()` method automatically.
- This ensures that resources are **released** properly, **even if an exception occurs**.

Equivalent to:

```csharp
var context = new MyDbContext();
try
{
    var students = context.Students.ToList();
}
finally
{
    context.Dispose(); // Ensures proper cleanup
}

```

### Why is This Important?

- Prevents **memory leaks** and resource exhaustion.
- Ensures efficient resource management in applications.
- Reduces the risk of database connection issues.

## Tracking and No-Tracking Queries in EF Core

- **Tracking** is the process of keeping **track of changes** made to entities.
- **No-tracking** queries are used to **retrieve data** without tracking changes.

### Tracking Queries

- **Tracking queries** are used to **keep track of changes** made to entities.
- By default, EF Core uses tracking queries.
- We can use the `AsTracking` method to explicitly enable tracking.

**Usage:**

```csharp
var students = context.Students.AsTracking().ToList();

Console.WriteLine(context.Entry(students).State); // Output: EntityState.Unchanged
students[0].Name = "John Doe";
Console.WriteLine(context.Entry(students).State); // Output: EntityState.Modified
```

### No-Tracking Queries

- **No-tracking queries** are used to **retrieve data** without tracking changes.
- No-tracking queries are **faster** than tracking queries because they **don't keep track of changes**.
- We can use the `AsNoTracking` method to explicitly disable tracking.

**Usage:**

```csharp
var students = context.Students.AsNoTracking().ToList();
// var students = context.Students.ToList(); // By default query tracking is applied

Console.WriteLine(context.Entry(students).State); // Output: EntityState.Detached
students[0].Name = "John Doe";
Console.WriteLine(context.Entry(students).State); // Output: EntityState.Detached
```

### Apply No-Tracking Globally in EF Core

- We can **globally disable tracking** for all queries in EF Core at the `DbContext` level.
- We can use the `UseQueryTrackingBehavior` method to set the default tracking behavior.

**Usage:**

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Server=.;Database=SchoolDB;Trusted_Connection=True;")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
```

## Access SQLQuery Generated via EF Core

- **SQL queries** generated by EF Core can be **accessed** and **logged** for debugging purposes.
- We can use the `ToQueryString` method to get the SQL query generated by EF Core.

**Usage:**

```csharp
var students = context.Students
    .Where(s => s.Age > 20)
    .OrderBy(s => s.Name)
    .Select(s => new { s.Id, s.Name })
    .ToQueryString();
```

## Server vs. Client Query Processing in EF Core

- **Server query processing** is when the **database server** processes the query.
- **Client query processing** is when the **application server** (e.g. `C# App`) processes the query.

### Client Query Processing

- We can custom implemented methods inside `Select`.
- We can't use those custom user defined methods in `Where` or `OrderBy`.
  - Because EF Core can't translate them to SQL, So in this case we can use `Database View` or `Database StoredProcedure` contains our custom query and then we can use it in EF Core.

## Write Raw SQL Queries in EF Core

- **Raw SQL queries** are used to execute SQL queries directly in EF Core.
- We can use the `FromSqlRaw` method to execute raw SQL queries.

### Execute Raw SQL Queries

- **Execute raw SQL queries** using the `FromSqlRaw` method.
- We can use **parameterized queries** to prevent SQL injection.

**Usage:**

```csharp
var students = context.Students.FromSqlRaw("SELECT * FROM Students WHERE Age > {0}", 20).ToList();
```

### Execute Query for Non-Entity Types

- Sometimes, we need to execute a **raw SQL query** that returns **data not mapped to an entity** (e.g., custom DTOs or anonymous types).
- Use `context.Database.SqlQueryRaw<T>("query", ...params)`.

**Usage:**

```csharp
var students = db.Database.SqlQueryRaw<StudentDto>("SELECT Id, Name FROM Students WHERE Age >= {0}", 30);

// StudentDto
public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString() => $"Student {{Id = {Id}, Name = {Name}}}";
}
```

### Execute Stored Procedures

- **Execute stored procedures** using the `FromSqlRaw` method.
- We can use **parameterized queries** to pass parameters to the stored procedure.

**Usage:**

```csharp
var students = context.Students.FromSqlRaw("EXEC GetStudentsByAge {0}", 20).ToList();
```

> [!Note]
>
> We can also execute Stored Procedure for Non-Entity Types, like in the section [above](#execute-query-for-non-entity-types).

## Organizing Entity Configurations with Fluent API in Separate Classes

### Why Use Separate Configuration Classes?

- Keeps your **DbContext** clean and organized.
- Makes it **easier to manage** complex entity relationships.
- Improves **code readability** and **reusability**.

### How to Do It?

- **Create a separate class** for each entity's configuration.
- **Implement** `IEntityTypeConfiguration<T>` for structured mapping.
- **Apply configurations** in `OnModelCreating` of `DbContext`.

### EntityConfigurations Classes Usage

**Student Entity:**

```cs
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

**StudentEntityTypeConfiguration:**

```cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
    }
}
```

**DbContext:**

```cs
public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
    }
}
```

### Using `ApplyConfigurationsFromAssembly` Instead of Registering Each Configuration Manually

Instead of adding each configuration manually in `OnModelCreating`, we can **automatically apply all configurations** from a given assembly using `ApplyConfigurationsFromAssembly`.

#### Steps to Use `ApplyConfigurationsFromAssembly`

**Student Entity:**

```cs
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

**StudentEntityTypeConfiguration:**

```cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
    }
}
```

**DbContext:**

```cs
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

## Practical Example of EF Core

For a practical example of using EF Core using the **Database-First** approach, **Code-First** approach using **Fluent API** and **Data Annotations**, and **Entity Configurations** in separate classes, refer to the following repository: [EFCore Playground](https://github.com/m7moudGadallah/EFCorePlayground)
