using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        public bool Login(string username, string password);

        public bool Register(string username, string password);

        public UserProfile GetProfile(Guid UserId);

        public bool UpdateProfile(UserProfile userProfile);

        public UserPreferences GetPreferences(Guid UserId);

        public bool SetPreferences(UserPreferences preferences);
    }
}
