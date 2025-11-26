using MongoDB.Driver;
using RecipeManagementService.Domain.Entities;
using RecipeManagementService.Domain.Interfaces;

namespace RecipeManagementService.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IMongoCollection<Recipe> _recipesCollection;

        public RecipeRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("RecipeDB");
            _recipesCollection = database.GetCollection<Recipe>("Recipes");
        }

        public async Task<List<Recipe>> GetAllRecipes()
        {
            return await _recipesCollection.Find(r => true).ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByRecipeId(string recipeId)
        {
            return await _recipesCollection
                .Find(r => r.RecipeId == recipeId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            await _recipesCollection.InsertOneAsync(recipe);
        }

        public async Task<bool> UpdateRecipe(string recipeId, Recipe updatedRecipe)
        {
            var update = Builders<Recipe>.Update
                .Set(r => r.Name, updatedRecipe.Name)
                .Set(r => r.Description, updatedRecipe.Description)
                .Set(r => r.Ingredients, updatedRecipe.Ingredients)
                .Set(r => r.Instructions, updatedRecipe.Instructions)
                .Set(r => r.Category, updatedRecipe.Category);

            var result = await _recipesCollection.UpdateOneAsync(
                r => r.RecipeId == recipeId,
                update
            );

            return result.MatchedCount > 0;
        }

        public async Task<bool> DeleteRecipe(string recipeId)
        {
            var result = await _recipesCollection.DeleteOneAsync(r => r.RecipeId == recipeId);
            return result.DeletedCount > 0;
        }
    }
}
