using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using RecommendationGraphProjectionService.Application.Events;
using RecommendationGraphProjectionService.Infrastructure.Messaging;
using RecommendationGraphProjectionService.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Text.Json;
using static Confluent.Kafka.ConfigPropertyNames;

namespace RecommendationGraphProjectionService.Application.Services
{
    public class RecipeGraphService : IRecipeGraphService
    {
        private readonly IRecipeGraphRepository _repository;
        private readonly IRecommendationResponseProducer _recommendationResponseProducer;
        private readonly ILogger<RecipeGraphService> _logger;

        public RecipeGraphService(
            IRecipeGraphRepository repository,
            IRecommendationResponseProducer recommendationResponseProducer,
            ILogger<RecipeGraphService> logger)
        {
            _repository = repository;
            _recommendationResponseProducer = recommendationResponseProducer;
            _logger = logger;
        }

        public async Task CreateRecipeAsync(
            string recipeId,
            string name,
            string description,
            List<string> ingredients,
            List<string> instructions,
            string category)
        {
            await _repository.CreateRecipeAsync(
                recipeId,
                name,
                description,
                ingredients,
                instructions,
                category
            );

            _logger.LogInformation("Recipe {Id} created in Neo4j graph.", recipeId);
        }

        public async Task GetRecommendedRecipesAsync(string correlationId, List<string> ingredientsFromFridge)
        {
            var recommended = await _repository.GetRecommendedRecipesAsync(ingredientsFromFridge);

            var response = new RecommendationResponseEvent
            {
                CorrelationId = correlationId,
                RecommendedRecipes = recommended
            };

            var json = JsonSerializer.Serialize(response);

            await _recommendationResponseProducer.ProduceAsync(
                "recommendation-response",
                new Message<string, string>
                {
                    Key = correlationId,
                    Value = json
                });

            _logger.LogInformation("Sent recommendation response for correlation {CorrelationId}", correlationId);
        }

        public async Task UpdateRecipeAsync(
            string recipeId,
            string updatedName,
            string updatedDescription,
            List<string> updatedIngredients,
            List<string> updatedInstructions,
            string updatedCategory)
        {
            await _repository.UpdateRecipeAsync(
                recipeId, updatedName, updatedDescription, updatedIngredients, updatedInstructions, updatedCategory
            );

            _logger.LogInformation("Recipe {Id} updated in Neo4j graph.", recipeId);
        }


        public async Task DeleteRecipeAsync(string recipeId)
        {
            await _repository.DeleteRecipeAsync(recipeId);
            _logger.LogInformation("Recipe {Id} deleted from Neo4j graph.", recipeId);
        }
    }
}