using System;
using System.Collections.Generic;

namespace Paging.Controllers
{
    internal class ForecastRepository : IForecastRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> All
        {
            get
            {
                var rng = new Random();
                for (int i = 0; i < 10_000; i++)
                {
                    yield return new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(i),
                        TemperatureC = 20,
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    };
                }
            }
        }
    }
}
