namespace RecipeGraphProjectionService.Application.Events
{
    public class RecipeCreatedEvent
    {
        public string RecipeId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public List<string> Ingredients { get; init; } = new();
        public List<string> Instructions { get; init; } = new();
        public string Category { get; init; } = default!;

        //public RecipeCreatedEvent(
        //    string recipeId,
        //    string name,
        //    string description,
        //    IEnumerable<string> ingredients,
        //    IEnumerable<string> instructions,
        //    string category)
        //{
        //    RecipeId = recipeId;
        //    Name = name;
        //    Description = description;
        //    Ingredients = ingredients.ToList();
        //    Instructions = instructions.ToList();
        //    Category = category;
        //}
    }
}
