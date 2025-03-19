using CubeService.Middleware;
using CubeService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<SharedError>();
builder.Services.AddTransient<CubeService.Pipeline.ErrorHandlingMiddleware>();
//Adds a singleton for the cube puzzle, ensures only one is given
builder.Services.AddScoped<LibNetCube.CubePuzzle>();

builder.Services.AddControllers(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
    options.Filters.Add<ActionErrorHandlingFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
