using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    /// <summary>
    /// StaffAPIController handles staff-related API operations.
    /// This controller provides endpoints to:
    /// - List all staff members
    /// - Retrieve details of a specific staff member by ID
    /// - Add a new staff member
    /// - Update details of an existing staff member
    /// - Delete a staff member by ID
    /// 
    /// Implements dependency injection for service interfaces to perform operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAPIController : ControllerBase
    {
        private readonly IStaffService _staffService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffAPIController"/> class.
        /// </summary>
        /// <param name="staffService">The staff service interface for performing operations.</param>
        public StaffAPIController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        /// <summary>
        /// Retrieves a list of all staff members.
        /// </summary>
        /// <returns>A collection of <see cref="StaffDto"/> objects.</returns>
        [HttpGet(template: "List")]
        public async Task<IEnumerable<StaffDto>> ListStaffs()
        {
            return await _staffService.ListStaffs();
        }

        /// <summary>
        /// Retrieves details of a specific staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to retrieve.</param>
        /// <returns>A <see cref="StaffDto"/> object containing the staff member's details.</returns>
        [HttpGet(template: "Find/{id}")]
        public async Task<StaffDto> GetStaff(int id)
        {
            return await _staffService.GetStaff(id);
        }

        /// <summary>
        /// Updates the details of an existing staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to update.</param>
        /// <param name="staffDto">The updated staff details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        [HttpPut("{id}")]
        public async Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto)
        {
            return await _staffService.UpdateStaff(id, staffDto);
        }

        /// <summary>
        /// Adds a new staff member.
        /// </summary>
        /// <param name="staffDto">The staff details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateStaff(StaffDto staffDto)
        {
            return await _staffService.CreateStaff(staffDto);
        }

        /// <summary>
        /// Deletes a staff member by their ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ServiceResponse> DeleteStaff(int id)
        {
            return await _staffService.DeleteStaff(id);
        }
    }
}
