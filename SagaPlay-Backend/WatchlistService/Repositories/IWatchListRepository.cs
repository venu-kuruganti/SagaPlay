using WatchlistService.Models;

namespace WatchlistService.Repositories
{
    public interface IWatchListRepository
    {
        Task<List<WatchList>> GetAllWatchListsAsync();

        Task<WatchList>? GetWatchlistByUserIdAsync(Guid userId);

        Task<bool> CreateWatchList(WatchList watchlist);

        Task<bool> UpdateWatchList(WatchList watchlist);

        Task<bool> DeleteWatchList(WatchList watchlist);

        Task<bool> CreateWatchListItem(WatchListItem item);

        Task<bool> UpdateWatchListItem(WatchListItem item);

    }
}
