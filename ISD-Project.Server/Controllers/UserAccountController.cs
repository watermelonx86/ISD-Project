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
        public Task<IActionResult> Register([FromBody] UserAccountRegisterRequest request)
        {
            return _userAccountService.Register(request);
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] UserAccountLoginRequest request)
        {
            return _userAccountService.Login(request);
        }

        [HttpGet("get-user-account")]
        public Task<IActionResult> GetUserAccount()
        {
            return _userAccountService.GetUserAccount();
        }

        [HttpGet("get-user-account/{id}")]
        public Task<IActionResult> GetUserAccount(int id)
        {
            return _userAccountService.GetUserAccount(id);
        }

        [HttpGet("get-role/{id}")]
        public Task<List<string>> GetRole(int id)
        {
            return _userAccountService.GetUserRole(id);
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
