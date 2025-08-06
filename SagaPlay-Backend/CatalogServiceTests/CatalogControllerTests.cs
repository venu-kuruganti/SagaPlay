using Moq;
using FluentAssertions;
using Xunit;
using CatalogService.Controllers;
using CatalogService.Models;
using CatalogService.Services;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;


namespace CatalogServiceTests
{
    
    public class CatalogControllerTests
    {
        private readonly Mock<IInternalCatalogService> _catalogServiceMock;
        private CatalogController _controller;
        public CatalogControllerTests()
        {
            _catalogServiceMock = new Mock<IInternalCatalogService>();
        }

        [Fact]
        public async Task CallingGetAll_WithNoParams_ReturnsListofContents()
        {
            //Arrange
            _catalogServiceMock.Setup(c => c.GetAllContentItemsAsync()).ReturnsAsync(MakeList());

            _controller = new CatalogController(_catalogServiceMock.Object);

            //Act
            var result = await _controller.GetAll();

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var OkResult = result as OkObjectResult;

            OkResult!.Value.Should().BeOfType<List<ContentItem>>();

            var items = OkResult!.Value as List<ContentItem>;
            items.Count.Should().NotBe(0);           


        }

        [Theory]
        [InlineData(1)]
        public async Task CallingGetContentById_ReturnsSingleContentItem(int id)
        {
            //Arrange
            var items = MakeList();
            _catalogServiceMock.Setup(c => c.GetContentByIdAsync(id)).ReturnsAsync(items.Where(i => i.Id == id).FirstOrDefault());
            _controller = new CatalogController(_catalogServiceMock.Object);

            //Act
            
        }

        private List<ContentItem> MakeList()
        {
            List<ContentItem> items = new List<ContentItem>();

            ContentItem item = new ContentItem();
            item.Id = 1;
            item.Title = "Test1";
            item.Director = "DIrector1";

            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 10, Name = "Actor 1", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 11, Name = "Actress 1", Gender = "Female" });
            
            items.Add(item);

            item = new ContentItem();
            item.Id = 2;
            item.Title = "Test2";
            item.Director = "DIrector2";

            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 12, Name = "Actor 2", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 13, Name = "Actress 2", Gender = "Female" });


            items.Add(item);

            item = new ContentItem();
            item.Id = 3;
            item.Title = "Test3";
            item.Director = "DIrector1";

            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 10, Name = "Actor 1", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 13, Name = "Actress 2", Gender = "Female" });

            items.Add(item);


            item = new ContentItem();
            item.Id = 4;
            item.Title = "Test4";
            item.Director = "DIrector3";

            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 12, Name = "Actor 2", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 11, Name = "Actress 1", Gender = "Female" });

            items.Add(item);

            item = new ContentItem();
            item.Id = 5;
            item.Title = "Test5";
            item.Director = "DIrector4";

            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 14, Name = "Actor 3", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 15, Name = "Actress 5", Gender = "Female" });

            items.Add(item);

            return items;
        }
        
    }
}