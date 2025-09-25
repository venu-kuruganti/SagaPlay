using System.Text.Json.Serialization;

namespace SagaPlay.Shared.Contracts
{
    public class ContentItemDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("PlotSummary")]
        public string PlotSummary { get; set; }

        [JsonPropertyName("ReleaseDate")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("Genre")]
        public string Genre { get; set; }

        [JsonPropertyName("Director")]
        public string Director { get; set; }

        [JsonPropertyName("Rating")]
        public string Rating { get; set; }

        [JsonPropertyName("PosterURL")]
        public string PosterURL { get; set; }

        [JsonPropertyName("MainCast")]
        public List<CastMemberDTO> MainCast { get; set; }
    }
}
