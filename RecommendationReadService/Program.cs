
using RecommendationReadService.Application.Services;
using RecommendationReadService.Infrastructure.Kafka;
using RecommendationReadService.Infrastructure.Repositories;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace RecommendationReadService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(new RedisConfiguration()
            {
                Name = "RecommendationCache",
                ConnectionString = builder.Configuration.GetConnectionString("RedisConnection")
                    ?? throw new InvalidOperationException("Missing redis connection string")

            });

            builder.Services.AddScoped<IRecommendationRequestProducer, RecommendationRequestProducer>();
            builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            builder.Services.AddScoped<IRecommendationService, RecommendationService>();

            // Hosted Kafka consumer
            builder.Services.AddHostedService<RecommendationResponseConsumer>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
