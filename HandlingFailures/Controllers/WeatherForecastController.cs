using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HandlingFailures.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastsService service;

        public WeatherForecastController(WeatherForecastsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int days = 3)
        {
            return service.GetForecasts(days);
        }
    }
}
