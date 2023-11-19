using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //TODO: Avoid providing too much information in error messages Register, Login, Verify
    //DONE: Fix violates principles single responsibility in CreatePasswordHash, CreateRandomToken, VerifyPasswordHash


    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest request)
        {
            return _userService.Register(request);
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] UserLoginRequest request)
        {
            return _userService.Login(request);
        }

        [HttpPost("verify")]
        public IActionResult Verify([FromForm] string token)
        {
            return _userService.Verify(token);
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromForm] UserForgotPasswordRequest request)
        {
            return _userService.ForgotPassword(request);
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromForm] UserResetPasswordRequest request)
        {
            return _userService.ResetPassword(request);
        }
    }
}
