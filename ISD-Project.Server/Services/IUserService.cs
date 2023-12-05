using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserService
    {
        Task<IActionResult> GetUser();
        Task<IActionResult> GetHealthInformationOfCustomer(int id);
        Task<IActionResult> AddCustomerCareDept(UserDto request);
        Task<IActionResult> FinancialDeptAdd(UserDto financialAddRequest);
        Task<IActionResult> ValidationDeptAdd(UserDto validationAddRequest);
        Task<IActionResult> DeleteCustomerCare(int id);
        Task<IActionResult> DeleteFinancialDept(int id);
        Task<IActionResult> DeleteValidationDept(int id);
    }
}
