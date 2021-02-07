using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}
