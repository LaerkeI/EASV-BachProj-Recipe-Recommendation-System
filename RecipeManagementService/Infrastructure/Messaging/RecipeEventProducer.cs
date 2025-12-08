using Confluent.Kafka;
using RecipeManagementService.Domain.Interfaces;

namespace RecipeManagementService.Infrastructure.Messaging
{
    public class RecipeEventProducer : IRecipeEventProducer
    {
        private readonly IProducer<string, string> _producer;

        public RecipeEventProducer(IConfiguration config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<string, string>(producerConfig).Build();
        }

        public async Task ProduceAsync(string topic, Message<string, string> message)
        {
            await _producer.ProduceAsync(topic, message);
        }
    }
}
