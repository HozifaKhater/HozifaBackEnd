using Hozifa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Context
{
    public class EntitiesConfig
    {
        public static void LoadPostConfig(ModelBuilder builder)
        {
            builder.Entity<Post>().HasKey(e => e.Id);
            builder.Entity<Post>().Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<Post>().Property(e => e.Title).HasMaxLength(150);
            builder.Entity<Post>().Property(e => e.Title).IsRequired();
            builder.Entity<Post>().Property(e => e.Description).IsRequired();
        }

        public static void LoadApplicationUserConfig(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasMany(e => e.Posts).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
