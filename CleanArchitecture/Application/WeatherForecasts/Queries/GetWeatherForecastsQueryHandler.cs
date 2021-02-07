using Application.Common;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.WeatherForecasts.Queries
{
    class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
    {
        private readonly IWeatherForecastRepository repository;

        public GetWeatherForecastsQueryHandler(IWeatherForecastRepository repository)
        {
            this.repository = repository;
        }
        
        public Task<IEnumerable<WeatherForecast>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(repository.GetWeatherForecasts());
        }
    }
}
