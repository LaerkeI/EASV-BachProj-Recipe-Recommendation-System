using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationGraphProjectionService.Application.Events
{
    public class RecipeDeleted
    {
        public string RecipeId { get; init; } = default!;

        public RecipeDeleted(string recipeId)
        {
            RecipeId = recipeId;
        }
    }
}
