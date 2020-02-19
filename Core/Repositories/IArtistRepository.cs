using Core.Models;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist> FindArtist(int id);
    }
}
