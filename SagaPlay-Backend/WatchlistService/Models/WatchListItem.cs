namespace WatchlistService.Models
{
    public class WatchListItem
    {
        public int Id { get; set; }

        //One Watchlist item is associated with one content item only and that too its Id. To avoid duplication.
        public int ContentItemId { get; set; }

        public DateTime AddedOn { get; set; }

        public StatusEnum WatchStatus { get; set; }

        //Navigation Properties        
        public int WatchListId { get; set; } //FK
        public WatchList? WatchList { get; set; }
    }
}
