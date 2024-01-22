﻿using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<IActionResult> Register(UserAccountRegisterRequest request);
        Task<IActionResult> Login(UserAccountLoginRequest request);
        Task<IActionResult> Verify(string token);
        Task<string> GetUserRoleAsync(int userId);
        Task<IActionResult> GetUserAccountAsync();
        Task<IActionResult> GetUserAccountAsync(int id);
        Task<IActionResult> ForgotPassword(UserForgotPasswordRequest request);
        Task<IActionResult> ResetPassword(UserResetPasswordRequest request);
        Task<IActionResult> EditInfoUserAsync(UserUpdateModel request);
    }
}
