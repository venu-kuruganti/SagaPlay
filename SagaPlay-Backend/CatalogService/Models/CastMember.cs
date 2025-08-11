using System.Text.Json.Serialization;

namespace CatalogService.Models
{
    public class CastMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }


        //Navigation properties
        [JsonIgnore]
        public ICollection<ContentItem> ContentItems { get; set; }



    }
}
