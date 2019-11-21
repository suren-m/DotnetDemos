using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWebapi.Models;
using DemoWebapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoWebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IDataService<Country> _countriesService;

        public CountriesController(ILogger<CountriesController> logger, IDataService<Country> countriesService)
        {
            _logger = logger;
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Get([FromQuery]string delay)
        {
            if (delay!=null)
            {
                int.TryParse(delay.Replace("s", string.Empty), out int delayInSeconds);
                await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
            }
            var data = await _countriesService.GetAllAsync();

            return data;
        }
    }
}
