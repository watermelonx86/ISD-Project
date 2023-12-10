using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserAccountService
    {
        Task<IActionResult> Register(UserAccountRegisterRequest request);
        Task<IActionResult> Login(UserAccountLoginRequest request);
        Task<IActionResult> Verify(string token);
        Task<List<string>> GetUserRoleAsync(int userId);
        Task<IActionResult> GetUserAccountAsync();
        Task<IActionResult> GetUserAccountAsync(int id);
        Task<IActionResult> ForgotPassword(UserForgotPasswordRequest request);
        Task<IActionResult> ResetPassword(UserResetPasswordRequest request);
    }
}
