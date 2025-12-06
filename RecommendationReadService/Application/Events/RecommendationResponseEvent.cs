using RecommendationReadService.Application.DTOs;

namespace RecommendationReadService.Application.Events
{
    public class RecommendationResponseEvent
    {
        public string CorrelationId { get; set; } = default!;
        public List<RecommendedRecipeDto> RecommendedRecipes { get; set; } = default!;
    }
}
