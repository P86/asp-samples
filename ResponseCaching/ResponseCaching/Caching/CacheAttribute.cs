using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResponseCaching.Caching
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)
        {
            this.timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var key = GetCacheKey(context.HttpContext.Request);
            //memory cache should be wrapped into service
            var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            if(cache.TryGetValue(key, out var value))
            {
                context.Result = new ContentResult
                {
                    Content = JsonSerializer.Serialize(value),
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var response = await next();
            if(response.Result is ObjectResult okObjectResult && okObjectResult.Value != null)
            {
                cache.Set(key, okObjectResult.Value, TimeSpan.FromSeconds(timeToLiveInSeconds));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            return request.Path; //should be path and all query parameters
        }
    }
}
