using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{
    /// <summary>
    /// Service class for managing staff-related operations in the Art On Wheels application.
    /// </summary>
    public class StaffService :  IStaffService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffService"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used for data access.</param>
        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all staff members with their associated exhibitions.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of staff details in <see cref="StaffDto"/> format.</returns>
        public async Task<IEnumerable<StaffDto>> ListStaffs()
        {
            List<Staff> staffList = await _context.Staff.Include(s => s.Exhibition).ToListAsync();

            // Map to StaffDto
            List<StaffDto> staffDtos = staffList.Select(staff => new StaffDto
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Position = staff.Position,
                Contact = staff.Contact,
                ExhibitionId = staff.ExhibitionId,
                ExhibitionName = staff.Exhibition?.ExhibitionName // Assuming Exhibition has a Name property
            }).ToList();

            return staffDtos;

        }

        /// <summary>
        /// Retrieves details of a specific staff member by ID, including associated exhibition information.
        /// </summary>
        /// <param name="id">The unique identifier of the staff member.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the details of the staff member in <see cref="StaffDto"/> format, or null if not found.</returns>
        public async Task<StaffDto> GetStaff(int id)
        {
            var staff = await _context.Staff
                .Include(e => e.Exhibition)
                .FirstOrDefaultAsync(e => e.StaffId == id);

            if (staff == null) return null;

            return new StaffDto
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Position = staff.Position,
                Contact = staff.Contact,
                ExhibitionId = staff.ExhibitionId,
                ExhibitionName = staff.Exhibition.ExhibitionName
            };
        }

        /// <summary>
        /// Creates a new staff member record.
        /// </summary>
        /// <param name="staffDto">The details of the staff member to be created.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> with the status of the operation.</returns>
        public async Task<ServiceResponse> CreateStaff(StaffDto staffDto)
        {
            ServiceResponse serviceResponse = new();

            Staff staff = new Staff
            {
                FirstName = staffDto.FirstName,
                LastName = staffDto.LastName,
                Position = staffDto.Position,
                Contact = staffDto.Contact,
                ExhibitionId = staffDto.ExhibitionId
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = staff.StaffId;
            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing staff member.
        /// </summary>
        /// <param name="id">The unique identifier of the staff member to update.</param>
        /// <param name="staffDto">The updated details of the staff member.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> with the status of the operation.</returns>
        public async Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto)
        {
            ServiceResponse serviceResponse = new();

            var existingStaff = await _context.Staff.FindAsync(id);

            if (existingStaff == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Staff not found.");
                return serviceResponse;
            }

            // Update properties
            existingStaff.FirstName = staffDto.FirstName ?? existingStaff.FirstName;
            existingStaff.LastName = staffDto.LastName ?? existingStaff.LastName;
            existingStaff.Position = staffDto.Position ?? existingStaff.Position;
            existingStaff.Contact = staffDto.Contact ?? existingStaff.Contact;
            existingStaff.ExhibitionId = staffDto.ExhibitionId != 0 ? staffDto.ExhibitionId : existingStaff.ExhibitionId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("An error occurred while updating the staff.");
                return serviceResponse;
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Staff {existingStaff.FirstName} {existingStaff.LastName} updated successfully.");

            return serviceResponse;

        }

        /// <summary>
        /// Deletes a specific staff member record by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the staff member to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a <see cref="ServiceResponse"/> with the status of the operation.</returns>
        public async Task<ServiceResponse> DeleteStaff(int id)
        {
            ServiceResponse response = new();

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Staff cannot be deleted because it does not exist.");
                return response;
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Deleted;
            response.Messages.Add($"Staff {staff.FirstName} {staff.LastName} deleted successfully.");
            return response;
    }
    }
}
