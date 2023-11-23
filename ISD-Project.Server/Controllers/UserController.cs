using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Authorization;
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
    //TODO: Use authentication
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
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            return _userService.Login(request);
        }

        [HttpGet("get_role")]
        public IActionResult GetRole(int userId)
        {
            return _userService.GetUserRole(userId);
        }

        [HttpPost("verify")]
        public IActionResult Verify([FromBody] string token)
        {
            return _userService.Verify(token);
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] UserForgotPasswordRequest request)
        {
            return _userService.ForgotPassword(request);
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] UserResetPasswordRequest request)
        {
            return _userService.ResetPassword(request);
        }
    }
}
