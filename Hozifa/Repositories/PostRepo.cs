using Hozifa.Context;
using Hozifa.Entities;
using Hozifa.Interfaces.PostInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _context;
        public PostRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            try
            {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Where(e=>e.Id == id).Include(a=>a.User).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.Include(e=>e.User).ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            try
            {
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
