using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{
    /// <summary>
    /// Service class for managing artworks. Provides methods to list, get, create, update, and delete artworks.
    /// </summary>
    public class ArtworkService : IArtworkService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor that accepts the application database context for dependency injection.
        /// </summary>
        public ArtworkService(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Lists all artworks including their artist and exhibition details.
        /// </summary>
        /// <returns>List of ArtworkDto objects</returns>
        public async Task<IEnumerable<ArtworkDto>> ListArtworks()
        {
            IEnumerable<ArtworkDto> artworks = await _context.Artworks
                .Select(artwork => new ArtworkDto
                {
                    ArtworkId = artwork.ArtworkId,
                    Title = artwork.Title,
                    Description = artwork.Description,
                    Price = artwork.Price,
                    CreationYear = artwork.CreationYear,
                    ArtistId = artwork.ArtistId,
                    ArtistName = artwork.Artist.FirstName,
                    ExhibitionNames = artwork.Exhibitions.Select(exhibitiion => exhibitiion.ExhibitionName).ToList()
                }).ToListAsync();
            return artworks;
        }
        /// <summary>
        /// Retrieves a single artwork by its ID, including its artist and exhibition details.
        /// </summary>
        /// <param name="id">The ID of the artwork to retrieve</param>
        /// <returns>ArtworkDto object containing the artwork details</returns>
        public async Task<ArtworkDto> GetArtwork(int id)
        {
            // Fetch the artwork with the associated artist and exhibitions using eager loading
            var artwork = await _context.Artworks
                .Include(a => a.Artist)
                .Include(a => a.Exhibitions)
                .FirstOrDefaultAsync(a => a.ArtworkId == id);
            if (artwork == null)
            {
                return null; // or you can throw an exception if preferred
            }

            // Map the artwork entity to the ArtworkDto
            ArtworkDto artworkDto = new ArtworkDto()
            {
                ArtworkId = artwork.ArtworkId,
                Title = artwork.Title,
                Description = artwork.Description,
                Price = artwork.Price,
                CreationYear = artwork.CreationYear,
                ArtistName = artwork.Artist.FirstName,
                // Ensure Exhibitions is initialized to prevent null reference exception
                ExhibitionNames = artwork.Exhibitions.Select(exhibitiion => exhibitiion.ExhibitionName).ToList()
            };
            return artworkDto;
        }
         /// <summary>
         /// Creates a new artwork and associates it with an artist.
         /// </summary>
         /// <param name="artworkDto">Data transfer object containing artwork details</param>
         /// <returns>A ServiceResponse indicating the success or failure of the operation</returns>
        public async Task<ServiceResponse> CreateArtwork(ArtworkDto artworkDto)
        {
            ServiceResponse serviceResponse = new();

            var artist = await _context.Artists.FindAsync(artworkDto.ArtistId);
            if (artist == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Artist not found. Please provide a valid ArtistId.");
                return serviceResponse;
            }

            Artwork artwork = new Artwork()
            {
                Title = artworkDto.Title,
                CreationYear = artworkDto.CreationYear,
                Description = artworkDto.Description,
                Price = artworkDto.Price,
                ArtistId = artworkDto.ArtistId
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = artwork.ArtworkId;
            serviceResponse.Messages.Add($"Artwork created successfully for artist: {artist.FirstName} {artist.LastName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing artwork.
        /// </summary>
        /// <param name="id">The ID of the artwork to update</param>
        /// <param name="artworkDto">Data transfer object containing updated artwork details</param>
        /// <returns>A ServiceResponse indicating the success or failure of the operation</returns>
        public async Task<ServiceResponse> UpdateArtworkDetails(int id, ArtworkDto artworkDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();

            var existingArtwork = await _context.Artworks.Include(a => a.Artist).FirstOrDefaultAsync(a => a.ArtworkId == id);

            if (existingArtwork == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Artwork not found.");
                return serviceResponse;
            }
            if (artworkDto.ArtistId != 0 && artworkDto.ArtistId != existingArtwork.ArtistId)
            {
                var artist = await _context.Artists.FindAsync(artworkDto.ArtistId);
                if (artist == null)
                {
                    serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                    serviceResponse.Messages.Add("Artist not found.");
                    return serviceResponse;
                }
                existingArtwork.ArtistId = artworkDto.ArtistId;
                existingArtwork.Artist = artist;
            }
            existingArtwork.Title = artworkDto.Title;
            existingArtwork.Price = artworkDto.Price;
            existingArtwork.CreationYear = artworkDto.CreationYear;
            existingArtwork.Description = artworkDto.Description;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Artwork updated successfully. Artist: {existingArtwork.Artist.FirstName} {existingArtwork.Artist.LastName}");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes an artwork by its ID.
        /// </summary>
        /// <param name="id">The ID of the artwork to delete</param>
        /// <returns>A ServiceResponse indicating the success or failure of the operation</returns>
        public async Task<ServiceResponse> DeleteArtwork(int id)
        {
            ServiceResponse response = new();

            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Artwork cannot be deleted because it does not exist.");
                return response;
            }
            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();
            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;
        }

    }
}
