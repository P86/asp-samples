using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CORS
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

            //allows requests from any origins
            services.AddCors(o => o.AddPolicy("public", builder => builder.AllowAnyOrigin()
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyHeader()));
            //allow request only from http://localhost:5000
            services.AddCors(o => o.AddPolicy("local", b => b.WithOrigins("http://localhost:5000/")));

            //allow access from subdomains of mycompany.com eg. app1.mycompany.com, test.mycompany.com
            services.AddCors(o => o.AddPolicy("subdomains", b => b.WithOrigins("https://*.mycompany.com")
                                                                  .SetIsOriginAllowedToAllowWildcardSubdomains()));

            services.AddCors(o => o.AddPolicy("dynamic", b => b.SetIsOriginAllowed(host =>
            {
                //do a check in this function, it will be called in each requesst
                return true;
            })));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //UseCors just simply setup middleware that will add Access-Control-Allow-Origin header to response
            //browser check this header and decide if given origin is able to read the response
            app.UseCors("local");

            app.Run(async context => { await context.Response.WriteAsync($"Check response headers for cors"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
