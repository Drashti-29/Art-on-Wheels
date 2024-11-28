using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{
    /// <summary>
    /// Service class for managing exhibitions.
    /// Provides methods for creating, retrieving, updating, and deleting exhibitions.
    /// </summary>
    public class ExhibitionService : IExhibitionService
    {
        private readonly ApplicationDbContext _context;

        // Dependency injection of database context
        public ExhibitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all exhibitions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of <see cref="ExhibitionDto"/> objects.</returns>
        public async Task<IEnumerable<ExhibitionDto>> ListExhibitions()
        {
            var exhibitions = await _context.Exhibitions
                .Include(e => e.Artworks) // Include related artworks
                .Include(e => e.Staffs) // Include related staffs
                .ToListAsync();
            
            // Map exhibitions to DTOs
            List<ExhibitionDto> exhibitionDtos = exhibitions.Select(exhibition => new ExhibitionDto
            {
                ExhibitionId = exhibition.ExhibitionId,
                ExhibitionName = exhibition.ExhibitionName,
                Location = exhibition.Location,
                Date = exhibition.Date
            }).ToList();

            return exhibitionDtos;
        }

        /// <summary>
        /// Retrieves details of a single exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="ExhibitionDto"/> object or null if not found.</returns>
        public async Task<ExhibitionDto> GetExhibition(int id)
        {
            var exhibition = await _context.Exhibitions
                .Include(e => e.Artworks) // Include related artworks
                .Include(e => e.Staffs) // Include related staffs
                .FirstOrDefaultAsync(e => e.ExhibitionId == id);

            if (exhibition == null)
            {
                return null;
            }

            // Map exhibition to DTO
            return new ExhibitionDto
            {
                ExhibitionId = exhibition.ExhibitionId,
                ExhibitionName = exhibition.ExhibitionName,
                Location = exhibition.Location,
                Date = exhibition.Date
            };
        }

        /// <summary>
        /// Creates a new exhibition.
        /// </summary>
        /// <param name="exhibitionDto">The DTO containing the details of the exhibition to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> indicating the status of the operation.</returns>
        public async Task<ServiceResponse> CreateExhibition(ExhibitionDto exhibitionDto)
        {
            ServiceResponse response = new();

            Exhibition exhibition = new Exhibition
            {
                ExhibitionName = exhibitionDto.ExhibitionName,
                Location = exhibitionDto.Location,
                Date = exhibitionDto.Date,
                Artworks = new List<Artwork>(), // Initialize empty collections for relationships
                Staffs = new List<Staff>()
            };

            _context.Exhibitions.Add(exhibition);
            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Created;
            response.CreatedId = exhibition.ExhibitionId;
            return response;
        }

        /// <summary>
        /// Updates the details of an existing exhibition.
        /// </summary>
        /// <param name="id">The ID of the exhibition to update.</param>
        /// <param name="exhibitionDto">The DTO containing the updated details of the exhibition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> indicating the status of the operation.</returns>
        public async Task<ServiceResponse> UpdateExhibitionDetails(int id, ExhibitionDto exhibitionDto)
        {
            ServiceResponse response = new();

            var existingExhibition = await _context.Exhibitions
                .Include(e => e.Artworks) // Include related artworks
                .Include(e => e.Staffs) // Include related staffs
                .FirstOrDefaultAsync(e => e.ExhibitionId == id);

            if (existingExhibition == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Exhibition not found.");
                return response;
            }

            // Update exhibition details
            existingExhibition.ExhibitionName = exhibitionDto.ExhibitionName ?? existingExhibition.ExhibitionName;
            existingExhibition.Location = exhibitionDto.Location ?? existingExhibition.Location;
            existingExhibition.Date = exhibitionDto.Date != default ? exhibitionDto.Date : existingExhibition.Date;

            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Updated;
            response.Messages.Add("Exhibition updated successfully.");
            return response;
        }

        /// <summary>
        /// Deletes an exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> indicating the status of the operation.</returns>
        public async Task<ServiceResponse> DeleteExhibition(int id)
        {
            ServiceResponse response = new();

            Exhibition exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Exhibition not found.");
                return response;
            }

            _context.Exhibitions.Remove(exhibition);
            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Deleted;
            return response;
        }
    }
}
