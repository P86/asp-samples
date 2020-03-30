using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForwardHeaders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            //this settings will override httpContext.Request.Sheme & httpContext.Request.Host with values passed in headers:
            //- X-Forwarded-Proto
            //- X-Forwarded-Host
            // more informations: https://docs.microsoft.com/pl-pl/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-3.1
            //Test script:
            //Invoke-WebRequest http://localhost:5000 -Headers @{'X-Forwarded-Host' = 'test.home.com'; 'X-Forwarded-Proto' = 'https'}
            app.UseForwardedHeaders(new ForwardedHeadersOptions{ ForwardedHeaders = ForwardedHeaders.All });
            
            app.Run(async context => { await context.Response.WriteAsync($"Scheme: {context.Request.Scheme}; Host: {context.Request.Host}");});
        }
    }
}
