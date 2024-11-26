using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface IArtworkService
    {
        Task<IEnumerable<ArtworkDto>> ListArtworks();
        Task<ArtworkDto> GetArtwork(int id);
        Task<ServiceResponse> UpdateArtworkDetails(int id, ArtworkDto artworkDto);
        Task<ServiceResponse> CreateArtwork(ArtworkDto artworkDto);
        Task<ServiceResponse> DeleteArtwork(int id);
    }
}
