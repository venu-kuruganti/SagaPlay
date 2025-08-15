using SagaPlay.Shared.Contracts;

namespace RecommendationService.Services
{
    public interface IRecommendationService
    {
        public Task<List<ContentItemDTO>> GetRecommendationsAsync();
    }
}
