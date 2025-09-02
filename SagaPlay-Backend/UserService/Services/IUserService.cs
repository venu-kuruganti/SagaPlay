using UserService.DTOs;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {       
        public Task<string> Register(RegisterDTO registerDTO);

        public Task<UserProfile> GetProfile(Guid UserId);

        public Task<UserProfile> UpdateProfile(UserProfile userProfile);

        public Task<UserPreferences> GetPreferences(Guid UserId);

        public Task<UserPreferences> SetPreferences(UserPreferences preferences);
    }
}
