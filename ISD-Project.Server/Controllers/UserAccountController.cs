using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //TODO: Avoid providing too much information in error messages Register, Login, Verify
    //DONE: Fix violates principles single responsibility in CreatePasswordHash, CreateRandomToken, VerifyPasswordHash
    //DONE: Use authentication

    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userService)
        {
            this._userAccountService = userService;
        }

        [HttpPost("register"), Authorize(Roles = "Admin")]
        public Task<IActionResult> Register([FromBody] UserAccountRegisterRequest request)
        {
            return _userAccountService.Register(request);
        }

        [HttpPost("add-user-example"), AllowAnonymous]
        public async Task<IActionResult>  AddUserExample()
        {
            var userAdmin = new UserAccountRegisterRequest
            {
                Email = "admin@example.com",
                Password = "string",
                ConfirmPassword = "string",
                Role = RoleType.Admin
            };

            var userFinancialDepartment = new UserAccountRegisterRequest
            {
                Email = "findep@example.com",
                Password = "string",
                ConfirmPassword = "string",
                Role = RoleType.FinancialDepartment
            };
            
            var userValidationDepartment = new UserAccountRegisterRequest
            {
                Email = "validdep@example.com",
                Password = "string",
                ConfirmPassword = "string",
                Role = RoleType.ValidationDepartment
            };
            
            var userCustomerCareDepartment = new UserAccountRegisterRequest
            {
                Email = "customercaredep@example.com",
                Password = "string",
                ConfirmPassword = "string",
                Role = RoleType.CustomerCareDepartment
            };
            try
            {
                await _userAccountService.Register(userAdmin);
                await _userAccountService.Register(userFinancialDepartment);
                await _userAccountService.Register(userValidationDepartment);
                await _userAccountService.Register(userCustomerCareDepartment);
                return new OkObjectResult("Users example data added successfully");
            } catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
            
            
        }
        

        [HttpPost("login"), AllowAnonymous]
        public Task<IActionResult> Login([FromBody] UserAccountLoginRequest request)
        {
            return _userAccountService.Login(request);
        }

        [HttpGet("get-user-account"), Authorize(Roles = "Admin")]
        public Task<IActionResult> GetUserAccount()
        {
            return _userAccountService.GetUserAccountAsync();
        }

        [HttpGet("get-user-account/{id}"), Authorize(Roles = "Admin")]
        public Task<IActionResult> GetUserAccount(int id)
        {
            return _userAccountService.GetUserAccountAsync(id);
        }

        [HttpGet("get-role/{id}"), Authorize(Roles = "Admin")]
        public Task<string> GetRole(int id)
        {
            return _userAccountService.GetUserRoleAsync(id);
        }

        [HttpPost("verify"), AllowAnonymous]
        public Task<IActionResult> Verify([FromBody] string token)
        {
            return _userAccountService.Verify(token);
        }

        [HttpPost("forgot-password"), AllowAnonymous]
        public Task<IActionResult> ForgotPassword([FromBody] UserForgotPasswordRequest request)
        {
            return _userAccountService.ForgotPassword(request);
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequest request)
        {
            return _userAccountService.ResetPassword(request);
        }
    }
}
