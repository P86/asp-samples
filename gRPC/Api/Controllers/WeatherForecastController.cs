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
        public async Task<IEnumerable<WeatherForecastDto>> Get(int days)
        {
            var response = await client.GetForecastsAsync(new ForecastsRequest { Days = days });

            return response.WeatherForecasts.Select(forecast => WeatherForecastDto.FromWeatherForecast(forecast));
        }
    }
}