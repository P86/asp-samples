using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Authorization
{
    public class ResourceBasedAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationService _authorizationService;

        public ResourceBasedAuthenticationMiddleware(RequestDelegate next, IAuthorizationService authorizationService)
        {
            _next = next;
            _authorizationService = authorizationService;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<AuthorizeUserAttribute>();
            if (attribute != null)
            {
                if (httpContext.Request.RouteValues.TryGetValue(attribute.ParameterName, out var userId))
                {
                    var result = await _authorizationService.AuthorizeAsync(httpContext.User, int.Parse(userId.ToString()), "ResourceBasedPolicy");
                    if (result.Succeeded)
                    {
                        await _next(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
