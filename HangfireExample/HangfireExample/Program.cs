using Hangfire;
using HangfireExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SomeService>();

//commented code is about using sql server to store jobs information
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseInMemoryStorage());
//.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
//{
//    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//    QueuePollInterval = TimeSpan.Zero,
//    UseRecommendedIsolationLevel = true,
//    DisableGlobalLocks = true
//}));

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

app.MapControllers();

app.Run();
