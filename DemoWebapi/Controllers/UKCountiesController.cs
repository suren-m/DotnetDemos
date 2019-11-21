using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoWebapi.Models;
using DemoWebapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoWebapi.Controllers
{
    /// <summary>
    /// Returns a list of Counties in UK
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UKCountiesController
    {
        private readonly ILogger<UKCountiesController> _logger;
        private readonly IDataService<UKCounty> _ukCountyServiceAsync;

        public UKCountiesController(ILogger<UKCountiesController> logger, IDataService<UKCounty> ukCountyServiceAsync)
        {
            _logger = logger;
            _ukCountyServiceAsync = ukCountyServiceAsync;
        }

        [HttpGet]
        public async Task<IEnumerable<UKCounty>> Get([FromQuery]string delay)
        {
            if (delay != null)
            {
                int.TryParse(delay.Replace("s", string.Empty), out int delayInSeconds);
                await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
            }

            var data = await _ukCountyServiceAsync.GetAllAsync();

            return data;
        }
      
    }
}
