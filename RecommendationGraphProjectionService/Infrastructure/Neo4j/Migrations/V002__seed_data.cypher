// ============================================
// Ingredients
// ============================================
MERGE (Egg:Ingredient {name: "Egg"})
MERGE (Flour:Ingredient {name: "Flour"})
MERGE (Milk:Ingredient {name: "Milk"})
MERGE (Salt:Ingredient {name: "Salt"})
MERGE (Sugar:Ingredient {name: "Sugar"})
MERGE (Butter:Ingredient {name: "Butter"})
MERGE (ChickenBreast:Ingredient {name: "Chicken Breast"})
MERGE (Garlic:Ingredient {name: "Garlic"})
MERGE (Rice:Ingredient {name: "Rice"})
MERGE (OliveOil:Ingredient {name: "Olive Oil"})

WITH *
  
// ============================================
// Recipes
// ============================================
MERGE (Pancakes:Recipe {
    id: "6936d30f4171514e0c543c5c",
    name: "Pancakes",
    description: "Fluffy homemade pancakes.",
    instructions: ["Mix ingredients", "Cook on skillet", "Serve with syrup"],
    category: "Breakfast"
})

MERGE (GarlicButterChicken:Recipe {
    id: "6936d30f4171514e0c543c5d",
    name: "Garlic Butter Chicken",
    description: "Juicy chicken in garlic butter sauce.",
    instructions: ["Season chicken", "Cook chicken", "Add garlic butter sauce"],
    category: "Dinner"
})

MERGE (GarlicFriedRice:Recipe {
    id: "6936d30f4171514e0c543c5e",
    name: "Garlic Fried Rice",
    description: "Quick and tasty fried rice with garlic.",
    instructions: ["Cook rice", "Fry garlic", "Mix together with oil and seasoning"],
    category: "Lunch"
})

WITH *
  
// ============================================
// Pancakes Ingredient Relationships
// ============================================
MATCH (p:Recipe {id: "6936d30f4171514e0c543c5c"})
MATCH (egg:Ingredient {name: "Egg"})
MATCH (flour:Ingredient {name: "Flour"})
MATCH (milk:Ingredient {name: "Milk"})
MATCH (sugar:Ingredient {name: "Sugar"})
MATCH (butter:Ingredient {name: "Butter"})
MERGE (p)-[:USES]->(egg)
MERGE (p)-[:USES]->(flour)
MERGE (p)-[:USES]->(milk)
MERGE (p)-[:USES]->(sugar)
MERGE (p)-[:USES]->(butter)

WITH *

// ============================================
// Garlic Butter Chicken Ingredient Relationships
// ============================================
MATCH (c:Recipe {id: "6936d30f4171514e0c543c5d"})
MATCH (chicken:Ingredient {name: "Chicken Breast"})
MATCH (garlic:Ingredient {name: "Garlic"})
MATCH (butter:Ingredient {name: "Butter"})
MATCH (oil:Ingredient {name: "Olive Oil"})
MATCH (salt:Ingredient {name: "Salt"})
MERGE (c)-[:USES]->(chicken)
MERGE (c)-[:USES]->(garlic)
MERGE (c)-[:USES]->(butter)
MERGE (c)-[:USES]->(oil)
MERGE (c)-[:USES]->(salt)

WITH *

// ============================================
// Garlic Fried Rice Ingredient Relationships
// ============================================
MATCH (r:Recipe {id: "6936d30f4171514e0c543c5e"})
MATCH (rice:Ingredient {name: "Rice"})
MATCH (garlic:Ingredient {name: "Garlic"})
MATCH (oil:Ingredient {name: "Olive Oil"})
MATCH (salt:Ingredient {name: "Salt"})
MERGE (r)-[:USES]->(rice)
MERGE (r)-[:USES]->(garlic)
MERGE (r)-[:USES]->(oil)
MERGE (r)-[:USES]->(salt)
