using Confluent.Kafka;
using RecipeGraphProjectionService.Application.Events;
using RecipeGraphProjectionService.Application.Services;
using RecipeGraphProjectionService.Infrastructure.Messaging;
using System.Text.Json;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public class RecipeEventConsumer : IRecipeEventConsumer
    {
        private readonly IRecipeGraphService _recipeGraphService;
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<RecipeEventConsumer> _logger;

        public RecipeEventConsumer(
            IRecipeGraphService recipeGraphService,
            ILogger<RecipeEventConsumer> logger)
        {
            _recipeGraphService = recipeGraphService;
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
                    var created = JsonSerializer.Deserialize<RecipeCreatedEvent>(message,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    _logger.LogInformation("RAW EVENT: " + message);

                    await _recipeGraphService.CreateRecipeAsync(
                        created!.RecipeId,
                        created.Name,
                        created.Description,
                        created.Ingredients,
                        created.Instructions,
                        created.Category
                    );
                    break;

                case "recipe-updated":
                    var updated = JsonSerializer.Deserialize<RecipeUpdatedEvent>(message,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    _logger.LogInformation("RAW EVENT: {Message}", message);

                    await _recipeGraphService.UpdateRecipeAsync(
                        updated!.RecipeId,
                        updated.UpdatedName,
                        updated.UpdatedDescription,
                        updated.UpdatedIngredients,
                        updated.UpdatedInstructions,
                        updated.UpdatedCategory
                    );
                    break;

                case "recipe-deleted":
                    var deleted = JsonSerializer.Deserialize<RecipeDeletedEvent>(message,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                    await _recipeGraphService.DeleteRecipeAsync(deleted.RecipeId);
                    break;
            }
        }
    }
}
