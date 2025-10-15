using Moq;
using FluentAssertions;
using Xunit;
using CatalogService.Controllers;
using CatalogService.Models;
using CatalogService.Services;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using SagaPlay.Shared.Contracts;

namespace CatalogServiceTests
{
    
    public class CatalogControllerTests
    {
        private readonly Mock<IInternalCatalogService> _catalogServiceMock;
        private CatalogController? _controller;
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
            var result = await _controller.GetById(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var Okresult = result as OkObjectResult;

            Okresult!.Value.Should().BeOfType<ContentItem>();            
        }

        [Theory]
        [InlineData("Test1", "title")]
        [InlineData("Genre2", "genre")]
        [InlineData("Director3", "director")]
        [InlineData("11/11/2003", "releasedate")]
        [InlineData("Actor 1", "maincast")]
        public async Task CallingGetContentByX_ReturnsSingleContentItem(string param, string typeofcall)
        {
            //Arrange
            var items = MakeList();
            var castMembers = MakeCastMembers();
            IActionResult result;

            //Act
            switch (typeofcall)
            {
                default:
                case "title":
                    {
                        _catalogServiceMock.Setup(c => c.GetContentByTitleAsync(param)).ReturnsAsync(items.Where(i => i.Title == param).ToList());
                        _controller = new CatalogController(_catalogServiceMock.Object);
                        result = await _controller.GetByTitle(param);                     

                        break;
                    }

                case "genre": {
                        _catalogServiceMock.Setup(c => c.GetContentByGenreAsync(param)).ReturnsAsync(items.Where(i => i.Genre == param).ToList());
                        _controller = new CatalogController(_catalogServiceMock.Object);
                        result = await _controller.GetByGenre(param);
                        break;
                    }

                case "director": {
                        _catalogServiceMock.Setup(c => c.GetContentByDirectorAsync(param)).ReturnsAsync(items.Where(i => i.Director == param).ToList());
                        _controller = new CatalogController(_catalogServiceMock.Object);
                       result = await _controller.GetByDirector(param);
                        break;
                    }

                case "releasedate": {
                        _catalogServiceMock.Setup(c => c.GetContentByReleaseDateAsync( DateTime.Parse(param))).ReturnsAsync(items.Where(i => i.ReleaseDate == DateTime.Parse(param)).ToList());
                        _controller = new CatalogController(_catalogServiceMock.Object);
                        result = await _controller.GetByReleaseDate(param);
                        break; 
                    }

                case "maincast": {
                        var filteredCastMembers = castMembers.Where(c => c.Name == param).ToList();
                        List<CastMemberDTO> memberDTOs = new List<CastMemberDTO>();

                        foreach (var member in filteredCastMembers)
                        {
                            CastMemberDTO memberDTO = new CastMemberDTO
                            {
                                Name = member.Name,
                                Gender = member.Gender
                            };

                            memberDTOs.Add(memberDTO);
                        }

                        _catalogServiceMock.Setup(c => c.GetContentByOneOrMoreCastMembersAsync(filteredCastMembers)).ReturnsAsync(items.Where(i => i.MainCast.Any(c => filteredCastMembers.Contains(c))).ToList());
                        _controller = new CatalogController(_catalogServiceMock.Object);
                        result = await _controller.GetByCastMember(memberDTOs);
                        break; 
                    }
               
                   
            }

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            var contentItems = okResult!.Value as List<ContentItem>;
            contentItems.Count.Should().NotBe(null);
        }

        private List<ContentItem> MakeList()
        {
            List<ContentItem> items = new List<ContentItem>();

            ContentItem item = new ContentItem();
            item.Id = 1;
            item.Title = "Test1";
            item.Director = "DIrector1";
            item.Genre = "Genre1";
            item.ReleaseDate = DateTime.Parse("01/10/1996");
            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 10, Name = "Actor 1", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 11, Name = "Actress 1", Gender = "Female" });
            
            items.Add(item);

            item = new ContentItem();
            item.Id = 2;
            item.Title = "Test2";
            item.Director = "DIrector2";
            item.Genre = "Genre2";
            item.ReleaseDate = DateTime.Parse("11/11/2003");
            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 12, Name = "Actor 2", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 13, Name = "Actress 2", Gender = "Female" });


            items.Add(item);

            item = new ContentItem();
            item.Id = 3;
            item.Title = "Test3";
            item.Director = "DIrector1";
            item.Genre = "Genre3";
            item.ReleaseDate = DateTime.Parse("21/12/2006");
            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 10, Name = "Actor 1", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 13, Name = "Actress 2", Gender = "Female" });

            items.Add(item);


            item = new ContentItem();
            item.Id = 4;
            item.Title = "Test4";
            item.Director = "DIrector3";
            item.Genre = "Genre1";
            item.ReleaseDate = DateTime.Parse("21/12/2012");
            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 12, Name = "Actor 2", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 11, Name = "Actress 1", Gender = "Female" });

            items.Add(item);

            item = new ContentItem();
            item.Id = 5;
            item.Title = "Test5";
            item.Director = "DIrector4";
            item.Genre = "Genre2";
            item.ReleaseDate = DateTime.Parse("01/10/2019");
            item.MainCast = new List<CastMember>();
            item.MainCast.Add(new CastMember { Id = 14, Name = "Actor 3", Gender = "Male" });
            item.MainCast.Add(new CastMember { Id = 15, Name = "Actress 5", Gender = "Female" });

            items.Add(item);

            return items;
        }

        private List<CastMember> MakeCastMembers()
        {
            List<CastMember> members = new List<CastMember>();

            CastMember member = new CastMember
            {
                Id = 101,
                Gender = "Male",
                Name = "Actor 1"

            };

            members.Add(member);

            return members;
        }
        
    }
}