using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Application.Services;

public interface IRecipeGraphService
{
    Task CreateRecipeAsync(RecipeCreated evt);
    Task UpdateRecipeAsync(RecipeUpdated evt);
    Task DeleteRecipeAsync(string recipeId);
}
