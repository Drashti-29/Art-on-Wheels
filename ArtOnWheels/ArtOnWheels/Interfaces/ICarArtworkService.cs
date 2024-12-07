using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface ICarArtworkService
    {
        Task<CarArtworkViewModel> GetCarArtworkViewModel();
    }
}
