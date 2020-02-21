using Core;
using Core.Models;
using Core.Services.Data;
using System;
using System.Threading.Tasks;
using CryptoHelper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Data
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Create(User user)
        {
            user.Password= Crypto.HashPassword(user.Password);
            user.AddedAt = DateTime.UtcNow.AddHours(4);
            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task<bool> IsExist(string email, string password)
        {
           User user= await _unitOfWork.User.SingleOrDefaultAsync(x => x.Email==email);

            if (user != null)
            {
                if (Crypto.VerifyHashedPassword(user.Password,password))
                {
                    return true;
                }
            }

            return false;
           
        }

        public async Task<User> IsExistUser(string email, string password)
        {
            User user = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                if (Crypto.VerifyHashedPassword(user.Password, password))
                {
                    return user;
                }
            }

            return null;
        }

        public string Login(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4).AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(tokendesc);



            return tokenHandle.WriteToken(token);
        }
    }
}
