namespace Kafka.Interfaces
{
    public interface IKafkaConsumer<TKey, TValue> where TValue : class
    {
        // Triggered when the service is ready to consume the Kafka topic.
        Task Consume(string topic, CancellationToken stoppingToken);
        // This will close the consumer, commit offsets and leave the group cleanly.
        void Close();
        // Releases all resources used by the current instance of the consumer
        void Dispose();
    }
}
