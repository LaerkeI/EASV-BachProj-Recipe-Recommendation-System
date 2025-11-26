// Use the RecipeDB database
db = db.getSiblingDB('RecipeDB');

// Insert seed documents
db.Recipes.insertMany([
    {   
        _id: ObjectId("692765fa1d2cc72ede3ed5c3"),
        name: "Spaghetti Carbonara",
        description: "Classic Italian pasta dish.",
        ingredients: ["Spaghetti", "Eggs", "Parmesan cheese", "Pancetta"],
        instructions: ["Cook pasta", "Mix ingredients", "Serve hot"],
        category: "Dinner"
    },
    {
        _id: ObjectId("692766711d2cc72ede3ed5c4"),
        name: "Chicken Curry",
        description: "Spicy Indian curry.",
        ingredients: ["Chicken", "Onions", "Tomatoes", "Spices"],
        instructions: ["Cook chicken", "Prepare sauce", "Combine"],
        category: "Dinner"
    }
]);

print("MongoDB seed data inserted successfully 🚀");
