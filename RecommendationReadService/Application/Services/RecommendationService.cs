using System.Text.Json;
using Confluent.Kafka;
using RecommendationReadService.Application.DTOs;
using RecommendationReadService.Application.Events;
using RecommendationReadService.Infrastructure.Kafka;
using RecommendationReadService.Infrastructure.Repositories;

namespace RecommendationReadService.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRequestProducer _recommendationRequestProducer;
        private readonly IRecommendationRepository _recommendationRepository;

        public RecommendationService(IRecommendationRequestProducer recommendationRequestProducer, IRecommendationRepository recommendationRepository)
        {
            _recommendationRequestProducer = recommendationRequestProducer;
            _recommendationRepository = recommendationRepository;
        }

        // ------------------------------------------------------------
        // STEP 1 — Start async recommendation job
        // ------------------------------------------------------------
        public async Task<string> StartRecommendationRequest(List<string> ingredients)
        {
            var correlationId = Guid.NewGuid().ToString();

            // Initial state
            var initialState = new RecommendationStateDto
            {
                CorrelationId = correlationId,
                Status = "IN_PROGRESS",
                Recipes = null
            };

            await _recommendationRepository.SaveRecommendationState(correlationId, initialState);

            // Create event
            var requestEvent = new RecommendationRequestEvent
            {
                CorrelationId = correlationId,
                Ingredients = ingredients
            };

            var json = JsonSerializer.Serialize(requestEvent);

            // Send to Kafka
            await _recommendationRequestProducer.ProduceAsync(
                "recommendation-request",
                new Message<string, string>
                {
                    Key = correlationId,
                    Value = json
                });

            return correlationId;
        }

        // ------------------------------------------------------------
        // STEP 2 — Check recommendation status (polling from controller)
        // ------------------------------------------------------------
        public async Task<RecommendationStateDto?> GetRecommendationStatus(string correlationId)
        {
            return await _recommendationRepository.GetRecommendationStatus(correlationId);
        }

        // ------------------------------------------------------------
        // STEP 3 — Called by Kafka consumer when recommendation is ready
        // ------------------------------------------------------------
        public async Task CacheRecommendationResponse(string correlationId, List<RecommendedRecipeDto> recommendedRecipes)
        {
            var completedState = new RecommendationStateDto
            {
                CorrelationId = correlationId,
                Status = "COMPLETE",
                Recipes = recommendedRecipes
            };

            await _recommendationRepository.SaveRecommendationState(correlationId, completedState);
        }
    }
}
