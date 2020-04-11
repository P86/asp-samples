using System.Collections.Generic;

namespace Paging.Controllers
{
    public interface IForecastRepository
    {
        IEnumerable<WeatherForecast> All { get; }
    }
}