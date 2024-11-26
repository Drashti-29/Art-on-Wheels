using ArtOnWheels.Models;

namespace ArtOnWheels.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDto>> ListArtists();
        Task<ArtistDto> GetArtist(int id);
        Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto);
        Task<ServiceResponse> CreateArtist(ArtistDto artistDto);
        Task<ServiceResponse> DeleteArtist(int id);

    }
}
