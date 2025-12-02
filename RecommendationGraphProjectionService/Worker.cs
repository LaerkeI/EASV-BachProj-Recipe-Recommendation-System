using RecommendationGraphProjectionService.Infrastructure.Messaging;

namespace RecommendationGraphProjectionService
{
    public class Worker : BackgroundService
    {
        private readonly IKafkaConsumer _consumer;
        private readonly ILogger<Worker> _logger;

        public Worker(IKafkaConsumer consumer, ILogger<Worker> logger)
        {
            _consumer = consumer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RecommendationGraphProjectionService started.");

            await _consumer.StartAsync(stoppingToken);

            _logger.LogInformation("Kafka consumer running...");
        }
    }

}
