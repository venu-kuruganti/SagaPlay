using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPreferences> GetPreferences(Guid Id)
        {
            return await _repository.GetUserPreferencesByUserIdAsync(Id);
        }

        public async Task<UserProfile> GetProfile(Guid UserId)
        {
           return await _repository.GetUserProfileByUserIdAsync(UserId);
        }

        public async Task<bool> Login(string username, string password)
        {
           User user = await _repository.GetUserbyUserNameAsync(username);
            bool passwordsMatch = true;

            byte[] computedHash = new byte[0];
            if (user!=null)
            {   
                var hmac = new HMACSHA512(user.PasswordSalt);
                computedHash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        passwordsMatch = false;
                        break;
                    }
                }
            }

            if (user==null || !passwordsMatch)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<bool> Register(string username, string password)
        {
            var hmac = new HMACSHA512();
            
            User user = new User { 
                UserName = username, 
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), 
                PasswordSalt=hmac.Key };

            return await _repository.AddUserAsync(user);
        }

        public async Task<UserPreferences> SetPreferences(UserPreferences preferences)
        {
            return await _repository.UpdateUserPreferencesAsync(preferences);
        }
        

        public async Task<UserProfile> UpdateProfile(UserProfile userProfile)
        {
           return await _repository.UpdateUserProfileAsync(userProfile);
        }
    }
}
