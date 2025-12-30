using RecipeGraphProjectionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Application.Events
{
    public class RecommendationResponseEvent
    {
        public string CorrelationId { get; set; } = default!;
        public List<RecommendedRecipeDto> RecommendedRecipes { get; set; } = default!;
    }
}
