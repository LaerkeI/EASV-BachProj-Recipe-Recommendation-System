namespace RecipeManagementService.Infrastructure.Messaging.Events
{
    public class RecipeUpdatedEvent
    {
        public string RecipeId { get; init; } = default!;
        public string UpdatedName { get; init; } = default!;
        public string UpdatedDescription { get; init; } = default!;
        public List<string> UpdatedIngredients { get; init; } = new();
        public List<string> UpdatedInstructions { get; init; } = new();
        public string UpdatedCategory { get; init; } = default!;

        //public RecipeUpdatedEvent(
        //    string recipeId,
        //    string updatedName,
        //    string updatedDescription,
        //    IEnumerable<string> updatedIngredients,
        //    IEnumerable<string> updatedInstructions,
        //    string updatedCategory)
        //{
        //    RecipeId = recipeId;
        //    UpdatedName = updatedName;
        //    UpdatedDescription = updatedDescription;
        //    UpdatedIngredients = updatedIngredients.ToList();
        //    UpdatedInstructions = updatedInstructions.ToList();
        //    UpdatedCategory = updatedCategory;
        //}
    }
}

