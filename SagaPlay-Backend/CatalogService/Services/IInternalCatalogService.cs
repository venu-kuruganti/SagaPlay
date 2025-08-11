using CatalogService.Models;

namespace CatalogService.Services
{
    public interface IInternalCatalogService
    {
        Task<List<ContentItem>> GetAllContentItemsAsync();
        Task<ContentItem> GetContentByIdAsync(int id);
        Task<List<ContentItem>> GetContentByTitleAsync(string title);
        Task<List<ContentItem>> GetContentByGenreAsync(string genre);
        Task<List<ContentItem>> GetContentByDirectorAsync(string director);
        Task<List<ContentItem>> GetContentByOneOrMoreCastMembersAsync(List<CastMember> castMembers);
        Task<List<ContentItem>> GetContentByReleaseDateAsync(DateTime releasedate);
        Task<bool> AddNewContentAsync(ContentItem item);
        Task<bool> AddNewCastMemberAsync(CastMember member);
        Task<bool> UpdateContentAsync(ContentItem item);
        Task<bool> UpdateCastMemberAsync(CastMember member);
        Task<bool> DeleteContentAsync(int itemId);
        Task<bool> DeleteCastMemberAsync(CastMember member);

        Task<List<CastMember>> GetCastMembers();

    }
}
