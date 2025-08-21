using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sesotho_Readability_Tool.Services;

var builder = WebApplication.CreateBuilder(args);

// Register MVC
builder.Services.AddControllersWithViews();

// Register ReadabilityService
builder.Services.AddSingleton<ReadabilityService>(provider =>
    new ReadabilityService("Data/Final list.txt"));

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();

// MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
