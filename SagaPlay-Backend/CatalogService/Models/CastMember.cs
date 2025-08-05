namespace CatalogService.Models
{
    public class CastMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }


        //Navigation properties
        public ICollection<ContentItem> ContentItems { get; set; }



    }
}
