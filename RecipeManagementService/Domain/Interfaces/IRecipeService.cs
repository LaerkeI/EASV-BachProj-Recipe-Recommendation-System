using RecipeManagementService.Domain.Entities;

namespace RecipeManagementService.Domain.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(string recipeId);
        Task CreateRecipeAsync(Recipe recipe);
        Task<bool> UpdateRecipeAsync(string recipeId, Recipe updatedRecipe);
        Task<bool> DeleteRecipeAsync(string recipeId);
    }
}
