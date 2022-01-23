using GrpcClient;

namespace Api
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public static WeatherForecastDto FromWeatherForecast(WeatherForecast forecast)
        {
            return new WeatherForecastDto
            {
                Date = forecast.Date.ToDateTime(),
                TemperatureC = forecast.TemperatureC,
                Summary = forecast.Summary,
            };
        }

    }
}