using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationReadService.Application.DTOs
{
    public class RecommendedRecipeDto
    {
        public string RecipeId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public List<string> Ingredients { get; set; } = default!;

    }
}
