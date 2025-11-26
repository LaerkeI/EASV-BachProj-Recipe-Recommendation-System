using Microsoft.AspNetCore.Mvc;
using RecipeManagementService.Domain.Entities;
using RecipeManagementService.Domain.Interfaces;

namespace RecipeManagementService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/Recipe/{recipeId}
        [HttpGet("{recipeId}")]
        public async Task<ActionResult<Recipe>> GetRecipeByRecipeId(string recipeId)
        {
            var recipe = await _recipeService.GetRecipeByRecipeId(recipeId);
            if (recipe == null)
            {
                return NotFound($"Recipe with ID {recipeId} not found.");
            }
            return Ok(recipe);
        }

        // POST: api/Recipe
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _recipeService.CreateRecipe(recipe);
            return CreatedAtAction(nameof(GetRecipeByRecipeId), new { recipeId = recipe.RecipeId }, recipe);
        }

        // PUT: api/Recipe/{recipeId}
        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateRecipe(string recipeId, [FromBody] Recipe updatedRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _recipeService.UpdateRecipe(recipeId, updatedRecipe);
            if (!success)
            {
                return NotFound($"Recipe with ID {recipeId} not found.");
            }

            return NoContent();
        }

        // DELETE: api/Recipe/{recipeId}
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipe(string recipeId)
        {
            var success = await _recipeService.DeleteRecipe(recipeId);
            if (!success)
            {
                return NotFound($"Recipe with ID {recipeId} not found.");
            }

            return NoContent();
        }
    }
}
