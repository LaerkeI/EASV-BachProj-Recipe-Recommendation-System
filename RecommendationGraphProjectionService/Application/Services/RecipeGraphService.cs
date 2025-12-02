using Microsoft.Extensions.Logging;
using RecommendationGraphProjectionService.Application.Events;
using RecommendationGraphProjectionService.Infrastructure.Repositories;

namespace RecommendationGraphProjectionService.Application.Services
{
    public class RecipeGraphService : IRecipeGraphService
    {
        private readonly IRecipeGraphRepository _repository;
        private readonly ILogger<RecipeGraphService> _logger;

        public RecipeGraphService(
            IRecipeGraphRepository repository,
            ILogger<RecipeGraphService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateRecipeAsync(RecipeCreated evt)
        {
            await _repository.CreateRecipeAsync(evt);
            _logger.LogInformation("Recipe {Id} created in Neo4j graph.", evt.RecipeId);
        }

        public async Task UpdateRecipeAsync(RecipeUpdated evt)
        {
            await _repository.UpdateRecipeAsync(evt);
            _logger.LogInformation("Recipe {Id} updated in Neo4j graph.", evt.RecipeId);
        }

        public async Task DeleteRecipeAsync(string recipeId)
        {
            await _repository.DeleteRecipeAsync(recipeId);
            _logger.LogInformation("Recipe {Id} deleted from Neo4j graph.", recipeId);
        }
    }
}