using Neo4j.Driver;
using RecommendationGraphProjectionService.Application.DTOs;
using RecommendationGraphProjectionService.Application.Events;

namespace RecommendationGraphProjectionService.Infrastructure.Repositories
{
    public class RecipeGraphRepository : IRecipeGraphRepository
    {
        private readonly IDriver _driver;

        public RecipeGraphRepository(IDriver driver)
        {
            _driver = driver;
        }

        // ---------------------------------------------------------
        // Create Recipe
        // ---------------------------------------------------------
        public async Task CreateRecipeAsync(
            string recipeId,
            string name,
            string description,
            List<string> ingredients,
            List<string> instructions,
            string category)
        {
            await using var session = _driver.AsyncSession();

            await session.ExecuteWriteAsync(async tx =>
            {
                // Create the recipe and bind it
                var cursor = await tx.RunAsync(
                    @"MERGE (r:Recipe {id: $id})
                      SET r.name = $name,
                          r.description = $description,
                          r.instructions = $instructions,
                          r.category = $category
                      RETURN r",
                    new
                    {
                        id = recipeId,
                        name,
                        description,
                        instructions = instructions.ToArray(),
                        category
                    }
                );

                var recipeNode = await cursor.SingleAsync();

                // Reuse recipe node — DO NOT MERGE AGAIN
                await tx.RunAsync(
                    @"UNWIND $ingredients AS ingredientName
                      MERGE (i:Ingredient {name: ingredientName})
                      WITH i
                      MATCH (r:Recipe {id: $id})
                      MERGE (r)-[:USES]->(i)",
                    new
                    {
                        id = recipeId,
                        ingredients
                    }
                );
            });
        }


        // ---------------------------------------------------------
        // Get Top Recommended Recipes (based on ingredient matching)
        // ---------------------------------------------------------
        public async Task<List<RecommendedRecipeDto>> GetRecommendedRecipesAsync(List<string> ingredientsFromFridge)
        {
            await using var session = _driver.AsyncSession();

            return await session.ExecuteReadAsync(async tx =>
            {
                var cursor = await tx.RunAsync(
                    @"MATCH (r:Recipe)-[:USES]->(i:Ingredient)
                      WITH r, collect(i.name) AS recipeIngredients,
                           $ingredientsFromFridge AS ingredientsFromFridge
                      WITH r,
                           recipeIngredients,
                           [x IN recipeIngredients WHERE x IN ingredientsFromFridge] AS matched,
                           [x IN recipeIngredients WHERE NOT x IN ingredientsFromFridge] AS missing
                      RETURN r.id AS recipeId,
                             r.name AS name,
                             r.description AS description,
                             r.instructions AS instructions,
                             r.category AS category,
                             recipeIngredients,
                             matched,
                             missing,
                             size(matched) AS matchCount,
                             size(recipeIngredients) AS totalIngredients,
                             (toFloat(size(matched)) / size(recipeIngredients)) AS score
                      ORDER BY score DESC, size(missing) ASC
                      LIMIT 3",
                    new { ingredientsFromFridge }
                );

                var results = new List<RecommendedRecipeDto>();

                await cursor.ForEachAsync(record =>
                {
                    results.Add(new RecommendedRecipeDto
                    {
                        RecipeId = record["recipeId"].As<string>(),
                        Name = record["name"].As<string>(),
                        Description = record["description"].As<string>(),
                        Instructions = record["instructions"].As<List<object>>().Cast<string>().ToList(),
                        Category = record["category"].As<string>(),
                        RecipeIngredients = record["recipeIngredients"].As<List<object>>().Cast<string>().ToList(),
                        MatchedIngredients = record["matched"].As<List<object>>().Cast<string>().ToList(),
                        MissingIngredients = record["missing"].As<List<object>>().Cast<string>().ToList(),
                        MatchCount = record["matchCount"].As<int>(),
                        TotalIngredients = record["totalIngredients"].As<int>(),
                        Score = record["score"].As<double>()
                    });
                });

                return results;
            });
        }


        // ---------------------------------------------------------
        // Update Recipe
        // ---------------------------------------------------------
        public async Task UpdateRecipeAsync(string recipeId,
            string updatedName,
            string updatedDescription,
            List<string> updatedIngredients,
            List<string> updatedInstructions,
            string updatedCategory)
        {
            await using var session = _driver.AsyncSession();

            await session.ExecuteWriteAsync(async tx =>
            {
                // Update recipe
                await tx.RunAsync(
                    @"MATCH (r:Recipe {id: $id})
                      SET r.name = $name,
                          r.description = $description,
                          r.instructions = $instructions,
                          r.category = $category",
                    new
                    {
                        id = recipeId,
                        name = updatedName,
                        description = updatedDescription,
                        instructions = updatedInstructions.ToArray(),
                        category = updatedCategory
                    }
                );

                // Remove ingredient relationships
                await tx.RunAsync(
                    @"MATCH (r:Recipe {id: $id})-[rel:USES]->(:Ingredient)
                      DELETE rel",
                    new { id = recipeId }
                );

                // Add new ingredient relationships
                await tx.RunAsync(
                    @"UNWIND $ingredients AS ingredientName
                      MERGE (i:Ingredient {name: ingredientName})
                      WITH i
                      MATCH (r:Recipe {id: $id})
                      MERGE (r)-[:USES]->(i)",
                    new { id = recipeId, ingredients = updatedIngredients }
                );
            });
        }


        // ---------------------------------------------------------
        // Delete Recipe
        // ---------------------------------------------------------
        public async Task DeleteRecipeAsync(string recipeId)
        {
            await using var session = _driver.AsyncSession();

            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(
                    @"MATCH (r:Recipe {id: $id})
                      DETACH DELETE r",
                    new { id = recipeId }
                );
            });
        }
    }
}
