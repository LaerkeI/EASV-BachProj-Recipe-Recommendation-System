using Confluent.Kafka;

namespace RecipeManagementService.Domain.Interfaces
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}
