using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserService
    {
        //TODO: Using asynchronous methods
        //TODO: Your code here
        Task<IActionResult> GetUser();
        Task<IActionResult> GetCustomer();
        Task<IActionResult> AddCustomer(UserDto request);
        Task<IActionResult> AddCustomerCareDept(UserDto request);

    }
}
