using FARApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        FARContext _context;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, FARContext context)
        {
            _logger = logger;
            _context = context;
        }

       // [HttpGet]
      //  public IEnumerable<WeatherForecast> Get()
      //{
      //      //var context = new FARContext();
      //      var result = _context.FARs.First();
      //      var rng = new Random();
      //      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      //      {
      //          Date = DateTime.Now.AddDays(index),
      //          TemperatureC = rng.Next(-20, 55),
      //          Summary = Summaries[rng.Next(Summaries.Length)]
      //      })
      //      .ToArray();
      //  }
    }
}
