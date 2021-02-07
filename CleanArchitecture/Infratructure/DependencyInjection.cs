using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Infratructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        }
    }
}
