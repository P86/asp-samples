using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService.Services
{
    public class WeatherForecastsService: WheaterForecasts.WheaterForecastsBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<WeatherForecastResponse> GetForecasts(ForecastsRequest request, ServerCallContext context)
        {
            var forecasats = Enumerable.Range(1, request.Days).Select(index => new WeatherForecast
            {
                Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            });

            var respose = new WeatherForecastResponse();
            respose.WeatherForecasts.AddRange(forecasats);

            return Task.FromResult(respose);
        }
    }
}

