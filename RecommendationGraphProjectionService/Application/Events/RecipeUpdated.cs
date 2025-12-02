using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecipeUpdated
    {
        public string RecipeId { get; init; } = default!;
        public string NewName { get; init; } = default!;
        public IEnumerable<string> NewIngredients { get; init; } = Enumerable.Empty<string>();

        public RecipeUpdated(string recipeId, string newName, IEnumerable<string> newIngredients)
        {
            RecipeId = recipeId;
            NewName = newName;
            NewIngredients = newIngredients;
        }
    }
}
