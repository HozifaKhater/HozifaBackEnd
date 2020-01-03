using Hozifa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hozifa.Interfaces.PostInterfaces
{
    public interface IPostService
    {

        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> DeletePostAsync(Post post);
    }
}
