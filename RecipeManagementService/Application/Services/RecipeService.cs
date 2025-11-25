using RecipeManagementService.Domain.Entities;
using RecipeManagementService.Domain.Interfaces;

namespace RecipeManagementService.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllRecipesAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(string recipeId)
        {
            return await _recipeRepository.GetRecipeByIdAsync(recipeId);
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.CreateRecipeAsync(recipe);
        }

        public async Task<bool> UpdateRecipeAsync(string recipeId, Recipe updatedRecipe)
        {
            return await _recipeRepository.UpdateRecipeAsync(recipeId, updatedRecipe);
        }

        public async Task<bool> DeleteRecipeAsync(string recipeId)
        {
            return await _recipeRepository.DeleteRecipeAsync(recipeId);
        }
    }
}
