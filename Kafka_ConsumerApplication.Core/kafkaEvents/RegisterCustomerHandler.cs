using Kafka.Constants;
using Kafka.Interfaces;
using Kafka.Messages;

namespace Kafka_ConsumerApplication.Core.kafkaEvents
{
    public class RegisterCustomerHandler : IKafkaHandler<string, CreateCustomer>
    {
        private readonly IKafkaProducer<string, CustomerRegistered> _producer;

        public RegisterCustomerHandler(IKafkaProducer<string, CustomerRegistered> producer)
        {
            _producer = producer;
        }

        public Task HandleAsync(string key, CreateCustomer value)
        {
            // Here we can actually write the code to register a User
            Console.WriteLine($"Consuming CustomerRegistered topic message with the below data\n FirstName: {value.FirstName}\n LastName: {value.LastName}\n UserName: {value.UserName}\n EmailId: {value.Email}");

            //After successful operation, suppose if the registered user has User Id as 1 the we can produce message for other service's consumption
            _producer.ProduceAsync(KafkaTopics.CustomerRegistered, "", new CustomerRegistered { UserId = 1 });

            return Task.CompletedTask;
        }
    }
}
