using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface ICarService
    {
        // Fetch all cars
        Task<IEnumerable<CarDto>> ListCars();
        Task<CarDto> GetCar(int id);
        Task<ServiceResponse> CreateCar(CarDto carDto);
        Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto);

    }
}
