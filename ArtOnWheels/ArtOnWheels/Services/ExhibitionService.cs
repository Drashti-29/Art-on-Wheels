using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly ApplicationDbContext _context;

        // Dependency injection of database context
        public ExhibitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all exhibitions
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

        // Get details of a single exhibition by ID
        public async Task<ExhibitionDto> GetExhibition(int id)
        {
            Exhibition exhibition = await _context.Exhibitions
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

        // Create a new exhibition
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

        // Update exhibition details
        public async Task<ServiceResponse> UpdateExhibitionDetails(int id, ExhibitionDto exhibitionDto)
        {
            ServiceResponse response = new();

            Exhibition existingExhibition = await _context.Exhibitions
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

        // Delete an exhibition by ID
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
