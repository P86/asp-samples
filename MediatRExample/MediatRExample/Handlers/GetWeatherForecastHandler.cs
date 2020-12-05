using MediatR;
using MediatRExample.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRExample.Handlers
{
    public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecasts, IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GetWeatherForecastHandler> logger;

        public GetWeatherForecastHandler(ILogger<GetWeatherForecastHandler> logger)
        {
            this.logger = logger;
        }

        public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
        {
            logger.LogDebug($"{nameof(GetWeatherForecastHandler)} has been exectued");
            
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            return Task.FromResult(result);
        }
    }
}
