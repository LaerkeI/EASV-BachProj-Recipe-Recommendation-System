using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Application.Services;

public interface IRecipeGraphService
{
    Task CreateRecipeAsync(string recipeId, string name, string description, List<string> ingredients, List<string> instructions, string category);
    Task GetRecommendedRecipesAsync(string correlationId, List<string> ingredientsFromFridge);
    Task UpdateRecipeAsync(string recipeId, string updatedName, string updatedDescription, List<string> updatedIngredients, List<string> updatedInstructions, string updatedCategory);
    Task DeleteRecipeAsync(string recipeId);
}
