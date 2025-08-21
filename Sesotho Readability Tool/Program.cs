using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Models.Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Services;

var builder = WebApplication.CreateBuilder(args);

// Register service with file path to your list of common Sesotho words
builder.Services.AddSingleton<ReadabilityService>(provider =>
    new ReadabilityService("Data/Final list.txt"));

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sesotho Readability API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.MapPost("/api/readability/dci", (InputModel input, ReadabilityService service) =>
{
    var result = service.CalculateDCI(input.Text);
    return Results.Ok(result);
})
.WithName("CalculateDCI")
.WithOpenApi();

app.Run();
