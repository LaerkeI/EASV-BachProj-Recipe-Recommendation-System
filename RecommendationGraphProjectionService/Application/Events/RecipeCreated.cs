using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecipeCreated
    {
        public string RecipeId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public IEnumerable<string> Ingredients { get; init; } = Enumerable.Empty<string>();

        public RecipeCreated(string recipeId, string name, IEnumerable<string> ingredients)
        {
            RecipeId = recipeId;
            Name = name;
            Ingredients = ingredients;
        }
    }
}
