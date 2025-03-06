using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DumbDbContext>((options)
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.Use(async (httpCx, next) =>
{
    Stopwatch watch = new();
    watch.Start();
    if (string.IsNullOrEmpty(httpCx.Request.Headers.RequestId))
    {
        httpCx.Request.Headers.RequestId = Guid.NewGuid().ToString();
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Incoming Request: {{RequestId = {httpCx.Request.Headers.RequestId}, Path = {httpCx.Request.Path}");
    Console.ForegroundColor = ConsoleColor.Gray;
    await next();
    watch.Stop();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Outgoing Response: {{RequestId = {httpCx.Request.Headers.RequestId},  Path = {httpCx.Request.Path}, Time Taken = {watch.ElapsedMilliseconds}ms}}");
    Console.ForegroundColor = ConsoleColor.Gray;
});

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
