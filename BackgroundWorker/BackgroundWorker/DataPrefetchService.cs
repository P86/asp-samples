using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class DataPrefetchService : BackgroundService
    {
        private readonly ILogger<DataPrefetchService> logger;
        private readonly WeatherForecastsProviderService forecastsProvider;

        public DataPrefetchService(ILogger<DataPrefetchService> logger, WeatherForecastsProviderService forecastsProvider)
        {
            this.logger = logger;
            this.forecastsProvider = forecastsProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("DataPrefetchService is starting");
            stoppingToken.Register(() => { logger.LogDebug("DataPrefetchService is stopping. Cancellation was requested."); });

            while(!stoppingToken.IsCancellationRequested)
            {
                logger.LogDebug("DataPrefetchService is performing work");

                forecastsProvider.WeatherForecasts = GetWeatherForecasts();

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            logger.LogDebug("DataPrefetchService is stopping");
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //let's pretend that this method loads data from database or external service using http callls
        private IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
