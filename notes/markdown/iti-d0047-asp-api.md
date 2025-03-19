# 🔖 ITI - D0047 - ASP .NET API

## Overview

- API is an acronym for Application Programming Interface.
- API is an interface for some functionality which allow users to access specific app features or data.
- Web API is an API over the web which can be accessed using HTTP protocol.
- Web APIs Can be built using different technologies like Java, .Net, etc...
- There are many API Architectural Styles like `REST`, `SOAP`, `RPC`, `GraphQL`, etc.

## REST APIs

- REST (Representational State Transfer) is an architectural style for designing and building Web APIs.
- **Stateless** - Each request is independent and self-contained.
- Uses standard **HTTP methods** (`GET`, `POST`, `PUT`, `DELETE`).
- Resources are identified by **URIs** (e.g. `/products`).
- Uses **JSON** data format for data exchange.
- Uses **HTTP status codes** to indicate the status of the request (e.g. `200 OK`, `404 Not Found`, `500 Internal Server Error`).

_Review the [HTTP Protocol](./iti-d0040-asp-mvc.md#http) notes for more information._

### Common HTTP Methods

- **GET** - Retrieve a resource. return **200 OK**.
- **POST** - Create a new resource. return **201 Created**.
- **PUT** - Replace an existing resource. return **200 OK** or **204 No Content**.
- **DELETE** - Delete an existing resource. return **200 OK** or **204 No Content**.
- **PATCH** - Update an existing resource. return **200 OK** or **204 No Content**.

### Common HTTP Status Codes

- **200** - OK
- **201** - Created
- **204** - No Content
- **3xx** - Redirection
- **400** - Bad Request
- **401** - Unauthorized
- **404** - Not Found
- **405** - Method Not Allowed
- **415** - Unsupported Media Type
- **500** - Internal Server Error

### Serialization & Deserialization

- **Serialization** is the process of converting an object (e.g. a class instance) into a format that can be transferred over the network (e.g. JSON, XML).
- **Deserialization** is the process of converting a serialized object back into an object (e.g. a class instance).

## Intro to ASP .NET Web API

- ASP .NET Web API is a framework for building RESTful HTTP services.
- ASP .NET Web API is built on top of ASP .NET and supports ASP .NET request/response pipeline.
- ASP .NET API maps HTTP verbs to method names.
- Support Data Format: JSON, XML, BSON and Custom.

### ASP .NET Web API Key Components

- **Controller** - Handles incoming HTTP requests.
- **Routing** - Maps HTTP requests to controller actions.
- **Model Binding** - Binds incoming data to model classes.
- **Model Validation** - Validates model classes using Data Annotations.
- **Security** - Supports authentication and authorization.

## Controllers in ASP .NET Web API

### Controller Class Key Points

- **Controllers** handle incoming HTTP requests and return HTTP responses.
- **Controller** classes inherit from the `ControllerBase` class.
- **Controller** classes are decorated with the `[ApiController]` attribute which enables API-specific features like:
  - Attribute routing.
  - Automatic HTTP 400 responses.
  - Binding source parameter inference.
  - Multipart/form-data request handling.
  - Problem details for error status codes.
- **Controller** classes are decorated with the `[Route]` attribute to define the route template.
- **Controller** classes contain **action methods** that handle HTTP requests.

### Action Method Key Points

- **Action methods** are decorated with HTTP method attributes (`[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`, `[HttpPatch]`).
- **Action methods** return `ActionResult` or `IActionResult` objects.
- **Action methods** can return different types of responses (e.g. `Ok`, `NotFound`, `BadRequest`, `Created`, `NoContent`, `CreatedAtAction`, `StatusCode`, ...).
- **Action methods** can accept parameters from the `Route`, `query string`, `request body`, or `headers`.
- **Action methods** can be decorated with `[Route]` attribute to define the route template for the action.
- **Action methods** parameters can be decorated with `[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromHeader]` attributes.

> [!NOTE]
>
> `CreatedAtAction` - Returns an `HTTP 201 Created` response with a location header.

## Route Templates in ASP .NET Web API

- Route templates are used to define the URI pattern for the controller. (e.g. `Route("api/[controller]")`)
- Route templates can include placeholders for parameters. (e.g. `Route("api/[controller]/{id}")`)
- Route templates can include constraints for parameters. (e.g. `Route("api/[controller]/{id:int}")`)
  - Supported constraints include `int`, `bool`, `datetime`, `decimal`, `double`, `float`, `guid`, `long`, `min`, `max`, `range`, `regex`, `alpha`, etc...
- Route templates can include default values for parameters. (e.g. `Route("api/[controller]/{id:int=1}")`)
- Route templates can include optional parameters. (e.g. `Route("api/[controller]/{id:int?}")`)

## Validation in ASP .NET Web API

- At first we add validation criteria to the model class using Metadata Validation Attributes.
  - `[Required]` - Specifies that a property must have a value.
  - `[StringLength]` - Specifies the maximum and minimum length of a string property.
  - `[Range]` - Specifies the range of values for a numeric property.
  - `[RegularExpression]` - Specifies a regular expression pattern for a string property.
  - `[Compare]` - Specifies that a property must have the same value as another property.
  - `[EmailAddress]` - Specifies that a property must be a valid email address.
  - `[Phone]` - Specifies that a property must be a valid phone number.
    - `[CreditCard]` - Specifies that a property must be a valid credit card number.
    - `[Url]` - Specifies that a property must be a valid URL.
    - `[DataType]` - Specifies the data type of a property.
    - `Exclude` - Specifies that a property should not be bound to a model.
- Then we use the `ApiController` class to perform model validation using the `ModelState.IsValid` property.
- If the model is not valid, we return a `BadRequest` response with the model state errors.

```csharp
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
```

## API Controller Example in ASP .NET Web API

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productRepository.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await _productRepository.CreateProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        await _productRepository.UpdateProduct(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productRepository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        await _productRepository.DeleteProduct(product);
        return NoContent();
    }
}
```

## OpenAPI Documentation in ASP .NET Web API

- **OpenAPI** is a specification (programming language agnostic) for documenting HTTP APIs.
- ASP .NET supports OpenAPI integration using some open-source libraries.
- So automatically generate OpenAPI documentation for your Web API in JSON format which contains all the API endpoints, request/response models, etc.
- OpenAPI generated JSON file can be accessed using the `/openapi/v1.json` endpoint.
- You can use Swagger UI to visualize and interact with the API endpoints.

### Integrate Swagger with ASP .NET Web API

1. Install the `Swashbuckle.AspNetCore` NuGet package.
2. Add the Swagger middleware to the `Program.cs` file.

   ```csharp
   app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
   ```

3. Configure `launchSettings.json` to run the Swagger UI.

   ```json
    // Properties/launchSettings.json
     "profiles": {
          "http": {
                "commandName": "Project",
                "dotnetRunMessages": true,
                "launchBrowser": true, // set this to true
                "launchUrl": "swagger", // set launchUrl to swagger
                "applicationUrl": "http://localhost:<port>",
                "environmentVariables": {
                 "ASPNETCORE_ENVIRONMENT": "Development"
                }
          }
   ```

4. Run the application and navigate to `https://localhost:<port>/swagger`.

## C Sharp API Client

- You can use the `HttpClient` class to send HTTP requests to a Web API.
- The `HttpClient` class is a part of the `System.Net.Http` namespace.
- Use `Microsoft.AspNet.WebApi.Client` to simplify the process of sending HTTP requests to a Web API from a C# client application.

### Steps to Create a C# API Client

1. Create simple C# console application.
2. Install the `Microsoft.AspNet.WebApi.Client` NuGet package.
3. Create an instance of the `HttpClient` class.
4. Use the `GetAsync`, `PostAsync`, `PutAsync`, `DeleteAsync` methods to send HTTP requests.
5. Create class models to represent the request and response data (e.g. `Product`).
6. Use the `ReadAsAsync` method to deserialize the response content.

**Example:**

```csharp
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("https://localhost:5168/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    HttpResponseMessage response = await client.GetAsync("api/products");
    if (response.IsSuccessStatusCode)
    {
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id} - {product.Name} - {product.Price}");
        }
    }
}
```

## Glossary

- **API** (Application Programming Interface): a set of rules that allows one software application to interact with another.
- **REST** (Representational State Transfer): an architectural style for designing web APIs.
- **CRUD**: Create, Read, Update, Delete (operations).
- **HTTP** (Hypertext Transfer Protocol): Application layer protocol for transmitting hypermedia documents.
- **URI**: Uniform Resource Identifier (URL + URN)
- **URL**: Uniform Resource Locator
- **URN**: Uniform Resource Name
- **JSON** (JavaScript Object Notation): a lightweight data-interchange format.
- **XML** (Extensible Markup Language): a markup language that defines a set of rules for encoding documents.
- **SOAP** (Simple Object Access Protocol): a protocol for exchanging structured information in the implementation of web services.
- **RPC** (Remote Procedure Call): a protocol that one program can use to request a service from a program located on another computer.
- **GraphQL** (Query Language for APIs): an architectural style for designing web APIs.
- **Serialization**: the process of converting an object into a format that can be transferred over the network.
- **Deserialization**: the process of converting a serialized object back into an object.
- **Endpoint** - URL that an API exposes for clients to interact with.

[← Prev](./iti-d0052-asp-mvc.md) | [🏠 Index](../../README.md#index) | [Next →](./iti-d0048-asp-api.md)
