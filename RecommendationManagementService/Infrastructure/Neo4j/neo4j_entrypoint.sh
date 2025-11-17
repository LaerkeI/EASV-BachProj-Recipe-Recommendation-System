#!/bin/bash
set -m

# Extract password from NEO4J_AUTH formatted as neo4j/<password>
NEO4J_PASSWORD=$(echo "$NEO4J_AUTH" | cut -d'/' -f2)

echo "Using Neo4j password from NEO4J_AUTH."

# Start Neo4j in the background
/startup/docker-entrypoint.sh neo4j &

# Wait until Neo4j is ready
echo "Waiting for Neo4j to start..."
until cypher-shell -u neo4j -p "$NEO4J_PASSWORD" "RETURN 1;" > /dev/null 2>&1
do
  echo -n "."
  sleep 2
done

echo ""
echo "Neo4j is ready. Running migrations..."

# Run all migrations
for cypher_file in /var/lib/neo4j/import/Migrations/*.cypher; do
    echo "Running migration: $cypher_file"
    cypher-shell -u neo4j -p "$NEO4J_PASSWORD" --file "$cypher_file"
done

# Bring Neo4j to the foreground so the container stays alive
fg %1
