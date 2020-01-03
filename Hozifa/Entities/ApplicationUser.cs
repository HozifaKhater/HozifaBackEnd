using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
