using com.tweetapp.Services.Repositories.lib;
using com.tweetapp.Services.lib.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.web.Controllers
{
    [Authorize]
    public class AuthController : BaseController
    {
        private readonly IAuthRepository repository;

        public AuthController(IAuthRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("tweets/users/all")]
        public async Task<IActionResult> Get()
        {
            var response = await repository.GetAllUsers();
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("tweets/user/search/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var response = await repository.GetUserByName(username);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("tweets/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            var response = await repository.RegisterUser(registerUser, registerUser.Password);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("tweets/login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var response =await repository.Login(userName, password);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("tweets/forgetPassword")]
        public async Task<IActionResult> ForgetPassword(string userName, string email, string password)
        {
            var response = await repository.ForgetPassword(userName, email, password);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}
