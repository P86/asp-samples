/* todo:
 * - create proto for weather forecasts 
 * - move weather forecassts to GrpcService
 * - add logging interceptor for grpc calls
 * - add authentication for grpc calls
 * - run this in containers using docker compose
 */

using GrpcClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<WheaterForecasts.WheaterForecastsClient>(o => o.Address = new Uri("https://localhost:7162"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.Services.AddGrpcClient

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
