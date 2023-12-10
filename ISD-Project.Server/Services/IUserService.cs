using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserService
    {
        Task<IActionResult> GetUserAsync();
        Task<IActionResult> GetUserByIdAsync(int id);
        Task<IActionResult> GetUserByRoleAsync(RoleType role);

    }
}
