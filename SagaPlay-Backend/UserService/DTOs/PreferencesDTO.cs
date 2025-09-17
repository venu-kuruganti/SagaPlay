using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserService.DTOs
{
    public class PreferencesDTO
    {
        [JsonPropertyName("UserId")]
        //Foreign Key to Users
        public Guid UserId { get; set; }
        [JsonPropertyName("Theme")]
        public string Theme { get; set; }

        [JsonPropertyName("Language")]
        public string Language { get; set; }

        [JsonPropertyName("NotificationSettings")]
        public string NotificationSettings { get; set; }

        [JsonPropertyName("PlayBackQualitySettings")]
        public string PlaybackQualitySettings { get; set; }

        [JsonPropertyName("ReceiveNewsLetter")]
        public bool ReceiveNewsLetter { get; set; }
    }
}
