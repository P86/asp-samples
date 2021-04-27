using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi;

namespace MultiContainerApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly IHttpClientFactory factory;

        public ProxyController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var client = factory.CreateClient("webapi");
            var result = await client.GetAsync("/WeatherForecast");
            
            if(!result.IsSuccessStatusCode)
            {
                throw new Exception($"Status code does not indicate success. Status code: {result.StatusCode}");
            }

            return await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(await result.Content.ReadAsStreamAsync());
        }
    }
}
