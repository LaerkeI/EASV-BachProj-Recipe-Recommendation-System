namespace RecipeManagementService.Infrastructure.Messaging.Events
{
    public class RecipeCreatedEvent
    {
        public string RecipeId { get; set; } = "";

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<string> Ingredients { get; set; } = new();

        public List<string> Instructions { get; set; } = new();

        public string Category { get; set; } = "";
    }
}
