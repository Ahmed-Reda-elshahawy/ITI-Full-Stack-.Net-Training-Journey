using ApiDemo.Database;
using ApiDemo.Filters;
using ApiDemo.UnitOfWorks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext
builder.Services.AddDbContext<ITIDbContext, ITIDbContext>(opt
        => opt.UseLazyLoadingProxies().UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register AutoMapper Profiles

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ModelStateValidationFilter)))
    .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
