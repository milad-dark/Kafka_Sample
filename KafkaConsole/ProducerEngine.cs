using Confluent.Kafka;
using KafkaConsole.Model;
using System.Net;
using System.Text.Json;

namespace KafkaConsole
{
    public static class ProducerEngine
    {
        private static readonly string _bootstrapServers = "localhost:9092";
        private static readonly string _topic = "toptest";

        public static async Task SendMessageToKafka()
        {
            var order = new OrderRequest
            {
                OrderId = 1,
                CustomerId = 1,
                ProductId = 1,
                Quantity = 10,
                Status = "order"
            };

            for (int i = 0; i < 3_000_000; i++)
            {
                order.OrderId = i;
                order.ProductId = i;
                order.CustomerId = i;
                order.Quantity += i;

                string message = JsonSerializer.Serialize(order);
                await SendOrderRequest(_topic, message);
                //Console.WriteLine(message);
            }
        }

        private static async Task<bool> SendOrderRequest(string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                ClientId = Dns.GetHostName(),
                Acks = Acks.All,
                LingerMs = 10,
                CompressionType = CompressionType.Lz4,
                BatchSize = 10000000
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = producer.ProduceAsync(_topic,
                        new Message<Null, string> { Value = message });

                    //Console.WriteLine($"Delivery Timestamp:{result.Result.Timestamp.UtcDateTime}");
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return await Task.FromResult(false);
        }
    }
}
