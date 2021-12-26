using EntityFrameworkCore.Data;
using EntityFrameworkCore.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.Controllers
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
        private readonly PeopleDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, PeopleDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var people = _dbContext.People;
            
            var arek = new Person
            {
                FirstName = "Arek",
                LastName = "Piznal"
            };
            
            _dbContext.People.Add(arek);    
            _dbContext.SaveChanges();

            
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