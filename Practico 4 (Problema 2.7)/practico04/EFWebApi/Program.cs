using EFWebApi.Models;
using EFWebApi.Data.Repositories;
using EFWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<db_TurnosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITurnosRepository, TurnosRepository>(); //crea una nueva instancia por cada request
//builder.Services.AddTransient<ITurnoService, TurnoService>();//crea siempre una instancia nueva
//builder.Services.AddSingleton<ITurnoService, TurnoService>();//crea una sola instancia para toda la aplicacion
builder.Services.AddScoped<ITurnoService, TurnoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
