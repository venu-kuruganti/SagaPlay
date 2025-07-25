using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Controllers;

namespace ApiGatewayTests
{
    public class ResponseControllerTests
    {
        [Fact]
        public void CallingResponseController_Gives_OkResult()
        {
            //Arrange
            var controller = new ResponseController();

            //Act
            var result = controller.SendResponse();

            //Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
