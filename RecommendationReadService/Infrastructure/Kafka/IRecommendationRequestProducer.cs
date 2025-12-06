using Confluent.Kafka;

namespace RecommendationReadService.Infrastructure.Kafka
{
    public interface IRecommendationRequestProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}
