using SagaPlay.Shared.Contracts;
using CatalogService.Models;
using CatalogService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        [HttpGet]       
        public async Task<IActionResult> GetAll()
        {
            var items = await _catalogService.GetAllContentItemsAsync();

            // Map from EF Entity to DTO
            var dtoList = items.Select( c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s=> new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()

            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("Cast")]
        public async Task<IActionResult> GetAllCastPeople()
        {
            var members = await _catalogService.GetCastMembers();
            var membersDTO = members.Select(m => new CastMemberDTO
            {
                Name = m.Name,
                Gender = m.Gender
            });

            return Ok(membersDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ContentItem item = new ContentItem();

            item = await _catalogService.GetContentByIdAsync(id);

            ContentItemDTO contentItemDTO = new ContentItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Director = item.Director,
                Genre = item.Genre,
                PlotSummary = item.PlotSummary,
                PosterURL = item.PosterURL,
                Rating = item.Rating,
                ReleaseDate = item.ReleaseDate.ToString("dd/MM/yyyy"),
                MainCast = item.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()

            };

            return Ok(contentItemDTO);
        }

        [HttpGet("GetByTitle/{title}" )]
        public async Task<IActionResult> GetByTitle(string title)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByTitleAsync(title);

            // Map from EF Entity to DTO
            var dtoList = items.Select(c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }


        [HttpGet("GetByDirector/{director}")]
        public async Task<IActionResult> GetByDirector(string director)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByDirectorAsync(director);

            // Map from EF Entity to DTO
            var dtoList = items.Select(c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("GetByGenre/{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByGenreAsync(genre);

            // Map from EF Entity to DTO
            var dtoList = items.Select(c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("GetByReleaseDate/{releaseDate}")]
        public async Task<IActionResult> GetByReleaseDate(string releaseDate)
        {
            List<ContentItem> items = new List<ContentItem>();

            items = await _catalogService.GetContentByReleaseDateAsync(DateTime.Parse(releaseDate).ToUniversalTime());

            // Map from EF Entity to DTO
            var dtoList = items.Select(c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("GetByCastMember")]
        public async Task<IActionResult> GetByCastMember([FromBody] List<CastMemberDTO> castMembersDTO)
        {
            var castMembers = await _catalogService.GetCastMembers();
            castMembers = castMembers.Where(c => castMembersDTO.Any(a => a.Name == c.Name)).ToList();
            var items = await _catalogService.GetContentByOneOrMoreCastMembersAsync(castMembers);

            // Map from EF Entity to DTO
            var dtoList = items.Select(c => new ContentItemDTO
            {
                Id = c.Id,
                Title = c.Title,
                PlotSummary = c.PlotSummary,
                ReleaseDate = c.ReleaseDate.ToString("dd/MM/yyyy"),
                Genre = c.Genre,
                Director = c.Director,
                Rating = c.Rating,
                PosterURL = c.PosterURL,
                MainCast = c.MainCast.Select(s => new CastMemberDTO
                {
                    Id = s.Id,
                    Gender = s.Gender,
                    Name = s.Name
                }).ToList()
            }).ToList();

            return Ok(dtoList);            
        }

        [HttpPost("CreateContent")]
        public async Task<IActionResult> CreateContent([FromBody] ContentItemDTO itemDTO)
        {
            List<CastMember> castMembers = await _catalogService.GetCastMembers();

            castMembers = castMembers.Where(c => itemDTO.MainCast.Any(i => i.Id == c.Id)).ToList();

            ContentItem item = new ContentItem
            {
                Title = itemDTO.Title,
                Director = itemDTO.Director,
                Genre = itemDTO.Genre,
                PlotSummary = itemDTO.PlotSummary,
                MainCast = castMembers,
                PosterURL = itemDTO.PosterURL,
                Rating = itemDTO.Rating,
                ReleaseDate = DateTime.Parse(itemDTO.ReleaseDate).ToUniversalTime()                
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
        public async Task<IActionResult> UpdateContent([FromBody] ContentItemDTO itemDTO)
        {
            ContentItem item = await _catalogService.GetContentByIdAsync(itemDTO.Id);

            //TODO : Write mapping here.

            var result = await _catalogService.UpdateContentAsync(item);
            return Ok(result);
        }


        [HttpPost("UpdateCastMember")]
        public async Task<IActionResult> UpdateCastMember([FromBody] CastMember member)
        {
            //TODO : Write mapping here
            var result = await _catalogService.UpdateCastMemberAsync(member);
            return Ok(result);
        }
    }

}

