using System.Text.Json;
using System.Text.Json.Serialization;

namespace CatalogService.DTOs
{
    public class CastMemberDTO
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }
    }
}
