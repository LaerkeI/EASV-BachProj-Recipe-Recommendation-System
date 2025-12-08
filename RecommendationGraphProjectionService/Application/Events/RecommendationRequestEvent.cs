using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecommendationRequestEvent
    {
        public string CorrelationId { get; set; } = default!;
        public List<string> Ingredients { get; set; } = default!;
    }

}
