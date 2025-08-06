using CatalogService.Models;
using CatalogService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IInternalCatalogService _catalogService;
        public CatalogController(IInternalCatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("Content")]       
        public async Task<IActionResult> GetAll()
        {
            var items = await _catalogService.GetAllContentItemsAsync();

            return Ok(items);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            ContentItem item = new ContentItem();

            return Ok(item);
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            List<ContentItem> items = new List<ContentItem>();

            return Ok(items);
        }

        [HttpGet("GetByGenre")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            List<ContentItem> items = new List<ContentItem>();

            return Ok(items);
        }

        [HttpGet("GetByReleaseDate")]
        public async Task<IActionResult> GetByReleaseDate(string releaseDate)
        {
            List<ContentItem> items = new List<ContentItem>();

            return Ok(items);
        }

        [HttpGet("GetByCastMember")]
        public async Task<IActionResult> GetByCastMember([FromBody] CastMember castMember)
        {
            List<ContentItem> items = new List<ContentItem>();

            return Ok(items);
        }

        [HttpPost("CreateContent")]
        public async Task<IActionResult> CreateContent([FromBody] ContentItem item)
        {
             item = new ContentItem();

            return Ok(true);

        }

        [HttpPost("CreateCastMember")]
        public async Task<IActionResult> CreateCastMember([FromBody] CastMember member)
        {
            member = new CastMember();

            return Ok(true);

        }

    }

}
