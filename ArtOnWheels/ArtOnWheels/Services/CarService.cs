using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtOnWheels.Services
{
    /// <summary>
    /// Provides services for managing cars in the ArtOnWheels application.
    /// Includes CRUD operations and managing relationships with associated artworks.
    /// </summary>
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CarService"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing car and related data.</param>
        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all cars with their associated artwork details.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="CarDto"/> objects.</returns>
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
        /// <summary>
        /// Retrieves a car by its ID, including associated artworks and exhibitions.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>A <see cref="CarDto"/> object if the car is found, otherwise null.</returns>
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

        /// <summary>
        /// Creates a new car in the database.
        /// </summary>
        /// <param name="carDto">The details of the car to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the operation's status.</returns>
        public async Task<ServiceResponse> CreateCar(CarDto carDto)
        {
            ServiceResponse response = new();

            Car car = new Car
            {
                Type = carDto.Type,
                ImageUrl = carDto.ImageUrl
            };

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Created;
            response.CreatedId = car.CarId;
            return response;
        }

        /// <summary>
        /// Updates the details of an existing car.
        /// </summary>
        /// <param name="id">The ID of the car to update.</param>
        /// <param name="carDto">The updated details of the car.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the operation's status.</returns>
        public async Task<ServiceResponse> UpdateCarDetails(int id, CarDto carDto)
        {
            ServiceResponse response = new();

            Car existingCar = await _context.Car
                .Include(c => c.Artworks) // Include related artworks
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (existingCar == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Car not found.");
                return response;
            }

            // Update car details
            existingCar.Type = carDto.Type ?? existingCar.Type;
            existingCar.ImageUrl = carDto.ImageUrl ?? existingCar.ImageUrl;

            // Update artworks if ArtworkIds are provided
            if (carDto.ArtworkIds != null && carDto.ArtworkIds.Any())
            {
                // Find artworks by IDs
                var artworksToAdd = await _context.Artworks
                    .Where(artwork => carDto.ArtworkIds.Contains(artwork.ArtworkId))
                    .ToListAsync();

                // Clear existing artworks and add new ones
                existingCar.Artworks.Clear();
                foreach (var artwork in artworksToAdd)
                {
                    existingCar.Artworks.Add(artwork);
                }
            }

            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Updated;
            response.Messages.Add("Car updated successfully.");
            return response;

        }

        /// <summary>
        /// Deletes a car by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the car to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the operation's status.</returns>
        public async Task<ServiceResponse> DeleteCar(int id)
        {
            ServiceResponse response = new();

            Car car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Car not found.");
                return response;
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            response.Status = ServiceResponse.ServiceStatus.Deleted;
            return response;
        }
    }
}
