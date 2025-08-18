using Microsoft.EntityFrameworkCore;
using WatchlistService.Database;
using WatchlistService.Models;

namespace WatchlistService.Repositories
{
    public class WatchListRepository : IWatchListRepository
    {
        private WatchListContext _watchListContext;

        public WatchListRepository(WatchListContext watchListContext)
        {
            _watchListContext = watchListContext;
        }

        public async Task<bool> CreateWatchList(WatchList watchlist)
        {
            try
            {
                _watchListContext.WatchLists.Add(watchlist);
                return await _watchListContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new ApplicationException(string.Format("Exception occured in database : {0}", ex.Message));
            }
        }

        public async Task<bool> CreateWatchListItem(WatchListItem item)
        {
            try
            {
                _watchListContext.WatchListItems.Add(item);
                return await _watchListContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new ApplicationException(string.Format("Exception occured in database : {0}", ex.Message));
            }
        }

        public async Task<bool> DeleteWatchList(WatchList watchlist)
        {
            try
            {
                _watchListContext.WatchLists.Remove(watchlist);
                return await _watchListContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw new ApplicationException(string.Format("Exception occured in database : {0}", ex.Message)); 
            }
        }

        public async Task<List<WatchList>> GetAllWatchListsAsync()
        {
            return await _watchListContext.WatchLists.ToListAsync();
        }

        public async Task<WatchList>? GetWatchlistByUserIdAsync(Guid userId)
        {
            return await _watchListContext.WatchLists.Where(l => l.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateWatchList(WatchList watchlist)
        {
            try
            {
                _watchListContext.WatchLists.Update(watchlist);
                return await _watchListContext.SaveChangesAsync() > 0;                

            }
            catch(Exception ex)
            {

                throw new ApplicationException(string.Format("Error occurred saving to database : {0}", ex.Message)); ;
            }
        }

        public async Task<bool> UpdateWatchListItem(WatchListItem item)
        {
            try
            {
                _watchListContext.WatchListItems.Update(item);
                return await _watchListContext.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {

                throw new ApplicationException(string.Format("Error occurred saving to database : {0}", ex.Message)); ;
            }
        }
    }
}
