namespace RecommendationReadService.Application.DTOs
{
    public class RecommendationStateDto
    {
        public string CorrelationId { get; set; } = default!;
        public string Status { get; set; } = default!; // IN_PROGRESS | COMPLETE
        public List<RecommendedRecipeDto>? Recipes { get; set; }
    }
}
