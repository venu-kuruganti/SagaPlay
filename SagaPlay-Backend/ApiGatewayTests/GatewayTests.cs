using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
    using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApiGatewayTests
{


    public class ApiGatewayTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApiGatewayTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterRoute_ShouldAllowAnonymousUsers() // happy case
        {
            var response = await _client.PostAsync("/user/register",
                new StringContent("{\"email\":\"test@example.com\",\"password\":\"Pass123!\"}",
                System.Text.Encoding.UTF8, "application/json"));

            Assert.NotEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ProfileRoute_ShouldRequireAuthentication() // sad case
        {
            var response = await _client.GetAsync("/user/profile"); // no token

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task UnknownRoute_ShouldReturnNotFound() // edge case
        {
            var response = await _client.GetAsync("/user/doesnotexist");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
