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
        Task<IActionResult> GetCustomer(int  id);
        Task<IActionResult> AddCustomer(UserDto request);
        Task<IActionResult> AddCustomerCareDept(UserDto request);
        Task<IActionResult> FinancialDeptAdd(UserDto financialAddRequest);
        Task<IActionResult> ValidationDeptAdd(UserDto validationAddRequest);
        Task<IActionResult> DeleteCustomer(int id);
        Task<IActionResult> DeleteCustomerCare(int id);
        Task<IActionResult> DeleteFinancialDept(int id);
        Task<IActionResult> DeleteValidationDept(int id);
    }
}
