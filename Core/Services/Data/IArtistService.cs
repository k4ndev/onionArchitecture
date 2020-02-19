using Core.Models;
using System.Threading.Tasks;

namespace Core.Services.Data
{
    public interface IArtistService
    {
        Task<Artist> GetUserById(int id);
        Task<Artist> CreateArtist(Artist artist);
    }
}
