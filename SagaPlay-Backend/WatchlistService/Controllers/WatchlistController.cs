using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchlistService.DTOs;
using WatchlistService.Models;
using WatchlistService.Services;

namespace WatchlistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private IWLService _WatchListService;
        public WatchlistController(IWLService WatchListService) 
        {
            _WatchListService = WatchListService;
        }

        //Get Watchlist for a User
        [HttpGet("GetWatchListOnUserId/${userId}")]
        public async Task<IActionResult> GetWatchListOnUserId(string userId)
        {
            var WL = await _WatchListService.GetWatchListOnUserId(Guid.Parse(userId));
            return Ok(WL);
        }

        //Add ContentItem to a watchlist based on UserId
        [HttpPost("AddToWatchList")]
        public async Task<IActionResult> AddWatchListItemToWatchList([FromBody] WatchListDTO watchListDTO)
        {

            if (!string.IsNullOrEmpty(watchListDTO.UserId))
            {
                WatchListItem item = new WatchListItem();
                item.ContentItemId = watchListDTO.ContentItemId;
                item.AddedOn = DateTime.UtcNow;

                var result = await _WatchListService.AddWatchListItemToWatchList(Guid.Parse(watchListDTO.UserId), watchListDTO.ContentItemId);

                return Ok(result);
            }
            else
            {
                return NotFound("UserId cannot be null");
            }
        }


        //Update watch status of an existing item on the list
        [HttpPatch("{userId}/items/{WatchListItemId}/UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(string userId, int WatchListItemId, StatusEnum newStatus)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                result = await _WatchListService.UpdateWatchListItem(Guid.Parse(userId), WatchListItemId, newStatus);
            }

            return Ok(result);
        }

        //Remove watchlist item from watchlist
        [HttpPatch("{userId}/items/{WatchListItemId}/RemoveWatchListItem")]
        public async Task<IActionResult> RemoveWatchListItem(string userId, int WatchListItemId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                result = await _WatchListService.RemoveWatchListItemFromWatchList(Guid.Parse(userId), WatchListItemId);
            }

            return Ok(result);
        }

        //Delete watchlist completely
        [HttpDelete("RemoveWatchList")]
        public async Task<IActionResult> DeleteWatchList(string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                result = await _WatchListService.DeleteWatchList(Guid.Parse(userId));
            }

            return Ok(result);
        }


    }
}
