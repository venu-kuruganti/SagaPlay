using WatchlistService.Models;


namespace WatchlistService.Services
{
    public interface IWLService
    {
        //Creating a watchlist for a user. It may or may not be empty.        
        Task<bool> CreateNewWatchList(Guid UserId);

        //Add a new watchlistitem to watchlist
        Task<bool> AddWatchListItemToWatchList(Guid UserId, int ContentItemId);

        //Updating the watchlist by changing the status of the watchlist item
        Task<bool> UpdateWatchListItem(Guid UserId, int WatchListItemId, StatusEnum status);

        //Removing a watchlist item from the watchlist when the user does a hard delete.
        Task<bool> RemoveWatchListItemFromWatchList(Guid UserId, int WatchListItemId);

        //Removing the watchlist itself.
        Task<bool> DeleteWatchList(Guid UserId);

        //Get Watchlist based on User ID
        Task<WatchList> GetWatchListOnUserId(Guid userId);
    }
}
