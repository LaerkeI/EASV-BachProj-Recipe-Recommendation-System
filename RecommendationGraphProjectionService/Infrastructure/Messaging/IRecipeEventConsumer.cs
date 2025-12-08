using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Infrastructure.Messaging
{
    public interface IRecipeEventConsumer
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
