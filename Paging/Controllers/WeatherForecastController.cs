using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Paging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class WeatherForecastController : ControllerBase
    {
        private readonly IForecastRepository repository;
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(IForecastRepository repository, ILogger<WeatherForecastController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        //idea: add attribute AddpaginationMetadata and handle metadata creation in middleware, here return PagedList<T> only
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery] ForecastParams parameters)
        {
            var result =  PagedList<WeatherForecast>.Create(repository.All, parameters.Page, parameters.Size);

            var paginationMetadata = new
            {
                currentPage = result.CurrentPage,
                pageSize = result.PageSize,
                totalCount = result.TotalCount,
                totalPages = result.TotalPages,
                previousPageLink = result.HasPrevious ? CreateResourceUri(parameters, ResourceUriType.Previous) : null,
                nextPageLink = result.HasNext ? CreateResourceUri(parameters, ResourceUriType.Next) : null,
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(result);
        }

        private string CreateResourceUri(ForecastParams parameters, ResourceUriType type) => (type) switch 
        {
            ResourceUriType.Previous => Url.Link("WeatherForecast", new {
                page = parameters.Page - 1,
                size = parameters.Size
            }),
            ResourceUriType.Next => Url.Link("WeatherForecast", new {
                page = parameters.Page + 1,
                size = parameters.Size
            }),
            _ => Url.Link("WeatherForecast", new {
                page = parameters.Page,
                size = parameters.Size
            })
        };
    }
}
