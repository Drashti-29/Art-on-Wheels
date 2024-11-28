using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using ArtOnWheels.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    /// <summary>
    /// CarAPIController handles car-related API operations.
    /// This controller provides endpoints to:
    /// - List all cars
    /// - Get details of a specific car by ID
    /// - Add a new car
    /// - Update details of an existing car
    /// 
    /// Implements dependency injection for service interfaces to perform operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CarAPIController : ControllerBase
    {
        private readonly ICarService _carService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAPIController"/> class.
        /// </summary>
        /// <param name="carService">The car service interface for performing operations.</param>
        public CarAPIController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Retrieves a list of all cars.
        /// </summary>
        /// <returns>A collection of <see cref="CarDto"/> objects.</returns>
        [HttpGet(template: "List")]
        public async Task<IEnumerable<CarDto>> ListCars()
        {
            return await _carService.ListCars();
        }

        /// <summary>
        /// Retrieves details of a specific car by its ID.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>A <see cref="CarDto"/> object containing the car's details.</returns>
        [HttpGet("{id}")]
        public async Task<CarDto> GetCar(int id)
        {
            return await _carService.GetCar(id);
        }

        /// <summary>
        /// Adds a new car.
        /// </summary>
        /// <param name="carDto">The car details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation operation.</returns>
        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateCar(CarDto carDto)
        {
            return await _carService.CreateCar(carDto);
        }

        /// <summary>
        /// Updates the details of an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="carDto">The updated car details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update operation.</returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto)
        {
            return await _carService.UpdateCarDetails(id, carDto);
        }
    }
}
