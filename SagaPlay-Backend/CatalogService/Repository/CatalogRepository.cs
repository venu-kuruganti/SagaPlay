using CatalogService.Database;
using CatalogService.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CatalogService.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogContext _context;
        
        public CatalogRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewContentItem(ContentItem item)
        {
            try
            {
                _context.ContentItems.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> CreateNewCastMember(CastMember member)
        {
            try
            {
                _context.CastMembers.Add(member);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCastMember(CastMember member)
        {
            _context.CastMembers.Remove(member);
            await _context.SaveChangesAsync();            
            return true;
        }

        public async Task<bool> DeleteContentItem(ContentItem item)
        {
            _context.ContentItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ContentItem>> GetAll()
        {
            return await _context.ContentItems
                .Include(c=>c.MainCast)
                .ToListAsync();
        }

        public async Task<List<CastMember>> GetCastMembers()
        {
            return await _context.CastMembers
                .Include(a=>a.ContentItems)
                .ToListAsync();
        }

        public async Task<bool> UpdateCastMember(CastMember member)
        {
            _context.CastMembers.Update(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateContentItem(ContentItem item)
        {
            _context.ContentItems.Update(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
