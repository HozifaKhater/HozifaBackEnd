using Hozifa.Entities;
using Hozifa.Interfaces.PostInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _repo;
        public PostService(IPostRepo repo)
        {
            _repo = repo;
        }
        public async Task<bool> CreatePostAsync(Post post)
        {
            return await _repo.CreatePostAsync(post);
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            return await _repo.DeletePostAsync(post);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _repo.GetPostByIdAsync(id);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _repo.GetPostsAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            return await _repo.UpdatePostAsync(post);
        }
    }
}
