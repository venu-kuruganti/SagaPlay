using System.ComponentModel.DataAnnotations;

namespace WatchlistService.Models
{
    public class WatchList
    {
        [Key]
        public int WatchListId { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        //navigation properties
        public ICollection<WatchListItem> WatchListItems { get; set; } = new List<WatchListItem>();
        //One watchlist can contain many watchlist items.

    }
}
