using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeopleDbContext>(options => options.UseInMemoryDatabase("People"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 * Todo:
 * + dodanie in memory db pozwalaj¹ce na zapis do bazy
 * - dodanie obs³ugi sql server
 * - dodanie obs³ugi sql server z u¿yciem docker-compose
 * - dodanie wsparcia dla postrgres
 */