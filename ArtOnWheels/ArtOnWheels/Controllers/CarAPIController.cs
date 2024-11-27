using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using ArtOnWheels.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtOnWheels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAPIController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarAPIController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet(template: "List")]
        public async Task<IEnumerable<CarDto>> ListCars()
        {
            return await _carService.ListCars();
        }
        [HttpGet("{id}")]
        public async Task<CarDto> GetCar(int id)

        {
            return await _carService.GetCar(id);
        }
        [HttpPost(template: "Add")]
        public async Task<ServiceResponse> CreateCar(CarDto carDto)
        {
            return await _carService.CreateCar(carDto);
        }
        [HttpPut(template: "Update/{id}")]
        public async Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto)
        {
            return await _carService.UpdateCarDetails(id, carDto);
        }

    }
}
