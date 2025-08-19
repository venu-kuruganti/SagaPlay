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
                await _watchListContext.WatchLists.AddAsync(watchlist);
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

        public async Task<bool> DeleteWatchList(Guid UserId)
        {
            try
            {
                return await _watchListContext.WatchLists.Where(i => i.UserId == UserId).ExecuteDeleteAsync() > 0;                
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Exception occured in database : {0}", ex.Message)); 
            }
        }

        public async Task<List<WatchList>> GetAllWatchListsAsync()
        {
            return await _watchListContext.WatchLists
                .Include(l=>l.WatchListItems)
                .ToListAsync();
        }

        public async Task<WatchList>? GetWatchlistByUserIdAsync(Guid userId)
        {
            return await _watchListContext.WatchLists
                .Include(l=>l.WatchListItems)
                .Where(l => l.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<WatchListItem> GetWatchListItem(int itemId)
        {
            return await _watchListContext.WatchListItems.Where(i => i.Id == itemId).FirstOrDefaultAsync();
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
