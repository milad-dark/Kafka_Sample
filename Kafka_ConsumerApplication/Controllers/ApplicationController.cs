using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kafka_ConsumerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Get Welcome Message", "This is a dummy endpoint")]
        public IActionResult Get()
        {
            return Ok("Welcome to Kafka Consumer Application.");
        }
    }
}
