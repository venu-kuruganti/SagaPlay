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
                return Ok(new { UserId = id });
            else
                return NotFound();
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            UserProfile profile = new UserProfile();

            //Get actual profile here
            profile = await _userService.GetProfile(id);
            ProfileDTO profileDTO = new ProfileDTO
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                EmailAddress = profile.EmailAddress,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                Bio = profile.Bio,
                DateofBirth = DateTime.Parse(profile.DateofBirth.ToString()),
                Country = profile.Country,
                PhoneNumber = profile.PhoneNumber
            };

            return Ok(profileDTO);
        }

        [HttpPatch("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDTO profile)
        {
            if (!profile.EmailAddress.Contains('@'))
            {
                return BadRequest("Required fields are missing");
            }
            else
            {
                UserProfile userProfile = await _userService.GetProfile(profile.UserId);//Get actual profile here

                if (userProfile == null)
                {
                    userProfile = new UserProfile();
                    userProfile.UserId = profile.UserId;
                }

                userProfile.FirstName = profile.FirstName;
                userProfile.LastName = profile.LastName;
                userProfile.EmailAddress = profile.EmailAddress;
                userProfile.Bio = profile.Bio;
                userProfile.ProfilePictureUrl = profile.ProfilePictureUrl;
                userProfile.PhoneNumber = profile.PhoneNumber;
                userProfile.Country = profile.Country;
                userProfile.DateofBirth =  profile.DateofBirth.ToUniversalTime();               

                var updatedUserProfile = await _userService.UpdateProfile(userProfile);

                return Ok(profile);
            }
        }

        [HttpGet("Preferences")]
        public async Task<IActionResult> GetPreferences(Guid Id)
        {
            UserPreferences preferences = new UserPreferences();

            preferences = await _userService.GetPreferences(Id);

            PreferencesDTO preferencesDTO = new PreferencesDTO
            {
                UserId = preferences.UserId,
                Theme = preferences.Theme,
                Language = preferences.Language,
                NotificationSettings = preferences.NotificationSettings,
                PlaybackQualitySettings = preferences.PlaybackQualitySettings,
                ReceiveNewsLetter = preferences.ReceiveNewsLetter
            };

            return Ok(preferencesDTO);
        }

        [HttpPatch("UpdatePreferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] PreferencesDTO newPreferences)
        {
            if (newPreferences != null )
            {
                UserPreferences preferences  = await _userService.GetPreferences(newPreferences.UserId);

                if (preferences == null)
                {
                    preferences = new UserPreferences();
                    preferences.UserId = newPreferences.UserId;
                }

                preferences.Theme = newPreferences.Theme;
                preferences.ReceiveNewsLetter = newPreferences.ReceiveNewsLetter;
                preferences.PlaybackQualitySettings = newPreferences.PlaybackQualitySettings;
                preferences.Language = newPreferences.Language;
                preferences.NotificationSettings = newPreferences.NotificationSettings;

                await _userService.SetPreferences(preferences);
                return Ok(newPreferences);
            }
            else
            {
                return BadRequest("An error happened.");
            }
                
        }
    }
}
