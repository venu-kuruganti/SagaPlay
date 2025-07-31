using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Login([FromBody] string username, string password)
        {
            //If username and password are correct, send back Ok. Else error.
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && this.userService.Login(username, password))
            {
                return Ok("Token");
            }
            else
            {
                return Unauthorized("Username or password don't match!");
            }
        }

        public IActionResult Register([FromBody] string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return BadRequest("Missing fields!");
            }

            //Create new user in database
            var success = userService.Register(username, password);

            if (success)
            {
                return Ok("Token");
            }
            else
            {
                return Conflict("Username already exists");
            }
           
        }

        public IActionResult GetProfile(Guid id)
        {
            UserProfile profile = new UserProfile();

            //Get actual profile here
            profile = userService.GetProfile(id);

            return Ok(profile);
        }

        public IActionResult UpdateProfile([FromBody] UserProfile profile)
        {
            if (!profile.EmailAddress.Contains('@'))
            {
                return BadRequest("Required fields are missing");
            }
            else
            {
                userService.UpdateProfile(profile);

                return Ok();
            }
        }

        public IActionResult GetPreferences(Guid Id)
        {
            UserPreferences preferences = new UserPreferences();

            preferences = userService.GetPreferences(Id);

            return Ok(preferences);
        }

        public IActionResult UpdatePreferences(UserPreferences newPreferences)
        {
            if (newPreferences != null && userService.SetPreferences(newPreferences))
            {                
                return Ok();
            }
            else
            {
                return BadRequest("An error happened.");
            }
                
        }
    }
}
