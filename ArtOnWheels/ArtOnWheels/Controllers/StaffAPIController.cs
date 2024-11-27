using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAPIController : ControllerBase
    {
        private readonly IStaffService _staffService;

        // Dependency injection of service interfaces
        public StaffAPIController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet(template: "List")]
        public async Task<IEnumerable<StaffDto>> ListStaffs()
        {
            return await _staffService.ListStaffs();
        }

        [HttpGet(template: "Find/{id}")]
        public async Task<StaffDto> GetStaff(int id)
        {
            return await _staffService.GetStaff(id);
        }

        [HttpPut("{id}")]
        public async Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto)
        {
            return await _staffService.UpdateStaff(id, staffDto);
        }

        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateStaff(StaffDto staffDto)
        {
            return await _staffService.CreateStaff(staffDto);
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResponse> DeleteStaff(int id)
        {
            return await _staffService.DeleteStaff(id);
        }
    }
}
