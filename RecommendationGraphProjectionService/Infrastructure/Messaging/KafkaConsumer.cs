using Confluent.Kafka;
using RecommendationGraphProjectionService.Application.Events;
using RecommendationGraphProjectionService.Application.Services;
using RecommendationGraphProjectionService.Infrastructure.Messaging;
using System.Text.Json;

namespace RecommendationGraphProjectionService.Infrastructure.Messaging
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly IRecipeGraphService _graphService;
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(
            IRecipeGraphService graphService,
            ILogger<KafkaConsumer> logger)
        {
            _graphService = graphService;
            _logger = logger;

            var config = new ConsumerConfig
            {
                GroupId = "recipe-projection-group",
                BootstrapServers = "kafka:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public Task StartAsync(CancellationToken token)
        {
            _consumer.Subscribe(new[]
            {
                "recipe-created",
                "recipe-updated",
                "recipe-deleted"
            });

            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var result = _consumer.Consume(token);

                    _logger.LogInformation("Event received on topic {Topic}", result.Topic);

                    await DispatchEventAsync(result.Topic, result.Message.Value);
                }
            }, token);
        }


        private async Task DispatchEventAsync(string topic, string message)
        {
            switch (topic)
            {
                case "recipe-created":
                    var created = JsonSerializer.Deserialize<RecipeCreated>(message);
                    await _graphService.CreateRecipeAsync(created);
                    break;

                case "recipe-updated":
                    var updated = JsonSerializer.Deserialize<RecipeUpdated>(message);
                    await _graphService.UpdateRecipeAsync(updated);
                    break;

                case "recipe-deleted":
                    var deletedId = message;
                    await _graphService.DeleteRecipeAsync(deletedId);
                    break;
            }
        }
    }
}
