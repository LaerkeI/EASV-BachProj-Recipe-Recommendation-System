using Neo4j.Driver;
using RecommendationGraphProjectionService.Application.Services;
using RecommendationGraphProjectionService.Infrastructure.Messaging;
using RecommendationGraphProjectionService.Infrastructure.Repositories;

namespace RecommendationGraphProjectionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            //Register Neo4j Driver
            builder.Services.AddSingleton<IDriver>(sp =>
            {
                var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("Neo4j");
                return GraphDatabase.Driver(
                    builder.Configuration["Neo4j:Uri"],
                    AuthTokens.Basic(
                        builder.Configuration["Neo4j:User"],
                        builder.Configuration["Neo4j:Password"]
                    )
                );
            });

            //Register RecommendationGraphService
            builder.Services.AddScoped<IRecipeGraphService, RecipeGraphService>();
            builder.Services.AddScoped<IRecipeGraphRepository, RecipeGraphRepository>();

            builder.Services.AddSingleton<IRecipeEventConsumer, RecipeEventConsumer>();
            builder.Services.AddSingleton<IRecommendationRequestConsumer, RecommendationRequestConsumer>();

            builder.Services.AddScoped<IRecommendationResponseProducer, RecommendationResponseProducer>();

            builder.Services.AddHostedService<RecipeEventConsumerService>();
            builder.Services.AddHostedService<RecommendationRequestConsumerService>();


            var host = builder.Build();
            host.Run();
        }
    }
}