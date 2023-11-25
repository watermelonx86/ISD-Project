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
    //[Authorize]
    //TODO: Avoid providing too much information in error messages Register, Login, Verify
    //DONE: Fix violates principles single responsibility in CreatePasswordHash, CreateRandomToken, VerifyPasswordHash
    //TODO: Use authentication
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userService)
        {
            this._userAccountService = userService;
        }

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            return _userAccountService.Register(request);
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            return _userAccountService.Login(request);
        }

        [HttpGet("get_role")]
        public Task<IActionResult> GetRole(int userId)
        {
            return _userAccountService.GetUserRole(userId);
        }

        [HttpPost("verify")]
        public Task<IActionResult> Verify([FromBody] string token)
        {
            return _userAccountService.Verify(token);
        }

        [HttpPost("forgot-password")]
        public Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordRequest request)
        {
            return _userAccountService.ForgotPassword(request);
        }

        [HttpPost("reset-password")]
        public Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequest request)
        {
            return _userAccountService.ResetPassword(request);
        }
    }
}
