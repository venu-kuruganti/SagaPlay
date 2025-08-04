using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            //If username and password are correct, send back Ok. Else error.
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && await _userService.Login(username, password))
            {
                return Ok("Token");
            }
            else
            {
                return Unauthorized("Username or password don't match!");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return BadRequest("Missing fields!");
            }

            //Create new user in database
            var success = await _userService.Register(username, password);

            if (success)
            {
                return Ok("Token");
            }
            else
            {
                return Conflict("Username already exists");
            }
           
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            UserProfile profile = new UserProfile();

            //Get actual profile here
            profile = await _userService.GetProfile(id);

            return Ok(profile);
        }

        [HttpPatch("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfile profile)
        {
            if (!profile.EmailAddress.Contains('@'))
            {
                return BadRequest("Required fields are missing");
            }
            else
            {
                var updatedUserProfile = await _userService.UpdateProfile(profile);

                return Ok(updatedUserProfile);
            }
        }

        [HttpGet("Preferences")]
        public async Task<IActionResult> GetPreferences(Guid Id)
        {
            UserPreferences preferences = new UserPreferences();

            preferences = await _userService.GetPreferences(Id);

            return Ok(preferences);
        }

        [HttpPatch("UpdatePreferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] UserPreferences newPreferences)
        {
            if (newPreferences != null )
            {
                UserPreferences newUserPreferences = await _userService.SetPreferences(newPreferences);
                return Ok(newUserPreferences);
            }
            else
            {
                return BadRequest("An error happened.");
            }
                
        }
    }
}
