using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface ICustomerService
    {
        Task<IActionResult> GetCustomerAsync();
        Task<IActionResult> GetCustomerPendingApprovalAsync();
        Task<IActionResult> GetCustomerApprovedAsync();
        Task<IActionResult> GetCustomerRejectedAsync();
        Task<IActionResult> GetCustomerAsync(int id);
        Task<IActionResult> GetHealthInformationOfCustomerAsync(int id);

        Task<IActionResult> AddCustomerAsync(CustomerRegisterRequest request);
        Task<IActionResult> DeleteCustomerForceAsync(int userId);
    }
}
