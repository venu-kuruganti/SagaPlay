using WatchlistService.Models;

namespace WatchlistService.Repositories
{
    public interface IWatchListRepository
    {
        Task<List<WatchList>> GetAllWatchListsAsync();

        Task<WatchList?> GetWatchlistByUserIdAsync(Guid userId);

        Task<bool> CreateWatchList(WatchList watchlist);

        Task<bool> UpdateWatchList(WatchList watchlist);

        Task<bool> DeleteWatchList(Guid UserId);

        Task<bool> CreateWatchListItem(WatchListItem item);

        Task<WatchListItem> GetWatchListItem(int itemId);

        Task<bool> UpdateWatchListItem(WatchListItem item);

    }
}
