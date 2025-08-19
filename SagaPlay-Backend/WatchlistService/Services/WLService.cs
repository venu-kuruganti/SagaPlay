using WatchlistService.Models;
using WatchlistService.Repositories;

namespace WatchlistService.Services
{
    public class WLService : IWLService
    {
        private IWatchListRepository _repository;

        public WLService(IWatchListRepository repository)
        {
            _repository = repository;
        }       

        public async Task<bool> AddWatchListItemToWatchList(Guid UserId, int ContentItemId)
        {
            var watchList = await GetWatchListOnUserId(UserId);

            if (watchList == null)
            {
                await CreateNewWatchList(UserId);
            }

            WatchListItem item = new WatchListItem();
            item.ContentItemId = ContentItemId;
            item.WatchStatus = StatusEnum.Planned;
            item.AddedOn = DateTime.UtcNow;
            item.UserId = UserId;

            await _repository.CreateWatchListItem(item);

            watchList.WatchListItems.Add(item);

            return await _repository.UpdateWatchList(watchList);            

        }

        /// <summary>
        /// Creates a new Watchlist for a user. May or may not be empty, hence item is optional.
        /// </summary>
        /// <param name="UserId"></param>        
        /// <returns>True if created successfully.</returns>
        /// <exception cref="ApplicationException">Database Error</exception>
        public async Task<bool> CreateNewWatchList(Guid UserId)
        {            
            WatchList watchList = new WatchList();
            watchList.UserId = UserId;
            watchList.CreatedOn = DateTime.UtcNow;
            watchList.UpdatedOn = DateTime.UtcNow;
            watchList.WatchListItems = new List<WatchListItem>();

            return await _repository.CreateWatchList(watchList);           
        }

        public async Task<bool> DeleteWatchList(Guid UserId)
        {
            return await _repository.DeleteWatchList(UserId);
        }

        public async Task<WatchList> GetWatchListOnUserId(Guid userId)
        {
            return await _repository.GetWatchlistByUserIdAsync(userId);
        }

        public async Task<bool> RemoveWatchListItemFromWatchList(Guid UserId, int WatchListItemId)
        {
            var watchList = await GetWatchListOnUserId(UserId);
            
            watchList.WatchListItems.Remove(watchList.WatchListItems.Where(i => i.Id == WatchListItemId).FirstOrDefault());
            
            return await _repository.UpdateWatchList(watchList);
        }

        public async Task<bool> UpdateWatchListItem(Guid UserId, int WatchListItemId, StatusEnum status)
        {
            var watchList = await GetWatchListOnUserId(UserId);
            var watchListItem = watchList.WatchListItems.Where(i => i.Id == WatchListItemId).FirstOrDefault();
            watchListItem!.WatchStatus = status;

            return await _repository.UpdateWatchList(watchList);
        }
    }
}
