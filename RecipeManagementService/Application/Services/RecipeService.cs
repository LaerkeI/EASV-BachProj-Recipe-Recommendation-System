using Confluent.Kafka;
using RecipeManagementService.Domain.Entities;
using RecipeManagementService.Domain.Interfaces;
using RecipeManagementService.Infrastructure.Messaging.Events;
using System.Text.Json;

namespace RecipeManagementService.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRecipeEventProducer _kafkaProducer;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeEventProducer kafkaProducer)
        {
            _recipeRepository = recipeRepository;            
            _kafkaProducer = kafkaProducer;
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

            var message = new RecipeCreatedEvent
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                Category = recipe.Category
            };

            await _kafkaProducer.ProduceAsync("recipe-created", new Message<string, string>
            {
                Key = recipe.RecipeId,
                Value = JsonSerializer.Serialize(message)
            });
        }

        public async Task<bool> UpdateRecipe(string recipeId, Recipe updatedRecipe)
        {
            var success = await _recipeRepository.UpdateRecipe(recipeId, updatedRecipe);

            if (!success)
                return false;

            var message = new RecipeUpdatedEvent
            {
                RecipeId = recipeId,
                UpdatedName = updatedRecipe.Name,
                UpdatedDescription = updatedRecipe.Description,
                UpdatedIngredients = updatedRecipe.Ingredients,
                UpdatedInstructions = updatedRecipe.Instructions,
                UpdatedCategory = updatedRecipe.Category
            };

            await _kafkaProducer.ProduceAsync("recipe-updated", new Message<string, string>
            {
                Key = recipeId,
                Value = JsonSerializer.Serialize(message)
            });

            return true;
        }

        public async Task<bool> DeleteRecipe(string recipeId)
        {
            var success = await _recipeRepository.DeleteRecipe(recipeId);

            if (!success)
                return false;

            var message = new RecipeDeletedEvent
            {
                RecipeId = recipeId
            };

            await _kafkaProducer.ProduceAsync("recipe-deleted", new Message<string, string>
            {
                Key = recipeId,
                Value = JsonSerializer.Serialize(message)
            });

            return true;
        }
    }
}
