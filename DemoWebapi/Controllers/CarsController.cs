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
    public class CarsController
    {
        private readonly ILogger<CarsController> _logger;
        private readonly IDataService<Car> _carsService;

        public CarsController(ILogger<CarsController> logger, IDataService<Car> carsService)
        {
            _logger = logger;
            _carsService = carsService;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> Get([FromQuery]string delay)
        {
            if (delay != null)
            {
                int.TryParse(delay.Replace("s", string.Empty), out int delayInSeconds);
                await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
            }
            var data = await _carsService.GetAllAsync();

            return data;
        }
    }
}
