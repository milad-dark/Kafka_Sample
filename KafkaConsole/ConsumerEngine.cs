using Confluent.Kafka;
using KafkaConsole.Model;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.Json;

namespace KafkaConsole
{
    public static class ConsumerEngine
    {
        private static readonly string _topic = "toptest";
        private static readonly string _groupId = "bulk_group";
        private static readonly string _bootstrapServers = "localhost:9092";

        public static Task StartConsumeAsync(CancellationToken cancellationToken)
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
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                            var orderRequest = JsonSerializer.Deserialize<OrderRequest>(consumer.Message.Value);
                            Console.WriteLine($"Processing Order Id: {orderRequest.OrderId} , CustomerId: {orderRequest.CustomerId} , Time Elapsed Read: {sw.Elapsed}");
                        }
                        sw.Stop();
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine($"Processing Order Operation Error: {ex.Message}");
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }

        public static Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Processing Order Operation stoped");
            return Task.CompletedTask;
        }
    }
}
