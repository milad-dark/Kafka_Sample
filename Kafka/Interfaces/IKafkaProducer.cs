namespace Kafka.Interfaces
{
    public interface IKafkaProducer<in TKey, in TValue> where TValue : class
    {
        //  Triggered when the service is ready to produce the Kafka topic.
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}
