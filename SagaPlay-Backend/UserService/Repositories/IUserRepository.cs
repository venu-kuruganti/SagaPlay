using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<UserProfile> GetUserProfileByUserIdAsync(Guid id);

        Task<UserPreferences> GetUserPreferencesByUserIdAsync(Guid id);

        Task<Guid> AddUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task AddUserProfileAsync(UserProfile profile);

        Task<UserProfile> UpdateUserProfileAsync(UserProfile profile);

        Task AddUserPreferencesAsync(UserPreferences preferences);

        Task<UserPreferences> UpdateUserPreferencesAsync(UserPreferences preferences);

        Task<User> GetUserbyUserNameAsync(string username);


    }
}
