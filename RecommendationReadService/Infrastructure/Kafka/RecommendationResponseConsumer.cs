using Confluent.Kafka;
using RecommendationReadService.Application.Events;
using RecommendationReadService.Application.Services;
using System.Text.Json;

public class RecommendationResponseConsumer : BackgroundService
{
    private readonly IRecommendationService _recommendationService;
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<RecommendationResponseConsumer> _logger;

    public RecommendationResponseConsumer(
        IRecommendationService recommendationService,
        ILogger<RecommendationResponseConsumer> logger)
    {
        _recommendationService = recommendationService;
        _logger = logger;

        var config = new ConsumerConfig
        {
            GroupId = "recommendation-group",
            BootstrapServers = "kafka:29092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("recommendation-response");
        _logger.LogInformation("Kafka consumer subscribed.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // IMPORTANT: timeout prevents deadlock
                var result = _consumer.Consume(TimeSpan.FromMilliseconds(200));

                if (result != null)
                {
                    await HandleMessageAsync(result.Message.Value);
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogError(ex, "Kafka consume error");
            }

            // Yield back to thread pool
            await Task.Delay(50, stoppingToken);
        }
    }

    private async Task HandleMessageAsync(string message)
    {
        try
        {
            var response = JsonSerializer.Deserialize<RecommendationResponseEvent>(message);
            if (response != null)
            {
                await _recommendationService.CacheRecommendationResponse(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Kafka message");
        }
    }
}
