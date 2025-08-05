using CatalogService.Models;
using CatalogService.Repository;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Services
{
    public class InternalCatalogService : IInternalCatalogService
    {
        private readonly CatalogRepository _repository;
        public InternalCatalogService(CatalogRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddNewCastMember(CastMember member)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddNewContent(ContentItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContent(ContentItem item)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentItem>> GetAllContentItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContentItem>> GetByDirector(string director)
        {
            throw new NotImplementedException();
            //return await _context.ContentItems.Where(c => c.Director == director).ToListAsync();
        }

        public Task<List<ContentItem>> GetByDirectorAsync(string director)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContentItem>> GetByGenre(string genre)
        {
            throw new NotImplementedException();
            // return await _context.ContentItems.Where(c => c.Genre == genre).ToListAsync();
        }

        public Task<List<ContentItem>> GetByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public async Task<ContentItem> GetById(int id)
        {
            throw new NotImplementedException();
            // return await _context.ContentItems.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<ContentItem> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContentItem>> GetByOneOrMoreCastMembers(List<CastMember> castMembers)
        {
            throw new NotImplementedException();
            //List<ContentItem> items = await GetAll();
            //var matchingItems = items.Where(i => i.MainCast.Any(c => castMembers.Contains(c)));
            //return matchingItems.ToList();
        }

        public Task<List<ContentItem>> GetByOneOrMoreCastMembersAsync(List<CastMember> castMembers)
        {
            throw new NotImplementedException();
        }

        public async Task<ContentItem> GetByTitle(string title)
        {
            throw new NotImplementedException();
            // return await _context.ContentItems.Where(c => c.Title == title).FirstOrDefaultAsync();
        }

        public Task<ContentItem> GetByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentItem>> GetContentByDirectorAsync(string director)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentItem>> GetContentByGenreAsync(string genre)
        {
            throw new NotImplementedException();
        }

        public Task<ContentItem> GetContentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentItem>> GetContentByOneOrMoreCastMembersAsync(List<CastMember> castMembers)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentItem>> GetContentByReleaseDate(DateTime releasedate)
        {
            throw new NotImplementedException();
        }

        public Task<ContentItem> GetContentByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCastMember(CastMember member)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContent(ContentItem item)
        {
            throw new NotImplementedException();
        }
    }
}
