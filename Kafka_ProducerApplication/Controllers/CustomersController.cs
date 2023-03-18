using Kafka.Constants;
using Kafka.Interfaces;
using Kafka.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kafka_ProducerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IKafkaProducer<string, CreateCustomer> _kafkaProducer;

        public CustomersController(IKafkaProducer<string, CreateCustomer> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Register User", "This endpoint can be used to register a Customer ,but for demo produces dummy message in Kafka Topic")]
        public async Task<IActionResult> ProduceMessage(CreateCustomer request)
        {
            await _kafkaProducer.ProduceAsync(KafkaTopics.CustomerRegistered, null, request);

            return Ok("Customer Registration In Progress");
        }
    }
}
