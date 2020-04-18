using AutoMapper;
using Mapping.Models;

namespace Mapping.Profiles
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastDto>()
                .ForMember(
                    dest => dest.TemperatureF, 
                    options => options.MapFrom(src => 32 + (int)(src.TemperatureC / 0.5556)));
        }

    }
}