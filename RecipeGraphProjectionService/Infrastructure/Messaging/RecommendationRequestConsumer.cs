using Confluent.Kafka;
using RecipeGraphProjectionService.Application.Events;
using RecipeGraphProjectionService.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public class RecommendationRequestConsumer : IRecommendationRequestConsumer
    {
        private readonly IRecipeGraphService _recipeGraphService;
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<RecipeEventConsumer> _logger;

        public RecommendationRequestConsumer(
            IRecipeGraphService recipeGraphService,
            ILogger<RecipeEventConsumer> logger)
        {
            _recipeGraphService = recipeGraphService;
            _logger = logger;

            var config = new ConsumerConfig
            {
                GroupId = "recommendation-group",
                BootstrapServers = "kafka:29092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public Task StartAsync(CancellationToken token)
        {
            _consumer.Subscribe(new[]
            {
                "recommendation-request"
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
                case "recommendation-request":
                    var request = JsonSerializer.Deserialize<RecommendationRequestEvent>(message);
                    await _recipeGraphService.GetRecommendedRecipesAsync(request.CorrelationId, request.Ingredients);
                    Console.WriteLine("Message received by RecommendationRequestConsumer");
                    break;
            }
        }
    }
}
