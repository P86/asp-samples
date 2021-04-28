using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Middleware
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //order of middlewares is important!

            //middleware defined in startup    
            app.Use(async (context, next) => {
                Console.WriteLine("First middleware - before call next");
                await next();
                Console.WriteLine("First middleware - after call next");
            });

            //middleware defined in separate class
            app.UseMiddleware<MiddlewareClass>();

            //this midleware will be executed only for /test path
            app.Map("/test", appBuilder => appBuilder.Run(async context => { await context.Response.WriteAsync("Content returned from middleware for /test"); }));

            //this middleware will be executed for other paths
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Content returned from middleware");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
