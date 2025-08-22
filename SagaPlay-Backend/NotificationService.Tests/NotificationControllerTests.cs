using Moq;
using FluentAssertions;
using NotificationService.Controllers;
using NotificationService.Utilities;
using NotificationService.Services;
using SagaPlay.Shared.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace NotificationService.Tests
{
    public class NotificationControllerTests
    {
        [Fact]
        public async Task Send_HappyPath_ReturnsOkResult()
        {
            // Arrange
            var mockDispatcher = new Mock<INotificationDispatcher>();
            mockDispatcher
                .Setup(d => d.SendAsync(It.IsAny<NotificationDTO>()))
                .Returns(Task.CompletedTask);

            var controller = new NotificationController(mockDispatcher.Object);

            var dto = new NotificationDTO
            {
                UserId = Guid.NewGuid(),
                To = "test@example.com",
                Subject = "Hello",
                Message = "Test message",
                CreatedOn = DateTime.UtcNow,
                Type = NotificationType.Email
            };

            // Act
            var result = await controller.Send(dto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo("Notification Sent to all subscribers.");
            mockDispatcher.Invocations.Should()
        .ContainSingle(invocation =>
         invocation.Method.Name == nameof(NotificationDispatcher.SendAsync) &&
         invocation.Arguments[0] == dto);
        }

        [Fact]
        public async Task Send_DispatcherThrows_ReturnsException()
        {
            // Arrange
            var mockDispatcher = new Mock<INotificationDispatcher>();
            mockDispatcher
                .Setup(d => d.SendAsync(It.IsAny<NotificationDTO>()))
                .ThrowsAsync(new InvalidOperationException("No notification service found for type."));

            var controller = new NotificationController(mockDispatcher.Object);

            var dto = new NotificationDTO
            {
                UserId = Guid.NewGuid(),
                To = "fail@example.com",
                Subject = "Oops",
                Message = "Fail test",
                CreatedOn = DateTime.UtcNow,
                Type = NotificationType.Push
            };

            // Act & Assert
            await FluentActions
            .Invoking(() => controller.Send(dto))
            .Should()
            .ThrowAsync<InvalidOperationException>()
            .WithMessage("No notification service found for type.");
        }

        [Fact]
        public async Task Send_NullMessage_ReturnsBadRequest()
        {
            // Arrange
            var mockDispatcher = new Mock<INotificationDispatcher>();
            var controller = new NotificationController(mockDispatcher.Object);

            // Act
            var result = await controller.Send(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockDispatcher.Invocations.Should().BeEmpty();
        }
    }
}