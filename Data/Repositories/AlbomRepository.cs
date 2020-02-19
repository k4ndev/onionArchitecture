using Core.Models;
using Core.Repositories;

namespace Data.Repositories
{
    public class AlbomRepository : Repository<Albom>, IAlbomRepository
    {
        private MusicAppContext _context => Context as MusicAppContext;
        public AlbomRepository(MusicAppContext context) : base(context)
        {

        }
    }
}
