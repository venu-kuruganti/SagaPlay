using CatalogService.Models;

namespace CatalogService.Services
{
    public interface IInternalCatalogService
    {
        Task<List<ContentItem>> GetAllContentItemsAsync();
        Task<ContentItem> GetContentByIdAsync(int id);
        Task<ContentItem> GetContentByTitleAsync(string title);
        Task<List<ContentItem>> GetContentByGenreAsync(string genre);
        Task<List<ContentItem>> GetContentByDirectorAsync(string director);
        Task<List<ContentItem>> GetContentByOneOrMoreCastMembersAsync(List<CastMember> castMembers);
        Task<List<ContentItem>> GetContentByReleaseDate(DateTime releasedate);
        Task<bool> AddNewContent(ContentItem item);
        Task<bool> AddNewCastMember(CastMember member);
        Task<bool> UpdateContent(ContentItem item);
        Task<bool> UpdateCastMember(CastMember member);
        Task<bool> DeleteContent(ContentItem item);

    }
}
