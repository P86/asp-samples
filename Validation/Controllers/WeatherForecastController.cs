using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Validation.Controllers
{
    [ApiController] //this attribute is necessary for validation attributes to work
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPut]
        public void Put([FromBody]WeatherForecast forecast)
        {
            //intentionally left empty
        }
    }
    public class WeatherForecast
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(-100, 100)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Between 1 and 30 chars")]
        public string Summary { get; set; }
    }
}
