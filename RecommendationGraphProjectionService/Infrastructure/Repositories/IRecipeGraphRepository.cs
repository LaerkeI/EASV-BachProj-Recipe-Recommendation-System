using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Infrastructure.Repositories
{
    public interface IRecipeGraphRepository
    {
        Task CreateRecipeAsync(RecipeCreated evt);
        Task UpdateRecipeAsync(RecipeUpdated evt);
        Task DeleteRecipeAsync(string recipeId);
    }

}