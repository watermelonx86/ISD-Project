using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserAccountService
    {
        //TODO: Using asynchronous methods
        Task<IActionResult> Register(UserRegisterRequest request);
        Task<IActionResult> Login(UserLoginRequest request);
        Task<IActionResult> Verify(string token);
        Task<IActionResult> GetUserRole(int userId);
        Task<IActionResult> ForgotPassword(UserForgotPasswordRequest request);
        Task<IActionResult> ResetPassword(UserResetPasswordRequest request);
    }
}
