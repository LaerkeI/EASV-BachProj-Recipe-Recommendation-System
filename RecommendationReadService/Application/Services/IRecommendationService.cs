using Microsoft.AspNetCore.Mvc;
using RecommendationReadService.Application.DTOs;
using RecommendationReadService.Application.Events;

namespace RecommendationReadService.Application.Services
{
    public interface IRecommendationService
    {
        Task<string> StartRecommendationRequest(List<string> ingredients);
        Task<RecommendationStateDto?> GetRecommendationStatus(string correlationId);
        Task CacheRecommendationResponse(string correlationId, List<RecommendedRecipeDto> recommendedRecipes);
    }
}
