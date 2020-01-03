using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Hozifa.Entities;
using Hozifa.Interfaces.AuthInterfaces;
using Hozifa.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hozifa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _service;
        public UsersController(IMapper mapper, IAuthService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseResult.Faild("ValidationError"));
            }


            var applicationUser = _mapper.Map<ApplicationUser>(model);

            var result = await _service.CreateUserAsync(applicationUser, model.Password);

            if (!result.Succeeded)
            {
                return Ok(ResponseResult.Faild(result.Errors.FirstOrDefault().Description));
            }

            return Ok(ResponseResult.SuccessWithMessage("Registered Successfully"));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResponseResult.Faild("ValidationError"));
            }

            // Authenticating Identity User
            var user = await _service.Login(model.Username, model.Password);
            if (user == null)
                return Ok(ResponseResult.Faild("Username or Password is Incorrect"));

            // Generating Token and Response model

            var token = _service.GenerateToken(user.Id);

            var result = LoginResponse.SetData(user.FirstName + " " + user.LastName, token);

            return Ok(ResponseResult.SuccessWithData(result));
        }
    }
}