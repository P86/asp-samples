using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRExample.Requests
{
    public class GetWeatherForecasts: IRequest<IEnumerable<WeatherForecast>>{}
}
