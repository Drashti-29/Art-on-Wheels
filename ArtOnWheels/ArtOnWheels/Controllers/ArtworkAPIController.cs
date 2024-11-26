using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworkAPIController : ControllerBase
    {
        private readonly IArtworkService _artworkService;

        public ArtworkAPIController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }
        [HttpGet(template: "List")]
        public async Task<IEnumerable<ArtworkDto>> ListArtworks()
        {
            return await _artworkService.ListArtworks();
        }

        [HttpGet("{id}")]
        public async Task<ArtworkDto> GetArtwork(int id)

        {
            return await _artworkService.GetArtwork(id);
        }

        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateArtworkDetails(int id, ArtworkDto artworkDto)
        {
            return await _artworkService.UpdateArtworkDetails(id, artworkDto);
        }

        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateArtwork(ArtworkDto artworkDto)
        {
            return await _artworkService.CreateArtwork(artworkDto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteArtwork(int id)
        {
            return await _artworkService.DeleteArtwork(id);
        }



    }
}
