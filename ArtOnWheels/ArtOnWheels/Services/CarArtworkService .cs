using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;

namespace ArtOnWheels.Services
{
    public class CarArtworkService : ICarArtworkService
    {
            private readonly ApplicationDbContext _context;
            private readonly IArtworkService _artworkService;
            private readonly ICarService _carService;

        // Constructor to inject the database context
        public CarArtworkService(ApplicationDbContext context, IArtworkService artworkService, ICarService carService)
        {
            _context = context;
            _artworkService = artworkService;
            _carService = carService;
        }

        public async Task<CarArtworkViewModel> GetCarArtworkViewModel()
            {
            IEnumerable<CarDto> cars = await _carService.ListCars();
            IEnumerable<ArtworkDto> artworks = await _artworkService.ListArtworks();

            // Map CarDto to CarViewModel
            var carViewModels = cars.Select(car => new CarDto
            {
                CarId = car.CarId,
                Type = car.Type,
                ImageUrl = car.ImageUrl,
                ArtworkIds = car.ArtworkIds,
                ArtworkTiles = car.ArtworkTiles
            });

            // Map ArtworkDto to ArtworkViewModel
            var artworkViewModels = artworks.Select(artwork => new ArtworkDto
            {
                ArtworkId = artwork.ArtworkId,
                Title = artwork.Title,
                Description = artwork.Description,
                Price = artwork.Price,
                CreationYear = artwork.CreationYear,
                ImageUrl = artwork.ImageUrl,
                ArtistId = artwork.ArtistId,
                ArtistName = artwork.ArtistName,
                ExhibitionNames = artwork.ExhibitionNames
            });

            // Combine into CarArtworkViewModel
            return new CarArtworkViewModel
            {
                CarList = carViewModels,
                ArtworkList = artworkViewModels
            };
            }
        }
    }

