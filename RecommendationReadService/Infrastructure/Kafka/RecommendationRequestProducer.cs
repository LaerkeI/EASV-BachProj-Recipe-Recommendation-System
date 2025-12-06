using Confluent.Kafka;

namespace RecommendationReadService.Infrastructure.Kafka
{
    public class RecommendationRequestProducer : IRecommendationRequestProducer
    {
        private readonly IProducer<string, string> _producer;

        public RecommendationRequestProducer(IConfiguration config)
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