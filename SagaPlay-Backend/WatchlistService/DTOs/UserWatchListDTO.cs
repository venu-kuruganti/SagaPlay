using System.Text.Json.Serialization;

namespace WatchlistService.DTOs
{
    public class UserWatchListDTO
    {
        [JsonPropertyName("WatchListId")]
        public int WatchListId { get; set; }

        [JsonPropertyName("WatchListItems")]
        public List<WatchListItemDTO> WatchListItems { get; set; }
    }
}
