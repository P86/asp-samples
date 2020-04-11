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
        public PagedList<WeatherForecast> Get([FromQuery] ForecastParams parameters)
        {
            return PagedList<WeatherForecast>.Create(repository.All, parameters.Page, parameters.Size);
        }
    }
}
