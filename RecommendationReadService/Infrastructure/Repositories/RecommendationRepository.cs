using RecommendationReadService.Application.DTOs;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace RecommendationReadService.Infrastructure.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly IRedisDatabase _redis;
        private const string RedisKeyPrefix = "recommendation:";
        private readonly TimeSpan RedisTTL = TimeSpan.FromMinutes(5);

        public RecommendationRepository(IRedisClient redisClient)
        {
            _redis = redisClient.GetDb(0);
        }

        // ------------------------------------------------------------
        // Read recommendation state (polling)
        // ------------------------------------------------------------
        public async Task<RecommendationStateDto?> GetRecommendationStatus(string correlationId)
        {
            return await _redis.GetAsync<RecommendationStateDto>(RedisKeyPrefix + correlationId);
        }

        // ------------------------------------------------------------
        // Store recommendation state (IN_PROGRESS or COMPLETE)
        // ------------------------------------------------------------
        public async Task SaveRecommendationState(string correlationId, RecommendationStateDto state)
        {
            await _redis.AddAsync(RedisKeyPrefix + correlationId, state, RedisTTL);
        }
    }
}
