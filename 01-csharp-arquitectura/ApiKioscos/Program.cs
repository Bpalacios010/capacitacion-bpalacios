using DominioKioscos;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<KioscosDbContext>(opciones =>
    opciones.UseSqlite("Data Source=kioscos.db"));

builder.Services.AddScoped<IRepositorioKioscos, RepositorioEfCore>();
builder.Services.AddScoped<MonitorDeKioscos>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
