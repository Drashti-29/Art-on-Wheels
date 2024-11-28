using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    /// <summary>
    /// Defines the service contract for artwork-related operations.
    /// Provides methods to:
    /// - Retrieve a list of all artworks
    /// - Get details of a specific artwork by ID
    /// - Add a new artwork
    /// - Update details of an existing artwork
    /// - Delete an artwork by ID
    /// </summary>
    public interface IArtworkService
    {
        /// <summary>
        /// Retrieves a list of all artworks.
        /// </summary>
        /// <returns>A collection of <see cref="ArtworkDto"/> objects representing all artworks.</returns>
        Task<IEnumerable<ArtworkDto>> ListArtworks();

        /// <summary>
        /// Retrieves the details of a specific artwork by its ID.
        /// </summary>
        /// <param name="id">The ID of the artwork to retrieve.</param>
        /// <returns>An <see cref="ArtworkDto"/> object containing the artwork's details.</returns>
        Task<ArtworkDto> GetArtwork(int id);

        /// <summary>
        /// Updates the details of an existing artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to update.</param>
        /// <param name="artworkDto">An <see cref="ArtworkDto"/> object containing the updated artwork details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        Task<ServiceResponse> UpdateArtworkDetails(int id, ArtworkDto artworkDto);

        /// <summary>
        /// Adds a new artwork.
        /// </summary>
        /// <param name="artworkDto">An <see cref="ArtworkDto"/> object containing the artwork details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        Task<ServiceResponse> CreateArtwork(ArtworkDto artworkDto);

        /// <summary>
        /// Deletes an artwork by its ID.
        /// </summary>
        /// <param name="id">The ID of the artwork to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion operation.</returns>
        Task<ServiceResponse> DeleteArtwork(int id);
    }
}
