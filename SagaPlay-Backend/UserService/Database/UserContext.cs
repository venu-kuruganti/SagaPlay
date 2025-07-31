using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Database
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserPreferences> UserPreferences { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        //Constructor
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User => UserProfile : One to one relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(u => u.User)
                .HasForeignKey<UserProfile>(u => u.UserId);

            //User => UserPreferences : One to One relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserPreferences)
                .WithOne(u => u.User)
                .HasForeignKey<UserPreferences>(k => k.UserId);

        }
    }
}
