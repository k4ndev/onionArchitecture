using Core.Models;
using System.Threading.Tasks;

namespace Core.Services.Data
{
    public interface IUserService
    {
        string Login(User user);
        Task<User> Create(User user);
        Task<bool> IsExist(string email, string password);
        Task<User> IsExistUser(string email, string password);
    }
}
