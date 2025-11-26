using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipeManagementService.Domain.Entities
{
    public class Recipe
    {
        [BsonId] // MongoDB _id field
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RecipeId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("ingredients")]
        public List<string> Ingredients { get; set; } = new List<string>();

        [BsonElement("instructions")]
        public List<string> Instructions { get; set; } = new List<string>();

        [BsonElement("category")]
        public string Category { get; set; } = null!;
    }
}
