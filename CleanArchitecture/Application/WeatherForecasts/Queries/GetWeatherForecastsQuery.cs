using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.WeatherForecasts.Queries
{
    public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>> { }
}
