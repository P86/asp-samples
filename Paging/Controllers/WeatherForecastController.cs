using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Paging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class WeatherForecastController : ControllerBase
    {
        private readonly IForecastRepository repository;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(IForecastRepository repository, ILogger<WeatherForecastController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] ForecastParams parameters)
        {
            return repository.All
                .Skip(parameters.Page * parameters.Size)
                .Take(parameters.Size);
        }
    }
}
