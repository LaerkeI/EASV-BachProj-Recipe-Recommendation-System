// Use the RecipeDB database
db = db.getSiblingDB('RecipeDB');

// Insert seed documents
db.Recipes.insertMany([
    {
        name: "Spaghetti Carbonara",
        description: "Classic Italian pasta dish.",
        ingredients: ["Spaghetti", "Eggs", "Parmesan cheese", "Pancetta"],
        instructions: ["Cook pasta", "Mix ingredients", "Serve hot"],
        category: "Dinner"
    },
    {
        name: "Chicken Curry",
        description: "Spicy Indian curry.",
        ingredients: ["Chicken", "Onions", "Tomatoes", "Spices"],
        instructions: ["Cook chicken", "Prepare sauce", "Combine"],
        category: "Dinner"
    }
]);

print("MongoDB seed data inserted successfully 🚀");
