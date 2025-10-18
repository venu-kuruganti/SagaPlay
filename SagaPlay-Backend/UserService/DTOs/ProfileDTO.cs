using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserService.DTOs
{
    public class ProfileDTO
    {
        [JsonPropertyName("UserId")]
        //Foreign Key to Users
        public Guid UserId { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("DateofBirth")]
        public string DateofBirth { get; set; }

        [JsonPropertyName("Bio")]
        public string Bio { get; set; }

        [JsonPropertyName("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
