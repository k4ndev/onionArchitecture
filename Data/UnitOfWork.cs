using Core;
using Core.Repositories;
using Data.Repositories;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly MusicAppContext _context;
        private IMusicRepository _musicRepository;
        private IAlbomRepository _albomRepository;
        private IArtistRepository _artistRepository;
        private IUserRepository _userRepository;

        public IArtistRepository Artist => _artistRepository ?? new ArtistRepository(_context);

        public IAlbomRepository Albom => _albomRepository ?? new AlbomRepository(_context);

        public IMusicRepository Music => _musicRepository ?? new MusicRepository(_context);

        public IUserRepository User => _userRepository ?? new UserRepository(_context);

        public UnitOfWork(MusicAppContext context)
        {
            this._context = context;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
