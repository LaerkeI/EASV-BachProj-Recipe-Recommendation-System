using MongoDB.Driver;
using RecipeManagementService.Application.Services;
using RecipeManagementService.Domain.Interfaces;
using RecipeManagementService.Infrastructure.Messaging;
using RecipeManagementService.Infrastructure.Repositories;

namespace RecipeManagementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var mongoConnectionString = configuration.GetConnectionString("MongoDBConnection");

                if (mongoConnectionString == null)
                    throw new Exception("MongoDBConnection was not loaded");

                return new MongoClient(mongoConnectionString);
            });

            builder.Services.AddScoped<IRecipeEventProducer, RecipeEventProducer>();

            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

            builder.Services.AddScoped<IRecipeService, RecipeService>();

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
