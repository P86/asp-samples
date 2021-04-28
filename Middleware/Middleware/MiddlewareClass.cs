using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Middleware
{
    public class MiddlewareClass
    {
        private readonly RequestDelegate _next;

        public MiddlewareClass(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Second middleware -before call next");
            await _next(context);
            Console.WriteLine("Second middleware - after call next");
        }
    }
}
