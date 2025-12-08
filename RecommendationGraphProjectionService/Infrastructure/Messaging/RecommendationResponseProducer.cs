using Confluent.Kafka;

namespace RecommendationGraphProjectionService.Infrastructure.Messaging
{
    public class RecommendationResponseProducer : IRecommendationResponseProducer
    {
        private readonly IProducer<string, string> _producer;

        public RecommendationResponseProducer(IConfiguration config)
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