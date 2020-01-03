using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.ViewModels
{
    public class LoginResponse
    {
        public string FullName { get; set; }
        public string Token { get; set; }

        public static LoginResponse SetData(string fullName, string token)
        {
            return new LoginResponse()
            {
                FullName = fullName,
                Token = token
            };
        }
    }
}
