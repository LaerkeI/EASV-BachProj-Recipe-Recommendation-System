// === Ingredient Constraints ===
CREATE CONSTRAINT ingredient_id IF NOT EXISTS
FOR (i:Ingredient)
REQUIRE i.id IS UNIQUE;

CREATE CONSTRAINT ingredient_name IF NOT EXISTS
FOR (i:Ingredient)
REQUIRE i.name IS UNIQUE;

// === Recipe Constraints ===
CREATE CONSTRAINT recipe_id IF NOT EXISTS
FOR (r:Recipe)
REQUIRE r.id IS UNIQUE;

CREATE CONSTRAINT recipe_name IF NOT EXISTS
FOR (r:Recipe)
REQUIRE r.name IS UNIQUE;

// === Relationship Constraints or Indexes (Neo4j 5+) ===
// Example: ensure each USES relationship has a unique uuid if needed
CREATE CONSTRAINT uses_id IF NOT EXISTS
FOR ()-[u:USES]-()
REQUIRE u.id IS UNIQUE;