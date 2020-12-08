using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IAuthorizationService authorizationService, ILogger<WeatherForecastController> logger)
        {
            _authorizationService = authorizationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get(Guid userId)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, userId, "ResourceBasedPolicy");
            if(!isAuthorized.Succeeded)
            {
                return Forbid();
            }

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
