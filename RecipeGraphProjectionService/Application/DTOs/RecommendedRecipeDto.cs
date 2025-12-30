using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeGraphProjectionService.Application.DTOs
{
    public class RecommendedRecipeDto
    {
        public string RecipeId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; init; } = default!;
        public IEnumerable<string> RecipeIngredients { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> MatchedIngredients { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> MissingIngredients { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Instructions { get; init; } = Enumerable.Empty<string>();
        public string Category { get; init; } = default!;

        public int MatchCount { get; set; }
        public int TotalIngredients { get; set; }
        public double Score { get; set; }
    }
}
