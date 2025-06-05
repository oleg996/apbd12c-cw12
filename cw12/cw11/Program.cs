using Microsoft.EntityFrameworkCore;
using cw11.data;
using cw11.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MasterContext>();

// Wstrzykiwanie zależności
// https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();