using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;

namespace ArtOnWheels.Services
{
    public class StaffService :  IStaffService
    {
        public async Task<IEnumerable<StaffDto>> ListStaffs()
        {

        }
        public async Task<StaffDto> GetStaff(int id)
        {

        }
        public async Task<ServiceResponse> CreateStaff(StaffDto staffDto)
        {

        }
        public async Task<ServiceResponse> UpdateStaff(int id, StaffDto staffDto)
        {

        }
        public async Task<ServiceResponse> DeleteStaff(int id)
        {

        }
    }
}
