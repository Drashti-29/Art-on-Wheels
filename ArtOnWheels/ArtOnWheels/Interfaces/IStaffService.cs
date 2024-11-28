using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    /// <summary>
    /// Defines the service contract for staff-related operations.
    /// Provides methods to:
    /// - Retrieve a list of all staff members
    /// - Get details of a specific staff member by ID
    /// - Add a new staff member
    /// - Update details of an existing staff member
    /// - Delete a staff member by ID
    /// </summary>
    public interface IStaffService
    {
        /// <summary>
        /// Retrieves a list of all staff members.
        /// </summary>
        /// <returns>A collection of <see cref="StaffDto"/> objects representing all staff members.</returns>
        Task<IEnumerable<StaffDto>> ListStaffs();

        /// <summary>
        /// Retrieves the details of a specific staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to retrieve.</param>
        /// <returns>A <see cref="StaffDto"/> object containing the staff member's details.</returns>
        Task<StaffDto> GetStaff(int id);

        /// <summary>
        /// Updates the details of an existing staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to update.</param>
        /// <param name="staffDto">A <see cref="StaffDto"/> object containing the updated staff member details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto);

        /// <summary>
        /// Adds a new staff member.
        /// </summary>
        /// <param name="staffDto">A <see cref="StaffDto"/> object containing the staff member details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        Task<ServiceResponse> CreateStaff(StaffDto staffDto);

        /// <summary>
        /// Deletes a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion operation.</returns>
        Task<ServiceResponse> DeleteStaff(int id);
    }
}
