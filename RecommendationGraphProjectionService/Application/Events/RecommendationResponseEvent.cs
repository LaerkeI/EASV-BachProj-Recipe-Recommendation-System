using RecommendationGraphProjectionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecommendationResponseEvent
    {
        public string CorrelationId { get; set; } = default!;
        public List<RecommendedRecipeDto> RecommendedRecipes { get; set; } = default!;
    }
}
