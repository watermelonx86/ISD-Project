using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserAccountService
    {
        //TODO: Using asynchronous methods
        IActionResult Register(UserRegisterRequest request);
        IActionResult Login(UserLoginRequest request);
        IActionResult Verify(string token);
        IActionResult GetUserRole(int userId);
        IActionResult ForgotPassword(UserForgotPasswordRequest request);
        IActionResult ResetPassword(UserResetPasswordRequest request);
    }
}
