using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionAPIController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;

        /// <summary>
        /// ExhibitionAPIController manages exhibition-related API operations.
        /// This controller provides endpoints to:
        /// - List all exhibitions
        /// - Get details of a specific exhibition by ID
        /// - Add a new exhibition
        /// - Update details of an existing exhibition
        /// - Delete an exhibition by ID
        /// It utilizes dependency injection to interact with the exhibition service layer.
        /// </summary>
        /// <param name="ExhibitionService">The exhibition service interface for performing operations.</param>

        public ExhibitionAPIController(IExhibitionService ExhibitionService)
        {
            _exhibitionService = ExhibitionService;
        }

        /// <summary>
        /// Retrieves a list of all exhibitions.
        /// </summary>
        /// <returns>A collection of ExhibitionDto objects.</returns>
        /// GET: api/ExhibitionAPI
        [HttpGet(template: "List")]
        public async Task<IEnumerable<ExhibitionDto>> ListExhibitions()
        {
            return await _exhibitionService.ListExhibitions();

        }

        /// <summary>
        /// Retrieves details of a specific exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to retrieve.</param>
        /// <returns>An ExhibitionDto object containing the exhibition's details.</returns>
        [HttpGet("{id}")]
        public async Task<ExhibitionDto> GetExhibition(int id)

        {
            return await _exhibitionService.GetExhibition(id);
        }

        /// <summary>
        /// Adds a new exhibition.
        /// </summary>
        /// <param name="exhibitionDto">The exhibition details to create.</param>
        /// <returns>A ServiceResponse indicating the result of the operation.</returns>
        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateExhibition(ExhibitionDto exhibitionDto)
        {
            return await _exhibitionService.CreateExhibition(exhibitionDto);
        }

        /// <summary>
        /// Updates the details of an existing exhibition.
        /// </summary>
        /// <param name="id">The ID of the exhibition to update.</param>
        /// <param name="exhibitionDto">The updated exhibition details.</param>
        /// <returns>A ServiceResponse indicating the result of the update operation.</returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateExhibitionDetails(int id, ExhibitionDto exhibitionDto)
        {
            return await _exhibitionService.UpdateExhibitionDetails(id, exhibitionDto);
        }

        /// <summary>
        /// Deletes an exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to delete.</param>
        /// <returns>A ServiceResponse indicating the result of the deletion.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ServiceResponse> DeleteArtist(int id)
        {
            return await _exhibitionService.DeleteExhibition(id);
        }

    }
}
