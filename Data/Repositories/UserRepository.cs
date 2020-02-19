using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private MusicAppContext _context => Context as MusicAppContext;

        public UserRepository(MusicAppContext context) : base(context){}
        
    }
}
