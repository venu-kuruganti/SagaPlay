using SagaPlay.Shared.Contracts;

namespace RecommendationService.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly HttpClient _httpClient;
        private readonly Random _random;

        public RecommendationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _random = new();
        }
        

        public async Task<List<ContentItemDTO>> GetRecommendationsAsync()
        {
            //Call catalog service
            var allContent = await _httpClient.GetFromJsonAsync<List<ContentItemDTO>>( "/api/Catalog/content");

            if (allContent==null || !allContent.Any())
            {
                return new List<ContentItemDTO>();
            }

            return allContent
                .OrderBy(_ => _random.Next())
                .Take(5)
                .ToList();
        }
    }
}
