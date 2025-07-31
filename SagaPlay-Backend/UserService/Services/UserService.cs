using UserService.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        public UserPreferences GetPreferences(Guid Id)
        {
            throw new NotImplementedException();
        }

        public UserProfile GetProfile(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool SetPreferences(UserPreferences preferences)
        {
            throw new NotImplementedException();
        }
        

        public bool UpdateProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
