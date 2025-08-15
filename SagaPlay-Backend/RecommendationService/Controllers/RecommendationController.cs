using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecommendationService.Services;

namespace RecommendationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("GetNext")]
        public async Task<IActionResult> GetNext()
        {
            var recommendations = await _recommendationService.GetRecommendationsAsync();

            return Ok(recommendations);
        }
    }
}
