using Microsoft.EntityFrameworkCore;
using WatchlistService.Models;

namespace WatchlistService.Database
{
    public class WatchListContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<WatchList> WatchLists { get; set; }

        public DbSet<WatchListItem> WatchListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Watchlist (1) => WatchListItem (Many) : One to many relationship
            modelBuilder.Entity<WatchList>()
                .HasMany(w => w.WatchListItems)
                .WithOne(i => i.WatchList)
                .HasForeignKey(i => i.WatchListId);

            //Enforce uniqueness
            modelBuilder.Entity<WatchListItem>()
                .HasIndex(w => new { w.WatchListId, w.ContentItemId })
                .IsUnique();

        }

    }
}
