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
                    ArtworkIds = car.Artworks.Select(artwork => artwork.ArtworkId).ToList(),
                    ArtworkTiles = car.Artworks.Select(artwork => artwork.Title).ToList(),
                    })
                .ToListAsync();
            return cars;
        }
    }
}
