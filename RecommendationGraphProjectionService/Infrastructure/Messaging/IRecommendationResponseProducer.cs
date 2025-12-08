using Confluent.Kafka;

namespace RecommendationGraphProjectionService.Infrastructure.Messaging
{
    public interface IRecommendationResponseProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}
