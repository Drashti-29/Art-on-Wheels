using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarDto>> ListCars()
        {
            IEnumerable<CarDto> cars = await _context.Car
                .Select(car => new CarDto
                {
                    CarId = car.CarId,
                    Type = car.Type,
                    ImageUrl = car.ImageUrl,
                    ArtworkIds = car.Artworks.Select(Artwork => Artwork.ArtworkId).ToList(),
                    ArtworkTiles = car.Artworks.Select(Artwork => Artwork.Title).ToList()
                }).ToListAsync();
            return cars;
        }
        public async Task<CarDto> GetCar(int id)
        {
            // Fetch the car
            var car = await _context.Car
                .Include(a => a.Artworks)
                .Include(a => a.Exhibitions)
                .FirstOrDefaultAsync(c => c.CarId == id);
            if (car == null)
            {
                return null; // or you can throw an exception if preferred
            }

            // Map the car entity to the CarDto
            CarDto carDto = new CarDto()
            {
                CarId = car.CarId,
                Type = car.Type,
                ImageUrl = car.ImageUrl,
                ArtworkIds = car.Artworks.Select(Artwork => Artwork.ArtworkId).ToList(),
                ArtworkTiles = car.Artworks.Select(Artwork => Artwork.Title).ToList()
            };
            return carDto;
        }

        public async Task<ServiceResponse> CreateCar(CarDto carDto)
        {
            ServiceResponse serviceResponse = new();


            var artist = await _context.Artists.FindAsync(carDto.CarId);
            if (artist == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Artist not found. Please provide a valid ArtistId.");
                return serviceResponse;
            }
            Car car = new Car()
            {
                Type = carDto.Type,
                ImageUrl= carDto.ImageUrl
            };

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = car.CarId;
            return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();

            var existingCar = await _context.Car.Include(a => a.CarId).FirstOrDefaultAsync(a => a.CarId == id);

            if (existingCar == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Car not found.");
                return serviceResponse;
            }
            if (carDto.CarId != 0 && carDto.CarId != existingCar.CarId)
            {
                var car = await _context.Car.FindAsync(carDto.CarId);
                if (car == null)
                {
                    serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                    serviceResponse.Messages.Add("Car not found.");
                    return serviceResponse;
                }
                existingCar.CarId = carDto.CarId;
                existingCar.CarId = existingCar.CarId;
            }
            existingCar.CarId = carDto.CarId;
            existingCar.Type = carDto.Type;
            existingCar.ImageUrl = carDto.ImageUrl;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Car updated successfully. Car: {existingCar.CarId}");

            return serviceResponse;
        }
        public async Task<ServiceResponse> DeleteCar(int id)
        {
            ServiceResponse response = new();

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Car cannot be deleted because it does not exist.");
                return response;
            }
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;
        }
    }
}
