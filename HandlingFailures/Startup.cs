using HandlingFailures.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HandlingFailures
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
            services.AddScoped<WeatherForecastsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //typically is used like that in production mode
            app.UseExceptionHandler(builder =>
                builder.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                    if(error is InvalidDataException) //translate domain exception to http error code
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(error.Message);
                        logger.LogInformation(error.Message);   
                    }
                    else //handle rest of exceptions
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Custom error message returned to the user");
                        logger.LogError(error, "An exception occured");
                    }
                }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
