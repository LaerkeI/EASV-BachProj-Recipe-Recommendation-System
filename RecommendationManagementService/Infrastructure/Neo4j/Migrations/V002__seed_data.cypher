// ===== Ingredient Seed Data =====
MERGE (i1:Ingredient {id: "ing_001", name: "Egg"})
MERGE (i2:Ingredient {id: "ing_002", name: "Flour"})
MERGE (i3:Ingredient {id: "ing_003", name: "Milk"})
MERGE (i4:Ingredient {id: "ing_004", name: "Salt"})
MERGE (i5:Ingredient {id: "ing_005", name: "Sugar"})
MERGE (i6:Ingredient {id: "ing_006", name: "Butter"})
MERGE (i7:Ingredient {id: "ing_007", name: "Chicken Breast"})
MERGE (i8:Ingredient {id: "ing_008", name: "Garlic"})
MERGE (i9:Ingredient {id: "ing_009", name: "Rice"})
MERGE (i10:Ingredient {id: "ing_010", name: "Olive Oil"});


// ===== Recipe Seed Data =====
MERGE (r1:Recipe {id: "rec_001", name: "Pancakes"})
MERGE (r2:Recipe {id: "rec_002", name: "Garlic Butter Chicken"})
MERGE (r3:Recipe {id: "rec_003", name: "Garlic Fried Rice"});


// ===== Relationships (Recipe USES Ingredient) =====
// Pancakes
MERGE (r1)-[:USES {id: "uses_001", amount: "2 pcs"}]->(i1)
MERGE (r1)-[:USES {id: "uses_002", amount: "100 g"}]->(i2)
MERGE (r1)-[:USES {id: "uses_003", amount: "200 ml"}]->(i3)
MERGE (r1)-[:USES {id: "uses_004", amount: "1 tbsp"}]->(i5)
MERGE (r1)-[:USES {id: "uses_005", amount: "1 tbsp"}]->(i6);

// Garlic Butter Chicken
MERGE (r2)-[:USES {id: "uses_006", amount: "1 piece"}]->(i7)
MERGE (r2)-[:USES {id: "uses_007", amount: "2 cloves"}]->(i8)
MERGE (r2)-[:USES {id: "uses_008", amount: "1 tbsp"}]->(i6)
MERGE (r2)-[:USES {id: "uses_009", amount: "1 tbsp"}]->(i10)
MERGE (r2)-[:USES {id: "uses_010", amount: "1 pinch"}]->(i4);

// Garlic Fried Rice
MERGE (r3)-[:USES {id: "uses_011", amount: "1 cup"}]->(i9)
MERGE (r3)-[:USES {id: "uses_012", amount: "3 cloves"}]->(i8)
MERGE (r3)-[:USES {id: "uses_013", amount: "1 tbsp"}]->(i10)
MERGE (r3)-[:USES {id: "uses_014", amount: "1 pinch"}]->(i4);
