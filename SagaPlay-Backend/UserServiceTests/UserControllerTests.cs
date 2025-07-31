using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.Models;
using Xunit;
using UserService.Services;
using Moq;

namespace UserServiceTests
{
    public class UserControllerTests
    {
        private UserController controller;
        private readonly Mock<IUserService> mockUserService;
       

        public UserControllerTests()
        {
            mockUserService = new Mock<IUserService>();                       
        }

        [Theory]
        [InlineData("VenuK", "Silver")]
        public void Login_WithProperCredentials_ReturnsOK(string username, string password)
        {
            //Arrange
            mockUserService.Setup(s => s.Login("VenuK", "Silver")).Returns(true);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.Login(username, password);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("Ghifa", "PASSword$1")]
        public void Login_WithImproperProperCredentials_ReturnsOK(string username, string password)
        {
            //Arrange
            mockUserService.Setup(s => s.Login("Ghifa", "PASSword$1")).Returns(false);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.Login(username, password);

            //Assert
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }

        [Theory]
        [InlineData("TestUser", "Pwd1")]
        public void CallingRegisterUser_WithValidData_ReturnsOK(string username, string password)
        {
            //Arrange
            mockUserService.Setup(s => s.Register("TestUser", "Pwd1")).Returns(true);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.Register(username, password);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("TestUser", "")]
        [InlineData("", "Whoopsie!")]
        public void CallingRegisterUser_WithInvalidData_ReturnsBadRequest(string username, string password)
        {
            //Arrange
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.Register(username, password);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>(); //Controller should catch invalid input and not send it to the service.
            mockUserService.Verify(s => s.Register(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("Venu", "Silver")]
        public void CallingRegisterUser_WithExistingUserName_ReturnsConflictResult(string username, string password)
        {
            //Arrange
            mockUserService.Setup(s => s.Register("Venu", "Silver")).Returns(false);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.Register(username, password);

            //Assert
            result.Should().BeOfType<ConflictObjectResult>();
            var conflictResult = result as ConflictObjectResult;
            conflictResult!.StatusCode.Should().Be(409);
            conflictResult!.Value.Should().Be("Username already exists");
        }



        [Theory]
        [InlineData("Venu", "Kuruganti", "venu@yahoo.com", "01/05/1982", "Just another guy who loves movies", "India", "7678039054", "www.picurl.com")]
        public void CallingUpdateProfile_WithProperData_ReturnsOk(string firstname, string lastname, string emailaddress, string dob, string bio, string country, string phone, string profilepicurl)
        {
            //Arrange
            UserProfile profile = new UserProfile
            {
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailaddress,
                DateofBirth = DateTime.Parse(dob),
                Bio = bio,
                Country = country,
                PhoneNumber = phone,
                ProfilePictureUrl = profilepicurl
            };

            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.UpdateProfile(profile);

            //Assert
            result.Should().BeOfType<OkResult>(); //Since updating is done, no information in body is being returned.
        }

        [Theory]
        [InlineData("Venu", "Kuruganti", "venu78236784jwuisdfh", "01/05/1982", "Just another guy who loves movies", "", "7678039054", "")]
        public void CallingUpdateProfile_WithImproperData_ReturnsBadRequest(string firstname, string lastname, string emailaddress, string dob, string bio, string country, string phone, string profilepicurl)
        {
            
            //Arrange
            UserProfile profile = new UserProfile
            {
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailaddress,
                DateofBirth = DateTime.Parse(dob),
                Bio = bio,
                Country = country,
                PhoneNumber = phone,
                ProfilePictureUrl = profilepicurl
            };

            controller = new UserController(mockUserService.Object);

            //Act           
            var result = controller.UpdateProfile(profile);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>(); //Controller should catch missing required fields.
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001")]
        public void CallingGetProfile_WithValidUserId_ReturnsUserProfileObject(Guid id)
        {
            //Arrange
            mockUserService.Setup(s => s.GetProfile(id)).Returns(new UserProfile { Id = id});
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.GetProfile(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            UserProfile profile = okResult.Value as UserProfile;

            profile!.Id.Should().Be("00000000-0000-0000-0000-000000000001");
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CallingGetProfile_WithEmptyUserId_ReturnsEmptyUserProfileObject(Guid id)
        {
            //Arrange
            mockUserService.Setup(s => s.GetProfile(Guid.Empty)).Returns(new UserProfile());
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.GetProfile(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            UserProfile profile = okResult.Value as UserProfile;

            profile!.Id.Should().Be("00000000-0000-0000-0000-000000000000");
            profile!.FirstName.Should().BeNullOrEmpty();
            profile!.LastName.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001")]
        public void CallingGetPreferences_WithValidUserId_ReturnsUserPreferencesObject(Guid id)
        {
            //Arrange
            UserPreferences temp = new UserPreferences();
            temp.Id = id;
            temp.Theme = "Light";

            mockUserService.Setup(s => s.GetPreferences(id)).Returns(temp);
            controller = new UserController(mockUserService.Object);
            
            //Act
            var result = controller.GetPreferences(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result! as OkObjectResult;

            UserPreferences userPreferences = okResult!.Value as UserPreferences;
            userPreferences!.Id = id;
            userPreferences!.Theme.Should().Be("Light"); //Default theme
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CallingGetPreferences_WithEmptyUserId_ReturnsEmptyUserPreferencesObject(Guid id)
        {
            //Arrange
            UserPreferences temp = new UserPreferences();
            temp.Id = Guid.Empty;            

            mockUserService.Setup(s => s.GetPreferences(id)).Returns(temp);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.GetPreferences(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result! as OkObjectResult;

            UserPreferences userPreferences = okResult!.Value as UserPreferences;
            userPreferences!.Theme.Should().BeNullOrEmpty(); //No theme.
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001", "DarkTheme")]
        public void CallingUpdatePreferences_WithAnyData_ReturnsOkResult(Guid Id, string theme)
        {
            //Arrange
            UserPreferences preferences = new UserPreferences();
            preferences.Id = Id;
            preferences.Theme = theme;
            mockUserService.Setup(s => s.SetPreferences(preferences)).Returns(true);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = controller.UpdatePreferences(preferences);

            //Assert
            result.Should().BeOfType<OkResult>(); //Not returning anything in body.
        }






    }
}
