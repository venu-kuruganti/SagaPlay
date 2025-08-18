using WatchlistService.Models;

namespace WatchlistService.Services
{
    public interface IWLService
    {
        //Creating a watchlist for a user. It may or may not be empty.        
        Task<bool> CreateNewWatchListWithItems(WatchList watchList, WatchListItem? item);

        //Updating the watchlist by changing the status of the watchlist item
        Task<bool> UpdateWatchListItem(WatchList watchList, WatchListItem item);

        //Removing a watchlist item from the watchlist when the user does a hard delete.
        Task<bool> DeleteWatchListItem(WatchList watchList, WatchListItem item);

        //Removing the watchlist itself.
        Task<bool> DeleteWatchList(WatchList watchList);
    }
}
