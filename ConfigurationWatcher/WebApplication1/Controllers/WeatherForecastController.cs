using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly ConfigurationWatcher<ClientSettings> clientSettings;
        private readonly ConfigurationWatcher<SecuritySettings> securitySetings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ConfigurationWatcher<ClientSettings> clientSettings, ConfigurationWatcher<SecuritySettings> securitySetings)
        {
            this.logger = logger;
            this.clientSettings = clientSettings;
            this.securitySetings = securitySetings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var client = clientSettings.CurrentValue;
            var security = securitySetings.CurrentValue;
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}