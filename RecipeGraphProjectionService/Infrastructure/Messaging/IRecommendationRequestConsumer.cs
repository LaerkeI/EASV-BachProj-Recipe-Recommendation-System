using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Infrastructure.Messaging
{
    public interface IRecommendationRequestConsumer
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
