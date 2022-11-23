using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ClientSettings>(builder.Configuration.GetRequiredSection(nameof(ClientSettings)));
builder.Services.AddSingleton<ConfigurationWatcher<ClientSettings>>();

builder.Services.Configure<SecuritySettings>(builder.Configuration.GetRequiredSection(nameof(SecuritySettings)));
builder.Services.AddSingleton<ConfigurationWatcher<SecuritySettings>>();

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
