using Core.Repositories;
using System.Threading.Tasks;

namespace Core
{
    public interface IUnitOfWork
    {
        IArtistRepository Artist { get; }
        IAlbomRepository Albom { get; }
        IMusicRepository Music { get; }
        IUserRepository User { get; }
        Task<int> CommitAsync();
    }
}
