# Use a small Linux base image
FROM alpine:3.18

# Set working directory inside the container
WORKDIR /neo4j

# Copy Neo4j scripts from host into the container
# Adjust the relative path depending on where you build from
COPY ../../ ./Neo4jTemp/

# Fix permissions
RUN chmod +x ./Neo4jTemp/neo4j_entrypoint.sh \
    && chmod 660 ./Neo4jTemp/Migrations/*.cypher

# Copy fixed files back to a volume shared with host
VOLUME ["/neo4j/output"]

# When container runs, copy files back to host
CMD ["sh", "-c", "cp -r ./Neo4jTemp/* /neo4j/output/"]
