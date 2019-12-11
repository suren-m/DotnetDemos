using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockHttpStatusController : ControllerBase
    {
        private readonly ILogger<MockHttpStatusController> _logger;

        public MockHttpStatusController(ILogger<MockHttpStatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]       
        public IActionResult GetStatusCode(int statusCode)
        {
            return new StatusCodeResult(statusCode);
        }

        [HttpGet]
        [Route("500")]
        public IActionResult Throw500()
        {
            return new StatusCodeResult(500);
        }

        [HttpGet]
        [Route("404")]
        public IActionResult Throw404()
        {
            return new StatusCodeResult(404);
        }

        [HttpGet]
        [Route("429")]
        public IActionResult Throw429()
        {
            return new StatusCodeResult(404);
        }
    }
}