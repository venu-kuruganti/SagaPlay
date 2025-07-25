using Xunit;
using FluentAssertions;
using Moq;
using ApiGateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace ApiGatewayTests
{
    public class RequestControllerTests
    {
        [Fact]
        public void Sending_ValidRequestToController_ReturnsOK()
        {
            //Arrange
            var controller = new RequestController();


            //Act
            var result = controller.GetRequest();

            //Assert
            result.Should().BeOfType<OkResult>();

        }
    }
}