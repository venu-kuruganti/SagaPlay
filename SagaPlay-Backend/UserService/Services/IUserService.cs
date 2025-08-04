using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        public Task<bool> Login(string username, string password);

        public Task<bool> Register(string username, string password);

        public Task<UserProfile> GetProfile(Guid UserId);

        public Task<UserProfile> UpdateProfile(UserProfile userProfile);

        public Task<UserPreferences> GetPreferences(Guid UserId);

        public Task<UserPreferences> SetPreferences(UserPreferences preferences);
    }
}
