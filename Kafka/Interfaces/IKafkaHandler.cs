namespace Kafka.Interfaces
{
    public interface IKafkaHandler<Tk, Tv>
    {
        // Provide mechanism to handle the consumer message from Kafka
        Task HandleAsync(Tk key, Tv value);
    }
}
