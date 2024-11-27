using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> ListStaffs();
        Task<StaffDto> GetStaff(int id);
        Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto);
        Task<ServiceResponse> CreateStaff(StaffDto staffDto);
        Task<ServiceResponse> DeleteStaff(int id);
    }
}
