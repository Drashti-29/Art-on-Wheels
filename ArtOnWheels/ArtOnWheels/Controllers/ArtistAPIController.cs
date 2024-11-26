using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using ArtOnWheels.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistAPIController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistAPIController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        [HttpGet(template: "List")]
        public async Task<IEnumerable<ArtistDto>> ListArtists()
        {
            return await _artistService.ListArtists();
        }

        [HttpGet("{id}")]
        public async Task<ArtistDto> GetArtist(int id)

        {
            return await _artistService.GetArtist(id);
        }
        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto)
        {
            return await _artistService.UpdateArtistDetails(id, artistDto);
        }

        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateArtist(ArtistDto artistDto)
        {
            return await _artistService.CreateArtist(artistDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteArtist(int id)
        {
            return await _artistService.DeleteArtist(id);
        }
    }
}
