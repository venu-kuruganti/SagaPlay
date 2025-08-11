using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogService.Models
{
    
    public class ContentItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PlotSummary { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }        

        public string Rating { get; set; }

        public string PosterURL { get; set; }

        //Navigation properties       
        public ICollection<CastMember> MainCast { get; set; }

    }
}
