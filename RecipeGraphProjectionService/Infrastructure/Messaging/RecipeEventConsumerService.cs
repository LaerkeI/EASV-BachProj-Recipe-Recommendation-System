using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public class RecipeEventConsumerService : BackgroundService
    {
        private readonly IRecipeEventConsumer _consumer;

        public RecipeEventConsumerService(IRecipeEventConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(stoppingToken);
        }
    }
}
