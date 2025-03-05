# 🔖 ITI - D0048 - ASP .NET API

## JsonIgnore

- `JsonIgnore` attribute is used to ignore a property or field in serialization and deserialization.
- It can uses based on condition like
  - `JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)`
  - `JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)`
  - `JsonIgnore(Condition = JsonIgnoreCondition.Never)`
  - `JsonIgnore(Condition = JsonIgnoreCondition.Always)`
- It's by default uses `JsonIgnoreCondition.Always` which means it will ignore the property in both serialization and deserialization.

```csharp
public class User
{
    public string Name { get; set; }
    public string Password { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Address { get; set; }

    [JsonIgnore]
    public string Token { get; set; }
}
```

## ASP.NET Core Attributes for Web API Controllers

- `[HttpGet], [HttpPost], [HttpPatch], [HttpPut], [HttpDelete]`: Specify the Http verb (method) for controller action.

- `[Route]`: Defines URL patterns for controllers/actions.

  ```csharp
  // [Route("api/[controller]")]
  [Route("api/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
      [HttpGet("{id}")]
      public IActionResult GetById(int id)
      {
          return Ok(new { message = $"Product {id}" });
      }
  }
  ```

- `[Bind]`: Specifies which properties should be included or excluded in model binding.

  ```csharp
  public class ProductDto
  {
      public int Id { get; set; }

      [Bind("Name,Price")] // Only Name and Price will be bound
      public string Name { get; set; }
      public decimal Price { get; set; }
      public string Description { get; set; } // Will not be bound
  }
  ```

- `[Consumes]`: Specify Allowed Request Content Type

  ```csharp
  [HttpPost]
  [Consumes("application/json")]
  public IActionResult CreateProduct([FromBody] ProductDto product)
  {
      return Ok(new { message = "Product created", product });
  }
  ```

- `[Produces]`: Specify Response Content Type

  ```csharp
  [HttpGet]
  [Produces("application/json")]
  public IActionResult GetProduct()
  {
      return Ok(new { id = 1, name = "Laptop", price = 1200.99 });
  }
  ```

### Token Replacement in Route Templates

For convenience, attribute routes support token replacement by
enclosing a token in square-braces ([, ]).

```csharp
[Route("[controller]/[action]")]
public class ProductsController:Controller
{
  [HttpGet] // endpoint `Get Products/List`
  public IActionResult List()
  {
    // ...
  }

  [HttpGet] // endpoint `Get Products/GetById`
  public IActionResult GetById(int id)
  {
    // ...
  }
}
```

### Multiple Routes

```csharp
[Route("Store")]
[Route("[controller]")]
public class ProductsController:Controller
{
  [HttpGet("ProductList")] // endpoint `Get Products/ProductList` or `Get Store/ProductList`
  [HttpGet("AllProducts")] // endpoint `Get Products/AllProducts` or `Get Store/AllProducts`
  public IActionResult List()
  {
    // ...
  }

  [HttpGet] // endpoint `Get Products/GetById`
  public IActionResult GetById(int id)
  {
    // ...
  }
}
```

## Binding Source Parameter Inference in ASP.NET Core

- `[FromBody]` - Bind Data from Request Body.

  ```csharp
  [HttpPost]
  public IActionResult CreateProduct([FromBody] ProductDto product)
  {
      return Ok(new { message = "Product created", product });
  }
  ```

  ```json
  // Request

  POST /api/products
  Content-Type: application/json

  {
    "name": "Laptop",
    "price": 1200.99
  }
  ```

- `[FromForm]` - Bind Data from Form Inputs

  ```csharp
  [HttpPost("upload")]
  public IActionResult UploadProduct([FromForm] ProductDto product, [FromForm] IFormFile image)
  {
      return Ok(new { message = "Product uploaded", product, imageName = image.FileName });
  }
  ```

  ```json
  // request

  POST /api/products/upload
  Content-Type: multipart/form-data
  ```

- `[FromHeader]` - Bind Data from Request Headers.

  ```csharp
  [HttpGet]
  public IActionResult GetUser([FromHeader] string Authorization)
  {
      return Ok(new { message = "Token received", token = Authorization });
  }
  ```

  ```json
  // request

  GET /api/user
  Authorization: Bearer abc123
  ```

- `[FromQuery]` - Bind Data from Query String Parameters.

  ```csharp
  [HttpGet]
  public IActionResult GetProducts([FromQuery] int page, [FromQuery] int pageSize)
  {
      return Ok(new { message = "Products retrieved", page, pageSize });
  }
  ```

  ```json
  // request

  GET /api/products?page=1&pageSize=10
  ```

- `[FromRoute]` - Bind Data from Route Parameters.

  ```csharp
  [HttpGet("{id}")]
  public IActionResult GetProductById([FromRoute] int id)
  {
      return Ok(new { message = $"Product {id} retrieved" });
  }
  ```

  ```json
  // request

  GET /api/products/5
  ```

- `[ExcludeFromDescription]` - Hide from API Documentation (OpenAPI docs).

  ```csharp
  [HttpGet("internal-data")]
  public IActionResult GetInternalData([FromHeader] string Authorization,
                                      [ExcludeFromDescription] string internalSecret)
  {
      if (internalSecret != "SuperSecretKey")
          return Unauthorized(new { message = "Access Denied" });

      return Ok(new { message = "Internal Data Accessed" });
  }
  ```

  ```json
  // request

  GET /api/internal-data
  Authorization: Bearer xyz123
  internalSecret: SuperSecretKey
  ```

## DTOs (Data Transfer Objects)

- Data Transfer Object (DTO) is a way to build data models which shape the data that is transferred between the client and the server.
- DTOs give us flexibility to show only the data that is needed by the client and hide the data that is not needed.
- DTOs give us flexibility to set custom validation rules for the data that is transferred between the client and the server.
- DTOs help us to avoid Referencing Loops and prevent Serialization issues.
  - Like Falling into infinite cycles of loading navigation properties by lazy loading in Entity Framework through serialization.

```csharp
public class UserDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email {get; set;}

    [Required]
    [MaxLength(100)]
    public string Address { get; set; }
}
```

### Use DTOs in ASP .NET API

- Best practice is to create a separate folder for DTOs.
- Create sub-folder for each controller and put DTOs in that folder.

```plaintext
├──DTOs
│  ├──Users
│  │  ├──GetUserDto.cs
│  │  ├──CreateUserDto.cs
│  │  ├──...
```

## AutoMapper

- AutoMapper is a library that helps to map one object to another.
- It is used to map DTOs to Models and Models to DTOs.

### Install AutoMapper

#### Using NuGet Package Manager

- Right click on the project and select `Manage NuGet Packages`.
- Search for `AutoMapper` and click on `Install`.

**OR Using Package Manager CLI:**

```bash
Install-Package AutoMapper
```

#### Using .NET CLI

```bash
dotnet add package AutoMapper
```

### AutoMapper Configuration (Profile)

- Create a new class `AutoMapperProfile` and inherit from `Profile` class.
- Create a constructor and use `CreateMap` method to map DTOs to Models and Models to DTOs.

  ```csharp
  public class AutoMapperProfile : Profile
  {
      public AutoMapperProfile()
      {
          CreateMap<User, UserDto>().ReverseMap();
      }
  }
  ```

- Add `AutoMapperProfile` class in `Program.cs`.

  ```csharp
  builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
  ```

- Inject `IMapper` in the controller.

  ```csharp
  private readonly IMapper _mapper;

  public UsersController(IMapper mapper)
  {
      _mapper = mapper;
  }
  ```

- Use `_mapper.Map` method to map DTOs to Models and Models to DTOs.

  ```csharp
  var user = _mapper.Map<User>(userDto);
  ```

### Multiple AutoMapper Profiles

- We can create multiple profiles for different mappings.
- We can use `CreateMap` method to map DTOs to Models and Models to DTOs.

  ```csharp
  public class AutoMapperProfile : Profile
  {
      public AutoMapperProfile()
      {
          CreateMap<User, UserDto>().ReverseMap();
          CreateMap<Product, ProductDto>().ReverseMap();
      }
  }
  ```

- OR we can separate the mappings in different classes.

  ```csharp
  // AutoMapperProfiles/UserAutoMapperProfile.cs
  public class UserAutoMapperProfile : Profile
  {
      public UserAutoMapperProfile()
      {
          CreateMap<User, UserDto>().ReverseMap();
      }
  }

  // AutoMapperProfiles/ProductAutoMapperProfile.cs
  public class ProductAutoMapperProfile : Profile
  {
      public ProductAutoMapperProfile()
      {
          CreateMap<Product, ProductDto>().ReverseMap();
      }
  }

  // Program.cs
  builder.Services.AddAutoMapper(typeof(UserAutoMapperProfile), typeof(ProductAutoMapperProfile));
  ```

**🌟 Quick Hack to Register all AutoMapper Profile Once**:

```csharp
builder.Services.AddAutoMapper(typeof(Program).Assembly);
```

### Mapping Different Properties Names

- if there is a property in the destination object that is not present in the source object, AutoMapper will set the value to `null`.
- we can map different properties names using `AfterMap` method.

  ```csharp
  CreateMap<User, UserDto>()
      .ReverseMap()
      .AfterMap((src, dest) => dest.FullName = $"{dest.FirstName} {dest.LastName}");
  ```

### Custom Property Mapping with AutoMapper

- We can use `ForMember` method to map properties with different names.

  ```csharp
  CreateMap<User, UserDto>()
      .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
  ```

- We can use `ResolveUsing` method to map properties with custom logic.

  ```csharp
  CreateMap<User, UserDto>()
      .ForMember(dest => dest.FullName, opt => opt.ResolveUsing(src => $"{src.FirstName} {src.LastName}"));
  ```

- We can use `AfterMap` method to map properties with custom logic after mapping.

  ```csharp
  CreateMap<User, UserDto>()
      .AfterMap((src, dest) => dest.FullName = $"{src.FirstName} {src.LastName}");
  ```

- We can use `BeforeMap` method to map properties with custom logic before mapping.

  ```csharp
  CreateMap<User, UserDto>()
      .BeforeMap((src, dest) => src.FirstName = src.FirstName.ToUpper());
  ```

### 🌟 Apply Partial Updates with AutoMapper

**Scenario**: we want to update only the properties that are not null in the DTO.

```csharp
// Models/Student.cs
public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
}


// DTOs/Students/UpdateStudentRequestDto.cs
public class UpdateStudentRequestDto
{
    [MinLength(2)]
    [MaxLength(50)]
    public string? Name { get; set; }

    [MinLength(5)]
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(5)]
    [MaxLength(30)]
    public string? Password { get; set; }
}

// Controllers/StudentsController.cs
[Route("api/[controller]")]
[ApiController]
public class StudentsController(IRepositoryBase<Student> _studentsRepo, IMapper _mapper) : ControllerBase
{
    [Route("{id:int}")]
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateStudentRequestDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _studentsRepo.GetById(id);

        if (existing == null)
        {
            return NotFound($"Student with id [{id}] is not found!");
        }

        _mapper.Map(dto, existing);
        await _studentsRepo.Update(existing);
        await _studentsRepo.Save();

        return NoContent();
    }
}

// AutoMapperProfiles/StudentAutoMapperProfile.cs
using AutoMapper;

public class StudentAutoMapperProfile:Profile
{
    public StudentAutoMapperProfile()
    {
        CreateMap<Student, GetStudentResponseDto>();
        CreateMap<CreateStudentRequestDto, Student>();
        CreateMap<UpdateStudentRequestDto, Student>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id to prevent overwriting
                    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Only map non-null values
    }
}
```

#### ⚠ Warning: Handling Nullable Value Types in AutoMapper Partial Updates

AutoMapper's `.ForAllMembers(opt => opt.Condition(...))` does not correctly handle nullable value types (`int?`, `double?`, etc.) by default. Even if `null`, these properties may still be mapped and set to their default values (e.g., `0` for `int`).

**Solution:**

```csharp
CreateMap<UpdateStudentRequestDto, Student>()
    .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.Age, opt => opt.Condition(src => src.Age.HasValue)) // Explicit handling
    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // General rule for reference types
```

## Web API Pagination

- Pagination is a technique to divide the data into multiple pages.
- It is used to reduce the load time of the page by dividing the data into multiple pages.

**Example:**

```csharp
[HttpGet]
public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
{
    var query = _articles.ToList();
    var totalCount = query.Count();
    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
    query = query.Skip((page - 1) * pageSize).Take(pageSize);
    return Ok(query);
}
```

## CORS (Cross-Origin Resource Sharing)

- CORS is a security feature that allows us to restrict the resources that can be requested from another domain.
- When we make a request from one domain to another domain, the browser will make a quick request to the server to check if the server allows the request.
- If the server allows the request, the browser will make the actual request, otherwise it will block the request.

### Enable CORS in ASP .NET API

- Define string variable as Cors policy in the `Program.cs`.

  ```csharp
  private const string CorsPolicy = "CorsPolicy";
  ```

- Register the CORS service in the `Program.cs`.

  ```csharp
  builder.Services.AddCors(options =>
  {
      options.AddPolicy(CorsPolicy, builder =>
      {
        // AllowAnyOrigin() - Allow any origin to make requests.
          builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();

          // WithOrigins() - Allow specific origin to make requests.
          // builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
      });
  });
  ```

- Add **UseCors** middleware in the `Program.cs`.

  ```csharp
  app.UseCors(CorsPolicy);
  ```

[← Prev](./iti-d0047-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0049-asp-api.md)
