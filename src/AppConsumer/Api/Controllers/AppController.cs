using Microsoft.AspNetCore.Mvc;

namespace AppConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger) => _logger = logger;

        [HttpGet]
        public IActionResult Get() => Ok();
    }
}