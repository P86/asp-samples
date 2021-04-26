using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Tests
{
    public class WeatherForecastTests
    {
        private readonly HttpClient _client;

        public WeatherForecastTests()
        {
            var factory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => { 
                        //here is possible to remove registered services and replace it with different one
                    });
                });
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task ShouldReturn5Forecasts()
        {
            var response = await _client.GetAsync("/WeatherForecast");

            Assert.True(response.IsSuccessStatusCode);

            var content = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(await response.Content.ReadAsStreamAsync());

            Assert.Equal(5, content.Count);
        }
    }
}
