using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public class RecommendationRequestConsumerService : BackgroundService
    {
        private readonly IRecommendationRequestConsumer _consumer;

        public RecommendationRequestConsumerService(IRecommendationRequestConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(stoppingToken);
        }
    }

}
