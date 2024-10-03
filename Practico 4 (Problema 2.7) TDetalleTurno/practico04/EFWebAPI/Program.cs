using EFWebAPI.Models;
using EFWebAPI.Services;
using EFWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<db_turnosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//Inyecta la conexion con un scoped.

builder.Services.AddScoped<IDetalleTurnoRepository, DetalleTurnoRepository>();
builder.Services.AddScoped<IDetalleService, DetalleService>();

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
