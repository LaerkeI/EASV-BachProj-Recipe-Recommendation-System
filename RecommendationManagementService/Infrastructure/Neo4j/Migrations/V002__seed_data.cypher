// ===== Ingredients =====
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


// ===== Recipes =====
MERGE (Pancakes:Recipe {name: "Pancakes"})
MERGE (GarlicButterChicken:Recipe {name: "Garlic Butter Chicken"})
MERGE (GarlicFriedRice:Recipe {name: "Garlic Fried Rice"})


// ===== Relationships =====
// Pancakes
MERGE (Pancakes)-[r1:USES]->(Egg) SET r1.amount = "2 pcs"
MERGE (Pancakes)-[r2:USES]->(Flour) SET r2.amount = "100 g"
MERGE (Pancakes)-[r3:USES]->(Milk) SET r3.amount = "200 ml"
MERGE (Pancakes)-[r4:USES]->(Sugar) SET r4.amount = "1 tbsp"
MERGE (Pancakes)-[r5:USES]->(Butter) SET r5.amount = "1 tbsp"


// Garlic Butter Chicken
MERGE (GarlicButterChicken)-[r6:USES]->(ChickenBreast) SET r6.amount = "1 piece"
MERGE (GarlicButterChicken)-[r7:USES]->(Garlic) SET r7.amount = "2 cloves"
MERGE (GarlicButterChicken)-[r8:USES]->(Butter) SET r8.amount = "1 tbsp"
MERGE (GarlicButterChicken)-[r9:USES]->(OliveOil) SET r9.amount = "1 tbsp"
MERGE (GarlicButterChicken)-[r10:USES]->(Salt) SET r10.amount = "1 pinch"


// Garlic Fried Rice
MERGE (GarlicFriedRice)-[r11:USES]->(Rice) SET r11.amount = "1 cup"
MERGE (GarlicFriedRice)-[r12:USES]->(Garlic) SET r12.amount = "3 cloves"
MERGE (GarlicFriedRice)-[r13:USES]->(OliveOil) SET r13.amount = "1 tbsp"
MERGE (GarlicFriedRice)-[r14:USES]->(Salt) SET r14.amount = "1 pinch"
