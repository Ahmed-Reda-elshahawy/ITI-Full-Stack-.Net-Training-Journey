using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmailExistenceService, EmailExistenceService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddDbContext<MVCD03DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCD03DB"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
