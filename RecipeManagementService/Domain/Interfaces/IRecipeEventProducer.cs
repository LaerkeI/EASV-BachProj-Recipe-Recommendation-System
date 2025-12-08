using Confluent.Kafka;

namespace RecipeManagementService.Domain.Interfaces
{
    public interface IRecipeEventProducer
    {
        Task ProduceAsync(string topic, Message<string, string> message);
    }
}
