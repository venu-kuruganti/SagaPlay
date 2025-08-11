using CatalogService.Models;
using CatalogService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CatalogService.Services
{
    public class InternalCatalogService : IInternalCatalogService
    {
        private readonly ICatalogRepository _repository;
        public InternalCatalogService(ICatalogRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddNewCastMemberAsync(CastMember member)
        {
            return await _repository.CreateNewCastMember(member);
        }

        public async Task<bool> AddNewContentAsync(ContentItem item)
        {
            return await _repository.AddNewContentItem(item);
        }

        public async Task<bool> DeleteContentAsync(int itemId)
        {
            var items = await _repository.GetAll();
            var item = items.Where(i => i.Id == itemId).First();
            return await _repository.DeleteContentItem(item);
        }

        public async Task<List<ContentItem>> GetAllContentItemsAsync()
        {
            return await _repository.GetAll();
        }

        private async Task<List<ContentItem>> GetContentBasedOnParam(string title="", 
            string director = "", 
            string genre = "",
            List<CastMember>? castMembers = null,
            string releaseDate = "")
        {
            List<ContentItem> items = await GetAllContentItemsAsync();
           

            if (!string.IsNullOrEmpty(title))
            {
                return items.Where(c => c.Title.Contains(title)).ToList(); //Searching for Lord of The Rings returns all three movies.
            }

            if (!string.IsNullOrEmpty(director))
            {
                return items.Where(c => c.Director.ToLower() == director.ToLower()).ToList(); //One director can have directed more than one movie!
            }

            if (!string.IsNullOrEmpty(genre))
            {
                return items.Where(c => c.Genre == genre).ToList();
            }

            if (castMembers != null)
            {
                var matchingItems = items.Where(i => i.MainCast.Any(c => castMembers.Contains(c)));
                return matchingItems.ToList(); //This is obvious why I'm returning a list. Tom Cruise was in HOW MANY movies again?
            }

            if (!string.IsNullOrEmpty(releaseDate))
            {
                return items.Where(c => c.ReleaseDate == DateTime.Parse(releaseDate)).ToList();
            }            

            return items;
        }

      

        public async Task<List<ContentItem>> GetContentByDirectorAsync(string director)
        {
            return await GetContentBasedOnParam(director: director);
        }

        public async Task<List<ContentItem>> GetContentByGenreAsync(string genre)
        {
            return await GetContentBasedOnParam(genre: genre);
        }

        public async Task<ContentItem> GetContentByIdAsync(int id)
        {
            var items = await GetAllContentItemsAsync();
            return items!.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<List<ContentItem>> GetContentByOneOrMoreCastMembersAsync(List<CastMember> castMembers)
        {
            return await GetContentBasedOnParam(castMembers: castMembers);
        }

        public async Task<List<ContentItem>> GetContentByReleaseDateAsync(DateTime releasedate)
        {
            return await GetContentBasedOnParam(releaseDate: releasedate.ToString());
        }

        public async Task<List<ContentItem>> GetContentByTitleAsync(string title)
        { 
            return await GetContentBasedOnParam(title: title);
        }

        public async Task<bool> UpdateCastMemberAsync(CastMember member)
        {
            return await _repository.UpdateCastMember(member);
        }

        public async Task<bool> UpdateContentAsync(ContentItem item)
        {
            return await _repository.UpdateContentItem(item);
        }

        public async Task<bool> DeleteCastMemberAsync(CastMember member)
        {
            return await _repository.DeleteCastMember(member);
        }

        public async Task<List<CastMember>> GetCastMembers()
        {
            return await _repository.GetCastMembers();
        }
    }
}
