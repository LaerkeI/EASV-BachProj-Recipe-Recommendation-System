using RecipeManagementService.Domain.Entities;
using RecipeManagementService.Domain.Interfaces;
using RecipeManagementService.Infrastructure.Messaging;
using RecipeManagementService.Infrastructure.Messaging.Events;
using System.Text.Json;

namespace RecipeManagementService.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;            
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _recipeRepository.GetAllRecipes();
        }

        public async Task<Recipe?> GetRecipeByRecipeId(string recipeId)
        {
            return await _recipeRepository.GetRecipeByRecipeId(recipeId);
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            await _recipeRepository.CreateRecipe(recipe);
        }

        public async Task<bool> UpdateRecipe(string recipeId, Recipe updatedRecipe)
        {
            return await _recipeRepository.UpdateRecipe(recipeId, updatedRecipe);
        }

        public async Task<bool> DeleteRecipe(string recipeId)
        {
            return await _recipeRepository.DeleteRecipe(recipeId);
        }
    }
}
