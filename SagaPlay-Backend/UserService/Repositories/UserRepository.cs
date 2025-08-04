
using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public async Task AddUserPreferencesAsync(UserPreferences preferences)
        {
            await _context.UserPreferences.AddAsync(preferences);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserProfileAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        }

        public async Task<User> GetUserbyUserNameAsync(string username)
        {
            return await _context.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<UserPreferences> GetUserPreferencesByUserIdAsync(Guid id)
        {
            return await _context.UserPreferences.Where(up => up.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(Guid id)
        {
            return await _context.UserProfiles.Where(prof => prof.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return await GetUserByIdAsync(user.Id);
        }

        public async Task<UserPreferences> UpdateUserPreferencesAsync(UserPreferences preferences)
        {
            _context.UserPreferences.Update(preferences);
            await _context.SaveChangesAsync();
            return await GetUserPreferencesByUserIdAsync(preferences.UserId);
        }

        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile profile)
        {

            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return await GetUserProfileByUserIdAsync(profile.UserId);
        }

        
    }
}
