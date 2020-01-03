using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Entities
{
    public class Post
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public virtual ApplicationUser User { get; set; }
    }
}
