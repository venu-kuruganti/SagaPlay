using System.Text.Json.Serialization;

namespace WatchlistService.DTOs
{
    public class WatchListItemDTO
    {
        [JsonPropertyName("WatchListItemId")]
        public int WatchListItemId { get; set; }

        [JsonPropertyName("ContentItemId")]
        public int ContentItemId { get; set; }

        [JsonPropertyName("WatchStatus")]
        public string WatchStatus { get; set; }


    }
}
