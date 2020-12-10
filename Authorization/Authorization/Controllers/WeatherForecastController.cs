using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IAuthorizationService _authorizationService;
        
        public WeatherForecastController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWithResourceBasedAuth([FromQuery]int userId = 506)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, userId, "ResourceBasedPolicy");
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return GetForecasts();
        }

        [HttpGet("{userId}")]
        [AuthorizeUser("userId")]
        public ActionResult<IEnumerable<WeatherForecast>> GetWithAttribute(int userId)
        {
            return GetForecasts();
        }

        private static ActionResult<IEnumerable<WeatherForecast>> GetForecasts()
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
