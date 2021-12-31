using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeopleDbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;User ID=sa;Password=zaq12wsx!;Database=People;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

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
 * + dodanie obs³ugi sql server
 * - dodanie bardziej skomplikowanej struktury danych 
 * - sprawdzenie jak dzia³a skomplikowane query
 * - dodanie obs³ugi sql server z u¿yciem docker-compose
 * - zapoznanie siê z dzia³aniem migracji
 * - dodanie wsparcia dla postgres
 */


//run sql server in container docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=zaq12wsx!" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-CU14-ubuntu-20.04
//sql server connection string Data Source=127.0.0.1,1433;User ID=sa;Password=zaq12wsx!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False