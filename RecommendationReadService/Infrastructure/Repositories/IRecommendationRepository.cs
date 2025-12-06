using RecommendationReadService.Application.DTOs;

namespace RecommendationReadService.Infrastructure.Repositories
{
    public interface IRecommendationRepository
    {
        Task<RecommendationStateDto?> GetRecommendationStatus(string correlationId);
        Task SaveRecommendationState(string correlationId, RecommendationStateDto state);
    }
}
