using HandlingFailures.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HandlingFailures
{
    public class WeatherForecastsService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> GetForecasts(int days)
        {
            if(days <= 0 || days >= 120)
            {
                throw new InvalidNumberOfDaysException(days);
            }

            var rng = new Random();
            return Enumerable.Range(1, days).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
