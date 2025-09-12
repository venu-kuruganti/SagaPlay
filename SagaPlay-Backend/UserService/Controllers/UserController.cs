using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.DTOs;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registrationDetails)
        {
            if (String.IsNullOrEmpty(registrationDetails.UserEmail) || String.IsNullOrEmpty(registrationDetails.Password) || String.IsNullOrEmpty(registrationDetails.UserName))
            {
                return BadRequest("Missing fields!");
            }

            //Create new user in database
            var result = await _userService.Register(registrationDetails);

            if (result != Guid.Empty)
            {
                Response.Cookies.Append("UserId", result.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return Ok(new { message = result.ToString() });
            }
            else
            {
                return Conflict("Username already exists");
            }
           
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> GetUserIdOnUserName(string username)
        {
            var id = await _userService.GetUserId(username);

            if (id != Guid.Empty)
                return Ok(id.ToString());
            else
                return NotFound();
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
