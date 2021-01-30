using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BackgroundWorker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastsProviderService service;
        
        public WeatherForecastController(WeatherForecastsProviderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return service.WeatherForecasts;
        }
    }
}
