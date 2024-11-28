using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    /// <summary>
    /// Defines the service contract for car-related operations.
    /// Provides methods to:
    /// - Retrieve a list of all cars
    /// - Get details of a specific car by ID
    /// - Add a new car
    /// - Update details of an existing car
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Retrieves a list of all cars.
        /// </summary>
        /// <returns>A collection of <see cref="CarDto"/> objects representing all cars.</returns>
        Task<IEnumerable<CarDto>> ListCars();

        /// <summary>
        /// Retrieves the details of a specific car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>A <see cref="CarDto"/> object containing the car's details.</returns>
        Task<CarDto> GetCar(int id);

        /// <summary>
        /// Adds a new car.
        /// </summary>
        /// <param name="carDto">A <see cref="CarDto"/> object containing the car details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        Task<ServiceResponse> CreateCar(CarDto carDto);

        /// <summary>
        /// Updates the details of an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="carDto">A <see cref="CarDto"/> object containing the updated car details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto);
    }
}
