// Select database (automatically creates if not exists)
db = db.getSiblingDB('RecipeDB');

// Create collection
db.createCollection('Recipes');



// Create indexes
db.Recipes.createIndex({ name: 1 });
db.Recipes.createIndex({ category: 1 });

print("MongoDB RecipeDB initialized successfully 🚀");
