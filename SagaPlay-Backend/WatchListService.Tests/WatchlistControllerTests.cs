using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WatchlistService.Controllers;
using WatchlistService.Models;
using WatchlistService.Services;

namespace WatchListService.Tests
{
    public class WatchlistControllerTests
    {
        private readonly Mock<IWLService> mockService;
        private WatchlistController controller;

        public WatchlistControllerTests()
        {
            mockService = new Mock<IWLService>();
        }

        [Theory]
        [InlineData("e1819ef5-51b2-4d2c-a10b-8bf2f92e96bd")]
        public async Task Calling_GetWatchListOnUserId_WithValidUserId_ReturnsWatchList(string UserId)
        {
            //Arrange
            WatchList list = CreateWatchList();
            mockService.Setup(s => s.GetWatchListOnUserId(Guid.Parse(UserId))).ReturnsAsync(list);
            controller = new(mockService.Object);

            //Act
            var result = await controller.GetWatchListOnUserId(UserId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeOfType<WatchList>();
            okResult!.Value.Should().BeEquivalentTo(list);            

        }

        private WatchList CreateWatchList()
        {
            WatchList list = new WatchList
            {
                UserId = Guid.Parse("e1819ef5-51b2-4d2c-a10b-8bf2f92e96bd"),
                WatchListItems = CreateWatchListItems(),
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            return list;
        }

        private ICollection<WatchListItem> CreateWatchListItems()
        {
            Guid USERID = Guid.Parse("e1819ef5-51b2-4d2c-a10b-8bf2f92e96bd");
            List<WatchListItem> items = new List<WatchListItem>();

            WatchListItem item;

            item = new WatchListItem();
            item.Id = 1;
            item.ContentItemId = 1;
            item.UserId = USERID;
            item.AddedOn = DateTime.UtcNow;
            item.WatchStatus = StatusEnum.Planned;
            items.Add(item);

            item = new WatchListItem();
            item.Id = 2;
            item.ContentItemId = 2;
            item.UserId = USERID;
            item.AddedOn = DateTime.UtcNow;
            item.WatchStatus = StatusEnum.Watching;
            items.Add(item);

            item = new WatchListItem();
            item.Id = 3;
            item.ContentItemId = 3;
            item.UserId = USERID;
            item.AddedOn = DateTime.UtcNow;
            item.WatchStatus = StatusEnum.Completed;
            items.Add(item);

            item = new WatchListItem();
            item.Id = 4;
            item.ContentItemId = 4;
            item.UserId = USERID;
            item.AddedOn = DateTime.UtcNow;
            item.WatchStatus = StatusEnum.Watching;
            items.Add(item);

            item = new WatchListItem();
            item.Id = 5;
            item.ContentItemId = 5;
            item.UserId = USERID;
            item.AddedOn = DateTime.UtcNow;
            item.WatchStatus = StatusEnum.Planned;
            items.Add(item);

            return items;
        }
    }
}