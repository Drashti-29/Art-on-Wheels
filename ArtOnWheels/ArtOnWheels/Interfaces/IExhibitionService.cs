using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface IExhibitionService
    {
        /// <summary>
        /// Retrieves a list of all exhibitions.
        /// </summary>
        /// <returns>A collection of ExhibitionDto objects.</returns>
        Task<IEnumerable<ExhibitionDto>> ListExhibitions();

        /// <summary>
        /// Retrieves details of a specific exhibition by ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to retrieve.</param>
        /// <returns>An ExhibitionDto object containing the exhibition's details.</returns>
        Task<ExhibitionDto> GetExhibition(int id);

        /// <summary>
        /// Creates a new exhibition.
        /// </summary>
        /// <param name="exhibitionDto">The details of the exhibition to create.</param>
        /// <returns>A ServiceResponse indicating the result of the creation operation.</returns>
        Task<ServiceResponse> CreateExhibition(ExhibitionDto exhibitionDto);

        /// <summary>
        /// Updates the details of an existing exhibition.
        /// </summary>
        /// <param name="id">The ID of the exhibition to update.</param>
        /// <param name="exhibitionDto">The updated exhibition details.</param>
        /// <returns>A ServiceResponse indicating the result of the update operation.</returns>
        Task<ServiceResponse> UpdateExhibitionDetails(int id, ExhibitionDto exhibitionDto);

        /// <summary>
        /// Deletes an exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to delete.</param>
        /// <returns>A ServiceResponse indicating the result of the deletion.</returns>
        Task<ServiceResponse> DeleteExhibition(int id);
    }
}
