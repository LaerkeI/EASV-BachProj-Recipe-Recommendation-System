// ===== Ingredient Constraints =====
CREATE CONSTRAINT ingredient_name IF NOT EXISTS
FOR (i:Ingredient)
REQUIRE i.name IS UNIQUE;

// ===== Recipe Constraints =====
CREATE CONSTRAINT recipe_name IF NOT EXISTS
FOR (r:Recipe)
REQUIRE r.name IS UNIQUE;