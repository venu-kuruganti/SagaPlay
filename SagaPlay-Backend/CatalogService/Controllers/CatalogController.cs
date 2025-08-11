using CatalogService.DTOs;
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

        [HttpGet("AllCastPeople")]
        public async Task<IActionResult> GetAllCastPeople()
        {
            var members = await _catalogService.GetCastMembers();
            return Ok(members);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            ContentItem item = new ContentItem();

            item = await _catalogService.GetContentByIdAsync(id);

            return Ok(item);
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByTitleAsync(title);

            return Ok(items);
        }


        [HttpGet("GetByDirector")]
        public async Task<IActionResult> GetByDirector(string director)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByDirectorAsync(director);

            return Ok(items);
        }

        [HttpGet("GetByGenre")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByGenreAsync(genre);

            return Ok(items);
        }

        [HttpGet("GetByReleaseDate")]
        public async Task<IActionResult> GetByReleaseDate(string releaseDate)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByReleaseDateAsync(DateTime.Parse(releaseDate));

            return Ok(items);
        }

        [HttpGet("GetByCastMember")]
        public async Task<IActionResult> GetByCastMember([FromBody] List<CastMemberDTO> castMembersDTO)
        {
            var castMembers = await _catalogService.GetCastMembers();
            castMembers = castMembers.Where(c => castMembersDTO.Any(a => a.Name == c.Name)).ToList();
            var items = await _catalogService.GetContentByOneOrMoreCastMembersAsync(castMembers);

            return Ok(items);
        }

        [HttpPost("CreateContent")]
        public async Task<IActionResult> CreateContent([FromBody] ContentItemDTO itemDTO)
        {
            List<CastMember> castMembers = await _catalogService.GetCastMembers();

            castMembers = castMembers.Where(c => itemDTO.MainCastIds.Any(i=>c.Id == i)).ToList();

            ContentItem item = new ContentItem
            {
                Title = itemDTO.Title,
                Director = itemDTO.Director,
                Genre = itemDTO.Genre,
                PlotSummary = itemDTO.PlotSummary,
                MainCast = castMembers,
                PosterURL = itemDTO.PosterURL,
                Rating = itemDTO.Rating,
                ReleaseDate = itemDTO.ReleaseDate
                
            };

            var result = await _catalogService.AddNewContentAsync(item);

            return Ok(result);

        }

        [HttpPost("CreateCastMember")]
        public async Task<IActionResult> CreateCastMember([FromBody] CastMemberDTO memberDTO)
        {
            CastMember member = new CastMember
            {
                Name = memberDTO.Name,
                Gender = memberDTO.Gender
            };

            var result = await _catalogService.AddNewCastMemberAsync(member);

            return Ok(result);

        }

        [HttpPost("DeleteContent")]
        public async Task<IActionResult> DeleteContent([FromBody] int itemId)
        {
            var result = await _catalogService.DeleteContentAsync(itemId);
            return Ok(result);
        }

        [HttpPost("DeleteCastMember")]
        public async Task<IActionResult> DeleteCastMember([FromBody] CastMember member)
        {          
            var result = await _catalogService.DeleteCastMemberAsync(member);
            return Ok(result);
        }

        [HttpPost("UpdateContent")]
        public async Task<IActionResult> UpdateContent([FromBody] ContentItem item)
        {
            var result = await _catalogService.UpdateContentAsync(item);
            return Ok(result);
        }


        [HttpPost("UpdateCastMember")]
        public async Task<IActionResult> UpdateCastMember([FromBody] CastMember member)
        {
            var result = await _catalogService.UpdateCastMemberAsync(member);
            return Ok(result);
        }
    }

}

