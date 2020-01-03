using Hozifa.Entities;
using Hozifa.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hozifa.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return _repo.CreateUserAsync(user, password);
        }

        public string GenerateToken(string userId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.Name, userId),
                        }),
                Expires = DateTime.UtcNow.AddMonths(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:Jwt_Secret"])), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var bearerToken = tokenHandler.WriteToken(securityToken);

            return bearerToken;
        }

        public Task<ApplicationUser> Login(string email, string password)
        {
            return _repo.Login(email, password);
        }
    }
}
