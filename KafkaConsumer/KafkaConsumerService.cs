using Confluent.Kafka;
using KafkaConsumer.Model;
using System.Diagnostics;
using System.Text.Json;

namespace KafkaConsumer
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly string _topic = "OrderRegister";
        private readonly string _groupId = "test_group";
        private readonly string _bootstrapServers = "localhost:9092";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(_topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                            var orderRequest = JsonSerializer.Deserialize<OrderProcessingRequest>(consumer.Message.Value);
                            Debug.WriteLine($"Processing Order Id: {orderRequest.OrderId}");
                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                        Debug.WriteLine($"Processing Order Operation Error: {ex.Message}");
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
