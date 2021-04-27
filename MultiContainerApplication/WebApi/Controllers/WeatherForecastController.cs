using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var database = connectionMultiplexer.GetDatabase();
            var result = await database.StringGetAsync("forecasts");
            if(result == RedisValue.Null)
            {
                var forecasts = GenerateForecasts();
                await database.StringSetAsync("forecasts", JsonSerializer.Serialize(forecasts), TimeSpan.FromSeconds(10));
                return forecasts;
            }
            
            
            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(result.ToString());
        }

        private static IEnumerable<WeatherForecast> GenerateForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
