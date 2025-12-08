using RecommendationGraphProjectionService.Application.DTOs;
using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Infrastructure.Repositories
{
    public interface IRecipeGraphRepository
    {
        Task CreateRecipeAsync(string recipeId, string name, string description, List<string> ingredients, List<string> instructions, string category);
        Task<List<RecommendedRecipeDto>> GetRecommendedRecipesAsync(List<string> ingredientsFromFridge);
        Task UpdateRecipeAsync(string recipeId, string updatedName, string updatedDescription, List<string> updatedIngredients, List<string> updatedInstructions, string updatedCategory);
        Task DeleteRecipeAsync(string recipeId);
    }
}