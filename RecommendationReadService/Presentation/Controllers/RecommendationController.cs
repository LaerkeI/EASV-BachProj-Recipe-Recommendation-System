using Microsoft.AspNetCore.Mvc;
using RecommendationReadService.Application.Services;

namespace RecommendationReadService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        // POST: api/Recommendation
        [HttpPost]
        public async Task<IActionResult> RequestRecommendations([FromBody] List<string> ingredients)
        {
            try
            {
                if (ingredients == null || ingredients.Count == 0)
                    return BadRequest("Ingredients cannot be empty.");

                var correlationId = await _recommendationService.StartRecommendationRequest(ingredients);
                
                Console.WriteLine("After call to StartRecommendationRequest");

                return Accepted(new
                {
                    correlationId,
                    status = "PROCESSING"
                });

            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Recommendation/{correlationId}
        [HttpGet("{correlationId}")]
        public async Task<IActionResult> GetRecommendationResult(string correlationId)
        {
            var state = await _recommendationService.GetRecommendationStatus(correlationId);

            if (state == null)
                return NotFound("Unknown correlation ID");

            return Ok(state);
        }
    }
}