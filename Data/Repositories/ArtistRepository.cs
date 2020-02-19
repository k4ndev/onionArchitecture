using Core.Models;
using Core.Repositories;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private MusicAppContext _context => Context as MusicAppContext;

        public ArtistRepository(MusicAppContext context) : base(context)
        {

        }

        public async Task<Artist> FindArtist(int id)
        {
            return await _context.Artists.FindAsync(id);
        }
    }
}
