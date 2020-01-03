using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Hozifa.Entities;
using Hozifa.Interfaces.PostInterfaces;
using Hozifa.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hozifa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(IPostService service,IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }


        // GET: api/Posts
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var posts = await _service.GetPostsAsync();
            var postList = _mapper.Map<List<PostViewModel>>(posts);
            return Ok(ResponseResult.SuccessWithData(postList));
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "Get")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _service.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();

            }

            var postItem = _mapper.Map<PostViewModel>(post);

            // Note I always make person table that have basic data to retrieve to end user instead of return all users data but i don't have time to do right now

            return Ok(ResponseResult.SuccessWithData(postItem));
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostViewModel model)
        {
            if (!ModelState.IsValid)
                return Ok(ResponseResult.Faild("Validation Error")) ;

            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await _userManager.FindByIdAsync(userId);
            model.PostAuthor = user;

            var mappedPost = _mapper.Map<Post>(model);
            var result = await _service.CreatePostAsync(mappedPost);

            if (result)
                return Ok(ResponseResult.SuccessWithMessage("Created Successfully"));

            return Ok(ResponseResult.Faild("Faild To Create Post"));
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> Put(int id, [FromBody] PostViewModel model)
        {
            if (!ModelState.IsValid)
                return Ok(ResponseResult.Faild("Validation Error"));


            var post = await _service.GetPostByIdAsync(id);

            if (post == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (post.User.Id != userId)
                return Ok(ResponseResult.Faild("Not Authorized")) ;

            model.PostAuthor = post.User;
            model.Id = post.Id;
            var mappedPost = _mapper.Map(model,post);
            var result = await _service.UpdatePostAsync(mappedPost);

            if (result)
                return Ok(ResponseResult.SuccessWithMessage("Updated Successfully"));

            return Ok(ResponseResult.Faild("Faild To Update Post"));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _service.GetPostByIdAsync(id);

            if (post == null)
                return NotFound();

            var userId = User.FindFirst(ClaimTypes.Name)?.Value;

            if (post.User.Id != userId)
                return Ok(ResponseResult.Faild("Not Authorized"));

            var result = await _service.DeletePostAsync(post);

            if (result)
            {
                return Ok(ResponseResult.SuccessWithMessage("Post Deleted Successfully"));
            }

            return Ok(ResponseResult.Faild("Faild to Delete Post"));

        }
    }
}
