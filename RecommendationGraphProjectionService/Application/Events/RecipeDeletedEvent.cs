namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecipeDeletedEvent
    {
        public string RecipeId { get; init; } = default!;

        //public RecipeDeletedEvent(string recipeId)
        //{
        //    RecipeId = recipeId;
        //}
    }
}