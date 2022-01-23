using GrpcClient;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WheaterForecasts.WheaterForecastsClient client;

        public WeatherForecastController(WheaterForecasts.WheaterForecastsClient client)
        {
            this.client = client;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var response = await client.GetForecastsAsync(new ForecastsRequest { Days = 2 });

            return new[] {
                new WeatherForecast {
                    Date = response.Date.ToDateTime(),
                    TemperatureC = response.TemperatureC,
                    Summary = response.Summary,
                }
            };
        }
    }
}