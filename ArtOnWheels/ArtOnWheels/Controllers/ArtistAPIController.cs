using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalArtShowcase.Controllers
{
    /// <summary>
    /// ArtistsAPIController handles artist-related API operations.
    /// This controller provides endpoints to:
    /// - List all artists
    /// - Get details of a specific artist by ID
    /// - Add a new artist
    /// - Update details of an existing artist
    /// - Delete an artist by ID
    /// 
    /// Implements dependency injection for service interfaces.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsAPIController : ControllerBase
    {
        private readonly IArtistService _artistService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistsAPIController"/> class.
        /// </summary>
        /// <param name="artistService">The artist service interface for performing operations.</param>
        public ArtistsAPIController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>A collection of <see cref="ArtistDto"/> objects.</returns>
        [HttpGet(template: "List")]
        public async Task<IEnumerable<ArtistDto>> ListArtists()
        {
            return await _artistService.ListArtists();
        }

        /// <summary>
        /// Retrieves details of a specific artist by their ID.
        /// </summary>
        /// <param name="id">The ID of the artist to retrieve.</param>
        /// <returns>An <see cref="ArtistDto"/> object containing the artist's details.</returns>
        [HttpGet("{id}")]
        public async Task<ArtistDto> GetArtist(int id)
        {
            return await _artistService.GetArtist(id);
        }

        /// <summary>
        /// Adds a new artist.
        /// </summary>
        /// <param name="artistDto">The artist details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateArtist(ArtistDto artistDto)
        {
            return await _artistService.CreateArtist(artistDto);
        }

        /// <summary>
        /// Updates the details of an existing artist.
        /// </summary>
        /// <param name="id">The ID of the artist to update.</param>
        /// <param name="artistDto">The updated artist details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto)
        {
            return await _artistService.UpdateArtistDetails(id, artistDto);
        }

        /// <summary>
        /// Deletes an artist by their ID.
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteArtist(int id)
        {
            return await _artistService.DeleteArtist(id);
        }
    }
}
