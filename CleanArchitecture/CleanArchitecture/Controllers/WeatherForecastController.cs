using Api.Dto;
using Application.WeatherForecasts.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public WeatherForecastController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastDto>> Get()
        {
            var result = await mediator.Send(new GetWeatherForecastsQuery());

            return mapper.Map<IEnumerable<WeatherForecastDto>>(result);

        }
    }
}
