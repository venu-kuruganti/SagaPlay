using System.Text.Json;
using System.Text.Json.Serialization;

namespace WatchlistService.DTOs
{
    public class WatchListDTO
    {
        [JsonPropertyName("UserId")]
        public string UserId { get; set; }

        [JsonPropertyName("ContentItemId")]
        public int ContentItemId { get; set; }
    }
}
