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
using UserService.DTOs;

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
        [InlineData("TestUser", "Pwd1")]
        public async Task CallingRegisterUser_WithValidData_ReturnsOK(string username, string password)
        {
            //Arrange
            RegisterDTO registerDTO = new RegisterDTO { UserName = username, Password = password };
            mockUserService.Setup(s => s.Register(registerDTO)).ReturnsAsync(Guid.Parse("00000000-0000-0000-0000-000000000001"));
            controller = new UserController(mockUserService.Object);
            

            //Act
            var result = await controller.Register(registerDTO);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData("TestUser", "")]
        [InlineData("", "Whoopsie!")]
        public async Task CallingRegisterUser_WithInvalidData_ReturnsBadRequest(string username, string password)
        {
            //Arrange
            controller = new UserController(mockUserService.Object);
            RegisterDTO registerDTO = new RegisterDTO { UserName = username, Password = password };

            //Act
            var result = await controller.Register(registerDTO);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>(); //Controller should catch invalid input and not send it to the service.
            mockUserService.Verify(s => s.Register(new RegisterDTO()), Times.Never);
        }

        [Theory]
        [InlineData("Venu", "Silver")]
        public async Task CallingRegisterUser_WithExistingUserName_ReturnsConflictResult(string username, string password)
        {
            //Arrange
            RegisterDTO registerDTO = new RegisterDTO { UserName = username, Password = password };
            mockUserService.Setup(s => s.Register(registerDTO)).ReturnsAsync(Guid.Empty);
            controller = new UserController(mockUserService.Object);


            //Act
            var result = await controller.Register(registerDTO);

            //Assert
            result.Should().BeOfType<ConflictObjectResult>();
            var conflictResult = result as ConflictObjectResult;
            conflictResult!.StatusCode.Should().Be(409);
            conflictResult!.Value.Should().Be("Username already exists");
        }



        [Theory]
        [InlineData("Venu", "Kuruganti", "venu@yahoo.com", "01/05/1982", "Just another guy who loves movies", "India", "7678039054", "www.picurl.com")]
        public async Task CallingUpdateProfile_WithProperData_ReturnsOk(string firstname, string lastname, string emailaddress, string dob, string bio, string country, string phone, string profilepicurl)
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
            var result = await controller.UpdateProfile(profile);

            //Assert
            result.Should().BeOfType<OkObjectResult>(); 
        }

        [Theory]
        [InlineData("Venu", "Kuruganti", "venu78236784jwuisdfh", "01/05/1982", "Just another guy who loves movies", "", "7678039054", "")]
        public async Task CallingUpdateProfile_WithImproperData_ReturnsBadRequest(string firstname, string lastname, string emailaddress, string dob, string bio, string country, string phone, string profilepicurl)
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
            var result = await controller.UpdateProfile(profile);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>(); //Controller should catch missing required fields.
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001")]
        public async Task CallingGetProfile_WithValidUserId_ReturnsUserProfileObject(Guid id)
        {
            //Arrange
            mockUserService.Setup(s => s.GetProfile(id)).ReturnsAsync(new UserProfile { UserId = id});
            controller = new UserController(mockUserService.Object);

            //Act
            var result = await controller.GetProfile(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            UserProfile profile = okResult.Value as UserProfile;

            profile!.UserId.Should().Be("00000000-0000-0000-0000-000000000001");
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task CallingGetProfile_WithEmptyUserId_ReturnsEmptyUserProfileObject(Guid id)
        {
            //Arrange
            mockUserService.Setup(s => s.GetProfile(Guid.Empty)).ReturnsAsync(new UserProfile());
            controller = new UserController(mockUserService.Object);

            //Act
            var result = await controller.GetProfile(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            UserProfile profile = okResult.Value as UserProfile;

            profile!.UserId.Should().Be("00000000-0000-0000-0000-000000000000");
            profile!.FirstName.Should().BeNullOrEmpty();
            profile!.LastName.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001")]
        public async Task CallingGetPreferences_WithValidUserId_ReturnsUserPreferencesObject(Guid id)
        {
            //Arrange
            UserPreferences temp = new UserPreferences();
            temp.UserId = id;
            temp.Theme = "Light";

            mockUserService.Setup(s => s.GetPreferences(id)).ReturnsAsync(temp);
            controller = new UserController(mockUserService.Object);
            
            //Act
            var result = await controller.GetPreferences(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result! as OkObjectResult;

            UserPreferences userPreferences = okResult!.Value as UserPreferences;
            userPreferences!.UserId = id;
            userPreferences!.Theme.Should().Be("Light"); //Default theme
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task CallingGetPreferences_WithEmptyUserId_ReturnsEmptyUserPreferencesObjectAsync(Guid id)
        {
            //Arrange
            UserPreferences temp = new UserPreferences();
            temp.UserId = Guid.Empty;            

            mockUserService.Setup(s => s.GetPreferences(id)).ReturnsAsync(temp);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = await controller.GetPreferences(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result! as OkObjectResult;

            UserPreferences userPreferences = okResult!.Value as UserPreferences;
            userPreferences!.Theme.Should().BeNullOrEmpty(); //No theme.
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000001", "DarkTheme")]
        public async Task CallingUpdatePreferences_WithAnyData_ReturnsOkResult(Guid Id, string theme)
        {
            //Arrange
            UserPreferences preferences = new UserPreferences();
            preferences.UserId = Id;
            preferences.Theme = theme;
            mockUserService.Setup(s => s.SetPreferences(preferences)).ReturnsAsync(preferences);
            controller = new UserController(mockUserService.Object);

            //Act
            var result = await controller.UpdatePreferences(preferences);

            //Assert
            result.Should().BeOfType<OkObjectResult>(); 
        }






    }
}
