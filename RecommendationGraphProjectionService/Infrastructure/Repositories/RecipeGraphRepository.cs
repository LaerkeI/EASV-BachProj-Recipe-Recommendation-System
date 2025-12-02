using Neo4j.Driver;
using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Infrastructure.Repositories
{
    public class RecipeGraphRepository : IRecipeGraphRepository
    {
        private readonly IDriver _driver;
        //private readonly ILogger<RecommendationGraphService> _logger;

        public RecipeGraphRepository(IDriver driver/*, ILogger<RecommendationGraphService> logger*/)
        {
            _driver = driver;
            //_logger = logger;
        }

        // -----------------------------
        // Create Recipe
        // -----------------------------
        public async Task CreateRecipeAsync(RecipeCreated evt)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.ExecuteWriteAsync(async tx =>
                {
                    // Create recipe node
                    await tx.RunAsync(
                        @"MERGE (r:Recipe {id: $id})
                          SET r.name = $name",
                        new { id = evt.RecipeId, name = evt.Name }
                    );

                    // For each ingredient: MERGE + CREATE RELATIONSHIP
                    foreach (var ingredient in evt.Ingredients)
                    {
                        await tx.RunAsync(
                            @"MERGE (i:Ingredient {name: $ingredient})
                              MERGE (r:Recipe {id: $id})-[:USES]->(i)",
                            new { ingredient, id = evt.RecipeId }
                        );
                    }
                });

                //_logger.LogInformation("Recipe {Id} created and projected into graph.", evt.RecipeId);
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        // -----------------------------
        // Update Recipe
        // -----------------------------
        public async Task UpdateRecipeAsync(RecipeUpdated evt)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.ExecuteWriteAsync(async tx =>
                {
                    // Update recipe name
                    await tx.RunAsync(
                        @"MATCH (r:Recipe {id: $id})
                          SET r.name = $newName",
                        new { id = evt.RecipeId, newName = evt.NewName }
                    );

                    // Remove old ingredient relationships
                    await tx.RunAsync(
                        @"MATCH (r:Recipe {id: $id})-[rel:USES]->(:Ingredient)
                          DELETE rel",
                        new { id = evt.RecipeId }
                    );

                    // Re-add relationships
                    foreach (var ingredient in evt.NewIngredients)
                    {
                        await tx.RunAsync(
                            @"MERGE (i:Ingredient {name: $ingredient})
                              MERGE (r:Recipe {id: $id})-[:USES]->(i)",
                            new { ingredient, id = evt.RecipeId }
                        );
                    }
                });

                //_logger.LogInformation("Recipe {Id} updated in graph.", evt.RecipeId);
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        // -----------------------------
        // Delete Recipe
        // -----------------------------
        public async Task DeleteRecipeAsync(string recipeId)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.ExecuteWriteAsync(tx =>
                    tx.RunAsync(
                        @"MATCH (r:Recipe {id: $id})
                          DETACH DELETE r",
                        new { id = recipeId }
                    )
                );

                //_logger.LogInformation("Recipe {Id} deleted from graph.", recipeId);
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}
