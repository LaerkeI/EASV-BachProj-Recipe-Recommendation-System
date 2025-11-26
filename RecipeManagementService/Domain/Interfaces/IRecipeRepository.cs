using RecipeManagementService.Domain.Entities;

namespace RecipeManagementService.Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllRecipes();
        Task<Recipe?> GetRecipeByRecipeId(string recipeId);
        Task CreateRecipe(Recipe recipe);
        Task<bool> UpdateRecipe(string recipeId, Recipe updatedRecipe);
        Task<bool> DeleteRecipe(string recipeId);
    }
}
