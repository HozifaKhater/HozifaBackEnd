using Hozifa.Context;
using Hozifa.Entities;
using Hozifa.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AuthRepo(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser> Login(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (result)
            {
                return user;
            }

            return null;
        }
    }
}
