using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Service class that handles CRUD operations for the Artist entity. 
/// Provides methods to list, create, update, and delete artists, as well as retrieve details of a single artist.
/// It works with the `ApplicationDbContext` for database interactions and uses Data Transfer Objects (DTOs) to facilitate data exchange.
/// </summary>
public class ArtistService : IArtistService
{
    private readonly ApplicationDbContext _context;

    // dependency injection of database context
    public ArtistService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a list of all artists, including their associated artworks and exhibitions.
    /// </summary>
    /// <returns>A list of ArtistDto objects representing the artists and their artworks.</returns>
    public async Task<IEnumerable<ArtistDto>> ListArtists()
    {
        List<Artist> artists = await _context.Artists
            .Include(a => a.Artworks)
            .ThenInclude(artwork => artwork.Exhibitions) // Include exhibitions for each artwork
            .ToListAsync();

        // empty list of data transfer object ArtistDto
        List<ArtistDto> artistDtos = new List<ArtistDto>();
        foreach (Artist artist in artists)
        {
            // create new instance of ArtistDto, add to list
            artistDtos.Add(new ArtistDto()
            {
                ArtistId = artist.ArtistId,
                FirstName = artist.FirstName,
                LastName = artist.LastName,
                ArtistBio = artist.ArtistBio,
                Email = artist.Email,
                ArtworkIds = artist.Artworks.Select(Artwork => Artwork.ArtworkId).ToList(),
                Artworks = artist.Artworks.Select(artwork => new ArtworkDto
                {
                    ArtworkId = artwork.ArtworkId,
                    Title = artwork.Title,
                    Description = artwork.Description,
                    CreationYear = artwork.CreationYear,
                    Price = artwork.Price,
                    ArtistId = artwork.ArtistId,
                    ArtistName = artwork.Artist.FirstName,
                    // Ensure Exhibitions is initialized to prevent null reference exception
                    ExhibitionNames = artwork.Exhibitions.Select(exhibitiion => exhibitiion.ExhibitionName).ToList()

                }).ToList()
            });
        }

        return artistDtos;
    }

    /// <summary>
    /// Retrieves the details of a specific artist by their ID, including their associated artworks and exhibitions.
    /// </summary>
    /// <param name="id">The ID of the artist to retrieve.</param>
    /// <returns>An ArtistDto object representing the artist, or null if the artist is not found.</returns>
    public async Task<ArtistDto> GetArtist(int id)
    {
        // Fetch the artist along with artworks and their related exhibitions
        var artist = await _context.Artists
            .Include(a => a.Artworks)
            .ThenInclude(artwork => artwork.Exhibitions) // Include exhibitions for each artwork
            .FirstOrDefaultAsync(a => a.ArtistId == id);

        // No artist found
        if (artist == null)
        {
            return null;
        }

        // Create an instance of ArtistDto and populate the artist details
        ArtistDto artistDto = new ArtistDto
        {
            ArtistId = artist.ArtistId,
            FirstName = artist.FirstName,
            LastName = artist.LastName,
            ArtistBio = artist.ArtistBio,
            Email = artist.Email,
            ArtworkIds = artist.Artworks.Select(Artwork => Artwork.ArtworkId).ToList(),
            Artworks = artist.Artworks.Select(artwork => new ArtworkDto
            {
                ArtworkId = artwork.ArtworkId,
                Title = artwork.Title,
                Description = artwork.Description,
                CreationYear = artwork.CreationYear,
                Price = artwork.Price,
                ArtistId = artwork.ArtistId,
                ArtistName = artwork.Artist.FirstName,
                // Ensure Exhibitions is initialized to prevent null reference exception
                ExhibitionNames = artwork.Exhibitions.Select(exhibitiion => exhibitiion.ExhibitionName).ToList()

            }).ToList()
        };

        return artistDto;
    }

    /// <summary>
    /// Creates a new artist and links any provided artworks to the artist.
    /// </summary>
    /// <param name="artistDto">The data transfer object containing the details of the artist to create.</param>
    /// <returns>A ServiceResponse object indicating the status of the operation.</returns>
    public async Task<ServiceResponse> CreateArtist(ArtistDto artistDto)
    {
        var response = new ServiceResponse();

        try
        {
            // Create the new artist entity
            var artist = new Artist
            {
                FirstName = artistDto.FirstName,
                LastName = artistDto.LastName,
                ArtistBio = artistDto.ArtistBio,
                Email = artistDto.Email
            };

            // Add the artist to the context
            _context.Artists.Add(artist);

            // Fetch and assign artworks
            if (artistDto.ArtworkIds != null && artistDto.ArtworkIds.Count > 0)
            {
                var artworks = await _context.Artworks
                    .Where(a => artistDto.ArtworkIds.Contains(a.ArtworkId))
                    .ToListAsync();

                if (artworks.Any())
                {
                    foreach (var artwork in artworks)
                    {
                        // Link the artwork to the artist
                        artwork.ArtistId = artist.ArtistId;
                    }

                    artist.Artworks = artworks;
                }
            }

            // Save changes to persist artist and artworks
            await _context.SaveChangesAsync();

            // Response creation
            response.Status = ServiceResponse.ServiceStatus.Created;
            response.CreatedId = artist.ArtistId;
            response.Messages.Add("Artist and related artworks successfully created and linked.");
        }
        catch (Exception ex)
        {
            response.Status = ServiceResponse.ServiceStatus.Error;
            response.Messages.Add($"An error occurred: {ex.Message}");
        }

        return response;
    }

    /// <summary>
    /// Updates the details of an existing artist, including their artworks.
    /// </summary>
    /// <param name="id">The ID of the artist to update.</param>
    /// <param name="artistDto">The data transfer object containing the updated artist details.</param>
    /// <returns>A ServiceResponse object indicating the status of the operation.</returns>
    public async Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto)
    {
        ServiceResponse serviceResponse = new ServiceResponse();

        // Find the artist with the associated artworks using eager loading (Include)
        var existingArtist = await _context.Artists.Include(a => a.Artworks).FirstOrDefaultAsync(a => a.ArtistId == id);

        // Check if the artist exists
        if (existingArtist == null)
        {
            serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
            serviceResponse.Messages.Add("Artist not found.");
            return serviceResponse;
        }

        // Update artist's basic details
        existingArtist.FirstName = artistDto.FirstName ?? existingArtist.FirstName;
        existingArtist.LastName = artistDto.LastName ?? existingArtist.LastName;
        existingArtist.ArtistBio = artistDto.ArtistBio ?? existingArtist.ArtistBio;
        existingArtist.Email = artistDto.Email ?? existingArtist.Email;

        // Handle artwork updates based on ArtworkIds in artistDto
        if (artistDto.ArtworkIds != null && artistDto.ArtworkIds.Any())
        {
            // Find artworks in the database that match the provided ArtworkIds
            var artworksToAdd = await _context.Artworks
                .Where(aw => artistDto.ArtworkIds.Contains(aw.ArtworkId))
                .ToListAsync();

            // Clear existing artworks
            existingArtist.Artworks.Clear();

            // Add the new artworks to the artist
            foreach (var artwork in artworksToAdd)
            {
                existingArtist.Artworks.Add(artwork);
            }
        }

        try
        {
            // Save the changes to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
            serviceResponse.Messages.Add("An error occurred while updating the artist.");
            return serviceResponse;
        }

        serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
        serviceResponse.Messages.Add($"Artist {existingArtist.FirstName} {existingArtist.LastName} updated successfully.");

        return serviceResponse;
    }

    /// <summary>
    /// Deletes an artist from the database.
    /// </summary>
    /// <param name="id">The ID of the artist to delete.</param>
    /// <returns>A ServiceResponse object indicating the status of the operation.</returns>
    public async Task<ServiceResponse> DeleteArtist(int id)
    {
        ServiceResponse response = new();
        // Artist must exist in the first place
        var artist = await _context.Artists.FindAsync(id);
        if (artist == null)
        {
            response.Status = ServiceResponse.ServiceStatus.NotFound;
            response.Messages.Add("Artist cannot be deleted because it does not exist.");
            return response;
        }
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();
        response.Status = ServiceResponse.ServiceStatus.Deleted;

        return response;
    }
}
