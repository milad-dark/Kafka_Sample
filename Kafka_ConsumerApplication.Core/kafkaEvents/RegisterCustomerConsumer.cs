using Kafka.Constants;
using Kafka.Interfaces;
using Kafka.Messages;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Kafka_ConsumerApplication.Core.kafkaEvents
{
    public class RegisterCustomerConsumer : BackgroundService
    {
        private readonly IKafkaConsumer<string, CreateCustomer> _consumer;

        public RegisterCustomerConsumer(IKafkaConsumer<string, CreateCustomer> consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _consumer.Consume(KafkaTopics.CustomerRegistered, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopics.CustomerRegistered}, {ex}");
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();

            base.Dispose();
        }
    }
}
