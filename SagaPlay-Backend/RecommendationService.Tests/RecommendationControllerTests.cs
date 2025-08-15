using Microsoft.AspNetCore.Mvc;
using Moq;
using RecommendationService.Controllers;
using RecommendationService.Services;
using SagaPlay.Shared.Contracts;

namespace RecommendationService.Tests
{
    public class RecommendationControllerTests
    {
        private Mock<IRecommendationService> _mockRecommendationService;

        public RecommendationControllerTests()
        {
            _mockRecommendationService = new Mock<IRecommendationService>();
        }

        [Fact]
        public async Task Calling_GetNext_ReturnsListOfContentItems()
        {

            //Arrange
            var fakeRecommendations = new List<ContentItemDTO>
        {
            new ContentItemDTO { Id = 1, Title = "The Lord of the Rings", Genre = "Fantasy" },
            new ContentItemDTO { Id = 2, Title = "Django", Genre = "Western" }
        };

            _mockRecommendationService
                .Setup(s => s.GetRecommendationsAsync())
                .ReturnsAsync(fakeRecommendations);

            var controller = new RecommendationController(_mockRecommendationService.Object);

            // Act
            var result = await controller.GetNext();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedItems = Assert.IsType<List<ContentItemDTO>>(okResult.Value);
            Assert.Equal(2, returnedItems.Count);
        }
    }
}