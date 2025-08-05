using CatalogService.Models;

namespace CatalogService.Repository
{
    public interface ICatalogRepository
    {
        Task<List<ContentItem>> GetAll();        
        
        Task<bool> AddNewContentItem(ContentItem item);
        Task<bool> UpdateContentItem(ContentItem item);
        Task<bool> DeleteContentItem(ContentItem item);
        Task<bool> CreateNewCastMember(CastMember member);
        Task<bool> UpdateCastMember(CastMember member);
        Task<bool> DeleteCastMember(CastMember member);
    }
}
