using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Database
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<ContentItem> ContentItems { get; set; }

        public DbSet<CastMember> CastMembers { get; set; }


    }
}
