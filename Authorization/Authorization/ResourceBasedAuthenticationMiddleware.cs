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
            if (endpoint != null)
            {
                var authorizeUser = endpoint.Metadata.GetMetadata<AuthorizeUserAttribute>();
                if(authorizeUser != null)
                {
                    if (httpContext.Request.Query.TryGetValue(authorizeUser.ParameterName, out var userId))
                    {

                    }
                    else
                    {
                        return httpContext.Response.
                    }
                }
            }
            
            await _next(httpContext);
        }
    }
}
