using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    /// <summary>
    /// Defines the service contract for artist-related operations.
    /// Provides methods to:
    /// - Retrieve a list of all artists
    /// - Get details of a specific artist by ID
    /// - Add a new artist
    /// - Update details of an existing artist
    /// - Delete an artist by ID
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>A collection of <see cref="ArtistDto"/> objects representing all artists.</returns>
        Task<IEnumerable<ArtistDto>> ListArtists();

        /// <summary>
        /// Retrieves the details of a specific artist by their ID.
        /// </summary>
        /// <param name="id">The ID of the artist to retrieve.</param>
        /// <returns>An <see cref="ArtistDto"/> object containing the artist's details.</returns>
        Task<ArtistDto> GetArtist(int id);

        /// <summary>
        /// Updates the details of an existing artist.
        /// </summary>
        /// <param name="id">The ID of the artist to update.</param>
        /// <param name="artistDto">An <see cref="ArtistDto"/> object containing the updated artist details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto);

        /// <summary>
        /// Adds a new artist.
        /// </summary>
        /// <param name="artistDto">An <see cref="ArtistDto"/> object containing the artist details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        Task<ServiceResponse> CreateArtist(ArtistDto artistDto);

        /// <summary>
        /// Deletes an artist by their ID.
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion operation.</returns>
        Task<ServiceResponse> DeleteArtist(int id);
    }
}
