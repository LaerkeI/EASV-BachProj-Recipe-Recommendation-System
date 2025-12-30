using Confluent.Kafka;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public interface IRecommendationResponseProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}
