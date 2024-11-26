using ArtOnWheels.Data;
using ArtOnWheels.Interfaces;
using ArtOnWheels.Models;

namespace ArtOnWheels.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        // dependency injection of database context
        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ArtistDto>> ListArtists()
        {

        }
        public async Task<ArtistDto> GetArtist(int id)
        {

        }
        public async Task<ServiceResponse> CreateArtist(ArtistDto artistDto)
        {

        }
        public async Task<ServiceResponse> UpdateArtistDetails(int id, ArtistDto artistDto)
        {

        }
        public async Task<ServiceResponse> DeleteArtist(int id)
        {

        }

    }
}
