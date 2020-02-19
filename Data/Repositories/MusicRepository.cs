using Core.Models;
using Core.Repositories;

namespace Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private MusicAppContext _context => Context as MusicAppContext;
        public MusicRepository(MusicAppContext context) : base(context)
        {

        }
    }
}
