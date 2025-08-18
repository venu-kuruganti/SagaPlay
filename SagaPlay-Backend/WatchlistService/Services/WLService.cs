using WatchlistService.Models;
using WatchlistService.Repositories;

namespace WatchlistService.Services
{
    public class WLService : IWLService
    {
        private WatchListRepository _repository;

        public WLService(WatchListRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new Watchlist for a user. May or may not be empty, hence item is optional.
        /// </summary>
        /// <param name="watchList"></param>
        /// <param name="item"></param>
        /// <returns>true if number of rows affected are greater than zero, else false.</returns>
        /// <exception cref="ApplicationException">Database Error</exception>
        public async Task<bool> CreateNewWatchListWithItems(WatchList watchList, WatchListItem? item)
        {
            bool success;

            if (item!=null)
            {
                success = await _repository.CreateWatchListItem(item);

                if (success)
                {
                    watchList.WatchListItems.Add(item);
                }                
            }

            return await _repository.CreateWatchList(watchList);
            
        }

        public Task<bool> DeleteWatchList(WatchList watchList)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWatchListItem(WatchList watchList, WatchListItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWatchListItem(WatchList watchList, WatchListItem item)
        {
            throw new NotImplementedException();
        }
    }
}
