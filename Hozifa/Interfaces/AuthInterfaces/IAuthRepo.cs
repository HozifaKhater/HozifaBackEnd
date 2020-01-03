﻿using Hozifa.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Interfaces.AuthInterfaces
{
    public interface IAuthRepo
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> Login(string email, string password);
    }
}
