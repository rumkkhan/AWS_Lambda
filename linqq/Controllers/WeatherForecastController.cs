using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using linqq.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace linqq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly PlutoCodeFirstContext _plutoCodeFirstContext;
        public WeatherForecastController(PlutoCodeFirstContext plutoCodeFirstContext)
        {
            _plutoCodeFirstContext = plutoCodeFirstContext;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Courses> Get()
        {
            return _plutoCodeFirstContext.Courses.ToList();
        }
    }
}
