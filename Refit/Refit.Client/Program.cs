// See https://aka.ms/new-console-template for more information
using Refit;

Console.WriteLine("Hello, World!");

var weatherForecastApi = RestService.For<IWeatherForecastApi>("https://localhost:7282/");
var forecasts = await weatherForecastApi.GetWeatherForecastAsync(15);

var externalWeatherApi = RestService.For<IExternalWeatherApi>("http://api.weatherapi.com/v1");
var currentWeeather = await externalWeatherApi.GetCurrentWeatherAsync("Warsaw");

Console.WriteLine(forecasts);

internal interface IExternalWeatherApi
{
    [Get("/current.json?q={city}&key=<provide key here>")]
    Task<Weather> GetCurrentWeatherAsync(string city);
}

public class Weather
{
    public class LocationData
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }

    public class CurrentData
    {
        public decimal temp_c { get; set; }
        public decimal temp_f { get; set; }
        public string last_updated{ get; set; }
    }

    public LocationData Location { get; set; }

    public CurrentData Current { get; set; }
}

interface IWeatherForecastApi
{
    [Get("/WeatherForecast?count={count}")]
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync(int count = 10);
}

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}