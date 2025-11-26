using Confluent.Kafka;
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
        private readonly IKafkaProducer _kafkaProducer;

        public RecipeService(IRecipeRepository recipeRepository, IKafkaProducer kafkaProducer)
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
            return await _recipeRepository.UpdateRecipe(recipeId, updatedRecipe);
        }

        public async Task<bool> DeleteRecipe(string recipeId)
        {
            return await _recipeRepository.DeleteRecipe(recipeId);
        }
    }
}
