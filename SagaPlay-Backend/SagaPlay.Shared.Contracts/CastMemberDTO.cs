using System.Text.Json;
using System.Text.Json.Serialization;

namespace SagaPlay.Shared.Contracts
{
    public class CastMemberDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }
    }
}
